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
        ControladoraRecursos controladoraRH;
        public ControladoraProyecto()
        {
            
        }

		public bool buscarAsignacionProyectos(string cedula)
        {
            return controladoraBDProyecto.buscarAsignacionProyectos(cedula);
        }

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
                        
                    }
                    break;
                case 2:
                    { // MODIFICAR
                        if(accion==1)//PROYECTO
                        {
                            EntidadProyecto entidadP = new EntidadProyecto(datos);
                            resultado = controladoraBDProyecto.modificarProyecto();
                        }
                    }
                    break;
                case 3:
                    { //ELIMINAR

                        int idProyecto = controladoraBDProyecto.consultarProyecto(nombre);
                        if (perfil.Equals("Administrador"))
                        {
                           
                           if (idProyecto!=-1)
                           {
                               
                               if (controladoraBDProyecto.eliminarProyectoCasoPueba(idProyecto))
                               {
                                   if (controladoraBDProyecto.eliminarOficinaProyecto(idProyecto))
                                   {
                                       resultado = true;
                                   }
                                   else
                                   {
                                       resultado = false;
                                   }

                               }
                               else
                               {
                                   resultado = false;
                               }
                           }
                           else
                           {
                               resultado = false;
                           }
                        }
                        else
                        {
                            //Miembro Llamar a cambiar estado 

                            if (idProyecto != -1)
                            {
                                controladoraBDProyecto.cambiarEstado(idProyecto);
                            }
                        }


                   
                        
                    }
                    break;
            }
            return resultado;
        }

        


	}

    
}