using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using ProyectoInge.App_Code.Capa_de_Acceso_a_Datos;
using ProyectoInge.App_Code.Capa_de_Datos__Entidad_;
using System.Data.SqlClient;
using System.Diagnostics;

namespace ProyectoInge.App_Code.Capa_de_Acceso_a_Datos
{
    public class ControladoraBDProyectos
    {
        AccesoBaseDatos acceso = new AccesoBaseDatos();

        /*Método para insertar en la base de datos una nueva oficina usuaria
         * Requiere: un objeto tipo entidadOficinaUsuaria con los datos a guardar
         * Modifica: realiza la sentencia sql para insertar la oficina usuaria
         * Retorna: true si la inserción fue exitosa, false si no se pudo insertar en la base de datos
         */
        public bool insertarOficina(EntidadOficinaUsuaria nuevo)
        {
            try
            {
                string insercion = "INSERT INTO Oficina_Usuaria (nombre_oficina, nombre_rep, ape1_rep, ape2_rep) VALUES ('" + nuevo.getNombreOficina + "', '" + nuevo.getNombreRep + "', '" + nuevo.getApe1Rep + "', '" + nuevo.getApe2Rep + "')";
                return acceso.insertarDatos(insercion);

            }
            catch (SqlException e)
            {
                return false;
            }
        }

        /*Método para obtener el id de una oficina usuaria a partir de su nombre
         * Requiere: el nombre de la oficina a buscar
         * Modifica:crea la sentencia sql para buscar el id de la oficina a partir del nombre y ejecuta la consulta
         * Retorna: el id de la oficina, -1 si no lo encuentra
         */
        public int obtenerOficinaAgregada(string nombreOficina) 
        {
            int resultado = -1;
            DataTable datosOficina = new DataTable();
            string consulta = "SELECT id_oficina FROM Oficina_Usuaria WHERE nombre_oficina='" + nombreOficina + "';";
            try
            {
                datosOficina = acceso.ejecutarConsultaTabla(consulta);
                if (datosOficina.Rows.Count == 1)
                {
                    resultado = Int32.Parse(datosOficina.Rows[0][0].ToString());
                }

            }
            catch (SqlException e)
            {

            }

            return resultado;
        }

        /*Método para insertar los telefonos de una oficina usuaria en la base de datos
         * Requiere: un objeto tipo EntidadTelOficina con los datos a guardar
         * Modifica: crea la sentencia sql y la ejecuta
         * Retorna: true si pudo almacenar los datos en la base de datos, false si no pudo
         */
        public bool insertarTelefono(EntidadTelOficina nuevo)
        {
            try
            {
                string insercion = "INSERT INTO Telefono_Oficina (id_oficina, num_telefono) VALUES ('" + nuevo.getIdOficina + "', '" + nuevo.getNumTelefono + "')";
                return acceso.insertarDatos(insercion);

            }
            catch (SqlException e)
            {
                return false;
            }
        }

        /*Método para insertar en la base de datos un nuevo proyecto
         * Requiere: un objeto tipo entidadProyecto con los datos a guardar
         * Modifica: realiza la sentencia sql para insertarel proyecto
         * Retorna: true si la inserción fue exitosa, false si no se pudo insertar en la base de datos
         */
        public bool insertarProyecto(EntidadProyecto nuevo)
        {
            try
            {
                string insercion = "INSERT INTO Proyecto (nombre_proyecto, obj_general, fecha_asignacion, tipo_estado, cedula_creador, cedula_lider, id_oficina) VALUES ('" + nuevo.getNombreProyecto + "', '" + nuevo.getObjGeneral + "', '" + nuevo.getFechaAsignacion + "', '" + nuevo.getTipoEstado + "', '" +nuevo.getCedulaCreador+ "', '"+nuevo.getCedulaLider+"', '"+nuevo.getIdOficina+"')";
         
                return acceso.insertarDatos(insercion);

            }
            catch (SqlException e)
            {
                return false;
            }
        }

        public bool insertarRequerimiento(EntidadRequerimiento nuevo)
        {
            try
            {
                string insercion = "INSERT INTO Requerimiento (id_req, id_proyecto, id_diseno, nombre_req) VALUES ('" + nuevo.getIdReq + "', '" + nuevo.getIdProyecto + "', '" + nuevo.getIdDiseno + "', '" + nuevo.getNombreReq + "')";
                return acceso.insertarDatos(insercion);

            }
            catch (SqlException e)
            {
                return false;
            }
        }

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
		
        /*Método para eliminar un proyecto de la BD
        * Requiere: el id del proyecto
        * Modifica: la BD
        * Retorna: boolean si logra eliminar
        */

        public bool eliminarProyecto(int idProyecto)
        {
            try
            {
                string borradoProyecto = "Delete from Proyecto where id_proyecto ='" + idProyecto + "';";
                acceso.eliminarDatos(borradoProyecto);
                return true;
            }
            catch (SqlException e)
            {
                return false;
            }

        }

        /*Método para obtiene el id de un proyecto apartir de su nombre
        * Requiere: el nombre del proyecto del cual se quiere saber el id
        * Modifica: consulta el id de un proyecto
        * Retorna: el id de un proyecto asociado a un nombre
        */
        public int obtenerIdProyecto(string nombre)
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


        /*Método para obtiene el id de una oficina apartir del id del proyecto asociado a esa oficina
        * Requiere: el id del proyecto asociado a la oficina que se dese conocer el id
        * Modifica: consulta el id de una oficina
        * Retorna: el id de una oficina
        */
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
            bool indicador = false;
            int resultado = consultarOficinaProyecto(idProyecto);
            DataTable datosProyecto = new DataTable();
            try
            {
                string borrarOficina = "Delete from Oficina_Usuaria where id_oficina ='"+resultado+"';";
                acceso.eliminarDatos(borrarOficina);
                indicador = true;
            }
            catch (SqlException e)
            {
                indicador = false;
            }

            return indicador;

        }

        /*Método para eliminar de la base de datos una oficina usuaria asociada a un proyecto
        * Requiere: el id de la oficina que se requiere eliminar 
        * Modifica: elimina la oficina usuaria para poder eliminar el proyecto
        * Retorna: true si se llevó a cabo correctamente la eliminación y false si no fue existosa.
        */
        public bool eliminarOficina(int idOficina)
        {       
            try
            {
                string borrarOficina = "Delete from Oficina_Usuaria where id_oficina ='" + idOficina + "';";
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
                modif = "UPDATE Proyecto SET tipo_estado = 'Cancelado' WHERE id_proyecto ='" + idProyecto + "'";
                return acceso.insertarDatos(modif);
            }
            catch (SqlException e)
            {
                return false;
            }
        }


      /*Método para consultar el estado del proyecto
      * Requiere: el id del proyecto
      * Modifica: no modifica datos
      * Retorna: Devuelve un string que indica el estado del proyecto
      */
        public string consultarEstadoProyecto(int idProyecto)
        {
            string resultado = "";
            DataTable datosProyecto = new DataTable();
            string consulta = "SELECT tipo_estado FROM Proyecto WHERE id_proyecto='" + idProyecto + "';";
            try
            {
                datosProyecto = acceso.ejecutarConsultaTabla(consulta);
                if (datosProyecto.Rows.Count > 0)
                {
                    resultado = datosProyecto.Rows[0][0].ToString();
                }

            }
            catch (SqlException e)
            {

            }

            return resultado;
        }




        /*Método para llevar a cabo la modificación de los datos de la oficina usuaria
       * Requiere: un objeto con los datos de la oficina usuaria
       * Modifica: Modifica los datos de una oficina
       * Retorna: Devuelve un true si se ejecutó la actualización correctamente en la base de datos.
       */
        public bool modificarOfUsuaria(EntidadOficinaUsuaria nuevo, string idOficina)
        {
            try
            {
                string modif = "UPDATE Oficina_Usuaria SET  nombre_oficina ='" + nuevo.getNombreOficina + "', nombre_rep= '" + nuevo.getNombreRep + "', ape1_rep= '" + nuevo.getApe1Rep + "', ape2_rep = '" + nuevo.getApe2Rep + "' WHERE id_Oficina ='" + idOficina + "';";
                Debug.Write(modif);
                return acceso.insertarDatos(modif);

            }
            catch (SqlException e)
            {
                return false;
            }
        }


        //recibe datos del proyecto y id del proyecto
        public bool modificarProyecto(EntidadProyecto entidadP, String idProyecto)
        {
            try
            {
                string modif = "UPDATE Proyecto SET nombre_proyecto ='" + entidadP.getNombreProyecto + "', obj_general = '" + entidadP.getObjGeneral + "', fecha_asignacion = '" + entidadP.getFechaAsignacion + "', tipo_estado = '" + entidadP.getTipoEstado +"', cedula_lider = '"+ entidadP.getCedulaLider + "', id_oficina = '" +entidadP.getIdOficina + "' WHERE id_proyecto ='" + idProyecto + "';";
                return acceso.insertarDatos(modif);

            }
            catch (SqlException e)
            {
                return false;
            }
        }

       

       public bool eliminarTelefonoOficinaUsuaria(int idOficina)
        {
            try
            {
                string borrado = "Delete from Telefono_Oficina where id_Oficina = '" + idOficina + "';" ;
                return acceso.eliminarDatos(borrado);
            }
            catch (SqlException e)
            {
                return false;
            }
        }

       /*Método para insertar los telefonos de las oficinas usuarias
       * Requiere: la entidad de telefonos
       * Modifica: modifica la tabla telefonos usuario
       * Retorna:booleano si logra insertar el telefono
       */
        public bool insertarTelefonoOficinaUsuaria(EntidadTelOficina nuevo)
        {
            try
            {
                string insercion = "INSERT INTO Telefono_Oficina (id_Oficina, num_telefono) VALUES ('" + nuevo.getIdOficina + "', '" + nuevo.getNumTelefono + "')";
                return acceso.insertarDatos(insercion);

            }
            catch (SqlException e)
            {
                return false;
            }
        }

        /* Método para obtener los estados que podría contener un proyecto.
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

                consulta = "SELECT E.tipo_estado " + " FROM Estado_Proceso E ";
                dt = acceso.ejecutarConsultaTabla(consulta);

            }
            catch
            {
                dt = null;
            }

            return dt;
        }

        /*Método para consultar un proyecto
       * Requiere: un string con el id del proyecto que se va a consultar
       * Modifica: no realiza modificaciones
       * Retorna: un DataTable con los resultados de la consulta 
       */
        public DataTable consultarProyectoTotal(string idProyecto)
        {
            DataTable dt = new DataTable();
            string consulta;

            try
            {
                consulta = "SELECT P.nombre_proyecto, P.obj_general, P.fecha_asignacion, P.tipo_estado, P.cedula_lider " + " FROM Proyecto P JOIN  Funcionario F ON P.cedula_creador = F.cedula  WHERE P.id_proyecto ='" + idProyecto + "'";
                dt = acceso.ejecutarConsultaTabla(consulta);

            }
            catch (SqlException e)
            {
                dt = null;
            }

            return dt;
        }

         /*Método para consultar los identificadores de los proyectos
       * Requiere: no requiere informacion
       * Modifica: no realiza modificaciones
       * Retorna: un DataTable con los id de los proyectos 
       */
        public DataTable consultarIdenficadoresProyectos()
        {
            DataTable dt = new DataTable();
            string consulta;

            try
            {
                consulta = "SELECT id_proyecto From Proyecto";
                dt = acceso.ejecutarConsultaTabla(consulta);

            }
            catch (SqlException e)
            {
                dt = null;
            }

            return dt;

        }

        /*Método para consultar los nombres de los proyectos
      * Requiere: no requiere informacion
      * Modifica: no realiza modificaciones
      * Retorna: un DataTable con los nombres de los proyectos 
      */
        public DataTable consultarNombresProyectos()
        {
            DataTable dt = new DataTable();
            string consulta;

            try
            {
                consulta = "SELECT nombre_proyecto, id_proyecto From Proyecto where nombre_proyecto != 'Dummy' ";
                dt = acceso.ejecutarConsultaTabla(consulta);

            }
            catch (SqlException e)
            {
                dt = null;
            }

            return dt;

        }



        /*Método para consultar todos los proyectos en caso de que el usuario utilizando el sistema sea el administrador, sino solamente
        * en los proyectos que se encuentra asociado en caso de que el usuario sea un miembro
        * Requiere: un DataTable con los identificadores de el/los proyecto(s) en los cuales el miembro labora en caso de que éste sea el usuario,
        * sino se envía null. 
        * Modifica: no realiza modificaciones
        * Retorna: un DataTable con los resultados de la consulta 
        */
        public DataTable consultarProyectos(DataTable idProyectos)
        {
            DataTable dt = new DataTable();
            string consulta = "";
            int contador = 0;

            if (idProyectos == null) //Si es null significa que el usuario del sistema es el administrador
            {
                try
                {
                    consulta = "SELECT P.id_proyecto, P.nombre_proyecto, P.tipo_estado, O.nombre_oficina, P.cedula_lider " + " FROM Proyecto P, Oficina_Usuaria O WHERE P.id_oficina = O.id_oficina ORDER BY P.id_proyecto DESC";
                    dt = acceso.ejecutarConsultaTabla(consulta);

                }
                catch (SqlException e)
                {
                    dt = null;
                }
            }

            else
            {
                try
                {

                    for (int i = 0; i < idProyectos.Rows.Count; ++i)
                    {
                        ++contador;
                      
                        consulta = consulta + " " + "SELECT P.id_proyecto, P.nombre_proyecto, P.tipo_estado, O.nombre_oficina, P.cedula_lider FROM Proyecto P, Oficina_Usuaria O  WHERE P.id_oficina = O.id_oficina AND P.id_proyecto = '" + idProyectos.Rows[i][0].ToString() + "'";
                        if (contador != idProyectos.Rows.Count)
                        {
                            consulta = consulta + "UNION";
                        }

                    }

                    dt = acceso.ejecutarConsultaTabla(consulta);

                }
                catch (SqlException e)
                {
                    dt = null;
                }
            }

            return dt;
        }

        /*Método para consultar una oficina
        * Requiere: un string con el id del proyecto para consultar la oficina asociada a éste.
        * Modifica: no realiza modificaciones
        * Retorna: un DataTable con los resultados de la consulta sobre la oficina
        */
        public DataTable consultarOficina(string idProyecto)
        {
            DataTable dt = new DataTable();
            string consulta;

            try
            {
                consulta = "SELECT P.id_oficina, O.nombre_oficina, O.nombre_rep, O.ape1_rep, O.ape2_rep " + " FROM Oficina_Usuaria O, Proyecto P WHERE P.id_proyecto = '" + idProyecto + "'" + "AND P.id_oficina = O.id_oficina ";
                dt = acceso.ejecutarConsultaTabla(consulta);

            }
            catch (SqlException e)
            {
                dt = null;
            }

            return dt;
        }


        /*Método para consultar los teléfonos de una oficina
        * Requiere: un string con el id del proyecto para consultar los teléfonos de la oficina asociada a éste.
        * Modifica: no realiza modificaciones
        * Retorna: un DataTable con los resultados de la consulta sobre los teléfonos de la oficina. 
        */
        public DataTable consultarTelOficina(string idProyecto)
        {
            DataTable dt = new DataTable();
            string consulta;

            try
            {
                consulta = "SELECT T.num_telefono " + " FROM Telefono_Oficina T, Proyecto P WHERE P.id_proyecto = '" + idProyecto + "'" + "AND P.id_oficina = T.id_oficina ";
                dt = acceso.ejecutarConsultaTabla(consulta);

            }
            catch (SqlException e)
            {
                dt = null;
            }

            return dt;
        }


        

        /* Método para consultar requerimientos de un diseño
        * Requiere: el id del diseño y el proyecto al que pertenece
        * Modifica: no modifica datos
        * Retorna: un DataTable que contiene los requerimientos del diseño
        */
        public DataTable consultarReqProyecto(int idProyecto)
        {
            DataTable dt = new DataTable();
            string consulta;

            try
            {
                consulta = "SELECT *  FROM Requerimiento WHERE id_proyecto = '" + idProyecto + "';";
                dt = acceso.ejecutarConsultaTabla(consulta);

            }
            catch (SqlException e)
            {
                dt = null;
            }

            return dt;
        }


        public bool actualizarRequerimiento(Object[] datos)
        {
            try
            {
                string actualizacion = "UPDATE Requerimiento SET id_diseno ='" + datos[2] + "' WHERE id_req ='" + datos[0] + "' AND id_proyecto = '" + datos[1] + "' AND nombre_req = '" + datos[3] + "';";
                return acceso.insertarDatos(actualizacion);

            }
            catch (SqlException e)
            {
                return false;
            }
        }

        internal bool eliminarRequerimiento(string sigla, string nombreReq, int idProyecto)
        {
            try
            {
                string borrarRequerimiento = "Delete from Requerimiento where id_proyecto ='" + idProyecto + " AND id_req ="+sigla + " AND nombre_req = " + nombreReq +" ';";
                acceso.eliminarDatos(borrarRequerimiento);
                return true;
            }
            catch (SqlException e)
            {
                return false;
            }
        }

        //metodo para traer proyectos si es lider
        public DataTable consultarProyectosLider(string cedula)
        {
            DataTable dt = new DataTable();
            string consulta;

            try
            {
                consulta = "SELECT nombre_proyecto, id_proyecto WHERE cedula_lider='"+cedula+"';";
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