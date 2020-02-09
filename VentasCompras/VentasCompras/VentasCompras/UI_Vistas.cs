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
    public partial class UI_Vistas : Form
    {
        /// <summary>
        /// Funcion para generar las vistas 
        /// </summary>
        /// <param name="titulo2">Titulo de la vista</param>
        /// <param name="query2">Setencia para llenar el datagrid</param>
        /// <param name="totalSum2">Define si la vista debe mostrar un total</param>
        /// <param name="indexSum2">Define el numero de la fila que se debe sumar para el total</param>
        public UI_Vistas(string titulo2, string query2, string tabla2, string whereQuery2, string accion2, bool totalSum2, int indexSum2, bool isCompra2)
        {
            InitializeComponent();
            titulo = titulo2;
            query = query2;
            tabla = tabla2;
            whereQuery = whereQuery2;
            totalSum = totalSum2;
            indexSum = indexSum2;
            accion = accion2;
            isCompra = isCompra2;
            LoadForm();
        }

        //Variables

        string titulo = "";
        string query = "";
        string tabla = "";
        string whereQuery = "";
        string accion = "";
        bool totalSum = false;
        bool isCompra = false;
        int indexSum = 0;

        //Variables


        //Funciones

        void LoadForm()
        {
            ///Carga de grid Principal
            ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            try
            {
                DataSet ds = cpd.LlenarGrid(query, tabla, whereQuery, accion);
                dataGridViewVistas.DataSource = ds.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error al cargar datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // Carga del titulo
            labelTitulo.Text = titulo;

            //Calculo del total
            if (totalSum)
            {
                labelTotal.Text = (sumarFilas(indexSum)).ToString();
            }

        }

        double sumarFilas(int numColumna)
        {
            double sumTotal = 0;
            try
            {
                foreach (DataGridViewRow row in dataGridViewVistas.Rows)
                {
                    sumTotal += Convert.ToDouble(row.Cells[numColumna].Value);
                }
            }
            catch (Exception ex) { }
            return sumTotal;
        }

        //Funciones


        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
