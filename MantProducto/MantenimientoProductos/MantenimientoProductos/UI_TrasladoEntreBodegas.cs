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
    public partial class UI_TrasladoEntreBodegas : Form
    {
        public UI_TrasladoEntreBodegas()
        {
            InitializeComponent();
            LoadForm();
        }


        //Variables

        string codBodega1 = "";
        string codBodega2 = "";
        string codProducto = "";

        //Variables

        //Funciones

        void LoadForm()
        {
            comboBoxBodega1.Items.Clear();
            comboBoxBodega2.Items.Clear();
            ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            try
            {
                cpd.cargarDatosACombobox(comboBoxBodega1, "NombreBodega", "tbl_Bodega");
                cpd.cargarDatosACombobox(comboBoxBodega2, "NombreBodega", "tbl_Bodega");
                if (comboBoxBodega1.Name == "comboBoxBodega1") codBodega1 = cpd.ExtraerCodigo("id", "tbl_Bodega", "NombreBodega", comboBoxBodega1.Text);
                if (comboBoxBodega2.Name == "comboBoxBodega2") codBodega2 = cpd.ExtraerCodigo("id", "tbl_Bodega", "NombreBodega", comboBoxBodega2.Text);
                codProducto = cpd.ExtraerCodigo("tbl_Producto_id", "tbl_productobodega", "tbl_Bodega_id", codBodega1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error al Cargar los Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            CargarGridDetalleBodegas(dtg_Productos1, comboBoxBodega1);
            CargarGridDetalleBodegas(dtg_Productos2, comboBoxBodega2);
        }

        void CargarGridDetalleBodegas(DataGridView dtg, ComboBox cbm)
        {
            ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            // Llenar Datagrid
            try
            {
                string idBodega = cpd.ExtraerCodigo("id", "tbl_Bodega", "NombreBodega", cbm.Text);
                DataSet ds = cpd.LlenarGrid("" +
                    "tbl_producto.id as 'CODIGO', " +
                    "tbl_producto.Nombre as 'NOMBRE', " +
                    "tbl_producto.tbl_Material_id as 'MATERIAL', " +
                    "tbl_productobodega.Cantidad as 'CANTIDAD' ",
                    "tbl_producto, tbl_productobodega ",
                    "tbl_productobodega.tbl_Producto_id = tbl_Producto.id " +
                    "and tbl_productobodega.tbl_Bodega_id = '" + idBodega + "' " +
                    "and tbl_Producto.status = 1",
                    "Visualizacion de productos en bodega");
                dtg.DataSource = ds.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error al Cargar los Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Funciones

        private void buttonAsignar_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(codBodega1 + "+" + codBodega2 + "+" + codProducto);
            UI_AsignarCantidad form = new UI_AsignarCantidad(codBodega1, codBodega2, codProducto);
            form.ShowDialog();
            CargarGridDetalleBodegas(dtg_Productos1, comboBoxBodega1);
            CargarGridDetalleBodegas(dtg_Productos2, comboBoxBodega2);
        }

        private void comboBoxBodega1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            try
            {
                if ((sender as ComboBox).Name == "comboBoxBodega1") codBodega1 = cpd.ExtraerCodigo("id", "tbl_Bodega", "NombreBodega", (sender as ComboBox).Text);
                if ((sender as ComboBox).Name == "comboBoxBodega2") codBodega2 = cpd.ExtraerCodigo("id", "tbl_Bodega", "NombreBodega", (sender as ComboBox).Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error al Cargar los Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            CargarGridDetalleBodegas(dtg_Productos1, comboBoxBodega1);
            CargarGridDetalleBodegas(dtg_Productos2, comboBoxBodega2);
            if (codBodega1 == codBodega2) {
                buttonAsignar.Enabled = false;
                buttonDesasignar.Enabled = false;
            }
            else
            {
                buttonAsignar.Enabled = true;
                buttonDesasignar.Enabled = true;
            }
        }

        private void dtg_Productos1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                codProducto = (sender as DataGridView).Rows[e.RowIndex].Cells[0].Value.ToString();
            }
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonDesasignar_Click(object sender, EventArgs e)
        {
            UI_AsignarCantidad form = new UI_AsignarCantidad(codBodega2, codBodega1, codProducto);
            form.ShowDialog();
            CargarGridDetalleBodegas(dtg_Productos1, comboBoxBodega1);
            CargarGridDetalleBodegas(dtg_Productos2, comboBoxBodega2);
        }

        private void buttonDesasignar_EnabledChanged(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn.Enabled == true) btn.BackColor = Color.FromArgb(49, 156, 202);
            else btn.BackColor = Color.Gray;
        }
    }
}
