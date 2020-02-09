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

namespace VentasCompras.COTIZACIONES
{
    public partial class UI_DetalleCotizaciones : Form
    {
        public UI_DetalleCotizaciones(string codigoEncabezado)
        {
            InitializeComponent();
            codigoEnc = codigoEncabezado;
            LoadForm();
        }

        //Variables

        string codigoEnc = "";
        string datosEmpleado = "";
        string NicknameEmpleado = "";
        string fechaCreacion = "";
        string proveedor = "";
        string aprobado = "";

        //Variables

        //FuncionesVarias

        void LoadForm()
        {
            CapaDatosFerreteria.ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            ClaseBitacoraFerreteria cbf = new ClaseBitacoraFerreteria();
            try
            {
                //Carga de datos del empleado
                datosEmpleado = cpd.ExtraerCodigo3("CONCAT(Apellidos,',',Nombre) AS ", "NombreC", "" +
                    " bd_ferreteria.tbl_Persona, bd_ferreteria.tbl_usuario, bd_ferreteria.tbl_Empleado",
                    "tbl_Empleado.status ", " 1 AND tbl_Persona.status = 1 " +
                    "AND tbl_Persona.id = tbl_Empleado.tbl_Persona_id " +
                    "AND tbl_usuario.tbl_empleado_DPI = tbl_empleado.DPI " +
                    "AND tbl_usuario.id = " + cbf.GetUserCodeForLog()
                    );
                //Carga de nickname del usuario
                NicknameEmpleado = cpd.ExtraerCodigo2("Nickname", "tbl_usuario", "id", cbf.GetUserCodeForLog());
                //Carga fecha de creacion
                fechaCreacion = cpd.ExtraerCodigo2("FechaEmision", "tbl_cotizacionencabezado", "id", codigoEnc);
                //Carga de Cliente
                proveedor = cpd.ExtraerCodigo3(
                    "Concat(tbl_proveedor.id,',',tbl_persona.Nombre,',',tbl_organizacion.Nombre) as ", "NombreC",
                    "tbl_persona, tbl_organizacion, tbl_proveedor, tbl_cotizacionencabezado",
                    "tbl_organizacion.id = tbl_proveedor.tbl_organizacion_id " +
                    "AND tbl_proveedor.tbl_Persona_id = tbl_Persona.id " +
                    "AND tbl_cotizacionencabezado.tbl_proveedor_id = tbl_proveedor.id " +
                    "AND tbl_cotizacionencabezado.id = " + codigoEnc + " " +
                    "AND tbl_proveedor.status ", " 1");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR AL CARGAR DATOS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //Asignacion de datos a los labels
            labelEmpleado.Text = datosEmpleado;
            labelNoDevolucion.Text = codigoEnc;
            labelFechaEntrega.Text = fechaCreacion;
            labelNickname.Text = NicknameEmpleado;
            cargarDetalle();
        }

        public void cargarDetalle()
        {
            ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            DataSet ds;
            try
            {
                ds = cpd.LlenarGrid("" +
                    "tbl_cotizaciondetalle.id as CODIGO, " +
                    "Concat(tbl_producto.id,',',tbl_producto.Nombre) as PRODUCTO, " +
                    "tbl_cotizaciondetalle.Cantidad as CANTIDAD ",
                    "tbl_cotizaciondetalle, tbl_cotizacionencabezado, " +
                    "tbl_Producto ",
                    "tbl_cotizacionencabezado.id = tbl_cotizaciondetalle.tbl_cotizacionencabezado_id " +
                    "AND tbl_cotizaciondetalle.tbl_cotizacionencabezado_id = " + codigoEnc + " " +
                    "AND tbl_cotizaciondetalle.tbl_producto_id = tbl_Producto.id", "Visualizacion de datos detalles de cotizaciones");
                dtg_ProductosDetalle.DataSource = ds.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR AL GUARDAR DATOS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //FuncioensVarias

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void numericUpDownAbono_ValueChanged(object sender, EventArgs e)
        {
            //if (labelSaldo.Text != "0.00")
        }

        private void buttonBack_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonReport_Click(object sender, EventArgs e)
        {
            string datosEmpleado = "";
            string motivo = "";
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
            ReporteadorFerreteria.REPORTS.ReportDevolucionCotizacionDetalle objRpt = new ReporteadorFerreteria.REPORTS.ReportDevolucionCotizacionDetalle();
            ReporteadorFerreteria.DATASETS.DataSetP ds1 = new ReporteadorFerreteria.DATASETS.DataSetP();
            foreach (DataGridViewRow row in dtg_ProductosDetalle.Rows)
            {
                ReporteadorFerreteria.DATASETS.DataSetP.tbl_Encabezado_DetalleRow rowDetalles = ds1.tbl_Encabezado_Detalle.Newtbl_Encabezado_DetalleRow();
                rowDetalles.id = Convert.ToString(row.Cells["CODIGO"].Value);
                rowDetalles.Producto = Convert.ToString(row.Cells["PRODUCTO"].Value);
                rowDetalles.Cantidad = Convert.ToString(row.Cells["CANTIDAD"].Value);

                ds1.tbl_Encabezado_Detalle.Addtbl_Encabezado_DetalleRow(rowDetalles);
            }
            objRpt.SetDataSource(ds1);
            TextObject txtObject = (TextObject)objRpt.ReportDefinition.Sections["Section1"].ReportObjects["TextBy"];
            TextObject txtObjectDate = (TextObject)objRpt.ReportDefinition.Sections["Section1"].ReportObjects["TextDate"];
            TextObject txtObjectType = (TextObject)objRpt.ReportDefinition.Sections["Section1"].ReportObjects["TextType"];
            TextObject txtObjectObjeto = (TextObject)objRpt.ReportDefinition.Sections["Section1"].ReportObjects["TextObjeto"];
            TextObject txtObjectNo = (TextObject)objRpt.ReportDefinition.Sections["Section1"].ReportObjects["TextNoOrden"];
            TextObject txtObjectFEmision = (TextObject)objRpt.ReportDefinition.Sections["Section1"].ReportObjects["TextFEmision"];
            TextObject txtObjectbjetivo = (TextObject)objRpt.ReportDefinition.Sections["Section1"].ReportObjects["TextObjetivo"];
            TextObject txtObjectMotivo = (TextObject)objRpt.ReportDefinition.Sections["Section1"].ReportObjects["TextMotivoDetalle"];
            TextObject txtObjectMotivoTitle = (TextObject)objRpt.ReportDefinition.Sections["Section1"].ReportObjects["TextMotivoTitle"];
            txtObjectDate.Text = DateTime.Now.ToLongTimeString();
            txtObject.Text = datosEmpleado;
            txtObjectType.Text = "COTIZACIONES";
            txtObjectObjeto.Text = "Proveedor:";
            txtObjectMotivo.Text = motivo;
            txtObjectMotivoTitle.Text = "";
            txtObjectNo.Text = labelNoDevolucion.Text;
            txtObjectFEmision.Text = labelFechaEntrega.Text;
            txtObjectbjetivo.Text = labelCliente.Text;

            UI_Visualizador form = new UI_Visualizador();
            form.crystalReportViewerGlobal.ReportSource = objRpt;
            form.ShowDialog();
        }
    }
}
