﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using ProyectoInge.App_Code.Capa_de_Datos__Entidad_;
using System.Data.SqlClient;
using System.Diagnostics;


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

        public int obtenerIdDisenoPorProposito(string proposito)
        {
            int resultado = -1;

            try
            {
                string consultaDiseno = "SELECT id_diseno FROM Diseno_Pruebas WHERE proposito_diseno ='" + proposito + "'" + "ORDER BY id_diseno DESC ";
                DataTable dataDiseno = acceso.ejecutarConsultaTabla(consultaDiseno);
                if (dataDiseno.Rows.Count >= 1)
                {
                    resultado = Convert.ToInt32(dataDiseno.Rows[0][0].ToString());
                }
            }
            catch (SqlException e)
            {
            }

            return resultado;
        }


        /* Método para obtener los niveles que puede tener un diseño
        * Requiere: no requiere informacion
        * Modifica: no modifica datos
        * Retorna: un DataTable que contiene los niveles
        */
        public DataTable consultarNiveles()
        {
            DataTable dt = new DataTable();
            string consulta;

            try
            {

                consulta = "SELECT nivel_Prueba FROM Nivel_Prueba ";
                dt = acceso.ejecutarConsultaTabla(consulta);

            }
            catch
            {
                dt = null;
            }

            return dt;
        }


        /* Método para obtener los tipos que puede tener un diseño
        * Requiere: no requiere informacion
        * Modifica: no modifica datos
        * Retorna: un DataTable que contiene los tipos
        */
        public DataTable consultarTipos()
        {
            DataTable dt = new DataTable();
            string consulta;

            try
            {

                consulta = "SELECT tipo_Prueba FROM Tipo_Prueba ";
                dt = acceso.ejecutarConsultaTabla(consulta);

            }
            catch
            {
                dt = null;
            }

            return dt;
        }


        /* Método para obtener las tecnicas que puede tener un diseño
        * Requiere: no requiere informacion
        * Modifica: no modifica datos
        * Retorna: un DataTable que contiene las tecnicas
        */
        public DataTable consultarTecnicas()
        {
            DataTable dt = new DataTable();
            string consulta;

            try
            {

                consulta = "SELECT tipo_Tecnica FROM Tecnica ";
                dt = acceso.ejecutarConsultaTabla(consulta);

            }
            catch
            {
                dt = null;
            }

            return dt;
        }



        /* Método para consultar un diseño
        * Requiere: un int con el identificador del diseño
        * Modifica: no modifica datos
        * Retorna: un DataTable que contiene los datos del diseño
        */
        public DataTable consultarDiseno(int idDiseño)
        {
            DataTable dt = new DataTable();
            string consulta;

            try
            {

                consulta = "SELECT * FROM Diseno_Pruebas WHERE id_diseno = '" + idDiseño + "';";
                dt = acceso.ejecutarConsultaTabla(consulta);

            }
            catch
            {
                dt = null;
            }

            return dt;
        }


        /* Método para consultar un diseño
        * Requiere: un int con el identificador del diseño
        * Modifica: no modifica datos
        * Retorna: un DataTable que contiene los datos del diseño
        */
        public DataTable consultarNomPropositoDiseno(int idProyecto)
        {
            DataTable dt = new DataTable();
            string consulta;

            try
            {

                consulta = "SELECT id_diseno, proposito_diseno FROM Diseno_Pruebas WHERE id_proyecto = '" + idProyecto + "';";
                dt = acceso.ejecutarConsultaTabla(consulta);

            }
            catch
            {
                dt = null;
            }

            return dt;
        }

        /*Método para modificar un requerimiento asociado a un diseño
        * Requiere: las llaves de la tabla requerimientos diseño y la instancia de la entidad 
        * Modifica: modifica la tabla de Diseno_Pruebas
        * Retorna:booleano si logra eliminar el diseño
        */
        public bool modificarRequerimientosDiseño(EntidadRequerimientoDiseño entRequerimientosDiseño, Object[] datos)
        {
            try
            {
                string modif = "UPDATE Requerimiento_Diseno SET id_diseno ='" + entRequerimientosDiseño.getIdDiseno + "', id_req = '" + entRequerimientosDiseño.getIdReq + "', id_proyecto = '" + +entRequerimientosDiseño.getIdProyecto + "' WHERE where id_diseno ='" + datos[0] + "and id_req ='" + datos[1] + " and id_proyecto='" + datos[2] + "';";
                return acceso.insertarDatos(modif);

            }
            catch (SqlException e)
            {
                return false;
            }
        }


        /*Método para modificar un diseño
        * Requiere: el id del diseño
        * Modifica: modifica la tabla de Diseno_Pruebas
        * Retorna:booleano si logra modificar el diseño
        */
        public bool modificarDiseño(EntidadDiseno nuevo, int identificador)
        {
            string consulta = "";
            bool resultado = false;
            try
            {
                if (nuevo.getTecnica == "Seleccione" && nuevo.getNivel == "Seleccione")
                {
                    consulta = "UPDATE Diseno_Pruebas SET proposito_diseno ='" + nuevo.getProposito + "', fecha = '" + nuevo.getFecha + "', procedimiento_diseno = '" + nuevo.getProcedimiento + "', ambiente_diseno = '" + nuevo.getAmbiente + "', criterios_aceptacion = '" + nuevo.getCriterio + "', tecnica = " + "null" + ", nivel = " + "null" + ", id_proyecto = '" + nuevo.getIdProyecto + "', cedula_responsable = '" + nuevo.getCedulaResponsable + "' WHERE id_diseno ='" + identificador + "';";
                    resultado = acceso.insertarDatos(consulta);
                }
                else if (nuevo.getNivel == "Seleccione")
                {
                    consulta = "UPDATE Diseno_Pruebas SET proposito_diseno ='" + nuevo.getProposito + "', fecha = '" + nuevo.getFecha + "', procedimiento_diseno = '" + nuevo.getProcedimiento + "', ambiente_diseno = '" + nuevo.getAmbiente + "', criterios_aceptacion = '" + nuevo.getCriterio + "', tecnica = '" + nuevo.getTecnica + "', nivel = " + "null" + ", id_proyecto = '" + nuevo.getIdProyecto + "', cedula_responsable = '" + nuevo.getCedulaResponsable + "' WHERE id_diseno ='" + identificador + "';";
                    resultado = acceso.insertarDatos(consulta);
                }
                else if (nuevo.getTecnica == "Seleccione")
                {
                    consulta = "UPDATE Diseno_Pruebas SET proposito_diseno ='" + nuevo.getProposito + "', fecha = '" + nuevo.getFecha + "', procedimiento_diseno = '" + nuevo.getProcedimiento + "', ambiente_diseno = '" + nuevo.getAmbiente + "', criterios_aceptacion = '" + nuevo.getCriterio + "', tecnica = " + "null" + ", nivel = '" + nuevo.getNivel + "', id_proyecto = '" + nuevo.getIdProyecto + "', cedula_responsable = '" + nuevo.getCedulaResponsable + "' WHERE id_diseno ='" + identificador + "';";
                    resultado = acceso.insertarDatos(consulta);
                }
                else
                {
                    consulta = "UPDATE Diseno_Pruebas SET proposito_diseno ='" + nuevo.getProposito + "', fecha = '" + nuevo.getFecha + "', procedimiento_diseno = '" + nuevo.getProcedimiento + "', ambiente_diseno = '" + nuevo.getAmbiente + "', criterios_aceptacion = '" + nuevo.getCriterio + "', tecnica = '" + nuevo.getTecnica + "', nivel = '" + nuevo.getNivel + "', id_proyecto = '" + nuevo.getIdProyecto + "', cedula_responsable = '" + nuevo.getCedulaResponsable + "' WHERE id_diseno ='" + identificador + "';";
                    resultado = acceso.insertarDatos(consulta);
                }
            }
            catch (SqlException e)
            {
                return false;
            }
            return resultado;

        }
        /*Método para consultar todos los diseños en caso de que el usuario utilizando el sistema sea el administrador, sino solamente
        * en los diseños que se encuentra asociado en caso de que el usuario sea un miembro
        * Requiere: un DataTable con los identificadores de el/los diseño(s) en los cuales el miembro labora en caso de que éste sea el usuario,
        * sino se envía null. 
        * Modifica: no realiza modificaciones
        * Retorna: un DataTable con los resultados de la consulta 
        */
        public DataTable consultarDisenos(DataTable idProyectos)
        {
            DataTable dt = new DataTable();
            string consulta = "";
            int contador = 0;

            if (idProyectos == null) //Si es null significa que el usuario del sistema es el administrador
            {
                try
                {
                    consulta = "SELECT D.id_diseno, D.proposito_diseno, D.tecnica, D.nivel, D.cedula_responsable, D.id_proyecto FROM Diseno_Pruebas D;";
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

                        consulta = "SELECT D.id_diseno, D.proposito_diseno, D.tecnica, D.nivel, D.cedula_responsable, D.id_proyecto FROM Diseno_Pruebas D WHERE D.id_proyecto='" + idProyectos.Rows[i][0].ToString() + "'";
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

        public DataTable consultarDisenosPorReq(DataTable reqs)
        {
            DataTable dt = new DataTable();
            string consulta = "";
            int contador = 0;
                try
                {

                    for (int i = 0; i < reqs.Rows.Count; ++i)
                    {
                        ++contador;

                        consulta = "Select id_diseno from Requerimiento_Diseno where id_req'" + reqs.Rows[i][0].ToString() + "'";
                        if (contador != reqs.Rows.Count)
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
            return dt;
        }

         public DataTable consultarPropositosDisenosPorId(DataTable idDisenos)
        {
            DataTable dt = new DataTable();
            string consulta = "";
            int contador = 0;
            try
            {

                for (int i = 0; i < idDisenos.Rows.Count; ++i)
                {
                    ++contador;

                    consulta = "Select proposito_diseno from Diseno_Pruebas where id_diseno = '" + idDisenos.Rows[i][0].ToString() + "'";
                    if (contador != idDisenos.Rows.Count)
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
            return dt;
        }

        public int obtenerIdReqPorNombre(string s)
        {
            string consulta = "Select id_req from Requerimiento where nombre_req = '" + s + "';"; ;
            int resultado = -1;
            DataTable dt = new DataTable();
            try
            {
                dt = acceso.ejecutarConsultaTabla(consulta);
                if (dt.Rows.Count == 1)
                {
                    resultado = Int32.Parse(dt.Rows[0][0].ToString());
                }
            }
            catch (SqlException e)
            {
            }
            return resultado;
        }

        /*Método para insertar un diseño
        * Requiere: la entidad de diseño
        * Modifica: modifica la tabla Diseno_Pruebas
        * Retorna:booleano si logra insertar el diseño
        */
        public bool insertarDiseno(EntidadDiseno nuevo)
        {
            try
            {
                if (nuevo.getTecnica == "Seleccione" && nuevo.getNivel == "Seleccione")
                {

                    string insercion = "INSERT INTO Diseno_Pruebas (proposito_diseno, fecha, procedimiento_diseno, ambiente_diseno, criterios_aceptacion, tecnica, nivel, id_proyecto, cedula_responsable) VALUES ('" + nuevo.getProposito + "', '" + nuevo.getFecha + "', '" + nuevo.getProcedimiento + "','" + nuevo.getAmbiente + "', '" + nuevo.getCriterio + "', null, null, '" + nuevo.getIdProyecto + "', '" + nuevo.getCedulaResponsable + "')";
                    return acceso.insertarDatos(insercion);
                }

                else if (nuevo.getNivel == "Seleccione")
                {
                    string insercion = "INSERT INTO Diseno_Pruebas (proposito_diseno, fecha, procedimiento_diseno, ambiente_diseno, criterios_aceptacion, tecnica, nivel, id_proyecto, cedula_responsable) VALUES ('" + nuevo.getProposito + "', '" + nuevo.getFecha + "', '" + nuevo.getProcedimiento + "','" + nuevo.getAmbiente + "', '" + nuevo.getCriterio + "', '" + nuevo.getTecnica + "', null , '" + nuevo.getIdProyecto + "', '" + nuevo.getCedulaResponsable + "')";
                    return acceso.insertarDatos(insercion);
                }
                else if (nuevo.getTecnica == "Seleccione")
                {
                    string insercion = "INSERT INTO Diseno_Pruebas (proposito_diseno, fecha, procedimiento_diseno, ambiente_diseno, criterios_aceptacion, tecnica, nivel, id_proyecto, cedula_responsable) VALUES ('" + nuevo.getProposito + "', '" + nuevo.getFecha + "', '" + nuevo.getProcedimiento + "','" + nuevo.getAmbiente + "', '" + nuevo.getCriterio + "', null , '" + nuevo.getNivel + "', '" + nuevo.getIdProyecto + "', '" + nuevo.getCedulaResponsable + "')";
                    return acceso.insertarDatos(insercion);
                }
                else
                {
                    string insercion = "INSERT INTO Diseno_Pruebas (proposito_diseno, fecha, procedimiento_diseno, ambiente_diseno, criterios_aceptacion, tecnica, nivel, id_proyecto, cedula_responsable) VALUES ('" + nuevo.getProposito + "', '" + nuevo.getFecha + "', '" + nuevo.getProcedimiento + "','" + nuevo.getAmbiente + "', '" + nuevo.getCriterio + "', '" + nuevo.getTecnica + "', '" + nuevo.getNivel + "', '" + nuevo.getIdProyecto + "', '" + nuevo.getCedulaResponsable + "')";
                    return acceso.insertarDatos(insercion);
                }

            }
            catch (SqlException e)
            {
                return false;
            }

        }

        /*Método para insertar un requerimiento de diseño
        * Requiere: la entidad de requerimientoDiseño
        * Modifica: modifica la tabla Requerimiento_Diseno
        * Retorna:booleano si logra insertar el requerimiento asociado al diseño
        */
        public bool insertarRequerimientoDiseño(EntidadRequerimientoDiseño nuevo)
        {
            try
            {
                string insercion = "INSERT INTO Requerimiento_Diseno (id_diseno, id_req, id_proyecto) VALUES ('" + nuevo.getIdDiseno + "', '" + nuevo.getIdReq + "', '" + nuevo.getIdProyecto + "')";
                return acceso.insertarDatos(insercion);

            }
            catch (SqlException e)
            {
                return false;
            }
        }

        /*Método para eliminar un diseño
        * Requiere: el id del diseño
        * Modifica: modifica la tabla de Diseno_Pruebas
        * Retorna:booleano si logra eliminar el diseño
        */
        public bool eliminarDiseño(int idDiseño)
        {
            try
            {
                string borradoProyecto = "Delete from Diseno_Pruebas where id_diseno ='" + idDiseño + "';";
                acceso.eliminarDatos(borradoProyecto);
                return true;

            }
            catch (SqlException e)
            {
                return false;
            }
        }


        /*Método para eliminar un requerimiento asociado a un diseño
        * Requiere: el id del diseño
        * Modifica: modifica la tabla de Diseno_Pruebas
        * Retorna:booleano si logra eliminar el diseño
        */
        public bool eliminarRequerimientoDiseño(int idD)
        {
            try
            {
                string borradoProyecto = "Delete from Requerimiento_Diseno where id_diseno ='" + idD + "';";
                acceso.eliminarDatos(borradoProyecto);
                return true;

            }
            catch (SqlException e)
            {
                return false;
            }
        }

        /* Método para consultar requerimientos de un diseño
        * Requiere: el id del diseño y el proyecto al que pertenece
        * Modifica: no modifica datos
        * Retorna: un DataTable que contiene los requerimientos del diseño
        */
        public DataTable consultarReqDisenoDeProyecto(int idDiseño, int idProyecto)
        {
            DataTable dt = new DataTable();
            string consulta;

            try
            {
                consulta = "SELECT *  FROM Requerimiento where id_req in (Select id_req From Requerimiento_Diseno WHERE id_proyecto = '" + idProyecto + "'AND id_diseno='" + idDiseño + "');";
                dt = acceso.ejecutarConsultaTabla(consulta);

            }
            catch (SqlException e)
            {
                dt = null;
            }

            return dt;
        }
        /* Método para consultar disenos asociados a un proyecto
        * Requiere: el id del proyecto
        * Modifica: no modifica datos
        * Retorna: un DataTable que contiene los disenos asociados al proyecto
        */
        public DataTable consultarDisenoAsociadoAProyecto(int idProyecto)
        {
            DataTable dt = new DataTable();
            string consulta = "";
            try
            {
                consulta = "SELECT D.id_diseno, D.proposito_diseno, D.tecnica, D.nivel, D.cedula_responsable FROM Diseno_Pruebas D WHERE D.id_proyecto='" + idProyecto + "';";
                dt = acceso.ejecutarConsultaTabla(consulta);
            }
            catch (SqlException e)
            {
                dt = null;
            }
            return dt;
        }
        /* Método para eliminar los requerimientos de diseno
        * Requiere: el identificador de el requrimieto, el id del proyecto
        * Modifica: no modifica datos
        * Retorna: un DataTable que contiene los requerimientos diseno
        */
        public bool eliminarRequerimientoDiseño(string sigla, int idProyecto)
        {
            try
            {
                string borrarRequerimiento = "Delete from Requerimiento_Diseno where id_proyecto ='" + idProyecto + "' AND id_req = '" + sigla + "';";
                acceso.eliminarDatos(borrarRequerimiento);
                return true;
            }
            catch (SqlException e)
            {
                return false;
            }
        }

        public DataTable getDatosDiseno(int idDiseno)
        {
            DataTable dt = new DataTable();
            string consulta = "";
            try
            {
                consulta = "SELECT D.proposito_diseno, D.tecnica, D.nivel, D.procedimiento_diseno, D.id_proyecto FROM Diseno_Pruebas D WHERE D.id_diseno= " + idDiseno;
                dt = acceso.ejecutarConsultaTabla(consulta);
            }
            catch (SqlException e)
            {
                dt = null;
            }
            return dt;
        }


        public DataTable getReqDiseno(int idDiseno, int idProyecto)
        {
            DataTable dt = new DataTable();
            string consulta = "";
            try
            {
                consulta = "SELECT R.id_req FROM Requerimiento_Diseno R WHERE R.id_proyecto = " + idProyecto + " AND id_diseno=" + idDiseno + ";";
                dt = acceso.ejecutarConsultaTabla(consulta);
            }
            catch (SqlException e)
            {
                dt = null;
            }
            return dt;
        }

        public DataTable consultarDisenosCasos(int idProyecto)
        {
            DataTable dt = new DataTable();
            string consulta = "";
            try
            {
                consulta = "SELECT DISTINCT D.id_diseno, D.proposito_diseno FROM Diseno_Pruebas D, Caso_Prueba C WHERE D.id_diseno = C.id_diseno AND D.id_proyecto = " + idProyecto;
                dt = acceso.ejecutarConsultaTabla(consulta);
            }
            catch (SqlException e)
            {
                dt = null;
            }
            return dt;
            
        }


        /* Método para obtener el nombre de todos los disenos y el nombre del proyecto asociado a cada diseno.
        * Requiere: nada
        * Modifica: no modifica datos
        * Retorna: un DataTable que contiene el nombre de todos los disenos y el nombre del proyecto asociado a cada diseno.
        */
        public DataTable consultarNombresIdDisenosProyectos()
        {
            DataTable dt = new DataTable();
            string consulta;
            try
            {
                consulta = "SELECT D.id_diseno, D.proposito_diseno, D.id_proyecto, P.nombre_proyecto  FROM Diseno_Pruebas D, Proyecto P WHERE P.id_proyecto = D.id_proyecto";
                dt = acceso.ejecutarConsultaTabla(consulta);

            }
            catch
            {
                dt = null;
            }

            return dt;
        }



        //metodo para consultar diseños asociados a requerimientos
        public DataTable consultarDisenosReq(int id, Object[] requerimientos, int contadorReq)
        {
            DataTable dt = new DataTable();
            string consulta = "";
            int contador = 0;
            try
            {
                for (int i = 0; i < contadorReq; ++i)
                {
  
                    ++contador;
                    consulta = consulta + " " + "SELECT d.id_diseno, d.proposito_diseno FROM Diseno_Pruebas d WHERE d.id_diseno IN (SELECT id_diseno FROM Requerimiento_Diseno WHERE id_req = '"+requerimientos[i].ToString().Substring(0,8)+"' AND id_proyecto ='"+id+"')";
                    if (contador != contadorReq)
                    {
                        consulta = consulta + "UNION";
                    }


                }

                Debug.WriteLine("la consulta es es:"+consulta);
                dt = acceso.ejecutarConsultaTabla(consulta);

            }
            catch (SqlException e)
            {
                dt = null;
            }

            return dt;
        }

        //metodo para consultar los nombres de proyectos que tienen diseños asociados
        public DataTable consultarNombreProyectosAsociados()
        {
            DataTable dt = new DataTable();
            string consulta = "";

            try
            {
                consulta = "SELECT DISTINCT P.nombre_proyecto from Proyecto P, Diseno_Pruebas D WHERE P.id_proyecto = D.id_proyecto";
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