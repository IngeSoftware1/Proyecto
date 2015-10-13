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
    }
}