using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProyectoInge.App_Code.Capa_de_Acceso_a_Datos;
using ProyectoInge.App_Code.Capa_de_Datos__Entidad_;
using System.Data;
using System.Data.SqlClient;


namespace ProyectoInge.App_Code.Capa_de_Acceso_a_Datos
{
    public class ControladoraBDProyectos
    {
        AccesoBaseDatos acceso = new AccesoBaseDatos();


        /*Método para eliminar de la base de datos un funcionario específico
        * Requiere: un string con la cedula de un funcionario específico
        * Modifica: Genera las consultas para mandarlas a la base de datos para borrar el funcionario de la tabla Funcionario y Trabaja_En
        * Retorna: true si se llevó a cabo correctamente la eliminación y false si no fue existosa.
        */
        public bool eliminarProyectoAdminitrador(int idProyecto)
        {
            try
            {
                string borrado = "Delete from Proyecto where id_proyecto ='" + idProyecto + "';";
                acceso.eliminarDatos(borrado);
                return true;
            }
            catch (SqlException e)
            {
                return false;
            }

        }




    }
}