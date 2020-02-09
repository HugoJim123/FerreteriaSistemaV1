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
    public partial class UI_MovimientosInventario : Form
    {
        public UI_MovimientosInventario()
        {
            InitializeComponent();
            LoadForm();
            comboBoxBusqueda.SelectedIndex = 0;
        }

        //Variables

        string filtro = "";

        //Variables

        //Funciones

        void LoadForm()
        {
            ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            // Llenar datagridview Principal
            try
            {
                DataSet ds = cpd.LlenarGrid("" +
                    "tbl_movimientoinventario.id as 'CODIGO', "+
                    "tbl_tipomovimiento.DescripcionMovimiento as 'MOVIMIENTO', " +
                    "tbl_producto.Nombre as 'PRODUCTO', " +
                    "tbl_movimientoinventario.FechaMovimiento as 'FECHA', " +
                    "tbl_movimientoinventario.CantidadMovimiento as 'CANTIDAD', " +
                    "tbl_movimientoinventario.PrecioMovimiento as 'PRECIO', " +
                    "tbl_movimientoinventario.Subtotal as 'SUBTOTAL' ",
                    "tbl_movimientoinventario, tbl_tipomovimiento, tbl_producto",
                    "tbl_movimientoinventario.tbl_TipoMovimiento_id = tbl_TipoMovimiento.id "+
                    "AND tbl_movimientoinventario.tbl_Producto_id = tbl_Producto.id " + filtro,
                    "Vista datos de productos");
                dtg_Productos.DataSource = ds.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error al Cargar los Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Llenar Ingresos
            try
            {
                string query = "select " +
                                "tbl_movimientoinventario.id " +
                                "from " +
                                "tbl_movimientoinventario, tbl_TipoMovimiento " +
                                "where " +
                                "tbl_movimientoinventario.tbl_TipoMovimiento_id = tbl_TipoMovimiento.id " +
                                "AND tbl_TipoMovimiento.DescripcionMovimiento = 'Compras';";
                labelIngresosQ.Text = cpd.ExtraerCodigo3("sum(subtotal) as ", "subtotal", "tbl_movimientoinventario, tbl_TipoMovimiento",
                                "tbl_movimientoinventario.tbl_TipoMovimiento_id = tbl_TipoMovimiento.id " +
                                "AND ( tbl_TipoMovimiento.DescripcionMovimiento",
                                "'Compras' " +
                                "OR tbl_TipoMovimiento.DescripcionMovimiento = 'Devoluciones') ;");
                labelIngresosC.Text = cpd.RetornoMenosExistencias(query).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error al Cargar los Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Llenar Egresos
            try
            {
                string query = "select " +
                                "tbl_movimientoinventario.id " +
                                "from " +
                                "tbl_movimientoinventario, tbl_TipoMovimiento " +
                                "where " +
                                "tbl_movimientoinventario.tbl_TipoMovimiento_id = tbl_TipoMovimiento.id " +
                                "AND ( tbl_TipoMovimiento.DescripcionMovimiento = 'Ventas'" +
                                "OR tbl_TipoMovimiento.DescripcionMovimiento = 'Devoluciones') ;";
                labelEgresosQ.Text = cpd.ExtraerCodigo3("sum(subtotal) as ", "subtotal", "tbl_movimientoinventario, tbl_TipoMovimiento",
                                "tbl_movimientoinventario.tbl_TipoMovimiento_id = tbl_TipoMovimiento.id " +
                                "AND tbl_TipoMovimiento.DescripcionMovimiento",
                                "'Ventas' ;" );
                labelEgresosC.Text = cpd.RetornoMenosExistencias(query).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error al Cargar los Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        //Funciones

        private void txtBusqueda_TextChanged(object sender, EventArgs e)
        {
            string tipoBusqueda = comboBoxBusqueda.Text;
            switch (tipoBusqueda)
            {
                case "Código":
                    filtro = " AND tbl_movimientoinventario.Id LIKE '" + txtBusqueda.Text + "%'";
                    break;
                case "Movimiento":
                    filtro = " AND tbl_tipomovimiento.DescripcionMovimiento LIKE '" + txtBusqueda.Text + "%'";
                    break;
                case "Producto":
                    filtro = " AND tbl_producto.Nombre LIKE '" + txtBusqueda.Text + "%'";
                    break;
                default:
                    filtro = "";
                    break;
            }
            LoadForm();
        }

        private void panelInfo1_Click(object sender, EventArgs e)
        {
            UI_Existencias_MovimientosProductos form = new UI_Existencias_MovimientosProductos(5,
                " tbl_producto.id AND tbl_TipoMovimiento.DescripcionMovimiento = "+
                                "'Ventas'",
                "0", "0");
            form.ShowDialog();
        }

        private void panelInfo2_Click(object sender, EventArgs e)
        {
            UI_Existencias_MovimientosProductos form = new UI_Existencias_MovimientosProductos(5,
                " tbl_producto.id AND ( tbl_TipoMovimiento.DescripcionMovimiento = " +
                                "'Compras' " +
                                "OR tbl_TipoMovimiento.DescripcionMovimiento = 'Devoluciones')",
                "0", "0");
            form.ShowDialog();
        }
    }
}
