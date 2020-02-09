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
using CrystalDecisions.CrystalReports.Engine;
using ReporteadorFerreteria.REPORTS;
using ReporteadorFerreteria;

namespace VentasCompras.REPORTES
{
    public partial class UI_FiltroReporteDevoluciones : Form
    {
        public UI_FiltroReporteDevoluciones()
        {
            InitializeComponent();
            try
            {
                dateTimePickerFCreacionMinimo.Value = DateTime.Parse("2000/01/01");
                dateTimePickerFCreacionMaximo.Value = DateTime.Parse("2050/01/01");
            }
            catch (Exception ex) { }
            LoadForm();
            comboBoxEstado.SelectedIndex = 0;
        }


        //Variables

        //Variables

        //Funciones
        //Fucniones

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonMaximized_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized) this.WindowState = FormWindowState.Normal;
            else this.WindowState = FormWindowState.Maximized;
        }

        //Variables

        string filtro = "";
        DataSet ds;

        //Variables

        //Fucniones

        void LoadForm()
        {
            ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            try
            {
                CargarGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void CargarGrid()
        {
            string tabla = "tbl_devolucionencabezado";
            string campoObjetivo = "tbl_Cliente";
            ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            try
            {
                ///id, fechaemision, fechacancelacion, total, objetivo, empleado, cancelado, saldo
                ds = cpd.LlenarGrid(tabla + ".id as ID, " + tabla + ".FechaEmision as 'FECHA EMISION', " +
                   "" + tabla + ".Motivo as 'MOTIVO'," +
                   "CONCAT(tbl_persona.Nombre,',',tbl_organizacion.Nombre) as 'CLIENTE'," +
                   "" + tabla + ".Aprobado as 'APROBADO'",
                   "" + tabla + ", tbl_persona, tbl_organizacion, " + campoObjetivo + "",
                   "" + tabla + "." + campoObjetivo + "_id = " + campoObjetivo + ".NIT " +
                   "AND " + campoObjetivo + ".tbl_persona_id = tbl_persona.id " +
                   "AND " + tabla + ".status = 1 " +
                   "AND " + campoObjetivo + ".tbl_organizacion_id = tbl_organizacion.id " + filtro,
                   "Consulta Reportes");
                DataGridViewDetalles.DataSource = ds.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public string IsCancelado(string state)
        {
            if (state == "0") return "NO CANCELADO";
            else return "CANCELADO";
        }

        void ValueChangeBD()
        {
            string tablaObj = "tbl_devolucionencabezado";
            filtro = "";

            string stateSelected = "";
            if (comboBoxEstado.Text == "Aprobado") stateSelected = "1";
            else stateSelected = "0";

            if (textBoxNombre.Text != "") filtro += " AND tbl_persona.Nombre LIKE '" + textBoxNombre.Text + "%' ";
            if (textBoxCodigo.Text != "") filtro += " AND " + tablaObj + ".id LIKE '" + textBoxCodigo.Text + "%' ";
            if (textBoxMotivo.Text != "") filtro += " AND " + tablaObj + ".Motivo LIKE '" + textBoxMotivo.Text + "%' ";
            if (comboBoxEstado.Text != "--") filtro += " AND " + tablaObj + ".Aprobado = " + stateSelected + " ";
            filtro += " AND " + tablaObj + ".FechaEmision BETWEEN " + dateTimePickerFCreacionMinimo.Value.ToString("yyyyMMdd") + " AND " + dateTimePickerFCreacionMaximo.Value.ToString("yyyyMMdd") + " ";
        }

        double sumarFilas(int numCol)
        {
            double sumTotal = 0;
            try
            {
                foreach (DataGridViewRow row in DataGridViewDetalles.Rows)
                {
                    sumTotal += Convert.ToDouble(row.Cells[numCol].Value);
                }
            }
            catch (Exception ex) { }
            return sumTotal;
        }

        //Funciones

        private void textBoxCodigo_TextChanged(object sender, EventArgs e)
        {
            ValueChangeBD();
            CargarGrid();
        }

        private void buttonGenerarReporte_Click(object sender, EventArgs e)
        {
            string datosEmpleado = "";
            ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            ClaseBitacoraFerreteria cbf = new ClaseBitacoraFerreteria();
            try
            {
                datosEmpleado = cpd.ExtraerCodigo3("CONCAT(Apellidos,',',Nombre) AS ", "NombreC", "" +
                    " bd_ferreteria.tbl_Persona, bd_ferreteria.tbl_usuario, bd_ferreteria.tbl_Empleado",
                    "tbl_Empleado.status ", " 1 AND tbl_Persona.status = 1 " +
                    "AND tbl_Persona.id = tbl_Empleado.tbl_Persona_id " +
                    "AND tbl_usuario.tbl_empleado_DPI = tbl_empleado.DPI " +
                    "AND tbl_usuario.id = " + cbf.GetUserCodeForLog()
                    );
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            ///
            ReporteadorFerreteria.REPORTS.ReportDevoluciones objRpt = new ReporteadorFerreteria.REPORTS.ReportDevoluciones();
            ReporteadorFerreteria.DATASETS.DataSetP ds1 = new ReporteadorFerreteria.DATASETS.DataSetP();
            foreach (DataGridViewRow row in DataGridViewDetalles.Rows)
            {
                ReporteadorFerreteria.DATASETS.DataSetP.tbl_ReporteDevolucionesRow rowDevoluciones = ds1.tbl_ReporteDevoluciones.Newtbl_ReporteDevolucionesRow();
                rowDevoluciones.id = Convert.ToString(row.Cells["ID"].Value);
                rowDevoluciones.FechaEmision = Convert.ToString(row.Cells["FECHA EMISION"].Value);
                rowDevoluciones.Objetivo = Convert.ToString(row.Cells["CLIENTE"].Value);
                rowDevoluciones.Aprobado = Convert.ToString(row.Cells["APROBADO"].Value);
                rowDevoluciones.Motivo = Convert.ToString(row.Cells["MOTIVO"].Value);

                ds1.tbl_ReporteDevoluciones.Addtbl_ReporteDevolucionesRow(rowDevoluciones);
            }
            objRpt.SetDataSource(ds1);
            TextObject txtObject = (TextObject)objRpt.ReportDefinition.Sections["Section1"].ReportObjects["TextBy"];
            TextObject txtObjectDate = (TextObject)objRpt.ReportDefinition.Sections["Section1"].ReportObjects["TextDate"];
            TextObject txtObjectType = (TextObject)objRpt.ReportDefinition.Sections["Section1"].ReportObjects["TextType"];
            txtObjectDate.Text = DateTime.Now.ToLongTimeString();
            txtObject.Text = datosEmpleado;
            UI_Visualizador form = new UI_Visualizador();
            form.crystalReportViewerGlobal.ReportSource = objRpt;
            form.ShowDialog();
        }
    }
}
