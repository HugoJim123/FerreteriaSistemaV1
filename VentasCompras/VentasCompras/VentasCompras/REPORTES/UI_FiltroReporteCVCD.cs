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
    public partial class UI_FiltroReporteCVCD : Form
    {
        public UI_FiltroReporteCVCD(string tablaObj2, string campoObj2, string titleObjetivo2)
        {
            InitializeComponent();
            tablaObj = tablaObj2;
            if (tablaObj == "tbl_comprasencabezado") labelObjetivo.Text = "Nombre Proveedor:";
            else labelObjetivo.Text = "Nombre Cliente:";
            campoObj = campoObj2;
            titleObjetivo = titleObjetivo2;
            try
            {
                dateTimePickerFCreacionMinimo.Value = DateTime.Parse("2000/01/01");
                dateTimePickerFCreacionMaximo.Value = DateTime.Parse("2050/01/01");
                dateTimePickerFCancelacionMinimo.Value = DateTime.Parse("2000/01/01");
                dateTimePickerFCancelacionMaximo.Value = DateTime.Parse("2050/01/01");
            }
            catch (Exception ex) { }
            LoadForm();
            comboBoxEstado.SelectedIndex = 0;
        }


        //Variables

        int tipo = 0;
        string tablaObj = "";
        string campoObj = "";
        string titleObjetivo = "";

        //Variables

        //Funciones

            void CargarDatosLabel()
        {
            switch (tipo)
            {
                case 0: //ComprasEncabezados
                    labelObjetivo.Text = "Nombre Proveedor:";
                    break;
                case 2: //VentasEncabezado
                    labelObjetivo.Text = "Nombre Cliente:";
                    break;
                default:
                    labelObjetivo.Text = "Nombre Proveedor:";
                    break;
            }
        }

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
                CargarGrid(tablaObj, campoObj);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void CargarGrid(string tabla, string campoObjetivo)
        {
            ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            try
            {
                if(campoObjetivo == "tbl_proveedor")
                {
                    ///id, fechaemision, fechacancelacion, total, objetivo, empleado, cancelado, saldo
                    ds = cpd.LlenarGrid(tabla + ".id as ID, " + tabla + ".FechaEmision as 'FECHA EMISION', " +
                       "" + tabla + ".FechaCancelacion as 'FECHA CANCELACION', " + tabla + ".Total as TOTAL, " +
                       "CONCAT(tbl_persona.Nombre,',',tbl_organizacion.Nombre) as '" + titleObjetivo + "'," +
                       "" + tabla + ".Cancelado as 'CANCELADO', " + tabla + ".Saldo as 'SALDO'",
                       "" + tabla + ", tbl_persona, tbl_organizacion, " + campoObjetivo + "",
                       "" + tabla + "." + campoObjetivo + "_id = " + campoObjetivo + ".id " +
                       "AND " + campoObjetivo + ".tbl_persona_id = tbl_persona.id " +
                       "AND " + tabla + ".status = 1 " +
                       "AND " + campoObjetivo + ".tbl_organizacion_id = tbl_organizacion.id " + filtro,
                       "Consulta Reportes");
                }
                else
                {
                    ds = cpd.LlenarGrid(tabla + ".id as ID, " + tabla + ".FechaEmision as 'FECHA EMISION', " +
                       "" + tabla + ".FechaCancelacion as 'FECHA CANCELACION', " + tabla + ".Total as TOTAL, " +
                       "CONCAT(tbl_persona.Nombre,',',tbl_organizacion.Nombre) as '" + titleObjetivo + "'," +
                       "" + tabla + ".Cancelado as 'CANCELADO', " + tabla + ".Saldo as 'SALDO'",
                       "" + tabla + ", tbl_persona, tbl_organizacion, " + campoObjetivo + "",
                       "" + tabla + "." + campoObjetivo + "_id = " + campoObjetivo + ".NIT " +
                       "AND " + campoObjetivo + ".tbl_persona_id = tbl_persona.id " +
                       "AND " + tabla + ".status = 1 " +
                       "AND " + campoObjetivo + ".tbl_organizacion_id = tbl_organizacion.id " + filtro,
                       "Consulta Reportes");
                }
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
            filtro = "";

            string stateSelected = "";
            if (comboBoxEstado.Text == "Cancelado") stateSelected = "1";
            else stateSelected = "0";

            if (textBoxNombre.Text != "") filtro += " AND tbl_persona.Nombre LIKE '" + textBoxNombre.Text + "%' ";
            if (textBoxCodigo.Text != "") filtro += " AND " + tablaObj + ".id LIKE '" + textBoxCodigo.Text + "%' ";
            if (comboBoxEstado.Text != "--") filtro += " AND " + tablaObj + ".Cancelado = " + stateSelected + " ";
            filtro += " AND " + tablaObj + ".FechaEmision BETWEEN " + dateTimePickerFCreacionMinimo.Value.ToString("yyyyMMdd") + " AND " + dateTimePickerFCreacionMaximo.Value.ToString("yyyyMMdd") + " ";
            filtro += " AND " + tablaObj + ".FechaCancelacion BETWEEN " + dateTimePickerFCancelacionMinimo.Value.ToString("yyyyMMdd") + " AND " + dateTimePickerFCancelacionMaximo.Value.ToString("yyyyMMdd") + " ";
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
            CargarGrid(tablaObj, campoObj);
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
            ReporteadorFerreteria.REPORTS.ReportComprasVentas objRpt = new ReporteadorFerreteria.REPORTS.ReportComprasVentas();
            ReporteadorFerreteria.DATASETS.DataSetP ds1 = new ReporteadorFerreteria.DATASETS.DataSetP();
            foreach (DataGridViewRow row in DataGridViewDetalles.Rows)
            {
                ReporteadorFerreteria.DATASETS.DataSetP.tbl_ReporteVentaCompraRow rowVentaCompra = ds1.tbl_ReporteVentaCompra.Newtbl_ReporteVentaCompraRow();
                rowVentaCompra.id = Convert.ToString(row.Cells["ID"].Value);
                rowVentaCompra.FechaEmision = Convert.ToString(row.Cells["FECHA EMISION"].Value);
                rowVentaCompra.FechaCancelacion = Convert.ToString(row.Cells["FECHA CANCELACION"].Value);
                rowVentaCompra.Total = Convert.ToString(row.Cells["TOTAL"].Value);
                rowVentaCompra.Objetivo = Convert.ToString(row.Cells[titleObjetivo].Value);
                rowVentaCompra.Cancelado = Convert.ToString(row.Cells["CANCELADO"].Value);
                rowVentaCompra.Saldo = Convert.ToString(row.Cells["SALDO"].Value);

                ds1.tbl_ReporteVentaCompra.Addtbl_ReporteVentaCompraRow(rowVentaCompra);
            }
            objRpt.SetDataSource(ds1);
            TextObject txtObject = (TextObject)objRpt.ReportDefinition.Sections["Section1"].ReportObjects["TextBy"];
            TextObject txtObjectDate = (TextObject)objRpt.ReportDefinition.Sections["Section1"].ReportObjects["TextDate"];
            TextObject txtTotal = (TextObject)objRpt.ReportDefinition.Sections["Section4"].ReportObjects["TextTotal"];
            TextObject txtSaldo = (TextObject)objRpt.ReportDefinition.Sections["Section4"].ReportObjects["TextSaldo"];
            string type = "";
            if (tablaObj == "tbl_comprasencabezado") type = "COMPRAS";
            else type = "VENTAS";
            TextObject txtObjectType = (TextObject)objRpt.ReportDefinition.Sections["Section1"].ReportObjects["TextType"];
            txtTotal.Text = sumarFilas(3).ToString();
            txtSaldo.Text = sumarFilas(6).ToString();
            txtObjectDate.Text = DateTime.Now.ToLongTimeString();
            txtObject.Text = datosEmpleado;
            txtObjectType.Text = type;
            UI_Visualizador form = new UI_Visualizador();
            form.crystalReportViewerGlobal.ReportSource = objRpt;
            form.ShowDialog();
        }
    }
}

