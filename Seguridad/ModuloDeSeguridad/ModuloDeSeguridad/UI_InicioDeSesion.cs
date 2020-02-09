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
using CapaDatosFerreteria;
using CacheFerreteria;
using System.Net.Mail;
using System.Net;
using System.Configuration;
using System.Net.Configuration;

namespace ModuloDeSeguridad
{
    public partial class UI_InicioDeSesion : Form
    {
        public UI_InicioDeSesion()
        {
            InitializeComponent();
            LoadForm();
            cargarImagen();
            textBoxPassword.Text = "Contraseña";
            textBoxPassword.ForeColor = Color.FromArgb(129, 132, 138);
            textBoxPassword.GotFocus += new EventHandler(this.TextGotFocus);
            textBoxPassword.LostFocus += new EventHandler(this.TextLostFocus);
        }

        public static bool inicioSesionExitoso = false;     //Establece si la sesion se inicio correctamente
        public static string nickname = "";     //Almacena permanente el nombre del usuario
        public static string imagenRuta = "";   //Almacena permanente la ruta de la imagen
        public static string permisosReal = ""; //Almacena permanente los permisos
        public string img_path = "";    //Almacena temporalmente la imagen
        public string permisos = "";    //Almacena temporalmente los permisos
        public static bool authorizedIn = false;

        public void LoadForm()
        {
            ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            try
            {
                cpd.cargarDatosACombobox(comboBoxNickname, "Nickname", "tbl_usuario WHERE status = 1");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error al Cargar Los Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void TextGotFocus(object sender, EventArgs e)
        {
            TextBox txtbox = sender as TextBox;
            if (txtbox.Name == "textBoxPassword")
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
            if (txtbox.Text == "")
            {
                if (txtbox.Name == "textBoxPassword")
                {
                    txtbox.Text = "Contraseña";
                    txtbox.ForeColor = Color.FromArgb(129, 132, 138);
                    txtbox.PasswordChar = '\0';
                }
            }
        }
        private void pictureBoxUserPhoto_Paint(object sender, PaintEventArgs e)
        {
            GraphicsPath figura = new GraphicsPath();
            int x, y, ancho, alto;
            ancho = pictureBoxUserPhoto.Width;
            alto = pictureBoxUserPhoto.Height;
            y = (pictureBoxUserPhoto.Height - alto) / 2;
            x = y;
            figura.AddEllipse(new RectangleF(x, y, ancho, alto));
            pictureBoxUserPhoto.Region = new Region(figura);
        }

        public void cargarImagen()
        {
            ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            try
            {
                img_path = cpd.ExtraerCodigo("image_path", "tbl_Usuario", "Nickname", comboBoxNickname.Text);
                if(img_path != "" && img_path != " ")
                {
                    pictureBoxUserPhoto.BackgroundImageLayout = ImageLayout.Zoom;
                    pictureBoxUserPhoto.BackgroundImage = Image.FromFile(img_path);
                }
                else
                {
                    pictureBoxUserPhoto.BackgroundImageLayout = ImageLayout.Center;
                    pictureBoxUserPhoto.BackgroundImage = pictureBoxUserPhoto.ErrorImage;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error al Guardar Los Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBoxNickname_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarImagen();
        }

        public void extraerPermisos()
        {
            ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            try
            {
                permisos = cpd.ExtraerCodigo2("CadenaPermisos",
                    "tbl_PerfilEncabezado, tbl_Usuario, tbl_PerfilUsuario",
                    "tbl_Usuario.Nickname", "'" + nickname + "' " +
                    "AND tbl_PerfilUsuario.tbl_Usuario_id = tbl_Usuario.id " +
                    "AND tbl_PerfilUsuario.tbl_PerfilEncabezado_id = tbl_PerfilEncabezado.id ");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error al Cargar Los Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public string getPermisos()
        {
            return permisosReal;
        }

        public bool loginProcess()
        {
            return inicioSesionExitoso;
        }

        public string getNickname()
        {
            return nickname;
        }

        public string getImg_Path()
        {
            return imagenRuta;
        }

        public string getUserCodeForLog()
        {
            ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            try
            {
                return cpd.ExtraerCodigo("id", "tbl_usuario", "Nickname", comboBoxNickname.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error al Iniciar Sesion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }
        }

        private void buttonIniciarSesion_Click(object sender, EventArgs e)
        {
            if(textBoxPassword.Text == "" || textBoxPassword.Text == "Contraseña")
            {
                MessageBox.Show("Porfavor ingrese la contraseña", "Error al Iniciar Sesion", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
                string password = "";
                try
                {
                    password = cpd.ExtraerCodigo("Password", "tbl_Usuario", "Nickname", comboBoxNickname.Text);
                    if(cpd.Encriptar(textBoxPassword.Text) == password)
                    {                        
                        ClassCacheFerreteria ccf = new ClassCacheFerreteria();
                        ccf.setUsuario(getUserCodeForLog());
                        inicioSesionExitoso = true;
                        nickname = comboBoxNickname.Text;
                        imagenRuta = img_path;
                        ClaseBitacoraFerreteria cpb = new ClaseBitacoraFerreteria();
                        cpb.setBitacora("Inicio de sesion de " + nickname);
                        extraerPermisos();
                        if(permisos != "")
                        {
                            permisosReal = permisos;
                        }
                        else { permisosReal = "0000000"; }
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Usuario o Contraseña incorrecto", "Error al Iniciar Sesion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Error al Iniciar Sesion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void labelOlvideContrasena_Click(object sender, EventArgs e)
        {
            UI_SeleccionDeUsuario usu = new UI_SeleccionDeUsuario();
            usu.ShowDialog();
        }

        public void SetAuthorized(bool autho)
        {
            authorizedIn = autho;
        }

        private void buttonRestore_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            form.ShowDialog();
            if (authorizedIn)
            {
                inicioSesionExitoso = true;
                permisosReal = "1111111";
            }
            else
            {
                inicioSesionExitoso = false;
                permisosReal = "0000000";
            }
            this.Close();
        }
    }
}
