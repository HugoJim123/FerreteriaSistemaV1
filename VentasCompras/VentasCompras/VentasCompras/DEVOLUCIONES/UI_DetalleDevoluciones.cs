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

namespace VentasCompras.DEVOLUCIONES
{
    public partial class UI_DetalleDevoluciones : Form
    {
        public UI_DetalleDevoluciones(string codigoEncabezado)
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
        string cliente = "";
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
                fechaCreacion = cpd.ExtraerCodigo2("FechaEmision", "tbl_devolucionencabezado", "id", codigoEnc);
                //Carga de aprobado
                string apro = cpd.ExtraerCodigo2("Aprobado", "tbl_devolucionencabezado", "id", codigoEnc);
                if (apro == "0")
                {
                    aprobado = "Desaprobado";
                    buttonCambiarEstado.Enabled = true;
                }
                else
                {
                    aprobado = "Aprobado";
                    buttonCambiarEstado.Enabled = false;
                }
                //Carga de Cliente
                cliente = cpd.ExtraerCodigo3(
                    "Concat(tbl_cliente.NIT,',',tbl_persona.Nombre,',',tbl_organizacion.Nombre) as ", "NombreC",
                    "tbl_persona, tbl_organizacion, tbl_cliente, tbl_ventasencabezado",
                    "tbl_organizacion.id = tbl_cliente.tbl_organizacion_id " +
                    "AND tbl_cliente.tbl_Persona_id = tbl_Persona.id " +
                    "AND tbl_ventasencabezado.tbl_Cliente_id = tbl_Cliente.NIT " +
                    "AND tbl_ventasencabezado.id = " + codigoEnc + " " +
                    "AND tbl_cliente.status ", " 1");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR AL CARGAR DATOS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //Asignacion de datos a los labels
            labelEmpleado.Text = datosEmpleado;
            labelNoDevolucion.Text = codigoEnc;
            labelFechaEntrega.Text = fechaCreacion;
            labelEstado.Text = aprobado;
            labelNickname.Text = NicknameEmpleado;
            labelCliente.Text = cliente;
            cargarDetalle();
        }

        public void cargarDetalle()
        {
            ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            DataSet ds;
            try
            {
                ds = cpd.LlenarGrid("" +
                    "tbl_devoluciondetalle.id as CODIGO, " +
                    "Concat(tbl_producto.id,',',tbl_producto.Nombre) as PRODUCTO, " +
                    "Concat(tbl_bodega.id,',',tbl_bodega.NombreBodega) as BODEGA, " +
                    "tbl_devoluciondetalle.Cantidad as CANTIDAD, " +
                    "tbl_devolucionencabezado.Motivo as MOTIVO ",
                    "tbl_devoluciondetalle, tbl_devolucionencabezado, tbl_bodega," +
                    "tbl_Producto ",
                    "tbl_bodega.id = tbl_devoluciondetalle.tbl_bodega_id " +
                    "AND tbl_devolucionencabezado.id = tbl_devoluciondetalle.tbl_devolucionencabezado_id " +
                    "AND tbl_devoluciondetalle.tbl_devolucionencabezado_id = " + codigoEnc + " " +
                    "AND tbl_devoluciondetalle.tbl_producto_id = tbl_Producto.id", "Visualizacion de datos detalles de devoluciones");
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

        private void buttonAgregarAbono_Click_1(object sender, EventArgs e)
        {
            string[] listaDetalle = new string[6];
            List<string> queryList = new List<string>();

            ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            try
            {
                foreach (DataGridViewRow row in dtg_ProductosDetalle.Rows)
                {
                    //Insercion de las cantidades a las bodegas
                    listaDetalle[0] = Convert.ToString(row.Cells[0].Value); //CODIGO
                    listaDetalle[1] = Convert.ToString(row.Cells[1].Value);//PRODUCTO
                    listaDetalle[2] = Convert.ToString(row.Cells[2].Value);//BODEGA
                    listaDetalle[3] = Convert.ToString(row.Cells[3].Value);//CANTIDAD
                    string[] datosProducto = listaDetalle[1].Split(',');
                    string[] datosBodega = listaDetalle[2].Split(',');
                    int sumaRows = 0;
                    foreach (DataGridViewRow row2 in dtg_ProductosDetalle.Rows)
                    {
                        if (row.Cells[1].Value.ToString() == row2.Cells[1].Value.ToString())
                        {
                            sumaRows += Convert.ToInt32(row2.Cells[3].Value);
                        }
                    }

                    //Agregar al inventario
                    string CantidadEnExistencia = cpd.ExtraerCodigo3("Cantidad as ", "Cantidad",
                        "tbl_productobodega",
                        "tbl_Bodega_id= " + datosBodega[0] +
                        " AND tbl_Producto_id ", datosProducto[0]);
                    if (CantidadEnExistencia == "") CantidadEnExistencia = "0";
                    int totalCantidad = Convert.ToInt32(CantidadEnExistencia) + sumaRows;
                    queryList.Add(cpd.DevolverSentencia_Insert("tbl_productobodega",
                        datosBodega[0] + ", " + datosProducto[0] + ", " + listaDetalle[3] + ")  ON DUPLICATE KEY UPDATE " +
                        "Cantidad = " + totalCantidad + "; -- "));
                    queryList.Add(cpd.DevolverSentencia_Modificar("tbl_devolucionencabezado", "aprobado = 1", "id", codigoEnc));
                }
                /*string review = "";
                foreach (string var in queryList)
                {
                    review += var + "\n";
                }
                MessageBox.Show(review);*/
                cpd.EjectuarOperacionExterna(queryList, "", "", "Aprobacion de devolucion");
                MessageBox.Show("Se ha Realizado la devolucion", "DATOS GUARDADOS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR AL GUARDAR DATOS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                motivo = cpd.ExtraerCodigo2("Motivo", "tbl_devolucionencabezado",
                    "id", labelNoDevolucion.Text);
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
            txtObjectType.Text = "DEVOLUCIONES";
            txtObjectObjeto.Text = "Cliente:";
            txtObjectMotivo.Text = motivo;
            txtObjectNo.Text = labelNoDevolucion.Text;
            txtObjectFEmision.Text = labelFechaEntrega.Text;
            txtObjectbjetivo.Text = labelCliente.Text;
            
            UI_Visualizador form = new UI_Visualizador();
            form.crystalReportViewerGlobal.ReportSource = objRpt;
            form.ShowDialog();
        }
    }
}