using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace ProyectoInge.App_Code.Capa_de_Acceso_a_Datos
{
    public class AccesoBaseDatos
    {

        /*En Initial Catalog se agrega la base de datos propia. Intregated Security es para utilizar Windows Authentication*/
        //String conexion = @"Data Source=RAQUELCHAVADDEF; Initial Catalog=RAQUEL; Integrated Security=SSPI";
        String conexion = @"Data Source=.; Initial Catalog=baseDatosRaque; Integrated Security=SSPI";
        //String conexion = @"Data Source=CAROLINA-HP\CAROLINA; Initial Catalog=Inge1; Integrated Security=SSPI";
        //String conexion = @"Data Source=LARI-PC; Initial Catalog=Inge1; Integrated Security=SSPI";
       // String conexion = @"Data Source=PC; Initial Catalog=g3inge; Integrated Security=SSPI";
       //String conexion = @"Data Source=eccibdisw; Initial Catalog=g3inge; Integrated Security=SSPI";        
        //String conexion = @"Data Source=ASUS; Initial Catalog=g3inge; Integrated Security=SSPI";
        //String conexion = @"Data Source=LEANDRO\SQLEXPRESS; Initial Catalog=g3inge; Integrated Security=SSPI"; 

        public AccesoBaseDatos()
        {
        }

        
        /**
         * Consulta para insertar datos
         */
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
                Debug.Print("SOY EL NUMERO DE EXCEPCION DE SQL " + ex.Number.ToString());
                return false;
            }
        }

        public bool insertarNC(int idCaso, int idEjecucion, String idTipoNC, String justificacion, byte[] imagen, String extensionImagen, String estadoEjecucion)
        {
            SqlConnection sqlConnection = new SqlConnection(conexion);
            sqlConnection.Open();
            try
            {
                
                String insercion = "INSERT INTO Caso_Ejecutado (id_caso, id_ejecucion, id_tipoNC, justificacion, imagen, extension_imagen, estado_ejecucion) VALUES (@idCaso, @idEjecucion, @idTipoNC, @justificacion, @imagen, @extensionImagen, @estadoEjecucion)";
                SqlCommand cons = new SqlCommand(insercion, sqlConnection);
                cons.Parameters.Add("@idCaso", System.Data.SqlDbType.Int, 4);
                cons.Parameters.Add("@idEjecucion", System.Data.SqlDbType.Int, 4);
                cons.Parameters.Add("@idTipoNC", System.Data.SqlDbType.VarChar, 70);
                cons.Parameters.Add("@justificacion", System.Data.SqlDbType.VarChar, 40);
                cons.Parameters.Add("@imagen", System.Data.SqlDbType.VarBinary);
                cons.Parameters.Add("@extensionImagen", System.Data.SqlDbType.VarChar, 10);
                cons.Parameters.Add("@estadoEjecucion", System.Data.SqlDbType.VarChar, 20);

                cons.Parameters["@idCaso"].Value = idCaso;
                cons.Parameters["@idEjecucion"].Value = idEjecucion;
                cons.Parameters["@idTipoNC"].Value = idTipoNC;
                cons.Parameters["@justificacion"].Value = justificacion;
                cons.Parameters["@imagen"].Value = imagen;
                cons.Parameters["@extensionImagen"].Value = extensionImagen;
                cons.Parameters["@estadoEjecucion"].Value = estadoEjecucion;

                
                cons.ExecuteNonQuery();
                sqlConnection.Close();
                return true;
            }
            catch (SqlException ex)
            {
                Debug.Print("SOY EL NUMERO DE EXCEPCION DE SQL " + ex.Number.ToString());
                return false;
            }
        }

        /**
         * Consulta para eliminarDatos
         */
        public bool eliminarDatos(String consulta)
        {
            return insertarDatos(consulta);

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


        /**
         * Permite ejecutar una consulta SQL, los datos son devueltos en un DataTable
         */
        public DataTable ejecutarConsultaTabla(String consulta)
        {
            SqlConnection sqlConnection = new SqlConnection(conexion);
            
            DataTable table = new DataTable();
            try{
                sqlConnection.Open();

                 SqlCommand comando = new SqlCommand(consulta, sqlConnection);

                 SqlDataAdapter dataAdapter = new SqlDataAdapter(comando);

                 SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);

                 dataAdapter.Fill(table);

            }
            catch (SqlException ex)
            {

            }


            return table;
        } 

    }

}