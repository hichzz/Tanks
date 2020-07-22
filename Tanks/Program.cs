using System;
using System.Windows.Forms;
using Tanks.Controllers;

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
            FieldCreator fieldCreator = new FieldCreator(args);

            PackmanController packmanController = new PackmanController(mainForm, gameDirector, fieldCreator); 

            Application.Run(mainForm);
        }
    }
}
