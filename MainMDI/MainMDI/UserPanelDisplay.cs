using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ModuloDeSeguridad;

namespace MainMDI
{
    public partial class UserPanelDisplay : Form
    {
        public UserPanelDisplay()
        {
            InitializeComponent();
            stateForm = false;
        }

        public bool stateSesion = false;
        public static bool stateForm = false;

        private void UserPanelDisplay_Deactivate(object sender, EventArgs e)
        {
            this.Close();
        }

        public bool getState()
        {
            return stateForm;
        }

        private void buttonCerrarSesion_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UserPanelDisplay_Leave(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UserPanelDisplay_Shown(object sender, EventArgs e)
        {
            //base.OnShown(e);
            this.Capture = true;
        }

        private void UserPanelDisplay_MouseCaptureChanged(object sender, EventArgs e)
        {
            if (!this.Capture)
            {
                if (!this.RectangleToScreen(this.DisplayRectangle).Contains(Cursor.Position))
                {
                    this.Close();
                }
                else
                {
                    this.Capture = true;
                }
            }
            //base.OnMouseCaptureChanged(e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClaseSeguridadModulo csm = new ClaseSeguridadModulo();
            csm.registrarSalidaSesion();
            stateForm = true;
            this.Close();
        }
    }
}
