﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using ProyectoInge.App_Code.Capa_de_Acceso_a_Datos;
using ProyectoInge.App_Code.Capa_de_Datos__Entidad_;

namespace ProyectoInge.App_Code.Capa_de_Control
{
    public class ControladoraCasosPrueba
    {
        ControladoraBDCasosPrueba controladoraBDCasosPrueba = new ControladoraBDCasosPrueba();
        ControladoraDiseno controladoraDiseno;
        ControladoraRecursos controladoraRH;
        ControladoraProyecto controladoraProyecto;

        public bool eliminarProyectoCasoPueba(int idProyecto)
        {
            return controladoraBDCasosPrueba.eliminarProyectoCasoPueba(idProyecto);
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
        /*Método para obtener un DataTable con los datos del funcionario especificado mediante el número de cédula.
        * Requiere: La cédula del funcionario que se desea consultar
        * Retorna: el DataTable con los datos del funcionario.
  */
        public DataTable consultarRH(string cedula)
        {
            controladoraRH = new ControladoraRecursos();
            return controladoraRH.consultarRH(cedula);
        }



        public bool ejecutarAccion(int modo, object[] datosNuevos, int idCaso, string v2)
        {
            Boolean resultado = false;
            switch (modo)
            {
                case 1:
                    { // INSERTAR

                        EntidadCaso nuevo = new EntidadCaso(datosNuevos);
                        resultado = controladoraBDCasosPrueba.insertarCasoPrueba(nuevo);
                    }
                    break;
                case 2:
                    {   //Modificar un caso de prueba
                        EntidadCaso modificado = new EntidadCaso(datosNuevos);
                        resultado = controladoraBDCasosPrueba.modificarCasoPrueba(modificado, idCaso);
                        break;
                    }
                case 3:
                    {
                        resultado = controladoraBDCasosPrueba.eliminarCasoPrueba(idCaso);
                        break;
                    }
            }
            return resultado;
        }

        //metodo para consultar infomacion del diseño de caso
        public DataTable consultarInformacionDiseno(int idDiseno)
        {
            controladoraDiseno = new ControladoraDiseno();
            return controladoraDiseno.consultarDiseno(idDiseno);
        }

        //metodo para consultar infomacion del proyecto asociado al diseño
        public DataTable consultarInformacionProyectoDiseno(int idDiseno)
        {
            controladoraDiseno = new ControladoraDiseno();
            return controladoraDiseno.consultarInformacionProyectoDiseno(idDiseno);
        }

        /* Método para consultar requerimientos de un diseño
        * Requiere: el id del diseño y el proyecto al que pertenece
        * Modifica: no modifica datos
        * Retorna: un DataTable que contiene los requerimientos del diseño
        */
        public DataTable consultarReqDisenoDeProyecto(int idDiseño, int idProyecto)
        {
            controladoraDiseno = new ControladoraDiseno();
            return controladoraDiseno.consultarReqDisenoDeProyecto(idDiseño, idProyecto);
        }

        //metodo para consultar el id del caso de prueba
        public int consultarIdCasoPrueba(string identificador)
        {
            return controladoraBDCasosPrueba.consultarIdCasoPrueba(identificador);
        }
        /* Método para consultar casos de prueba asociados al diseno
        * Requiere: el id del diseño 
        * Modifica: no modifica datos
        * Retorna: un DataTable que contiene los casos de prueba asociados al diseño
        */
        public DataTable consultarCasosPruebas(string idDiseño)
        {
            int idD = Int32.Parse(idDiseño);
            return controladoraBDCasosPrueba.consultarCasosDePruebaAsociadoADiseno(idD);
        }
        /* Método para consultar los nombres de proyectos
        * Requiere: el id del proyecto 
        * Modifica: no modifica datos
        * Retorna: un DataTable que contiene los proyectos
        */
        public DataTable consultarNombreProyecto(string idProyecto)
        {
            controladoraProyecto = new ControladoraProyecto();
            return controladoraProyecto.consultarNombreProyecto(idProyecto);
        }
        /* Método para consultar casos de prueba 
        * Requiere: el id del caso 
        * Modifica: no modifica datos
        * Retorna: un DataTable que contiene los casos de prueba 
        */
        public DataTable consultarCasoPruebas(string id_caso)
        {

            return controladoraBDCasosPrueba.consultarCasoPrueba(id_caso);
        }

        public DataTable getCodigosCasos(int idDiseno)
        {
            return controladoraBDCasosPrueba.getCodigosCasos(idDiseno);
        }



        internal DataTable consultarCasosDePruebaAsociadoADisenoID(string idDiseno)
        {
            int idD = Int32.Parse(idDiseno);
            return controladoraBDCasosPrueba.consultarCasosDePruebaAsociadoADisenoID(idD);
        }

        internal DataTable consultarResultadoCaso(string caso)
        {
            return controladoraBDCasosPrueba.consultarResultadoCaso(caso);
        }
    }

}