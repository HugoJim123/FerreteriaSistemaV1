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
    public partial class Proveedores : Form
    {
        public Proveedores()
        {
            InitializeComponent();
        }

        // Variables

        bool flagEnable = false;

        // Variables

        //Funciones varias

        public string nextCodigo(string tabla)
        {
            ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            return cpd.ExtraerCodigo2("AUTO_INCREMENT", "information_schema.TABLES", "TABLE_SCHEMA = 'bd_ferreteria' " +
                "AND TABLE_NAME", "'" + tabla + "'");
        }

        public void Limpiar()
        {
            textBoxNombre.Text = "";
            textBoxApellidos.Text = "";
            textBoxDireccion.Text = "";
            textBoxOrganizacion.Text = "";
        }

        //Funciones varias

        private void textBoxNombre_TextChanged(object sender, EventArgs e)
        {
            if (textBoxNombre.Text == "" || textBoxApellidos.Text == "" || textBoxOrganizacion.Text == "" || textBoxDireccion.Text == "") flagEnable = false;
            else flagEnable = true;
            buttonGuardar.Enabled = flagEnable;
        }

        private void buttonGuardar_EnabledChanged(object sender, EventArgs e)
        {
            if(buttonGuardar.Enabled == true) buttonGuardar.BackColor = Color.FromArgb(49, 156, 202);
            else buttonGuardar.BackColor = Color.FromArgb(128, 128, 128);
        }

        private void buttonGuardar_Click(object sender, EventArgs e)
        {
            string cap = "";
            ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            List<string> queryList = new List<string>();
            queryList.Add(cpd.DevolverSentencia_Insert("tbl_Persona", "0, '" + textBoxNombre.Text + "', '" + textBoxApellidos.Text + "' , 1"));
            queryList.Add(cpd.DevolverSentencia_Insert("tbl_Organizacion", "0, '" + textBoxOrganizacion.Text + "', '" + textBoxDireccion.Text + "', 1"));
            queryList.Add(cpd.DevolverSentencia_Insert("tbl_Proveedor", "0,  '" + textBoxDireccion.Text + "', " + nextCodigo("tbl_Persona") + "," + nextCodigo("tbl_Organizacion") + ", 1"));
            try
            {
                cpd.EjectuarOperacionExterna(queryList, "a", "a", "Transaccion de ingreso de usuario realizada");
                MessageBox.Show("Datos Ingresados Exitosamente", "Datos Ingresados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Limpiar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR AL GUARDAR DATOS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
