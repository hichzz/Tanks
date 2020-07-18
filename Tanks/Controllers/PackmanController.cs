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
        private FieldController fieldController;
        private AppleController appleController;
        private KolobokView kolobokView;
        public Kolobok Kolobok;
        private MainView mainView;
        private System.Windows.Forms.Timer timer;
        public PackmanController(string[] args, MainView mainView)
        {
            fieldController = new FieldController(args);
            appleController = new AppleController(GetField());
            this.mainView = mainView;
            mainView.SetController(this);

            CreateKolobok();

            kolobokView = new KolobokView(Kolobok);
        }
        public Field GetField() => fieldController.Field; // TODO: remove

        private void CreateKolobok()
        {
            FieldObject freeCell = GetField().Grounds.First();
            Kolobok = new Kolobok(freeCell.Position.X, freeCell.Position.Y, FieldObjectType.Kolobok);
            Kolobok.Speed = Kolobok.DefaultSpeed;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            Move(Kolobok);
            mainView.UpdateField(kolobokView);
        }

        public void StartGame(PictureBox map) //in game director
        {
            //kolobokView.DrawKolobok(map);
            //map.Refresh();

            timer = new System.Windows.Forms.Timer();
            timer.Interval = Kolobok.Speed;
            timer.Tick += new EventHandler(timer_Tick);
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
            if (CanMove(Kolobok))
                timer.Start();
        }
        public void Move(MovingObject movingObject)
        {
            if (CanMove(movingObject))
            {
                switch (movingObject.Direction)
                {
                    case Direction.Right:
                        movingObject.Position.X += MovingObject.MoveOffset;
                        break;
                    case Direction.Left:
                        movingObject.Position.X -= MovingObject.MoveOffset;
                        break;
                    case Direction.Up:
                        movingObject.Position.Y -= MovingObject.MoveOffset;
                        break;
                    case Direction.Down:
                        movingObject.Position.Y += MovingObject.MoveOffset;
                        break;
                }
            }
            else
                timer.Stop();
        }

        private bool CanMove(MovingObject movingObject) //TODO: переименовать
        {
            Rectangle nextPoint = movingObject.HitBox;
            switch (movingObject.Direction)
            {
                case Direction.Right:
                    nextPoint = new Rectangle(movingObject.Position.X + MovingObject.MoveOffset, movingObject.Position.Y, movingObject.HitBox.Width, movingObject.HitBox.Height);
                    break;
                case Direction.Left:
                    nextPoint = new Rectangle(movingObject.Position.X - MovingObject.MoveOffset, movingObject.Position.Y, movingObject.HitBox.Width, movingObject.HitBox.Height);
                    break;
                case Direction.Up:
                    nextPoint = new Rectangle(movingObject.Position.X, movingObject.Position.Y - MovingObject.MoveOffset, movingObject.HitBox.Width, movingObject.HitBox.Height);
                    break;
                case Direction.Down:
                    nextPoint = new Rectangle(movingObject.Position.X, movingObject.Position.Y + MovingObject.MoveOffset, movingObject.HitBox.Width, movingObject.HitBox.Height);
                    break;
            }            

            foreach (FieldObject fieldObject in GetField().FieldObjects)
            {
                if (fieldObject.HitBox.IntersectsWith(nextPoint))
                {
                    if (fieldObject.IsPassable())
                    {
                        if (fieldObject.ObjectType == FieldObjectType.Apple)
                            appleController.TakeApple(fieldObject); //TODO: bug, иногда срабатывает дважды
                        return true;
                    }
                    else
                        return false;
                }
            }
            return true;
        }
    }

}
