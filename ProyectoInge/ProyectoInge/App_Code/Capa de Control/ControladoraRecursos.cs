using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using ProyectoInge.App_Code.Capa_de_Acceso_a_Datos;
using ProyectoInge.App_Code.Capa_de_Datos__Entidad_;
using ProyectoInge.App_Code;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace ProyectoInge.App_Code.Capa_de_Control
{
    public class ControladoraRecursos
    {
        ControladoraBDRecursos controladoraBDRecurso = new ControladoraBDRecursos();
        ControladoraProyecto controladoraProyecto = new ControladoraProyecto();
        ControladoraDiseno controladoraDiseno = new ControladoraDiseno();
        ControladoraEjecucion controladoraEjecucion = new ControladoraEjecucion();

        public ControladoraRecursos()
        {

        }

        /*Método para obtener toda la información relacionada a un usuarios, yendo a la base de datos y buscando por nombre y contraseña.
         * Requiere: requiere el usuario del usuario y la contraseña del usuario.
         * Retorna: Un booleano con el valor en true cuando se ha encontrado el usuario con el que coincide la cédula y el password de entrada.
         */
        public bool consultarUsuario(String user, String pass)
        {
            return controladoraBDRecurso.consultarUsuario(user, pass);
        }

        /*Método para asignar el valor de sesión cerrada en la base de datos
         * Requiere: requiere la cédula del usuario al cual se le asignará la sesión como cerrada en la base de datos.
         * Modifica: Modifica el valor del atributo de login, yendo a la controladora de base de datos.
         * Retorna: Devuelve un true si se ejecutó la actualización correctamente en la base de datos.
         */
        public Boolean modificarEstadoCerrar(String cedula)
        {
            Boolean resultado = controladoraBDRecurso.modificarEstadoCerrar(cedula);
            return resultado;
        }

        /*Método para asignar un estado de sesión en la base de datos, para un usuario específico.
        * Requiere: requiere la cédula del usuario al cual se le asignará un estado específico para la sesión (abierta o cerrada), y el estado de la sesión.
        * Modifica: Modifica el valor del atributo de login.
        * Retorna: Devuelve un true si se ejecutó la actualización correctamente yendo a la controladora de base de datos.
        */
        public Boolean modificarEstadoAbrir(string ced)
        {
            Boolean resultado = controladoraBDRecurso.modificarEstadoAbrir(ced);
            return resultado;
        }

        /*Método para obtener los tipos de roles de la base de datos en la tabla Rol
         * Retorna: el DataTable (la fila de la tabla rol) con los tipos de roles 
         */
        public DataTable consultarRoles()
        {
            DataTable resultado = controladoraBDRecurso.consultarRoles();
            return resultado;
        }


        /*Método para obtener un DataTable con la cédula y nombre todos los funcionarios
         * Requiere: La cédula y contraseña del funcionario que se desea consultar
         * Retorna: el DataTable (la fila de la tabla funcionario) en caso de que encuentre el usuario y contraseña
         */
        public Boolean modificarContrasena(String cedula, String pass, String newPass)
        {
            Boolean resultado = controladoraBDRecurso.modificarContrasena(cedula, pass, newPass);
            return resultado;
        }


        /*Método para obtener un DataTable con la cédula y nombre todos los funcionarios
         * Requiere: La cédula y contraseña del funcionario que se desea consultar
         * Retorna: el DataTable (la fila de la tabla funcionario) en caso de que encuentre el usuario y contraseña
         */
        public string consultarCedula(string user, string password)
        {
            return controladoraBDRecurso.consultarCedula(user, password);
        }

        public string consultarEstadoFuncionario(string ced)
        {
            return controladoraBDRecurso.consultarEstadoFuncionario(ced);
        }


        /*Método para ejecutar la acción del IMEC correspondiente a la base de datos.
         * Requiere: un modo que corresponde a 1 si es una inserción, 2 - modificación y 3 Borrado.
         * Modifica una variable boolean dependiendo si la inserción el borrado y el modificar se llevan a cabo correctamente.
         * Retorna el valor de la variable booleana.
         */
        public bool ejecutarAccion(int modo, int accion, Object[] datos, String cedula)
        {
            Boolean resultado = false;
            switch (modo)
            {
                case 1:
                    { // INSERTAR
                        if (accion == 1)//Insertar Funcionario
                        {
                            Funcionario nuevo = new Funcionario(datos);
                            resultado = controladoraBDRecurso.insertarFuncionario(nuevo);
                        }
                        else if (accion == 2) //Insertar administrador
                        {
                            EntidadAdministrador nuevoadmin = new EntidadAdministrador(datos);
                            resultado = controladoraBDRecurso.insertarAdministrador(nuevoadmin);
                        }
                        else if (accion == 3) //Insertar miembro de equipo de pruebas
                        {
                            EntidadMiembro nuevoamiembro = new EntidadMiembro(datos);
                            resultado = controladoraBDRecurso.insertarMiembro(nuevoamiembro);
                        }
                        else if (accion == 4) //Inserta telefonos del funcionario
                        {
                            EntidadTelFuncionario telefonos = new EntidadTelFuncionario(datos);
                            resultado = controladoraBDRecurso.insertarTelefono(telefonos);
                        }
                    }
                    break;
                case 2:
                    { // MODIFICAR

                        if (accion == 1)//Modificar funcionario , por parte de un miembro
                        {
                            Funcionario nuevo = new Funcionario(datos);
                            resultado = controladoraBDRecurso.modificarFuncionario(nuevo, cedula);
                        }
                    }
                    break;
                case 3:
                    {
                        if (accion == 2)
                        {
                            if (controladoraBDRecurso.eliminarTelefonoFuncionario(cedula))
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
                            string perfil = buscarPerfil(cedula);
                            if (perfil == "Administrador")
                            {
                                if (controladoraProyecto.buscarAsignacionProyectos(cedula) == true)
                                {
                                    resultado = false;

                                }
                                else
                                {
                                    resultado = controladoraBDRecurso.eliminarFuncionario(cedula);
                                }
                            }
                            else
                            {
                                //Miembro
                                if (controladoraProyecto.buscarAsignacionMiembrosProyecto(cedula) == true)
                                {
                                    resultado = false;

                                }
                                else if (controladoraDiseno.buscarAsignacionMiembrosDiseno(cedula) == true)
                                {
                                    resultado = false;
                                }
                                else if(controladoraEjecucion.buscarAsignacionMiembrosEjecucion(cedula)==true)
                                {
                                    resultado = false;
                                }
                                else
                                {
                                    resultado = controladoraBDRecurso.eliminarFuncionario(cedula);
                                }//auqi se llamo a elimimnar
                            }
                        }
                    }
                    break;
            }
            return resultado;
        }


        /*Método para obtener un DataTable con los datos del funcionario especificado mediante el número de cédula.
       * Requiere: La cédula del funcionario que se desea consultar
       * Retorna: el DataTable con los datos del funcionario.
       */
        public DataTable consultarRH(string cedula)
        {
            return controladoraBDRecurso.consultarRH(cedula);
        }

        /*Método para obtener un DataTable con la cédula, nombre, primer apellido y segundo apellido de todos los funcionarios en caso de que
         * el sistema esté siendo utilizando por el administrador o solo el correspondiente al usuario del sistema en caso de que éste sea un 
         * miembro.
        * Requiere: La cédula del funcionario al que se le desea consultar los teléfonos
        * Retorna: el DataTable con los datos del funcionario.
        */
        public DataTable consultarRecursosHumanos(string cedula)
        {
            return controladoraBDRecurso.consultarRecursosHumanos(cedula);
        }

        /*Método para obtener un DataTable con los números de teléfono (en caso de que tenga) el funcionario consultado. 
       * Retorna: el DataTable con los teléfonos del funcionario.
       */
        public DataTable consultarTelefonosRH(string idRH)
        {
            return controladoraBDRecurso.consultarTelefonosRH(idRH);
        }

        /*Método para obtener un  string con el perfil del usuario ya sea "Administrador" o "Miembro"
        * Requiere: La cédula del funcionario del cual se desea conocer el perfil.
        * Retorna: el string con el perfil del funcionario.
        */
        public string buscarPerfil(string cedulaDeFuncionario)
        {
            return controladoraBDRecurso.buscarPerfil(cedulaDeFuncionario);
        }

        public DataTable consultarLideres()
        {
            return controladoraBDRecurso.consultarLideres();
        }

        public DataTable consultarMiembros()
        {
            return controladoraBDRecurso.consultarMiembros();
        }

        public DataTable consultarMiembrosProyecto(string idProyecto)
        {
            return controladoraBDRecurso.consultarMiembrosProyecto(idProyecto);
        }

    }

}
