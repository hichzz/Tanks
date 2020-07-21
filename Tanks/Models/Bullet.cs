using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks.Models
{
    public class Bullet : MovingObject
    {
        public const int FlyDelay = 45;
        public const int FlySpeed = 5;
        public const int DefaultBulletHeight = 20;
        public const int DefaultBulletWidth = 20;
        public override Rectangle HitBox => new Rectangle(Position, new Size(DefaultBulletWidth, DefaultBulletHeight));
        public bool IsKolobokBullet { get; set; }
        public Bullet(int x, int y, FieldObjectType fieldObjectType) : base(x, y, FieldObjectType.Bullet)
        {

        }

        public Bullet()
        {
            this.ObjectType = FieldObjectType.Bullet;
        }
    }
}
