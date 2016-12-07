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

        AutomationElementCollection aePanes = null;
        AutomationElement aeFaceDownCardStackPane = null;
        AutomationElement aeDealtStackPane = null;
        AutomationElement aeSuitStacksPane = null;
        AutomationElement aeRowStacksPane = null;
        AutomationElement aeDeckPane = null;
        AutomationElement aeStackPane1 = null;
        AutomationElement aeStackPane2 = null;
        AutomationElement aeStackPane3 = null;
        AutomationElement aeStackPane4 = null;
        AutomationElement aeStackPane5 = null;
        AutomationElement aeStackPane6 = null;
        AutomationElement aeStackPane7 = null;

        AutomationElement aeCardDeckButton = null;
        AutomationElement aeDealtSpaceButton = null;
        AutomationElement aeSuitStackButton1 = null;
        AutomationElement aeSuitStackButton2 = null;
        AutomationElement aeSuitStackButton3 = null;
        AutomationElement aeSuitStackButton4 = null;
        AutomationElement aeStackButton1 = null;
        AutomationElement aeStackButton2 = null;
        AutomationElement aeStackButton3 = null;
        AutomationElement aeStackButton4 = null;
        AutomationElement aeStackButton5 = null;
        AutomationElement aeStackButton6 = null;
        AutomationElement aeStackButton7 = null;

        public Solitaire()
        {
            Console.WriteLine("Launching Solitaire application");
            solitaire = Process.Start("C:\\Program Files\\Microsoft Games\\Solitaire\\Solitaire.exe");

            findProcess();
            findDesktop();
            findMainWindow();
        }

        private void findProcess()
        {
            int ct = 0;
            do
            {
                Console.WriteLine("Looking for Solitaire process. . .");
                ++ct;
                Thread.Sleep(100);
            } while (solitaire == null && ct < 50);

            if (solitaire == null)
            {
                throw new Exception("Failed to find Solitaire process");
            }
            else
            {
                Console.WriteLine("Found Solitaire process");
            }
        }

        private void findDesktop()
        {
            Console.WriteLine("\nGetting Desktop");
            aeDesktop = AutomationElement.RootElement;
            if (aeDesktop == null)
            {
                throw new Exception("Unable to get Desktop");
            }
            else
            {
                Console.WriteLine("Found Desktop\n");
            }
        }

        private void findMainWindow()
        {
            int numWaits = 0;
            do
            {
                Console.WriteLine("Looking for Solitaire mian window. . .");
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
                Console.WriteLine("Found Solitaire main window");
            }
        }

        public void getUserControls()
        {
            getPanes();
        }

        private void getPanes()
        {
            Console.WriteLine("Looking for panes");
            aePanes = aeSolitaire.FindAll(TreeScope.Children, new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Pane));
            if (aePanes == null)
            {
                throw new Exception("No panes collection");
            }
            else
            {
                Console.WriteLine("Got panes collection");
            }
        }
    }
}