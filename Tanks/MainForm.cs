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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            KeyPreview = true;
            InitializeComponent();
        }
    }
}
