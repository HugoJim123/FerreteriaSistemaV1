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
using System.Data;

namespace ModuloDeSeguridad
{
    public partial class UI_ConsultasUsuario : Form
    {
        string codigoACambiar;
        Panel frm;
        Panel Seguimiento;
        Button btn;
        string tipoBusqueda = "";
        string filtro = "";

        public UI_ConsultasUsuario(Panel frmparent, Panel seg, Button bt)
        {
            InitializeComponent();
            frm = frmparent;
            Seguimiento = seg;
            btn = bt;
            LoadForm();
            comboBoxFiltros.SelectedIndex = 0;
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

        void LoadForm()
        {
            ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            try
            {
                DataSet ds = cpd.LlenarGrid("" +
                    "DISTINCT tbl_usuario.id as 'CODIGO',  tbl_usuario.Nickname as 'NOMBRE USUARIO', " +
                    "tbl_empleado.DPI as 'DPI', tbl_persona.Nombre as 'NOMBRE', tbl_persona.Apellidos as 'APELLIDOS' " +
                    ", tbl_telefono.NumeroTelefono as 'TELEFONO', tbl_correo.Correo as 'CORREO' " +
                "", "" +
                    "tbl_usuario, tbl_persona, tbl_empleado, tbl_telefono, tbl_correo " +
                "", "" +
                    "tbl_usuario.tbl_empleado_DPI = tbl_empleado.DPI AND " +
                    "tbl_empleado.status = 1 AND " +
                    "tbl_persona.status = 1 AND " +
                    "tbl_usuario.tbl_empleado_DPI = tbl_empleado.DPI AND " +
                    "tbl_empleado.tbl_persona_id = tbl_persona.id AND " +
                    "tbl_telefono.tbl_persona_id = tbl_persona.id AND " +
                    "tbl_correo.tbl_persona_id = tbl_persona.id AND " +
                    "tbl_usuario.status = 1 "+ filtro +
                    "; ", "Vista de datos de usuario");
                dtg_Usuarios.DataSource = ds.Tables[0];
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error al Cargar los Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            try
            {
                DialogResult dialogResult = MessageBox.Show("Seguro de Eliminar los datos", "Eliminar Datos", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    cpd.ModificarDatos("tbl_usuario", "status = 0", "id", codigoACambiar,"Eliminacion de datos de Usuarios");
                    MessageBox.Show("Datos eliminados correctamente", "Datos Eliminados", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    LoadForm();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error al Cargar los Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dtg_Usuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                codigoACambiar = dtg_Usuarios.Rows[e.RowIndex].Cells[0].Value.ToString();
            }
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            LoadForm();
        }

        private void buttonChangePassword_Click(object sender, EventArgs e)
        {
            int tipoMov = 0;
            moverPanelSeguimientoSubmenu(btn, Seguimiento);
            switch ((sender as Button).Name)
            {
                case "buttonAdd":
                    tipoMov = 0;
                    break;
                case "buttonChangePassword":
                    tipoMov = 1;
                    break;
                default:
                    tipoMov = 0;
                    break;
            }
            if (codigoACambiar == "" && tipoMov == 1)
            {
                MessageBox.Show("Seleccione el dato a modificar", "Error al cargar datos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                UI_IngresoDeUsuarios form = new UI_IngresoDeUsuarios(tipoMov, codigoACambiar);
                form.TopLevel = false;
                form.AutoScroll = true;
                frm.Controls.Clear();
                frm.Controls.Add(form);
                form.Show();
                form.Dock = System.Windows.Forms.DockStyle.Fill;
            }
        }

        private void comboBoxFiltros_SelectedIndexChanged(object sender, EventArgs e)
        {
            tipoBusqueda = comboBoxFiltros.Text;
        }

        private void txtBusqueda_TextChanged(object sender, EventArgs e)
        {
            switch (tipoBusqueda)
            {
                case "Código":
                    filtro = " AND tbl_Usuario.Id LIKE '" + txtBusqueda.Text + "%'";
                    break;
                case "Nombre de Usuario":
                    filtro = " AND tbl_Usuario.Nickname LIKE '" + txtBusqueda.Text + "%'";
                    break;
                case "DPI":
                    filtro = " AND tbl_Empleado.DPI LIKE '" + txtBusqueda.Text + "%'";
                    break;
                case "Nombre":
                    filtro = " AND tbl_Persona.Nombre LIKE '" + txtBusqueda.Text + "%'";
                    break;
                case "Apellido":
                    filtro = " AND tbl_Persona.Apellidos LIKE '" + txtBusqueda.Text + "%'";
                    break;
                default:
                    filtro = "";
                    break;
            }
            LoadForm();
        }
    }
}
