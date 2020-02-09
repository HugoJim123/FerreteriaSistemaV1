using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModuloDeSeguridad
{
    public partial class UI_SinPermisos : Form
    {
        public UI_SinPermisos()
        {
            InitializeComponent();
        }

        private void panel1_SizeChanged(object sender, EventArgs e)
        {
        }

        private void panel2_SizeChanged(object sender, EventArgs e)
        {
            panel2.Height = Convert.ToInt32(panel2.Width * 0.252);
        }
    }
}
