using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks.Models
{
    public class Tank : MovingObject
    {
        public Tank(int x, int y, FieldObjectType fieldObjectType) : base(x, y, FieldObjectType.Tank)
        {

        }
    }
}
