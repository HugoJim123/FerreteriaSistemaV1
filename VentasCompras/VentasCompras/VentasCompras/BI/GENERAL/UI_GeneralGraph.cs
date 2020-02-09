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
    public partial class UI_GeneralGraph : Form
    {
        public UI_GeneralGraph()
        {
            InitializeComponent();
            CargarGrafica1();
            CargarSegundaGrafica();
            CargarRelacionVentasCompras();
            CargarTercerGrafica();
            /*System.Windows.Forms.MessageBox.Show(
            GetPreviousMonth().Month.ToString());*/
        }

        //Variables
        //Variables

        //Funciones

        string GetMonthByNumber(string number)
        {
            string result = "";
            switch (number)
            {
                case "1":
                    result = "Enero";
                    break;
                case "2":
                    result = "Febrero";
                    break;
                case "3":
                    result = "Marzo";
                    break;
                case "4":
                    result = "Abril";
                    break;
                case "5":
                    result = "Mayo";
                    break;
                case "6":
                    result = "Junio";
                    break;
                case "7":
                    result = "Julio";
                    break;
                case "8":
                    result = "Agosto";
                    break;
                case "9":
                    result = "Septiembre";
                    break;
                case "10":
                    result = "Octubre";
                    break;
                case "11":
                    result = "Noviembre";
                    break;
                case "12":
                    result = "Diciembre";
                    break;
                default:
                    result = "Enero";
                    break;
            }
            return result;
        }

        DateTime GetPreviousMonth(int numberMonthMinus)
        {
            return DateTime.Now.AddMonths(numberMonthMinus);
        }

        /// <summary>
        /// Retorna el valor dependiendo de la tabla y el mes
        /// </summary>
        /// <param name="monthNum">numero de mes</param>
        /// <param name="table">tabla de la que se extraeran datos</param>
        /// <returns></returns>
        double ExtraerValor(string monthNum, string table)
        {
            double res = 0;
            string resaux = "0";
            ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            try
            {
                resaux = cpd.ExtraerCodigo3("SUM(Total) AS ", "Total",
                    table,
                    "MONTH(FechaEmision) = '" + monthNum + "' AND YEAR(FechaEmision) ", " (SELECT YEAR(NOW())) AND status = 1 ");
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "Error al Cargar los Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (resaux == "") res = 0;
            else res = Convert.ToDouble(resaux);
            return res;
        }

        /// <summary>
        /// Retorna el valor dependiendo de la tabla y el mes
        /// </summary>
        /// <param name="monthNum">numero de mes</param>
        /// <param name="table">tabla de la que se extraeran datos</param>
        /// <returns></returns>
        double ExtraerValorDevoluciones(string monthNum, string table)
        {
            double res = 0;
            string resaux = "0";
            ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            try
            {
                string command = "SELECT " +
                    "id " +
                    "FROM " +
                    table + " " +
                    "WHERE " +
                    "MONTH(FechaEmision) = '" + monthNum + "' " +
                    "AND YEAR(FechaEmision) = (SELECT YEAR(NOW())) " +
                    "AND status = 1; ";
                resaux = cpd.RetornoMenosExistencias(command).ToString();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "Error al Cargar los Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (resaux == "") res = 0;
            else res = Convert.ToDouble(resaux);
            return res;
        }

        void CargarRelacionVentasCompras()
        {
            ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            try
            {
                double ventas = ExtraerValor(GetPreviousMonth(0).Month.ToString(), "tbl_ventasencabezado");
                double compras = ExtraerValor(GetPreviousMonth(0).Month.ToString(), "tbl_comprasencabezado");
                labelResultado.Text = "Q" + ((ventas - compras).ToString());
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "Error al Cargar los Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Funciones

        public void CargarGrafica1()
        {
            //Extraer Los meses
            string[] monthNumber = new string[5];
            monthNumber[0] = GetPreviousMonth(-4).Month.ToString();
            monthNumber[1] = GetPreviousMonth(-3).Month.ToString();
            monthNumber[2] = GetPreviousMonth(-2).Month.ToString();
            monthNumber[3] = GetPreviousMonth(-1).Month.ToString();
            monthNumber[4] = GetPreviousMonth(0).Month.ToString();

            //Extrare Los meses [nombres]
            string[] monthName = new string[5];
            monthName[0] = GetMonthByNumber(monthNumber[0]);
            monthName[1] = GetMonthByNumber(monthNumber[1]);
            monthName[2] = GetMonthByNumber(monthNumber[2]);
            monthName[3] = GetMonthByNumber(monthNumber[3]);
            monthName[4] = GetMonthByNumber(monthNumber[4]);

            //Cargar comprasMes
            double[] buyMonth = new double[5];
            buyMonth[0] = ExtraerValor(monthNumber[0], "tbl_comprasencabezado");
            buyMonth[1] = ExtraerValor(monthNumber[1], "tbl_comprasencabezado");
            buyMonth[2] = ExtraerValor(monthNumber[2], "tbl_comprasencabezado");
            buyMonth[3] = ExtraerValor(monthNumber[3], "tbl_comprasencabezado");
            buyMonth[4] = ExtraerValor(monthNumber[4], "tbl_comprasencabezado");

            //Cargar ventasMes
            double[] sellMonth = new double[5];
            sellMonth[0] = ExtraerValor(monthNumber[0], "tbl_ventasencabezado");
            sellMonth[1] = ExtraerValor(monthNumber[1], "tbl_ventasencabezado");
            sellMonth[2] = ExtraerValor(monthNumber[2], "tbl_ventasencabezado");
            sellMonth[3] = ExtraerValor(monthNumber[3], "tbl_ventasencabezado");
            sellMonth[4] = ExtraerValor(monthNumber[4], "tbl_ventasencabezado");

            cartesianChart1.Series = new SeriesCollection
            {
                new StackedColumnSeries
                {
                    //Compras
                    
                    Name = "COMPRAS",
                    Title = "COMPRAS",
                    Values = new ChartValues<double> {buyMonth[0], buyMonth[1], buyMonth[2], buyMonth[3], buyMonth[4],},
                    Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(63, 97, 140)),
                    Stroke = new SolidColorBrush(System.Windows.Media.Color.FromRgb(63, 97, 140)),
                    DataLabels = true
                },
                new StackedColumnSeries
                {
                    //Ventas
                    Name = "VENTAS",
                    Title = "VENTAS",
                    Values = new ChartValues<double> { sellMonth[0], sellMonth[1], sellMonth[2], sellMonth[3], sellMonth[4],},
                    Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(163, 191, 217)),
                    Stroke = new SolidColorBrush(System.Windows.Media.Color.FromRgb(163, 191, 217)),
                    DataLabels = true
                }
            };

            cartesianChart1.AxisX.Add(new Axis
            {
                Title = "MES",
                Labels = new[] { monthName[0], monthName[1], monthName[2], monthName[3], monthName[4], },
                Separator = DefaultAxes.CleanSeparator
            });

            cartesianChart1.AxisY.Add(new Axis
            {
                Title = "VALOR",
                LabelFormatter = value => "Q " + value
            });
        }

        void CargarSegundaGrafica()
        {
            //Extraer Los meses
            string[] monthNumber = new string[5];
            monthNumber[0] = GetPreviousMonth(-4).Month.ToString();
            monthNumber[1] = GetPreviousMonth(-3).Month.ToString();
            monthNumber[2] = GetPreviousMonth(-2).Month.ToString();
            monthNumber[3] = GetPreviousMonth(-1).Month.ToString();
            monthNumber[4] = GetPreviousMonth(0).Month.ToString();

            //Extrare Los meses [nombres]
            string[] monthName = new string[5];
            monthName[0] = GetMonthByNumber(monthNumber[0]);
            monthName[1] = GetMonthByNumber(monthNumber[1]);
            monthName[2] = GetMonthByNumber(monthNumber[2]);
            monthName[3] = GetMonthByNumber(monthNumber[3]);
            monthName[4] = GetMonthByNumber(monthNumber[4]);

            //Cargar comprasMes
            double[] devMonth = new double[5];
            devMonth[0] = ExtraerValorDevoluciones(monthNumber[0], "tbl_devolucionencabezado");
            devMonth[1] = ExtraerValorDevoluciones(monthNumber[1], "tbl_devolucionencabezado");
            devMonth[2] = ExtraerValorDevoluciones(monthNumber[2], "tbl_devolucionencabezado");
            devMonth[3] = ExtraerValorDevoluciones(monthNumber[3], "tbl_devolucionencabezado");
            devMonth[4] = ExtraerValorDevoluciones(monthNumber[4], "tbl_devolucionencabezado");

            labelTotalCantidad.Text = devMonth[4].ToString();
            double resultado = devMonth[4] - devMonth[3];
            if(resultado <= 0)
            {
                labelTotalMinus.ForeColor = Color.Green;
                labelTotalMinus.Text = resultado.ToString();
            }
            else
            {
                labelTotalMinus.ForeColor = Color.Red;
                labelTotalMinus.Text = "+";
                labelTotalMinus.Text += resultado.ToString();
            }

            cartesianChart2.Series = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "DEVOLUCIONES TOTALES",
                    Values = new ChartValues<double> { devMonth[0], devMonth[1], devMonth[2], devMonth[3], devMonth[4] },
                    LineSmoothness = 0.3,
                    Fill = new SolidColorBrush(System.Windows.Media.Color.FromArgb(100, 242, 75, 75)),
                    PointForeground = System.Windows.Media.Brushes.Red,
                    Stroke = new SolidColorBrush(System.Windows.Media.Color.FromRgb(166, 28, 28)),

                }
            };

            cartesianChart2.AxisX.Add(new Axis
            {
                Title = "MES",
                Labels = new[] { monthName[0], monthName[1], monthName[2], monthName[3], monthName[4], },
                Separator = DefaultAxes.CleanSeparator
            });

            cartesianChart2.AxisY.Add(new Axis
            {
                Title = "CANTIDAD"
            });
        }
         
        ////////////////////////////////// PROCESOS TERCER GRAFICA

        string GetIDProducto(string numeroPosicion)
        {
            string id = "";
            ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            try
            {
                id = cpd.ExtraerCodigo3("CAN, PRODUCTO as ","NombreC",
                    "( " +
                    "    SELECT PRODUCTO, MAX(CANTIDAD) as CAN " +
                    "    FROM " +
                    "    ( " +
                    "        SELECT distinct " +
                    "        tbl_producto.id as 'PRODUCTO', SUM(tbl_ventasdetalle.Cantidad) as CANTIDAD " +
                    "        FROM " +
                    "        tbl_producto, tbl_ventasdetalle, tbl_movimientoinventario " 
                    ,
                    "        tbl_ventasdetalle.tbl_Producto_id "," tbl_Producto.id " +
                    "        AND tbl_movimientoinventario.id = tbl_ventasdetalle.tbl_movimientoinventario_id " +
                    "        group by PRODUCTO " +
                    "        order by CANTIDAD DESC " +
                    "        limit 10 " +
                    "    ) " +
                    "    a GROUP by PRODUCTO " +
                    ") " +
                    "a GROUP by PRODUCTO "+
                    "order by CAN DESC " +
                    "limit " + numeroPosicion + ", 1 ");
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "Error al Cargar los Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return id;
        }

        double GetCantidadVendidaPorProducto(string codProducto)
        {
            string cant = "";
            if (codProducto != "")
            {
                ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
                try
                {
                    cant = cpd.ExtraerCodigo3("SUM(tbl_ventasdetalle.Cantidad) as ", "CANTIDAD",
                        "tbl_producto, tbl_ventasdetalle, tbl_movimientoinventario ",
                        "tbl_ventasdetalle.tbl_Producto_id",
                        "tbl_Producto.id " +
                        "AND tbl_ventasdetalle.tbl_Producto_id = " + codProducto + " " +
                        "AND tbl_movimientoinventario.id = tbl_ventasdetalle.tbl_movimientoinventario_id");
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.ToString(), "Error al Cargar los Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            double res = 0;
            if (cant == "") res = 0;
            else res = Convert.ToDouble(cant);
            return  res;
        }

        string GetNameProducto(string codProduct)
        {
            string name = "";
            if (codProduct != "")
            {
                ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
                try
                {
                    name = cpd.ExtraerCodigo2("Nombre",
                        "tbl_producto",
                        "id", codProduct);
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
            string[] productID = new string[10];
            productID[9] = GetIDProducto("0");
            productID[8] = GetIDProducto("1");
            productID[7] = GetIDProducto("2");
            productID[6] = GetIDProducto("3");
            productID[5] = GetIDProducto("4");
            productID[4] = GetIDProducto("5");
            productID[3] = GetIDProducto("6");
            productID[2] = GetIDProducto("7");
            productID[1] = GetIDProducto("8");
            productID[0] = GetIDProducto("9");

            double[] productoCantidad = new double[10];
            productoCantidad[0] = GetCantidadVendidaPorProducto(productID[0]);
            productoCantidad[1] = GetCantidadVendidaPorProducto(productID[1]);
            productoCantidad[2] = GetCantidadVendidaPorProducto(productID[2]);
            productoCantidad[3] = GetCantidadVendidaPorProducto(productID[3]);
            productoCantidad[4] = GetCantidadVendidaPorProducto(productID[4]);
            productoCantidad[5] = GetCantidadVendidaPorProducto(productID[5]);
            productoCantidad[6] = GetCantidadVendidaPorProducto(productID[6]);
            productoCantidad[7] = GetCantidadVendidaPorProducto(productID[7]);
            productoCantidad[8] = GetCantidadVendidaPorProducto(productID[8]);
            productoCantidad[9] = GetCantidadVendidaPorProducto(productID[9]);

            string[] productName = new string[10];
            productName[0] = GetNameProducto(productID[0]);
            productName[1] = GetNameProducto(productID[1]);
            productName[2] = GetNameProducto(productID[2]);
            productName[3] = GetNameProducto(productID[3]);
            productName[4] = GetNameProducto(productID[4]);
            productName[5] = GetNameProducto(productID[5]);
            productName[6] = GetNameProducto(productID[6]);
            productName[7] = GetNameProducto(productID[7]);
            productName[8] = GetNameProducto(productID[8]);
            productName[9] = GetNameProducto(productID[9]);

            cartesianChart3.Series = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "CANTIDAD VENDIDA",
                    Values = new ChartValues<double> { productoCantidad[0], productoCantidad[1], productoCantidad[2], productoCantidad[3], productoCantidad[4],
                         productoCantidad[5], productoCantidad[6], productoCantidad[7], productoCantidad[8], productoCantidad[9]
                    },
                    Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(250, 188, 8)),
                    Stroke = new SolidColorBrush(System.Windows.Media.Color.FromRgb(250, 188, 8)),

                }
            };

            cartesianChart3.AxisX.Add(new Axis
            {
                Title = "PRODUCTO",
                Labels = new[] { productName[0], productName[1], productName[2], productName[3], productName[4],
                         productName[5], productName[6], productName[7], productName[8], productName[9]
                    },
                Separator = DefaultAxes.CleanSeparator
            });

            cartesianChart3.AxisY.Add(new Axis
            {
                Title = "CANTIDAD"
            });
        }
    }
}
