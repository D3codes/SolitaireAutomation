using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Threading;
using System.Windows.Automation;

namespace SolitaireAutomation
{
    [TestClass]
    public class SolitaireAutomation
    {
        [TestMethod]
        public void Solitaire()
        {
            Solitaire s = new Solitaire();
            s.getUserControls();
        }
    }
}
