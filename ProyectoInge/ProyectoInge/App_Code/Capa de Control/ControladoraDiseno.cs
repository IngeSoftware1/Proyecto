using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProyectoInge.App_Code.Capa_de_Acceso_a_Datos;
using ProyectoInge.App_Code.Capa_de_Datos__Entidad_;
using System.Data;

namespace ProyectoInge.App_Code.Capa_de_Control
{
    public class ControladoraDiseno
    {
        ControladoraBDDiseno controladoraBDDiseno = new ControladoraBDDiseno();
        ControladoraRecursos controladoraRH;
        ControladoraProyecto controladoraProyectos;

        /*Método para buscar, yendo a la base de datos, si existe un usuario con una cedula particular, que sea líder de diseño
        * Requiere: un string con la cedula de un funcionario específico
        * Modifica: Genera la consulta para mandarla a la base de datos para saber si hay un usuario líder de diseño.
        * Retorna: true si se encontro que el funcionario tenía era líder y false si no.
        */
        public bool buscarAsignacionMiembrosDiseno(string cedula)
        {
            return controladoraBDDiseno.buscarAsignacionMiembrosDiseno(cedula);
        }

        public int obtenerIdDisenoPorProposito(string proposito)
        {
            return controladoraBDDiseno.obtenerIdDisenoPorProposito(proposito);
        }

        /* Método para obtener los id de los proyectos 
        * Requiere: no requiere informacion
        * Modifica: no modifica datos
        * Retorna: un DataTable que contiene los id de los proyectos
        */
        public DataTable consultaridentificadoresProyectos()
        {
            controladoraProyectos = new ControladoraProyecto();
            return controladoraProyectos.consultarIdentificadoresProyectos();
        }

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

        /* Método para obtener los diferentes niveles que puede tener un diseño
        * Requiere: no requiere informacion
        * Modifica: no modifica datos
        * Retorna: un DataTable que contiene los niveles 
        */

        public DataTable consultarNiveles()
        {
            return controladoraBDDiseno.consultarNiveles();
        }

        /* Método para obtener los tipos que puede tener un diseño
        * Requiere: no requiere informacion
        * Modifica: no modifica datos
        * Retorna: un DataTable que contiene los tipos
        */
        public DataTable consultarTipos()
        {
            return controladoraBDDiseno.consultarTipos();
        }

        /* Método para obtener las tecnicas que puede tener un diseño
       * Requiere: no requiere informacion
       * Modifica: no modifica datos
       * Retorna: un DataTable que contiene las tecnicas
       */
        public DataTable consultarTecnicas()
        {
            return controladoraBDDiseno.consultarTecnicas();
        }

        /* Método para obtener las recursos que puede tener un diseño
      * Requiere: no requiere informacion
      * Modifica: no modifica datos
      * Retorna: un DataTable que contiene los recursos
      */
        public DataTable consultarRecursos(String idProyecto)
        {
            controladoraRH = new ControladoraRecursos();
            return controladoraRH.consultarMiembrosProyecto(idProyecto);
        }
     

        /* Método para obtener los id de los proyectos pero tomando en cuenta el usuario
        * Requiere: un string con el identificador del usuario
        * Modifica: no modifica datos
        * Retorna: un DataTable que contiene los id del proyecto
        */
        public DataTable consultarProyectosDeUsuario(string cedula)
        {
            controladoraRH = new ControladoraRecursos();
            return controladoraRH.consultarNombresProyectos(cedula);
        }

    }
}