using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using CapaDatosFerreteria;

namespace ModuloDeSeguridad
{
    public partial class UI_CreacionDePerfiles : Form
    {
        public UI_CreacionDePerfiles()
        {
            InitializeComponent();
            LoadForm();
        }

        string codigoPerfil = "";
        string codigoPerfilUsuario = "";

        public void LoadForm()
        {
            comboBoxUsuario.Items.Clear();
            ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            try
            {
                DataSet ds = cpd.LlenarGrid("id as CODIGO, NombrePerfil as NOMBRE", "tbl_PerfilEncabezado", "1", "Vista datos de Perfiles");
                dtg_PerfilesAll.DataSource = ds.Tables[0];
                cpd.cargarDatosACombobox2(comboBoxUsuario, " CONCAT(id,',',Nickname) AS NombreC", "Nombre", "" +
                    " bd_ferreteria.tbl_usuario ");
                DataSet ds2 = cpd.LlenarGrid("tbl_PerfilEncabezado.id as CODIGO, tbl_PerfilEncabezado.NombrePerfil as NOMBRE", "" +
                    "tbl_PerfilEncabezado, tbl_Usuario, tbl_PerfilUsuario", " " +
                    "tbl_PerfilEncabezado.id = tbl_PerfilUsuario.tbl_PerfilEncabezado_id AND tbl_PerfilUsuario.tbl_Usuario_id = tbl_Usuario.id AND tbl_Usuario.id = " + codigoUsuario().ToString(), "Vista datos de perfiles");
                dataGridViewPerfilesUsuario.DataSource = ds2.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error al Guardar Los Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void cargarPermisosUsuario()
        {
            ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            try
            {
                DataSet ds2 = cpd.LlenarGrid("tbl_PerfilEncabezado.id as CODIGO, tbl_PerfilEncabezado.NombrePerfil as NOMBRE", "" +
                    "tbl_PerfilEncabezado, tbl_Usuario, tbl_PerfilUsuario", " " +
                    "tbl_PerfilEncabezado.id = tbl_PerfilUsuario.tbl_PerfilEncabezado_id AND tbl_PerfilUsuario.tbl_Usuario_id = tbl_Usuario.id AND tbl_Usuario.id = " + codigoUsuario().ToString(), "Vista datos de perfil");
                dataGridViewPerfilesUsuario.DataSource = ds2.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error al Guardar Los Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public string codigoUsuario()
        {
            string[] datos = comboBoxUsuario.Text.Split(',');
            return datos[0];
        }

        public bool[] privilegies = new bool[8];
        public string[] namePanel = new string[] {
           "panelARInicio", "panelARInventarios", "panelARCompras",
           "panelARVentas", "panelARDevoluciones", "panelARInicio",
           "panelARClientes", "panelARSeguridad"
        };

        public void asignarColores(Panel pnl, int index)
        {
            if(privilegies[index] == true)
            {
                pnl.BackColor = Color.FromArgb(242, 107, 107);
                privilegies[index] = false;
            }else
            {
                pnl.BackColor = Color.FromArgb(132, 191, 73);
                privilegies[index] = true;
            }
        }
        public void clear()
        {
            for(int i = 0; i < 8; i++)
            {
                privilegies[i] = true;
                Control[] ctl = this.Controls.Find(namePanel[i], true);
                Panel pnl = new Panel();
                foreach(Control value in ctl)
                {
                    if(value is Panel)
                    {
                        pnl = value as Panel;
                    }
                }
                asignarColores(pnl, i);
            }
            textBoxNombrePerfil.Text = "";
        }

        private void PrivilegeClicked(object sender, EventArgs e)
        {
            if(sender is Label)
            {
                Label send = sender as Label;
                if (send.Name == labelInicio.Name)
                {
                    asignarColores(panelARInicio, 0);
                }
                if (send.Name == labelInventarios.Name)
                {
                    asignarColores(panelARInventarios, 1);
                }
                if (send.Name == labelCompras.Name)
                {
                    asignarColores(panelARCompras, 2);
                }
                if (send.Name == labelVentas.Name)
                {
                    asignarColores(panelARVentas, 3);
                }
                if (send.Name == labelDevoluciones.Name)
                {
                    asignarColores(panelARDevoluciones, 4);
                }
                if (send.Name == labelClientes.Name)
                {
                    asignarColores(panelARClientes, 5);
                }
                if (send.Name == labelSeguridad.Name)
                {
                    asignarColores(panelARSeguridad, 6);
                }
            }
            if (sender is PictureBox)
            {
                PictureBox pcsender = sender as PictureBox;
                if (pcsender.Name == pictureBoxInicio.Name)
                {
                    asignarColores(panelARInicio, 0);
                }
                if (pcsender.Name == pictureBoxInventarios.Name)
                {
                    asignarColores(panelARInventarios, 1);
                }
                if (pcsender.Name == pictureBoxCompras.Name)
                {
                    asignarColores(panelARCompras, 2);
                }
                if (pcsender.Name == pictureBoxVentas.Name)
                {
                    asignarColores(panelARVentas, 3);
                }
                if (pcsender.Name == pictureBoxClientes.Name)
                {
                    asignarColores(panelARClientes, 5);
                }
                if (pcsender.Name == pictureBoxDevoluciones.Name)
                {
                    asignarColores(panelARDevoluciones, 4);
                }
                if (pcsender.Name == pictureBoxSeguridad.Name)
                {
                    asignarColores(panelARSeguridad, 6);
                }
            }
        }

        private void textBoxNombrePerfil_TextChanged(object sender, EventArgs e)
        {
            if(textBoxNombrePerfil.Text == "")
            {
                buttonAddPerfil.Enabled = false;
            }else if(textBoxNombrePerfil.Text != "")
            {
                buttonAddPerfil.Enabled = true;
            }
        }

        private void buttonAddPerfil_Click(object sender, EventArgs e)
        {
            string cadenaPrivilegios = "";
            foreach(bool value in privilegies)
            {
                if (value == true) cadenaPrivilegios += "1";
                else cadenaPrivilegios += "0";
            }
            ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            try
            {
                cpd.InsertDatos("tbl_perfilencabezado", "0, '" + textBoxNombrePerfil.Text + "', '" + cadenaPrivilegios+"'", "Insertar datos en Perfil");
                MessageBox.Show("Datos Guardados Exitosamente", "Datos Guardados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                clear();
                LoadForm();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error al Guardar Los Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dtg_PerfilesAll_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                codigoPerfil = dtg_PerfilesAll.Rows[e.RowIndex].Cells[0].Value.ToString();
            }
        }

        private void dataGridViewPerfilesUsuario_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                codigoPerfilUsuario = dataGridViewPerfilesUsuario.Rows[e.RowIndex].Cells[0].Value.ToString();
            }
        }

        private void buttonIngresarPerfilUsuario_Click(object sender, EventArgs e)
        {
            if(codigoPerfil == "")
            {
                MessageBox.Show("Seleccione el dato a Asignar", "Error al cargar datos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
                try
                {
                    cpd.InsertDatos("tbl_PerfilUsuario", codigoPerfil.ToString() + ", " + codigoUsuario().ToString(), "Insercion de datos en asignacion de perfil a usuarios");
                    cargarPermisosUsuario();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Error al Guardar Los Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonEliminarPerfilUsuario_Click(object sender, EventArgs e)
        {
            if (codigoPerfilUsuario == "")
            {
                MessageBox.Show("Seleccione el dato a Asignar", "Error al cargar datos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
                try
                {
                    cpd.EliminarDatos("tbl_PerfilUsuario", "tbl_PerfilEncabezado_id ", codigoPerfilUsuario.ToString() + " AND tbl_Usuario_id = " + codigoUsuario().ToString(), "Eliminacion de datos de asignacion de perfiles a usuarios");
                    cargarPermisosUsuario();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Error al Guardar Los Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void comboBoxUsuario_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarPermisosUsuario();
        }
    }
}
