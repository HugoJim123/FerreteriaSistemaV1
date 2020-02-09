using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace LoginFerreteria
{
    public partial class LoginUIFerreteria : Form
    {
        public LoginUIFerreteria()
        {
            InitializeComponent();
        }

        int type = 0;
        Color YourCustomColor = Color.FromArgb(50, 57, 66);
        Color YourCustomColor2 = Color.FromArgb(216, 222, 231);

        private void LoginUIFerreteria_Paint(object sender, PaintEventArgs e)
        {
            System.Drawing.Graphics graphics = e.Graphics;
            System.Drawing.Rectangle gradient_rectangle = new System.Drawing.Rectangle(0, 0, this.Width, this.Height);
            System.Drawing.Brush b = new System.Drawing.Drawing2D.LinearGradientBrush(gradient_rectangle, YourCustomColor, YourCustomColor2, 65f);
            graphics.FillRectangle(b, gradient_rectangle);
        }

        private void LoginUIFerreteria_Resize(object sender, EventArgs e)
        {
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxUserName_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBoxUserName_Click(object sender, EventArgs e)
        {
        }

        private void LoginUIFerreteria_Load(object sender, EventArgs e)
        {
            textBoxUserName.Text = "Nombre de usuario";
            textBoxUserName.ForeColor = Color.FromArgb(129, 132, 138);
            textBoxUserName.GotFocus += new EventHandler(this.TextGotFocus);
            textBoxUserName.LostFocus += new EventHandler(this.TextLostFocus);

            textBoxPassword.Text = "Contraseña";
            textBoxPassword.ForeColor = Color.FromArgb(129, 132, 138);
            textBoxPassword.GotFocus += new EventHandler(this.TextGotFocus);
            textBoxPassword.LostFocus += new EventHandler(this.TextLostFocus);
        }


        public void TextGotFocus(object sender, EventArgs e)
        {
            TextBox txtbox = sender as TextBox;
            if (txtbox.Name == "textBoxUserName")
            {
                if (txtbox.Text == "Nombre de usuario")
                {
                    txtbox.Text = "";
                    txtbox.ForeColor = Color.FromArgb(216, 222, 231);
                }
            }else if(txtbox.Name == "textBoxPassword")
            {
                if (txtbox.Text == "Contraseña")
                {
                    txtbox.Text = "";
                    txtbox.ForeColor = Color.FromArgb(216, 222, 231);
                    txtbox.PasswordChar = '*';
                }
            }
        }

        public void TextLostFocus(object sender, EventArgs e)
        {            
            TextBox txtbox = sender as TextBox;
            if(txtbox.Text == "")
            {
                if (txtbox.Name == "textBoxUserName")
                {
                    txtbox.Text = "Nombre de usuario";
                    txtbox.ForeColor = Color.FromArgb(129, 132, 138);
                }
                else if (txtbox.Name == "textBoxPassword")
                {
                    txtbox.Text = "Contraseña";
                    txtbox.ForeColor = Color.FromArgb(129, 132, 138);
                    txtbox.PasswordChar = '\0';
                }
            }
        }
    }
}
