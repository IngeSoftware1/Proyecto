using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using ProyectoInge.App_Code.Capa_de_Acceso_a_Datos;
using ProyectoInge.App_Code.Capa_de_Datos__Entidad_;
using System.Data.SqlClient;

namespace ProyectoInge.App_Code.Capa_de_Acceso_a_Datos
{
    public class ControladoraBDProyectos
    {
        AccesoBaseDatos acceso = new AccesoBaseDatos();

        /*Método para buscar si en la base de datos existe un proyecto asociado a un usuario con una cedula particular
        * Requiere: un string con la cedula de un funcionario específico
        * Modifica: Genera la consulta para mandarla a la base de datos para saber si hay un proyecto asociado al funcionario.
        * Retorna: true si se encontro que el funcionario tenía un proyecto asociado y false si no.
        */
        public bool buscarAsignacionProyectos(string cedulaDeFuncionario)
        {


            bool resultado = false;
            try
            {
                string consultaP = "SELECT * FROM Proyecto WHERE cedula_creador ='" + cedulaDeFuncionario + "'";
                DataTable data = acceso.ejecutarConsultaTabla(consultaP);
                if (data.Rows.Count >= 1)
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

        /*Método para buscar si en la base de datos un usuario específico es líder o no.
        * Requiere: un string con la cedula de un funcionario específico
        * Modifica: Genera la consulta para mandarla a la base de datos para saber si el usuario con la cédula específica es líder.
        * Retorna: true si se encontro que el funcionario era líder y false si no.
        */
        public bool buscarAsignacionMiembros(string cedulaDeFuncionario)
        {

            bool resultado = false;
            try
            {
                string consultaProyectos = "SELECT * FROM Proyecto WHERE cedula_lider ='" + cedulaDeFuncionario + "'";
                DataTable data = acceso.ejecutarConsultaTabla(consultaProyectos);
                if (data.Rows.Count >= 1)
                    if (data.Rows.Count >= 1)
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
		
		/*Método para eliminar de la base de datos un caso de prueba asociado a un proyecto
        * Requiere: un string con el id del proyecto que se va a eliminar
        * Modifica: eliminar el caso de prueba asociado al proyecto
        * Retorna: true si se llevó a cabo correctamente la eliminación y false si no fue existosa.
        */
        public bool eliminarProyectoCasoPueba(string idProyecto)
        {
            try
            {
                string borradoProyecto = "Delete from Caso_Prueba where id_proyecto ='" + idProyecto + "';";
                acceso.eliminarDatos(borradoProyecto);
                return true;
            }
            catch (SqlException e)
            {
                return false;
            }

        }



        public DataTable consultarProyecto(string nombre)
        {
            DataTable datosProyecto = new DataTable();
            string consulta = "SELECT * FROM Proyecto WHERE nombre_proyecto='" + nombre + "';";
            try
            {
                datosProyecto = acceso.ejecutarConsultaTabla(consulta);
            }
            catch (SqlException e)
            {
                datosProyecto = null;
            }

            return datosProyecto;
        }


        /*Método para eliminar de la base de datos una oficina usuaria asociada a un proyecto
        * Requiere: un string con el nombre del proyecto que se requiere eliminar para ver la oficina asociada
        * Modifica: elimina la oficina usuaria para poder eliminar el proyecto
        * Retorna: true si se llevó a cabo correctamente la eliminación y false si no fue existosa.
        */
        public bool eliminarProyecto(String nombre)
        {

            try
            {
                string borradoOficinaUsuaria = "Delete from Oficina_Usuaria where nombre_proyecto ='" + nombre + "'";
                acceso.eliminarDatos(borradoOficinaUsuaria);

                return true;
            }
            catch (SqlException e)
            {
                return false;
            }

        }

        /*Método para cancelar un proyecto
        * Requiere: un string con el nombre del proyecto que se va a cancelar
        * Modifica: el estado del proyecto
        * Retorna: true si se llevó a cabo correctamente y false si no fue existosa.
        */
        public bool cambiarEstado(String nombre)
        {
            string modif;
            try
            {
                modif = "UPDATE Proyecto SET tipo_estado = 'Cancelado' WHERE nombre_proyecto ='" + nombre + "'";
                return acceso.insertarDatos(modif);
            }
            catch (SqlException e)
            {
                return false;
            }
        }

        /*Método para llevar a cabo la modificación de los datos de la oficina usuaria
       * Requiere: un objeto con los datos de la oficina usuaria
       * Modifica: Modifica los datos de una oficina
       * Retorna: Devuelve un true si se ejecutó la actualización correctamente en la base de datos.
       */
        public bool modificarOficina(EntidadOficinaUsuaria nuevo, String cedula)
        {
            try
            {
                string modif = "UPDATE Funcionario SET id_oficina ='" + nuevo.get_id_oficina + "', nombre_oficina ='" + nuevo.get_nombre_oficina + "' , nombre_rep= '" + nuevo.get_nombre_rep + "', ape1_rep= '" + nuevo.get_ape1_rep + "', ape2_rep = '" + nuevo.ape2_rep + "';";
                return acceso.insertarDatos(modif);

            }
            catch (SqlException e)
            {
                return false;
            }
        }



        internal bool modificarProyecto()
        {
            throw new NotImplementedException();
        }
    }
}