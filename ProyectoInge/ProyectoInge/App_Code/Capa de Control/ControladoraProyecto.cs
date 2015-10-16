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
                        else if(accion == 4) //insertar miembros de un equipo de pruebas
                        {
                            EntidadTrabajaEn miembro = new EntidadTrabajaEn(datos);
                            resultado = controladoraBDProyecto.insertarMiembro(miembro);
                        }
                        
                    }
                    break;
                case 2:
                    { // MODIFICAR
                        if (accion == 1)//PROYECTO
                        {
                            EntidadProyecto entidadP = new EntidadProyecto(datos);
                            resultado = controladoraBDProyecto.modificarProyecto(entidadP);
                        }
                        else if (accion == 2)
                        {
                            EntidadOficinaUsuaria entidadOU = new EntidadOficinaUsuaria(datos);
                            resultado = controladoraBDProyecto.modificarOfUsuaria(entidadOU);
                        }
                        else if (accion == 3)//TELEFONOOFICINAUSUARIA
                        {
                            resultado = controladoraBDProyecto.insertarTelefonoOficinaUsuaria(datos);//en nombre viene el id de la oficina usuaria, para eliminarlo
                        }
                        else if (accion == 4)//TRABAJA_EN
                        {
                            resultado = controladoraBDProyecto.eliminarTrabaja_En(datos);//en nombre viene elidmiembroConsultado, idProyectoConsultado, para eliminarlo
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
                        if (accion == 3)//TELEFONOOFICINAUSUARIA
                        {
                            resultado = controladoraBDProyecto.eliminarTelefonoOficinaUsuaria(nombre);//en nombre viene el id de la oficina usuaria, para eliminarlo
                        }
                        else if (accion == 4)//TRABAJA_EN
                        {
                            resultado = controladoraBDProyecto.eliminarTrabaja_En(nombre, perfil);//en nombre viene elidmiembroConsultado, idProyectoConsultado, para eliminarlo
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

        /*
         */
        public int obtenerIDconNombreProyecto(string nomProyecto)
        {
            return controladoraBDProyecto.consultarProyecto(nomProyecto);
        }

        public DataTable consultarEstados()
        {
   
            DataTable resultado = controladoraBDProyecto.consultarEstados();
            return resultado;
        }

        public DataTable consultarLideres()
        {
            controladoraRH = new ControladoraRecursos();
            DataTable resultado = controladoraRH.consultarLideres();
            return resultado;
        }

        public DataTable consultarMiembros()
        {
            controladoraRH = new ControladoraRecursos();
            DataTable resultado = controladoraRH.consultarMiembros();
            return resultado;
        }

        public DataTable consultarProyectoTotal(string idProyecto)
        {

            DataTable resultado = controladoraBDProyecto.consultarProyectoTotal(idProyecto);
            return resultado;
        }

        public DataTable consultarProyectos(string idMiembro)
        {

            DataTable resultado = controladoraBDProyecto.consultarProyectos(idMiembro);
            return resultado;
        }

        public DataTable consultarOficina(string idProyecto)
        {

            DataTable resultado = controladoraBDProyecto.consultarOficina(idProyecto);
            return resultado;
        }

        public DataTable consultarTelOficina(string idProyecto)
        {

            DataTable resultado = controladoraBDProyecto.consultarTelOficina(idProyecto);
            return resultado;
        }

        public DataTable consultarMiembrosProyecto(string idProyecto)
        {
            controladoraRH = new ControladoraRecursos();
            DataTable resultado = controladoraRH.consultarMiembrosProyecto(idProyecto);
            return resultado;
        }


        


	}

    
}