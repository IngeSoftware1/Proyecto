using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProyectoInge.App_Code.Capa_de_Acceso_a_Datos;
using ProyectoInge.App_Code.Capa_de_Datos__Entidad_;
using System.Data;
using ProyectoInge.App_Code;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace ProyectoInge.App_Code.Capa_de_Control
{
    public class ControladoraProyecto
    {
        ControladoraBDProyectos controladoraBDProyecto = new ControladoraBDProyectos();
        ControladoraCasosPrueba controladoraCasoPrueba = new ControladoraCasosPrueba();
        ControladoraRecursos controladoraRH;
        ControladoraDiseno controladoraDiseno;
        public ControladoraProyecto()
        {

        }


        /*Método para poner nombre usuario logueado
*/
        public DataTable consultarRH(string cedula)
        {
            return controladoraRH.consultarRH(cedula);
        }
        /*Método buscar la asignación del proyecto con la cédula
        * Requiere: la cedula de la persona 
        * Modifica: no modifica
        * Retorna: true si es exitosa la búsqueda
        */
        public bool buscarAsignacionProyectos(string cedula)
        {
            return controladoraBDProyecto.buscarAsignacionProyectos(cedula);
        }
        /*Método buscar la asignación de los miembros del proyecto con la cédula
        * Requiere: la cedula de la persona 
        * Modifica: no modifica
        * Retorna: true si es exitosa la búsqueda
        */
        public bool buscarAsignacionMiembrosProyecto(string cedula)
        {
            return controladoraBDProyecto.buscarAsignacionProyectos(cedula);
        }

        /*Método para poder hacer que la controladora proyecto y  recursos humanos se comuniquen y cerrar sesión
        * Requiere: la cedula de la persona que se encuentra en la sesión
        * Modifica el estado de login de la persona, para indicar que ha cerrado la sesión
        * Retorna el valor de la variable booleana.
        */
        public bool cerrarSesion(string ced)
        {
            controladoraRH = new ControladoraRecursos();
            Boolean a = controladoraRH.modificarEstadoCerrar(ced);
            return a;
        }

        /*Método para obtener el id de una oficina usuaria a partir de su nombre
         * Requiere: el nombre de la oficina usuaria por la cual buscar
         * Modifica: obtiene el id de la oficina usuaria
         * Retorna: el id obtenido 
         */
        public int obtenerOficinaAgregada(string nombreOficina)
        {
            return controladoraBDProyecto.obtenerOficinaAgregada(nombreOficina);
        }

        /*Método para ejecutar la acción del IMEC correspondiente a la base de datos.
        * Requiere: un modo que corresponde a 1 si es una inserción, 2 - modificación y 3 Borrado.
        * Modifica una variable boolean dependiendo si la inserción el borrado y el modificar se llevan a cabo correctamente.
        * Retorna el valor de la variable booleana.
        */
        public bool ejecutarAccion(int modo, int accion, Object[] datos, String nombre, string perfil)
        {

            Boolean resultado = false;
            switch (modo)
            {
                case 1:
                    { // INSERTAR
                        if (accion == 1) //inserta oficina usuaria
                        {
                            EntidadOficinaUsuaria nuevo = new EntidadOficinaUsuaria(datos);
                            resultado = controladoraBDProyecto.insertarOficina(nuevo);
                        }
                        else if (accion == 2) //inserta telefonos de oficina usuaria
                        {
                            EntidadTelOficina telefonosOficina = new EntidadTelOficina(datos);
                            resultado = controladoraBDProyecto.insertarTelefono(telefonosOficina);
                        }
                        else if (accion == 3) //inserta un nuevo proyecto
                        {
                            EntidadProyecto proyecto = new EntidadProyecto(datos);
                            resultado = controladoraBDProyecto.insertarProyecto(proyecto);
                        }
                        else if (accion == 4) //insertar miembros de un equipo de pruebas
                        {
                            ControladoraRecursos controladoraRH = new ControladoraRecursos();
                            resultado = controladoraRH.ejecutarAccion(1, 5, datos, "");
                        }
                        else if (accion == 5) //Insertar requerimientos
                        {
                            EntidadRequerimiento requerimiento = new EntidadRequerimiento(datos);
                            resultado = controladoraBDProyecto.insertarRequerimiento(requerimiento);
                        }
                    }
                    break;
                case 2:
                    { // MODIFICAR

                        if (accion == 1)//PROYECTO
                        {
                            EntidadProyecto entidadP = new EntidadProyecto(datos);
                            //int idProyecto = Int32.Parse(nombre); //supongo que nombre viene el id sino habría que hacer llamado de otro método
                            return controladoraBDProyecto.modificarProyecto(entidadP, nombre);
                        }
                        else if (accion == 2)//OFICINA USUARIA
                        {
                            EntidadOficinaUsuaria entidadOU = new EntidadOficinaUsuaria(datos);

                            resultado = controladoraBDProyecto.modificarOfUsuaria(entidadOU, nombre);
                        }
                    }
                    break;
                case 3:
                    { //ELIMINAR
                        if (accion == 3)//TELEFONOOFICINAUSUARIA
                        {
                            int idOficinaUsuaria = Int32.Parse(nombre); //en nombre viene el id de la oficina usuaria, para eliminarlo
                            resultado = controladoraBDProyecto.eliminarTelefonoOficinaUsuaria(idOficinaUsuaria);
                        }
                        else if (accion == 4)//TRABAJA_EN
                        {
                            int idProyecto = Int32.Parse(nombre);
                            controladoraRH = new ControladoraRecursos();
                            resultado = controladoraRH.eliminarTrabaja_En(idProyecto);//en nombre viene elidmiembroConsultado, idProyectoConsultado, para eliminarlo
                        }
                    }
                    break;
            }
            return resultado;

        }

        /*Método para eliminar una oficina usuaria mediante su id
         * Requiere: el id de la oficina usuaria
         * Modifica: llama al eliminar de la controladora de BD
         * Retorna: no retorna ningún valor
         */
        public void eliminarOficina(int idOficina)
        {
            controladoraBDProyecto.eliminarOficina(idOficina);
        }

        /*Método para comunicarse con la controladora de BD de Proyectos para eliminar un proyecto
        * Requiere: el id del proyecto
        * Modifica: modifica la BD con el llamado
        * Retorna: no retorna
        */
        public void eliminarProyecto(int idProyecto)
        {
            controladoraBDProyecto.eliminarOficina(idProyecto);
        }

        /*Método para obtener el id del proyecto mediante el nombre.
         * Requiere: el id del proyecto a consultar.
         * Modifica: no modifica datos
         * Retorna: el id del proyecto consultado
         */
        public int obtenerIDconNombreProyecto(string nomProyecto)
        {
            return controladoraBDProyecto.obtenerIdProyecto(nomProyecto);
        }


        public string consultarEstadoProyecto(int idProyecto)
        {
            return controladoraBDProyecto.consultarEstadoProyecto(idProyecto);
        }

        /*Método para eliminar un proyecto
         * Requiere: el id del proyecto, el id de la oficina asociada y el perfil de la persona que realiza la acción
         * Modifica: llama al eliminar de la controladora de BD
         * Retorna: retorna el resultado de la operación
         */
        public bool eliminarProyecto(string idProyectoS, string idOficinaS, string perfil)
        {
            bool resultado = false;
            int idProyecto = Int32.Parse(idProyectoS);
            int idOficina = Int32.Parse(idOficinaS);

            if (perfil.Equals("Administrador"))
            {

                if (idProyecto != -1)
                {

                    if (controladoraCasoPrueba.eliminarProyectoCasoPueba(idProyecto) == true)
                    {
                        if (controladoraBDProyecto.eliminarProyecto(idProyecto) == true)
                        {
                            controladoraBDProyecto.eliminarOficina(idOficina);
                            resultado = true;
                        }
                    }
                }

            }
            else
            {
                //Miembro Llamar a cambiar estado 

                if (idProyecto != -1)
                {
                    if (controladoraBDProyecto.cambiarEstado(idProyecto) == true)
                    {
                        resultado = true;
                    }
                }
            }

            return resultado;
        }

        /* Método para obtener los estados que podría contener un proyecto.
         * Requiere: no requiere parámetros.
         * Modifica: llama al consultar estados de la controladora de BD de proyecto
         * Retorna: un DataTable con los estados que tiene almacenada la base de datos
         */
        public DataTable consultarEstados()
        {

            DataTable resultado = controladoraBDProyecto.consultarEstados();
            return resultado;
        }

        /* Método para obtener los miembros que tienen como rol: líder de pruebas 
         * Requiere: no requiere parámetros.
         * Modifica: llama al consultar líderes de la controladora de Recursos Humanos
         * Retorna: un DataTable con el nombre y apellido de los miembros que tienen como rol: líder de pruebas. 
         */
        public DataTable consultarLideres()
        {
            controladoraRH = new ControladoraRecursos();
            DataTable resultado = controladoraRH.consultarLideres();
            return resultado;
        }

        /* Método para obtener los miembros almacenados en la base de datos.
        * Requiere: no requiere parámetros.
        * Modifica: llama al consultar miembros de la controladora de Recursos Humanos
        * Retorna: un DataTable con el nombre y apellidos de los miembros 
        */
        public DataTable consultarMiembros()
        {
            controladoraRH = new ControladoraRecursos();
            DataTable resultado = controladoraRH.consultarMiembros();
            return resultado;
        }

        /* Método para obtener los datos asociados a un proyecto en específico
        * Requiere: un string con el identificador del proyecto que se desea consultar.
        * Modifica: llama al consultar Proyecto Total de la controladora de BD de proyecto.
        * Retorna: un DataTable con los datos del proyecto
        */
        public DataTable consultarProyectoTotal(string idProyecto)
        {

            DataTable resultado = controladoraBDProyecto.consultarProyectoTotal(idProyecto);
            return resultado;
        }

        /* Método para obtener los id de todos los proyectos
        * Requiere: no requiere informacion
        * Modifica: no modifica datos
        * Retorna: un DataTable con los id de los proyectos
        */
        public DataTable consultarIdentificadoresProyectos()
        {

            DataTable resultado = controladoraBDProyecto.consultarIdenficadoresProyectos();
            return resultado;
        }


        /* Método para obtener los nombres de todos los proyectos
       * Requiere: no requiere informacion
       * Modifica: no modifica datos
       * Retorna: un DataTable con los nombres de los proyectos
       */
        public DataTable consultarNombresProyectos()
        {

            DataTable resultado = controladoraBDProyecto.consultarNombresProyectos();
            return resultado;
        }

        /* Método para obtener los proyectos almacenados en la BD
        * Requiere: un DataTable con los identificadores de los proyectos que se desean consultar, en caso de que el parámetro
        sea null significa que se desea obtener todos los proyectos.
        * Modifica: llama al consultar Proyectos de la controladora de BD de proyecto.
        * Retorna: un DataTable con los proyectos (solo se obtienen ciertos atributos como el ID, el nombre del proyecto y de la oficina y 
        y el líder).
        */
        public DataTable consultarProyectos(DataTable idProyectos)
        {

            DataTable resultado = controladoraBDProyecto.consultarProyectos(idProyectos);
            return resultado;
        }

        /* Método para obtener los datos de la oficina asociada a un proyecto.
       * Requiere: un string con el identificador del proyecto para consultar la oficina asociada a éste.
       * Modifica: llama al consultar Oficina de la controladora de BD de proyecto.
       * Retorna: un DataTable con los datos de la oficina
       */
        public DataTable consultarOficina(string idProyecto)
        {

            DataTable resultado = controladoraBDProyecto.consultarOficina(idProyecto);
            return resultado;
        }

        public int consultarOficinaProyecto(int idProyecto)
        {

            int resultado = controladoraBDProyecto.consultarOficinaProyecto(idProyecto);
            return resultado;
        }

        /* Método para obtener el/los teléfono(s) de la oficina asociada a un proyecto.
        * Requiere: un string con el identificador del proyecto para consultar los teloficina asociada a éste.
        * Modifica: llama al consultar Tel Oficina de la controladora de BD de proyecto.
        * Retorna: un DataTable con los teléfonos de la oficina
        */
        public DataTable consultarTelOficina(string idProyecto)
        {

            DataTable resultado = controladoraBDProyecto.consultarTelOficina(idProyecto);
            return resultado;
        }

        /* Método para obtener los miembros asociados a un determinado proyecto.
        * Requiere: un string con el identificador del proyecto.
        * Modifica: llama al consultar Miembros Proyecto de la controladora de recursos humanos.
        * Retorna: un DataTable con los miembros asociados al proyecto especificado
        */
        public DataTable consultarMiembrosProyecto(string idProyecto)
        {
            controladoraRH = new ControladoraRecursos();
            DataTable resultado = controladoraRH.consultarMiembrosProyecto(idProyecto);
            return resultado;
        }

        /* Método para obtener el/los proyectos en los cuales el miembro trabaja
        * Requiere: un string con la cédula del miembro.
        * Modifica: llama al consultar Proyectos Asociados de la controladora de recursos humanos.
        * Retorna: un DataTable con los identificadores de los proyectos en los cuales el miembro trabaja
        */
        public DataTable consultarProyectosAsociados(string idUsuario)
        {
            controladoraRH = new ControladoraRecursos();
            DataTable resultado = controladoraRH.consultarProyectosAsociados(idUsuario);
            return resultado;
        }



        /* Método para obtener el nombre y apellido del líder de un proyecto
        * Requiere: una lista con la cédula de los líderes que se desea consultar
        * Modifica: llama al obtener Nombres Lideres de la controladora de recursos humanos.
        * Retorna: un DataTable con el nombre y apellido del líder
        */
        public DataTable obtenerNombresLideres(List<string> cedLideres)
        {
            controladoraRH = new ControladoraRecursos();
            DataTable resultado = controladoraRH.obtenerNombresLideres(cedLideres);
            return resultado;
        }

        //metodo para traer proyectos si es lider
        public DataTable consultarProyectosLider(string cedula)
        {
            return controladoraBDProyecto.consultarProyectosLider(cedula);
        }

        //metodo para consultar infomacion del proyecto asociado al diseño
        public DataTable consultarInformacionProyectoDiseno(int idDiseno)
        {
            return controladoraBDProyecto.consultarInformacionProyectoDiseno(idDiseno);
        }


        /* Método para consultar requerimientos disponibles de un proyecto
      * Requiere: el id del proyecto
      * Modifica: no modifica datos
      * Retorna: un DataTable que contiene los requerimientos del proyecto
      */
        public DataTable consultarReqDisponibles(int idProyecto, int idDiseno)
        {
            return controladoraBDProyecto.consultarReqDisponibles(idProyecto, idDiseno);
        }





        /* Método para consultar requerimientos de un proyecto
        * Requiere: el id del diseño y el proyecto al que pertenece
        * Modifica: no modifica datos
        * Retorna: un DataTable que contiene los requerimientos del proyecto
        */
        public DataTable consultarReqProyecto(int idProyecto)
        {
            return controladoraBDProyecto.consultarReqProyecto(idProyecto);
        }

        /* Método para actualizar los requerimientos de un proyecto
        * Requiere: el id del requerimiento, el id del proyecto asociado, el id del diseño y el nombre del requerimiento
        * Modifica: modifica la tabla requerimiento
        * Retorna: un booleano que indica si fue posible realizar la actualización
        */
        public bool actualizarRequerimiento(Object[] datos)
        {
            bool resultado = controladoraBDProyecto.actualizarRequerimiento(datos);
            return resultado;
        }

        /**
        internal bool eliminarRequerimientosDiseno(string sigla, string nombreReq, int idProyecto)
             {
                 ControladoraBDDiseno controladoraBDDiseno = new ControladoraBDDiseno();
                 bool resultado = controladoraBDDiseno.eliminarRequerimientoDiseno(sigla,nombreReq,idProyecto);
                 return resultado;
             } 
         **/

        /* Método para obtener los miembros asociados a un determinado proyecto junto con la cédula
        * Requiere: un string con el identificador del proyecto.
        * Modifica: llama al consultar Miembros Proyecto de la controladora de recursos humanos.
        * Retorna: un DataTable con los miembros asociados al proyecto especificado
        */
        public DataTable consultarMiembrosDeProyecto(string idProyecto)
        {
            controladoraRH = new ControladoraRecursos();
            DataTable resultado = controladoraRH.consultarMiembrosDeProyecto(idProyecto);
            return resultado;
        }
        /* Método para cominicarse con la BD de proyecto para eliminar un requerimiento
        * Requiere: un string como la sigla de la base, un string, como el nombre del requerimiento, un int como el id del proyecto
        * Modifica: llama al eliminarRequerimiento de la controladora de BD de Proy.
        * Retorna: booleano si lo logro
        */
        internal bool eliminarRequeriminto(string sigla, string nombreReq, int idProyecto)
        {
            return controladoraBDProyecto.eliminarRequerimiento(sigla, nombreReq, idProyecto);
        }
        /* Método para cominicarse con la BD de proyecto para consultar proyectos, el nombre y el id, de acuerdo a un miembro
        * Requiere: un string el id del miembro
        * Modifica: llama al consultarIDNombresProyectos de la controladora de BD de Proy.
        * Retorna: DataTable
        */
        internal DataTable consultarProyectosAsociadosIDNombre(string idRH)
        {
            return controladoraBDProyecto.consultarIDNombresProyectos(idRH);
        }

        /* Método para cominicarse con la BD de proyecto para consultar proyectos, el nombre, de acuerdo a un proyecto
        * Requiere: un string el id del proyecto
        * Modifica: llama al consultarNombreProyecto de la controladora de BD de Proy.
        * Retorna: DataTable
        */
        internal DataTable consultarNombreProyecto(string idProyecto)
        {
            int idP = Int32.Parse(idProyecto);
            return controladoraBDProyecto.consultarNombreProyecto(idP);
        }

        /* Método para cominicarse con la BD de diseno para eliminar un requerimiento de diseno
        * Requiere: un string como la sigla de la base, un int como el id del proyecto
        * Modifica: llama al eliminarRequerimintoDiseno de la controladora de diseno.
        * Retorna: booleano si lo logro
        */
        internal bool eliminarRequerimintoDiseno(string siglaBase, int idProyecto)
        {
            controladoraDiseno = new ControladoraDiseno();
            return controladoraDiseno.eliminarRequerimintoDiseno(siglaBase, idProyecto);
        }
        /* Método para consultar si un miembro tiene como rol el lider de pruebas, para consultar de cuales proyecto es lider
               * Requiere: un string con la cedula del miembro
               * Retorna: un booleano que me indica si el miembro es lider o no  
               */
        public Boolean consultarRolLider(string cedula)
        {
            controladoraRH = new ControladoraRecursos();
            Boolean resultado = controladoraRH.consultarRolLider(cedula);
            return resultado;
        }



        /**Metodo para consultar los modulos de los proyectos
        * Requiere: los nombres de los modulos
        * Modifica: no modifica nada
        * Retorna: un datable  con los identificadores para obtener los nombres de los modulos.
        */
        public DataTable consultarIdReqProyecto(int idProyecto)
        {
            return controladoraBDProyecto.consultarIdReqProyecto(idProyecto);
        }



        /* Método para consultar los proyectos en los cuales un miembro es lider
        * Requiere: un string con la cedula del miembro
        * Retorna: un DataTable con los proyectos en los que el miembro es lider
        */
        public DataTable consultarProyectosDeLider(string cedula)
        {
            return controladoraBDProyecto.consultarProyectosDeLider(cedula);
        }

        public DataTable consultarNombresProyectosDeDisenos(DataTable diseños)
        {

            return controladoraBDProyecto.consultarNombresProyectosDeDisenos(diseños);

        }


        public DataTable getNombreReqDiseno(DataTable ReqDiseno, int idProyecto)
        {
            return controladoraBDProyecto.getNombreReqDiseno(ReqDiseno, idProyecto);
        }


        //consultar requerimientos de los modulos asociados a los proyectos
        public DataTable consultarReqModulos(Object[] modulos,int contador)
        {
            return controladoraBDProyecto.consultarReqModulos(modulos,contador);
        }

    }
}