namespace Tanks.Models
{
    public class Tank : MovingObject
    {
        public const int ChangeDirectionDelay = 20;
        public Tank(int x, int y, FieldObjectType fieldObjectType) : base(x, y, FieldObjectType.Tank) { }
    }
}
