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

        public bool Move(MovingObject movingObject, Field field)
        {

            Rectangle nextPoint = GetNextPoint(movingObject);
            List<FieldObject> intersectsFieldObjects = GetIntersectObjects(field, nextPoint);
            bool isPassable = intersectsFieldObjects.All(o => o.IsPassable());

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
                    return true;
                }
            }
            else
            {
                if (movingObject is Kolobok)
                {
                    return false;
                }
                else if (movingObject is Tank)
                {
                    //развернуть танк TODO
                    return true;
                }
            }

            //столкнулись танк и колобок - гейм овер TODO

            return true;
        }

        public void StartGame(int speed)
        {
            
        }

    }
}
