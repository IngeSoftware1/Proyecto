using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using ProyectoInge.App_Code.Capa_de_Datos__Entidad_;
using System.Data.SqlClient;

namespace ProyectoInge.App_Code.Capa_de_Acceso_a_Datos
{
    public class ControladoraBDDiseno
    {
        AccesoBaseDatos acceso = new AccesoBaseDatos();

        public bool buscarAsignacionMiembrosDiseno(string cedulaDeFuncionario)
    {

        bool resultado = false;

        try
        {
            string consultaPruebas = "SELECT * FROM Diseno_Pruebas WHERE cedula_responsable ='" + cedulaDeFuncionario + "'";
            DataTable dataDiseno = acceso.ejecutarConsultaTabla(consultaPruebas);
            if (dataDiseno.Rows.Count >= 1)
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