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

namespace VentasCompras.CLIENTES
{
    public partial class UI_DetalleClientes : Form
    {
        public UI_DetalleClientes(string nit, string nombre, string organizacion, string direccion, string telefono)
        {
            InitializeComponent();
            labelNIT.Text = nit;
            labelNombre.Text = nombre;
            labelOrganizacion.Text = organizacion;
            labelDireccion.Text = direccion;
            labelTelefono.Text = telefono;
            if (nit != "")
            {
                ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
                DataSet ds= cpd.LlenarGrid(
                    "tbl_Producto.id as 'CODIGO', tbl_Producto.Nombre as 'NOMBRE', tbl_ventasdetalle.Cantidad as 'CANTIDAD', " +
                    "tbl_ventasdetalle.Precio as 'PRECIO', tbl_ventasdetalle.Subtotal as 'SUBTOTAL', " +
                    "tbl_ventasencabezado.FechaEmision as 'FECHA' ",
                    "tbl_ventasdetalle, tbl_Producto, tbl_VentasEncabezado, tbl_Cliente ",
                    "tbl_ventasdetalle.tbl_VentasEncabezado_id = tbl_VentasEncabezado.id AND " +
                    "tbl_VentasEncabezado.tbl_Cliente_id = tbl_Cliente.NIT AND " +
                    "tbl_ventasdetalle.tbl_Producto_id = tbl_Producto.id AND " +
                    "tbl_Cliente.NIT = " + nit,
                    "Visualizacion de historial de clientes");

                dtg_ClientesDetalles.DataSource = ds.Tables[0];
            }
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
