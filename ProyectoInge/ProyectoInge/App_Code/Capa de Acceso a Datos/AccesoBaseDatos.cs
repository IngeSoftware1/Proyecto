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
        String conexion = @"Data Source=ASUS; Initial Catalog=g3inge; Integrated Security=SSPI";
        //String conexion = @"Data Source=LEANDRO\SQLEXPRESS; Initial Catalog=g3inge; Integrated Security=SSPI";
        //String conexion = @"Data Source=eccibdisw; Initial Catalog=g3inge; Integrated Security=SSPI";
        //String conexion = @"Data Source=RAQUELCHAVADDEF; Initial Catalog=RAQUEL; Integrated Security=SSPI";
       // String conexion = @"Data Source=.; Initial Catalog=baseRaque; Integrated Security=SSPI";
        //String conexion = @"Data Source=CAROLINA-HP\CAROLINA; Initial Catalog=Inge1; Integrated Security=SSPI";
        //String conexion = @"Data Source=LARI-PC; Initial Catalog=Inge1; Integrated Security=SSPI";
        //String conexion = @"Data Source=PC; Initial Catalog=g3inge; Integrated Security=SSPI";

        public AccesoBaseDatos()
        {
        }


        //  aca hay un conflicto
        public int suma()
        {
            int x = 0;
            ++x;
            return x;

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