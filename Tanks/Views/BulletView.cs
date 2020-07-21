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
    public class BulletView
    {
        private Field field;
       
        public BulletView(Field field)
        {
            this.field = field;
        }

        public void DrawBullets(PictureBox pictureBox)
        {
            Graphics flagGraphics = Graphics.FromImage(pictureBox.Image);
            SolidBrush brush; 
            foreach (Bullet bullet in field.Bullets)
            {
                if (bullet.IsKolobokBullet)
                    brush = new SolidBrush(Color.Blue);
                else
                    brush = new SolidBrush(Color.MediumVioletRed);

                flagGraphics.FillEllipse(brush, bullet.HitBox);
            }
        }
    }
}
