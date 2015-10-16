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

        /*Método para buscar si en la base de datos existe un usuario con una cedula particular, que sea líder de diseño
        * Requiere: un string con la cedula de un funcionario específico
        * Modifica: Genera la consulta para mandarla a la base de datos para saber si hay un usuario líder de diseño.
        * Retorna: true si se encontro que el funcionario tenía era líder y false si no.
        */
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