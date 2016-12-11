using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Windows.Automation;

namespace SolitaireAutomation
{
    internal class Board
    {
        static int NUMBER_OF_SUIT_STACKS = 4;
        static int NUMBER_OF_ROW_STACKS = 7;

        public AutomationElement deck;
        public Card dealSpace;
        public Card[] suitStacks;
        public Card[] rowStacksBottom;
        public Card[] rowStacksTop;
        int dealSpaceCounter;

        public Board()
        {
            suitStacks = new Card[NUMBER_OF_SUIT_STACKS];
            rowStacksBottom = new Card[NUMBER_OF_ROW_STACKS];
            rowStacksTop = new Card[NUMBER_OF_ROW_STACKS];
        }

        public void print()
        {
            Debug.Write("DealSpace: ");
            if (dealSpace.getRank() != Rank.EMPTY)
            {
                Debug.WriteLine(dealSpace.toString());
            }
            else
            {
                Debug.WriteLine("empty");
            }

            Debug.Write("SuitStacks: ");
            for(int i = 0; i < suitStacks.Length; i++)
            {
                if(suitStacks[i].getRank() != Rank.EMPTY)
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
            for(int i = 0; i < rowStacksTop.Length; i++)
            {
                if(rowStacksTop[i].getRank() != Rank.EMPTY)
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
            for(int i = 0; i < rowStacksBottom.Length; i++)
            {
                if(rowStacksBottom[i].getRank() != Rank.EMPTY)
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

        public void setBoard(List<AutomationElement> aeButtons, AutomationElement aeSuitStacksPane)
        {
            dealSpaceCounter = 0;
            for (int i = 0; i < rowStacksTop.Length; i++)
            {
                rowStacksTop[i] = null;
                rowStacksBottom[i] = null;
            }
            for(int i = 0; i < suitStacks.Length; i++)
            {
                suitStacks[i] = null;
            }

            foreach(AutomationElement ae in aeButtons)
            {
                string[] elementName = ae.GetCurrentPropertyValue(AutomationElement.NameProperty).ToString().Split(' ');
                switch(elementName[0])
                {
                    case "Deck,":
                    case "Card":
                        deck = ae;
                        break;

                    case "DealSpace":
                        dealSpace = Card.emptySpace(ae);
                        break;

                    case "Deal":
                        setDealSpace(elementName, ae);
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

                    case "End":
                        endGame(ae);
                        return;

                    case "Play":
                        playAgain(ae);
                        return;
                }
            }

            checkSuitStacks(aeSuitStacksPane);
            checkRowStacks();
        }

        private void playAgain(AutomationElement playAgainButton)
        {
            InvokePattern ipClickCard = (InvokePattern)playAgainButton.GetCurrentPattern(InvokePattern.Pattern);
            ipClickCard.Invoke();
            Thread.Sleep(1500);
        }

        private void endGame(AutomationElement endGameButton)
        {
            InvokePattern ipClickCard = (InvokePattern)endGameButton.GetCurrentPattern(InvokePattern.Pattern);
            ipClickCard.Invoke();
            Thread.Sleep(1500);
        }

        private void setEmptySuitStack(int suitStackNumber, AutomationElement ae)
        {
            switch(suitStackNumber)
            {
                case 1:
                    suitStacks[0] = Card.emptySpace(ae);
                    break;

                case 2:
                    suitStacks[1] = Card.emptySpace(ae);
                    break;

                case 3:
                    suitStacks[2] = Card.emptySpace(ae);
                    break;

                case 4:
                    suitStacks[3] = Card.emptySpace(ae);
                    break;
            }
        }

        private void checkSuitStacks(AutomationElement aeSuitStacksPane)
        {
            AutomationElementCollection aeSuitPanes = aeSuitStacksPane.FindAll(TreeScope.Children, new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Pane));
            foreach (AutomationElement pane in aeSuitPanes)
            {
                string paneName = pane.GetCurrentPropertyValue(AutomationElement.NameProperty).ToString();
                string[] paneNameArray = paneName.Split(' ');
                if (paneName.StartsWith("Suit Stack 1"))
                {    
                    suitStacks[0] = new Card(paneNameArray[3] + " " + paneNameArray[4] + " " + paneNameArray[5], pane, int.Parse(paneNameArray[2]));

                } else if(paneName.StartsWith("Suit Stack 2"))
                {
                    suitStacks[1] = new Card(paneNameArray[3] + " " + paneNameArray[4] + " " + paneNameArray[5], pane, int.Parse(paneNameArray[2]));

                } else if(paneName.StartsWith("Suit Stack 3"))
                {
                    suitStacks[2] = new Card(paneNameArray[3] + " " + paneNameArray[4] + " " + paneNameArray[5], pane, int.Parse(paneNameArray[2]));

                } else if(paneName.StartsWith("Suit Stack 4"))
                {
                    suitStacks[3] = new Card(paneNameArray[3] + " " + paneNameArray[4] + " " + paneNameArray[5], pane, int.Parse(paneNameArray[2]));
                }
            }

            AutomationElementCollection aeSuitButtons = aeSuitStacksPane.FindAll(TreeScope.Children, new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Button));
            foreach(AutomationElement button in aeSuitButtons)
            {
                string[] buttonNameArray = button.GetCurrentPropertyValue(AutomationElement.NameProperty).ToString().Split(' ');
                setEmptySuitStack(int.Parse(buttonNameArray[2]), button);
            }
        }

        private void setDealSpace(string[] elementName, AutomationElement ae)
        {
            if(int.Parse(elementName[3]) > dealSpaceCounter)
            {
                dealSpaceCounter = int.Parse(elementName[3]);
                dealSpace = new Card(elementName[4] + " " + elementName[5] + " " + elementName[6], ae, int.Parse(elementName[3]));
            }
        }

        private void setRowStack(string[] elementName, AutomationElement ae)
        {
            int rowStackNumber = int.Parse(elementName[1]);
            switch (rowStackNumber)
            {
                case 1:
                    if (rowStacksTop[0] == null)
                    {
                        rowStacksTop[0] = new Card(elementName[4] + " " + elementName[5] + " " + elementName[6], ae, int.Parse(elementName[3]));
                    }
                    else
                    {
                        rowStacksBottom[0] = new Card(elementName[4] + " " + elementName[5] + " " + elementName[6], ae, int.Parse(elementName[3]));
                    }
                    break;

                case 2:
                    if (rowStacksTop[1] == null)
                    {
                        rowStacksTop[1] = new Card(elementName[4] + " " + elementName[5] + " " + elementName[6], ae, int.Parse(elementName[3]));
                    }
                    else
                    {
                        rowStacksBottom[1] = new Card(elementName[4] + " " + elementName[5] + " " + elementName[6], ae, int.Parse(elementName[3]));
                    }
                    break;

                case 3:
                    if (rowStacksTop[2] == null)
                    {
                        rowStacksTop[2] = new Card(elementName[4] + " " + elementName[5] + " " + elementName[6], ae, int.Parse(elementName[3]));
                    }
                    else
                    {
                        rowStacksBottom[2] = new Card(elementName[4] + " " + elementName[5] + " " + elementName[6], ae, int.Parse(elementName[3]));
                    }
                    break;

                case 4:
                    if (rowStacksTop[3] == null)
                    {
                        rowStacksTop[3] = new Card(elementName[4] + " " + elementName[5] + " " + elementName[6], ae, int.Parse(elementName[3]));
                    }
                    else
                    {
                        rowStacksBottom[3] = new Card(elementName[4] + " " + elementName[5] + " " + elementName[6], ae, int.Parse(elementName[3]));
                    }
                    break;

                case 5:
                    if (rowStacksTop[4] == null)
                    {
                        rowStacksTop[4] = new Card(elementName[4] + " " + elementName[5] + " " + elementName[6], ae, int.Parse(elementName[3]));
                    }
                    else
                    {
                        rowStacksBottom[4] = new Card(elementName[4] + " " + elementName[5] + " " + elementName[6], ae, int.Parse(elementName[3]));
                    }
                    break;

                case 6:
                    if (rowStacksTop[5] == null)
                    {
                        rowStacksTop[5] = new Card(elementName[4] + " " + elementName[5] + " " + elementName[6], ae, int.Parse(elementName[3]));
                    }
                    else
                    {
                        rowStacksBottom[5] = new Card(elementName[4] + " " + elementName[5] + " " + elementName[6], ae, int.Parse(elementName[3]));
                    }
                    break;

                case 7:
                    if (rowStacksTop[6] == null)
                    {
                        rowStacksTop[6] = new Card(elementName[4] + " " + elementName[5] + " " + elementName[6], ae, int.Parse(elementName[3]));
                    }
                    else
                    {
                        rowStacksBottom[6] = new Card(elementName[4] + " " + elementName[5] + " " + elementName[6], ae, int.Parse(elementName[3]));
                    }
                    break;
            }
        }

        private void checkRowStacks()
        {
            for(int i = 0; i < rowStacksBottom.Length; i++)
            {
                if(rowStacksBottom[i] == null)
                {
                    rowStacksBottom[i] = rowStacksTop[i];
                }
            }
        }
    }
}