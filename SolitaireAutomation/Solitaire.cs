using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Threading;
using System.Windows.Automation;
using System.Collections.Generic;

namespace SolitaireAutomation
{
    internal class Solitaire
    {
        static int NUMBER_OF_SUIT_STACKS = 4;
        static int NUMBER_OF_ROW_STACKS = 7;

        Process solitaire = null;
        AutomationElement aeDesktop = null;
        AutomationElement aeSolitaire = null;
        AutomationElement aeDeckButton = null;
        AutomationElementCollection aeDealSpaceButtons = null;
        AutomationElementCollection aeRowStacksButtons = null;
        List<AutomationElement> aeButtons;
        AutomationElement aeSuitStacksPane = null;
        Board board;

        PropertyCondition pcSolitaire = new PropertyCondition(AutomationElement.NameProperty, "Solitaire");
        PropertyCondition pcButton = new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Button);
        PropertyCondition pcFaceDownCardStack = new PropertyCondition(AutomationElement.NameProperty, "Face Down Card Stack");
        PropertyCondition pcDealtStack = new PropertyCondition(AutomationElement.NameProperty, "Dealt Stack");
        PropertyCondition pcRowStacks = new PropertyCondition(AutomationElement.NameProperty, "Row Stacks");
        PropertyCondition pcNoMoreMoves = new PropertyCondition(AutomationElement.NameProperty, "No More Moves");
        PropertyCondition pcEndGame = new PropertyCondition(AutomationElement.NameProperty, "End Game");
        PropertyCondition pcGameLost = new PropertyCondition(AutomationElement.NameProperty, "Game Lost");
        PropertyCondition pcPlayAgain = new PropertyCondition(AutomationElement.NameProperty, "Play again");
        PropertyCondition pcSuitStacks = new PropertyCondition(AutomationElement.NameProperty, "Suit Stacks");

        public Solitaire()
        {
            Debug.WriteLine("Launching Solitaire application");
            solitaire = Process.Start("C:\\Program Files\\Microsoft Games\\Solitaire\\Solitaire.exe");

            findProcess();
            findDesktop();
            findMainWindow();
            aeButtons = new List<AutomationElement>();

            board = new Board();

            setDeckButton();
            setDealSpaceButtons();
            setRowStacksButtons();
            setSuitStackPane();
        }

        private void findProcess()
        {
            int count = 0;
            do
            {
                Debug.WriteLine("Looking for Solitaire process. . .");
                ++count;
                Thread.Sleep(100);
            } while (solitaire == null && count < 50);

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
                aeSolitaire = aeDesktop.FindFirst(TreeScope.Children, pcSolitaire);
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
                setButtons();
                board.setBoard(aeButtons, aeSuitStacksPane);

            } while (board.rowStacksTop[0] == null);
            //board.print();
        }

        private void setDeckButton()
        {
            Debug.WriteLine("Setting Deck Button. . .");
            AutomationElement aeDeckPane = aeSolitaire.FindFirst(TreeScope.Children, pcFaceDownCardStack);
            aeDeckButton = aeDeckPane.FindFirst(TreeScope.Descendants, pcButton);
        }

        private void setDealSpaceButtons()
        {
            Debug.WriteLine("Setting Deal Space Buttons. . .");
            AutomationElement aeDealSpacePane = aeSolitaire.FindFirst(TreeScope.Children, pcDealtStack);
            aeDealSpaceButtons = aeDealSpacePane.FindAll(TreeScope.Descendants, pcButton);
        }

        private void setRowStacksButtons()
        {
            Debug.WriteLine("Setting Row Stacks Buttons. . .");
            AutomationElement aeRowStacksPane = aeSolitaire.FindFirst(TreeScope.Children, pcRowStacks);
            aeRowStacksButtons = aeRowStacksPane.FindAll(TreeScope.Descendants, pcButton);
        }

        private void setButtons()
        {
            aeButtons.Clear();

            AutomationElement noMoreMoves = aeSolitaire.FindFirst(TreeScope.Children, pcNoMoreMoves);
            if (noMoreMoves != null)
            {
                AutomationElement endGame = noMoreMoves.FindFirst(TreeScope.Children, pcEndGame);
                aeButtons.Add(endGame);
            }
            AutomationElement gameLost = aeSolitaire.FindFirst(TreeScope.Children, pcGameLost);
            if (gameLost != null)
            {
                AutomationElement playAgain = gameLost.FindFirst(TreeScope.Children, pcPlayAgain);
                aeButtons.Add(playAgain);
            }

            aeButtons.Add(aeDeckButton);
            foreach(AutomationElement button in aeDealSpaceButtons)
            {
                aeButtons.Add(button);
            }
            foreach(AutomationElement button in aeRowStacksButtons)
            {
                aeButtons.Add(button);
            }
        }

        private void setSuitStackPane()
        {
            Debug.WriteLine("Setting suit stack pane. . .");
            aeSuitStacksPane = aeSolitaire.FindFirst(TreeScope.Children, pcSuitStacks);
        }

        public void play()
        {
            Debug.WriteLine("Playing Solitaire. . .");
            while(true)
            {
                refreshBoard();

                if(checkRowStacksForMove())
                {
                    setRowStacksButtons();
                    continue;
                }

                if(checkDealSpaceForMove())
                {
                    setDealSpaceButtons();
                    setRowStacksButtons();
                    continue;
                }

                if(checkSuitSpaceForMove())
                {
                    setDealSpaceButtons();
                    setRowStacksButtons();
                    setSuitStackPane();
                    continue;
                }

                clickDeck();
                setDeckButton();
                setDealSpaceButtons();
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
            for(int i = 0; i < NUMBER_OF_SUIT_STACKS; i++)
            {
                for(int j = 0; j < NUMBER_OF_ROW_STACKS; j++)
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
            for(int i = 0; i < NUMBER_OF_ROW_STACKS; i++)
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
            for (int i = NUMBER_OF_ROW_STACKS; i > 0; i--)
            {
                for (int j = 0; j < NUMBER_OF_ROW_STACKS; j++)
                {
                    if(canMove(board.rowStacksTop[i-1], board.rowStacksBottom[j]))
                    {
                        Debug.WriteLine("Move found");
                        move(board.rowStacksTop[i-1], board.rowStacksBottom[j]);
                        return true;
                    }
                }
            }
            Debug.WriteLine("No moves found");
            return false;
        }

        private bool canSendHome(Card c1, Card c2)
        {
            if(c2.getRank() == Rank.ACE && c1.getRank() == Rank.EMPTY)
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
            if(c1.getRank() == Rank.EMPTY)
            {
                return false;
            }

            if(c2.getRank() == Rank.EMPTY)
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