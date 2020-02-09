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
    public partial class UI_OtrasConfiguraciones : Form
    {

        string codigoACambiar = "";
        string cadenaDeCampos = "";
        string tabla = "";
        string sWhere = "status = 1";

        public UI_OtrasConfiguraciones()
        {
            InitializeComponent();
            comboBoxTipoDato.SelectedIndex = 0;
        }

        void LoadForm()
        {
            ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            try
            {
                DataSet ds = cpd.LlenarGrid(cadenaDeCampos, tabla, sWhere);
                dataGridViewDetalles.DataSource = ds.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error al Cargar Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UI_OtrasConfiguraciones_Load(object sender, EventArgs e)
        {

        }

        private void comboBoxTipoDato_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBoxTipoDato.Text)
            {
                case "Material":
                    cadenaDeCampos = "id, DescripcionMaterial";
                    tabla = "tbl_Material";
                    break;
                case "Tipo":
                    cadenaDeCampos = "id, DescripcionTipo";
                    tabla = "tbl_Tipo";
                    break;
                case "Marca":
                    cadenaDeCampos = "id, DescripcionMarca";
                    tabla = "tbl_Marca";
                    break;
                case "Pais":
                    cadenaDeCampos = "id, DescripcionPais";
                    tabla = "tbl_Pais";
                    break;
                case "Color":
                    cadenaDeCampos = "id, DescripcionColor";
                    tabla = "tbl_ColorProducto";
                    break;
            }
            LoadForm();
        }

        private void dataGridViewDetalles_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                codigoACambiar = dataGridViewDetalles.Rows[e.RowIndex].Cells[0].Value.ToString();
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
                    cpd.ModificarDatos(tabla, "status = 0", "id", codigoACambiar);
                    MessageBox.Show("Dato Eliminado Correctamente", "Dato Eliminado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error al Eliminar el Dato", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            LoadForm();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            UI_MantenimientosVarios form = new UI_MantenimientosVarios(comboBoxTipoDato.Text);
            form.ShowDialog();
            LoadForm();
        }
    }
}
