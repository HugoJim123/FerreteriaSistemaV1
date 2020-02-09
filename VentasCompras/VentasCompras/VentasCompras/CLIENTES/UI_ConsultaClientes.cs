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
    public partial class UI_ConsultaClientes : Form
    {
        public UI_ConsultaClientes(Panel frmparent, Panel seg, Button bt)
        {
            InitializeComponent();
            frm = frmparent;
            Seguimiento = seg;
            btn = bt;
            LoadForm();
            comboBoxBusquedaPor.SelectedIndex = 0;
        }

        //Variables

        string filtro = "";
        string codigoACambiar = "";
        Panel frm;
        Panel Seguimiento;
        Button btn;

        //Variables

        //Funciones varias

        void LoadForm()
        {
            ///Carga de grid Principal
            ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            try
            {
                DataSet ds = cpd.LlenarGrid("" +
                    "tbl_cliente.NIT as 'NIT', " +
                    "tbl_persona.Nombre as 'NOMBRE', " +
                    "tbl_persona.Apellidos as 'APELLIDOS', " +
                    "tbl_organizacion.Nombre as 'ORGANIZACION', " +
                    "tbl_organizacion.Direccion as 'DIRECCION', " +
                    "tbl_cliente.Transporte as 'TRANSPORTE', " +
                    "tbl_telefono.NumeroTelefono as 'TELEFONO', " +
                    "tbl_correo.Correo as 'CORREO'"
                    ,
                    "tbl_persona, tbl_organizacion, tbl_cliente,tbl_telefono, tbl_correo"
                    ,
                    "tbl_persona.id = tbl_telefono.tbl_persona_id "+
                    "AND tbl_persona.id = tbl_correo.tbl_persona_id " +
                    "AND tbl_organizacion.id = tbl_cliente.tbl_organizacion_id " +
                    "AND tbl_cliente.tbl_Persona_id = tbl_Persona.id " +
                    "AND tbl_cliente.status = 1 " +
                    filtro,
                    "Visualizacion de Proveedores");
                DataGridViewClientes.DataSource = ds.Tables[0];
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
                if (comboBoxBusquedaPor.Text == "NIT")
                {
                    filtro = "AND tbl_cliente.NIT LIKE '" + txtBusqueda.Text + "%'";
                }
                if (comboBoxBusquedaPor.Text == "Nombre de Cliente")
                {
                    filtro = "AND tbl_persona.nombre LIKE '" + txtBusqueda.Text + "%'";
                }
                if (comboBoxBusquedaPor.Text == "Organización")
                {
                    filtro = "AND tbl_organizacion.Nombre LIKE '" + txtBusqueda.Text + "%'";
                }
                if (comboBoxBusquedaPor.Text == "Transporte")
                {
                    filtro = "AND tbl_cliente.Transporte LIKE '" + txtBusqueda.Text + "%'";
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
                codigoACambiar = DataGridViewClientes.Rows[e.RowIndex].Cells[0].Value.ToString();
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
                    cpd.ModificarDatos("tbl_cliente", "status = 0", "NIT", codigoACambiar, "Eliminacion de Cliente");
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
            moverPanelSeguimientoSubmenu(btn, Seguimiento);
            UI_IngresoClientes form = new UI_IngresoClientes();
            form.TopLevel = false;
            form.AutoScroll = true;
            frm.Controls.Clear();
            frm.Controls.Add(form);
            form.Show();
            form.Dock = System.Windows.Forms.DockStyle.Fill;

        }

        private void DataGridViewClientes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                codigoACambiar = DataGridViewClientes.Rows[e.RowIndex].Cells[0].Value.ToString();
                string nit = DataGridViewClientes.Rows[e.RowIndex].Cells[0].Value.ToString();
                string nombre = DataGridViewClientes.Rows[e.RowIndex].Cells[1].Value.ToString() + ", " + DataGridViewClientes.Rows[e.RowIndex].Cells[2].Value.ToString();
                string organizacion = DataGridViewClientes.Rows[e.RowIndex].Cells[3].Value.ToString();
                string direccion = DataGridViewClientes.Rows[e.RowIndex].Cells[4].Value.ToString();
                string transporte = DataGridViewClientes.Rows[e.RowIndex].Cells[5].Value.ToString();
                string telefono = DataGridViewClientes.Rows[e.RowIndex].Cells[6].Value.ToString();

                UI_DetalleClientes udc = new UI_DetalleClientes(nit, nombre, organizacion, direccion, telefono);
                udc.ShowDialog();
            }
        }
    }
}
