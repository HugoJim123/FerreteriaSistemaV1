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
    public partial class UI_AsignacionDeProductoABodega : Form
    {
        string codigoproducto = "";
        string nombreproducto = "";
        public UI_AsignacionDeProductoABodega(string codigoproducto2, string nombreProducto2)
        {
            InitializeComponent();
            codigoproducto = codigoproducto2;
            nombreproducto = nombreProducto2;
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UI_AsignacionDeProductoABodega_Load(object sender, EventArgs e)
        {
            string valorabuscar = codigoproducto + "-" + nombreproducto;
            ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            cpd.cargarDatosConIDACombobox(comboBoxProducto, "id","Nombre", "tbl_producto");
            cpd.cargarDatosConIDACombobox(comboBoxBodega, "id", "NombreBodega", "tbl_bodega");
            comboBoxProducto.SelectedItem = valorabuscar;
            /*MessageBox.Show(valorabuscar);
            MessageBox.Show(comboBoxProducto.SelectedIndex.ToString());
            MessageBox.Show(comboBoxProducto.Text);*/
        }

        private void comboBoxProducto_TextChanged(object sender, EventArgs e)
        {
            string codigoProducto = "", NombreProducto = "";
            string[] codeList = comboBoxProducto.Text.Split('-');
            codigoProducto = codeList[0];
            NombreProducto = codeList[1];

            /// Carga el detalle

            ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            string[] datos;
            datos = new string[18];
            datos = cpd.cargarDetallesProductos(codigoProducto , "Carga de detalles de productos");
            labelVoltaje.Text = datos[2] + " V";
            labelAmperaje.Text = datos[3] + " A";
            labelCapacitancia.Text = datos[4] + " C";
            labelResistencia.Text = datos[5] + " R";
            labelTamaño.Text = datos[6] + " cm";
            labelDiametro.Text = datos[7] + " cm";
            labelFuerza.Text = datos[8] + " N";
            labelTemperatura.Text = datos[9] + " °C";
            labelRPM.Text = datos[10];
            labelHz.Text = datos[11];
            labelPrecio.Text = "Q " + datos[12];
            labelPais.Text = datos[13];
            labelMarca.Text = datos[14];
            labelColor.Text = datos[15];
            labelObservaciones.Text = datos[16];
                //cargarExistencias();
        }

        void cargarExistencias()
        {
            //MessageBox.Show("--" + comboBoxBodega.Text + " -- " + comboBoxProducto.Text+ "--" );
            if (comboBoxProducto.Text == "" || comboBoxBodega.Text == "")
            {
            }
            else
            {
                string codigoProduct = "", codigoBodega = "";
                string[] codeList = comboBoxProducto.Text.Split('-');
                codigoProduct = codeList[0];
                string[] codeList2 = comboBoxBodega.Text.Split('-');
                codigoBodega = codeList2[0];
                ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
                string cant = cpd.ExtraerCodigo2("cantidad", "tbl_productobodega", "tbl_bodega_id = " + codigoBodega + " AND tbl_producto_id ", codigoProduct);
                //MessageBox.Show("-" + cant + "-");
                if (cant != "")
                {
                    numericUpDownCantidad.Value = Convert.ToInt32(cant);
                }
                else
                {
                    numericUpDownCantidad.Value = 0;
                }
            }
        }

        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonGuardar_Click(object sender, EventArgs e)
        {
            string codigoProducto = "", codigoBodega = "";
            string[] codeList = comboBoxProducto.Text.Split('-');
            codigoProducto = codeList[0];
            string[] codeList2 = comboBoxBodega.Text.Split('-');
            codigoBodega = codeList2[0];
            // MessageBox.Show(codigoProducto + " - " + codigoBodega);
            ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            cpd.InsertDatos("tbl_productoBodega", codigoBodega + ", " + codigoProducto + ", " +  numericUpDownCantidad.Value,"Insercion de datos en productos a bodegas");
        }

        private void comboBoxBodega_TextChanged(object sender, EventArgs e)
        {
        }

        private void comboBoxBodega_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarExistencias();
        }
    }
}
