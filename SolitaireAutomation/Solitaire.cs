using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Threading;
using System.Windows.Automation;

namespace SolitaireAutomation
{
    internal class Solitaire
    {
        Process solitaire = null;
        AutomationElement aeDesktop = null;
        AutomationElement aeSolitaire = null;
        AutomationElementCollection aeButtons = null;
        AutomationElement aeSuitStacksPane = null;
        Board board;

        public Solitaire()
        {
            Debug.WriteLine("Launching Solitaire application");
            solitaire = Process.Start("C:\\Program Files\\Microsoft Games\\Solitaire\\Solitaire.exe");

            findProcess();
            findDesktop();
            findMainWindow();

            board = new Board();
        }

        private void findProcess()
        {
            int ct = 0;
            do
            {
                Debug.WriteLine("Looking for Solitaire process. . .");
                ++ct;
                Thread.Sleep(100);
            } while (solitaire == null && ct < 50);

            if (solitaire == null)
            {
                throw new Exception("Failed to find Solitaire process");
            }
            else
            {
                Debug.WriteLine("Found Solitaire process");
            }
        }

        private void findDesktop()
        {
            Debug.WriteLine("\nGetting Desktop");
            aeDesktop = AutomationElement.RootElement;
            if (aeDesktop == null)
            {
                throw new Exception("Unable to get Desktop");
            }
            else
            {
                Debug.WriteLine("Found Desktop\n");
            }
        }

        private void findMainWindow()
        {
            int numWaits = 0;
            do
            {
                Debug.WriteLine("Looking for Solitaire main window. . .");
                aeSolitaire = aeDesktop.FindFirst(TreeScope.Children, new PropertyCondition(AutomationElement.NameProperty, "Solitaire"));
                ++numWaits;
                Thread.Sleep(200);
            } while (aeSolitaire == null && numWaits < 50);

            if(aeSolitaire == null)
            {
                throw new Exception("Fialed to find Solitaire main window");
            }
            else
            {
                Debug.WriteLine("Found Solitaire main window");
            }
        }

        public void refreshBoard()
        {
            Debug.WriteLine("Refreshing board. . .");
            do
            {
                findSuitStackPane();
                findButtons();
                board.setBoard(aeButtons, aeSuitStacksPane);

            } while (board.rowStacksTop[0] == null);
            //board.print();
        }

        private void findButtons()
        {
            Debug.WriteLine("Looking for buttons");
            aeButtons = aeSolitaire.FindAll(TreeScope.Descendants, new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Button));
            if (aeButtons == null)
            {
                throw new Exception("No buttons collection");
            }
            else
            {
                Debug.WriteLine("Got buttons collection");
            }
        }

        private void findSuitStackPane()
        {
            Debug.WriteLine("Looking for suit stack pane");
            aeSuitStacksPane = aeSolitaire.FindFirst(TreeScope.Children, new PropertyCondition(AutomationElement.NameProperty, "Suit Stacks"));
            if(aeSuitStacksPane == null)
            {
                throw new Exception("No suit stack pane");
            } else
            {
                Debug.WriteLine("Got suit stack pane");
            }
        }

        public void play()
        {
            Debug.WriteLine("Playing Solitaire. . .");
            while(true)
            {
                refreshBoard();

                if(checkRowStacksForMove())
                {
                    continue;
                }

                if(checkDealSpaceForMove())
                {
                    continue;
                }

                if(checkSuitSpaceForMove())
                {
                    continue;
                }

                clickDeck();
            }
        }

        private void clickDeck()
        {
            Debug.WriteLine("Clicking on deck. . .");
            InvokePattern ipClickCard = (InvokePattern)board.deck.GetCurrentPattern(InvokePattern.Pattern);
            ipClickCard.Invoke();
            Thread.Sleep(500);
        }
        
        private bool checkSuitSpaceForMove()
        {
            Debug.WriteLine("Checking for possible moves in suit space. . .");
            for(int i = 0; i < 4; i++)
            {
                for(int j = 0; j < 7; j++)
                {
                    if(canSendHome(board.suitStacks[i], board.rowStacksBottom[j]))
                    {
                        Debug.WriteLine("Move Found");
                        move(board.rowStacksBottom[j], board.suitStacks[i]);
                        return true;
                    }
                }
                if(canSendHome(board.suitStacks[i], board.dealSpace))
                {
                    move(board.dealSpace, board.suitStacks[i]);
                    return true;
                }
            }
            Console.WriteLine("No moves found");
            return false;
        }

        private bool checkDealSpaceForMove()
        {
            Debug.WriteLine("Checking for possible moves in deal space. . .");
            for(int i = 0; i < 7; i++)
            {
                if(canMove(board.dealSpace, board.rowStacksBottom[i]))
                {
                    Debug.WriteLine("Move found");
                    move(board.dealSpace, board.rowStacksBottom[i]);
                    return true;
                }
            }
            Debug.WriteLine("No moves found");
            return false;
        }

        private bool checkRowStacksForMove()
        {
            Debug.WriteLine("Checking for possible moves in row stacks. . .");
            for (int i = 6; i >= 0; i--)
            {
                for (int j = 0; j < 7; j++)
                {
                    if(canMove(board.rowStacksTop[i], board.rowStacksBottom[j]))
                    {
                        Debug.WriteLine("Move found");
                        move(board.rowStacksTop[i], board.rowStacksBottom[j]);
                        return true;
                    }
                }
            }
            Debug.WriteLine("No moves found");
            return false;
        }

        private bool canSendHome(Card c1, Card c2)
        {
            if(c2.getRank() == Rank.ACE && c1.getRank() == Rank.JOKER)
            {
                return true;
            }

            if(c1.getSuit() == c2.getSuit() && c1.getRank() == c2.getRank() - 1)
            {
                return true;
            }

            return false;
        }

        private bool canMove(Card c1, Card c2)
        {
            if(c1.getRank() == Rank.JOKER)
            {
                return false;
            }

            if(c2.getRank() == Rank.JOKER)
            {
                if(c1.getRank() == Rank.KING && c1.getNumberInStack() != 1)
                {
                    return true;
                } else
                {
                    return false;
                }
            }

            if((c1.isBlack() != c2.isBlack()) && (c1.getRank() == c2.getRank()-1))
            {
                return true;
            }
            return false;
        }

        private void move(Card c1, Card c2)
        {
            Debug.WriteLine("Moving card");
            c1.click();
            c2.click();
        }
    }
}