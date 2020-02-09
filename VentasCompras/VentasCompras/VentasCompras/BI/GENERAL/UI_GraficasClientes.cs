using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using LiveCharts;
using LiveCharts.Wpf;
using Color = System.Drawing.Color;
using HorizontalAlignment = System.Windows.HorizontalAlignment;
using Panel = System.Windows.Controls.Panel;
using CapaDatosFerreteria;
using System.Drawing;

namespace VentasCompras.BI.GENERAL
{
    public partial class UI_GraficasClientes : Form
    {
        public UI_GraficasClientes()
        {
            InitializeComponent();
            CargarTercerGrafica();
            CargarGraficaProveedores();
        }

        ////////////////////////////////// PROCESOS TERCER GRAFICA

        string GetClienteID(string numeroPosicion)
        {
            string id = "";
            ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            try
            {
                id = cpd.ExtraerCodigo3(""+ 
                    "MAX(Total) as TOTAL, MAX(Saldo) as SALDO,tbl_Cliente_id as " , "CLIENTE",
                    "tbl_ventasencabezado ", "1","1 "+
                    "group by CLIENTE "+
                    "order by TOTAL DESC " +
                    "limit " + numeroPosicion + ", 1");
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "Error al Cargar los Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return id;
        }

        double GetDatoPorCliente(string codCliente, string campo)
        {
            string cant = "";
            if (codCliente != "")
            {
                ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
                try
                {
                    cant = cpd.ExtraerCodigo3("SUM(" + campo + ")" + " as ", "NombreC",
                        "tbl_ventasencabezado ",
                        "tbl_Cliente_id",
                        codCliente);
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.ToString(), "Error al Cargar los Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            double res = 0;
            if (cant == "") res = 0;
            else res = Convert.ToDouble(cant);
            return res;
        }

        string GetNameCliente(string codCliente)
        {
            string name = "";
            if (codCliente != "")
            {
                ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
                try
                {
                    name = cpd.ExtraerCodigo3("CONCAT(tbl_Persona.Nombre,',',tbl_organizacion.Nombre) as ", "NombreC",
                        "tbl_persona, tbl_organizacion, tbl_cliente",
                        "tbl_cliente.NIT", codCliente + " "+
                        "AND tbl_cliente.tbl_Persona_id = tbl_persona.id " +
                        "AND tbl_cliente.tbl_organizacion_id = tbl_organizacion.id "
                        );
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.ToString(), "Error al Cargar los Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return name;
        }


        void CargarTercerGrafica()
        {
            //Extraer Los nombres de productos
            string[] clientID = new string[10];
            clientID[9] = GetClienteID("0");
            clientID[8] = GetClienteID("1");
            clientID[7] = GetClienteID("2");
            clientID[6] = GetClienteID("3");
            clientID[5] = GetClienteID("4");
            clientID[4] = GetClienteID("5");
            clientID[3] = GetClienteID("6");
            clientID[2] = GetClienteID("7");
            clientID[1] = GetClienteID("8");
            clientID[0] = GetClienteID("9");

            double[] clienteTotal = new double[10];
            clienteTotal[0] = GetDatoPorCliente(clientID[0], "Total");
            clienteTotal[1] = GetDatoPorCliente(clientID[1], "Total");
            clienteTotal[2] = GetDatoPorCliente(clientID[2], "Total");
            clienteTotal[3] = GetDatoPorCliente(clientID[3], "Total");
            clienteTotal[4] = GetDatoPorCliente(clientID[4], "Total");
            clienteTotal[5] = GetDatoPorCliente(clientID[5], "Total");
            clienteTotal[6] = GetDatoPorCliente(clientID[6], "Total");
            clienteTotal[7] = GetDatoPorCliente(clientID[7], "Total");
            clienteTotal[8] = GetDatoPorCliente(clientID[8], "Total");
            clienteTotal[9] = GetDatoPorCliente(clientID[9], "Total");

            double[] clienteSaldo = new double[10];
            clienteSaldo[0] = GetDatoPorCliente(clientID[0], "Saldo");
            clienteSaldo[1] = GetDatoPorCliente(clientID[1], "Saldo");
            clienteSaldo[2] = GetDatoPorCliente(clientID[2], "Saldo");
            clienteSaldo[3] = GetDatoPorCliente(clientID[3], "Saldo");
            clienteSaldo[4] = GetDatoPorCliente(clientID[4], "Saldo");
            clienteSaldo[5] = GetDatoPorCliente(clientID[5], "Saldo");
            clienteSaldo[6] = GetDatoPorCliente(clientID[6], "Saldo");
            clienteSaldo[7] = GetDatoPorCliente(clientID[7], "Saldo");
            clienteSaldo[8] = GetDatoPorCliente(clientID[8], "Saldo");
            clienteSaldo[9] = GetDatoPorCliente(clientID[9], "Saldo");

            string[] clientName = new string[10];
            clientName[0] = GetNameCliente(clientID[0]);
            clientName[1] = GetNameCliente(clientID[1]);
            clientName[2] = GetNameCliente(clientID[2]);
            clientName[3] = GetNameCliente(clientID[3]);
            clientName[4] = GetNameCliente(clientID[4]);
            clientName[5] = GetNameCliente(clientID[5]);
            clientName[6] = GetNameCliente(clientID[6]);
            clientName[7] = GetNameCliente(clientID[7]);
            clientName[8] = GetNameCliente(clientID[8]);
            clientName[9] = GetNameCliente(clientID[9]);

            cartesianChart2.Series = new SeriesCollection
            {
                new StackedColumnSeries
                {
                    Title = "Total Pagado Venta",
                    Values = new ChartValues<double> { clienteTotal[0] - clienteSaldo[0], clienteTotal[1] - clienteSaldo[1], clienteTotal[2] - clienteSaldo[2], clienteTotal[3] - clienteSaldo[3],
                    clienteTotal[4] - clienteSaldo[4], clienteTotal[5] - clienteSaldo[5], clienteTotal[6] - clienteSaldo[6], clienteTotal[7] - clienteSaldo[7], clienteTotal[8] - clienteSaldo[8],
                    clienteTotal[9] - clienteSaldo[9],                    },
                    Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(250, 188, 8)),
                    Stroke = new SolidColorBrush(System.Windows.Media.Color.FromRgb(250, 188, 8)),

                },

                new StackedColumnSeries
                {
                    Title = "Faltante de Venta",
                    Values = new ChartValues<double> { clienteSaldo[0],  clienteSaldo[1],  clienteSaldo[2],  clienteSaldo[3],
                     clienteSaldo[4],  clienteSaldo[5],  clienteSaldo[6],  clienteSaldo[7],  clienteSaldo[8],
                     clienteSaldo[9]
                    },
                    Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(63, 97, 140)),
                    Stroke = new SolidColorBrush(System.Windows.Media.Color.FromRgb(63, 97, 140)),

                }
            };

            cartesianChart2.AxisX.Add(new Axis
            {
                Title = "CLIENTE",
                Labels = new[] { clientName[0], clientName[1], clientName[2], clientName[3], clientName[4],
                         clientName[5], clientName[6], clientName[7], clientName[8], clientName[9]
                    },
                Separator = DefaultAxes.CleanSeparator
            });

            cartesianChart2.AxisY.Add(new Axis
            {
                Title = "Valor",
                LabelFormatter = value => "Q " + value
            });
        }

        ////////////////////////////////// PROCESOS GRAFICA PROVEEDORES

        string GetProveedorID(string numeroPosicion)
        {
            string id = "";
            ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            try
            {
                id = cpd.ExtraerCodigo3("" +
                    "MAX(Total) as TOTAL, MAX(Saldo) as SALDO,tbl_Proveedor_id as ", "PROVEEDOR",
                    "tbl_comprasencabezado ", "1", "1 " +
                    "group by PROVEEDOR " +
                    "order by TOTAL DESC " +
                    "limit " + numeroPosicion + ", 1");
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "Error al Cargar los Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return id;
        }

        double GetDatoPorProveedor(string codCliente, string campo)
        {
            string cant = "";
            if (codCliente != "")
            {
                ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
                try
                {
                    cant = cpd.ExtraerCodigo3("SUM(" + campo + ")" + " as ", "NombreC",
                        "tbl_comprasencabezado ",
                        "tbl_Proveedor_id",
                        codCliente);
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.ToString(), "Error al Cargar los Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            double res = 0;
            if (cant == "") res = 0;
            else res = Convert.ToDouble(cant);
            return res;
        }

        string GetNameProveedor(string codProveedor)
        {
            string name = "";
            if (codProveedor != "")
            {
                ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
                try
                {
                    name = cpd.ExtraerCodigo3("CONCAT(tbl_Persona.Nombre,',',tbl_organizacion.Nombre) as ", "NombreC",
                        "tbl_persona, tbl_organizacion, tbl_proveedor",
                        "tbl_proveedor.id", codProveedor + " " +
                        "AND tbl_proveedor.tbl_Persona_id = tbl_persona.id " +
                        "AND tbl_proveedor.tbl_organizacion_id = tbl_organizacion.id "
                        );
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.ToString(), "Error al Cargar los Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return name;
        }


        void CargarGraficaProveedores()
        {
            //Extraer Los nombres de productos
            string[] proveedorID = new string[10];
            proveedorID[9] = GetProveedorID("0");
            proveedorID[8] = GetProveedorID("1");
            proveedorID[7] = GetProveedorID("2");
            proveedorID[6] = GetProveedorID("3");
            proveedorID[5] = GetProveedorID("4");
            proveedorID[4] = GetProveedorID("5");
            proveedorID[3] = GetProveedorID("6");
            proveedorID[2] = GetProveedorID("7");
            proveedorID[1] = GetProveedorID("8");
            proveedorID[0] = GetProveedorID("9");

            double[] proveedorTotal = new double[10];
            proveedorTotal[0] = GetDatoPorProveedor(proveedorID[0], "Total");
            proveedorTotal[1] = GetDatoPorProveedor(proveedorID[1], "Total");
            proveedorTotal[2] = GetDatoPorProveedor(proveedorID[2], "Total");
            proveedorTotal[3] = GetDatoPorProveedor(proveedorID[3], "Total");
            proveedorTotal[4] = GetDatoPorProveedor(proveedorID[4], "Total");
            proveedorTotal[5] = GetDatoPorProveedor(proveedorID[5], "Total");
            proveedorTotal[6] = GetDatoPorProveedor(proveedorID[6], "Total");
            proveedorTotal[7] = GetDatoPorProveedor(proveedorID[7], "Total");
            proveedorTotal[8] = GetDatoPorProveedor(proveedorID[8], "Total");
            proveedorTotal[9] = GetDatoPorProveedor(proveedorID[9], "Total");

            double[] proveedorSaldo = new double[10];
            proveedorSaldo[0] = GetDatoPorProveedor(proveedorID[0], "Saldo");
            proveedorSaldo[1] = GetDatoPorProveedor(proveedorID[1], "Saldo");
            proveedorSaldo[2] = GetDatoPorProveedor(proveedorID[2], "Saldo");
            proveedorSaldo[3] = GetDatoPorProveedor(proveedorID[3], "Saldo");
            proveedorSaldo[4] = GetDatoPorProveedor(proveedorID[4], "Saldo");
            proveedorSaldo[5] = GetDatoPorProveedor(proveedorID[5], "Saldo");
            proveedorSaldo[6] = GetDatoPorProveedor(proveedorID[6], "Saldo");
            proveedorSaldo[7] = GetDatoPorProveedor(proveedorID[7], "Saldo");
            proveedorSaldo[8] = GetDatoPorProveedor(proveedorID[8], "Saldo");
            proveedorSaldo[9] = GetDatoPorProveedor(proveedorID[9], "Saldo");

            string[] proveedorName = new string[10];
            proveedorName[0] = GetNameProveedor(proveedorID[0]);
            proveedorName[1] = GetNameProveedor(proveedorID[1]);
            proveedorName[2] = GetNameProveedor(proveedorID[2]);
            proveedorName[3] = GetNameProveedor(proveedorID[3]);
            proveedorName[4] = GetNameProveedor(proveedorID[4]);
            proveedorName[5] = GetNameProveedor(proveedorID[5]);
            proveedorName[6] = GetNameProveedor(proveedorID[6]);
            proveedorName[7] = GetNameProveedor(proveedorID[7]);
            proveedorName[8] = GetNameProveedor(proveedorID[8]);
            proveedorName[9] = GetNameProveedor(proveedorID[9]);

            cartesianChart1.Series = new SeriesCollection
            {
                new StackedColumnSeries
                {
                    Title = "Total Pagado Compra",
                    Values = new ChartValues<double> { proveedorTotal[0] - proveedorSaldo[0], proveedorTotal[1] - proveedorSaldo[1], proveedorTotal[2] - proveedorSaldo[2], proveedorTotal[3] - proveedorSaldo[3],
                    proveedorTotal[4] - proveedorSaldo[4], proveedorTotal[5] - proveedorSaldo[5], proveedorTotal[6] - proveedorSaldo[6], proveedorTotal[7] - proveedorSaldo[7], proveedorTotal[8] - proveedorSaldo[8],
                    proveedorTotal[9] - proveedorSaldo[9],                    },
                    Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(245, 235, 159)),
                    Stroke = new SolidColorBrush(System.Windows.Media.Color.FromRgb(245, 235, 159)),

                },

                new StackedColumnSeries
                {
                    Title = "Faltante Compra",
                    Values = new ChartValues<double> { proveedorSaldo[0],  proveedorSaldo[1],  proveedorSaldo[2],  proveedorSaldo[3],
                     proveedorSaldo[4],  proveedorSaldo[5],  proveedorSaldo[6],  proveedorSaldo[7],  proveedorSaldo[8],
                     proveedorSaldo[9]
                    },
                    Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(235, 161, 78)),
                    Stroke = new SolidColorBrush(System.Windows.Media.Color.FromRgb(235, 161, 78)),

                }
            };

            cartesianChart1.AxisX.Add(new Axis
            {
                Title = "PROVEEDOR",
                Labels = new[] { proveedorName[0], proveedorName[1], proveedorName[2], proveedorName[3], proveedorName[4],
                         proveedorName[5], proveedorName[6], proveedorName[7], proveedorName[8], proveedorName[9]
                    },
                Separator = DefaultAxes.CleanSeparator
            });

            cartesianChart1.AxisY.Add(new Axis
            {
                Title = "Valor",
                LabelFormatter = value => "Q " + value
            });
        }
    }
}