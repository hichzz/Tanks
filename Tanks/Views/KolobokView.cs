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
    public class KolobokView
    {
        private Kolobok kolobok; //TODO: переделать под ресурсы
        private Bitmap bitmap_up = new Bitmap("C:\\Users\\Serpiente\\source\\epam\\Tanks\\Tanks\\Tanks\\Images\\kolobok_up.bmp");
        private Bitmap bitmap_down = new Bitmap("C:\\Users\\Serpiente\\source\\epam\\Tanks\\Tanks\\Tanks\\Images\\kolobok_down.bmp");
        private Bitmap bitmap_left = new Bitmap("C:\\Users\\Serpiente\\source\\epam\\Tanks\\Tanks\\Tanks\\Images\\kolobok_left.bmp");
        private Bitmap bitmap_right = new Bitmap("C:\\Users\\Serpiente\\source\\epam\\Tanks\\Tanks\\Tanks\\Images\\kolobok_right.bmp");

        public KolobokView(Kolobok kolobok)
        {
            this.kolobok = kolobok;
        }

        public void DrawKolobok(PictureBox pictureBox)
        {
            Graphics flagGraphics = Graphics.FromImage(pictureBox.Image);
            switch (kolobok.Direction)
            {
                case Direction.Down:
                    flagGraphics.DrawImage(bitmap_down, kolobok.Position);
                    break;
                case Direction.Left:
                    flagGraphics.DrawImage(bitmap_left, kolobok.Position);
                    break;
                case Direction.Right:
                    flagGraphics.DrawImage(bitmap_right, kolobok.Position);
                    break;
                case Direction.Up:
                    flagGraphics.DrawImage(bitmap_up, kolobok.Position);
                    break;
            }
        }
    }
}
