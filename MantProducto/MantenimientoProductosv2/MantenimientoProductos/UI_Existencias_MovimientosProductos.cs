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
    public partial class UI_Existencias_MovimientosProductos : Form
    {
        int opcionVista = 0;
        string codigo = "0";
        string valorMenor = "0";
        string valorMayor = "0";
        public UI_Existencias_MovimientosProductos(int opcionVista2, string codigo2, string valorMenor2, string valorMayor2)//Recibe como parametro una variable que indica que informacion cargar (0 = existencias, 1 = movimientos)
        {
            InitializeComponent();
            opcionVista = opcionVista2;
            codigo = codigo2;
            valorMenor = valorMenor2;
            valorMayor = valorMayor2;
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Nombra al label si es un producto el que se visualiza
        /// </summary>
        void NombrarLabel()
        {
            try
            {
                ClaseCapaDatosFerreteria cpd0 = new ClaseCapaDatosFerreteria();
                string nombre = cpd0.ExtraerCodigo("Nombre", "tbl_producto", "id", codigo.ToString());
                labelNombreProducto.Text = nombre;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error al Cargar Los Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void LoadForm()
        {
            if (opcionVista == 0)    // Existencias
            {
                try
                {
                    NombrarLabel();
                    ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
                    DataSet ds = cpd.LlenarGrid("" +
                        "DISTINCT " +
                        "tbl_Bodega.id as 'CODIGO BODEGA'," +
                        "tbl_Bodega.NombreBodega as BODEGA," +
                        "tbl_Producto.Nombre as 'NOMBRE PRODUCTO'," +
                        "tbl_productobodega.cantidad as CANTIDAD", "" +
                        "bd_ferreteria.tbl_producto," +
                        "bd_ferreteria.tbl_bodega, bd_ferreteria.tbl_productobodega", "" +
                        "tbl_producto.id =" + codigo + " " +
                        "AND " +
                        "tbl_productobodega.tbl_producto_id = tbl_producto.id " +
                        "AND " +
                        "tbl_bodega.id = tbl_productobodega.tbl_bodega_id " +
                        "AND " +
                        "tbl_producto.status = 1 ");
                    dataGridViewMovimientosDeProducto.DataSource = ds.Tables[0];
                    ///
                    const int columna = 3;
                    int suma = 0;
                    foreach (DataGridViewRow row in dataGridViewMovimientosDeProducto.Rows)
                    {
                        suma += (int)row.Cells[columna].Value;
                    }
                    labelTotal.Text = suma.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Error al Cargar Los Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (opcionVista == 1) // Movimientos
            {
                NombrarLabel();
                panelFooter.Visible = false;
            }
            else if (opcionVista == 2)
            {
                try
                {
                    labelNombreProducto.Text = "PRONTOS A ACABAR";
                    panelFooter.Visible = false;
                    ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
                    DataSet ds = cpd.LlenarGrid("" +
                        "tbl_producto.id as CODIDGO, tbl_producto.Nombre as NOMBRE, SUM(tbl_productobodega.Cantidad) as CANTIDAD ",
                        "bd_ferreteria.tbl_productobodega, bd_ferreteria.tbl_producto ",
                        "tbl_producto.id = tbl_productobodega.tbl_producto_id AND " +
                        "tbl_producto.status = 1 " +
                        "group by tbl_producto.Nombre " +
                        "HAVING CANTIDAD <= " + valorMenor +
                        "");
                    dataGridViewMovimientosDeProducto.DataSource = ds.Tables[0];
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Error al Cargar Los Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (opcionVista == 3)
            {
                try
                {
                    labelNombreProducto.Text = "CON MAS EXISTENCIAS";
                    panelFooter.Visible = false;
                    ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
                    DataSet ds = cpd.LlenarGrid("" +
                        "tbl_producto.id as CODIDGO, tbl_producto.Nombre as NOMBRE, SUM(tbl_productobodega.Cantidad) as CANTIDAD ",
                        "bd_ferreteria.tbl_productobodega, bd_ferreteria.tbl_producto ",
                        "tbl_producto.id = tbl_productobodega.tbl_producto_id AND " +
                        "tbl_producto.status = 1 " +
                        "HAVING CANTIDAD >= " + valorMayor +
                        "");
                    dataGridViewMovimientosDeProducto.DataSource = ds.Tables[0];
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Error al Cargar Los Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (opcionVista == 4)
            {
                try
                {
                    labelNombreProducto.Text = "EN STOCK";
                    panelFooter.Visible = false;
                    ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
                    DataSet ds = cpd.LlenarGrid("" +
                        "tbl_producto.id as CODIDGO, tbl_producto.Nombre as NOMBRE, tbl_productobodega.Cantidad as CANTIDAD ",
                        "bd_ferreteria.tbl_productobodega, bd_ferreteria.tbl_producto, bd_ferreteria.tbl_bodega ",
                        "tbl_producto.id = tbl_productobodega.tbl_producto_id AND " +
                        "tbl_productobodega.tbl_bodega_id = tbl_bodega.id AND " +
                        "tbl_bodega.NombreBodega = 'Stock' AND " +
                        "tbl_producto.status = 1 " +
                        "");
                    dataGridViewMovimientosDeProducto.DataSource = ds.Tables[0];
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Error al Cargar Los Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void UI_Existencias_MovimientosProductos_Load(object sender, EventArgs e)
        {
            LoadForm();
        }

        private void buttonAgregarExistencias_Click(object sender, EventArgs e)
        {
            UI_AsignacionDeProductoABodega form = new UI_AsignacionDeProductoABodega(codigo, labelNombreProducto.Text);
            form.ShowDialog();
            LoadForm();
        }
    }
}
