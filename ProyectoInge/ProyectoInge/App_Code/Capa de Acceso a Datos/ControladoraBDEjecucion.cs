using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using ProyectoInge.App_Code.Capa_de_Datos__Entidad_;
using System.Data.SqlClient;

namespace ProyectoInge.App_Code.Capa_de_Acceso_a_Datos
{
    public class ControladoraBDEjecucion
    {
        AccesoBaseDatos acceso = new AccesoBaseDatos();
        public bool buscarAsignacionMiembrosEjecucion(string cedula)
        {
           bool resultado = false;

            try
            {
                string consultaEjecucionPruebas = "SELECT * FROM Ejecucion_Prueba WHERE cedula_responsable ='" + cedula + "'";
                DataTable dataEjec = acceso.ejecutarConsultaTabla(consultaEjecucionPruebas);
                if (dataEjec.Rows.Count >= 1)
                {
                    resultado = true;
                }
                else
                {
                    resultado = false;
                }
            }
            catch (SqlException e)
            {
                resultado = false;
            }

            return resultado;
        }
        /* Método para obtener los tipos de no conformidades que podría tener una ejecución.
  * Requiere: no requiere parámetros.
  * Modifica: no realiza modificaciones.
  * Retorna: un DataTable con los tipos de no conformidades que tiene almacenada la base de datos.
  */
        public DataTable consultarTiposNC()
        {
            DataTable dt = new DataTable();
            string consulta;
            try
            {

                consulta = "SELECT T.id_tipoNC" + " FROM Tipo_NC  T ";
                dt = acceso.ejecutarConsultaTabla(consulta);


            }
            catch (SqlException e)
            {
                dt = null;
            }

            return dt;
        }

        /* Método para obtener los estados que podría tener una no conformidad.
        * Requiere: no requiere parámetros.
        * Modifica: no realiza modificaciones.
        * Retorna: un DataTable con los estados que tiene almacenada la base de datos.
        */
        public DataTable consultarEstados()
        {
            DataTable dt = new DataTable();
            string consulta;
            try
            {

                consulta = "SELECT E.estado_ejecucion" + " FROM Estado_Ejecucion E ";
                dt = acceso.ejecutarConsultaTabla(consulta);

            }
            catch (SqlException e)
            {
                dt = null;
            }

            return dt;
        }

        public DataTable getDescripcionN(string tipoNC)
        {
            DataTable dt = new DataTable();
            string consulta;
            try
            {
                consulta = "SELECT T.desc_NC" + " FROM Tipo_NC T WHERE T.id_tipoNC ='" + tipoNC + "';";
                dt = acceso.ejecutarConsultaTabla(consulta);

            }
            catch (SqlException e)
            {
                dt = null;
            }

            return dt;
        }

    }
}