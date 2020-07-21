using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks.Models
{
    public class MovingObject : FieldObject
    {
        public const int MoveOffset = 1;
        public const int DefaultSpeed = 10;
        public const int DefaultBulletsSpeed = 100;
        public const int DefaultHitBoxWidthTanks = 40;
        public const int DefaultHitBoxHeightTanks = 40;
        public override Rectangle HitBox  => new Rectangle(Position, new Size(DefaultHitBoxWidthTanks, DefaultHitBoxHeightTanks));
        public Direction Direction { get; set; }
        public int Speed { get; set; }
        public MovingObject(int x, int y, FieldObjectType fieldObjectType) : base(x, y, fieldObjectType)
        {

        }

        public MovingObject()
        {

        }
    }
}
