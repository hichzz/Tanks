using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks.Models
{
    public class FieldObject
    {
        public const int DefaultHitBoxWidth = 50;
        public const int DefaultHitBoxHeight = 50;

        public FieldObjectType ObjectType;
        public Point Position;
        public Rectangle HitBox => new Rectangle(Position, new Size(DefaultHitBoxWidth, DefaultHitBoxHeight));

        public FieldObject(int x, int y)
        {
            Position = new Point(x, y);
        }

    }
}
