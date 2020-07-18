using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tanks.Models;

namespace Tanks.Controllers
{
    class FieldController
    {
        public Field Field;
        public FieldController(string[] args)
        {
            Field = new Field();
            try
            {
                if (args.Length == 5)
                {
                    Field.Width = Convert.ToInt32(args[0]);
                    Field.Height = Convert.ToInt32(args[1]);
                    Field.CountEnemies = Convert.ToInt32(args[2]);
                    Field.CountApples = Convert.ToInt32(args[3]);
                    Field.ObjectsSpeed = Convert.ToInt32(args[4]);
                    ClearField();
                    FillGameMap();

                }
            }
            catch
            {
                //сделать вывод об использовании дефолтных значений
            }
        }

        private void FillGameMap()
        {
            FillFieldObjects(FieldObjectType.Wall, Field.DefaultCountWalls);
            FillFieldObjects(FieldObjectType.River, Field.DefaultCountRivers); //TODO: temp
            FillFieldObjects(FieldObjectType.Apple, Field.CountApples);
        }

        private void FillFieldObjects(FieldObjectType fieldObjectType, int countObjects)
        {
            Random random = new Random();
            for (int i = 0; i < countObjects; i++)
                Field.GenerateRandomObject(fieldObjectType, random);
        }

        public void ClearField()
        {
            Field.Grounds.Clear();
            for (int x = 0; x <= Field.Width - FieldObject.DefaultHitBoxWidth; x += FieldObject.DefaultHitBoxWidth)
                for (int y = 0; y <= Field.Height - FieldObject.DefaultHitBoxHeight; y += FieldObject.DefaultHitBoxHeight)
                {
                    Field.Grounds.Add(new FieldObject(x, y, FieldObjectType.Ground));
                }
        }

        public FieldObject GetFieldObjectByHitBox(Rectangle hitBox)
        {
            foreach(FieldObject fieldObject in Field.FieldObjects)
            {
                if (fieldObject.HitBox.IntersectsWith(hitBox))
                {
                    return fieldObject;
                }
            }

            return new FieldObject();
        }
    }
}
