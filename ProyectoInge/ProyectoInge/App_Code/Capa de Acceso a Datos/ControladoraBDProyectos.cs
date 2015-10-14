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
		
		/*Método para eliminar de la base de datos un funcionario específico
        * Requiere: un string con la cedula de un funcionario específico
        * Modifica: Genera las consultas para mandarlas a la base de datos para borrar el funcionario de la tabla Funcionario y Trabaja_En
        * Retorna: true si se llevó a cabo correctamente la eliminación y false si no fue existosa.
        */
        public bool eliminarProyectoAdminitrador(int idProyecto)
        {
            try
            {
                string borradoProyecto = "Delete from Proyecto where id_proyecto ='" + idProyecto + "';";
                acceso.eliminarDatos(borradoProyecto);

                string borradoOficinaUsuaria = "Delete from Oficina_Usuaria where id_proyecto ='" + idProyecto + "';";
                acceso.eliminarDatos(borradoOficinaUsuaria);
                //string borradoTrabajaEn = "Delete from Trabaja_En where id_proyecto ='" + idProyecto + "';";
                //acceso.eliminarDatos(borradoTrabajaEn);
                
                return true;
            }
            catch (SqlException e)
            {
                return false;
            }

        }

        public DataTable consultarOficina(string nombre)
        {
            DataTable datosFuncionario = new DataTable();
            string consulta = "SELECT * FROM Proyecto WHERE nombre_proyecto='" + nombre + "';";
            try
            {
                datosFuncionario = acceso.ejecutarConsultaTabla(consulta);
            }
            catch (SqlException e)
            {
                datosFuncionario = null;
            }

            return datosFuncionario;
        }

        public bool eliminarProyecto(String nombre)
        {

            try
            {
                string borrado = "Delete from Proyecto where nombre_proyecto ='" + nombre + "'";
                acceso.eliminarDatos(borrado);


                string borradoOficinaUsuaria = "Delete from Oficina_Usuaria where nombre_proyecto ='" + nombre + "'";
                acceso.eliminarDatos(borradoOficinaUsuaria);

                return true;
            }
            catch (SqlException e)
            {
                return false;
            }

        }


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



        internal bool modificarProyecto()
        {
            throw new NotImplementedException();
        }
    }
}