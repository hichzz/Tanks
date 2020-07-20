using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tanks.Models;

namespace Tanks
{
    public partial class Report : Form
    {
        public Report()
        {
            InitializeComponent();           
        }

        public void LoadReportData(Kolobok kolobok, Field field)
        {
            ClearData();
            reportGridView.Rows.Add("Kolobok", kolobok.Position.X, kolobok.Position.Y);

            foreach (Tank tank in field.Tanks)
                reportGridView.Rows.Add(tank.ObjectType, tank.Position.X, tank.Position.Y);

            foreach (FieldObject fieldObject in field.FieldObjects)
                reportGridView.Rows.Add(fieldObject.ObjectType, fieldObject.Position.X, fieldObject.Position.Y);        

            Show();
        }

        public void RefreshData(Kolobok kolobok, Field field)
        {
            int index = 1;
            reportGridView[1, 0].Value = kolobok.Position.X;
            reportGridView[2, 0].Value = kolobok.Position.Y;

            foreach (Tank tank in field.Tanks)
            {
                reportGridView[1, index].Value = tank.Position.X;
                reportGridView[2, index].Value = tank.Position.Y;
                index++;
            }
            foreach (FieldObject fieldObject in field.FieldObjects)
            {
                reportGridView[1, index].Value = fieldObject.Position.X;
                reportGridView[2, index].Value = fieldObject.Position.Y;
                index++;
            }
        }

        public void ClearData()
        {
            reportGridView.Rows.Clear();
        }

        private void Report_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = (e.CloseReason == CloseReason.UserClosing);
        }
    }
}
