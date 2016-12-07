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
    }
}