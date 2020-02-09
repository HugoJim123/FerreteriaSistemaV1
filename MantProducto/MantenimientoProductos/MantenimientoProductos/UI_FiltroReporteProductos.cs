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
using CrystalDecisions.CrystalReports.Engine;
using ReporteadorFerreteria;
using ReporteadorFerreteria.REPORTS;

namespace MantenimientoProductos
{
    public partial class UI_FiltroReporteProductos : Form
    {
        public UI_FiltroReporteProductos()
        {
            InitializeComponent();
            LoadForm();
        }

        //Variables

        string filtro = "";
        DataSet ds;

        //Variables

        //Fucniones

        void LoadForm()
        {
            ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            try
            {
                CargarGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void CargarGrid()
        {
            ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            try
            {
                ds = cpd.LlenarGrid("tbl_producto.id as id, tbl_producto.Nombre as Nombre, " +
                   "tbl_producto.precioCompra as PrecioCompra, tbl_producto.precioVenta as PrecioVenta, " +
                   "tbl_material.DescripcionMaterial as Material, tbl_marca.DescripcionMarca as Marca, tbl_colorproducto.DescripcionColor as Color, " +
                   "tbl_productobodega.Cantidad as Cantidad, " +
                   "tbl_bodega.NombreBodega as Bodega",
                   "tbl_producto, tbl_bodega, tbl_productobodega, tbl_material, tbl_marca, tbl_colorproducto",
                   "tbl_producto.id = tbl_productobodega.tbl_producto_id " +
                   "AND tbl_material.id = tbl_producto.tbl_material_id " +
                   "AND tbl_marca.id = tbl_producto.tbl_marca_id " +
                   "AND tbl_colorproducto.id = tbl_producto.tbl_colorproducto_id " +
                   "AND tbl_productobodega.tbl_bodega_id = tbl_bodega.id " + filtro,
                   "Consulta Productos");
                dtg_Productos.DataSource = ds.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void ValueChanceBD()
        {
            filtro = "";
            if (textBoxNombre.Text != "") filtro += " AND tbl_Producto.Nombre LIKE '" + textBoxNombre.Text + "%' ";
            if (textBoxCodigo.Text != "") filtro += " AND tbl_Producto.id LIKE '" + textBoxCodigo.Text + "%' ";
            if (textBoxBodega.Text != "") filtro += " AND tbl_bodega.NombreBodega LIKE '" + textBoxBodega.Text + "%' ";
            if (textBoxMaterial.Text != "") filtro += " AND tbl_material.DescripcionMaterial LIKE '" + textBoxMaterial.Text + "%' ";
            if (numericUpDownPrecioCompraMinimo.Value > 0 && numericUpDownCompraMaximo.Value > 0) filtro += " AND tbl_Producto.precioCompra BETWEEN " + numericUpDownPrecioCompraMinimo.Value.ToString() +
                    " AND " + numericUpDownCompraMaximo.Value.ToString() + " ";
            else if (numericUpDownPrecioCompraMinimo.Value > 0) filtro += " AND tbl_Producto.precioCompra >= " + numericUpDownPrecioCompraMinimo.Value.ToString() + " ";
            else if (numericUpDownCompraMaximo.Value > 0) filtro += " AND tbl_Producto.precioCompra <= " + numericUpDownCompraMaximo.Value.ToString() + " ";
            if (numericUpDownVentaMinimo.Value > 0 && numericUpDownVentaMaximo.Value > 0) filtro += " AND tbl_Producto.precioVenta BETWEEN " + numericUpDownVentaMinimo.Value.ToString() +
                    " AND " + numericUpDownVentaMaximo.Value.ToString() + " ";
            else if (numericUpDownVentaMinimo.Value > 0) filtro += " AND tbl_Producto.precioVenta >= " + numericUpDownVentaMinimo.Value.ToString() + " ";
            else if (numericUpDownVentaMaximo.Value > 0) filtro += " AND tbl_Producto.precioVenta <= " + numericUpDownVentaMaximo.Value.ToString() + " ";
        }

        //Funciones

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonMaximized_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized) this.WindowState = FormWindowState.Normal;
            else this.WindowState = FormWindowState.Maximized;
        }

        private void textBoxCodigo_TextChanged(object sender, EventArgs e)
        {
            ValueChanceBD();
            CargarGrid();
        }

        private void buttonGenerarReporte_Click(object sender, EventArgs e)
        {
            string datosEmpleado = "";
            ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            ClaseBitacoraFerreteria cbf = new ClaseBitacoraFerreteria();
            try
            {
                datosEmpleado = cpd.ExtraerCodigo3("CONCAT(Apellidos,',',Nombre) AS ", "NombreC", "" +
                    " bd_ferreteria.tbl_Persona, bd_ferreteria.tbl_usuario, bd_ferreteria.tbl_Empleado",
                    "tbl_Empleado.status ", " 1 AND tbl_Persona.status = 1 " +
                    "AND tbl_Persona.id = tbl_Empleado.tbl_Persona_id " +
                    "AND tbl_usuario.tbl_empleado_DPI = tbl_empleado.DPI " +
                    "AND tbl_usuario.id = " + cbf.GetUserCodeForLog()
                    );
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            ///
            ReporteadorFerreteria.REPORTS.ReportProductos objRpt = new ReporteadorFerreteria.REPORTS.ReportProductos();
            ReporteadorFerreteria.DATASETS.DataSetP ds1 = new ReporteadorFerreteria.DATASETS.DataSetP();
            foreach (DataGridViewRow row in dtg_Productos.Rows)
            {
                ReporteadorFerreteria.DATASETS.DataSetP.tbl_productoRow rowProducto = ds1.tbl_producto.Newtbl_productoRow();
                rowProducto.id = Convert.ToString(row.Cells["id"].Value);
                rowProducto.Nombre = Convert.ToString(row.Cells["Nombre"].Value);
                rowProducto.PrecioCompra = Convert.ToString(row.Cells["PrecioCompra"].Value);
                rowProducto.PrecioVenta = Convert.ToString(row.Cells["PrecioVenta"].Value);
                rowProducto.Material = Convert.ToString(row.Cells["Material"].Value);
                rowProducto.Marca = Convert.ToString(row.Cells["Marca"].Value);
                rowProducto.Color = Convert.ToString(row.Cells["Color"].Value);
                rowProducto.Cantidad = Convert.ToString(row.Cells["Cantidad"].Value);
                rowProducto.Bodega = Convert.ToString(row.Cells["Bodega"].Value);

                ds1.tbl_producto.Addtbl_productoRow(rowProducto);
            }
            objRpt.SetDataSource(ds1);
            TextObject txtObject = (TextObject)objRpt.ReportDefinition.Sections["Section1"].ReportObjects["TextBy"];
            TextObject txtObjectDate = (TextObject)objRpt.ReportDefinition.Sections["Section1"].ReportObjects["TextDate"];
            txtObjectDate.Text = DateTime.Now.ToLongTimeString();
            txtObject.Text = datosEmpleado;
            UI_Visualizador form = new UI_Visualizador();
            form.crystalReportViewerGlobal.ReportSource = objRpt;
            form.ShowDialog();
        }
    }
}
