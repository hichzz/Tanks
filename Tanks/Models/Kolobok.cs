using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks.Models
{
    public class Kolobok : MovingObject
    {
        
        public Kolobok(int x, int y, FieldObjectType fieldObjectType) : base (x, y, FieldObjectType.Kolobok)
        {

        }
    }
}
