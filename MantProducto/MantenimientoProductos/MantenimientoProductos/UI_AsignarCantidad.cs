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
    public partial class UI_AsignarCantidad : Form
    {
        public UI_AsignarCantidad(string codBodega12, string codBodega22, string codProducto2)
        {
            InitializeComponent();
            codBodega1 = codBodega12;
            codBodega2 = codBodega22;
            codProducto = codProducto2;
            LoadForm();
        }

        //Variables

        string codBodega1 = "", codBodega2 = "", codProducto = "";

        //Variables

        //Funciones

        void LoadForm()
        {
            ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            // Llenar datagridview Principal
            try
            {
                textBoxCantidadTotal.Text = cpd.ExtraerCodigo2("Cantidad", "tbl_productobodega", "tbl_Bodega_id",
                    codBodega1 + " AND tbl_Producto_id = " + codProducto);
                textBoxProducto.Text = cpd.ExtraerCodigo("Nombre", "tbl_Producto", "id", codProducto);
                labelDe.Text = cpd.ExtraerCodigo2("NombreBodega", "tbl_Bodega", "id", codBodega1);
                labelHacia.Text = cpd.ExtraerCodigo2("NombreBodega", "tbl_Bodega", "id", codBodega2);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error al Cargar los Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if(textBoxCantidadTotal.Text == "") textBoxCantidadTotal.Text = "0";        // Si noy hay valor, la cantidad es cero
        }

        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Funciones
        private void numericUpDownCantidadAPagar_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDownCantidadAPagar.Value <= Convert.ToInt32(textBoxCantidadTotal.Text)) buttonAceptar.Enabled = true;
            else buttonAceptar.Enabled = false;
        }

        private void buttonAceptar_Click(object sender, EventArgs e)
        {
            List<string> queryList = new List<string>();
            ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            try
            {
                //Agregar al inventario
                string CantidadEnExistenciaBodega1 = cpd.ExtraerCodigo2("Cantidad", "tbl_productobodega", "tbl_Bodega_id",
                    codBodega1 + " AND tbl_Producto_id = " + codProducto);
                string CantidadEnExistenciaBodega2 = cpd.ExtraerCodigo2("Cantidad", "tbl_productobodega", "tbl_Bodega_id",
                    codBodega2 + " AND tbl_Producto_id = " + codProducto);
                if (CantidadEnExistenciaBodega1 == "") CantidadEnExistenciaBodega1 = "0";
                if (CantidadEnExistenciaBodega2 == "") CantidadEnExistenciaBodega2 = "0";
                int nuevaCantidadBodega1 = Convert.ToInt32(CantidadEnExistenciaBodega1) - Convert.ToInt32(numericUpDownCantidadAPagar.Value);
                int nuevaCantidadBodega2 = Convert.ToInt32(CantidadEnExistenciaBodega2) + Convert.ToInt32(numericUpDownCantidadAPagar.Value);
                queryList.Add(cpd.DevolverSentencia_Insert("tbl_productobodega",
                    codBodega1 + ", " + codProducto + ", " + nuevaCantidadBodega1 + ")  ON DUPLICATE KEY UPDATE " +
                    "Cantidad = " + nuevaCantidadBodega1 + "; -- "));
                queryList.Add(cpd.DevolverSentencia_Insert("tbl_productobodega",
                    codBodega2 + ", " + codProducto + ", " + nuevaCantidadBodega2 + ")  ON DUPLICATE KEY UPDATE " +
                    "Cantidad = " + nuevaCantidadBodega2 + "; -- "));
                //Agregar el movimiento
                queryList.Add(cpd.DevolverSentencia_Insert("tbl_movimientoInventario",
                    "0, 1, " + codProducto + ", " + DateTime.Today.ToString("yyyyMMdd") + ", " + numericUpDownCantidadAPagar.Value +
                 ", 0, 0, " + codBodega2));                cpd.EjectuarOperacionExterna(queryList, "", "", "Registro de compra");
                MessageBox.Show("Traslado de productos exitoso", "DATOS GUARDADOS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error al Guardar los Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
