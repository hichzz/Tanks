using System;
using System.Collections.Generic;

namespace Tanks.Models
{
    public class Field
    {
        private const int DefaultMapHeight = 700;
        private const int DefailtMapWidth = 1000;
        private const int DefaultCountEnemies = 5;
        private const int DefaultCountApples = 5;
        private const int DefaultObjectsSpeed = 5;
        public const int DefaultCountWalls = 5; 
        public const int DefaultCountRivers = 7; 

        public int GameScore { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public int CountEnemies { get; set; }
        public int CountApples { get; set; }
        public int ObjectsSpeed { get; set; } 
        public List<Tank> Tanks { get; set; }
        public List<FieldObject> FieldObjects { get; set; }
        public List<FieldObject> Grounds { get; set; } 
        public List<FieldObject> Map { get; set; }
        public List<Bullet> Bullets { get; set; }
        public List<Bullet> HitsBullets { get; set; }

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
            Bullets = new List<Bullet>();
            HitsBullets = new List<Bullet>();
        }

        public void GenerateNewRandomObject(FieldObjectType fieldObjectType, Random random) 
        {
            int index = random.Next(Grounds.Count);
            FieldObject fieldObject = Grounds[index];
            fieldObject.ObjectType = fieldObjectType;
            FieldObjects.Add(fieldObject);
            Grounds.RemoveAt(index);
        }
    }
}
