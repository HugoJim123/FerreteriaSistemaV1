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
    public partial class UI_IngresoModificacionPerfiles : Form
    {
        public UI_IngresoModificacionPerfiles()
        {
            InitializeComponent();
            LoadForm();
        }

        //Variables

        string cadenaPermisos = "";
        string[] permisosList = new string[8];
        string[] permisosListInsert = {"0" , "0", "0", "0", "0", "0", "0", "0" };
        string userCode = "";

        //Variables


        public void LoadForm()
        {
            comboBoxNombrePerfil.Items.Clear();
            comboBoxUserData.Items.Clear();
            ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            try
            {
                cpd.cargarDatosACombobox(comboBoxNombrePerfil, "NombrePerfil", "tbl_PerfilEncabezado");
                cpd.cargarDatosACombobox(comboBoxUserData, "Nickname", "tbl_Usuario WHERE status = 1");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error al Cargar los Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonNewProfile_Click(object sender, EventArgs e)
        {
            tableLayoutPanelPermisos.Enabled = true;
            privilegesState(2);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tableLayoutPanelPermisos.Enabled = false;
            LoadForm();
        }

        private void textBoxNombrePerfil_TextChanged(object sender, EventArgs e)
        {
            if(textBoxNombrePerfil.Text == "")
            {
                buttonGuardarPerfil.Enabled = false;
            }
            else
            {
                buttonGuardarPerfil.Enabled = true;
            }
        }

        public void privilegesState(int mode)
        {
            if(mode == 1)
            {
                if (permisosList[0] == "1")
                {
                    panelInicioPermiso.BackColor = Color.FromArgb(132, 191, 73);
                }
                else
                {
                    panelInicioPermiso.BackColor = Color.FromArgb(242, 107, 107);
                }
                if (permisosList[1] == "1")
                {
                    panelInventarioPermisos.BackColor = Color.FromArgb(132, 191, 73);
                }
                else
                {
                    panelInventarioPermisos.BackColor = Color.FromArgb(242, 107, 107);
                }
                if (permisosList[2] == "1")
                {
                    panelComprasPermisos.BackColor = Color.FromArgb(132, 191, 73);
                }
                else
                {
                    panelComprasPermisos.BackColor = Color.FromArgb(242, 107, 107);
                }
                if (permisosList[3] == "1")
                {
                    panelVentasPermisos.BackColor = Color.FromArgb(132, 191, 73);
                }
                else
                {
                    panelVentasPermisos.BackColor = Color.FromArgb(242, 107, 107);
                }
                if (permisosList[4] == "1")
                {
                    panelDevolucionesPermisos.BackColor = Color.FromArgb(132, 191, 73);
                }
                else
                {
                    panelDevolucionesPermisos.BackColor = Color.FromArgb(242, 107, 107);
                }
                if (permisosList[5] == "1")
                {
                    panelClientesPermisos.BackColor = Color.FromArgb(132, 191, 73);
                }
                else
                {
                    panelClientesPermisos.BackColor = Color.FromArgb(242, 107, 107);
                }
                if (permisosList[6] == "1")
                {
                    panelSeguridadPermis.BackColor = Color.FromArgb(132, 191, 73);
                }
                else
                {
                    panelSeguridadPermis.BackColor = Color.FromArgb(242, 107, 107);
                }
            }
           else if(mode == 2)
            {
                panelInicioPermiso.BackColor = Color.FromArgb(242, 107, 107);
                panelInventarioPermisos.BackColor = Color.FromArgb(242, 107, 107);
                panelComprasPermisos.BackColor = Color.FromArgb(242, 107, 107);
                panelVentasPermisos.BackColor = Color.FromArgb(242, 107, 107);
                panelDevolucionesPermisos.BackColor = Color.FromArgb(242, 107, 107);
                panelClientesPermisos.BackColor = Color.FromArgb(242, 107, 107);
                panelSeguridadPermis.BackColor = Color.FromArgb(242, 107, 107);
            }
        }

        private void comboBoxNombrePerfil_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            try
            {
                cadenaPermisos = cpd.ExtraerCodigo("CadenaPermisos", "tbl_PerfilEncabezado", "NombrePerfil", comboBoxNombrePerfil.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error al Cargar los Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            for(int i = 0; i < cadenaPermisos.Length; i++)
            {
                permisosList[i] = cadenaPermisos[i].ToString();
            }
            try
            {
                DataSet ds = cpd.LlenarGrid("" +
                    "DISTINCT tbl_usuario.id as 'CODIGO',  tbl_usuario.Nickname as 'NOMBRE USUARIO', " +
                    "tbl_empleado.DPI as 'DPI', tbl_persona.Nombre as 'NOMBRE', tbl_persona.Apellidos as 'APELLIDOS' " +
                "", "" +
                    "tbl_usuario, tbl_persona, tbl_empleado, tbl_telefono, tbl_correo, tbl_PerfilUsuario, tbl_PerfilEncabezado " +
                "", "" +
                    "tbl_usuario.tbl_empleado_DPI = tbl_empleado.DPI AND " +
                    "tbl_empleado.tbl_persona_id = tbl_persona.id AND " +
                    "tbl_PerfilUsuario.tbl_Usuario_id = tbl_Usuario.id AND " +
                    "tbl_PerfilUsuario.tbl_PerfilEncabezado_id = tbl_PerfilEncabezado.id AND " +
                    "'" + comboBoxNombrePerfil.Text + "' = tbl_PerfilEncabezado.NombrePerfil AND " +
                    "tbl_usuario.status = 1 " +
                    "; ", "Vista de usuarios y perfiles");
                dtg_PerfilesUsuario.DataSource = ds.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error al Cargar los Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            privilegesState(1);
        }

        public void buttonPrivilegeState(Panel pnl, Button btn, int index)
        {
            if(permisosListInsert[index] == "0")
            {
                pnl.BackColor = Color.FromArgb(132, 191, 73);
                permisosListInsert[index] = "1";
            }
            else
            {
                pnl.BackColor = Color.FromArgb(242, 107, 107);
                permisosListInsert[index] = "0";
            }
        }

        public void resizePanel(Button btn, Panel pnl)
        {
            btn.Width = pnl.Width - 12;
        }

        private void buttonInicioPermisos_Click(object sender, EventArgs e)
        {
            buttonPrivilegeState(panelInicioPermiso, buttonInicioPermisos, 0);
        }

        private void buttonInventariosPermisos_Click(object sender, EventArgs e)
        {
            buttonPrivilegeState(panelInventarioPermisos, buttonInventariosPermisos, 1);
        }

        private void buttonComprasPErmisos_Click(object sender, EventArgs e)
        {
            buttonPrivilegeState(panelComprasPermisos, buttonComprasPErmisos, 2);
        }

        private void buttonVentasPermisos_Click(object sender, EventArgs e)
        {
            buttonPrivilegeState(panelVentasPermisos, buttonVentasPermisos, 3);
        }

        private void buttonDevolucionesPermisos_Click(object sender, EventArgs e)
        {
            buttonPrivilegeState(panelDevolucionesPermisos, buttonDevolucionesPermisos, 4);
        }

        private void buttonClientesPermisos_Click(object sender, EventArgs e)
        {
            buttonPrivilegeState(panelClientesPermisos, buttonClientesPermisos, 5);
        }

        private void buttonSeguridadPermisos_Click(object sender, EventArgs e)
        {
            buttonPrivilegeState(panelSeguridadPermis, buttonSeguridadPermisos, 6);
        }

        private void buttonGuardarPerfil_Click(object sender, EventArgs e)
        {
            string charprofile = "";
            foreach(string value in permisosListInsert)
            {
                charprofile += value;
            }
            //MessageBox.Show(charprofile);
            ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            try
            {
                cpd.InsertDatos("tbl_PerfilEncabezado", "0, '" + textBoxNombrePerfil.Text + "','" + charprofile + "'","Insercion de datos en Perfil Encabezado");
                LoadForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error al Guardar los Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void panelInicioPermiso_Resize(object sender, EventArgs e)
        {
            switch((sender as Panel).Name)
            {
                case "panelInicioPermiso":
                    resizePanel(buttonInicioPermisos, panelInicioPermiso);
                    break;
                case "panelInventarioPermisos":
                    resizePanel(buttonInventariosPermisos, panelInventarioPermisos);
                    break;
                case "panelComprasPermisos":
                    resizePanel(buttonComprasPErmisos, panelComprasPermisos);
                    break;
                case "panelVentasPermisos":
                    resizePanel(buttonVentasPermisos, panelVentasPermisos);
                    break;
                case "panelDevolucionesPermisos":
                    resizePanel(buttonDevolucionesPermisos, panelDevolucionesPermisos);
                    break;
                case "panelClientesPermisos":
                    resizePanel(buttonClientesPermisos, panelClientesPermisos);
                    break;
                case "panelSeguridadPermis":
                    resizePanel(buttonSeguridadPermisos, panelSeguridadPermis);
                    break;
            }
        }

        private void dtg_PerfilesUsuario_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                userCode = dtg_PerfilesUsuario.Rows[e.RowIndex].Cells[0].Value.ToString();
            }
        }

        private void buttonRemoveUserProfile_Click(object sender, EventArgs e)
        {
            ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            try
            {
                string perfCode = cpd.ExtraerCodigo("id", "tbl_PerfilEncabezado", "NombrePerfil", comboBoxNombrePerfil.Text);
                string usCode = cpd.ExtraerCodigo("id", "tbl_usuario", "Nickname", comboBoxUserData.Text);
                cpd.InsertDatos("tbl_PerfilUsuario", perfCode.ToString() + ", " + usCode, "Insercion de datos en Asignacion de perfil a usuario");
                LoadForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error al Guardar Los Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonAddUserProfile_Click(object sender, EventArgs e)
        {
            if (userCode == "")
            {
                MessageBox.Show("Seleccione el usuario al que le desea eliminar el perfil", "Error al cargar datos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
                try
                {
                    string perfCode = cpd.ExtraerCodigo("id", "tbl_PerfilEncabezado", "NombrePerfil", comboBoxNombrePerfil.Text);
                    cpd.EliminarDatos("tbl_PerfilUsuario", "tbl_PerfilEncabezado_id ", perfCode + " AND tbl_Usuario_id = " + userCode, "Eliminacion de datos de asignacion de perfiles a usuarios");
                    LoadForm();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Error al Eliminar Los Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
