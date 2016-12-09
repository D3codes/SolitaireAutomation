using System;
using System.Diagnostics;
using System.Windows.Automation;

namespace SolitaireAutomation
{
    internal class Board
    {
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
            if (dealSpace != null)
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
                if(suitStacks[i] != null)
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
                if(rowStacksTop[i] != null)
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
                if(rowStacksBottom[i] != null)
                {
                    Debug.Write(rowStacksBottom[i].toString());
                }
                else
                {
                    Debug.Write("empty");
                }
                Debug.Write(", ");
            }
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
                    case "DealSpace":
                        dealSpace = null;
                        break;

                    case "Deal":
                        setDealSpace(elementName, ae);
                        break;

                    case "Stack":
                        setRowStack(elementName, ae);
                        break;
                }

                checkSuitStacks(aePanes);
                checkRowStacks();
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