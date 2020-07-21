using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tanks.Controllers;
using Tanks.Models;
using Tanks.Views;

namespace Tanks
{
    static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            GameDirector gameDirector = new GameDirector();
            MainForm mainForm = new MainForm();

            PackmanController packmanController = new PackmanController(mainForm, gameDirector, args);

            Application.Run(mainForm);
        }
    }
}
