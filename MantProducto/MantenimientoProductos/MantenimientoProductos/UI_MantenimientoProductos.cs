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
using CrystalDecisions.Shared;

namespace MantenimientoProductos
{
    public partial class UI_MantenimientoProductos : Form
    {
        Panel frm;
        Button butn;
        Panel Seguimiento;
        string codigoACambiar = "";
        string tipoBusqueda = "";
        string filtro = "";
        string valorMenor;
        string valorMayor;

        public UI_MantenimientoProductos(Panel frmparent, Button btn, Panel pnlSeguimiento, string valorMenor2, string valorMayor2)
        {
            InitializeComponent();
            frm = frmparent;
            butn = btn;
            Seguimiento = pnlSeguimiento;
            comboBox1.SelectedIndex = 0;
            valorMenor = valorMenor2;
            valorMayor = valorMayor2;
        }

        /// <summary>
        /// Mueve el panel de seguimiento segun lo que este seleccionado
        /// </summary>
        /// <param name="btn">nombre del boton</param>
        /// <param name="pnl">nombre del panel</param>
        void moverPanelSeguimientoSubmenu(Button btn, Panel pnl)
        {
            pnl.Location = new Point(btn.Location.X, btn.Location.Y + 45);
        }

        private void buttonManteinence_Click(object sender, EventArgs e)
        {
            int tipoMov = 0;
            moverPanelSeguimientoSubmenu(butn, Seguimiento);
            switch((sender as Button).Name)
            {
                case "buttonAdd":
                    tipoMov = 0;
                    break;
                case "buttonEdit":
                    tipoMov = 1;
                    break;
                default:
                    tipoMov = 0;
                    break;
            }
            if(codigoACambiar == "" && tipoMov == 1)
            {
                MessageBox.Show("Seleccione el dato a modificar", "Error al cargar datos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                UI_IngresoModifiacionProductosV2 form = new UI_IngresoModifiacionProductosV2(tipoMov, codigoACambiar);
                form.TopLevel = false;
                form.AutoScroll = true;
                frm.Controls.Clear();
                frm.Controls.Add(form);
                form.Show();
                form.Dock = System.Windows.Forms.DockStyle.Fill;
            }
        }

        private void dtg_Productos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string codigo = "";
            if (e.RowIndex >= 0)
            {
                codigo = dtg_Productos.Rows[e.RowIndex].Cells[0].Value.ToString();
                UI_DetalleProductos form = new UI_DetalleProductos(codigo);
                form.ShowDialog();
            }
        }

        private void dtg_Productos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                codigoACambiar = dtg_Productos.Rows[e.RowIndex].Cells[0].Value.ToString();
            }
        }

        void refresh()
        {
            ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            try
            {
                DataSet ds = cpd.LlenarGrid("" +
                    "tbl_producto.id as CÓDIGO, tbl_producto.nombre as NOMBRE, tbl_producto.voltaje as 'VOL.', tbl_producto.amperaje as 'AMP.', " +
                    "tbl_producto.capacitancia as 'CAP.', tbl_producto.resistencia as 'RES.', tbl_producto.tamaño as TAMAÑO, tbl_producto.diametro as DIAMETRO, " +
                    "tbl_producto.fuerza as FUERZA, tbl_producto.temperatura as 'TEMP.', tbl_producto.rpm as RPM, tbl_producto.hz as Hz, tbl_producto.precioVenta as 'PRECIO VENTA', " +
                    "tbl_producto.precioCompra as 'PRECIO COMPRA', tbl_Material.DescripcionMaterial as MATERIAL, tbl_Tipo.DescripcionTipo as TIPO, " +
                    "tbl_Pais.DescripcionPais as PAIS, tbl_Marca.DescripcionMarca as MARCA, tbl_ColorProducto.DescripcionColor as COLOR, " +
                    "tbl_producto.observaciones as OBSERVACIONES ", "" +                                                // TABLAS
                    "bd_ferreteria.tbl_producto, bd_ferreteria.tbl_Material, bd_ferreteria.tbl_Tipo, " +
                    "bd_ferreteria.tbl_Pais, bd_ferreteria.tbl_Marca, bd_ferreteria.tbl_ColorProducto ", "" +           // WHERE
                    "tbl_producto.tbl_Material_id = tbl_Material.id AND " +
                    "tbl_producto.tbl_Tipo_id = tbl_Tipo.id AND " +
                    "tbl_producto.tbl_Pais_id = tbl_Pais.id AND " +
                    "tbl_producto.tbl_Marca_id = tbl_Marca.id AND " +
                    "tbl_producto.tbl_ColorProducto_id = tbl_ColorProducto.id AND " +
                    "tbl_producto.status = 1 " + filtro, "Vista datos de productos");
                dtg_Productos.DataSource = ds.Tables[0];
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error al Cargar los Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            try
            {
                labelInStock.Text = cpd.ExtraerCodigo2("COUNT(tbl_ProductoBodega.Cantidad)", "bd_Ferreteria.tbl_Producto, bd_Ferreteria.tbl_ProductoBodega, bd_Ferreteria.tbl_Bodega",
                    "tbl_ProductoBodega.tbl_Producto_id = tbl_Producto.id " +
                    "AND tbl_ProductoBodega.tbl_Bodega_id = tbl_Bodega.id " +
                    "AND tbl_ProductoBodega.Cantidad > 0 " +
                    "AND tbl_Producto.status = 1 AND tbl_Bodega.status = 1 " +
                    "AND tbl_Bodega.NombreBodega ", 
                    "'Stock'");
            }catch(Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error al Cargar los Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            try
            {
                string comand = "SELECT tbl_producto.id, tbl_producto.Nombre, SUM(tbl_productobodega.Cantidad) as sum_cantidad " +
                    "FROM bd_ferreteria.tbl_productobodega, bd_ferreteria.tbl_producto " +
                    "WHERE tbl_producto.id = tbl_productobodega.tbl_producto_id " +
                    "AND tbl_producto.status = 1 " +
                    "group by tbl_producto.Nombre " +
                    "HAVING sum_cantidad <= " + valorMenor + " ";
                labelProntosAAcabar.Text = (cpd.RetornoMenosExistencias(comand).ToString());
            }catch(Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error al Cargar los Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            try
            {
                string comand = "SELECT tbl_producto.id, tbl_producto.Nombre, SUM(tbl_productobodega.Cantidad) as sum_cantidad " +
                    "FROM bd_ferreteria.tbl_productobodega, bd_ferreteria.tbl_producto " +
                    "WHERE tbl_producto.id = tbl_productobodega.tbl_producto_id " +
                    "AND tbl_producto.status = 1 " +
                    "group by tbl_producto.Nombre " +
                    "HAVING sum_cantidad >= " + valorMayor + " ";
                labelConMasExistencias.Text = (cpd.RetornoMenosExistencias(comand).ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error al Cargar los Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void UI_MantenimientoProductos_Load(object sender, EventArgs e)
        {
            refresh();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
                DialogResult dialogResult = MessageBox.Show("Seguro de Eliminar los datos", "Eliminar Datos", MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    cpd.ModificarDatos("tbl_producto", "status = 0", "id", codigoACambiar, "Eliminacion de productos");
                    MessageBox.Show("Datos eliminados correctamente", "Datos Eliminados", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    refresh();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            refresh();
        }

        private void txtBusqueda_TextChanged(object sender, EventArgs e)
        {
            switch (tipoBusqueda)
            {
                case "Código":
                    filtro = " AND tbl_Producto.Id LIKE '" + txtBusqueda.Text + "%'"; 
                    break;
                case "Nombre":
                    filtro = " AND tbl_Producto.Nombre LIKE '" + txtBusqueda.Text + "%'";
                    break;
                case "Material":
                    filtro = " AND tbl_Material.DescripcionMaterial LIKE '" + txtBusqueda.Text + "%'";
                    break;
                case "Tipo":
                    filtro = " AND tbl_Tipo.DescripcionTipo LIKE '" + txtBusqueda.Text + "%'";
                    break;
                case "Marca":
                    filtro = " AND tbl_Marca.DescripcionMarca LIKE '" + txtBusqueda.Text + "%'";
                    break;
                case "Pais":
                    filtro = " AND tbl_Pais.DescripcionPais LIKE '" + txtBusqueda.Text + "%'";
                    break;
                case "Color":
                    filtro = " AND tbl_ColorProducto.DescripcionColor LIKE '" + txtBusqueda.Text + "%'";
                    break;
                default:
                    filtro = "";
                    break;
            }
            refresh();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            tipoBusqueda = comboBox1.Text;
        }

        private void panelInfo2_DoubleClick(object sender, EventArgs e)
        {
            UI_Existencias_MovimientosProductos form = new UI_Existencias_MovimientosProductos(3, "", valorMenor, valorMayor);
            form.ShowDialog();
        }

        private void panelInfo3_Click(object sender, EventArgs e)
        {
            UI_Existencias_MovimientosProductos form = new UI_Existencias_MovimientosProductos(2, "", valorMenor, valorMayor);
            form.ShowDialog();
        }

        private void panelInfo5_Click(object sender, EventArgs e)
        {
            UI_Existencias_MovimientosProductos form = new UI_Existencias_MovimientosProductos(4, "", valorMenor, valorMayor);
            form.ShowDialog();
        }

        private void buttonReport_Click(object sender, EventArgs e)
        {
            UI_FiltroReporteProductos form = new UI_FiltroReporteProductos();
            form.ShowDialog();
            /*ReporteProductos objRpt = new ReporteProductos();
            ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            DataSetProductos ds = new DataSetProductos();
            DataSet ds1 = new DataSet();
            ds1 = cpd.LlenarReport("SELECT tbl_producto.id as ID, tbl_producto.nombre as NOMBRE, tbl_producto.voltaje as Voltaje, tbl_producto.amperaje as Amperaje, "+
                "tbl_producto.capacitancia as Capacitancia, tbl_producto.resistencia as Resistencia, tbl_producto.tamaño as TAMAÑO, tbl_producto.diametro as DIAMETRO, tbl_producto.precioVenta as PRECIOVENTA, " +
                "tbl_producto.precioCompra as PRECIOCOMPRA, tbl_Material.DescripcionMaterial as MATERIAL, tbl_Tipo.DescripcionTipo as TIPO, " +
                "tbl_Pais.DescripcionPais as PAIS, tbl_Marca.DescripcionMarca as MARCA, tbl_ColorProducto.DescripcionColor as COLOR " +
                "FROM bd_ferreteria.tbl_producto, bd_ferreteria.tbl_Material, bd_ferreteria.tbl_Tipo, " +
                "bd_ferreteria.tbl_Pais, bd_ferreteria.tbl_Marca, bd_ferreteria.tbl_ColorProducto  " +
                "WHERE " +
                "tbl_producto.tbl_Material_id = tbl_Material.id AND " +
                "tbl_producto.tbl_Tipo_id = tbl_Tipo.id AND " +
                "tbl_producto.tbl_Pais_id = tbl_Pais.id AND " +
                "tbl_producto.tbl_Marca_id = tbl_Marca.id AND " +
                "tbl_producto.tbl_ColorProducto_id = tbl_ColorProducto.id AND " +
                "tbl_producto.status = 1; ", "Reporte de Productos"); 
            objRpt.SetDataSource(ds1);
            TextObject txtObject = (TextObject)objRpt.ReportDefinition.Sections["Section1"].ReportObjects["TextBy"];
            txtObject.Text = "Hugo Jimenez";
            ReportViewProductos form = new ReportViewProductos(this);
            form.crystalReportViewerProductos.ReportSource = objRpt;
            form.ShowDialog();*/

        }
    }
}
