using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using Tanks.Models;
using Tanks.Views;

namespace Tanks.Controllers
{
    public class PackmanController
    {
        private KolobokView kolobokView;
        private TankView tankView;
        private FieldView fieldView;
        private Field field;
        public Kolobok Kolobok;
        private MainForm mainForm;
        private System.Windows.Forms.Timer timer;
        private GameDirector gameDirector;
        public PackmanController(Field field, MainForm mainForm, FieldView fieldView, GameDirector gameDirector)
        {
            this.gameDirector = gameDirector;
            this.field = field;
            this.mainForm = mainForm;
            this.fieldView = fieldView;
            this.mainForm.StartGameButton.Click += new EventHandler(StartGameButton_Click);
        }

        private void CreateKolobok()
        {
            FieldObject freeCell = field.Grounds.First();
            Kolobok = new Kolobok(freeCell.Position.X, freeCell.Position.Y, FieldObjectType.Kolobok);
            kolobokView = new KolobokView(Kolobok);
        }

        private void CreateTanks()
        {
            Random random = new Random();
            for (int i = 0; i < field.CountEnemies; i++)
            {
                FieldObject freeCell = field.Grounds[random.Next(field.Grounds.Count)];

                field.Tanks.Add(new Tank(freeCell.Position.X, freeCell.Position.Y, FieldObjectType.Tank));                
            }
            tankView = new TankView(field.Tanks);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            gameDirector.Move(Kolobok, field);
            fieldView.UpdateField(kolobokView, tankView, mainForm.MapPictureBox, mainForm.GameScoreLabel);
        }

        public void StartGameButton_Click(object sender, EventArgs e)
        {
            mainForm.KeyUp += new KeyEventHandler(ChangeDirection_KeyUp);
            StartGame(MovingObject.DefaultSpeed);
        }
        public void StartGame(int movingSpeed) 
        {
            CreateKolobok();
            CreateTanks();

            fieldView.UpdateField(kolobokView, tankView, mainForm.MapPictureBox, mainForm.GameScoreLabel);
            mainForm.MapPictureBox.Visible = true;

            timer = new System.Windows.Forms.Timer();
            timer.Interval = movingSpeed;
            timer.Tick += new EventHandler(Timer_Tick);
            timer.Start();
        }

        public void ChangeDirection_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    Kolobok.Direction = Direction.Left;
                    break;
                case Keys.Right:
                    Kolobok.Direction = Direction.Right;
                    break;
                case Keys.Down:
                    Kolobok.Direction = Direction.Down;
                    break;
                case Keys.Up:
                    Kolobok.Direction = Direction.Up;
                    break;
            }
        }

    }
}
