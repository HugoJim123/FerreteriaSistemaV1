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
using System.Drawing.Drawing2D;

namespace ModuloDeSeguridad
{
    public partial class UI_SeleccionDeUsuario : Form
    {
        public UI_SeleccionDeUsuario()
        {
            InitializeComponent();
            LoadForm();
            cargarImagen();
        }

        //Variables
        public string img_path = "";    //Almacena temporalmente la imagen

        //Variables

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
                if (img_path != "" && img_path != " ")
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

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonSiguiente_Click(object sender, EventArgs e)
        {
            string codigoUU = "";
            ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            try
            {
                codigoUU = cpd.ExtraerCodigo("id", "tbl_Usuario", "Nickname", comboBoxNickname.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error al Cargar Los Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            UI_RestaurarPassword urp = new UI_RestaurarPassword(codigoUU);
            urp.ShowDialog();
            this.Close();
        }

        private void comboBoxNickname_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarImagen();
        }
    }
}
