using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tanks.Models;

namespace Tanks.Controllers
{
    class AppleController
    {
        private Field field;

        public AppleController(Field field)
        {
            this.field = field;
        }

        public void GenerateNewApple()
        {
            Random random = new Random();
            FieldObject newApple = field.GenerateRandomObject(FieldObjectType.Apple, random);
            field.FieldObjects.Add(newApple);
        }

        public void TakeApple(FieldObject apple)
        {
            field.GameScore++;
            GenerateNewApple();
            field.FieldObjects.Remove(apple);
        }
    }
}
