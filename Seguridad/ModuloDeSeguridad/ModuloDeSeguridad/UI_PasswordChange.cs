using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaDatosFerreteria;

namespace ModuloDeSeguridad
{
    public partial class UI_PasswordChange : Form
    {
        public UI_PasswordChange(string userCode)
        {
            InitializeComponent();
            userCod = userCode;
        }

        //Variables

        bool stateEye1 = false;
        bool stateEye2 = false;
        string userCod = "";

        //Variables

        private void buttonSeePassword1_Click(object sender, EventArgs e)
        {
            switch((sender as Button).Name)
            {
                case "buttonSeePassword1":
                    stateEye1 = true;
                    break;
                case "buttonSeePassword2":
                    stateEye1 = false;
                    break;
                case "buttonSeePassword3":
                    stateEye2 = true;
                    break;
                case "buttonSeePassword4":
                    stateEye2 = false;
                    break;
            }
            if(stateEye1 == true)
            {
                buttonSeePassword1.Visible = false;
                buttonSeePassword2.Visible = true;
                textBoxPassword.PasswordChar = '\0';
            }
            else
            {
                buttonSeePassword1.Visible = true;
                buttonSeePassword2.Visible = false;
                textBoxPassword.PasswordChar = '*';
            }
            if (stateEye2 == true)
            {
                buttonSeePassword3.Visible = false;
                buttonSeePassword4.Visible = true;
                textBoxRepeatPassword.PasswordChar = '\0';
            }
            else
            {
                buttonSeePassword3.Visible = true;
                buttonSeePassword4.Visible = false;
                textBoxRepeatPassword.PasswordChar = '*';
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonSiguiente_Click(object sender, EventArgs e)
        {
            ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            try
            {
                cpd.ModificarDatos("tbl_Usuario", "Password = '" + cpd.Encriptar(textBoxPassword.Text) + "'", "id", userCod, "Actualización de Contraseña");
                MessageBox.Show("Contraseña actualizada", "Datos guardados correctamente", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error al Guardar Los Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonSiguiente_EnabledChanged(object sender, EventArgs e)
        {
            if (buttonSiguiente.Enabled == true) buttonSiguiente.BackColor = Color.FromArgb(49, 156, 202);
            else buttonSiguiente.BackColor = SystemColors.ControlDarkDark;
        }

        private void textBoxPassword_TextChanged(object sender, EventArgs e)
        {
            if (textBoxPassword.Text != "" && textBoxRepeatPassword.Text != "" && textBoxPassword.Text == textBoxRepeatPassword.Text) buttonSiguiente.Enabled = true;
            else buttonSiguiente.Enabled = false;
        }
    }
}
