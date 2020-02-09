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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            UI_InicioDeSesion form = new UI_InicioDeSesion();
            form.SetAuthorized(false);
        }

        //Variables

        string masterKey = "R4BAgEPKEDI0LBub7VdpUnWwMoybK44meKU1vk3naxGT0OsYeNPRT2U0DxNzmYx";

        //Variables

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == masterKey)
            {
                UI_InicioDeSesion form = new UI_InicioDeSesion();
                form.SetAuthorized(true);
                this.Close();
            }
            else
            {
                UI_InicioDeSesion form = new UI_InicioDeSesion();
                form.SetAuthorized(false);
                this.Close();
            }
        }
    }
}
