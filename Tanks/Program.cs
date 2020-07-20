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

            FieldCreator fieldCreator = new FieldCreator();
            Field field = fieldCreator.CreateGameMap(args);
            FieldView fieldView = new FieldView(field);

            MainForm mainForm = new MainForm();

            PackmanController packmanController = new PackmanController(field, mainForm, fieldView, gameDirector);

            Application.Run(mainForm);
        }
    }
}
