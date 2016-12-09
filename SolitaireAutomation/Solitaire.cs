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
        AutomationElementCollection aePanes = null;
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
            getButtons();
            getPanes();
            board.setBoard(aeButtons, aePanes);
            board.print();
        }

        private void getButtons()
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

        private void getPanes()
        {
            Debug.WriteLine("Looking for panes");
            aePanes = aeSolitaire.FindAll(TreeScope.Descendants, new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Pane));
            if(aePanes == null)
            {
                throw new Exception("No panes collection");
            } else
            {
                Debug.WriteLine("Got panes collection");
            }
        }

        public void play()
        {
            while(true)
            {
                refreshBoard();

            }
        }
    }
}