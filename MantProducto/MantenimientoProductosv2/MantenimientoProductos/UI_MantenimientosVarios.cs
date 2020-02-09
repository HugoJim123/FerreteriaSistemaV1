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

namespace MantenimientoProductos
{
    public partial class UI_MantenimientosVarios : Form
    {
        String ntabla;
        public UI_MantenimientosVarios(String nombreMantenimiento)
        {
            InitializeComponent();
            label1.Text = nombreMantenimiento;
            ntabla = nombreTabla(nombreMantenimiento);
        }
        /*************************************/

        String nombreTabla(String nombreMant)
        {
            String nameTabla = "";
            if(nombreMant == "Material")
            {
                nameTabla = "tbl_material";
            }else if (nombreMant == "Tipo")
            {
                nameTabla = "tbl_tipo";
            }
            else if (nombreMant == "Pais")
            {
                nameTabla = "tbl_pais";
            }
            else if (nombreMant == "Marca")
            {
                nameTabla = "tbl_marca";
            }
            else if (nombreMant == "Color")
            {
                nameTabla = "tbl_colorproducto";
            }
            else if (nombreMant == "Bodega")
            {
                nameTabla = "tbl_bodega";
            }
            return nameTabla; 
        }
    
        /*************************************/

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                CapaDatosFerreteria.ClaseCapaDatosFerreteria CD = new CapaDatosFerreteria.ClaseCapaDatosFerreteria();
                CD.InsertDatos(ntabla, "0, '" + textBoxDescripcion.Text + "', 1");
                MessageBox.Show("Datos Ingresados Exitosamente", "Datos Ingresados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBoxDescripcion.Text = "";
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error al Ingresar Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
