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
        * Requiere: con el id del proyecto que se va a eliminar
        * Modifica: eliminar el caso de prueba asociado al proyecto
        * Retorna: true si se llevó a cabo correctamente la eliminación y false si no fue existosa.
        */
        public bool eliminarProyectoCasoPueba(int idProyecto)
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


        //Método para consultar el id del proyecto
        
        public int consultarProyecto(string nombre)
        {
            int resultado = -1;
            DataTable datosProyecto = new DataTable();
            string consulta = "SELECT id_proyecto FROM Proyecto WHERE nombre_proyecto='" + nombre + "';";
            try
            {
                datosProyecto = acceso.ejecutarConsultaTabla(consulta);
                if(datosProyecto.Rows.Count==1){
                    resultado = Int32.Parse(datosProyecto.Rows[0][0].ToString());
                }
                
            }
            catch (SqlException e)
            {
                
            }

            return resultado ;
        }


        //metodo para consultar el id de la oficina de un proyecto
        public int consultarOficinaProyecto(int idProyecto)
        {
            int resultado = -1;
            DataTable datosProyecto = new DataTable();
            string consulta = "SELECT id_oficina FROM Proyecto WHERE id_proyecto='" + idProyecto + "';";
            try
            {
                datosProyecto = acceso.ejecutarConsultaTabla(consulta);
                if (datosProyecto.Rows.Count == 1)
                {
                    resultado = Int32.Parse(datosProyecto.Rows[0][0].ToString());
                }

            }
            catch (SqlException e)
            {

            }

            return resultado;
        }


        /*Método para eliminar de la base de datos una oficina usuaria asociada a un proyecto
        * Requiere: el id del proyecto que se requiere eliminar para ver la oficina asociada
        * Modifica: elimina la oficina usuaria para poder eliminar el proyecto
        * Retorna: true si se llevó a cabo correctamente la eliminación y false si no fue existosa.
        */
        public bool eliminarOficinaProyecto(int idProyecto)
        {

            int resultado = consultarOficinaProyecto(idProyecto);
            DataTable datosProyecto = new DataTable();
            try
            {
                string borrarOficina = "Delete from Oficina_Usuaria where id_oficina ='" + resultado+ "';";
                acceso.eliminarDatos(borrarOficina);
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
        public bool cambiarEstado(int idProyecto)
        {
            string modif;
            try
            {
                modif = "UPDATE Proyecto SET tipo_estado = 'Cancelado' WHERE id_proyecto ='" + IdProyecto + "'";
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
        public bool modificarOficina(EntidadOficinaUsuaria nuevo, int idOficina)
        {
            try
            {
                string modif = "UPDATE Oficina_Usuaria SET  nombre_oficina ='" + nuevo.get_nombre_oficina + "' , nombre_rep= '" + nuevo.get_nombre_rep + "', ape1_rep= '" + nuevo.get_ape1_rep + "', ape2_rep = '" + nuevo.ape2_rep + "WHERE id_Oficina ='" + idOficina + "';";
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