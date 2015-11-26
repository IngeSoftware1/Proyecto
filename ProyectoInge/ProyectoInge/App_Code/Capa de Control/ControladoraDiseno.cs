using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProyectoInge.App_Code.Capa_de_Acceso_a_Datos;
using ProyectoInge.App_Code.Capa_de_Datos__Entidad_;
using System.Data;
using System.Diagnostics;

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

        public DataTable consultarNomPropositoDiseno(int proy)
        {
            return controladoraBDDiseno.consultarNomPropositoDiseno(proy);
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
            return controladoraRH.consultarProyectosDeUsuario(cedula);
        }

        /* Método para consultar un diseño
        * Requiere: un int con el identificador del diseño
        * Modifica: no modifica datos
        * Retorna: un DataTable que contiene los datos del diseño
        */
        public DataTable consultarDiseno(int idDiseño)
        {
            return controladoraBDDiseno.consultarDiseno(idDiseño);
        }

        /* Método para consultar representante del diseño
        * Requiere: el id del representante
        * Modifica: no modifica datos
        * Retorna: un DataTable que contiene los datos del representante
        */
        public DataTable consultarRepresentanteDiseno(string ced)
        {
            controladoraRH = new ControladoraRecursos();
            return controladoraRH.consultarRepresentanteDiseno(ced);
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


        /* Método para obtener los diseños almacenados en la BD
        * Requiere: un DataTable con los identificadores de los proyectos que se desean consultar, en caso de que el parámetro
        sea null significa que se desea obtener todos los diseños
        * Modifica: no modifica datos
        * Retorna: un DataTable con los diseños
        */
        public DataTable consultarDisenos(DataTable idProyectos)
        {

            DataTable resultado = controladoraBDDiseno.consultarDisenos(idProyectos);
            return resultado;
        }

        public DataTable consultarDisenosPorReq(DataTable idReqs)
        {

            DataTable resultado = controladoraBDDiseno.consultarDisenosPorReq(idReqs);
            return resultado;
        }

        //Metodo para consultar representantes de los diseños
        public DataTable consultarRepresentantesDisenos()
        {
            controladoraRH = new ControladoraRecursos();
            DataTable resultado = controladoraRH.consultarRepresentantesDisenos();
            return resultado;
        }



        /* Método para obtener los diseños asociados a un proyecto
      * Requiere: un string con la cédula del miembro.
      * Modifica: llama al consultar diseños Asociados de la controladora de recursos humanos.
      * Retorna: un DataTable con los identificadores de los diseños en los cuales el miembro trabaja
      */
        public DataTable consultarDisenosAsociados(string idUsuario)
        {
            controladoraRH = new ControladoraRecursos();
            DataTable resultado = controladoraRH.consultarDisenosAsociados(idUsuario);
            return resultado;
        }


        //metodo para consultar proyectos en los que trabaja el usuario
        public DataTable consultarProyectosAsociados(string idUsuario)
        {
            controladoraRH = new ControladoraRecursos();
            DataTable resultado = controladoraRH.consultarProyectosAsociados(idUsuario);
            return resultado;
        }

        
        //metodo para consultar el ID del proyecto a partir del nombre
        public int obtenerIdProyecto(string nombreProyecto)
        {
            controladoraProyectos = new ControladoraProyecto();
            return controladoraProyectos.obtenerIDconNombreProyecto(nombreProyecto);
        }

     /*Método para ejecutar la acción del IMEC correspondiente a la base de datos.
        * Requiere: un modo que corresponde a 1 si es una inserción, 2 - modificación y 3 Borrado.
        * Modifica una variable boolean dependiendo si la inserción el borrado y el modificar se llevan a cabo correctamente.
        * Retorna el valor de la variable booleana.
        */
        public bool ejecutarAccion(int modo, int accion, Object[] datos, int identificador, string perfil)
        {

            Boolean resultado = false;
            switch (modo)
            {
                case 1:
                    { // INSERTAR
                        if (accion == 1) //insertar diseño de prueba
                        {
                            EntidadDiseno nuevo = new EntidadDiseno(datos);
                            resultado = controladoraBDDiseno.insertarDiseno(nuevo);
                            Debug.WriteLine("Este es el resultado de insertar diseno: " + resultado);
                        }
                        else if (accion == 2) //insertar en EntidadRequerimientoDiseño
                        {
                            EntidadRequerimientoDiseño nuevo = new EntidadRequerimientoDiseño(datos);
                            resultado = controladoraBDDiseno.insertarRequerimientoDiseño(nuevo);
                        }
                    }
                    break;
                case 2:
                    { // MODIFICAR

                        if (accion == 1)
                        {
                            EntidadDiseno entDiseño = new EntidadDiseno(datos);                            
                            return controladoraBDDiseno.modificarDiseño(entDiseño, identificador);

                        }
                        else if (accion == 2)
                        {
                            EntidadRequerimientoDiseño entReqD = new EntidadRequerimientoDiseño(datos);
                            return controladoraBDDiseno.modificarRequerimientosDiseño(entReqD, datos);
                        }
                    }
                    break;
                case 3:
                    { //ELIMINAR
                        if (accion == 1)//Eliminar diseño
                        {
                            resultado = controladoraBDDiseno.eliminarDiseño(identificador);
                        }
                        else if (accion == 2)//Eliminar requerimiento diseño
                        {
                            resultado = controladoraBDDiseno.eliminarRequerimientoDiseño(identificador);
                        }
                    }
                    break;
            }
            return resultado;
        }


        /* Método para obtener los miembros asociados a un determinado proyecto.
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


        /* Método para consultar requerimientos de un diseño
        * Requiere: el id del diseño y el proyecto al que pertenece
        * Modifica: no modifica datos
        * Retorna: un DataTable que contiene los requerimientos del diseño
        */
        public DataTable consultarReqDisenoDeProyecto(int idDiseño, int idProyecto)
        {
            return controladoraBDDiseno.consultarReqDisenoDeProyecto(idDiseño, idProyecto);
        }

      //metodo para consultarProyectos si es lider
        public DataTable consultarProyectosLider(string cedula)
        {
            controladoraProyectos = new ControladoraProyecto();
            return controladoraProyectos.consultarProyectosLider(cedula);
        }

        //metodo para consultar infomacion del proyecto asociado al diseño
        public DataTable consultarInformacionProyectoDiseno(int idDiseno)
        {
            controladoraProyectos = new ControladoraProyecto();
            return controladoraProyectos.consultarInformacionProyectoDiseno(idDiseno);
        }

        //metodo para consultar lider de un proyecto
        public DataTable consultarLider(int idProyecto)
        {

            controladoraRH = new ControladoraRecursos();
            DataTable resultado = controladoraRH.consultarLider(idProyecto);
            return resultado;
        }

        /*Método para eliminar un diseño a partir del ID.
       * Requiere: el id del diseño a eliminar.
       * Modifica: no modifica datos
       * Retorna: no retorna ningun valor
       */
        public bool eliminarDiseño(int idDiseño)
        {
            return controladoraBDDiseno.eliminarDiseño(idDiseño);
        }

       /* Método para consultar requerimientos disponibles de un proyecto
       * Requiere: el id del proyecto
       * Modifica: no modifica datos
       * Retorna: un DataTable que contiene los requerimientos del proyecto
       */
        public DataTable consultarReqDisponibles(int idProyecto, int idDiseno)
        {
            controladoraProyectos = new ControladoraProyecto();
            return controladoraProyectos.consultarReqDisponibles(idProyecto, idDiseno);
        }

        /* Método para eliminar los requerimientos de diseno
        * Requiere: el identificador de el requrimieto, el id del proyecto
        * Modifica: no modifica datos
        * Retorna: un DataTable que contiene los requerimientos diseno
        */
        public bool eliminarRequerimintoDiseno(string siglaBase, int idProyecto)
        {
            return controladoraBDDiseno.eliminarRequerimientoDiseño(siglaBase, idProyecto);
        }

        //metodo para consultar los nombres de los proyectos de diseños
        public DataTable consultarNombresProyectosDeDisenos(DataTable diseños) {

            controladoraProyectos = new ControladoraProyecto();
            return controladoraProyectos.consultarNombresProyectosDeDisenos(diseños);

        }
        public DataTable consultarPropositosDisenosPorId(DataTable idDisenos)
        {

            controladoraBDDiseno = new ControladoraBDDiseno();
            return controladoraBDDiseno.consultarPropositosDisenosPorId(idDisenos);
        }

        public DataTable consultarDisenos(int idProyecto)
        {
            DataTable disenos = controladoraBDDiseno.consultarDisenoAsociadoAProyecto(idProyecto);
            return disenos;
        }

        public DataTable getDatosDiseno(int idDiseno)
        {
            DataTable disenos = controladoraBDDiseno.getDatosDiseno(idDiseno);
            return disenos;
        }

        public DataTable getReqDiseno(int idDiseno, int idProyecto)
        {
            DataTable IdRequerimientos = controladoraBDDiseno.getReqDiseno(idDiseno, idProyecto);
            return IdRequerimientos;
        }

        public DataTable consultarDisenosCasos(int idProyecto)
        {
            return controladoraBDDiseno.consultarDisenosCasos(idProyecto);
        }
        /*Método para obtener un DataTable con los datos del funcionario especificado mediante el número de cédula.
        * Requiere: La cédula del funcionario que se desea consultar
        * Retorna: el DataTable con los datos del funcionario.
         */
        public DataTable consultarRH(string cedula)
        {
            controladoraRH = new ControladoraRecursos();
            return controladoraRH.consultarRH(cedula);
        }

        /* Método para obtener el nombre de todos los disenos y el nombre del proyecto asociado a cada diseno.
        * Requiere: nada
        * Modifica: no modifica datos
        * Retorna: un DataTable que contiene el nombre de todos los disenos y el nombre del proyecto asociado a cada diseno.
        */
        public DataTable consultarNombresIdDisenosProyectos()
        {

            return controladoraBDDiseno.consultarNombresIdDisenosProyectos();
        }


        //metodo para consultar diseños asociados a requerimientos
        public DataTable consultarDisenosReq(int id, Object[] requerimientos, int contadorReq)
        {
            return controladoraBDDiseno.consultarDisenosReq(id, requerimientos, contadorReq);
        }

    }
}