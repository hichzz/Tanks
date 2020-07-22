using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Tanks.Models;
using Tanks.Views;

namespace Tanks.Controllers
{
    public class PackmanController
    {
        private KolobokView kolobokView;
        private BulletView bulletView;
        private TankView tankView;
        private FieldView fieldView;

        private GameDirector gameDirector;
        private FieldCreator fieldCreator;
        private Field field;

        private MainForm mainForm;
        private Report report;

        private System.Windows.Forms.Timer moveTimer;
        private System.Windows.Forms.Timer shootTimer;
        private int countTimer;

        private KeyEventHandler keyEvent;
        private GameDirector.GameOverHandler gameOverHandler;

        private Random random;

        public Kolobok Kolobok;

        public PackmanController(MainForm mainForm, GameDirector gameDirector, FieldCreator fieldCreator)
        {            
            report = new Report();

            random = new Random((int)DateTime.Now.Ticks);

            this.fieldCreator = fieldCreator;
            this.gameDirector = gameDirector;
            this.mainForm = mainForm;

            InitHandlers();
            InitTimers(fieldCreator.ObjectsSpeed);
        }

        private void InitTimers(int interval)
        {
            InitMoveTimer(interval);
            InitShootTimer(interval);
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

        private void Timer_Tick(object sender, EventArgs e)
        {
            countTimer++;

            gameDirector.Move(Kolobok, field);
            gameDirector.HandleTanks(field, countTimer, random);

            fieldView.UpdateField(bulletView, kolobokView, tankView, mainForm.MapPictureBox);
            report.RefreshData(Kolobok, field);
        }

        private void Shoot_Tick(object sender, EventArgs e)
        {
            gameDirector.BulletMove(field);
        }

        public void StartGameButton_Click(object sender, EventArgs e)
        {
            mainForm.KeyUp += keyEvent;
            StartGame();
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

        private void InitHandlers()
        {
            mainForm.StartGameButton.Click += new EventHandler(StartGameButton_Click);
            gameOverHandler += new GameDirector.GameOverHandler(GameOver);
            keyEvent += new KeyEventHandler(KeyReaction_KeyUp);
        }

        private void CreateKolobok()
        {
            FieldObject freeCell = field.Grounds.First();
            Kolobok = new Kolobok(freeCell.Position.X, freeCell.Position.Y, FieldObjectType.Kolobok);
            kolobokView = new KolobokView(Kolobok);
        }

        private void CreateTanks()
        {
            List<FieldObject> freeCells =
                    field.Grounds.Where(g => g.Position.X != Kolobok.Position.X
                    && g.Position.Y != Kolobok.Position.Y)
                    .ToList();

            for (int i = 0; i < field.CountEnemies; i++)
            {
                FieldObject freeCell = freeCells[random.Next(freeCells.Count)];                
                field.Tanks.Add(new Tank(freeCell.Position.X, freeCell.Position.Y, FieldObjectType.Tank));
                freeCells.Remove(freeCell);
            }
            tankView = new TankView(field.Tanks);
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

        private void InitGameMap()
        {
            field = fieldCreator.CreateGameMap();
            fieldView = new FieldView(field);
            bulletView = new BulletView(field);

            CreateKolobok();
            CreateTanks();
        }

        private void SubscribeToGameEvents()
        {
            gameDirector.GameOverNotify += gameOverHandler;
            gameDirector.GameScoreChanged += new GameDirector.GameScoreChangeHandler(UpdateGameScore);
        }

        private void LoadGameData()
        {
            fieldView.UpdateField(bulletView, kolobokView, tankView, mainForm.MapPictureBox);
            mainForm.MapPictureBox.Visible = true;

            report.LoadReportData(Kolobok, field);
            mainForm.Focus();
        }

        public void StartGame()
        {
            InitGameMap();
            SubscribeToGameEvents();
            StartTimers();
            LoadGameData();
        }
    }
}
