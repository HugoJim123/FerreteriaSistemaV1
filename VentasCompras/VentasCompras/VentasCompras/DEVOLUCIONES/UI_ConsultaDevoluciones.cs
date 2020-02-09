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

namespace VentasCompras.DEVOLUCIONES
{
    public partial class UI_ConsultaDevoluciones : Form
    {
        public UI_ConsultaDevoluciones(string valorMenor1, Panel frmparent, Button btn, Panel pnlSeguimiento)
        {
            InitializeComponent();
            valorMenor = valorMenor1;
            LoadForm();
            comboBoxBusquedaPor.SelectedIndex = 0;
            butn = btn;
            Seguimiento = pnlSeguimiento;
            frm = frmparent;
        }

        //Variables

        string valorMenor = "";
        string filtro = "";
        string codigoACambiar = "";
        Panel frm;
        Button butn;
        Panel Seguimiento;

        //Variables

        ///Funciones varias

        // Funcion para cargar datos a paneles informativos
        void CargaPaneInformativo(Label lbl, string typeDateYear, string typeDateMonth, string typeDateDay, int variant, string typePanelInfo)
        {
            ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            string commandWHERE1 = "";
            string commandWHERE2 = "";
            if (typeDateMonth != "")
            {
                commandWHERE1 += " AND " + typeDateMonth + "(FechaEmision) " +
                " = (SELECT(" + typeDateMonth + "(NOW()))) ";
                if (typeDateDay != "")
                {
                    commandWHERE2 += " AND " + typeDateDay + "(FechaEmision) " +
                    " = (SELECT(" + typeDateDay + "(NOW()))) ";
                }
            }

            //Carga el segundo panel informativo
            try
            {
                /*
                string comand = "SELECT tbl_producto.id, tbl_producto.Nombre, SUM(tbl_productobodega.Cantidad) as sum_cantidad " +
                    "FROM bd_ferreteria.tbl_productobodega, bd_ferreteria.tbl_producto " +
                    "WHERE tbl_producto.id = tbl_productobodega.tbl_producto_id " +
                    "AND tbl_producto.status = 1 " +
                    "group by tbl_producto.Nombre " +
                    "HAVING sum_cantidad <= " + valorMenor + " ";
                labelProntosAAcabar.Text = (cpd.RetornoMenosExistencias(comand).ToString());*/
                if (variant == 0)
                {
                    string command = "SELECT id " +
                        "FROM tbl_devolucionencabezado " +
                        "WHERE " +
                        typeDateYear + "(FechaEmision) = " +
                        "(SELECT(" + typeDateYear + "(NOW()))) " + commandWHERE1 + commandWHERE2
                        + typePanelInfo;
                    //MessageBox.Show(command);
                    lbl.Text = (cpd.RetornoMenosExistencias(command).ToString());
                    /*lbl.Text = cpd.ExtraerCodigo3("SUM(total) as ", "Total",
                        "tbl_devolucionencabezado ",
                        typeDateYear + "(FechaEmision) ",
                        "(SELECT(" + typeDateYear + "(NOW()))) " + commandWHERE1 + commandWHERE2
                        + typePanelInfo
                        );*/
                }
                else
                {
                    string command = "SELECT id " +
                        "FROM tbl_devolucionencabezado " +
                        typePanelInfo;
                    //MessageBox.Show(command);
                    lbl.Text = (cpd.RetornoMenosExistencias(command).ToString());
                    /*lbl.Text = cpd.ExtraerCodigo3("SUM(total) as ", "Total",
                        "tbl_devolucionencabezado",
                        "1",
                        "1" + typePanelInfo);*/
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error al Cargar los Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Funcion que carga grid dependiendo del filtro
        void CargarGrid()
        {
            ///Carga de grid Principal
            ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            try
            {
                DataSet ds = cpd.LlenarGrid("" +
                    "tbl_devolucionencabezado.id as 'CÓDIGO',  CONCAT(tbl_persona.Nombre,',',tbl_organizacion.Nombre) as 'CLIENTE', " +
                    "tbl_devolucionencabezado.Motivo as 'MOTIVO', " +
                    "tbl_devolucionencabezado.FechaEmision as 'FECHA DE EMISIÓN' ",
                    "tbl_devolucionencabezado, tbl_organizacion,tbl_persona,tbl_cliente ",
                    "tbl_devolucionencabezado.tbl_Cliente_id = tbl_Cliente.NIT " +
                    "AND tbl_Cliente.tbl_organizacion_id = tbl_organizacion.id " +
                    "AND tbl_Cliente.tbl_Persona_id = tbl_Persona.id " +
                    "AND tbl_devolucionencabezado.status = 1 " +
                    filtro,
                    "Visualizacion de Devoluciones Encabezado");
                DataGridViewDevoluciones.DataSource = ds.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error al cargar datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        ///Funciona que contiene todas las acciones para cargar el form
        void LoadForm()
        {
            CargarGrid();

            ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            //Carga el segundo panel informativo [compras anho T]
            CargaPaneInformativo(labelDevAnho, "YEAR", "", "", 0, " AND status = 1");

            //Carga el segundo panel informativo [compras mes T]
            CargaPaneInformativo(labelDevMes, "YEAR", "MONTH", "", 0, " AND status = 1");

            //Carga el segundo panel informativo [compras dia T]
            CargaPaneInformativo(labelDevDia, "YEAR", "MONTH", "DAY", 0, " AND status = 1");

            //Carga el segundo panel informativo [compras totales T]
            CargaPaneInformativo(labelDevTotales, "YEAR", "", "", 1, " WHERE status = 1");

            //Carga el segundo panel informativo [compras anho C]
            CargaPaneInformativo(labelAnhoD, "YEAR", "", "", 0, " AND status = 1 AND Aprobado = 1");

            //Carga el segundo panel informativo [compras mes C]
            CargaPaneInformativo(labelMesD, "YEAR", "MONTH", "", 0, " AND status = 1 AND Aprobado = 1");

            //Carga el segundo panel informativo [compras dia C]
            CargaPaneInformativo(labelDiaD, "YEAR", "MONTH", "DAY", 0, " AND status = 1 AND Aprobado = 1");

            //Carga el segundo panel informativo [compras totales C]
            CargaPaneInformativo(labelTotalD, "YEAR", "", "", 1, " WHERE status = 1 AND Aprobado = 1");
        }

        void CambiarBusqueda()
        {
            if (txtBusqueda.Text == "")
            {
                filtro = "";
            }
            else
            {
                if (comboBoxBusquedaPor.Text == "Código")
                {
                    filtro = "AND tbl_devolucionencabezado.id LIKE '" + txtBusqueda.Text + "%'";
                }
                if (comboBoxBusquedaPor.Text == "Cliente Persona")
                {
                    filtro = "AND tbl_persona.Nombre LIKE '" + txtBusqueda.Text + "%'";
                }
                if (comboBoxBusquedaPor.Text == "Cliente Organización")
                {
                    filtro = "AND tbl_organizacion.Nombre LIKE '" + txtBusqueda.Text + "%'";
                }
            }
            CargarGrid();
        }

        void moverPanelSeguimientoSubmenu(Button btn, Panel pnl)
        {
            pnl.Location = new Point(btn.Location.X, btn.Location.Y + 45);
        }

        ///Fucniones varias    

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            LoadForm();
        }

        private void txtBusqueda_TextChanged(object sender, EventArgs e)
        {
            CambiarBusqueda();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {

            try
            {
                ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
                DialogResult dialogResult = MessageBox.Show("Seguro de Eliminar los datos", "Eliminar Datos", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    cpd.ModificarDatos("tbl_devolucionencabezado", "status = 0", "id", codigoACambiar, "Eliminacion de Ventas");
                    MessageBox.Show("Datos eliminados correctamente", "Datos Eliminados", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    LoadForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void dtg_Compras_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                codigoACambiar = DataGridViewDevoluciones.Rows[e.RowIndex].Cells[0].Value.ToString();
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            moverPanelSeguimientoSubmenu(butn, Seguimiento);
            UI_IngresoDevoluciones form = new UI_IngresoDevoluciones();
            form.TopLevel = false;
            form.AutoScroll = true;
            frm.Controls.Clear();
            frm.Controls.Add(form);
            form.Show();
            form.Dock = System.Windows.Forms.DockStyle.Fill;
        }

        private void dtg_Compras_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
           if (e.RowIndex >= 0)
            {
                string cdEnc = DataGridViewDevoluciones.Rows[e.RowIndex].Cells[0].Value.ToString();
                UI_DetalleDevoluciones form = new UI_DetalleDevoluciones(cdEnc);
                form.ShowDialog();
            }
            LoadForm();
        }

        private void panel9_Click(object sender, EventArgs e)
        {
            string filter = "";
            string title = "";
            switch ((sender as Panel).Name)
            {
                case "panelCAT":
                    title = "Devoluciones Totales por Año";
                    filter = "AND YEAR(FechaEmision) = (SELECT(YEAR(NOW()))) ";
                    break;
                case "panelCMT":
                    title = "Devoluciones Totales por Mes";
                    filter = "AND YEAR(FechaEmision) = (SELECT(YEAR(NOW()))) " +
                        "AND MONTH(FechaEmision) = (SELECT(MONTH(NOW())))";
                    break;
                case "panelCDT":
                    title = "Devoluciones Totales por Dia";
                    filter = "AND YEAR(FechaEmision) = (SELECT(YEAR(NOW()))) " +
                        "AND MONTH(FechaEmision) = (SELECT(MONTH(NOW()))) " +
                        "AND DAY(FechaEmision) = (SELECT(DAY(NOW())))";
                    break;
                case "panelCTT":
                    title = "Devoluciones Totales Totales";
                    break;
                case "panelCAC":
                    title = "Devoluciones Aprobadas por Año";
                    filter = "AND YEAR(FechaEmision) = (SELECT(YEAR(NOW()))) " +
                        "AND Aprobado = 1";
                    break;
                case "panelCMC":
                    title = "Devoluciones Aprobadas por Mes";
                    filter = "AND YEAR(FechaEmision) = (SELECT(YEAR(NOW()))) " +
                        "AND MONTH(FechaEmision) = (SELECT(MONTH(NOW())))" +
                        "AND Aprobado = 1";
                    break;
                case "panelCDC":
                    title = "Devoluciones Aprobadas por Dia";
                    filter = "AND YEAR(FechaEmision) = (SELECT(YEAR(NOW()))) " +
                        "AND MONTH(FechaEmision) = (SELECT(MONTH(NOW()))) " +
                        "AND DAY(FechaEmision) = (SELECT(DAY(NOW())))" +
                        "AND Aprobado = 1";
                    break;
                case "panelCTC":
                    title = "Devoluciones Aprobadas Totales";
                    filter = "AND Aprobado = 1";
                    break;
            }
            UI_Vistas form = new UI_Vistas(title, "" +
                    "tbl_devolucionencabezado.id as 'CÓDIGO',  CONCAT(tbl_persona.Nombre,',',tbl_organizacion.Nombre) as 'CLIENTE', " +
                    "tbl_devolucionencabezado.Motivo as 'MOTIVO', " +
                    "tbl_devolucionencabezado.FechaEmision as 'FECHA DE EMISIÓN' ",
                    "tbl_devolucionencabezado, tbl_organizacion,tbl_persona,tbl_cliente ",
                    "tbl_devolucionencabezado.tbl_Cliente_id = tbl_Cliente.NIT " +
                    "AND tbl_Cliente.tbl_organizacion_id = tbl_organizacion.id " +
                    "AND tbl_Cliente.tbl_Persona_id = tbl_Persona.id " +
                    "AND tbl_devolucionencabezado.status = 1 " +
                filter,
                "Visualizacion de Devoluciones Encabezado",
                true,
                5,
                true);
            form.ShowDialog();
        }

        private void buttonReport_Click(object sender, EventArgs e)
        {
            REPORTES.UI_FiltroReporteDevoluciones form = new REPORTES.UI_FiltroReporteDevoluciones();
            form.ShowDialog();
        }
    }
}