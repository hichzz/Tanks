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

namespace Tanks
{
    public partial class MainView : Form
    {
        private Field field;
        public MainView(Field field)
        {
            InitializeComponent();
            this.field = field;
            MouseMove += new MouseEventHandler(MainForm_MouseMove);
        }

        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {
            Text = string.Format($"Mouse = {e.X}:{e.Y}"); //for debug
        }

        private void DrawFieldImage()
        {
            MapPictureBox.Size = new Size(field.Width, field.Height);
            Bitmap fieldImage = new Bitmap(field.Width, field.Height);
            Graphics flagGraphics = Graphics.FromImage(fieldImage);

            flagGraphics.FillRectangle(Brushes.Wheat, 0, 0, field.Width, field.Height);
            MapPictureBox.Image = fieldImage;
        }

        private void DrawWalls()
        {
            //TODO: добавить проверку на FieldObjectType
            Graphics flagGraphics = Graphics.FromImage(MapPictureBox.Image);
            SolidBrush brush = new SolidBrush(Color.Chocolate);
            foreach (FieldObject wall in field.FieldObjects)
            {
                flagGraphics.FillRectangle(brush, wall.HitBox);
                label1.Text = label1.Text + "..." + wall.HitBox.X.ToString() + "-" + wall.HitBox.Y.ToString(); //for debug
            }
        }

        private void Tanks_Load(object sender, EventArgs e)
        {
            DrawFieldImage();
            DrawWalls();
        }

        private void StartGameButton_Click(object sender, EventArgs e)
        {

        }
    }
}
