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
                            int idProyecto = Int32.Parse(nombre); //supongo que nombre viene el id sino habría que hacer llamado de otro método
                            resultado = controladoraBDProyecto.modificarProyecto(entidadP,idProyecto);
                        }
                        else if (accion == 2)
                        {
                            EntidadOficinaUsuaria entidadOU = new EntidadOficinaUsuaria(datos);
                            int idOficina = Int32.Parse(nombre); //supongo que en nombre viene el id de la oficina que se requiere modificar
                            resultado = controladoraBDProyecto.modificarOfUsuaria(entidadOU,idOficina);
                        }
                        else if (accion == 3)//TELEFONOOFICINAUSUARIA
                        {
                            EntidadTelOficina telefonosOficina = new EntidadTelOficina(datos);
                            resultado = controladoraBDProyecto.insertarTelefonoOficinaUsuaria(telefonosOficina);//en nombre viene el id de la oficina usuaria, para eliminarlo
                        }
                        else if (accion == 4)//TRABAJA_EN
                        {
                            resultado = controladoraBDProyecto.eliminarTrabaja_En(datos);//en nombre viene elidmiembroConsultado, idProyectoConsultado, para eliminarlo
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


        public void eliminarProyecto(int idProyecto)
        {
            controladoraBDProyecto.eliminarOficina(idProyecto);
        }

        /*
         */
        public int obtenerIDconNombreProyecto(string nomProyecto)
        {
            return controladoraBDProyecto.obtenerIdProyecto(nomProyecto);
        } 		
        
        
        public bool eliminarProyecto(string idProyectoS, string idOficinaS, string perfil)
        {
            bool resultado = false;
            int idProyecto = Int32.Parse(idProyectoS);
            int idOficina = Int32.Parse(idOficinaS);
           
            if (perfil.Equals("Administrador"))
            {

                if (idProyecto != -1)
                {

                    if (controladoraBDProyecto.eliminarProyectoCasoPueba(idProyecto) == true)
                    {
                        if (controladoraBDProyecto.eliminarProyecto(idProyecto)== true)
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

        public int consultarOficinaProyecto(int idProyecto)
        {

            int resultado = controladoraBDProyecto.consultarOficinaProyecto(idProyecto);
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