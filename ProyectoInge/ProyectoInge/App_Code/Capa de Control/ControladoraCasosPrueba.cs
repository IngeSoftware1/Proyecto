using System;
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
        ControladoraRecursos controladoraRH;

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

        public bool ejecutarAccion(int modo, object[] datosNuevos, string v1, string v2)
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
            }
            return resultado;

        }
    }

}