using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tanks.Models;

namespace Tanks.Views
{
    public class TankView
    {
        private List<Tank> tanks;
        //TODO: переложить в приложение 
        //TODO: убрать пути в файл отдельный 
        private Bitmap bitmap_up = new Bitmap("C:\\Users\\Serpiente\\source\\epam\\Tanks\\Tanks\\Tanks\\Images\\tank_up.bmp");
        private Bitmap bitmap_down = new Bitmap("C:\\Users\\Serpiente\\source\\epam\\Tanks\\Tanks\\Tanks\\Images\\tank_down.bmp");
        private Bitmap bitmap_left = new Bitmap("C:\\Users\\Serpiente\\source\\epam\\Tanks\\Tanks\\Tanks\\Images\\tank_left.bmp");
        private Bitmap bitmap_right = new Bitmap("C:\\Users\\Serpiente\\source\\epam\\Tanks\\Tanks\\Tanks\\Images\\tank_right.bmp");

        public TankView(List<Tank> tanks)
        {
            this.tanks = tanks;
        }

        public void DrawTanks(PictureBox pictureBox)
        {
            Graphics flagGraphics = Graphics.FromImage(pictureBox.Image);
            foreach (Tank tank in tanks)
            {
                switch (tank.Direction)
                {
                    case Direction.Down:
                        flagGraphics.DrawImage(bitmap_down, tank.Position);
                        break;
                    case Direction.Left:
                        flagGraphics.DrawImage(bitmap_left, tank.Position);
                        break;
                    case Direction.Right:
                        flagGraphics.DrawImage(bitmap_right, tank.Position);
                        break;
                    case Direction.Up:
                        flagGraphics.DrawImage(bitmap_up, tank.Position);
                        break;
                }
            }
            //TODO: добавить дефолт или проверка на нулл
        }
    }
}
