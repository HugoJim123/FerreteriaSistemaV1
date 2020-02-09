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

namespace MantenimientoProductos
{
    public partial class UI_DetalleProductos : Form
    {
        String codigo;
        public UI_DetalleProductos(String codigo2)
        {
            InitializeComponent();
            codigo = codigo2;
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonDetails_Click(object sender, EventArgs e)
        {
            ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            string[] datos;
            datos = new string[18];
            datos = cpd.cargarDetallesProductos(codigo, "Carga de detalles de productos");
            int tipoSeleccion = 0;
            if((sender as Button).Name == "buttonExistencias") tipoSeleccion = 0;
            else tipoSeleccion = 5;
            UI_Existencias_MovimientosProductos form = new UI_Existencias_MovimientosProductos(tipoSeleccion, datos[0], "0", "0");
            form.ShowDialog();
            LoadForm();
        }

        void LoadForm()
        {
            ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            string[] datos;
            datos = new string[18];
            datos = cpd.cargarDetallesProductos(codigo, "Carga de detalles de productos");
            labelNombreProducto.Text = datos[1];
            labelVoltaje.Text = datos[2] ;
            labelAmperaje.Text = datos[3] ;
            labelCapacitancia.Text = datos[4] ;
            labelResistencia.Text = datos[5]  ;
            labelTamaño.Text = datos[6] ;
            labelDiametro.Text = datos[7] ;
            labelFuerza.Text = datos[8] ;
            labelTemperatura.Text = datos[9];
            labelRPM.Text = datos[10];
            labelHz.Text = datos[11];
            labelPrecio.Text = "Q " + datos[12];
            labelPrecioCompra.Text = "Q " + datos[20];
            labelMaterial.Text = datos[13];
            labelTipo.Text = datos[14];
            labelPais.Text = datos[15];
            labelMarca.Text = datos[16];
            labelColor.Text = datos[17];
            labelObservaciones.Text = datos[18];
            if (datos[19] == "")
            {
                pictureBox1.Image = Image.FromFile(@"C:\Ferreteria\Imagenes\closeBtn.png");
                pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            }
            else
            {
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox1.Image = Image.FromFile(datos[19]);
            }
        }

        private void UI_DetalleProductos_Load(object sender, EventArgs e)
        {
            LoadForm();
        }
    }
}
