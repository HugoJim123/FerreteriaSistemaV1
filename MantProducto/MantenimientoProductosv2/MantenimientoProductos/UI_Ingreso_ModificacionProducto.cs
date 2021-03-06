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
using System.IO;

namespace MantenimientoProductos
{
    public partial class UI_Ingreso_ModificacionProducto : Form
    {
        public int tipoMovimiento = 0;
        public string codigo = "";
        public UI_Ingreso_ModificacionProducto(int tipoMovimiento2, string codigo2)              // Tipo movimiento 0: Ingreso, 1: Modificacion
        {
            InitializeComponent();
            tipoMovimiento = tipoMovimiento2;
            codigo = codigo2;
            LoadForm();
        }

        void LoadForm()
        {
            comboBoxMaterial.Items.Clear();
            comboBoxTipo.Items.Clear();
            comboBoxPais.Items.Clear();
            comboBoxMarca.Items.Clear();
            comboBoxColor.Items.Clear();
            CapaDatosFerreteria.ClaseCapaDatosFerreteria cpd = new CapaDatosFerreteria.ClaseCapaDatosFerreteria();
            cpd.cargarDatosACombobox(comboBoxMaterial, "DescripcionMaterial", "tbl_Material WHERE status = 1");
            cpd.cargarDatosACombobox(comboBoxTipo, "DescripcionTipo", "tbl_Tipo WHERE status = 1");
            cpd.cargarDatosACombobox(comboBoxPais, "DescripcionPais", "tbl_Pais WHERE status = 1");
            cpd.cargarDatosACombobox(comboBoxMarca, "DescripcionMarca", "tbl_Marca WHERE status = 1");
            cpd.cargarDatosACombobox(comboBoxColor, "DescripcionColor", "tbl_ColorProducto WHERE status = 1");
            if (tipoMovimiento == 1)
            {
                cargardatos(codigo);
                panelProductoAModificar.Visible = true;
                buttonGuardar.Text = "MODIFICAR";
            }
            else
            {
                panelProductoAModificar.Visible = false;
                buttonGuardar.Text = "INSERTAR";
            }
        }

        /// <summary>
        /// Carga los datos en caso de ser una modificacion de producto
        /// </summary>
        /// <param name="codigoACambiar"></param>
        void cargardatos(string codigoACambiar)
        {
            ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            string[] datos;
            datos = new string[18];
            try
            {
                datos = cpd.cargarDetallesProductos(codigoACambiar);
                textBoxNombre.Text = datos[1];
                numericUpDownVoltaje.Text = datos[2] + " V";
                numericUpDownAmperaje.Text = datos[3] + " A";
                numericUpDownCapacitancia.Text = datos[4] + " C";
                numericUpDownResistencia.Text = datos[5] + " R";
                numericUpDownTamano.Text = datos[6] + " cm";
                numericUpDownDiametro.Text = datos[7] + " cm";
                numericUpDownFuerza.Text = datos[8] + " N";
                numericUpDownTemperatura.Text = datos[9] + " °C";
                numericUpDownRPM.Text = datos[10];
                numericUpDownHz.Text = datos[11];
                numericUpDownPrecio.Text = "Q " + datos[12];
                comboBoxMaterial.SelectedItem = datos[13];
                comboBoxTipo.SelectedItem = datos[14];
                comboBoxPais.SelectedItem = datos[15];
                comboBoxMarca.SelectedItem = datos[16];
                comboBoxColor.SelectedItem = datos[17];
                textBoxObservaciones.Text = datos[18];
                textBoxImagen.Text = datos[19];
                ///     Carga del detalle del producto (Lo que no se va a modificar)
                labelCodigoDetalle.Text = datos[0];
                labelNombreDetalle.Text = datos[1];
                labelMaterialDetalle.Text = datos[13];
                labelTipoDetalle.Text = datos[14];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR AL CARGAR DATOS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonAddMaintenance_Click(object sender, EventArgs e)     // Button general para agregar datos, se utiliza un switch para hacer mas peque;o el codigo
        {
            String btn = (sender as Button).Name.ToString();
            String btntype = "";
            switch (btn)
            {
                case "buttonAddMaterial":
                    btntype = "Material";
                    break;
                case "buttonAddTipo":
                    btntype = "Tipo";
                    break;
                case "buttonAddPais":
                    btntype = "Pais";
                    break;
                case "buttonAddMarca":
                    btntype = "Marca";
                    break;
                case "buttonAddColor":
                    btntype = "Color";
                    break;
                default:
                    btntype = "Material";
                    break;
            }
            UI_MantenimientosVarios form = new UI_MantenimientosVarios(btntype);
            form.ShowDialog();
            LoadForm();
        }

        private void textBoxNombre_TextChanged(object sender, EventArgs e)
        {
            if((sender as TextBox).Text != "")
            {
                buttonGuardar.Enabled = true;
                (buttonGuardar).BackColor = Color.FromArgb(49, 156, 202);
            }
            else
            {
                buttonGuardar.Enabled = false;
                (buttonGuardar).BackColor = Color.FromArgb(181, 186, 202);
            }
        }

        /// <summary>
        /// Retorna el valor del NumericUpDown
        /// </summary>
        /// <param name="nud">NumericUpDown a evaluar</param>
        /// <returns></returns>
        public double valorAIngresar(NumericUpDown nud)
        {
            return Convert.ToDouble(nud.Value);
        }

        /// <summary>
        /// Ingreso de codigos
        /// </summary>
        /// <param name="cmb">Combobox a Guardar</param>
        /// <returns></returns>
        public int valorAIngresarNumero(ComboBox cmb, String Tabla, String detalle)
        {
            ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            return Convert.ToInt32(cpd.ExtraerCodigo("id",Tabla,detalle,cmb.Text));
        }


        /// <summary>
        /// Funcion que nos permite crear la carpeta con imagenes
        /// </summary>
        public void CrearCarpetaImagenes()
        {
            string carpeta = @"C:\Ferreteria\Imagenes";
            try
            {
                if (!(Directory.Exists(carpeta)))
                {
                    Directory.CreateDirectory(carpeta);
                }
            }
            catch (Exception errorC)
            {
                MessageBox.Show("Error ocurrido al tratar de crear la carpeta: "+errorC.ToString(), "Error al cargar imagen", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        /// <summary>
        /// Genera una copia en el disco C:
        /// </summary>
        public void guardarCopiaImagen(string filename)
        {
            if (textBoxImagen.Text != "")
            {
                CrearCarpetaImagenes();
                string nuevaRuta = Path.Combine(@"C:\Ferreteria\Imagenes", filename);

                if (!File.Exists(nuevaRuta))
                {
                    File.Copy(@textBoxImagen.Text, nuevaRuta);
                }
                else
                {
                    //MessageBox.Show("La ruta de destino ya contiene un archivo con el mismo nombre.", "Error al guardar imagen", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonGuardar_Click(object sender, EventArgs e)        /// Ingreso y modifica de datos de producto
        {
            ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            if(tipoMovimiento == 0)                                         // Ingreso de producto                 
            {
                string nameImage = Path.GetFileName(textBoxImagen.Text);
                guardarCopiaImagen(nameImage);
                string rutaImagen ="";
                if (textBoxImagen.Text != "")
                {
                    rutaImagen = @"C:\\Ferreteria\\Imagenes\\" + nameImage;
                }
                try
                {
                    cpd.InsertarDatos("tbl_Producto", "0,'" +
                        textBoxNombre.Text + "'," +
                        valorAIngresar(numericUpDownVoltaje) + "," +
                        valorAIngresar(numericUpDownAmperaje) + "," +
                        valorAIngresar(numericUpDownCapacitancia) + "," +
                        valorAIngresar(numericUpDownResistencia) + "," +
                        valorAIngresar(numericUpDownTamano) + "," +
                        valorAIngresar(numericUpDownDiametro) + "," +
                        valorAIngresar(numericUpDownFuerza) + "," +
                        valorAIngresar(numericUpDownTemperatura) + "," +
                        valorAIngresar(numericUpDownRPM) + "," +
                        valorAIngresar(numericUpDownHz) + "," +
                        valorAIngresar(numericUpDownPrecio) + ",'" +
                        textBoxObservaciones.Text + "'," +
                        valorAIngresarNumero(comboBoxMaterial, "tbl_Material", "DescripcionMaterial") + "," +
                        valorAIngresarNumero(comboBoxTipo, "tbl_Tipo", "DescripcionTipo") + "," +
                        valorAIngresarNumero(comboBoxPais, "tbl_Pais", "DescripcionPais") + "," +
                        valorAIngresarNumero(comboBoxMarca, "tbl_Marca", "DescripcionMarca") + "," +
                        valorAIngresarNumero(comboBoxColor, "tbl_ColorProducto", "DescripcionColor") + ", '" +
                        rutaImagen + "', " +
                        "1");
                    MessageBox.Show("Datos Guardados Exitosamente", "Datos Guardados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Limpiar();
                }catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Error al Guardar Los Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }else if(tipoMovimiento == 1)                                   // Modificacion de productos
            {
                string nameImage = Path.GetFileName(textBoxImagen.Text);
                guardarCopiaImagen(nameImage);
                string rutaImagen = "";
                if (textBoxImagen.Text != "")
                {
                    rutaImagen = @"C:\\Ferreteria\\Imagenes\\" + nameImage;
                }
                try
                {
                    cpd.ModificarDatos("tbl_Producto", " " +
                        "Nombre = '" + textBoxNombre.Text + "'," +
                        "Voltaje = " + valorAIngresar(numericUpDownVoltaje) + "," +
                        "Amperaje = " + valorAIngresar(numericUpDownAmperaje) + "," +
                        "Capacitancia = " + valorAIngresar(numericUpDownCapacitancia) + "," +
                        "Resistencia = " + valorAIngresar(numericUpDownResistencia) + "," +
                        "Tamaño = " + valorAIngresar(numericUpDownTamano) + "," +
                        "Diametro = " + valorAIngresar(numericUpDownDiametro) + "," +
                        "Fuerza = " + valorAIngresar(numericUpDownFuerza) + "," +
                        "Temperatura = " + valorAIngresar(numericUpDownTemperatura) + "," +
                        "RPM = " + valorAIngresar(numericUpDownRPM) + "," +
                        "Hz = " + valorAIngresar(numericUpDownHz) + "," +
                        "Precio = " + valorAIngresar(numericUpDownPrecio) + "," +
                        "Observaciones = '" + textBoxObservaciones.Text + "'," +
                        "tbl_Material_id = " + valorAIngresarNumero(comboBoxMaterial, "tbl_Material", "DescripcionMaterial") + "," +
                        "tbl_Tipo_id = " + valorAIngresarNumero(comboBoxTipo, "tbl_Tipo", "DescripcionTipo") + "," +
                        "tbl_Pais_id = " + valorAIngresarNumero(comboBoxPais, "tbl_Pais", "DescripcionPais") + "," +
                        "tbl_Marca_id = " + valorAIngresarNumero(comboBoxMarca, "tbl_Marca", "DescripcionMarca") + "," +
                        "tbl_ColorProducto_id = " + valorAIngresarNumero(comboBoxColor, "tbl_ColorProducto", "DescripcionColor") + ", " +
                        "image_path = '" + rutaImagen + "'", "" +
                        "id", codigo);
                    MessageBox.Show("Datos Modificados Exitosamente", "Datos Modificados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Error al Modificar Los Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Funcion que limpia los datos del formulario
        /// </summary>
        void Limpiar()
        {
            textBoxImagen.Text = "";
            textBoxNombre.Text = "";
            numericUpDownVoltaje.Value = 0;
            numericUpDownAmperaje.Value = 0;
            numericUpDownCapacitancia.Value = 0;
            numericUpDownResistencia.Value = 0;
            numericUpDownTamano.Value = 0;
            numericUpDownDiametro.Value = 0;
            numericUpDownFuerza.Value = 0;
            numericUpDownTemperatura.Value = 0;
            numericUpDownRPM.Value = 0;
            numericUpDownHz.Value = 0;
            numericUpDownPrecio.Value = 0;
            textBoxObservaciones.Text = "";
            comboBoxMaterial.SelectedIndex = 0;
            comboBoxTipo.SelectedIndex = 0;
            comboBoxPais.SelectedIndex = 0;
            comboBoxMarca.SelectedIndex = 0;
            comboBoxColor.SelectedIndex = 0;
        }

        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            /*string filename = textBoxImagen.Text;
            string result = Path.GetFileName(filename);
            MessageBox.Show(result);*/
            Limpiar();
        }

        private void buttonCargarImagen_Click(object sender, EventArgs e)
        {
            OpenFileDialog buscar = new OpenFileDialog();
            if (buscar.ShowDialog() == DialogResult.OK)
            {
                textBoxImagen.Text = buscar.FileName;
            }
        }

        private void textBoxImagen_TextChanged(object sender, EventArgs e)
        {
           //textBoxImagen.Text = textBoxImagen.Text.Replace("\\","\\\\");
        }
    }
}
