using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace ProyectoInge.App_Code.Capa_de_Acceso_a_Datos
{
    public class AccesoBaseDatos
    {

        /*En Initial Catalog se agrega la base de datos propia. Intregated Security es para utilizar Windows Authentication*/
        String conexion = @"Data Source=PC; Initial Catalog=g3inge; Integrated Security=SSPI";
        
        public AccesoBaseDatos()
        {
        }

        public bool insertarDatos(String consulta)
        {
            SqlConnection sqlConnection = new SqlConnection(conexion);
            sqlConnection.Open();
            try
            {
                SqlCommand cons = new SqlCommand(consulta, sqlConnection);
                cons.ExecuteNonQuery();
                sqlConnection.Close();
                return true;
            }
            catch (SqlException ex)
            {
                return false;
            }
        }

        /**
         * Permite ejecutar una consulta SQL, los datos son devueltos en un SqlDataReader
         */
        public SqlDataReader ejecutarConsulta(String consulta)
        {
            SqlConnection sqlConnection = new SqlConnection(conexion);
            sqlConnection.Open();

            SqlDataReader datos = null;
            SqlCommand comando = null;

            try
            {
                comando = new SqlCommand(consulta, sqlConnection);
                datos = comando.ExecuteReader();
            }
            catch (SqlException ex)
            {
                
            }
            return datos;
        }
    }

}