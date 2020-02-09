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
using System.IO;

namespace ModuloDeSeguridad
{
    public partial class UI_ConsultaBitacora : Form
    {
        public UI_ConsultaBitacora()
        {
            InitializeComponent();
            LoadForm();
            comboBoxBusquedaPor.SelectedIndex = 0;
        }

        //Variables

        string filtro = "";
        string codigoACambiar = "";

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
                    "tbl_bitacora.id as 'CÓDIGO', " +
                    "CONCAT(tbl_persona.Nombre, ',', tbl_persona.Apellidos) as 'USUARIO PERSONA', " +
                    "tbl_usuario.Nickname as 'USUARIO NICKNAME', " +
                    "tbl_bitacora.host as 'HOST', " +
                    "tbl_bitacora.fecha as 'FECHA', " +
                    "tbl_bitacora.hora as 'HORA', " +
                    "tbl_bitacora.accion as 'ACCION'",
                    "tbl_bitacora, tbl_persona, tbl_usuario, tbl_empleado ",
                    "tbl_bitacora.tbl_usuario_id = tbl_usuario.id " +
                    "AND tbl_usuario.tbl_empleado_DPI = tbl_empleado.DPI " +
                    "AND tbl_empleado.tbl_persona_id = tbl_persona.id "  +
                    filtro,
                    "Visualizacion de Bitacora");
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
                if (comboBoxBusquedaPor.Text == "Usuario (Nickname)")
                {
                    filtro = "AND tbl_usuario.Nickname LIKE '" + txtBusqueda.Text + "%'";
                }
                if (comboBoxBusquedaPor.Text == "Actividad")
                {
                    filtro = "AND tbl_bitacora.accion LIKE '" + txtBusqueda.Text + "%'";
                }
            }
            CargarGrid();
        }

        ///Fucniones varias  

        private void txtBusqueda_TextChanged(object sender, EventArgs e)
        {
            CambiarBusqueda();
        }

        private void buttonReport_Click(object sender, EventArgs e)
        {
            try
            {
                string data = "";
                foreach (DataGridViewRow row in dtg_Compras.Rows)
                {
                    data += row.Cells[0].Value.ToString() + " - " +
                        row.Cells[1].Value.ToString() + " - " +
                        row.Cells[2].Value.ToString() + " - " +
                        row.Cells[3].Value.ToString() + " - " +
                        row.Cells[4].Value.ToString() + " - " +
                        row.Cells[5].Value.ToString() + " - " +
                        row.Cells[6].Value.ToString() + "\n";
                }

                string ruta = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\bitacora.txt";
                StreamWriter sw = new StreamWriter(ruta);
                sw.Write(data);
                sw.Close();

                MessageBox.Show("Datos exportados correctamente al archivo txt, en la ruta " + ruta, "DATOS EXPORTADOS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }catch(Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error al cargar datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
