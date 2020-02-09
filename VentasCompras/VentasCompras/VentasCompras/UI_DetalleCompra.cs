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

namespace VentasCompras
{
    public partial class UI_DetalleCompra : Form
    {
        public UI_DetalleCompra(string codigoEncabezado)
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
        string fechaCancelacion = "";
        string saldo = "";
        string proveedor = "";
        string total = "";

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
                fechaCreacion = cpd.ExtraerCodigo2("FechaEmision","tbl_comprasencabezado","id",codigoEnc);
                //Carga fecha de cancelacion
                fechaCancelacion = cpd.ExtraerCodigo2("FechaCancelacion", "tbl_comprasencabezado", "id", codigoEnc);
                //Carga de saldo
                saldo = cpd.ExtraerCodigo2("saldo", "tbl_comprasencabezado", "id", codigoEnc);
                //Carga de total
                total = cpd.ExtraerCodigo2("total", "tbl_comprasencabezado", "id", codigoEnc);
                //Carga de proveedor
                proveedor = cpd.ExtraerCodigo3("tbl_organizacion.Nombre AS " , "NombreC",
                    "tbl_comprasencabezado, tbl_organizacion, tbl_proveedor ",
                    "tbl_comprasencabezado.tbl_proveedor_id = tbl_proveedor.id " +
                    "AND tbl_proveedor.tbl_organizacion_id = tbl_organizacion.id " +
                    "AND tbl_comprasencabezado.id",
                    codigoEnc);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR AL CARGAR DATOS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //Asignacion de datos a los labels
            labelEmpleado.Text = datosEmpleado;
            labelNoOrdenCompra.Text = codigoEnc;
            labelFechaEntrega.Text = fechaCreacion;
            labelFechaCancelación.Text = fechaCancelacion;
            labelSaldo.Text = saldo;
            labelTotal.Text = total;
            labelProveedor.Text = proveedor;
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
                    "tbl_comprasDetalle.id as CODIGO, " +
                    "tbl_producto.Nombre as PRODUCTO, " +
                    "tbl_comprasDetalle.Cantidad as CANTIDAD, " +
                    "tbl_comprasDetalle.Precio as PRECIO, " +
                    "tbl_comprasDetalle.Subtotal as SUBTOTAL ",
                    "tbl_comprasDetalle, " +
                    "tbl_Producto ",
                    "tbl_comprasDetalle.tbl_comprasEncabezado_id = " + codigoEnc + " " +
                    "AND tbl_comprasDetalle.tbl_producto_id = tbl_Producto.id", "Visualizacion de datos detalles de compras");
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

        private void buttonAgregarAbono_Click(object sender, EventArgs e)
        {
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
            double cantidadAbono = 0;
            double newSaldo = 0;
            ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            if (Convert.ToDouble(numericUpDownAbono.Value) > Convert.ToDouble(labelSaldo.Text)) cantidadAbono = Convert.ToDouble(labelSaldo.Text);
            else
            {
                cantidadAbono = Convert.ToDouble(numericUpDownAbono.Value);
                newSaldo = Convert.ToDouble(labelSaldo.Text) - cantidadAbono;
            }
            try
            {
                if(newSaldo == 0) cpd.ModificarDatos("tbl_comprasencabezado", "Cancelado = 1, saldo = " + newSaldo, "id", labelNoOrdenCompra.Text, "Abono de saldo");
                else cpd.ModificarDatos("tbl_comprasencabezado", "saldo = " + newSaldo, "id", labelNoOrdenCompra.Text, "Abono de saldo");
                MessageBox.Show("Abono guardado", "DATOS GUARDADOS CON EXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                numericUpDownAbono.Value = 0;
                LoadForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR AL GUARDAR DATOS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void numericUpDownAbono_ValueChanged_1(object sender, EventArgs e)
        {
            if(numericUpDownAbono.Value > 0) buttonAgregarAbono.Enabled = true;
            else buttonAgregarAbono.Enabled = false;
        }

        private void buttonReport_Click(object sender, EventArgs e)
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
            ReporteadorFerreteria.REPORTS.ReportDetallesComprasVentas objRpt = new ReporteadorFerreteria.REPORTS.ReportDetallesComprasVentas();
            ReporteadorFerreteria.DATASETS.DataSetP ds1 = new ReporteadorFerreteria.DATASETS.DataSetP();
            foreach (DataGridViewRow row in dtg_ProductosDetalle.Rows)
            {
                ReporteadorFerreteria.DATASETS.DataSetP.tbl_Encabezado_DetalleRow rowDetalles = ds1.tbl_Encabezado_Detalle.Newtbl_Encabezado_DetalleRow();
                rowDetalles.id = Convert.ToString(row.Cells["CODIGO"].Value);
                rowDetalles.Producto = Convert.ToString(row.Cells["PRODUCTO"].Value);
                rowDetalles.Cantidad = Convert.ToString(row.Cells["CANTIDAD"].Value);
                rowDetalles.Precio = Convert.ToString(row.Cells["PRECIO"].Value);
                rowDetalles.Subtotal = Convert.ToString(row.Cells["SUBTOTAL"].Value);

                ds1.tbl_Encabezado_Detalle.Addtbl_Encabezado_DetalleRow(rowDetalles);
            }
            objRpt.SetDataSource(ds1);
            TextObject txtObject = (TextObject)objRpt.ReportDefinition.Sections["Section1"].ReportObjects["TextBy"];
            TextObject txtObjectDate = (TextObject)objRpt.ReportDefinition.Sections["Section1"].ReportObjects["TextDate"];
            TextObject txtObjectType = (TextObject)objRpt.ReportDefinition.Sections["Section1"].ReportObjects["TextType"];
            TextObject txtObjectObjeto = (TextObject)objRpt.ReportDefinition.Sections["Section1"].ReportObjects["TextObjeto"];
            TextObject txtObjectNo = (TextObject)objRpt.ReportDefinition.Sections["Section1"].ReportObjects["TextNoOrden"];
            TextObject txtObjectFEmision = (TextObject)objRpt.ReportDefinition.Sections["Section1"].ReportObjects["TextFEmision"];
            TextObject txtObjectFCancelacion = (TextObject)objRpt.ReportDefinition.Sections["Section1"].ReportObjects["TextFCancelacion"];
            TextObject txtObjectbjetivo = (TextObject)objRpt.ReportDefinition.Sections["Section1"].ReportObjects["TextObjetivo"];
            TextObject txtObjectSaldo = (TextObject)objRpt.ReportDefinition.Sections["Section1"].ReportObjects["TextSaldo"];
            TextObject txtObjectTotal = (TextObject)objRpt.ReportDefinition.Sections["Section1"].ReportObjects["TextTotal"];
            txtObjectDate.Text = DateTime.Now.ToLongTimeString();
            txtObject.Text = datosEmpleado;
            txtObjectType.Text = "COMPRAS";
            txtObjectObjeto.Text = "Proveedor:";
            txtObjectNo.Text = labelNoOrdenCompra.Text;
            txtObjectFEmision.Text = labelFechaEntrega.Text;
            txtObjectFCancelacion.Text = labelFechaCancelación.Text;
            txtObjectbjetivo.Text = labelProveedor.Text;
            txtObjectSaldo.Text = labelSaldo.Text;
            txtObjectTotal.Text = labelTotal.Text;
            UI_Visualizador form = new UI_Visualizador();
            form.crystalReportViewerGlobal.ReportSource = objRpt;
            form.ShowDialog();
        }
    }
}
