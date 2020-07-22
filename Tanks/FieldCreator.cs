using System;
using Tanks.Models;

namespace Tanks.Controllers
{
    public class FieldCreator
    {
        public int Height { get; set; }
        public int Width { get; set; }
        public int CountEnemies { get; set; }
        public int CountApples { get; set; }
        public int ObjectsSpeed { get; set; }
        public FieldCreator(string[] args) 
        {
            try
            {
                if (args.Length == 5)
                {
                    Width = Convert.ToInt32(args[0]);
                    Height = Convert.ToInt32(args[1]);
                    CountEnemies = Convert.ToInt32(args[2]);
                    CountApples = Convert.ToInt32(args[3]);
                    ObjectsSpeed = Convert.ToInt32(args[4]);
                }
            }
            catch { }
        }
        
        public Field CreateGameMap()
        {
            Field field = new Field
            {
                Width = Width,
                Height = Height,
                CountEnemies = CountEnemies,
                CountApples = CountApples,
                ObjectsSpeed = ObjectsSpeed
            };

            InitFieldGrounds(field);
            FillGameMap(field);

            return field;
        }

        private void FillGameMap(Field field)
        {
            field.FieldObjects.Clear();
            FillFieldObjects(FieldObjectType.Wall, Field.DefaultCountWalls, field);
            FillFieldObjects(FieldObjectType.River, Field.DefaultCountRivers, field); 
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
            field.Grounds.Clear(); 
            for (int x = 0; x <= field.Width - FieldObject.DefaultHitBoxWidth; x += FieldObject.DefaultHitBoxWidth)
                for (int y = 0; y <= field.Height - FieldObject.DefaultHitBoxHeight; y += FieldObject.DefaultHitBoxHeight)
                {
                    field.Grounds.Add(new FieldObject(x, y, FieldObjectType.Ground));
                }
        }
    }
}
