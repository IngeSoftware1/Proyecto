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

        public bool insertarMiembro(EntidadTrabajaEn nuevo)
        {
            try
            {
                string insercion = "INSERT INTO Trabaja_En (cedula_miembro, id_proyecto) VALUES ('" + nuevo.getCedulaMiembro + "', '" + nuevo.geIdProyecto + "')";
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

        /*Método para llevar a cabo la modificación de los datos de la oficina usuaria
       * Requiere: un objeto con los datos de la oficina usuaria
       * Modifica: Modifica los datos de una oficina
       * Retorna: Devuelve un true si se ejecutó la actualización correctamente en la base de datos.
       */
        public bool modificarOficina(EntidadOficinaUsuaria nuevo, int idOficina)
        {
            try
            {
                string modif = "UPDATE Oficina_Usuaria SET  nombre_oficina ='" + nuevo.getNombreOficina + "', nombre_rep= '" + nuevo.getNombreRep + "', ape1_rep= '" + nuevo.getApe1Rep + "', ape2_rep = '" + nuevo.getApe2Rep + "WHERE id_Oficina ='" + idOficina + "';";
                return acceso.insertarDatos(modif);

            }
            catch (SqlException e)
            {
                return false;
            }
        }



        internal bool modificarProyecto(EntidadProyecto entidadP)
        {
            throw new NotImplementedException();
        }

        internal bool modificarOfUsuaria(EntidadOficinaUsuaria entidadOU)
        {
            throw new NotImplementedException();
        }

        internal bool eliminarTelefonoOficinaUsuaria(string idOficina)
        {
            throw new NotImplementedException();
        }

        internal bool eliminarTrabaja_En(string idmiembroConsultado, string idProyectoConsultado)
        {
            throw new NotImplementedException();
        }

        internal bool insertarTelefonoOficinaUsuaria(object[] datos)
        {
            throw new NotImplementedException();
        }

        internal bool eliminarTrabaja_En(object[] datos)
        {
            throw new NotImplementedException();
        }

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

        public DataTable consultarProyectoTotal(string idProyecto)
        {
            DataTable dt = new DataTable();
            string consulta;

            try
            {
                consulta = "SELECT P.nombre_proyecto, P.obj_general, P.fecha_asignacion, P.tipo_estado, F.nombre, F.apellido1 " + " FROM Proyecto P JOIN  Funcionario F ON P.cedula_creador = F.cedula WHERE P.id_proyecto ='" + idProyecto + "'"; ;
                dt = acceso.ejecutarConsultaTabla(consulta);

            }
            catch
            {
                dt = null;
            }

            return dt;
        }

        public DataTable consultarProyectos(string idMiembro)
        {
            DataTable dt = new DataTable();
            string consulta;

            if (idMiembro == null)
            {
                try
                {
                    consulta = "SELECT P.id_proyecto, P.nombre_proyecto, P.tipo_estado, O.nombre_oficina, F.nombre, F.apellido1, F.apellido2 " + " FROM Proyecto P, Oficina_Usuaria O, Funcionario F WHERE P.id_oficina = O.id_oficina AND P.cedula_lider = F.cedula ";
                    dt = acceso.ejecutarConsultaTabla(consulta);

                }
                catch
                {
                    dt = null;
                }
            }

            else
            {
                try
                {
                    consulta = "SELECT P.id_proyecto, P.nombre_proyecto, P.tipo_estado, O.nombre, F.nombre, F.apellido1, F.apellido2" + " FROM Proyecto P, Trabaja_En T,  Oficina_Usuaria O, Funcionario F WHERE P.id_proyecto = T.id_proyecto AND T.cedulaMiembro = '" + idMiembro + "'" + " AND P.id_oficina = O.id_oficina AND P.cedula_lider = F.cedula ";
                    dt = acceso.ejecutarConsultaTabla(consulta);
                }
                catch
                {
                    dt = null;
                }
            }

            return dt;
        }

        public DataTable consultarOficina(string idProyecto)
        {
            DataTable dt = new DataTable();
            string consulta;

            try
            {
                consulta = "SELECT O.id_oficina O.nombre_oficina, O.nombre_rep, O.ape1_rep, O.ape2_rep " + " FROM Oficina_Usuaria O, Proyecto P WHERE P.id_proyecto = '" + idProyecto + "'" + "AND P.id_oficina = O.id_oficina ";
                dt = acceso.ejecutarConsultaTabla(consulta);

            }
            catch
            {
                dt = null;
            }

            return dt;
        }


        public DataTable consultarTelOficina(string idProyecto)
        {
            DataTable dt = new DataTable();
            string consulta;

            try
            {
                consulta = "SELECT T.num_telefono " + " FROM Telefono_Oficina T, Proyecto P WHERE P.id_proyecto = '" + idProyecto + "'" + "AND P.id_oficina = T.id_oficina ";
                dt = acceso.ejecutarConsultaTabla(consulta);

            }
            catch
            {
                dt = null;
            }

            return dt;
        }


    }
}