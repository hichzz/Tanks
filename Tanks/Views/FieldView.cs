using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tanks.Controllers;
using Tanks.Models;
using Tanks.Views;

namespace Tanks
{
    public partial class MainView : Form
    {
        private Field field;
        private PackmanController packmanController;
        public MainView()
        {
            KeyPreview = true;
            InitializeComponent();
        }

        public void SetController(PackmanController packmanController)
        {
            this.packmanController = packmanController;
            this.field = packmanController.GetField(); 
        }
        private void DrawEmptyField()
        {
            MapPictureBox.Size = new Size(field.Width, field.Height);
            Bitmap fieldImage = new Bitmap(field.Width, field.Height);
            Graphics flagGraphics = Graphics.FromImage(fieldImage);

            flagGraphics.FillRectangle(Brushes.Wheat, 0, 0, field.Width, field.Height);
            MapPictureBox.Image = fieldImage;
        }

        private void DrawFieldObjects()
        {
            Graphics flagGraphics = Graphics.FromImage(MapPictureBox.Image);

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

        public void UpdateField(KolobokView kolobokView)
        {
            GameScoreLabel.Text = $"Game Score: {field.GameScore}";
            DrawEmptyField();
            DrawFieldObjects();
            kolobokView.DrawKolobok(MapPictureBox);
            MapPictureBox.Refresh();
        }

        public void ClearCell(FieldObject fieldObject)
        {
            Graphics flagGraphics = Graphics.FromImage(MapPictureBox.Image);
            SolidBrush brush = new SolidBrush(Color.Wheat);
            flagGraphics.FillRectangle(brush, fieldObject.HitBox);
        }

        private void Tanks_Load(object sender, EventArgs e)
        {
            DrawEmptyField();
            DrawFieldObjects();
        }

        private void StartGameButton_Click(object sender, EventArgs e)
        {
            KeyUp += new KeyEventHandler(packmanController.ChangeDirection_KeyUp);
            packmanController.StartGame(MapPictureBox);
        }
    }
}
