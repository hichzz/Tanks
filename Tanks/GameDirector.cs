using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Tanks.Models;

namespace Tanks
{
    public class GameDirector
    {
        public delegate void GameOverHandler();
        public event GameOverHandler GameOverNotify;

        public delegate void GameScoreChangeHandler();
        public event GameScoreChangeHandler GameScoreChanged;

        private void TryToEat(Field field, List<FieldObject> intersectsFieldObjects)
        {
            foreach (FieldObject fieldObject in intersectsFieldObjects)
                if (fieldObject.ObjectType == FieldObjectType.Apple)
                    ConsumeApple(fieldObject, field);
        }

        private void ConsumeApple(FieldObject apple, Field field)
        {
            Random random = new Random();
            field.GenerateNewRandomObject(FieldObjectType.Apple, random);

            field.Grounds.Add(apple);
            field.FieldObjects.Remove(apple);

            field.GameScore++;
            GameScoreChanged();
        }

        private List<FieldObject> GetIntersectObjects(Field field, Rectangle hitBox)
        {
            List<FieldObject> intersectObjects = new List<FieldObject>();

            intersectObjects.AddRange(field.FieldObjects
                .Where(o => o.HitBox.IntersectsWith(hitBox)));

            return intersectObjects;
        }

        private Rectangle GetNextPoint(MovingObject movingObject)
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

            return nextPoint;
        }

        private bool IsBorder(Field field, Rectangle point) => 
            (point.X + MovingObject.DefaultHitBoxWidthTanks > field.Width 
            || point.X < 0
            || point.Y + MovingObject.DefaultHitBoxHeightTanks > field.Height
            || point.Y < 0);

        private void ReversTank(Tank tank)
        {
            switch (tank.Direction)
            {
                case Direction.Right:
                    tank.Direction = Direction.Left;
                    break;
                case Direction.Left:
                    tank.Direction = Direction.Right;
                    break;
                case Direction.Up:
                    tank.Direction = Direction.Down;
                    break;
                case Direction.Down:
                    tank.Direction = Direction.Up;
                    break;
            }
        }

        public void MoveTanks(Field field)
        {
            foreach (Tank tank in field.Tanks)
                Move(tank, field);
        }

        public void ChangeDirectionTank(Tank tank, Random random)
        {
            if (random.Next(0, 9) <= 2)
                tank.Direction = (Direction)random.Next(0, 4);
        }



        public void MoveObject(MovingObject movingObject)
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

        public List<FieldObject> GetCollision(MovingObject movingObject, Field field, Rectangle nextPoint)
        {
            List<FieldObject> intersectsFieldObjects = GetIntersectObjects(field, nextPoint);

            if (movingObject is Kolobok)
            {
                intersectsFieldObjects.AddRange(field.Tanks
                    .Where(o => o.HitBox.IntersectsWith(nextPoint)));
            }
            else if (movingObject is Tank)
            {
                List<Tank> tmp = new List<Tank>();
                tmp.AddRange(field.Tanks);
                tmp.Remove((Tank)movingObject);
                intersectsFieldObjects.AddRange(tmp
                    .Where(o => o.HitBox.IntersectsWith(nextPoint)));
            }

            return intersectsFieldObjects;
        }

        public bool Move(MovingObject movingObject, Field field)
        {
            Rectangle nextPoint = GetNextPoint(movingObject);
            List<FieldObject> intersectsFieldObjects = GetCollision(movingObject, field, nextPoint);

            bool isPassable = intersectsFieldObjects.All(o => o.IsPassable());
            bool isTankCollision = intersectsFieldObjects.Count > 0 && intersectsFieldObjects.First() is Tank;  //TODO ???

            if (isPassable && !IsBorder(field, nextPoint))
            {
                if (movingObject is Kolobok)
                {
                    MoveObject(movingObject);
                    TryToEat(field, intersectsFieldObjects);
                    return true;
                }
                else if (movingObject is Tank)
                {
                    MoveObject(movingObject);
                    return true;
                }
            }
            else
            {
                if (movingObject is Kolobok)
                {
                    if (isTankCollision)
                        GameOverNotify();
                    return false;
                }
                else if (movingObject is Tank)
                {
                    if (isTankCollision)
                        ReversTank((Tank)movingObject);
                    else
                        ChangeDirectionTank((Tank)movingObject, new Random());
                    return true;
                }
            }

            //столкнулись танк и колобок - гейм овер TODO

            return true;
        }
    }
}
