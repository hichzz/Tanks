using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tanks.Models
{
    public class Field
    {
        private const int DefaultMapHeight = 700;
        private const int DefailtMapWidth = 1000;
        private const int DefaultCountEnemies = 5;
        private const int DefaultCountApples = 5;
        private const int DefaultObjectsSpeed = 5;
        public const int DefaultCountWalls = 5; //todo сделать рассчет
        public const int DefaultCountRivers = 7; //todo сделать рассчет

        public int GameScore { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public int CountEnemies { get; set; }
        public int CountApples { get; set; }
        public int ObjectsSpeed { get; set; } //todo move to obj
        public List<Tank> Tanks { get; set; }
        public List<FieldObject> FieldObjects { get; set; }
        public List<FieldObject> Grounds { get; set; } //free cells
        public List<FieldObject> Map { get; set; }

        public Field(int height, int width, int apples, int enemies)
        {
            Height = height;
            Width = width;
            CountApples = apples;
            CountEnemies = enemies;
        }

        public Field()
        {
            Height = DefaultMapHeight;
            Width = DefailtMapWidth;
            CountApples = DefaultCountApples;
            ObjectsSpeed = DefaultObjectsSpeed;
            CountEnemies = DefaultCountEnemies;
            FieldObjects = new List<FieldObject>();
            Tanks = new List<Tank>();
            Grounds = new List<FieldObject>();
            Map = new List<FieldObject>();
        }

        public FieldObject GenerateRandomObject(FieldObjectType fieldObjectType, Random random) //TODO: перенести
        {
            int index = random.Next(Grounds.Count);
            FieldObject fieldObject = Grounds[index];
            fieldObject.ObjectType = fieldObjectType;
            FieldObjects.Add(fieldObject);
            Grounds.RemoveAt(index);
            return fieldObject;
        }
    }
}
