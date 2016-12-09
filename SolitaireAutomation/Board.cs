using System;
using System.Diagnostics;
using System.Windows.Automation;

namespace SolitaireAutomation
{
    internal class Board
    {
        Card dealSpace;
        Card suitStack1, suitStack2, suitStack3, suitStack4;
        Card stack1, stack2, stack3, stack4, stack5, stack6, stack7;
        int dealSpaceCounter, stack1Counter, stack2Counter, stack3Counter, stack4Counter, stack5Counter, stack6Counter, stack7Counter;

        public Board()
        {

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
            if(suitStack1 != null)
            {
                Debug.Write(suitStack1.toString());
            } else
            {
                Debug.Write("empty");
            }
            Debug.Write(", ");
            if(suitStack2 != null)
            {
                Debug.Write(suitStack2.toString());
            }
            else
            {
                Debug.Write("empty");
            }
            Debug.Write(", ");
            if(suitStack3 != null)
            {
                Debug.Write(suitStack3.toString());
            }
            else
            {
                Debug.Write("empty");
            }
            Debug.Write(", ");
            if(suitStack4 != null)
            {
                Debug.WriteLine(suitStack4.toString());
            }
            else
            {
                Debug.WriteLine("empty");
            }

            Debug.Write("RowStacks: ");
            if(stack1 != null)
            {
                Debug.Write(stack1.toString());
            }
            else
            {
                Debug.Write("empty");
            }
            Debug.Write(", ");
            if(stack2 != null)
            {
                Debug.Write(stack2.toString());
            }
            else
            {
                Debug.Write("empty");
            }
            Debug.Write(", ");
            if(stack3 != null)
            {
                Debug.Write(stack3.toString());
            }
            else
            {
                Debug.Write("empty");
            }
            Debug.Write(", ");
            if(stack4 != null)
            {
                Debug.Write(stack4.toString());
            } else
            {
                Debug.Write("empty");
            }
            Debug.Write(", ");
            if(stack5 != null)
            {
                Debug.Write(stack5.toString());
            }
            else
            {
                Debug.Write("empty");
            }
            Debug.Write(", ");
            if(stack6 != null)
            {
                Debug.Write(stack6.toString());
            }
            else
            {
                Debug.Write("empty");
            }
            Debug.Write(", ");
            if(stack7 != null)
            {
                Debug.WriteLine(stack7.toString());
            } else
            {
                Debug.WriteLine("empty");
            }
        }

        public void setBoard(AutomationElementCollection aeButtons, AutomationElementCollection aePanes)
        {
            dealSpaceCounter = 0;
            stack1Counter = 0;
            stack2Counter = 0;
            stack3Counter = 0;
            stack4Counter = 0;
            stack5Counter = 0;
            stack6Counter = 0;
            stack7Counter = 0;

            foreach(AutomationElement ae in aeButtons)
            {
                string[] elementName = ae.GetCurrentPropertyValue(AutomationElement.NameProperty).ToString().Split(' ');
                switch(elementName[0])
                {
                    case "DealSpace":
                        dealSpace = null;
                        break;

                    case "Deal":
                        setDealSpace(elementName);
                        break;

                    case "Suit":
                        setSuitStack(elementName);
                        break;

                    case "Stack1,":
                        stack1 = null;
                        break;

                    case "Stack2,":
                        stack2 = null;
                        break;

                    case "Stack3,":
                        stack3 = null;
                        break;

                    case "Stack4,":
                        stack4 = null;
                        break;

                    case "Stack5,":
                        stack5 = null;
                        break;

                    case "Stack6,":
                        stack6 = null;
                        break;

                    case "Stack7,":
                        stack7 = null;
                        break;

                    case "Stack":
                        setRowStack(elementName);
                        break;
                }

                checkSuitStacks(aePanes);
            }
        }

        private void checkSuitStacks(AutomationElementCollection aePanes)
        {
            foreach (AutomationElement pane in aePanes)
            {
                string[] paneName = pane.GetCurrentPropertyValue(AutomationElement.NameProperty).ToString().Split(' ');
                if (pane.GetCurrentPropertyValue(AutomationElement.NameProperty).ToString().StartsWith("Suit Stack 1"))
                {    
                    suitStack1 = new Card(paneName[3] + " " + paneName[4] + " " + paneName[5]);

                } else if(pane.GetCurrentPropertyValue(AutomationElement.NameProperty).ToString().StartsWith("Suit Stack 2"))
                {
                    suitStack2 = new Card(paneName[3] + " " + paneName[4] + " " + paneName[5]);

                } else if(pane.GetCurrentPropertyValue(AutomationElement.NameProperty).ToString().StartsWith("Suit Stack 3"))
                {
                    suitStack3 = new Card(paneName[3] + " " + paneName[4] + " " + paneName[5]);

                } else if(pane.GetCurrentPropertyValue(AutomationElement.NameProperty).ToString().StartsWith("Suit Stack 4"))
                {
                    suitStack4 = new Card(paneName[3] + " " + paneName[4] + " " + paneName[5]);
                }
            }
        }

        private void setDealSpace(string[] elementName)
        {
            if(int.Parse(elementName[3]) > dealSpaceCounter)
            {
                dealSpaceCounter = int.Parse(elementName[3]);
                dealSpace = new Card(elementName[4] + " " + elementName[5] + " " + elementName[6]);
            }
        }

        private void setSuitStack(string[] elementName)
        {
            switch (elementName[2])
            {
                case "1":
                    suitStack1 = null;
                    break;

                case "2":
                    suitStack2 = null;
                    break;

                case "3":
                    suitStack3 = null;
                    break;

                default:
                    suitStack4 = null;
                    break;
            }
        }

        private void setRowStack(string[] elementName)
        {
            switch (elementName[1])
            {
                case "1":
                    if (int.Parse(elementName[3]) > stack1Counter)
                    {
                        stack1Counter = int.Parse(elementName[3]);
                        stack1 = new Card(elementName[4] + " " + elementName[5] + " " + elementName[6]);
                    }
                    break;

                case "2":
                    if (int.Parse(elementName[3]) > stack2Counter)
                    {
                        stack2Counter = int.Parse(elementName[3]);
                        stack2 = new Card(elementName[4] + " " + elementName[5] + " " + elementName[6]);
                    }
                    break;

                case "3":
                    if (int.Parse(elementName[3]) > stack3Counter)
                    {
                        stack3Counter = int.Parse(elementName[3]);
                        stack3 = new Card(elementName[4] + " " + elementName[5] + " " + elementName[6]);
                    }
                    break;

                case "4":
                    if (int.Parse(elementName[3]) > stack4Counter)
                    {
                        stack4Counter = int.Parse(elementName[3]);
                        stack4 = new Card(elementName[4] + " " + elementName[5] + " " + elementName[6]);
                    }
                    break;

                case "5":
                    if (int.Parse(elementName[3]) > stack5Counter)
                    {
                        stack5Counter = int.Parse(elementName[3]);
                        stack5 = new Card(elementName[4] + " " + elementName[5] + " " + elementName[6]);
                    }
                    break;

                case "6":
                    if (int.Parse(elementName[3]) > stack6Counter)
                    {
                        stack6Counter = int.Parse(elementName[3]);
                        stack6 = new Card(elementName[4] + " " + elementName[5] + " " + elementName[6]);
                    }
                    break;

                default:
                    if (int.Parse(elementName[3]) > stack7Counter)
                    {
                        stack7Counter = int.Parse(elementName[3]);
                        stack7 = new Card(elementName[4] + " " + elementName[5] + " " + elementName[6]);
                    }
                    break;
            }
        }
    }
}