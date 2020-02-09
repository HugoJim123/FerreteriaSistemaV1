using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using CapaDatosFerreteria;

namespace MantenimientoProductos
{
    public partial class UI_IngresoModifiacionProductosV2 : Form
    {
        public UI_IngresoModifiacionProductosV2(int tipoMovimiento2, string codigo2)
        {
            InitializeComponent();
            tipoMovimiento = tipoMovimiento2;
            codigo = codigo2;
            LoadForm();
        }
        // Variables
        public int tipoMovimiento = 0;
        public string codigo = "";
        public string path_Image = "";
        // Variables

        //Funciones Varias

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
                cargarImagenAPicturebox();
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
            datos = cpd.cargarDetallesProductos(codigoACambiar, "Vista de detalles de productos");
            textBoxNombre.Text = datos[1];
            numericUpDownVoltaje.Text = datos[2];
            numericUpDownAmperaje.Text = datos[3];
            numericUpDownCapacitancia.Text = datos[4];
            numericUpDownResistencia.Text = datos[5];
            numericUpDownTamano.Text = datos[6];
            numericUpDownDiametro.Text = datos[7];
            numericUpDownFuerza.Text = datos[8];
            numericUpDownTemperatura.Text = datos[9] ;
            numericUpDownRPM.Text = datos[10];
            numericUpDownHz.Text = datos[11];
            numericUpDownPrecioVenta.Text = datos[12];
            numericUpDownPrecioCompra.Text = datos[20];
            comboBoxMaterial.SelectedItem = datos[13];
            comboBoxTipo.SelectedItem = datos[14];
            comboBoxPais.SelectedItem = datos[15];
            comboBoxMarca.SelectedItem = datos[16];
            comboBoxColor.SelectedItem = datos[17];
            textBoxObservaciones.Text = datos[18];
            path_Image = datos[19];
            ///     Carga del detalle del producto (Lo que no se va a modificar)
            labelCodigoDetalle.Text = datos[0];
            labelNombreDetalle.Text = datos[1];
            labelMaterialDetalle.Text = datos[13];
            labelTipoDetalle.Text = datos[14];
        }

        /// <summary>
        /// Retorna el valor del NumericUpDown
        /// </summary>
        /// <param name="nud">NumericUpDown a evaluar</param>
        /// <returns></returns>
        /*public double valorAIngresar(TextBox nud)
        {
            return Convert.ToDouble(nud.Text);
        }*/


        public double valorAIngresar2(NumericUpDown nud)
        {
            if (nud.Value >= 0)
                return Convert.ToDouble(nud.Value);
            else
                return 0;
        }

        /// <summary>
        /// Ingreso de codigos
        /// </summary>
        /// <param name="cmb">Combobox a Guardar</param>
        /// <returns></returns>
        public int valorAIngresarNumero(ComboBox cmb, String Tabla, String detalle)
        {
            ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            return Convert.ToInt32(cpd.ExtraerCodigo("id", Tabla, detalle, cmb.Text));
        }


        /// <summary>
        /// Funcion que nos permite crear la carpeta con imagenes
        /// </summary>
        public void CrearCarpetaImagenes()
        {
            string carpeta = @"C:\Ferreteria\Imagenes\Productos";
            try
            {
                if (!(Directory.Exists(carpeta)))
                {
                    Directory.CreateDirectory(carpeta);
                }
            }
            catch (Exception errorC)
            {
                MessageBox.Show("Error ocurrido al tratar de crear la carpeta: " + errorC.ToString(), "Error al cargar imagen", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        /// <summary>
        /// Genera una copia en el disco C:
        /// </summary>
        public void guardarCopiaImagen(string filename)
        {
            if (path_Image != "")
            {
                CrearCarpetaImagenes();
                string nuevaRuta = Path.Combine(@"C:\Ferreteria\Imagenes\Productos", filename);

                if (!File.Exists(nuevaRuta))
                {
                    File.Copy(@path_Image, nuevaRuta);
                }
                else
                {
                    //MessageBox.Show("La ruta de destino ya contiene un archivo con el mismo nombre.", "Error al guardar imagen", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Funcion que limpia los datos del formulario
        /// </summary>
        void Limpiar()
        {
            path_Image = "";
            textBoxNombre.Text = "";
            numericUpDownVoltaje.Text = "0";
            numericUpDownAmperaje.Text = "0";
            numericUpDownCapacitancia.Text = "0";
            numericUpDownResistencia.Text = "0";
            numericUpDownTamano.Text = "0";
            numericUpDownDiametro.Text = "0";
            numericUpDownFuerza.Text = "0";
            numericUpDownTemperatura.Text = "0";
            numericUpDownRPM.Text = "0";
            numericUpDownHz.Text = "0";
            numericUpDownPrecioVenta.Value = 0;
            numericUpDownPrecioCompra.Value = 0;
            textBoxObservaciones.Text = "";
            comboBoxMaterial.SelectedIndex = 0;
            comboBoxTipo.SelectedIndex = 0;
            comboBoxPais.SelectedIndex = 0;
            comboBoxMarca.SelectedIndex = 0;
            comboBoxColor.SelectedIndex = 0;
        }

        void cargarImagenAPicturebox()
        {
            if(path_Image != "")
            {
                pictureBox1.BackgroundImage = Image.FromFile(path_Image);
            }
            else
            {
                pictureBox1.BackgroundImage = pictureBox1.ErrorImage;
            }
        }


        //Funciones Varias



        private void buttonAddMaintenance_Click(object sender, EventArgs e)     // Button general para agregar datos, se utiliza un switch para hacer mas peque;o el codigo
        {
            CapaDatosFerreteria.ClaseCapaDatosFerreteria cpd = new CapaDatosFerreteria.ClaseCapaDatosFerreteria();
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
            switch (btn)
            {
                case "buttonAddMaterial":
                    comboBoxMaterial.Items.Clear();
                    cpd.cargarDatosACombobox(comboBoxMaterial, "DescripcionMaterial", "tbl_Material WHERE status = 1");
                    break;
                case "buttonAddTipo":
                    comboBoxTipo.Items.Clear();
                    cpd.cargarDatosACombobox(comboBoxTipo, "DescripcionTipo", "tbl_Tipo WHERE status = 1");
                    break;
                case "buttonAddPais":
                    comboBoxPais.Items.Clear();
                    cpd.cargarDatosACombobox(comboBoxPais, "DescripcionPais", "tbl_Pais WHERE status = 1");
                    break;
                case "buttonAddMarca":
                    comboBoxMarca.Items.Clear();
                    cpd.cargarDatosACombobox(comboBoxMarca, "DescripcionMarca", "tbl_Marca WHERE status = 1");
                    break;
                case "buttonAddColor":
                    comboBoxColor.Items.Clear();
                    cpd.cargarDatosACombobox(comboBoxColor, "DescripcionColor", "tbl_ColorProducto WHERE status = 1");
                    break;
                default:
                    comboBoxMaterial.Items.Clear();
                    cpd.cargarDatosACombobox(comboBoxMaterial, "DescripcionMaterial", "tbl_Material WHERE status = 1");
                    break;
            }
        }

        private void textBoxNombre_TextChanged(object sender, EventArgs e)
        {
            if ((sender as TextBox).Text != "")
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

        private void buttonGuardar_Click(object sender, EventArgs e)
        {
            ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            if (tipoMovimiento == 0)                                         // Ingreso de producto                 
            {
                string nameImage = Path.GetFileName(path_Image);
                guardarCopiaImagen(nameImage);
                string rutaImagen = "";
                if (path_Image != "")
                {
                    rutaImagen = @"C:\\Ferreteria\\Imagenes\\Productos\\" + nameImage;
                }
                try
                {
                    cpd.InsertDatos("tbl_Producto", "0,'" +
                        textBoxNombre.Text + "','" +
                        (numericUpDownVoltaje.Text) + "','" +
                        (numericUpDownAmperaje.Text) + "','" +
                        (numericUpDownCapacitancia.Text) + "','" +
                        (numericUpDownResistencia.Text) + "','" +
                        (numericUpDownTamano.Text) + "','" +
                        (numericUpDownDiametro.Text) + "','" +
                        (numericUpDownFuerza.Text) + "','" +
                        (numericUpDownTemperatura.Text) + "','" +
                        (numericUpDownRPM.Text) + "','" +
                        (numericUpDownHz.Text) + "'," +
                        valorAIngresar2(numericUpDownPrecioVenta) + ",'" +
                        textBoxObservaciones.Text + "'," +
                        valorAIngresarNumero(comboBoxMaterial, "tbl_Material", "DescripcionMaterial") + "," +
                        valorAIngresarNumero(comboBoxTipo, "tbl_Tipo", "DescripcionTipo") + "," +
                        valorAIngresarNumero(comboBoxPais, "tbl_Pais", "DescripcionPais") + "," +
                        valorAIngresarNumero(comboBoxMarca, "tbl_Marca", "DescripcionMarca") + "," +
                        valorAIngresarNumero(comboBoxColor, "tbl_ColorProducto", "DescripcionColor") + ", '" +
                        rutaImagen + "', " +
                        "1, "+
                        valorAIngresar2(numericUpDownPrecioCompra), 
                        "Insercion de datos de Productos");
                    MessageBox.Show("Datos Guardados Exitosamente", "Datos Guardados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Limpiar();
                    /*cpd.InsertDatos("tbl_Producto", "0,'" +
                        textBoxNombre.Text + "','" +
                        valorAIngresar(numericUpDownVoltaje) + "','" +
                        valorAIngresar(numericUpDownAmperaje) + "','" +
                        valorAIngresar(numericUpDownCapacitancia) + "','" +
                        valorAIngresar(numericUpDownResistencia) + "','" +
                        valorAIngresar(numericUpDownTamano) + "','" +
                        valorAIngresar(numericUpDownDiametro) + "','" +
                        valorAIngresar(numericUpDownFuerza) + "','" +
                        valorAIngresar(numericUpDownTemperatura) + "','" +
                        valorAIngresar(numericUpDownRPM) + "','" +
                        valorAIngresar(numericUpDownHz) + "'," +
                        valorAIngresar2(numericUpDownPrecioVenta) + ",'" +
                        textBoxObservaciones.Text + "'," +
                        valorAIngresarNumero(comboBoxMaterial, "tbl_Material", "DescripcionMaterial") + "," +
                        valorAIngresarNumero(comboBoxTipo, "tbl_Tipo", "DescripcionTipo") + "," +
                        valorAIngresarNumero(comboBoxPais, "tbl_Pais", "DescripcionPais") + "," +
                        valorAIngresarNumero(comboBoxMarca, "tbl_Marca", "DescripcionMarca") + "," +
                        valorAIngresarNumero(comboBoxColor, "tbl_ColorProducto", "DescripcionColor") + ", '" +
                        rutaImagen + "', " +
                        "1, "+
                        valorAIngresar2(numericUpDownPrecioCompra), 
                        "Insercion de datos de Productos");
                    MessageBox.Show("Datos Guardados Exitosamente", "Datos Guardados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Limpiar();*/
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Error al Guardar Los Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (tipoMovimiento == 1)                                   // Modificacion de productos
            {
                string nameImage = Path.GetFileName(path_Image);
                guardarCopiaImagen(nameImage);
                string rutaImagen = "";
                if (path_Image != "")
                {
                    rutaImagen = @"C:\\Ferreteria\\Imagenes\\Productos\\" + nameImage;
                }
                try
                {
                    cpd.ModificarDatos("tbl_Producto", " " +
                        "Nombre = '" + textBoxNombre.Text + "'," +
                        "Voltaje = '" + (numericUpDownVoltaje.Text) + "'," +
                        "Amperaje = '" + (numericUpDownAmperaje.Text) + "'," +
                        "Capacitancia = '" + (numericUpDownCapacitancia.Text) + "'," +
                        "Resistencia = '" + (numericUpDownResistencia.Text) + "'," +
                        "Tamaño = '" + (numericUpDownTamano.Text) + "'," +
                        "Diametro = '" + (numericUpDownDiametro.Text) + "'," +
                        "Fuerza = '" + (numericUpDownFuerza.Text) + "'," +
                        "Temperatura = '" + (numericUpDownTemperatura.Text) + "'," +
                        "RPM = '" + (numericUpDownRPM.Text) + "'," +
                        "Hz = '" + (numericUpDownHz.Text) + "'," +
                        "PrecioVenta = " + valorAIngresar2(numericUpDownPrecioVenta) + "," +
                        "PrecioCompra = " + valorAIngresar2(numericUpDownPrecioCompra) + "," +
                        "Observaciones = '" + textBoxObservaciones.Text + "'," +
                        "tbl_Material_id = " + valorAIngresarNumero(comboBoxMaterial, "tbl_Material", "DescripcionMaterial") + "," +
                        "tbl_Tipo_id = " + valorAIngresarNumero(comboBoxTipo, "tbl_Tipo", "DescripcionTipo") + "," +
                        "tbl_Pais_id = " + valorAIngresarNumero(comboBoxPais, "tbl_Pais", "DescripcionPais") + "," +
                        "tbl_Marca_id = " + valorAIngresarNumero(comboBoxMarca, "tbl_Marca", "DescripcionMarca") + "," +
                        "tbl_ColorProducto_id = " + valorAIngresarNumero(comboBoxColor, "tbl_ColorProducto", "DescripcionColor") + ", " +
                        "image_path = '" + rutaImagen + "'", "" +
                        "id", codigo, "Actualizacion de datos de productos");
                    MessageBox.Show("Datos Modificados Exitosamente", "Datos Modificados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    /*cpd.ModificarDatos("tbl_Producto", " " +
                        "Nombre = '" + textBoxNombre.Text + "'," +
                        "Voltaje = '" + valorAIngresar(numericUpDownVoltaje) + "'," +
                        "Amperaje = '" + valorAIngresar(numericUpDownAmperaje) + "'," +
                        "Capacitancia '= " + valorAIngresar(numericUpDownCapacitancia) + "'," +
                        "Resistencia '= " + valorAIngresar(numericUpDownResistencia) + "'," +
                        "Tamaño '= " + valorAIngresar(numericUpDownTamano) + "'," +
                        "Diametro '= " + valorAIngresar(numericUpDownDiametro) + "'," +
                        "Fuerza '= " + valorAIngresar(numericUpDownFuerza) + "'," +
                        "Temperatura '= " + valorAIngresar(numericUpDownTemperatura) + "'," +
                        "RPM '= " + valorAIngresar(numericUpDownRPM) + "'," +
                        "Hz '= " + valorAIngresar(numericUpDownHz) + "'," +
                        "PrecioVenta = " + valorAIngresar2(numericUpDownPrecioVenta) + "," +
                        "PrecioCompra = " + valorAIngresar2(numericUpDownPrecioCompra) + "," +
                        "Observaciones '= '" + textBoxObservaciones.Text + "'," +
                        "tbl_Material_id = " + valorAIngresarNumero(comboBoxMaterial, "tbl_Material", "DescripcionMaterial") + "," +
                        "tbl_Tipo_id = " + valorAIngresarNumero(comboBoxTipo, "tbl_Tipo", "DescripcionTipo") + "," +
                        "tbl_Pais_id = " + valorAIngresarNumero(comboBoxPais, "tbl_Pais", "DescripcionPais") + "," +
                        "tbl_Marca_id = " + valorAIngresarNumero(comboBoxMarca, "tbl_Marca", "DescripcionMarca") + "," +
                        "tbl_ColorProducto_id = " + valorAIngresarNumero(comboBoxColor, "tbl_ColorProducto", "DescripcionColor") + ", " +
                        "image_path = '" + rutaImagen + "'", "" +
                        "id", codigo, "Actualizacion de datos de productos");
                    MessageBox.Show("Datos Modificados Exitosamente", "Datos Modificados", MessageBoxButtons.OK, MessageBoxIcon.Information);*/
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Error al Modificar Los Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            OpenFileDialog buscar = new OpenFileDialog();
            if (buscar.ShowDialog() == DialogResult.OK)
            {
                path_Image = buscar.FileName;
            }
            cargarImagenAPicturebox();
        }

        private void numericUpDownPrecioVenta_TextChanged(object sender, EventArgs e)
        {
            TextBox txtb = sender as TextBox;
            if (txtb.Text == "") txtb.Text = "0";
        }
    }
}
