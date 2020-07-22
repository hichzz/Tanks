using System.Drawing;

namespace Tanks.Models
{
    public class FieldObject
    {
        public const int DefaultHitBoxWidth = 50;
        public const int DefaultHitBoxHeight = 50;

        public FieldObjectType ObjectType;
        public Point Position;
        public virtual Rectangle HitBox => 
            new Rectangle(Position, new Size(DefaultHitBoxWidth, DefaultHitBoxHeight));

        public bool IsPassable() => 
            ObjectType == FieldObjectType.Apple
            || ObjectType == FieldObjectType.Ground;

        public bool IsShootable() => 
            ObjectType == FieldObjectType.Apple 
            || ObjectType == FieldObjectType.Ground 
            || ObjectType == FieldObjectType.River;

        public FieldObject(int x, int y, FieldObjectType fieldObjectType)
        {
            Position = new Point(x, y);
            ObjectType = fieldObjectType;
        }

        public FieldObject()
        {
            ObjectType = FieldObjectType.Empty;
        }

    }
}
