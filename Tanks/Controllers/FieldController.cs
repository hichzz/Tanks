using System;
using System.Collections.Generic;
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
                    
                }
            }
            catch
            { 
                //сделать вывод об использовании дефолтных значений
            }
        }

        public bool CanBePassed() => true; //Is Type FieldObject Wall, River
        public bool CanBeShooted() => true; //is type river, ground, apple

        //TODO: добавить проверку на свободные ячейки через Grounds
        public List<FieldObject> GetFieldObjects()
        {
            List<FieldObject> fieldObjects = new List<FieldObject>();
            Random random = new Random();
            for (int i = 0; i <= Field.DefaultCountWalls; i++)
            {
                //todo попробовать улучшить
                FieldObject fieldObject;
                do
                {
                    fieldObject = new FieldObject(GetRandomCoordinate(Field.Width, FieldObject.DefaultHitBoxWidth, random), GetRandomCoordinate(Field.Height, FieldObject.DefaultHitBoxHeight, random));
                }
                while (fieldObjects.Contains(fieldObject));

                fieldObjects.Add(fieldObject);
            }
            return fieldObjects;
        }

        public int GetRandomCoordinate(int border, int hitbox, Random random)
        {
            int coordinate = random.Next(0, border - hitbox);
            return coordinate + Math.Abs(hitbox - coordinate % hitbox);
        }
    }
}
