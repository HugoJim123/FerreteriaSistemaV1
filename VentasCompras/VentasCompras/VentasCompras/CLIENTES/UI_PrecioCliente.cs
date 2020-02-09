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

namespace VentasCompras.CLIENTES
{
    public partial class UI_PrecioCliente : Form
    {
        public UI_PrecioCliente()
        {
            InitializeComponent();
            LoadForm();
        }

        //Variables
        string[] datosProducto;
        string[] datosCliente;

        public void LoadForm()
        {
            CapaDatosFerreteria.ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            try
            {
                //Carga del combobox de proveedores
                cpd.cargarDatosACombobox2(comboBoxCliente,
                    "Concat(tbl_cliente.NIT,',',tbl_persona.Nombre,',',tbl_organizacion.Nombre) as NombreC ", "NombreC",
                    "tbl_persona, tbl_organizacion, tbl_cliente " +
                    "WHERE " +
                    "tbl_organizacion.id = tbl_cliente.tbl_organizacion_id " +
                    "AND tbl_cliente.tbl_Persona_id = tbl_Persona.id " +
                    "AND tbl_cliente.status = 1");
                //Cargar productos
                cpd.cargarDatosACombobox2(comboBoxProducto, " CONCAT(id,',',Nombre) AS NombreC", "Nombre", "" +
                    "tbl_Producto WHERE tbl_Producto.status = 1 ");
                CargarGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR AL CARGAR DATOS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void CargarGrid()
        {
            CapaDatosFerreteria.ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            try
            {
                //Cargar DGV
                DataSet ds;
                ds = cpd.LlenarGrid("tbl_producto.Nombre as 'PRODUCTOS', " +
                    "tbl_producto_tbl_cliente.PrecioVenta as 'PRECIO' "
                    ,
                    "tbl_producto_tbl_cliente, tbl_producto "
                    ,
                    "tbl_producto.id = tbl_producto_tbl_cliente.tbl_producto_id AND " +
                    "tbl_producto_tbl_cliente.tbl_cliente_NIT = " + datosCliente[0]
                    ,
                    "Visualizacion de Precios por cliente"
                    );
                DataGridViewClientes.DataSource = ds.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR AL CARGAR DATOS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBoxCliente_TextChanged(object sender, EventArgs e)
        {
            datosProducto = comboBoxProducto.Text.Split(',');
            datosCliente = comboBoxCliente.Text.Split(',');
            CargarGrid();
        }

        private void numericUpDownPrecio_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDownPrecio.Value >= 0) buttonAdd.Enabled = true;
            else buttonAdd.Enabled = false;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            CapaDatosFerreteria.ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            try
            {
                cpd.InsertDatos("tbl_producto_tbl_cliente",
                        datosProducto[0] + ", " + datosCliente[0] + ", " + numericUpDownPrecio.Value.ToString() + ")  ON DUPLICATE KEY UPDATE " +
                        "PrecioVenta = " + numericUpDownPrecio.Value.ToString() + "; -- ", "Ingreso de Precio a cliente");
                MessageBox.Show("Se ha Realizado el ingreso de los datos.", "DATOS GUARDADOS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR AL GUARDAR DATOS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
