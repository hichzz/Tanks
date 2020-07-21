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
using System.Diagnostics;

namespace Tanks.Controllers
{
    public class PackmanController
    {
        private FieldCreator fieldCreator;
        private KolobokView kolobokView;
        private BulletView bulletView;
        private TankView tankView;
        private FieldView fieldView;
        private Field field;
        public Kolobok Kolobok;
        private string[] args;
        private MainForm mainForm;
        private System.Windows.Forms.Timer moveTimer;
        private System.Windows.Forms.Timer shootTimer;
        private Report report;
        private int countTimer;
        private KeyEventHandler keyEvent;
        private GameDirector.GameOverHandler gameOverHandler;

        private GameDirector gameDirector;
        private Random random;
        public PackmanController(MainForm mainForm, GameDirector gameDirector, string[] args)
        {
            this.gameDirector = gameDirector;
            this.mainForm = mainForm;
            this.mainForm.StartGameButton.Click += new EventHandler(StartGameButton_Click);
            gameOverHandler += new GameDirector.GameOverHandler(GameOver);
            keyEvent += new KeyEventHandler(KeyReaction_KeyUp);
            random = new Random((int)DateTime.Now.Ticks);
            fieldCreator = new FieldCreator();
            this.args = args;
            report = new Report();
            InitMoveTimer(Convert.ToInt32(args[4]));
            InitShootTimer(Convert.ToInt32(args[4]));
        }

        private void CreateKolobok()
        {
            FieldObject freeCell = field.Grounds.First();
            Kolobok = new Kolobok(freeCell.Position.X, freeCell.Position.Y, FieldObjectType.Kolobok);
            kolobokView = new KolobokView(Kolobok);
            field.Grounds.RemoveAt(0);
        }

        private void CreateTanks()
        {            
            for (int i = 0; i < field.CountEnemies; i++)
            {
                FieldObject freeCell = field.Grounds[random.Next(field.Grounds.Count)];
                
                field.Tanks.Add(new Tank(freeCell.Position.X, freeCell.Position.Y, FieldObjectType.Tank));                
            }
            tankView = new TankView(field.Tanks);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            countTimer++;

            gameDirector.Move(Kolobok, field);
            gameDirector.MoveTanks(field);

            if (countTimer % Tank.ChangeDirectionDelay == 0)
                foreach (Tank tank in field.Tanks)
                    gameDirector.ChangeDirectionTank(tank, random);

            if (countTimer % Bullet.CreateBulletDelay == 0)
                foreach (Tank tank in field.Tanks)                 
                    gameDirector.CreateBullet(tank, field);

            fieldView.UpdateField(bulletView, kolobokView, tankView, mainForm.MapPictureBox);
            report.RefreshData(Kolobok, field);
        }

        private void Shoot_Tick(object sender, EventArgs e)
        {
            gameDirector.Shoot(field);
        }

        private void InitMoveTimer(int movingSpeed)
        {
            moveTimer = new System.Windows.Forms.Timer();
            moveTimer.Interval = movingSpeed;
            moveTimer.Tick += new EventHandler(Timer_Tick);
        }

        private void InitShootTimer(int movingSpeed)
        {
            shootTimer = new System.Windows.Forms.Timer();
            shootTimer.Interval = movingSpeed;
            shootTimer.Tick += new EventHandler(Shoot_Tick);
        }

        private void StartTimers()
        {
            moveTimer.Start();
            shootTimer.Start();
        }

        private void StopTimers()
        {
            moveTimer.Stop();
            shootTimer.Stop();
        }

        private void GameOver()
        {
            StopTimers();
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show("Start new game?", "Game over", buttons);
            if (result == DialogResult.Yes)
            {
                report.ClearData();
                gameDirector.GameOverNotify -= gameOverHandler;
                StartGame();
            }
            else
            {
                mainForm.Close();
            }
        }

        private void UpdateGameScore()
        {
            fieldView.ShowGameScore(mainForm.GameScoreLabel);
        }

        public void StartGameButton_Click(object sender, EventArgs e)
        {
            mainForm.KeyUp += keyEvent;
            StartGame();
        }
        public void StartGame()
        {
            field = fieldCreator.CreateGameMap(args);
            fieldView = new FieldView(field);

            CreateKolobok();
            CreateTanks();
            bulletView = new BulletView(field);

            gameDirector.GameOverNotify += gameOverHandler;
            gameDirector.GameScoreChanged += new GameDirector.GameScoreChangeHandler(UpdateGameScore);

            fieldView.UpdateField(bulletView, kolobokView, tankView, mainForm.MapPictureBox);
            mainForm.MapPictureBox.Visible = true;
            StartTimers();

            report.LoadReportData(Kolobok, field);
            mainForm.Focus();
        }

        public void KeyReaction_KeyUp(object sender, KeyEventArgs e)
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
                case Keys.S:
                    gameDirector.CreateBullet(Kolobok, field);
                    break;
            }
        }
    }
}
