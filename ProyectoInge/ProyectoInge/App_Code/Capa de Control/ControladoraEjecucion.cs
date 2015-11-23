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

        public DataTable consultarDisenos(int idProyecto)
        {
            controladoraDiseno = new ControladoraDiseno();
            DataTable disenos = controladoraDiseno.consultarDisenos(idProyecto);
            return disenos;
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
                        
                    }
                    break;
                case 2:
                    { // MODIFICAR

                       
                    }
                    break;
                case 3:
                    { //ELIMINAR
                        
                    }
                    break;
            }
            return resultado;

        }
    }


}