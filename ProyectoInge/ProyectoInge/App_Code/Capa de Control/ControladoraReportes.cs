using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace ProyectoInge.App_Code.Capa_de_Control
{

    public class ControladoraReportes
    {

        ControladoraRecursos controladoraRH;
        ControladoraProyecto controladoraProyectos;
        ControladoraDiseno controladoraDiseno;


        /* Método para obtener los nombres de los proyectos 
           * Requiere: no requiere informacion
           * Modifica: no modifica datos
           * Retorna: un DataTable que contiene los nombres de los proyectos
           */
        public DataTable consultarNombresProyectos()
        {
            controladoraProyectos = new ControladoraProyecto();
            return controladoraProyectos.consultarNombresProyectos();
        }

        
        public DataTable consultarNomPropositoDiseno(int id)
        {
            controladoraDiseno = new ControladoraDiseno();
            return controladoraDiseno.consultarNomPropositoDiseno(id);
        }

        /* Método para obtener los id de los proyectos pero tomando en cuenta el usuario
        * Requiere: un string con el identificador del usuario
        * Modifica: no modifica datos
        * Retorna: un DataTable que contiene los id del proyecto
        */
        public DataTable consultarProyectosDeUsuario(string cedula)
        {
            controladoraRH = new ControladoraRecursos();
            return controladoraRH.consultarProyectosDeUsuario(cedula);
        }

        //metodo para consultarProyectos si es lider
        public DataTable consultarProyectosLider(string cedula)
        {
            controladoraProyectos = new ControladoraProyecto();
            return controladoraProyectos.consultarProyectosLider(cedula);
        }

        /* Método para consultar requerimientos de un proyecto
        * Requiere: el id del proyecto
        * Modifica: no modifica datos
        * Retorna: un DataTable que contiene los requerimientos del proyecto
        */
        public DataTable consultarReqProyecto(int idProyecto)
        {
            controladoraProyectos = new ControladoraProyecto();
            return controladoraProyectos.consultarReqProyecto(idProyecto);
        }

        /**Metodo para consultar los modulos de los proyectos
         * Requiere: los nombres de los modulos
         * Modifica: no modifica nada
         * Retorna: un datable  con los identificadores para obtener los nombres de los modulos.
         */
        public DataTable consultarIdReqProyecto(int idProyecto)
        {
            controladoraProyectos = new ControladoraProyecto();
            return controladoraProyectos.consultarIdReqProyecto(idProyecto);
        }

        /*Método para obtener el id del proyecto mediante el nombre.
         * Requiere: el id del proyecto a consultar.
         * Modifica: no modifica datos
         * Retorna: el id del proyecto consultado
         */
        public int obtenerIDconNombreProyecto(string nomProyecto)
        {
            controladoraProyectos = new ControladoraProyecto();
            return controladoraProyectos.obtenerIDconNombreProyecto(nomProyecto);
        }

        //consultar requerimientos de los modulos asociados a los proyectos
        public DataTable consultarReqModulos(Object[] modulos,int contador)
        {
            controladoraProyectos = new ControladoraProyecto();
            return controladoraProyectos.consultarReqModulos(modulos,contador);
        }


        internal DataTable consultarInformacionCasos(string diseno)
        {
            throw new NotImplementedException();
        }

        internal DataTable consultarInformacionEjecuciones(string diseno)
        {
            throw new NotImplementedException();
        }

        //metodo para consultar diseños asociados a requerimientos
        public DataTable consultarDisenosReq(int id, Object[] requerimientos, int contadorReq){
            controladoraDiseno = new ControladoraDiseno();
            return controladoraDiseno.consultarDisenosReq(id, requerimientos, contadorReq);
        }

        public int obtenerIdReqPorNombre(string s)
        {
            controladoraDiseno = new ControladoraDiseno();
            return controladoraDiseno.obtenerIdReqPorNombre(s);
        }

        public DataTable consultarDisenosPorReq(DataTable s)
        {
            controladoraDiseno = new ControladoraDiseno();
            return controladoraDiseno.consultarDisenosPorReq(s);
        }

        public DataTable consultarPropositosDisenosPorId(DataTable idDisenos)
        {
            controladoraDiseno = new ControladoraDiseno();
            return controladoraDiseno.consultarPropositosDisenosPorId(idDisenos);
        }


        internal DataTable consultarCasosAociadosADiseno(string idDiseno)
        {
            throw new NotImplementedException();
        }
    }
}