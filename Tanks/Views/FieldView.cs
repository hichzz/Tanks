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
    public class FieldView
    {
        private Field field;
        public FieldView(Field field)
        {
            this.field = field;
        }

        public void DrawEmptyField(PictureBox mapPictureBox)
        {
            mapPictureBox.Size = new Size(field.Width, field.Height);
            Bitmap fieldImage = new Bitmap(field.Width, field.Height);
            Graphics flagGraphics = Graphics.FromImage(fieldImage);

            flagGraphics.FillRectangle(Brushes.Wheat, 0, 0, field.Width, field.Height);
            mapPictureBox.Image = fieldImage;
        }

        public void DrawFieldObjects(PictureBox mapPictureBox)
        {
            Graphics flagGraphics = Graphics.FromImage(mapPictureBox.Image);

            foreach (FieldObject fieldObject in field.FieldObjects)
            {
                SolidBrush brush;
                switch (fieldObject.ObjectType)
                {
                    case FieldObjectType.Wall:
                        brush = new SolidBrush(Color.Peru);
                        flagGraphics.FillRectangle(brush, fieldObject.HitBox);

                        break;
                    case FieldObjectType.River:
                        brush = new SolidBrush(Color.Teal);
                        flagGraphics.FillRectangle(brush, fieldObject.HitBox);
                        break;
                    case FieldObjectType.Apple:
                        brush = new SolidBrush(Color.Maroon);
                        flagGraphics.FillEllipse(brush, fieldObject.HitBox);
                        break;
                }
            }
        }

        public void UpdateField(BulletView bulletView, KolobokView kolobokView, TankView tankView, PictureBox mapPictureBox)
        {
            DrawEmptyField(mapPictureBox);
            DrawFieldObjects(mapPictureBox);
            kolobokView.DrawKolobok(mapPictureBox);
            tankView.DrawTanks(mapPictureBox);
            bulletView.DrawBullets(mapPictureBox);
            mapPictureBox.Refresh();
        }

        public void ShowGameScore(Label gameScoreLabel)
        {
            gameScoreLabel.Text = $"Game Score: {field.GameScore}";
        }
    }
}
