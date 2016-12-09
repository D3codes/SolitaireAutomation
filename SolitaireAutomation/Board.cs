using System;
using System.Diagnostics;
using System.Windows.Automation;

namespace SolitaireAutomation
{
    internal class Board
    {
        public AutomationElement deck;
        public Card dealSpace;
        public Card[] suitStacks;
        public Card[] rowStacksBottom;
        public Card[] rowStacksTop;
        int dealSpaceCounter;

        public Board()
        {
            suitStacks = new Card[4];
            rowStacksBottom = new Card[7];
            rowStacksTop = new Card[7];
        }

        public void print()
        {
            Debug.Write("DealSpace: ");
            if (dealSpace.getRank() != Rank.JOKER)
            {
                Debug.WriteLine(dealSpace.toString());
            }
            else
            {
                Debug.WriteLine("empty");
            }

            Debug.Write("SuitStacks: ");
            for(int i = 0; i < 4; i++)
            {
                if(suitStacks[i].getRank() != Rank.JOKER)
                {
                    Debug.Write(suitStacks[i].toString());
                }
                else
                {
                    Debug.Write("empty");
                }
                Debug.Write(", ");
            }

            Debug.Write("\nRowStacksTop: ");
            for(int i = 0; i < 7; i++)
            {
                if(rowStacksTop[i].getRank() != Rank.JOKER)
                {
                    Debug.Write(rowStacksTop[i].toString());
                }
                else
                {
                    Debug.Write("empty");
                }
                Debug.Write(", ");
            }

            Debug.Write("\nRowStacksBottom: ");
            for(int i = 0; i < 7; i++)
            {
                if(rowStacksBottom[i].getRank() != Rank.JOKER)
                {
                    Debug.Write(rowStacksBottom[i].toString());
                }
                else
                {
                    Debug.Write("empty");
                }
                Debug.Write(", ");
            }
            Debug.WriteLine("");
        }

        public void setBoard(AutomationElementCollection aeButtons, AutomationElementCollection aePanes)
        {
            dealSpaceCounter = 0;
            for (int i = 0; i < 7; i++)
            {
                rowStacksTop[i] = null;
                rowStacksBottom[i] = null;
            }
            for(int i = 0; i < 4; i++)
            {
                suitStacks[i] = null;
            }

            foreach(AutomationElement ae in aeButtons)
            {
                string[] elementName = ae.GetCurrentPropertyValue(AutomationElement.NameProperty).ToString().Split(' ');
                switch(elementName[0])
                {
                    case "Deck,":
                        deck = ae;
                        break;

                    case "Card":
                        deck = ae;
                        break;

                    case "DealSpace":
                        dealSpace = Card.emptySpace(ae);
                        break;

                    case "Deal":
                        setDealSpace(elementName, ae);
                        break;

                    case "Suit":
                        setEmptySuitStack(elementName, ae);
                        break;

                    case "Stack1,":
                        rowStacksTop[0] = Card.emptySpace(ae);
                        rowStacksBottom[0] = Card.emptySpace(ae);
                        break;

                    case "Stack2,":
                        rowStacksTop[1] = Card.emptySpace(ae);
                        rowStacksBottom[1] = Card.emptySpace(ae);
                        break;

                    case "Stack3,":
                        rowStacksTop[2] = Card.emptySpace(ae);
                        rowStacksBottom[2] = Card.emptySpace(ae);
                        break;

                    case "Stack4,":
                        rowStacksTop[3] = Card.emptySpace(ae);
                        rowStacksBottom[3] = Card.emptySpace(ae);
                        break;

                    case "Stack5,":
                        rowStacksTop[4] = Card.emptySpace(ae);
                        rowStacksBottom[4] = Card.emptySpace(ae);
                        break;

                    case "Stack6,":
                        rowStacksTop[5] = Card.emptySpace(ae);
                        rowStacksBottom[5] = Card.emptySpace(ae);
                        break;

                    case "Stack7,":
                        rowStacksTop[6] = Card.emptySpace(ae);
                        rowStacksBottom[6] = Card.emptySpace(ae);
                        break;

                    case "Stack":
                        setRowStack(elementName, ae);
                        break;
                }

                checkSuitStacks(aePanes);
                checkRowStacks();
            }
        }

        private void setEmptySuitStack(string[] elementName, AutomationElement ae)
        {
            switch(elementName[2])
            {
                case "1":
                    suitStacks[0] = Card.emptySpace(ae);
                    break;

                case "2":
                    suitStacks[1] = Card.emptySpace(ae);
                    break;

                case "3":
                    suitStacks[2] = Card.emptySpace(ae);
                    break;

                case "4":
                    suitStacks[3] = Card.emptySpace(ae);
                    break;
            }
        }

        private void checkSuitStacks(AutomationElementCollection aePanes)
        {
            foreach (AutomationElement pane in aePanes)
            {
                string[] paneName = pane.GetCurrentPropertyValue(AutomationElement.NameProperty).ToString().Split(' ');
                if (pane.GetCurrentPropertyValue(AutomationElement.NameProperty).ToString().StartsWith("Suit Stack 1"))
                {    
                    suitStacks[0] = new Card(paneName[3] + " " + paneName[4] + " " + paneName[5], pane);

                } else if(pane.GetCurrentPropertyValue(AutomationElement.NameProperty).ToString().StartsWith("Suit Stack 2"))
                {
                    suitStacks[1] = new Card(paneName[3] + " " + paneName[4] + " " + paneName[5], pane);

                } else if(pane.GetCurrentPropertyValue(AutomationElement.NameProperty).ToString().StartsWith("Suit Stack 3"))
                {
                    suitStacks[2] = new Card(paneName[3] + " " + paneName[4] + " " + paneName[5], pane);

                } else if(pane.GetCurrentPropertyValue(AutomationElement.NameProperty).ToString().StartsWith("Suit Stack 4"))
                {
                    suitStacks[3] = new Card(paneName[3] + " " + paneName[4] + " " + paneName[5], pane);
                }
            }
        }

        private void setDealSpace(string[] elementName, AutomationElement ae)
        {
            if(int.Parse(elementName[3]) > dealSpaceCounter)
            {
                dealSpaceCounter = int.Parse(elementName[3]);
                dealSpace = new Card(elementName[4] + " " + elementName[5] + " " + elementName[6], ae);
            }
        }

        private void setRowStack(string[] elementName, AutomationElement ae)
        {
            switch (elementName[1])
            {
                case "1":
                    if (rowStacksTop[0] == null)
                    {
                        rowStacksTop[0] = new Card(elementName[4] + " " + elementName[5] + " " + elementName[6], ae);
                    }
                    else
                    {
                        rowStacksBottom[0] = new Card(elementName[4] + " " + elementName[5] + " " + elementName[6], ae);
                    }
                    break;

                case "2":
                    if (rowStacksTop[1] == null)
                    {
                        rowStacksTop[1] = new Card(elementName[4] + " " + elementName[5] + " " + elementName[6], ae);
                    }
                    else
                    {
                        rowStacksBottom[1] = new Card(elementName[4] + " " + elementName[5] + " " + elementName[6], ae);
                    }
                    break;

                case "3":
                    if (rowStacksTop[2] == null)
                    {
                        rowStacksTop[2] = new Card(elementName[4] + " " + elementName[5] + " " + elementName[6], ae);
                    }
                    else
                    {
                        rowStacksBottom[2] = new Card(elementName[4] + " " + elementName[5] + " " + elementName[6], ae);
                    }
                    break;

                case "4":
                    if (rowStacksTop[3] == null)
                    {
                        rowStacksTop[3] = new Card(elementName[4] + " " + elementName[5] + " " + elementName[6], ae);
                    }
                    else
                    {
                        rowStacksBottom[3] = new Card(elementName[4] + " " + elementName[5] + " " + elementName[6], ae);
                    }
                    break;

                case "5":
                    if (rowStacksTop[4] == null)
                    {
                        rowStacksTop[4] = new Card(elementName[4] + " " + elementName[5] + " " + elementName[6], ae);
                    }
                    else
                    {
                        rowStacksBottom[4] = new Card(elementName[4] + " " + elementName[5] + " " + elementName[6], ae);
                    }
                    break;

                case "6":
                    if (rowStacksTop[5] == null)
                    {
                        rowStacksTop[5] = new Card(elementName[4] + " " + elementName[5] + " " + elementName[6], ae);
                    }
                    else
                    {
                        rowStacksBottom[5] = new Card(elementName[4] + " " + elementName[5] + " " + elementName[6], ae);
                    }
                    break;

                default:
                    if (rowStacksTop[6] == null)
                    {
                        rowStacksTop[6] = new Card(elementName[4] + " " + elementName[5] + " " + elementName[6], ae);
                    }
                    else
                    {
                        rowStacksBottom[6] = new Card(elementName[4] + " " + elementName[5] + " " + elementName[6], ae);
                    }
                    break;
            }
        }

        private void checkRowStacks()
        {
            for(int i = 0; i < 7; i++)
            {
                if(rowStacksBottom[i] == null)
                {
                    rowStacksBottom[i] = rowStacksTop[i];
                }
            }
        }
    }
}