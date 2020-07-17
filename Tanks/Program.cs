using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tanks.Controllers;
using Tanks.Models;

namespace Tanks
{
    static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            PackmanController packmanController = new PackmanController(args);

            Application.Run(new MainView(packmanController.GetField()));
        }
    }
}
