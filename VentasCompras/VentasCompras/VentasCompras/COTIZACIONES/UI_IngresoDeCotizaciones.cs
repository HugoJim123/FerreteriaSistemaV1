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

namespace VentasCompras.COTIZACIONES
{
    public partial class UI_IngresoDeCotizaciones : Form
    {
        public UI_IngresoDeCotizaciones()
        {
            InitializeComponent();
            LoadForm();
        }

        // Variables

        string datosEmpleado = "";
        string NicknameEmpleado = "";
        string noCotizacion = "";
        string codigoProducto = "";
        int datoAEliminar = 0;

        // Variables

        // Funciones Varias

        void LoadForm()
        {
            CapaDatosFerreteria.ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            ClaseBitacoraFerreteria cbf = new ClaseBitacoraFerreteria();
            try
            {
                cargarProveedor();
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
                cargarProductos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR AL CARGAR DATOS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //Asignacion de datos a los labels
            labelEmpleado.Text = datosEmpleado;
            labelNoOrdenCompra.Text = nextCodigo("tbl_cotizacionencabezado");
            noCotizacion = nextCodigo("tbl_cotizacionencabezado");
            labelFechaEntrega.Text = DateTime.Now.ToShortDateString();
            labelNickname.Text = NicknameEmpleado;
        }


        public void cargarProductos()
        {
            CapaDatosFerreteria.ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            try
            {
                //Cargar datos de los productos
                cpd.cargarDatosACombobox2(comboBoxProducto, " CONCAT(id,',',Nombre) AS NombreC", "Nombre", "" +
                    "tbl_Producto WHERE tbl_Producto.status = 1 ");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR AL CARGAR DATOS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void cargarProveedor()
        {
            CapaDatosFerreteria.ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            try
            {
                //Carga del combobox de proveedores
                cpd.cargarDatosACombobox2(comboBoxProveedor,
                    "CONCAT(tbl_proveedor.id,',',tbl_persona.Nombre,',',tbl_organizacion.Nombre) as NombreC", "Nombre",
                    "tbl_organizacion, tbl_proveedor, tbl_persona " +
                    "WHERE " +
                    "tbl_proveedor.tbl_persona_id = tbl_persona.id " +
                    "AND tbl_proveedor.tbl_organizacion_id = tbl_organizacion.id " +
                    "AND tbl_proveedor.status = 1");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR AL CARGAR DATOS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public string nextCodigo(string table)
        {
            ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            try
            {
                return cpd.ExtraerCodigo2("AUTO_INCREMENT", "information_schema.TABLES", "TABLE_SCHEMA = 'bd_ferreteria' " +
                    "AND TABLE_NAME", "'" + table + "'");
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
            labelCodigoDetalle.Text = "ID: " + datos[0];
            labelNombreDetalle.Text = "NOMBRE: " + datos[1];
            labelMaterialDetalle.Text = "MAT.: " + datos[13];
            labelTipoDetalle.Text = "TIP.: " + datos[14];
            labelTamano.Text = "TAM.: " + datos[6];
            labelDiametro.Text = "DIAM.: " + datos[7];
            labelFuerza.Text = "FUER.: " + datos[8];
            labelVoltaje.Text = "VOL.: " + datos[2];
            labelCorriente.Text = "AMP.: " + datos[3];
        }

        public void Refrresh()
        {
            LoadForm();
            comboBoxProducto.SelectedIndex = 0;
            comboBoxProveedor.SelectedIndex = 0;
            dtg_ProductosDetalle.Rows.Clear();
        }

        // Funciones Varias

        private void comboBoxProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargardatos();
        }

        private void buttonAgregarProducto_Click(object sender, EventArgs e)
        {
            dtg_ProductosDetalle.Rows.Add(comboBoxProducto.Text, numericUpDownCant.Value.ToString());
        }

        private void buttonAgregarEncabezado_Click(object sender, EventArgs e)
        {
            panelProductos.Visible = true;
            panelDataGird.Visible = true;
            string[] codigoProveedor = comboBoxProveedor.Text.Split(',');
            string codigoEmpleado = "";
            DateTime fEntrega = Convert.ToDateTime(labelFechaEntrega.Text);

            string[] listaDetalle = new string[6];
            List<string> queryList = new List<string>();

            ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            try
            {
                // Insersion del encabezado
                string[] datosProveedor = comboBoxProveedor.Text.Split(',');
                codigoEmpleado = cpd.ExtraerCodigo("tbl_Empleado_DPI", "tbl_Usuario", "Nickname", labelNickname.Text);
                queryList.Add(cpd.DevolverSentencia_Insert("tbl_cotizacionencabezado", "0, " + fEntrega.ToString("yyyyMMdd") + ",  " + codigoProveedor[0] + "" +
                    " ," + codigoEmpleado + ", 1"));
                //Insersino del detalle
                foreach (DataGridViewRow row in dtg_ProductosDetalle.Rows)
                {
                    listaDetalle[0] = Convert.ToString(row.Cells[0].Value); //PRODUCTO
                    listaDetalle[1] = Convert.ToString(row.Cells[1].Value);//CANTIDAD
                    string[] datosProducto = listaDetalle[0].Split(',');
                    queryList.Add(cpd.DevolverSentencia_Insert("tbl_cotizaciondetalle", "0, " + datosProducto[0] + ", " + listaDetalle[1]
                    + ",  " + labelNoOrdenCompra.Text ));
                }
                /*string review = "";
                foreach (string var in queryList)
                {
                    review += var + "\n";
                }
                MessageBox.Show(review);*/
                cpd.EjectuarOperacionExterna(queryList, "", "", "Registro de cotizacion");
                MessageBox.Show("Se ha Realizado la cotizacion", "DATOS GUARDADOS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Refrresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR AL GUARDAR DATOS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonAddPais_Click(object sender, EventArgs e)
        {
            Proveedores form = new Proveedores();
            form.ShowDialog();
            comboBoxProveedor.Items.Clear();
            cargarProveedor();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Seguro de Eliminar los datos", "Eliminar Datos", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                dtg_ProductosDetalle.Rows.RemoveAt(datoAEliminar);
            }
        }

        private void dtg_ProductosDetalle_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                datoAEliminar = dtg_ProductosDetalle.CurrentRow.Index;
            }
            if (datoAEliminar < 0) button1.Enabled = false;
            else button1.Enabled = true;
        }

        private void numericUpDownCant_ValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(numericUpDownCant.Value) > 0) buttonAgregarProducto.Enabled = true;
            else buttonAgregarProducto.Enabled = false;
        }
    }
}
