using System.Drawing;

namespace Tanks.Models
{
    public class Bullet : MovingObject
    {
        public const int CreateBulletDelay = 83;
        public const int FlySpeed = 5;
        public const int DefaultBulletHeight = 20;
        public const int DefaultBulletWidth = 20;
        public override Rectangle HitBox => 
            new Rectangle(Position, new Size(DefaultBulletWidth, DefaultBulletHeight));

        public bool IsKolobokBullet { get; set; }
        public Bullet(int x, int y, FieldObjectType fieldObjectType) : base(x, y, FieldObjectType.Bullet) { }

        public Bullet()
        {
            this.ObjectType = FieldObjectType.Bullet;
        }
    }
}
