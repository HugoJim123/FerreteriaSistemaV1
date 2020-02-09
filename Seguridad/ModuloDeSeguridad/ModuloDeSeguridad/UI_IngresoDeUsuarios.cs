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
using System.IO;
using System.Globalization;
using System.Text.RegularExpressions;

namespace ModuloDeSeguridad
{
    public partial class UI_IngresoDeUsuarios : Form
    {
        int tipoMov;
        string codigoACambiar;
        string[] datosUsuario = new string[12];

        public UI_IngresoDeUsuarios(int tipoMov2, string codigo2)
        {
            InitializeComponent();
            tipoMov = tipoMov2;
            codigoACambiar = codigo2;
            LoadForm();            
        }

        bool empleadoCargado = false;
        string llave = "dDiPVK9R3oFvX";

        void LoadForm()
        {
            comboBoxEmpleado.Items.Clear();
            comboBoxPuesto.Items.Clear();
            CapaDatosFerreteria.ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            try
            {
                cpd.cargarDatosACombobox2(comboBoxEmpleado, " CONCAT(Apellidos,',',Nombre) AS NombreC", "Nombre", "" +
                    " bd_ferreteria.tbl_Persona, bd_ferreteria.tbl_Empleado WHERE tbl_Empleado.status = 1 AND tbl_Persona.status = 1 AND tbl_Persona.id = tbl_Empleado.tbl_Persona_id ");
                cpd.cargarDatosACombobox(comboBoxPuesto, "Nombre", "tbl_Puesto");
                comboBoxEmpleado.SelectedIndex = 0;
                comboBoxPuesto.SelectedIndex = 0;
                comboBoxSexo.SelectedIndex = 0;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR AL CARGAR DATOS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (tipoMov == 1)
            {
                panel9.Visible = false;
                comboBoxEmpleado.Visible = false;
                cargarUsuarios(codigoACambiar);
                textBoxDPI.Enabled = false;
                panelDatosModificar.Visible = true;
                buttonAddUsuario.Text = "MODIFICAR USUARIO";
            }
            else
            {
                panel9.Visible = true;
                comboBoxEmpleado.Visible = true;
                textBoxDPI.Enabled = true;
                panelDatosModificar.Visible = false;
                buttonAddUsuario.Text = "AGREGAR USUARIO";
            }
        }

        void cargarUsuarios(string codigo)
        {
            ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            string[] datos = new string[15];
            try
            {
                datos = cpd.cargarDetallesUsuarios(codigoACambiar, "Vista de Detalles de usuarios");
                datosUsuario = datos;
                labelUserID.Text = datos[0];
                labelNameUser.Text = datos[1];
                textBoxNickname.Text = datos[1];
                textBoxPassword.Text = cpd.DesEncriptar(datos[11]);
                textBoxImagen.Text = datos[2];
                textBoxDPI.Text = datos[3];
                textBoxNombre.Text = datos[4];
                textBoxApellidos.Text = datos[5];
                numericUpDownEdad.Value = Convert.ToInt32(datos[6]);
                comboBoxSexo.SelectedItem = datos[7];
                comboBoxPuesto.SelectedItem = datos[8];
                textBoxTelefono.Text = datos[9];
                textBoxCorreo.Text = datos[10];
            }catch(Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR AL CARGAR DATOS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public string idCombobox(string datoAEvaluar)
        {
            ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            string[] datos = datoAEvaluar.Split(',');
            string res = cpd.ExtraerCodigo("id", "tbl_Persona", "Nombre = '" + datos[1] + "' AND Apellidos ", datos[0]);
            return res;
        }

        public string nextCodigo()
        {
            ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            return cpd.ExtraerCodigo2("AUTO_INCREMENT", "information_schema.TABLES", "TABLE_SCHEMA = 'bd_ferreteria' "+
                "AND TABLE_NAME", "'tbl_persona'");
        }

        /// <summary>
        /// Funcion que nos permite crear la carpeta con imagenes
        /// </summary>
        public void CrearCarpetaImagenes()
        {
            string carpeta = @"C:\Ferreteria\Imagenes\Usuarios";
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
            if (textBoxImagen.Text != "")
            {
                CrearCarpetaImagenes();
                string nuevaRuta = Path.Combine(@"C:\Ferreteria\Imagenes\Usuarios", filename);

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

        private void buttonAddUsuario_Click(object sender, EventArgs e)
        {
            string nameImage = Path.GetFileName(textBoxImagen.Text);
            guardarCopiaImagen(nameImage);
            string rutaImagen = " ";
            if (textBoxImagen.Text != "")
            {
                rutaImagen = @"C:\\Ferreteria\\Imagenes\\Usuarios\\" + nameImage;
            }
            ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            List<string> queryList = new List<string>();
            if (tipoMov == 0)
            {                
                if (empleadoCargado == false)
                {
                    queryList.Add(cpd.DevolverSentencia_Insert("tbl_Persona", "0, '" + textBoxNombre.Text + "', '" + textBoxApellidos.Text + "' , 1"));
                    queryList.Add(cpd.DevolverSentencia_Insert("tbl_Empleado", textBoxDPI.Text + "," + numericUpDownEdad.Value.ToString() + ",'" + comboBoxSexo.Text + "'," +
                        "" + cpd.ExtraerCodigo("id", "tbl_Puesto", "Nombre", comboBoxPuesto.Text) + "," + nextCodigo() + ", 1"));
                    queryList.Add(cpd.DevolverSentencia_Insert("tbl_Telefono", "0, '" + textBoxTelefono.Text + "' , " + nextCodigo() + ", 1"));
                    queryList.Add(cpd.DevolverSentencia_Insert("tbl_Correo", "0, '" + textBoxCorreo.Text + "' , " + nextCodigo() + ", 1"));
                }
                queryList.Add(cpd.DevolverSentencia_Insert("tbl_Usuario", "0, '" + textBoxNickname.Text + "', '" + cpd.Encriptar(textBoxPassword.Text) + "', " + textBoxDPI.Text + ", '" + rutaImagen + "' , 1"));
                try
                {
                    cpd.EjectuarOperacionExterna(queryList, "a", "a", "Transaccion de ingreso de usuario realizada");
                    MessageBox.Show("Datos Ingresados Exitosamente", "Datos Ingresados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "ERROR AL GUARDAR DATOS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            if (tipoMov == 1)
            {
                if (empleadoCargado == false)
                {
                    queryList.Add(cpd.DevolverSentencia_Modificar("tbl_Persona", "Nombre = '" + textBoxNombre.Text + "', Apellidos='" + textBoxApellidos.Text+"'", "id", datosUsuario[12]));
                    queryList.Add(cpd.DevolverSentencia_Modificar("tbl_Empleado", "Edad = " + numericUpDownEdad.Value.ToString() + ", Sexo = '" + comboBoxSexo.Text + "'," +
                        "tbl_Puesto_id  = " + cpd.ExtraerCodigo("id", "tbl_Puesto", "Nombre", comboBoxPuesto.Text), "DPI" , datosUsuario[3]));
                    queryList.Add(cpd.DevolverSentencia_Modificar("tbl_Telefono", "NumeroTelefono = '" + textBoxTelefono.Text + "'","id",datosUsuario[13]));
                    queryList.Add(cpd.DevolverSentencia_Modificar("tbl_Correo", "Correo = '" + textBoxCorreo.Text + "'", "id", datosUsuario[14]));
                }
                queryList.Add(cpd.DevolverSentencia_Modificar("tbl_Usuario", "Nickname = '" + textBoxNickname.Text + "', Password = '" + cpd.Encriptar(textBoxPassword.Text) + "', image_path = '" + rutaImagen + "'", "id" , datosUsuario[0]));
                try
                {
                    cpd.EjectuarOperacionExterna(queryList, "a", "a", "Transaccion de actualizacion de usuarios realiazada");
                    MessageBox.Show("Datos Modificados Exitosamente", "Datos Modificados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "ERROR AL GUARDAR DATOS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonAddImagen_Click(object sender, EventArgs e)
        {
            OpenFileDialog buscar = new OpenFileDialog();
            if (buscar.ShowDialog() == DialogResult.OK)
            {
                textBoxImagen.Text = buscar.FileName;
            }
            if(textBoxImagen.Text != ""){
                pictureBoxImagenDePerfil.BackgroundImage = Image.FromFile(textBoxImagen.Text);
            }
        }

        public void stateChangeEmpleados(bool state)
        {
            textBoxDPI.Enabled = state;
            textBoxNombre.Enabled = state;
            textBoxApellidos.Enabled = state;
            numericUpDownEdad.Enabled = state;
            comboBoxPuesto.Enabled = state;
            comboBoxSexo.Enabled = state;
            textBoxTelefono.Enabled = state;
            textBoxCorreo.Enabled = state;
        }

        private void buttonCargarEmpleado_Click(object sender, EventArgs e)
        {
            empleadoCargado = true;
            buttonCargarEmpleado.Visible = false;
            buttonDescargarEmpleados.Visible = true;
            string[] datos;
            datos = new string[9];
            ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            datos = cpd.cargarDetallesEmpleados(idCombobox(comboBoxEmpleado.Text),"Vista de datalles de usuarios");
            textBoxDPI.Text = datos[0];
            textBoxNombre.Text = datos[1];
            textBoxApellidos.Text = datos[3];
            numericUpDownEdad.Text = datos[4];
            comboBoxPuesto.SelectedItem = datos[5];
            comboBoxSexo.SelectedItem = datos[6];
            textBoxTelefono.Text = cpd.ExtraerCodigo2("NumeroTelefono", "tbl_telefono", "tbl_persona_id", datos[2]);
            textBoxCorreo.Text = cpd.ExtraerCodigo2("Correo", "tbl_correo", "tbl_persona_id", datos[2]);
            stateChangeEmpleados(false);
        }

        private void buttonDescargarEmpleados_Click(object sender, EventArgs e)
        {
            empleadoCargado = false;
            buttonDescargarEmpleados.Visible = false;
            buttonCargarEmpleado.Visible = true;
            stateChangeEmpleados(true);
        }

        private void buttonAddPuesto_Click(object sender, EventArgs e)
        {
            UI_MantenimientosPlantilla form = new UI_MantenimientosPlantilla("Puesto");
            form.ShowDialog();
            comboBoxPuesto.Items.Clear();
            ClaseCapaDatosFerreteria cpd = new ClaseCapaDatosFerreteria();
            try
            {
                cpd.cargarDatosACombobox(comboBoxPuesto, "Nombre", "tbl_Puesto");
                comboBoxPuesto.SelectedIndex = 0;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR AL CARGAR DATOS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBoxDPI_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validaciones val = new Validaciones();
            val.SoloNumeros(sender, e);
        }

        private void textBoxNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validaciones val = new Validaciones();
            val.soloLetras(sender, e);
        }

        private void textBoxNickname_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validaciones val = new Validaciones();
            val.soloLetrasSinEspacios(sender, e);
        }

        bool validarEmail(string email)
        {
            try
            {
                return Regex.IsMatch(email,
                    @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        void habilitarBoton()
        {
            if (textBoxDPI.Text == "")
            {
                pictureBoxErrorDPI.Visible = true;
            }
            else
            {
                pictureBoxErrorDPI.Visible = false;
            }
            if (textBoxNombre.Text == "")
            {
                pictureBoxErrorNombre.Visible = true;
            }
            else
            {
                pictureBoxErrorNombre.Visible = false;
            }
            if (textBoxApellidos.Text == "")
            {
                pictureBoxErrorApellidos.Visible = true;
            }
            else
            {
                pictureBoxErrorApellidos.Visible = false;
            }
            if (textBoxTelefono.Text == "")
            {
                pictureBoxErrorTelefono.Visible = true;
            }
            else
            {
                pictureBoxErrorTelefono.Visible = false;
            }
            if (!validarEmail(textBoxCorreo.Text))
            {
                pictureBoxErrorCorreo.Visible = true;
            }
            else
            {
                pictureBoxErrorCorreo.Visible = false;
            }
            if (textBoxNickname.Text == "")
            {
                pictureBoxErrorUserName.Visible = true;
            }
            else
            {
                pictureBoxErrorUserName.Visible = false;
            }
            if (textBoxPassword.Text == "")
            {
                pictureBoxErrorPassword.Visible = true;
            }
            else
            {
                pictureBoxErrorPassword.Visible = false;
            }
            if (numericUpDownEdad.Value < 18)
            {
                pictureBoxErrorEdad.Visible = true;
            }
            else
            {
                pictureBoxErrorEdad.Visible = false;
            }
            ///////
            if (textBoxDPI.Text == "" || textBoxNombre.Text == "" || textBoxApellidos.Text == "" || textBoxTelefono.Text == "" || !validarEmail(textBoxCorreo.Text) || textBoxNickname.Text == ""
                 || textBoxPassword.Text == "" || numericUpDownEdad.Value < 18)
            {
                buttonAddUsuario.Enabled = false;
            }
            else
            {
                buttonAddUsuario.Enabled = true;
            }
        }

        private void textBoxDPI_TextChanged(object sender, EventArgs e)
        {
            habilitarBoton();
        }
    }
}
