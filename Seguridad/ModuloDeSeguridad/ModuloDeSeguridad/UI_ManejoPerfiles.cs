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
    public partial class UI_ManejoPerfiles : Form
    {
        public UI_ManejoPerfiles(Panel frmparent, Button btn, Panel pnlSeguimiento)
        {
            InitializeComponent();
            butn = btn;
            Seguimiento = pnlSeguimiento;
            form = frmparent;
            LoadForm();
        }

        // Variables
        string codigoACambiar = "";
        string filtro = "id";
        Button butn;
        Panel Seguimiento;
        Panel form;
        // Variables

        /// <summary>
        /// Mueve el panel de seguimiento segun lo que este seleccionado
        /// </summary>
        /// <param name="btn">nombre del boton</param>
        /// <param name="pnl">nombre del panel</param>
        void moverPanelSeguimientoSubmenu(Button btn, Panel pnl)
        {
            pnl.Location = new Point(btn.Location.X, btn.Location.Y + 45);
        }

        public void LoadForm()
        {
            comboBoxTipoBusqueda.SelectedIndex = 0;
            ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            DataSet ds;
            try
            {
                ds = cpd.LlenarGrid("id as 'CÓDIGO', NombrePerfil as 'NOMBRE PERFIL'", "tbl_PerfilEncabezado", "1" ,"Vista de datos de Perfiles");
                dtg_Perfiles.DataSource = ds.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error al Cargar los Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dtg_Perfiles_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                codigoACambiar = dtg_Perfiles.Rows[e.RowIndex].Cells[0].Value.ToString();
            }
        }

        private void txtBusqueda_TextChanged(object sender, EventArgs e)
        {
            ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            DataSet ds;
            try
            {
                if(txtBusqueda.Text == "")
                {
                    ds = cpd.LlenarGrid("id as 'CÓDIGO', NombrePerfil as 'NOMBRE PERFIL'", "tbl_PerfilEncabezado", "1", "Vista de datos de perfiles");
                }
                else
                {
                    ds = cpd.LlenarGrid("id as 'CÓDIGO', NombrePerfil as 'NOMBRE PERFIL'", "tbl_PerfilEncabezado", filtro + " = " + txtBusqueda.Text, "Vista de datos de perfiles");
                }
                dtg_Perfiles.DataSource = ds.Tables[0];
            }
            catch (Exception ex)
            {
            }
        }

        private void comboBoxTipoBusqueda_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBoxTipoBusqueda.Text == "Código")
            {
                filtro = "id";
            }
            else
            {
                filtro = "NombrePerfil";
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if(codigoACambiar != "")
            {
                ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
                try
                {
                    DialogResult dialogResult = MessageBox.Show("Seguro de Eliminar el perfil. Si lo elimina automáticamente se eliminaran los privilegios asignados a los usuarios con este perfil.", "Eliminar Datos", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        cpd.EliminarDatos("tbl_PerfilUsuario", "tbl_PerfilEncabezado_id", codigoACambiar,"Eliminacion de datos de asginacion de perfil a usuario");
                        cpd.EliminarDatos("tbl_PerfilEncabezado", "id", codigoACambiar, "Eliminacion de datos de perfiles");
                        MessageBox.Show("Datos eliminados correctamente", "Datos Eliminados", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        LoadForm();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Error al Cargar los Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            moverPanelSeguimientoSubmenu(butn, Seguimiento);
            UI_IngresoModificacionPerfiles man = new UI_IngresoModificacionPerfiles();
            man.TopLevel = false;
            man.AutoScroll = true;
            form.Controls.Clear();
            form.Controls.Add(man);
            man.Show();
            man.Dock = System.Windows.Forms.DockStyle.Fill;
        }
    }
}
