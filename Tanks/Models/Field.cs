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

        public int Height { get; set; }
        public int Width { get; set; }
        public int CountEnemies { get; set; }
        public int CountApples { get; set; }
        public int ObjectsSpeed { get; set; } //todo move to obj
        public List<Apple> Apples { get; set; }
        public List<Enemy> Enemies { get; set; }
        public List<FieldObject> FieldObjects { get; set; }
        public List<FieldObject> Grounds { get; set; } //free cells

/*        public Field(string[] args) //todo переделать по-человечески
        {
            try
            {
                if (args.Length == 5)
                {
                    Height = Convert.ToInt32(args[0]);
                    Width = Convert.ToInt32(args[1]);
                    CountEnemies = Convert.ToInt32(args[2]);
                    CountApples = Convert.ToInt32(args[3]);
                    ObjectsSpeed = Convert.ToInt32(args[4]);
                }
            }
            catch (Exception ex)
            {
                Height = DefaultMapHeight;
                Width = DefailtMapWidth;
                CountApples = DefaultCountApples;
                ObjectsSpeed = DefaultObjectsSpeed;
                CountEnemies = DefaultCountEnemies;
            }
        }*/
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
            Apples = new List<Apple>();
            Enemies = new List<Enemy>();
        }
    }
}
