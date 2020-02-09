using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;
using System.Data.Odbc;
using CacheFerreteria;

namespace CapaDatosFerreteria
{
    public class ClaseBitacoraFerreteria
    {

        //Variables

        public string dsn = "dsn=Ferreteria";
        public string userCode = "";

        //Variables

        public string GetUserCodeForLog()
        {
            try
            {
                ClassCacheFerreteria ccf = new ClassCacheFerreteria();
                return ccf.getUsuario();
            }catch(Exception ex)
            {
                return "AdminMaster";
            }
        }

        //GET CURRENTE HOST
        private string getCurrentHost()
        {
            OdbcDataReader dr = null;
            string host = "";

            try
            {
                using (var conn = new OdbcConnection(dsn))
                {
                    conn.Open();

                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT SUBSTRING_INDEX(USER(), '@', -1) AS HOST,  @@hostname as hostname, @@port as port, DATABASE() as current_database;";
                        dr = cmd.ExecuteReader();
                        dr.Read();

                        host = dr["HOST"].ToString();
                        dr.Close();
                        conn.Close();

                        return host;
                    }

                }
            }
            catch (Exception ex)
            {
                return host;
            }
        }

        //GET TIME
        private string getTime()
        {
            OdbcDataReader dr = null;
            string hora = "";

            try
            {
                using (var conn = new OdbcConnection(dsn))
                {
                    conn.Open();

                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT time (NOW()) as HORA;";
                        dr = cmd.ExecuteReader();
                        dr.Read();

                        hora = dr["HORA"].ToString();
                        dr.Close();
                        conn.Close();

                        return hora;
                    }

                }
            }
            catch (Exception ex)
            {
                return hora;
            }
        }

        //GET DATE
        private string getDate()
        {
            OdbcDataReader dr = null;
            string fecha = "";

            try
            {
                using (var conn = new OdbcConnection(dsn))
                {
                    conn.Open();

                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT DATE_FORMAT(NOW(), \"%Y-%m-%d\" ) AS FECHA;";
                        dr = cmd.ExecuteReader();
                        dr.Read();

                        fecha = dr["FECHA"].ToString();
                        dr.Close();
                        conn.Close();

                        return fecha;
                    }
                }
            }
            catch (Exception ex)
            {
                return fecha;
            }
        }




        //ACCION FROM CAPA_LOGICA
        public bool setBitacora(string _accion)
        {
            string host = "";
            string time = "";
            string date = "";
            //HOST
            host = getCurrentHost();
            //TIME
            time = getTime();
            //DATE
            date = getDate();
            //Usuario
            userCode = GetUserCodeForLog();
            //ENVIO DE PARAMETROS A INSERT ACCION
            if (insertBitacora(Convert.ToInt32(userCode), host, _accion, time, date))
            {
                return true;
            }
            return false;

        }




        //INSERT ACCION (BITACORA)
        private bool insertBitacora(int usu_cod, string host, string accion, string hora, string fecha)
        {
            try
            {
                using (var conn = new OdbcConnection(dsn))
                {
                    conn.Open();

                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "INSERT INTO tbl_bitacora VALUES(0,'" + host + "','" + fecha + "','" + accion + "','" + usu_cod + "','" + hora + "')";
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        return true;

                    }

                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
