using System.Drawing;
using System.Windows.Forms;
using Tanks.Models;

namespace Tanks.Views
{
    public class KolobokView
    {
        private Kolobok kolobok;
        private Bitmap bitmap_up = new Bitmap(Properties.Resources.kolobok_up);
        private Bitmap bitmap_down = new Bitmap(Properties.Resources.kolobok_down);
        private Bitmap bitmap_left = new Bitmap(Properties.Resources.kolobok_left);
        private Bitmap bitmap_right = new Bitmap(Properties.Resources.kolobok_right);

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
