﻿using System;
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
    public partial class UI_IngresoComprasv2 : Form
    {
        public UI_IngresoComprasv2()
        {
            InitializeComponent();
            LoadForm();
        }


        // Variables

        string datosEmpleado = "";
        string NicknameEmpleado = "";
        string ordenDeCompra = "";
        string codigoProducto = "";
        int datoAEliminar = 0; 

        // Variables

        // Funciones Varias

        void LoadForm()
        {
            CapaDatosFerreteria.ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            ClaseBitacoraFerreteria cbf = new ClaseBitacoraFerreteria();
            try
            {
                cargarProveedor();
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
                cargarBodegas();
                cargarProductos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR AL CARGAR DATOS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //Asignacion de datos a los labels
            labelEmpleado.Text = datosEmpleado;
            labelNoOrdenCompra.Text = nextCodigo("tbl_ComprasEncabezado");
            ordenDeCompra = nextCodigo("tbl_ComprasEncabezado");
            labelFechaEntrega.Text = DateTime.Now.ToShortDateString();
            labelNickname.Text = NicknameEmpleado;
        }


        public void cargarProductos()
        {
            CapaDatosFerreteria.ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            try
            {
                //Cargar datos de los productos
                cpd.cargarDatosACombobox2(comboBoxProducto, " CONCAT(id,',',Nombre) AS NombreC", "Nombre", "" +
                    "tbl_Producto WHERE tbl_Producto.status = 1 ");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR AL CARGAR DATOS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void cargarBodegas()
        {
            comboBoxBodega.Items.Clear();
            CapaDatosFerreteria.ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            try
            {
                //Cargar datos de las bodegas
                cpd.cargarDatosACombobox2(comboBoxBodega, " CONCAT(id,',',NombreBodega) AS NombreC", "Nombre", "" +
                    "tbl_Bodega WHERE tbl_Bodega.status = 1 ");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR AL CARGAR DATOS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void cargarProveedor()
        {
            comboBoxProveedor.Items.Clear();
            CapaDatosFerreteria.ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            try
            {
                //Carga del combobox de proveedores
                cpd.cargarDatosACombobox2(comboBoxProveedor,
                    "tbl_organizacion.Nombre as NombreC", "Nombre",
                    "tbl_organizacion, tbl_proveedor " +
                    "WHERE " +
                    "tbl_proveedor.tbl_organizacion_id = tbl_organizacion.id " +
                    "AND tbl_proveedor.status = 1");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR AL CARGAR DATOS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public string nextCodigo(string table)
        {
            ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            try
            {
                return cpd.ExtraerCodigo2("AUTO_INCREMENT", "information_schema.TABLES", "TABLE_SCHEMA = 'bd_ferreteria' " +
                    "AND TABLE_NAME", "'" + table + "'");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR AL CARGAR DATOS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }
        }
        /// <summary>
        /// Carga los datos en caso de ser una modificacion de producto
        /// </summary>
        /// <param name="codigoACambiar"></param>
        void cargardatos()
        {
            string[] cdProducto = comboBoxProducto.Text.Split(',');
            codigoProducto = cdProducto[0];
            ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            string[] datos;
            datos = new string[18];
            try
            {
                datos = cpd.cargarDetallesProductos(cdProducto[0], "Vista de detalles de productos");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR AL CARGAR DATOS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            ///     Carga del detalle del producto (Lo que no se va a modificar)
            labelCodigoDetalle.Text = "ID: " + datos[0];
            labelNombreDetalle.Text = "NOMBRE: " + datos[1];
            labelMaterialDetalle.Text = "MAT.: " + datos[13];
            labelTipoDetalle.Text = "TIP.: " + datos[14];
            labelTamano.Text = "TAM.: " + datos[6];
            labelDiametro.Text = "DIAM.: " + datos[7];
            labelFuerza.Text = "FUER.: " + datos[8];
            labelVoltaje.Text = "VOL.: " + datos[2];
        }

        double sumarFilas()
        {
            double sumTotal = 0;
            int numColumna = 4 ;
            try
            {
                foreach (DataGridViewRow row in dtg_ProductosDetalle.Rows)
                {
                    sumTotal += Convert.ToDouble(row.Cells[numColumna].Value);
                }
            }
            catch (Exception ex) { }
            return sumTotal;
        }

        void ActualizarTotal()
        {
            ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            try
            {
                cpd.ModificarDatos("tbl_ComprasEncabezado", "Total=" + labelTotalOrden.Text, "id", labelNoOrdenCompra.Text, "Ingreso de Total");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR AL GUARDAR DATOS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Refrresh()
        {
            LoadForm();
            numericUpDownCant.Value = 0;
            numericUpDownPrecioCompra.Value = 0;
            textBoxSubtotal.Text = "";
            dtg_ProductosDetalle.Rows.Clear();
        }

        // Funciones Varias

        private void comboBoxProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargardatos();
            ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            try
            {
                numericUpDownPrecioCompra.Value = Convert.ToDecimal(cpd.ExtraerCodigo2("PrecioCompra", "tbl_Producto", "id", codigoProducto));
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString(), "ERROR AL CARGAR DATOS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonAgregarProducto_Click(object sender, EventArgs e)
        {
            dtg_ProductosDetalle.Rows.Add(comboBoxProducto.Text, comboBoxBodega.Text, numericUpDownCant.Value.ToString(), numericUpDownPrecioCompra.Value.ToString(), textBoxSubtotal.Text);
            labelTotalOrden.Text = sumarFilas().ToString();
        }

        private void numericUpDownPrecioCompra_ValueChanged(object sender, EventArgs e)
        {
            textBoxSubtotal.Text = (numericUpDownCant.Value * numericUpDownPrecioCompra.Value).ToString();
        }

        private void textBoxSubtotal_TextChanged(object sender, EventArgs e)
        {
            if (textBoxSubtotal.Text != "" && Convert.ToDouble(textBoxSubtotal.Text) > 0)
            {
                buttonAgregarProducto.Enabled = true;
            }
            else
            {
                buttonAgregarProducto.Enabled = false;
            }
        }

        private void buttonAgregarEncabezado_Click(object sender, EventArgs e)
        {
            panelProductos.Visible = true;
            panelDataGird.Visible = true;
            string codigoProveedor = "";
            string codigoEmpleado = "";
            DateTime fEntrega = Convert.ToDateTime(labelFechaEntrega.Text);
            DateTime fCancelacion = Convert.ToDateTime(dateTimePickerFechaCancelacion.Value.ToString("yyyy-MM-dd"));

            string[] listaDetalle = new string[6];
            List<string> queryList = new List<string>();

            ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            try
            {
                // Insersion del encabezado
                codigoEmpleado = cpd.ExtraerCodigo("tbl_Empleado_DPI", "tbl_Usuario", "Nickname", labelNickname.Text);
                codigoProveedor = cpd.ExtraerCodigo3("tbl_proveedor.id AS ", "NombreC", "tbl_Proveedor, tbl_Organizacion", "tbl_Organizacion.Nombre = '" + comboBoxProveedor.Text
                    + "' AND tbl_Organizacion.id ", " tbl_Proveedor.tbl_Organizacion_id");
                queryList.Add(cpd.DevolverSentencia_Insert("tbl_ComprasEncabezado", "0, " + codigoProveedor + ", " + fEntrega.ToString("yyyyMMdd") + ", " + fCancelacion.ToString("yyyyMMdd") +
                    " ," + labelTotalOrden.Text + " ," + codigoEmpleado + ", 1, 0, " + labelTotalOrden.Text));
                //Insersino del detalle
                foreach (DataGridViewRow row in dtg_ProductosDetalle.Rows)
                {
                    listaDetalle[0] = Convert.ToString(row.Cells[0].Value); //PRODUCTO
                    listaDetalle[1] = Convert.ToString(row.Cells[1].Value);//BODEGA
                    listaDetalle[2] = Convert.ToString(row.Cells[2].Value);//CANTIDAD
                    listaDetalle[3] = Convert.ToString(row.Cells[3].Value);//PRECIO
                    listaDetalle[4] = Convert.ToString(row.Cells[4].Value);//SUBTOTAL
                    string[] datosProducto = listaDetalle[0].Split(',');
                    string[] datosBodega = listaDetalle[1].Split(',');
                    //Agregar el movimiento
                    string movCode = nextCodigo("tbl_MovimientoInventario");
                    queryList.Add(cpd.DevolverSentencia_Insert("tbl_movimientoInventario",
                        "0, 1, " + datosProducto[0] + ", " + fEntrega.ToString("yyyyMMdd") + ", " + listaDetalle[2] +
                     ", " + listaDetalle[3] + ", " + listaDetalle[4] + ", " + datosBodega[0]));
                    queryList.Add(cpd.DevolverSentencia_Insert("tbl_comprasdetalle", "0, " + labelNoOrdenCompra.Text + ", " + datosProducto[0] + ", " + listaDetalle[2]
                    + ", " + listaDetalle[3] + ", " + listaDetalle[4] +", " + movCode));
                }
                foreach (DataGridViewRow row in dtg_ProductosDetalle.Rows)
                {
                    listaDetalle[0] = Convert.ToString(row.Cells[0].Value); //PRODUCTO
                    listaDetalle[1] = Convert.ToString(row.Cells[1].Value);//BODEGA
                    listaDetalle[2] = Convert.ToString(row.Cells[2].Value);//CANTIDAD
                    listaDetalle[3] = Convert.ToString(row.Cells[3].Value);//PRECIO
                    listaDetalle[4] = Convert.ToString(row.Cells[4].Value);//SUBTOTAL
                    string[] datosProducto = listaDetalle[0].Split(',');
                    string[] datosBodega = listaDetalle[1].Split(',');
                    int sumaRows = 0;
                    foreach (DataGridViewRow row2 in dtg_ProductosDetalle.Rows)
                    {
                        if (row.Cells[0].Value.ToString() == row2.Cells[0].Value.ToString())
                        {
                            sumaRows += Convert.ToInt32(row2.Cells[2].Value);
                        }
                    }

                    //Agregar al inventario
                    string CantidadEnExistencia = cpd.ExtraerCodigo3("Cantidad as ","Cantidad", 
                        "tbl_productobodega",
                        "tbl_Bodega_id= " + datosBodega[0] +
                        " AND tbl_Producto_id ", datosProducto[0]);
                    if (CantidadEnExistencia == "") CantidadEnExistencia = "0";
                    int totalCantidad = Convert.ToInt32(CantidadEnExistencia) + sumaRows;
                    queryList.Add(cpd.DevolverSentencia_Insert("tbl_productobodega",
                        datosBodega[0] + ", " + datosProducto[0] + ", " + listaDetalle[2] + ")  ON DUPLICATE KEY UPDATE " +
                        "Cantidad = " + totalCantidad + "; -- "));
                }
                /*string review = "";
                foreach(string var in queryList)
                {
                    review += var + "\n";
                }
                MessageBox.Show(review);*/
                cpd.EjectuarOperacionExterna(queryList, "", "", "Registro de compra");
                MessageBox.Show("Se ha Realizado la compra", "DATOS GUARDADOS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Refrresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR AL GUARDAR DATOS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonAddPais_Click(object sender, EventArgs e)
        {
            Proveedores form = new Proveedores();
            form.ShowDialog();
            comboBoxProveedor.Items.Clear();
            cargarProveedor();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Seguro de Eliminar los datos", "Eliminar Datos", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                dtg_ProductosDetalle.Rows.RemoveAt(datoAEliminar);
            }
            labelTotalOrden.Text = sumarFilas().ToString();
        }

        private void dtg_ProductosDetalle_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                datoAEliminar = dtg_ProductosDetalle.CurrentRow.Index;
            }
            if (datoAEliminar < 0) button1.Enabled = false;
            else button1.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UI_MantenimientosPlantilla form = new UI_MantenimientosPlantilla("Bodega");
            form.ShowDialog();
        }

        private void buttonAddBodega_Click(object sender, EventArgs e)
        {
            UI_MantenimientosPlantilla form = new UI_MantenimientosPlantilla("Bodega");
            form.ShowDialog();
            cargarBodegas();
        }

        private void buttonAddProducto_Click(object sender, EventArgs e)
        {

        }
    }
}
