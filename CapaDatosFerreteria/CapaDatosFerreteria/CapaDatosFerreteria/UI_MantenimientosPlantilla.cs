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

namespace CapaDatosFerreteria
{
    public partial class UI_MantenimientosPlantilla : Form
    {
        string ntabla = "";
        public string nameAccion = "";
        public UI_MantenimientosPlantilla(string nombreMantenimiento)
        {
            InitializeComponent();
            label1.Text = nombreMantenimiento;
            nameAccion = nombreMantenimiento;
            ntabla = nombreTabla(nombreMantenimiento);
        }

        string nombreTabla(string nombreMant)
        {
            string nameTabla = "";
            if(nombreMant == "Material") nameTabla = "tbl_material";
            else if (nombreMant == "Tipo") nameTabla = "tbl_tipo";
            else if (nombreMant == "Pais") nameTabla = "tbl_pais";
            else if (nombreMant == "Marca") nameTabla = "tbl_marca";
            else if (nombreMant == "Color") nameTabla = "tbl_colorproducto";
            else if (nombreMant == "Bodega") nameTabla = "tbl_bodega";
            else if (nombreMant == "Puesto") nameTabla = "tbl_puesto";
            else if (nombreMant == "Bodega") nameTabla = "tbl_bodega";
            /*switch (nameTabla)
        {
            case "Material":
                nameTabla = "tbl_material";
                break;
            case "Tipo":
                nameTabla = "tbl_tipo";
                break;
            case "Pais":
                nameTabla = "tbl_pais";
                break;
            case "Marca":
                nameTabla = "tbl_marca";
                break;
            case "Color":
                nameTabla = "tbl_colorproducto";
                break;
            case "Bodega":
                nameTabla = "tbl_bodega";
                break;
            case "Puesto":
                nameTabla = "tbl_Puesto";
                break;
        }*/
            return nameTabla;
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                CapaDatosFerreteria.ClaseCapaDatosFerreteria CD = new CapaDatosFerreteria.ClaseCapaDatosFerreteria();
                CD.InsertDatos(ntabla, "0, '" + textBoxDescripcion.Text + "', 1","Insercion de datos en " + nameAccion);
                MessageBox.Show("Datos Ingresados Exitosamente", "Datos Ingresados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBoxDescripcion.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error al Ingresar Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBoxDescripcion_TextChanged(object sender, EventArgs e)
        {
            if (textBoxDescripcion.Text != "")
            {
                buttonGuardar.Enabled = true;
            }
            else if (textBoxDescripcion.Text == "")
            {
                buttonGuardar.Enabled = false;
            }
        }
    }
}
