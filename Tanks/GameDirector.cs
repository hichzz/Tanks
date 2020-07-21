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

        private bool IsTankOrBulletCollision(List<FieldObject> intersectsFieldObjects)
        {
            if (intersectsFieldObjects.Count > 0)
            {
                foreach (FieldObject fieldObject in intersectsFieldObjects)
                {
                    if (fieldObject.ObjectType == FieldObjectType.Tank || fieldObject.ObjectType == FieldObjectType.Bullet)
                        return true;
                }
            }
            return false;
        }

        private bool IsDestroyObject(List<FieldObject> intersectsFieldObjects)
        {
            if (intersectsFieldObjects.Count > 0)
            {
                foreach (FieldObject fieldObject in intersectsFieldObjects)
                {
                    if (fieldObject.ObjectType == FieldObjectType.Bullet)
                    {
                        Bullet bullet = (Bullet)fieldObject;
                        if (bullet.IsKolobokBullet)
                            return true;
                        else
                            return false;
                    }
                }
            }
            return false;
        }
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

        private void ChangeTankLocation(MovingObject movingObject, Field field)
        {
            Random random = new Random();
            int index = random.Next(field.Grounds.Count);
            FieldObject fieldObject = field.Grounds[index];
            movingObject.Position = fieldObject.Position;
        }

        public void ChangeDirectionTank(Tank tank, Random random)
        {
            if (random.Next(0, 9) <= 2)
                tank.Direction = (Direction)random.Next(0, 4);
        }

        public void MoveTanks(Field field)
        {
            foreach (Tank tank in field.Tanks)
                Move(tank, field);
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

            switch (movingObject.ObjectType)
            {
                case FieldObjectType.Kolobok:
                    intersectsFieldObjects.AddRange(field.Tanks
                        .Where(o => o.HitBox.IntersectsWith(nextPoint)));

                    intersectsFieldObjects.AddRange(field.Bullets
                        .Where(o => o.HitBox.IntersectsWith(nextPoint))
                        .Where(o => !o.IsKolobokBullet));

                    break;
                case FieldObjectType.Tank:
                    List<Tank> neededTanks = new List<Tank>();
                    neededTanks.AddRange(field.Tanks);
                    neededTanks.Remove((Tank)movingObject);

                    intersectsFieldObjects.AddRange(neededTanks
                        .Where(o => o.HitBox.IntersectsWith(nextPoint)));

                    intersectsFieldObjects.AddRange(field.Bullets
                        .Where(o => o.HitBox.IntersectsWith(nextPoint))
                        .Where(o => o.IsKolobokBullet));
                    break;
            }

            return intersectsFieldObjects;
        }

        public void Move(MovingObject movingObject, Field field)
        {
            Rectangle nextPoint = GetNextPoint(movingObject);

            List<FieldObject> collisions = GetCollision(movingObject, field, nextPoint);

            bool isTankOrBulletCollision = IsTankOrBulletCollision(collisions);
            bool isPassable = collisions.All(o => o.IsPassable());
            bool isShootable = collisions.All(o => o.IsShootable());

            if (isPassable && !IsBorder(field, nextPoint))
            {
                switch (movingObject.ObjectType)
                {
                    case FieldObjectType.Kolobok:
                        MoveObject(movingObject);
                        TryToEat(field, collisions);
                        break;
                    case FieldObjectType.Tank:
                    case FieldObjectType.Bullet:
                        MoveObject(movingObject);
                        break;
                }
            }
            else
            {
                switch(movingObject.ObjectType)
                {
                    case FieldObjectType.Kolobok:
                        if (isTankOrBulletCollision)
                        {
                            GameOverNotify();
                        }
                        break;
                    case FieldObjectType.Tank:
                        if (isTankOrBulletCollision)
                        {
                            if (IsDestroyObject(collisions))
                            {
                                ChangeTankLocation(movingObject, field);
                            }
                            else
                            {
                                ReversTank((Tank)movingObject);
                            }
                        }
                        else
                        {
                            ChangeDirectionTank((Tank)movingObject, new Random());
                        }
                        break;
                    case FieldObjectType.Bullet:
                        if (isShootable && !IsBorder(field, nextPoint))
                        {
                            MoveObject(movingObject);
                        }
                        else
                        {
                            field.HitsBullets.Add((Bullet)movingObject);
                        }
                        break;
                }
            }
        }

        public void CreateBullet(MovingObject movingObject, Field field)
        {
            Bullet bullet = new Bullet();
            int verticalShift = MovingObject.DefaultHitBoxHeightTanks / 2 - Bullet.DefaultBulletHeight / 2;
            int horizontalShift = MovingObject.DefaultHitBoxWidthTanks / 2 - Bullet.DefaultBulletWidth / 2;

            switch (movingObject.Direction)
            {
                case Direction.Right:
                    bullet.Position.X = movingObject.Position.X + MovingObject.DefaultHitBoxWidthTanks;
                    bullet.Position.Y = movingObject.Position.Y + verticalShift;
                    break;
                case Direction.Left:
                    bullet.Position.X = movingObject.Position.X;
                    bullet.Position.Y = movingObject.Position.Y + verticalShift;
                    break;
                case Direction.Up:
                    bullet.Position.X = movingObject.Position.X + horizontalShift;
                    bullet.Position.Y = movingObject.Position.Y;
                    break;
                case Direction.Down:
                    bullet.Position.X = movingObject.Position.X + horizontalShift;
                    bullet.Position.Y = movingObject.Position.Y + MovingObject.DefaultHitBoxHeightTanks;
                    break;
            }

            bullet.Direction = movingObject.Direction;

            if (movingObject.ObjectType == FieldObjectType.Tank)
                bullet.IsKolobokBullet = false;
            else
                bullet.IsKolobokBullet = true;

            field.Bullets.Add(bullet);
        }

        public void BulletMove(Field field)
        {
            foreach (Bullet bullet in field.Bullets)
                Move(bullet, field);

            foreach (Bullet hitbullet in field.HitsBullets)
                field.Bullets.Remove(hitbullet);
        }
    }
}
