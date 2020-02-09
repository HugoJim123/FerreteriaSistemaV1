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
    public partial class UI_GraficasCompras : Form
    {
        public UI_GraficasCompras()
        {
            InitializeComponent();
            CargarGraficaPrincipal();
            CargarGraficaDonut();
        }


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
        double ExtraerValor(string monthNum, string table, string campo)
        {
            double res = 0;
            string resaux = "0";
            ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            try
            {
                resaux = cpd.ExtraerCodigo3("SUM("+ campo + ") AS ", campo,
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


        void CargarGraficaPrincipal()
        {
            //Extraer Los meses
            string[] monthNumber = new string[10];
            monthNumber[0] = GetPreviousMonth(-9).Month.ToString();
            monthNumber[1] = GetPreviousMonth(-8).Month.ToString();
            monthNumber[2] = GetPreviousMonth(-7).Month.ToString();
            monthNumber[3] = GetPreviousMonth(-6).Month.ToString();
            monthNumber[4] = GetPreviousMonth(-5).Month.ToString();
            monthNumber[5] = GetPreviousMonth(-4).Month.ToString();
            monthNumber[6] = GetPreviousMonth(-3).Month.ToString();
            monthNumber[7] = GetPreviousMonth(-2).Month.ToString();
            monthNumber[8] = GetPreviousMonth(-1).Month.ToString();
            monthNumber[9] = GetPreviousMonth(0).Month.ToString();

            //Extrare Los meses [nombres]
            string[] monthName = new string[10];
            monthName[0] = GetMonthByNumber(monthNumber[0]);
            monthName[1] = GetMonthByNumber(monthNumber[1]);
            monthName[2] = GetMonthByNumber(monthNumber[2]);
            monthName[3] = GetMonthByNumber(monthNumber[3]);
            monthName[4] = GetMonthByNumber(monthNumber[4]);
            monthName[5] = GetMonthByNumber(monthNumber[5]);
            monthName[6] = GetMonthByNumber(monthNumber[6]);
            monthName[7] = GetMonthByNumber(monthNumber[7]);
            monthName[8] = GetMonthByNumber(monthNumber[8]);
            monthName[9] = GetMonthByNumber(monthNumber[9]);

            //Cargar comprasMes
            double[] sellMonth = new double[10];
            sellMonth[0] = ExtraerValor(monthNumber[0], "tbl_comprasencabezado", "Total");
            sellMonth[1] = ExtraerValor(monthNumber[1], "tbl_comprasencabezado", "Total");
            sellMonth[3] = ExtraerValor(monthNumber[3], "tbl_comprasencabezado", "Total");
            sellMonth[4] = ExtraerValor(monthNumber[4], "tbl_comprasencabezado", "Total");
            sellMonth[5] = ExtraerValor(monthNumber[5], "tbl_comprasencabezado", "Total");
            sellMonth[6] = ExtraerValor(monthNumber[6], "tbl_comprasencabezado", "Total");
            sellMonth[7] = ExtraerValor(monthNumber[7], "tbl_comprasencabezado", "Total");
            sellMonth[8] = ExtraerValor(monthNumber[8], "tbl_comprasencabezado", "Total");
            sellMonth[9] = ExtraerValor(monthNumber[9], "tbl_comprasencabezado", "Total");

            double mesActual = 0;
            double mesAnterior = 0;

            mesActual = ExtraerValor(monthNumber[9] + "' AND Cancelado = '1", "tbl_comprasencabezado", "Total");
            mesAnterior = ExtraerValor(monthNumber[0] + "' AND Cancelado = '1", "tbl_comprasencabezado", "Total"); 

            double resultado2 = mesActual - mesAnterior;

            if (resultado2 <= 0)
            {
                /////
                labelVentaMensualConcretada.Text = "Q" + resultado2.ToString();
                labelTotalMesualConcretada.Text = "Q" + mesActual;
                panelMenus2.Visible = true;
                panelMas2.Visible = false;
            }
            else
            {
                /////
                labelVentaMensualConcretada.Text = "Q" + resultado2.ToString();
                labelTotalMesualConcretada.Text = "Q" + mesActual;
                panelMenus2.Visible = false;
                panelMas2.Visible = true;
            }

            labelTotalCantidad.Text = sellMonth[9].ToString();
            double resultado = sellMonth[9] - sellMonth[8];
            if (resultado <= 0)
            {
                labelTotalMinus.ForeColor = Color.Red;
                labelTotalMinus.Text = resultado.ToString();
                /////
                labelTotalVentaMensualTotal.Text = "Q" + resultado.ToString();
                labelTotalMensualTotal.Text = "Q" + sellMonth[9];
                panelMenus1.Visible = true;
                panelMas1.Visible = false;
            }
            else
            {
                labelTotalMinus.ForeColor = Color.Green;
                labelTotalMinus.Text = "+";
                labelTotalMinus.Text += resultado.ToString();

                ////////////////////////////
                labelTotalMensualTotal.Text = "Q" + sellMonth[9];
                labelTotalVentaMensualTotal.Text = "Q" + resultado.ToString();
                panelMenus1.Visible = false;
                panelMas1.Visible = true;

                //


            }

            cartesianChart2.Series = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "COMPRAS TOTALES",
                    Values = new ChartValues<double> { sellMonth[0], sellMonth[1], sellMonth[2], sellMonth[3], sellMonth[4],
                    sellMonth[5], sellMonth[6], sellMonth[7], sellMonth[8], sellMonth[9]},
                    LineSmoothness = 0.3,
                    Fill = new SolidColorBrush(System.Windows.Media.Color.FromArgb(100, 71, 61, 168)),
                    PointForeground = System.Windows.Media.Brushes.Red,
                    Stroke = new SolidColorBrush(System.Windows.Media.Color.FromRgb(71, 61, 168)),

                }
            };

            cartesianChart2.AxisX.Add(new Axis
            {
                Title = "MES",
                Labels = new[] { monthName[0], monthName[1], monthName[2], monthName[3], monthName[4],
                monthName[5], monthName[6], monthName[7], monthName[8], monthName[9]},
                Separator = DefaultAxes.CleanSeparator
            });

            cartesianChart2.AxisY.Add(new Axis
            {
                Title = "CANTIDAD"
            });
        }


        /////////////////////////////////// CARGAR SEGUNDA GRAFICA
        ///

        void CargarGraficaDonut()
        {
            string monthNumber = GetPreviousMonth(0).Month.ToString();

            string monthName = GetMonthByNumber(monthNumber);

            double[] monthCant = new double[2];
            monthCant[0] = ExtraerValor(monthNumber, "tbl_comprasencabezado", "Total");
            monthCant[1] = ExtraerValor(monthNumber , "tbl_comprasencabezado", "Saldo");
            pieChart1.LegendLocation = LegendLocation.Right;

            pieChart1.Series = new SeriesCollection
            {
            new PieSeries
                {
                    Title = "Compras Faltantes",
                    Values = new ChartValues<double> {monthCant[0] - monthCant[1]},
                    PushOut = 15,
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "Compras Canceladas",
                    Values = new ChartValues<double> { monthCant[1]},
                    DataLabels = true
                }
            };
        }
    }
}
