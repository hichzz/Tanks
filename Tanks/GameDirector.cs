using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Tanks.Models;

namespace Tanks
{
    public class GameDirector
    {
        private const int CountDirections = 4;

        public delegate void GameOverHandler();
        public event GameOverHandler GameOverNotify;

        public delegate void GameScoreChangeHandler();
        public event GameScoreChangeHandler GameScoreChanged;

        private void TryEat(Field field, List<FieldObject> intersectsFieldObjects)
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
                    if (fieldObject.ObjectType == FieldObjectType.Tank 
                        || fieldObject.ObjectType == FieldObjectType.Bullet)
                        return true;
                }
            }
            return false;
        }

        private bool IsDestroyObject(List<FieldObject> intersectsFieldObjects) => 
            intersectsFieldObjects.Count > 0 
            && intersectsFieldObjects.OfType<Bullet>().Any(b => b.IsKolobokBullet);

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

        private bool IsProbabilityChangeDirection(Random random) => random.Next(0, 9) <= 2;

        public void ChangeDirectionTank(Tank tank, Random random)
        {
            if (IsProbabilityChangeDirection(random))
                tank.Direction = (Direction)random.Next(0, CountDirections);
        }

        private List<FieldObject> GetIntersectKolobokObjects(Field field, Rectangle nextPoint)
        {
            List<FieldObject> intersectsFieldObjects = new List<FieldObject>();

            IEnumerable<FieldObject> intersectedTanks = field.Tanks
                .Where(t => t.HitBox.IntersectsWith(nextPoint));

            IEnumerable<FieldObject> intersectedBullets = field.Bullets
                .Where(o => o.HitBox.IntersectsWith(nextPoint))
                .Where(o => !o.IsKolobokBullet);

            intersectsFieldObjects.AddRange(intersectedTanks);
            intersectsFieldObjects.AddRange(intersectedBullets);

            return intersectsFieldObjects;
        }

        private List<FieldObject> GetIntersectedTankObjects(MovingObject movingObject, Field field, Rectangle nextPoint)
        {
            List<FieldObject> intersectsFieldObjects = new List<FieldObject>();

            List<Tank> otherTanks = new List<Tank>();
            otherTanks.AddRange(field.Tanks);
            otherTanks.Remove((Tank)movingObject);

            IEnumerable<FieldObject> intersectedTanks = otherTanks
                .Where(o => o.HitBox.IntersectsWith(nextPoint));

            IEnumerable<FieldObject> intersectedBullets = field.Bullets
                .Where(o => o.HitBox.IntersectsWith(nextPoint))
                .Where(o => o.IsKolobokBullet);

            intersectsFieldObjects.AddRange(intersectedTanks);
            intersectsFieldObjects.AddRange(intersectedBullets);

            return intersectsFieldObjects;
        }

        private void MoveBehavior(MovingObject movingObject, Field field, List<FieldObject> collisions)
        {
            switch (movingObject.ObjectType)
            {
                case FieldObjectType.Kolobok:
                    MoveObject(movingObject);
                    TryEat(field, collisions);
                    break;
                case FieldObjectType.Tank:
                case FieldObjectType.Bullet:
                    MoveObject(movingObject);
                    break;
            }
        }

        private void CollisionBehavior(MovingObject movingObject, Field field, Rectangle nextPoint, List<FieldObject> collisions)
        {
            bool isTankOrBulletCollision = IsTankOrBulletCollision(collisions);
            bool isShootable = collisions.All(o => o.IsShootable());

            switch (movingObject.ObjectType)
            {
                case FieldObjectType.Kolobok:
                    if (isTankOrBulletCollision)
                        GameOverNotify();
                    break;

                case FieldObjectType.Tank:
                    TankCollisionBehavior(movingObject, field, collisions, isTankOrBulletCollision);
                    break;

                case FieldObjectType.Bullet:
                    BulletCollisionBehavior(movingObject, field, nextPoint, isShootable);
                    break;
            }
        }

        private void BulletCollisionBehavior(MovingObject movingObject, Field field, Rectangle nextPoint, bool isShootable)
        {
            if (isShootable && !IsBorder(field, nextPoint))
                MoveObject(movingObject);
            else
                field.HitsBullets.Add((Bullet)movingObject);
        }

        private void TankCollisionBehavior(MovingObject movingObject, Field field, List<FieldObject> collisions, bool isTankOrBulletCollision)
        {
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
        }

        public void MoveTanks(Field field)
        {
            foreach (Tank tank in field.Tanks)
                Move(tank, field);
        }

        public void HandleTanks(Field field, int countTimer, Random random)
        {
            MoveTanks(field);

            if (countTimer % Tank.ChangeDirectionDelay == 0)
                foreach (Tank tank in field.Tanks)
                    ChangeDirectionTank(tank, random);

            if (countTimer % Bullet.CreateBulletDelay == 0)
                foreach (Tank tank in field.Tanks)
                    CreateBullet(tank, field);
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
                    intersectsFieldObjects.AddRange(GetIntersectKolobokObjects(field, nextPoint));
                    break;

                case FieldObjectType.Tank:
                    intersectsFieldObjects.AddRange(GetIntersectedTankObjects(movingObject, field, nextPoint));
                    break;
            }

            return intersectsFieldObjects;
        }



        public void Move(MovingObject movingObject, Field field)
        {
            Rectangle nextPoint = GetNextPoint(movingObject);

            List<FieldObject> collisions = GetCollision(movingObject, field, nextPoint);

            bool isPassable = collisions.All(o => o.IsPassable());

            if (isPassable && !IsBorder(field, nextPoint))
                MoveBehavior(movingObject, field, collisions);
            else
                CollisionBehavior(movingObject, field, nextPoint, collisions);
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

            bullet.IsKolobokBullet = movingObject.ObjectType == FieldObjectType.Kolobok;

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
