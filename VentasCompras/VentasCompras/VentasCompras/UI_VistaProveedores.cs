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
    public partial class UI_VistaProveedores : Form
    {
        public UI_VistaProveedores()
        {
            InitializeComponent();
            LoadForm();
            comboBoxBusquedaPor.SelectedIndex = 0;
        }

        //Variables

        string filtro = "";
        string codigoACambiar = "";

        //Variables

        //Funciones varias

        void LoadForm()
        {
            ///Carga de grid Principal
            ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            try
            {
                DataSet ds = cpd.LlenarGrid("" +
                    "tbl_proveedor.id as CODIGO, "+
                    "tbl_persona.nombre as NOMBRE, " +
                    "tbl_persona.apellidos as APELLIDO, " +
                    "tbl_organizacion.nombre as ORGANIZACION, " +
                    "tbl_organizacion.direccion as DIRECCION " 
                    ,
                    "tbl_proveedor, tbl_persona, tbl_organizacion " 
                    ,
                    "tbl_persona.id = tbl_proveedor.tbl_persona_id " +
                    "AND tbl_organizacion.id = tbl_proveedor.tbl_organizacion_id " +
                    "AND tbl_proveedor.status = 1 " +
                    filtro,
                    "Visualizacion de Proveedores");
                DataGridViewProveedores.DataSource = ds.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error al cargar datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void CambiarBusqueda()
        {
            if (txtBusqueda.Text == "")
            {
                filtro = "";
            }
            else
            {
                if (comboBoxBusquedaPor.Text == "Código")
                {
                    filtro = "AND tbl_proveedor.id LIKE '" + txtBusqueda.Text + "%'";
                }
                if (comboBoxBusquedaPor.Text == "Nombre")
                {
                    filtro = "AND tbl_persona.nombre LIKE '" + txtBusqueda.Text + "%'";
                }
                if (comboBoxBusquedaPor.Text == "Organización")
                {
                    filtro = "AND tbl_organizacion.Nombre LIKE '" + txtBusqueda.Text + "%'";
                }
            }
            LoadForm();
        }

        void moverPanelSeguimientoSubmenu(Button btn, Panel pnl)
        {
            pnl.Location = new Point(btn.Location.X, btn.Location.Y + 45);
        }

        //Funciones varias

        private void txtBusqueda_TextChanged(object sender, EventArgs e)
        {
            CambiarBusqueda();
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            LoadForm();
        }

        private void DataGridViewProveedores_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                codigoACambiar = DataGridViewProveedores.Rows[e.RowIndex].Cells[0].Value.ToString();
            }
            if (codigoACambiar == "") buttonDelete.Enabled = false;
            else buttonDelete.Enabled = true;
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
                DialogResult dialogResult = MessageBox.Show("Seguro de Eliminar los datos", "Eliminar Datos", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    cpd.ModificarDatos("tbl_proveedor", "status = 0", "id", codigoACambiar, "Eliminacion de Proveedores");
                    MessageBox.Show("Datos eliminados correctamente", "Datos Eliminados", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    LoadForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            Proveedores form = new Proveedores();
            form.ShowDialog();
            LoadForm();
        }
    }
}
