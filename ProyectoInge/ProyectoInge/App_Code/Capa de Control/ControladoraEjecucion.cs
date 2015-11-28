using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProyectoInge.App_Code.Capa_de_Acceso_a_Datos;
using ProyectoInge.App_Code.Capa_de_Datos__Entidad_;
using System.Data;

namespace ProyectoInge.App_Code.Capa_de_Control
{
    public class ControladoraEjecucion
    {
        ControladoraBDEjecucion controladoraBDEjecucion = new ControladoraBDEjecucion();
        ControladoraRecursos controladoraRH;
        ControladoraDiseno controladoraDiseno;
        ControladoraProyecto controladoraProyecto;
        ControladoraCasosPrueba controladoraCasosPruebas;

        public bool buscarAsignacionMiembrosEjecucion(string cedula)
        {
            return controladoraBDEjecucion.buscarAsignacionMiembrosEjecucion(cedula);
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

        /*Método para obtener los responsables de un proyecto.
        * Requiere: nada
        * Modifica: nada
        * Retorna los responsables de un proyecto.
        */
        public DataTable consultarResponsables()
        {
            controladoraRH = new ControladoraRecursos();
            DataTable responsables = controladoraRH.consultarResponsables();
            return responsables;
        }

        public DataTable consultarTiposNC()
        {
            DataTable tiposNC = controladoraBDEjecucion.consultarTiposNC();
            return tiposNC;
        }


        public DataTable consultarEstados()
        {
            DataTable estados = controladoraBDEjecucion.consultarEstados();
            return estados;
        }

        public DataTable consultarNombresProyectos()
        {
            controladoraDiseno = new ControladoraDiseno();
            DataTable nombresProyectos = controladoraDiseno.consultarNombresProyectos();
            return nombresProyectos;
        }

        public DataTable consultarProyectosLider(string cedulaUsuario)
        {
            controladoraDiseno = new ControladoraDiseno();
            DataTable nombresProyectosLider = controladoraDiseno.consultarProyectosLider(cedulaUsuario);
            return nombresProyectosLider;
        }

        public DataTable consultarProyectosDeUsuario(string cedulaUsuario)
        {
            controladoraDiseno = new ControladoraDiseno();
            DataTable proyectosUsuario = controladoraDiseno.consultarProyectosLider(cedulaUsuario);
            return proyectosUsuario;
        }

        public DataTable consultarLider(int id)
        {
            controladoraRH = new ControladoraRecursos();
            DataTable proyectosUsuario = controladoraRH.consultarLider(id);
            return proyectosUsuario;
        }

        public int obtenerIdProyecto(string nombreProyecto)
        {
            controladoraProyecto = new ControladoraProyecto();
            int idProyecto = controladoraProyecto.obtenerIDconNombreProyecto(nombreProyecto);
            return idProyecto;
        }

        public DataTable consultarMiembrosDeProyecto(string idProyecto)
        {
            controladoraProyecto = new ControladoraProyecto();
            DataTable miembrosProyecto = controladoraProyecto.consultarMiembrosDeProyecto(idProyecto);
            return miembrosProyecto;
        }

        public DataTable consultarDisenosCasos(int idProyecto)
        {
            controladoraDiseno = new ControladoraDiseno();
            DataTable disenos = controladoraDiseno.consultarDisenosCasos(idProyecto);
            return disenos;
        }

        public DataTable consultarDiseno(int idDiseno)
        {
            controladoraDiseno = new ControladoraDiseno();
            DataTable disenos = controladoraDiseno.consultarDiseno(idDiseno);
            return disenos;
        }

        /* Método para obtener el caso ejecutado
         * Requiere: no requiere parámetros.
         * Modifica: no realiza modificaciones.
         * Retorna: un DataTable con los estados que tiene almacenada la base de datos.
         */
        public DataTable consultarCasoEjecutado(int idEjecucion)
        {
            DataTable casos = controladoraBDEjecucion.consultarCasoEjecutado(idEjecucion);
            return casos;
        }


        
        /* Método para obtener todas la ejecucion de prueba.
        * Requiere: no requiere parámetros.
        * Modifica: no realiza modificaciones.
        * Retorna: un DataTable con todas las ejecuciones de prueba.
        */
        public DataTable consultarEjecucionesDePrueba()
        {
            return controladoraBDEjecucion.consultarEjecucionesDePrueba();
        }


        /* Método para obtener el nombre de todos los disenos y el nombre del proyecto asociado a cada diseno.
        * Requiere: nada
        * Modifica: no modifica datos
        * Retorna: un DataTable que contiene el nombre de todos los disenos y el nombre del proyecto asociado a cada diseno.
        */
        public DataTable consultarNombresIdDisenosProyectos()
        {
            controladoraDiseno = new ControladoraDiseno();
            return controladoraDiseno.consultarNombresIdDisenosProyectos();
        }


        //metodo para consultar proyectos en los que trabaja el usuario
        public DataTable consultarProyectosAsociados(string idUsuario)
        {
            controladoraRH = new ControladoraRecursos();
            DataTable resultado = controladoraRH.consultarProyectosAsociados(idUsuario);
            return resultado;
        }


        public DataTable consultarEjecucionPrueba(int idEjecucion)
        {
            DataTable casos = controladoraBDEjecucion.consultarEjecucionPrueba(idEjecucion);
            return casos;
        }

        public DataTable getDatosDiseno(int idDiseno)
        {
            controladoraDiseno = new ControladoraDiseno();
            DataTable datosDiseno = controladoraDiseno.getDatosDiseno(idDiseno);
            return datosDiseno;
        }

        public DataTable getCodigosCasos(int idDiseno)
        {
            controladoraCasosPruebas = new ControladoraCasosPrueba();
            DataTable codigoCasos = controladoraCasosPruebas.getCodigosCasos(idDiseno);
            return codigoCasos;
        }

        public DataTable getDescripcionNC(string comboNC)
        {
            DataTable estados = controladoraBDEjecucion.getDescripcionN(comboNC);
            return estados;
        }


        public DataTable getReqDiseno(int idDiseno, int idProyecto)
        {
            controladoraDiseno = new ControladoraDiseno();
            DataTable IdRequerimientos = controladoraDiseno.getReqDiseno(idDiseno, idProyecto);
            return IdRequerimientos;
        }

        public DataTable getNombreReqDiseno(DataTable ReqDiseno, int idProyecto)
        {
            controladoraProyecto = new ControladoraProyecto();
            DataTable nombreReq = controladoraProyecto.getNombreReqDiseno(ReqDiseno, idProyecto);
            return nombreReq;
        }

        /*Método para ejecutar la acción del IMEC correspondiente a la base de datos.
       * Requiere: un modo que corresponde a 1 si es una inserción, 2 - modificación y 3 Borrado.
       * Modifica una variable boolean dependiendo si la inserción el borrado y el modificar se llevan a cabo correctamente.
       * Retorna el valor de la variable booleana.
       */
        public bool ejecutarAccion(int modo, int accion, Object[] datos, String nombre)
        {

            Boolean resultado = false;
            switch (modo)
            {
                case 1:
                    { // INSERTAR
                        if (accion == 1) //inserta ejecución de pruebas
                        {
                            EntidadEjecucionPrueba nuevo = new EntidadEjecucionPrueba(datos);
                            resultado = controladoraBDEjecucion.insertarEjecucionPrueba(nuevo);
                        }

                        if (accion == 2) //inserta no conformidad
                        {
                            EntidadCasoEjecutado nuevo = new EntidadCasoEjecutado(datos);
                            resultado = controladoraBDEjecucion.insertarNC(nuevo);
                        }
                        
                    }
                    break;
                case 2:
                    { // MODIFICAR
                        if (accion == 1) //inserta ejecución de pruebas
                        {
                            EntidadEjecucionPrueba nuevo = new EntidadEjecucionPrueba(datos);
                            resultado = controladoraBDEjecucion.insertarEjecucionPrueba(nuevo);
                        }   
                    }
                    break;
                case 3:
                    { //ELIMINAR
                        if (accion == 1) //
                        {
                        } 
                    }
                    break;
            }
            return resultado;

        }
        /*Método para poner nombre usuario logueado
        */
        public DataTable consultarRH(string cedula)
        {
            controladoraRH = new ControladoraRecursos();
            return controladoraRH.consultarRH(cedula);
        }

        /*Método que obtiene el ultimo id autoincremental que se creó para una ejecución de pruebas
         * Requiere: No recibe parámetros
         * Modifica: Obtiene el valor autonumerico de la tabla
         * Retorna: el valor autonumerico
         */
        public int obtenerIdEjecucionRecienCreado()
        {
            return controladoraBDEjecucion.obtenerIdEjecucionRecienCreado();
        }

        public int consultarIdCasoPrueba(string identificador)
        { 
            controladoraCasosPruebas = new ControladoraCasosPrueba();
            return controladoraCasosPruebas.consultarIdCasoPrueba(identificador);
        }
        internal DataTable consultarEjecucionesDePrueba(Object[] caso, int numCasos, int p)
        {
            return controladoraBDEjecucion.consultarEjecucionesDePrueba(caso, numCasos, p);
        }

        //metodo para consultar estado de ejecución de un caso
        public string consultarTipoNC_Caso(int idCaso)
        {
            return controladoraBDEjecucion.consultarTipoNC_Caso(idCaso);
        }

        //metodo para consultar estados de casos ejecutados relacionados a un diseño
        public DataTable consultarEstadosDeCasos(DataTable casos)
        {
            return controladoraBDEjecucion.consultarEstadosDeCasos(casos);
        }

    }


}