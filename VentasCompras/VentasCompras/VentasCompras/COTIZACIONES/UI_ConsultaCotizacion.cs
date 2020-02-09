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
    public partial class UI_ConsultaCotizacion : Form
    {
        public UI_ConsultaCotizacion(string valorMenor1, Panel frmparent, Button btn, Panel pnlSeguimiento)
        {
            InitializeComponent();
            LoadForm();
            comboBoxBusquedaPor.SelectedIndex = 0;
            butn = btn;
            Seguimiento = pnlSeguimiento;
            frm = frmparent;
        }

        //Variables

        string filtro = "";
        string codigoACambiar = "";
        Panel frm;
        Button butn;
        Panel Seguimiento;

        //Variables

        ///Funciones varias

        //Funcion que carga grid dependiendo del filtro
        void CargarGrid()
        {
            ///Carga de grid Principal
            ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            try
            {
                DataSet ds = cpd.LlenarGrid("" +
                    "tbl_cotizacionencabezado.id as 'CÓDIGO', " +
                    "CONCAT(tbl_proveedor.id, ',', tbl_persona.Nombre, ',', tbl_organizacion.Nombre) as 'PROVEEDOR', "+
                    "tbl_cotizacionencabezado.FechaEmision as 'FECHA DE EMISIÓN'",
                    "tbl_cotizacionencabezado, tbl_organizacion, tbl_proveedor, tbl_persona ",
                    "tbl_proveedor.tbl_persona_id = tbl_persona.id " +
                    "AND tbl_cotizacionencabezado.tbl_proveedor_id = tbl_proveedor.id " +
                    "AND tbl_proveedor.tbl_organizacion_id = tbl_organizacion.id " +
                    "AND tbl_cotizacionencabezado.status = 1 " +
                    filtro,
                    "Visualizacion de Cotizaciones Encabezado");
                dtg_Compras.DataSource = ds.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error al cargar datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        ///Funciona que contiene todas las acciones para cargar el form
        void LoadForm()
        {
            CargarGrid();
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
                    filtro = "AND tbl_cotizacionencabezado.id LIKE '" + txtBusqueda.Text + "%'";
                }
                if (comboBoxBusquedaPor.Text == "Proveedor Persona")
                {
                    filtro = "AND tbl_persona.Nombre LIKE '" + txtBusqueda.Text + "%'";
                }
                if (comboBoxBusquedaPor.Text == "Proveedor Organizacion")
                {
                    filtro = "AND tbl_organizacion.Nombre LIKE '" + txtBusqueda.Text + "%'";
                }
            }
            CargarGrid();
        }

        void moverPanelSeguimientoSubmenu(Button btn, Panel pnl)
        {
            pnl.Location = new Point(btn.Location.X, btn.Location.Y + 45);
        }

        ///Fucniones varias    

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            LoadForm();
        }

        private void txtBusqueda_TextChanged(object sender, EventArgs e)
        {
            CambiarBusqueda();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {

            try
            {
                ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
                DialogResult dialogResult = MessageBox.Show("Seguro de Eliminar los datos", "Eliminar Datos", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    cpd.ModificarDatos("tbl_cotizacionencabezado", "status = 0", "id", codigoACambiar, "Eliminacion de Cotizacion");
                    MessageBox.Show("Datos eliminados correctamente", "Datos Eliminados", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    LoadForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void dtg_Compras_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                codigoACambiar = dtg_Compras.Rows[e.RowIndex].Cells[0].Value.ToString();
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            moverPanelSeguimientoSubmenu(butn, Seguimiento);
            UI_IngresoDeCotizaciones form = new UI_IngresoDeCotizaciones();
            form.TopLevel = false;
            form.AutoScroll = true;
            frm.Controls.Clear();
            frm.Controls.Add(form);
            form.Show();
            form.Dock = System.Windows.Forms.DockStyle.Fill;
        }

        private void dtg_Compras_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string cdEnc = dtg_Compras.Rows[e.RowIndex].Cells[0].Value.ToString();
                UI_DetalleCotizaciones form = new UI_DetalleCotizaciones(cdEnc);
                form.ShowDialog();
            }
            LoadForm();
        }

        private void buttonReport_Click(object sender, EventArgs e)
        {
            REPORTES.UI_FilltroReporteCD form = new REPORTES.UI_FilltroReporteCD();
            form.ShowDialog();
        }
    }
}
