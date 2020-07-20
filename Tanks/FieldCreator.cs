using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tanks.Models;

namespace Tanks.Controllers
{
    class FieldCreator
    {
        public FieldCreator()
        {

        }

        public Field CreateGameMap(string[] args)
        {
            Field field = new Field();
            try
            {
                if (args.Length == 5)
                {
                    field.Width = Convert.ToInt32(args[0]);
                    field.Height = Convert.ToInt32(args[1]);
                    field.CountEnemies = Convert.ToInt32(args[2]);
                    field.CountApples = Convert.ToInt32(args[3]);
                    field.ObjectsSpeed = Convert.ToInt32(args[4]);
                }
            }
            catch
            {
                //сделать вывод об использовании дефолтных значений
            }
            InitFieldGrounds(field);
            FillGameMap(field);

            return field;
        }

        private void FillGameMap(Field field)
        {
            FillFieldObjects(FieldObjectType.Wall, Field.DefaultCountWalls, field);
            FillFieldObjects(FieldObjectType.River, Field.DefaultCountRivers, field); //TODO: temp
            FillFieldObjects(FieldObjectType.Apple, field.CountApples, field);
        }

        private void FillFieldObjects(FieldObjectType fieldObjectType, int countObjects, Field field)
        {
            Random random = new Random();
            for (int i = 0; i < countObjects; i++)
                field.GenerateNewRandomObject(fieldObjectType, random);
        }

        private void InitFieldGrounds(Field field) 
        {            
            //TODO: может пригодиться для очистки поля
            //Field.Grounds.Clear(); 
            for (int x = 0; x <= field.Width - FieldObject.DefaultHitBoxWidth; x += FieldObject.DefaultHitBoxWidth)
                for (int y = 0; y <= field.Height - FieldObject.DefaultHitBoxHeight; y += FieldObject.DefaultHitBoxHeight)
                {
                    field.Grounds.Add(new FieldObject(x, y, FieldObjectType.Ground));
                }
        }
    }
}
