using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks.Models
{
    class MovingMapObject : FieldObject
    {
        public Direction Direction { get; set; }
        public int Speed { get; set; }
        public MovingMapObject(int x, int y) : base(x, y)
        {

        }

        public void Move()
        {

        }
    }
}
