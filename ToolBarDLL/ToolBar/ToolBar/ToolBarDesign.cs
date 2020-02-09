using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LogicaToolBar;

namespace ToolBar
{
    public partial class ToolBarDesign : UserControl
    {
        public ToolBarDesign()
        {
            InitializeComponent();
        }


        // Variables
        static string estado;
        private int nControl;
        static Form forma;
        static bool existEstatus = false;
        LogicaToolbarClass lo = new LogicaToolbarClass();

        // Datagrid
        static DataGridView dataGr;
        /*[Description("Nombre del DataGridView")]
        [DisplayName("DataGridView")]
        [Category("Objeto Navegador")]*/
        public DataGridView DataGr
        {
            get { return dataGr; }
            set { dataGr = value; }
        }

        private void ToolBarDesign_Load(object sender, EventArgs e)
        {
        }


        /* Cargar Datos */
        public void nombreForm(Form fm)
        {
            forma = fm;
        }
        public void dgv_datos(DataGridView aux)
        {
            dataGr = aux;
            //this.cargarCampos();
        }


        /*----------------------------*/

        private void button1_Click(object sender, EventArgs e)
        {
            int j = 0;
            //sBanIngresar = 1;
            btn_Ingresar.Enabled = false;
            foreach (Control componente in forma.Controls)
            {
                if ((componente is TextBox) || (componente is ComboBox) || (componente is DateTimePicker) || (componente is DataGridView))
                {
                    componente.Text = "";
                    componente.Enabled = true;
                    j++;
                }
            }
            nControl = j;
            estado = "insertar";
            btn_Aceptar.Enabled = true;
            btn_Cancelar.Enabled = true;

            btn_Ingresar.Enabled = false;
            btn_Actualizar.Enabled = false;
            btn_Consultar.Enabled = false;
            btn_Eliminar.Enabled = false;
        }

        private void btn_Actualizar_Click(object sender, EventArgs e)
        {
            int j = 0;
            //sBanIngresar = 1;
            btn_Actualizar.Enabled = false;
            foreach (Control componente in forma.Controls)
            {
                if ((componente is TextBox) || (componente is ComboBox) || (componente is DateTimePicker))
                {

                    j++;
                    componente.Enabled = true;

                }
            }
            nControl = j;
            estado = "editar";
            btn_Aceptar.Enabled = true;
            btn_Cancelar.Enabled = true;

            btn_Ingresar.Enabled = false;
            btn_Actualizar.Enabled = false;
            btn_Consultar.Enabled = false;
            btn_Eliminar.Enabled = false;
        }

        private void btn_Eliminar_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("¿Esta seguro de eliminar el registro?", "Salir", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
            if (res == DialogResult.Yes)
            {

                int fila = DataGr.CurrentRow.Index;
                string id = dataGr.Rows[fila].Cells[0].Value.ToString();
                if (existEstatus)
                {
                    lo.pubEliminar(tabla, id, camposTabla);
                    MessageBox.Show("Registro eliminado exitosamente!!!");
                }
                else
                {
                    MessageBox.Show("Error al eliminar el registro (Campo status faltante)");
                }
            }
        }
    }
}
