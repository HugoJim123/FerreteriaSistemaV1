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
using System.Net.Mail;
using System.Net;
using System.Configuration;
using System.Threading;
using System.Net.Configuration;

namespace ModuloDeSeguridad
{
    public partial class UI_RestaurarPassword : Form
    {

        //Variables
        string codigoUser = "";
        string codeKey = "";
        string bodyMail = "<h1>Cambiando contraseña<h1><h3>Por favor, copie el siguiente código e insertelo en la pestaña que lo pide.<h3><br/><br/><br/>";
        //Variables

        public UI_RestaurarPassword(string codigoUsuario)
        {
            InitializeComponent();
            codigoUser = codigoUsuario;
            //GenerarKey();
            SendMail();
        }

        public int GenerarRandomNumber()
        {
            Random random = new Random();
            int randomNumber = random.Next(0, 9);
            return randomNumber;
        }

        public string GenerarRandomLetra()
        {
            Random rnd = new Random();
            char randomChar = (char)rnd.Next('A', 'Z');
            return randomChar.ToString();
        }

        public string GenerarKey()
        {
            string key = "";
            for(int i = 0; i <= 100; i++)
            {
                key += GenerarRandomNumber().ToString();
                key += GenerarRandomLetra().ToString();
                codeKey += GenerarRandomNumber().ToString();
                codeKey += GenerarRandomLetra().ToString();
                if (i == 25 || i == 50 || i == 75)
                {
                    key += "<br/>";
                }
                Thread.Sleep(15);
            }
           // MessageBox.Show(key);
            return key;
        }

        public string MailUsuario()
        {
            string mail = "";
            ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            try
            {
                mail = cpd.ExtraerCodigo2("Correo",
                    "tbl_Correo, tbl_Usuario, tbl_Empleado, tbl_Persona",
                    "tbl_correo.tbl_persona_id = tbl_persona.id " +
                    "and tbl_Persona.status = 1 " +
                    "and tbl_empleado.status = 1 " +
                    "and tbl_empleado.tbl_persona_id = tbl_persona.id " +
                    "and tbl_usuario.tbl_empleado_dpi = tbl_empleado.dpi " +
                    "and tbl_usuario.id ", codigoUser);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error al Cargar Los Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return mail;
        }

        public void SendMail()
        {
            try
            {
                bodyMail += GenerarKey();
                MailMessage email = new MailMessage();
                email.To.Add(new MailAddress(MailUsuario()));
                //email.To.Add(new MailAddress("hugojiim123@gmail.com"));
                email.From = new MailAddress("ferresis123@gmail.com");
                email.Subject = "Restauración de Contraseña.   " + "(" + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss") + ")";
                bodyMail += "<br/><br/><br/>(" + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss") + ")";
                email.Body = bodyMail;
                email.IsBodyHtml = true;
                email.Priority = MailPriority.Normal;

                ///
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("ferresis123@gmail.com", "Credencial2019");

                ///
                try
                {
                    smtp.Send(email);
                    email.Dispose();
                    MessageBox.Show("Correo enviado con éxito, por favor revise su badeja de entrada.", "Correo Enviado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Error al Enviar el correo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error al Enviar el correo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonSiguiente_Click(object sender, EventArgs e)
        {
            string keyComparasion = textBoxKey.Text.Replace(Environment.NewLine, "");
            if(keyComparasion != codeKey)
            {
                MessageBox.Show("El numero de la llave no coincide, por favor verifique los datos", "Error en la llave", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                UI_PasswordChange upc = new UI_PasswordChange(codigoUser);
                upc.ShowDialog();
                this.Close();
            }
        }
    }
}
