using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Odbc;
using System.Windows.Forms;
using System.Data;
using MantenimientoProductos;
using System.Security.Cryptography;

namespace CapaDatosFerreteria
{
    public class ClaseCapaDatosFerreteria
    {
        public string dsn = "dsn=Ferreteria";

        public Tuple<OdbcConnection, OdbcTransaction> ObtenerConexion()
        {
            OdbcConnection conectar = new OdbcConnection(dsn);
            conectar.Open();
            OdbcTransaction transaction = conectar.BeginTransaction();
            return Tuple.Create(conectar, transaction);
        }D:\SEMESTRE IX\SISTEMA TESIS\CapaDatosFerreteria\CapaDatosFerreteria\CapaDatosFerreteria\ClaseCapaDatosFerreteria.cs

        OdbcTransaction transaction = null;                                             // Transaccion

        /*public string Encriptar(string dato, string llave)
        {
            byte[] keyArray;
            byte[] encriptar = Encoding.UTF8.GetBytes(dato);

            keyArray = Encoding.UTF8.GetBytes(llave);

            var tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();

            byte[] resultado = cTransform.TransformFinalBlock(encriptar, 0, encriptar.Length);
            tdes.Clear();

            return Convert.ToBase64String(resultado, 0, resultado.Length);

        }

        public string DesEncriptar(string dato, string llave)
        {
            byte[] keyArray;
            byte[] decriptar = Convert.FromBase64String(dato);

            keyArray = Encoding.UTF8.GetBytes(llave);

            var tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();

            byte[] resultado = cTransform.TransformFinalBlock(decriptar, 0, decriptar.Length);
            tdes.Clear();

            return Encoding.UTF8.GetString(resultado);

        }*/
        /// Encripta una cadena
        public string Encriptar(string cadenaAencriptar)
        {
            string result = string.Empty;
            byte[] encryted = System.Text.Encoding.Unicode.GetBytes(cadenaAencriptar);
            result = Convert.ToBase64String(encryted);
            return result;
        }

        /// Esta función desencripta la cadena que le envíamos en el parámentro de entrada.
        public string DesEncriptar(string cadenaAdesencriptar)
        {
            string result = string.Empty;
            byte[] decryted = Convert.FromBase64String(cadenaAdesencriptar);
            result = System.Text.Encoding.Unicode.GetString(decryted);
            return result;
        }

        //Funciones de encriptamiento


        public void InsertDatos(string Tabla, string DatosAInsertar, string accion)
        {
            try
            {
                using (var conn = new OdbcConnection(dsn))
                {
                    conn.Open();

                    {
                        using (var cmd = conn.CreateCommand())
                        {
                            string query = "INSERT INTO " + Tabla + " VALUES(" + DatosAInsertar + ");";
                            cmd.CommandText = query;
                            cmd.ExecuteNonQuery();
                            //MessageBox.Show("Insertado Exitosamente");
                        }
                    }
                    conn.Close();
                }
                ClaseBitacoraFerreteria cpb = new ClaseBitacoraFerreteria();
                cpb.setBitacora(accion);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error al Guardar Los Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // CARGAR COMBOBOX
        /// <summary>
        /// CARGA DATOS A COMBOBOX
        /// </summary>
        /// <param name="cboEnviado"></param>
        /// <param name="DatoALlenar"></param>
        /// <param name="Tabla"></param>
        public void cargarDatosACombobox(ComboBox cboEnviado, string DatoALlenar, string Tabla)
        {
            try
            {
                using (var conn = new OdbcConnection(dsn))
                {
                    OdbcDataReader Reader;
                    conn.Open();
                    {
                        using (var cmd = conn.CreateCommand())
                        {
                            cmd.CommandText = "SELECT " + DatoALlenar + " FROM " + Tabla + " ; ";
                            Reader = cmd.ExecuteReader();
                            while (Reader.Read())
                            {
                                cboEnviado.Items.Add((Reader[DatoALlenar].ToString()));
                            }
                        }
                    }
                    conn.Close();
                }
                cboEnviado.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error al Cargar Los Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Extrae el codigo para poder evaluar tambien
        /// </summary>
        /// <param name="cboEnviado"></param>
        /// <param name="DatoALlenar"></param>
        /// <param name="Tabla"></param>
        public void cargarDatosConIDACombobox(ComboBox cboEnviado, string Codigo, string DatoALlenar, string Tabla)
        {
            try
            {
                using (var conn = new OdbcConnection(dsn))
                {
                    OdbcDataReader Reader;
                    conn.Open();
                    {
                        using (var cmd = conn.CreateCommand())
                        {
                            cmd.CommandText = "SELECT " + DatoALlenar + ", "+ Codigo+" FROM " + Tabla + " ; ";
                            Reader = cmd.ExecuteReader();
                            while (Reader.Read())
                            {
                                cboEnviado.Items.Add((Reader[Codigo].ToString() + "-" + Reader[DatoALlenar].ToString()));
                            }
                        }
                    }
                    conn.Close();
                }
                cboEnviado.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error al Cargar Los Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Extrae el codigo a partir de una descripcion
        /// </summary>
        /// <param name="Codigo">Campo del codigo</param>
        /// <param name="Tabla">Tabla donde se realizara la busqueda</param>
        /// <param name="BusquedaPor">Campo de la descripcion</param>
        /// <param name="Busqueda">Dato a buscar</param>
        /// <returns></returns>
        public String ExtraerCodigo(string Codigo, string Tabla, string BusquedaPor, string Busqueda)
        {
            string datos = "";
            try
            {
                using (var conn = new OdbcConnection(dsn))
                {
                    OdbcDataReader Reader;
                    conn.Open();
                    {
                        using (var cmd = conn.CreateCommand())
                        {
                            cmd.CommandText = "SELECT " + Codigo + " FROM " + Tabla + " WHERE " + BusquedaPor + " = '" + (Busqueda) + "'";
                            Reader = cmd.ExecuteReader();
                            while (Reader.Read())
                            {
                                datos = ((Reader[Codigo].ToString()));
                            }
                        }
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error al Cargar Los Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return datos;
        }

        public String ExtraerCodigo2(string Codigo, string Tabla, string BusquedaPor, string Busqueda)
        {
            string datos = "";
            try
            {
                using (var conn = new OdbcConnection(dsn))
                {
                    OdbcDataReader Reader;
                    conn.Open();
                    {
                        using (var cmd = conn.CreateCommand())
                        {
                            cmd.CommandText = "SELECT " + Codigo + " FROM " + Tabla + " WHERE " + BusquedaPor + " = " + (Busqueda) + ";";
                            Reader = cmd.ExecuteReader();
                            while (Reader.Read())
                            {
                                datos = ((Reader[Codigo].ToString()));
                            }
                        }
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error al Cargar Los Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return datos;
        }

        /// <summary>
        /// Extrae el codigo, pero se pueden pedir sum, count, etc.
        /// </summary>
        /// <param name="Codigo1">Valor que llevara el sum</param>
        /// <param name="Codigo2">Nombre de la consulta</param>
        /// <param name="Tabla">Tabla de la consulta</param>
        /// <param name="BusquedaPor">campo en donde se buscara</param>
        /// <param name="Busqueda">valor que se buscara</param>
        /// <returns></returns>
        public String ExtraerCodigo3(string Codigo1, string Codigo2, string Tabla, string BusquedaPor, string Busqueda)
        {
            string datos = "";
            try
            {
                using (var conn = new OdbcConnection(dsn))
                {
                    OdbcDataReader Reader;
                    conn.Open();
                    {
                        using (var cmd = conn.CreateCommand())
                        {
                            cmd.CommandText = "SELECT " + Codigo1 + Codigo2 + " FROM " + Tabla + " WHERE " + BusquedaPor + " = " + (Busqueda) + ";";
                            Reader = cmd.ExecuteReader();
                            while (Reader.Read())
                            {
                                datos = ((Reader[Codigo2].ToString()));
                            }
                        }
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error al Cargar Los Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return datos;
        }

        /// <summary>
        /// Funcion que debuelve un dataset para llenar un data grid
        /// </summary>
        /// <param name="CadenaDeCampos">Campos a utilizar</param>
        /// <param name="Tabla">Tabla en la que hay que buscar</param>
        /// <returns></returns>
        // FUNCION QUE DEVUELVE UN DATASET PARA LLENAR UN DATAGIRD DE UNA TABLA QUE TENGA STATUS
        public DataSet LlenarGrid(string CadenaDeCampos, string Tabla, string statusWhere, string accion)   // ENVIO DE PARAMETROS, LOS CAMPOS A LLENAR Y LA TABLA A BUSCAR
        {
            DataSet ds = new DataSet();
            try
            {
                using (var conn = new OdbcConnection(dsn))
                {
                    conn.Open();

                    {
                        using (var cmd = conn.CreateCommand())
                        {
                            cmd.CommandText = "SELECT " + CadenaDeCampos + " FROM " + Tabla + " WHERE "+ statusWhere +" ;";
                            OdbcDataAdapter m_datos = new OdbcDataAdapter(cmd);
                            ds = new DataSet();
                            m_datos.Fill(ds);
                        }
                    }
                    conn.Close();
                }
                ClaseBitacoraFerreteria cpb = new ClaseBitacoraFerreteria();
                cpb.setBitacora(accion);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error al Cargar Los Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return ds;
        }
        
        /// <summary>
        /// Carga datos de detalles de productos
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public string[] cargarDetallesProductos(string codigo, string accion)
        {
            string[] datos;
            datos = new string[21];
            try
            {
                using (var conn = new OdbcConnection(dsn))
                {
                    OdbcDataReader Reader;
                    conn.Open();
                    {
                        using (var cmd = conn.CreateCommand())
                        {
                            cmd.CommandText = "SELECT "+
                                "tbl_producto.id as CÓDIGO, tbl_producto.nombre as NOMBRE, tbl_producto.voltaje as 'VOL.', tbl_producto.amperaje as 'AMP.', " +
                                "tbl_producto.capacitancia as 'CAP.', tbl_producto.resistencia as 'RES.', tbl_producto.tamaño as TAMAÑO, tbl_producto.diametro as DIAMETRO, " +
                                "tbl_producto.fuerza as FUERZA, tbl_producto.temperatura as 'TEMP.', tbl_producto.rpm as RPM, tbl_producto.hz as Hz, " +
                                "tbl_producto.precioCompra as PRECIOC, tbl_producto.precioVenta as PRECIOV, " +
                                "tbl_Material.DescripcionMaterial as MATERIAL, tbl_Tipo.DescripcionTipo as TIPO, " +
                                "tbl_Pais.DescripcionPais as PAIS, tbl_Marca.DescripcionMarca as MARCA, tbl_ColorProducto.DescripcionColor as COLOR, " +
                                "tbl_producto.observaciones as OBSERVACIONES , tbl_producto.image_path as IMAGEN " +
                                "FROM " +
                                "tbl_producto, tbl_Material, tbl_Tipo, " +
                                "tbl_Pais, tbl_Marca, tbl_ColorProducto " +
                                "WHERE " +
                                "tbl_producto.tbl_Material_id = tbl_Material.id AND " +
                                "tbl_producto.tbl_Tipo_id = tbl_Tipo.id AND " +
                                "tbl_producto.tbl_Pais_id = tbl_Pais.id AND " +
                                "tbl_producto.tbl_Marca_id = tbl_Marca.id AND " +
                                "tbl_producto.tbl_ColorProducto_id = tbl_ColorProducto.id AND " +
                                "tbl_producto.id = " + codigo +
                                "; ";
                            Reader = cmd.ExecuteReader();
                            while (Reader.Read())
                            {
                                datos[0] = (Reader["CÓDIGO"].ToString());
                                datos[1] = (Reader["NOMBRE"].ToString());
                                datos[2] = (Reader["VOL."].ToString());
                                datos[3] = (Reader["AMP."].ToString());
                                datos[4] = (Reader["CAP."].ToString());
                                datos[5] = (Reader["RES."].ToString());
                                datos[6] = (Reader["TAMAÑO"].ToString());
                                datos[7] = (Reader["DIAMETRO"].ToString());
                                datos[8] = (Reader["FUERZA"].ToString());
                                datos[9] = (Reader["TEMP."].ToString());
                                datos[10] = (Reader["RPM"].ToString());
                                datos[11] = (Reader["Hz"].ToString());
                                datos[12] = (Reader["PRECIOV"].ToString());
                                datos[13] = (Reader["MATERIAL"].ToString());
                                datos[14] = (Reader["TIPO"].ToString());
                                datos[15] = (Reader["PAIS"].ToString());
                                datos[16] = (Reader["MARCA"].ToString());
                                datos[17] = (Reader["COLOR"].ToString());
                                datos[18] = (Reader["OBSERVACIONES"].ToString());
                                datos[19] = (Reader["IMAGEN"].ToString());
                                datos[20] = (Reader["PRECIOC"].ToString());
                            }
                        }
                    }
                    conn.Close();
                }
                ClaseBitacoraFerreteria cpb = new ClaseBitacoraFerreteria();
                cpb.setBitacora(accion);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error al Cargar Los Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return datos;
        }


        /// <summary>
        /// Carga datos de detalles de usuarios
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public string[] cargarDetallesUsuarios(string codigo, string accion)
        {
            string[] datos;
            datos = new string[15];
            try
            {
                using (var conn = new OdbcConnection(dsn))
                {
                    OdbcDataReader Reader;
                    conn.Open();
                    {
                        using (var cmd = conn.CreateCommand())
                        {
                            cmd.CommandText = "SELECT "+
                            "DISTINCT tbl_usuario.Nickname as 'NOMBRE USUARIO', tbl_usuario.Password as 'PASSWORD', tbl_usuario.id as 'CODIGO', tbl_usuario.image_path as 'IMAGEN' "+
                            ",tbl_empleado.DPI as 'DPI'," +
                            "tbl_persona.Id as 'ID', tbl_persona.Nombre as 'NOMBRE', tbl_persona.Apellidos as 'APELLIDOS' " +
                            ", tbl_empleado.Edad as 'EDAD', tbl_empleado.Sexo as 'SEXO', tbl_puesto.Nombre as 'PUESTO' " +
                            ", tbl_telefono.id as 'IDTEL', tbl_telefono.NumeroTelefono as 'TELEFONO',  tbl_correo.id as 'IDCOR',tbl_correo.Correo as 'CORREO' " +
                            "FROM " +
                            "tbl_usuario, tbl_persona, tbl_empleado, tbl_telefono, tbl_correo, tbl_puesto " +
                            "WHERE " +
                            "tbl_usuario.tbl_empleado_DPI = tbl_empleado.DPI AND " +
                            "tbl_empleado.tbl_persona_id = tbl_persona.id AND " +
                            "tbl_telefono.tbl_persona_id = tbl_persona.id AND " +
                            "tbl_correo.tbl_persona_id = tbl_persona.id AND " +
                            "tbl_puesto.id = tbl_empleado.tbl_puesto_id AND " +
                            "tbl_usuario.status = 1 AND " +
                            "tbl_usuario.id = "+ codigo +
                            "; ";
                            Reader = cmd.ExecuteReader();
                            while (Reader.Read())
                            {
                                datos[0] = (Reader["CODIGO"].ToString());
                                datos[1] = (Reader["NOMBRE USUARIO"].ToString());
                                datos[2] = (Reader["IMAGEN"].ToString());
                                datos[3] = (Reader["DPI"].ToString());
                                datos[4] = (Reader["NOMBRE"].ToString());
                                datos[5] = (Reader["APELLIDOS"].ToString());
                                datos[6] = (Reader["EDAD"].ToString());
                                datos[7] = (Reader["SEXO"].ToString());
                                datos[8] = (Reader["PUESTO"].ToString());
                                datos[9] = (Reader["TELEFONO"].ToString());
                                datos[10] = (Reader["CORREO"].ToString());
                                datos[11] = (Reader["PASSWORD"].ToString());
                                datos[12] = (Reader["ID"].ToString());
                                datos[13] = (Reader["IDTEL"].ToString());
                                datos[14] = (Reader["IDCOR"].ToString());
                            }
                        }
                    }
                    conn.Close();
                }
                ClaseBitacoraFerreteria cpb = new ClaseBitacoraFerreteria();
                cpb.setBitacora(accion);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error al Cargar Los Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return datos;
        }

        /// <summary>
        /// Valida la existencia de codigos
        /// </summary>
        /// <param name="Tabla"></param>
        /// <param name="CampoABuscar"></param>
        /// <param name="DatoABuscar"></param>
        /// <returns></returns>
        public bool ValidarExistenciaDeCodigos(string Tabla, string CampoABuscar, string DatoABuscar)
        {
            bool res = false;
            string datos = "";
            try
            {
                using (var conn = new OdbcConnection(dsn))
                {
                    OdbcDataReader Reader;
                    conn.Open();
                    {
                        using (var cmd = conn.CreateCommand())
                        {
                            cmd.CommandText = "SELECT " + CampoABuscar + " FROM " + Tabla + " WHERE " + CampoABuscar + " = " + DatoABuscar + ";";
                            Reader = cmd.ExecuteReader();
                            while (Reader.Read())
                            {
                                datos = (Reader[CampoABuscar].ToString());
                            }
                        }
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error al Cargar Los Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (datos == "")
            {
                res = true;
            }
            else
            {
                res = false;
            }
            return res;
        }

        /// <summary>
        /// Modificacion de datos en una tabla
        /// </summary>
        /// <param name="Tabla">Tabla a cambiar</param>
        /// <param name="DatosACambiar">Nuevos datos a ingresar</param>
        /// <param name="CampoABuscar">Campos a modificar</param>
        /// <param name="DatoABuscar">Datos que se busca para cambiar</param>
        public void ModificarDatos(string Tabla, string DatosACambiar, string CampoABuscar, string DatoABuscar, string accion)
        {
            try
            {
                using (var conn = new OdbcConnection(dsn))
                {
                    conn.Open();

                    {
                        using (var cmd = conn.CreateCommand())
                        {
                            cmd.CommandText = "UPDATE " + Tabla + " SET " + DatosACambiar + " WHERE " + CampoABuscar + "=" + DatoABuscar + ";";
                            cmd.ExecuteNonQuery();
                        }
                    }
                    conn.Close();
                }
                ClaseBitacoraFerreteria cpb = new ClaseBitacoraFerreteria();
                cpb.setBitacora(accion);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error al Modificar Los Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public int RetornoMenosExistencias(string comando)
        {
            int res = 0;
            try
            {
                using (var conn = new OdbcConnection(dsn))
                {
                    OdbcDataReader Reader;
                    conn.Open();
                    {
                        using (var cmd = conn.CreateCommand())
                        {
                            cmd.CommandText = comando;
                            Reader = cmd.ExecuteReader();
                            while (Reader.Read())
                            {
                                res++;
                            }
                        }
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error al Cargar Los Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return res;
        }

        /// <summary>
        /// Llenar Reporte
        /// </summary>
        /// <param name="CadenaDeCampos"></param>
        /// <param name="Tabla"></param>
        /// <param name="statusWhere"></param>
        /// <returns></returns>
        public DataSet LlenarReport(string myQuery, string accion)   // ENVIO DE PARAMETROS, LA SENTENCIA
        {
            DataSet ds = new DataSet();
            try
            {
                using (var conn = new OdbcConnection(dsn))
                {
                    conn.Open();

                    {
                        using (var cmd = conn.CreateCommand())
                        {
                            cmd.CommandText = myQuery;
                            OdbcDataAdapter m_datos = new OdbcDataAdapter(cmd);
                            ds = new MantenimientoProductos.DataSetProductos();
                            m_datos.Fill(ds,"tbl_Productos");
                        }
                    }
                    conn.Close();
                }
                ClaseBitacoraFerreteria cpb = new ClaseBitacoraFerreteria();
                cpb.setBitacora(accion);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error al Cargar Los Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return ds;
        }

        /// <summary>
        /// Carga datos  a combobox
        /// </summary>
        /// <param name="cboEnviado"></param>
        /// <param name="DatoALlenar"></param>
        /// <param name="DatoQueLLena"></param>
        /// <param name="Tabla"></param>
        public void cargarDatosACombobox2(ComboBox cboEnviado, string DatoALlenar, string DatoQueLLena1, string Tabla)
        {
            try
            {
                using (var conn = new OdbcConnection(dsn))
                {
                    OdbcDataReader Reader;
                    string aux = "";
                    conn.Open();
                    {
                        using (var cmd = conn.CreateCommand())
                        {
                            cmd.CommandText = "SELECT " + DatoALlenar + " FROM " + Tabla + " ; ";
                            Reader = cmd.ExecuteReader();
                            while (Reader.Read())
                            {
                                aux = Reader["NombreC"].ToString();
                                cboEnviado.Items.Add(aux);
                            }
                        }
                    }
                    conn.Close();
                }
                cboEnviado.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error al Cargar Los Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Funcion que devuleve sentencia para en ingreso de datos
        /// </summary>
        /// <param name="Tabla">
        /// Tabla en la cual se ingresaran los datos</param>
        /// <param name="DatosAInsertar">
        /// Datos a insertar en la tabla</param>
        /// <returns>Retorna la sentencia</returns>
        public string DevolverSentencia_Insert(string Tabla, string DatosAInsertar)
        {
            string command = "";
            try
            {
                command = "INSERT INTO " + Tabla + " VALUES(" + DatosAInsertar + ");";
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }
            return command;
        }

        /// <summary>
        /// Sentencia para modificar datos
        /// </summary>
        /// <param name="Tabla"></param>
        /// Tabla donde se realizara la accion
        /// <param name="DatosACambiar"></param>
        /// Nuevos datos a ingresar
        /// <param name="CampoABuscar"></param>
        /// <param name="DatoABuscar"></param>
        /// <returns></returns>
        public string DevolverSentencia_Modificar(string Tabla, string DatosACambiar, string CampoABuscar, string DatoABuscar)
        {
            string command = "";
            try
            {
                command = "UPDATE " + Tabla + " SET " + DatosACambiar + " WHERE " + CampoABuscar + "=" + DatoABuscar + ";";
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }
            return command;
        }

        /// <summary>
        /// Funcino que devuelve la sentencia para eliminar datos
        /// </summary>
        /// <param name="Tabla">Tabla de la cual se eliminaran los dtaos</param>
        /// <param name="CampoABuscar">El campo en el cual se realizara la comparacion</param>
        /// <param name="DatoABuscar">Dato con el cual se realizara la comparacion</param>
        /// <returns></returns>
        public string DevolverSentencia_Eliminar(string Tabla, string CampoABuscar, string DatoABuscar)
        {
            string command = "";
            try
            {
                command = "DELETE FROM " + Tabla + " WHERE " + CampoABuscar + "=" + DatoABuscar + ";";
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }
            return command;
        }

        /// <summary>
        /// Funcion encargada de realizar ela transaccion
        /// </summary>
        /// <param name="sParametro">Lista de sentencias a ejecutar</param>
        /// <param name="accionBitacora">accion que se guardara en la bitacora</param>
        /// <param name="appCod">Codigo de la aplicacion en la que se realiza la transaccion</param>
        public void EjectuarOperacionExterna(List<string> sParametro, string accionBitacora, string appCod,string accion)
        {
            try
            {
               // graphicLayer cp = new graphicLayer();
                var resultado = ObtenerConexion();
                OdbcTransaction transaction = resultado.Item2;
                OdbcCommand cmd = resultado.Item1.CreateCommand();
                cmd.Transaction = transaction;

                foreach (string value in sParametro)
                {
                    cmd.CommandText = value;
                    cmd.ExecuteNonQuery();
                }
                transaction.Commit();
                ClaseBitacoraFerreteria cpb = new ClaseBitacoraFerreteria();
                cpb.setBitacora(accion);
                //cp.Accion(accionBitacora, appCod);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al realizar esta accíon " + ex, "ERROR AL REALIZAR LA TRANSACCION", MessageBoxButtons.OK, MessageBoxIcon.Error);
                try
                {
                    transaction.Rollback();
                    Console.WriteLine("Se realizo Rollback");
                }
                catch (Exception)
                {
                    Console.WriteLine("No se pudo realizar Rollback");
                }
            }
        }


        /// <summary>
        /// Carga datos de detalles de productos
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public string[] cargarDetallesEmpleados(string codigo, string accion)
        {
            string[] datos;
            datos = new string[9];
            try
            {
                using (var conn = new OdbcConnection(dsn))
                {
                    OdbcDataReader Reader;
                    conn.Open();
                    {
                        using (var cmd = conn.CreateCommand())
                        {
                            cmd.CommandText = "SELECT " +
                            "tbl_empleado.dpi as DPI, tbl_persona.nombre as Nombre, tbl_empleado.sexo as Sexo, tbl_persona.id as PersonaID,tbl_persona.apellidos as Apellido, tbl_empleado.edad as Edad, " +
                            "tbl_Puesto.nombre as Puesto " +
                            "FROM " +
                            "bd_ferreteria.tbl_persona, bd_ferreteria.tbl_empleado, " +
                            "bd_ferreteria.tbl_puesto " +
                            "WHERE " +
                            "tbl_empleado.tbl_Persona_id = tbl_persona.id AND " +
                            "tbl_empleado.tbl_Puesto_id = tbl_puesto.id AND " +
                            "tbl_persona.id = " + codigo + "; ";
                            Reader = cmd.ExecuteReader();
                            while (Reader.Read())
                            {
                                datos[0] = (Reader["DPI"].ToString());
                                datos[1] = (Reader["Nombre"].ToString());
                                datos[2] = (Reader["PersonaID"].ToString());
                                datos[3] = (Reader["Apellido"].ToString());
                                datos[4] = (Reader["Edad"].ToString());
                                datos[5] = (Reader["Puesto"].ToString());
                                datos[6] = (Reader["Sexo"].ToString());
                            }
                        }
                    }
                    conn.Close();
                }
                ClaseBitacoraFerreteria cpb = new ClaseBitacoraFerreteria();
                cpb.setBitacora(accion);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error al Cargar Los Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return datos;
        }

        /// <summary>
        /// Elimina los datos
        /// </summary>
        /// <param name="Tabla">tabla de donde se eliminaran los datos</param>
        /// <param name="CampoABuscar">campo que se compara para elimnar datos</param>
        /// <param name="DatoABuscar">datos que se buscara para eliminar</param>
        public void EliminarDatos(string Tabla, string CampoABuscar, string DatoABuscar, string accion)
        {
            try
            {
                using (var conn = new OdbcConnection("dsn=Ferreteria"))
                {
                    conn.Open();

                    {
                        using (var cmd = conn.CreateCommand())
                        {
                            cmd.CommandText = "DELETE FROM " + Tabla + " WHERE " + CampoABuscar + "=" + DatoABuscar + ";";
                            cmd.ExecuteNonQuery();
                            //MessageBox.Show("Modificado");
                        }
                    }
                    conn.Close();
                }
                ClaseBitacoraFerreteria cpb = new ClaseBitacoraFerreteria();
                cpb.setBitacora(accion);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error al Eliminar Los Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
