using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using CapaDatosFerreteria;

namespace VentasCompras.CLIENTES
{
    public partial class UI_IngresoClientes : Form
    {
        public UI_IngresoClientes()
        {
            InitializeComponent();
        }

        //Variables
        //Variables

        //Funciones

        bool validarEmail(string email)
        {
            try
            {
                return Regex.IsMatch(email,
                    @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        void Limpiar()
        {
            textBoxNIT.Text = "";
            textBoxNombre.Text = "";
            textBoxApellidos.Text = "";
            textBoxOrganizacion.Text = "";
            textBoxDireccion.Text = "";
            textBoxTransporte.Text = "";
            textBoxCorreo.Text = "";
            textBoxTelefono.Text = "";
        }

        public string nextCodigo(string tabla)
        {
            ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            return cpd.ExtraerCodigo2("AUTO_INCREMENT", "information_schema.TABLES", "TABLE_SCHEMA = 'bd_ferreteria' " +
                "AND TABLE_NAME", "'" + tabla + "'");
        }

        //Funciones


        //Validacion de campos
        private void textBoxNombre_TextChanged(object sender, EventArgs e)
        {
            if (textBoxNIT.Text == "" || textBoxNombre.Text == "" || textBoxApellidos.Text == "" || textBoxOrganizacion.Text == "" || textBoxDireccion.Text == ""
                 || textBoxTransporte.Text == "" || !validarEmail(textBoxCorreo.Text) || textBoxTelefono.Text == "") buttonGuardar.Enabled = false;
            else buttonGuardar.Enabled = true;
        }

        //Cambiar Color si esta habilitado o no
        private void buttonGuardar_EnabledChanged(object sender, EventArgs e)
        {
            Button btn = sender as Button;  //Se define el boton
            if (btn.Enabled == true) btn.BackColor = Color.FromArgb(49, 156, 202);
            else btn.BackColor = Color.Gray;
        }

        //Guardar los datos
        private void buttonGuardar_Click(object sender, EventArgs e)
        {
            ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            List<string> queryList = new List<string>();
            queryList.Add(cpd.DevolverSentencia_Insert("tbl_Persona", "0, '" + textBoxNombre.Text + "', '" + textBoxApellidos.Text + "' , 1"));
            queryList.Add(cpd.DevolverSentencia_Insert("tbl_Telefono", "0, '" + textBoxTelefono.Text + "', " + nextCodigo("tbl_Persona") + " , 1"));
            queryList.Add(cpd.DevolverSentencia_Insert("tbl_Correo", "0, '" + textBoxCorreo.Text + "', " + nextCodigo("tbl_Persona") + " , 1"));
            queryList.Add(cpd.DevolverSentencia_Insert("tbl_Organizacion", "0, '" + textBoxOrganizacion.Text + "', '" + textBoxDireccion.Text + "', 1"));
            queryList.Add(cpd.DevolverSentencia_Insert("tbl_cliente", textBoxNIT.Text+", " + nextCodigo("tbl_Persona") + ", '"+textBoxTransporte.Text+"', " +
                "" + nextCodigo("tbl_Organizacion") + ", 1"));
            /*string review = "";
            foreach (string var in queryList)
            {
                review += var + "\n";
            }
            MessageBox.Show(review);*/
            try
            {
                cpd.EjectuarOperacionExterna(queryList, "a", "a", "Transaccion de ingreso de Clientes realizada");
                MessageBox.Show("Datos Ingresados Exitosamente", "Datos Ingresados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Limpiar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR AL GUARDAR DATOS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
