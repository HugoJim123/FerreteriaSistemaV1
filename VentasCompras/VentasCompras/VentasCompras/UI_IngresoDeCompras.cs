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

namespace VentasCompras
{
    public partial class UI_IngresoDeCompras : Form
    {
        public UI_IngresoDeCompras()
        {
            InitializeComponent();
            LoadForm();
        }


        // Variables

        string datosEmpleado = "";
        string NicknameEmpleado = "";
        string ordenDeCompra = "";
        string codigoProducto = "";

        // Variables

        // Funciones Varias

        void LoadForm()
        {
            CapaDatosFerreteria.ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            ClaseBitacoraFerreteria cbf = new ClaseBitacoraFerreteria();
            try
            {
                //Carga de datos del empleado
                datosEmpleado = cpd.ExtraerCodigo3("CONCAT(Apellidos,',',Nombre) AS ", "NombreC", "" +
                    " bd_ferreteria.tbl_Persona, bd_ferreteria.tbl_usuario, bd_ferreteria.tbl_Empleado",
                    "tbl_Empleado.status ", " 1 AND tbl_Persona.status = 1 " +
                    "AND tbl_Persona.id = tbl_Empleado.tbl_Persona_id " +
                    "AND tbl_usuario.tbl_empleado_DPI = tbl_empleado.DPI " +
                    "AND tbl_usuario.id = " + cbf.GetUserCodeForLog()
                    );
                //Carga de nickname del usuario
                NicknameEmpleado = cpd.ExtraerCodigo2("Nickname", "tbl_usuario", "id", cbf.GetUserCodeForLog());
                //Carga del combobox de proveedores
                cpd.cargarDatosACombobox2(comboBoxProveedor,
                    "tbl_organizacion.Nombre as NombreC", "Nombre",
                    "tbl_organizacion, tbl_proveedor " +
                    "WHERE " +
                    "tbl_proveedor.tbl_organizacion_id = tbl_organizacion.id " +
                    "AND tbl_proveedor.status = 1");
                //Cargar datos de los productos
                cpd.cargarDatosACombobox2(comboBoxProducto, " CONCAT(id,',',Nombre) AS NombreC", "Nombre", "" +
                    "tbl_Producto WHERE tbl_Producto.status = 1 ");
                //Cargar datos de los productos
                cpd.cargarDatosACombobox2(comboBoxBodega, " CONCAT(id,',',NombreBodega) AS NombreC", "Nombre", "" +
                    "tbl_bodega WHERE tbl_bodega.status = 1 ");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR AL CARGAR DATOS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //Asignacion de datos a los labels
            labelEmpleado.Text = datosEmpleado;
            labelNoOrdenCompra.Text = nextCodigo();
            ordenDeCompra = nextCodigo();
            labelFechaEntrega.Text = DateTime.Now.ToShortDateString();
            labelNickname.Text = NicknameEmpleado;
        }


        public string nextCodigo()
        {
            ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            try
            {
                return cpd.ExtraerCodigo2("AUTO_INCREMENT", "information_schema.TABLES", "TABLE_SCHEMA = 'bd_ferreteria' " +
                    "AND TABLE_NAME", "'tbl_comprasEncabezado'");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR AL CARGAR DATOS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }
        }
        /// <summary>
        /// Carga los datos en caso de ser una modificacion de producto
        /// </summary>
        /// <param name="codigoACambiar"></param>
        void cargardatos()
        {
            string[] cdProducto = comboBoxProducto.Text.Split(',');
            codigoProducto = cdProducto[0];
            ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            string[] datos;
            datos = new string[18];
            try
            {
                datos = cpd.cargarDetallesProductos(cdProducto[0], "Vista de detalles de productos");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR AL CARGAR DATOS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            ///     Carga del detalle del producto (Lo que no se va a modificar)
            labelCodigoDetalle.Text = datos[0];
            labelNombreDetalle.Text = datos[1];
            labelMaterialDetalle.Text = datos[13];
            labelTipoDetalle.Text = datos[14];
        }


        public void cargarDetalle()
        {
            ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            DataSet ds;
            try
            {
                ds = cpd.LlenarGrid("" +
                    "tbl_comprasDetalle.id as CODIGO, " +
                    "tbl_producto.Nombre as PRODUCTO, " +
                    "tbl_comprasDetalle.Cantidad as CANTIDAD, " +
                    "tbl_comprasDetalle.Precio as PRECIO, " +
                    "tbl_comprasDetalle.Subtotal as SUBTOTAL ",
                    "tbl_comprasDetalle, " +
                    "tbl_Producto ",
                    "tbl_comprasDetalle.tbl_comprasEncabezado_id = " + labelNoOrdenCompra.Text + " " +
                    "AND tbl_comprasDetalle.tbl_producto_id = tbl_Producto.id", "Visualizacion de datos detalles de compras");
                dtg_ProductosDetalle.DataSource = ds.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR AL GUARDAR DATOS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        double sumarFilas()
        {
            double sumTotal = 0;
            int numColumna = 4;
            try
            {
                foreach (DataGridViewRow row in dtg_ProductosDetalle.Rows)
                {
                    sumTotal += Convert.ToDouble(row.Cells[numColumna].Value);
                }
            }
            catch (Exception ex) { }
            return sumTotal;
        }

        void ActualizarTotal()
        {
            ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            try
            {
                cpd.ModificarDatos("tbl_ComprasEncabezado", "Total=" + labelTotalOrden.Text, "id", labelNoOrdenCompra.Text, "Ingreso de Total");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR AL GUARDAR DATOS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Funciones Varias

        private void comboBoxProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargardatos();
            ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            try
            {
                numericUpDownPrecioCompra.Value = Convert.ToInt32(cpd.ExtraerCodigo2("PrecioCompra", "tbl_Producto", "id", codigoProducto));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR AL CARGAR DATOS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void numericUpDownPrecioVenta_ValueChanged(object sender, EventArgs e)
        {
        }

        private void buttonAgregarEncabezado_Click(object sender, EventArgs e)
        {
            panelProductos.Visible = true;
            panelDataGird.Visible = true;
            string codigoProveedor = "";
            string codigoEmpleado = "";
            DateTime fEntrega = Convert.ToDateTime(labelFechaEntrega.Text);
            DateTime fCancelacion = Convert.ToDateTime(dateTimePickerFechaCancelacion.Value.ToString("yyyy-MM-dd"));
            ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            try
            {
                codigoEmpleado = cpd.ExtraerCodigo("tbl_Empleado_DPI", "tbl_Usuario", "Nickname", labelNickname.Text);
                codigoProveedor = cpd.ExtraerCodigo3("tbl_proveedor.id AS ","NombreC", "tbl_Proveedor, tbl_Organizacion", "tbl_Organizacion.Nombre = '" + comboBoxProveedor.Text
                    + "' AND tbl_Organizacion.id ", " tbl_Proveedor.tbl_Organizacion_id");
                cpd.InsertDatos("tbl_ComprasEncabezado", "0, " + codigoProveedor + ", " + fEntrega.ToString("yyyyMMdd") + ", " + fCancelacion.ToString("yyyyMMdd") +
                    " ," + labelTotalOrden.Text + " ," + codigoEmpleado + ", 1, 0", "Ingreso de orden de compra");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR AL GUARDAR DATOS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonAgregarProducto_Click(object sender, EventArgs e)
        {
            string[] datos = comboBoxProducto.Text.Split(',');
            ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            try
            {
                cpd.InsertDatos("tbl_comprasDetalle", "0, " + labelNoOrdenCompra.Text + ", " + datos[0] + ", " + numericUpDownCant.Value.ToString()
                    + ", " + numericUpDownPrecioCompra.Value.ToString() + ", " + textBoxSubtotal.Text, "Ingreso de Detalle de Orden de compra");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR AL GUARDAR DATOS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            cargarDetalle();
            labelTotalOrden.Text = sumarFilas().ToString();
            ActualizarTotal();
        }

        private void numericUpDownPrecioCompra_ValueChanged(object sender, EventArgs e)
        {
            textBoxSubtotal.Text = (numericUpDownCant.Value * numericUpDownPrecioCompra.Value).ToString();
        }

        private void numericUpDownCant_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void numericUpDownPrecioCompra_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void textBoxSubtotal_TextChanged(object sender, EventArgs e)
        {
            if (textBoxSubtotal.Text != "")
            {
                buttonAgregarProducto.Enabled = true;
            }
            else
            {
                buttonAgregarProducto.Enabled = false;
            }
        }

        private void buttonAddPais_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
