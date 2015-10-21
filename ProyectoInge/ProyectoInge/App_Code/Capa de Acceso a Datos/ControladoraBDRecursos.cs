using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using ProyectoInge.App_Code.Capa_de_Acceso_a_Datos;
using ProyectoInge.App_Code.Capa_de_Datos__Entidad_;
using System.Data.SqlClient;

namespace ProyectoInge.App_Code.Capa_de_Acceso_a_Datos
{

    
    public class ControladoraBDRecursos
      {
                      
      AccesoBaseDatos acceso = new AccesoBaseDatos();
      public ControladoraBDRecursos()
      {
      }

     /*Método para consultar los datos de un recurso humano específico.
     * Requiere: requiere la cédula y contraseña del usuario al cual se le consultarán los datos.
     * Modifica: lleva a cabo las consultas en la base de datos
     * Retorna: Un booleano con el valor en true cuando se ha encontrado el usuario con el que coincide la cédula y el password de entrada.
     */
     public bool consultarUsuario(String user, String pass)
        {

         bool resultado = false;
          try
         {
             string consulta = "SELECT * FROM Funcionario WHERE usuario =" + user + " and contrasena = " + pass;
             DataTable data = acceso.ejecutarConsultaTabla(consulta);
             if (data.Rows.Count==1)
             {

                 resultado = true;

             } 
         }
          catch (SqlException e)
          {
              resultado = false;
          }

          return resultado;
         
     }

        /*Método para modificar la contraseña de un usuario.
        * Requiere: requiere la cédula y contraseña anterior del usuario y contraseña actual.
        * Modifica: lleva a cabo las actualizacion de la contraseña en la base de datos.
        * Retorna: Devuelve un boolean true en caso de que se ejecute la actualización correctamente.
        */
        public Boolean modificarContrasena(String ced, String pass, String newPass)
     {
         try
         {
             string modif = "UPDATE FUNCIONARIO SET contrasena ='"+ newPass+"' WHERE cedula ='" + ced + "' and contrasena = '" + pass +"'";
             return acceso.insertarDatos(modif);
         }
         catch (SqlException e)
         {
             return false;
         }
     }

        /*Método para consultar los datos de un recurso humano específico.
        * Requiere: requiere la cédula y contraseña del usuario al cual se le consultarán los datos.
        * Modifica: lleva a cabo las consultas en la base de datos
        * Retorna: Devuelve un datatable (la fila de la base de datos que reponde a la consulta) con la tabla funcionario en caso de que encuentre el usuario y contraseña y en caso de que no esta retorna null .
        */
        public string consultarCedula(string user, string password)
     {
         string resultado="";
         DataTable datosFuncionario = new DataTable();
         string consulta = "SELECT cedula FROM Funcionario WHERE usuario='" + user + "' and contrasena = '" + password +"'";
         try{
             datosFuncionario = acceso.ejecutarConsultaTabla(consulta);
             if(datosFuncionario != null && datosFuncionario.Rows.Count == 1){
                 resultado = datosFuncionario.Rows[0][0].ToString();
             }
              
         }
         catch (SqlException e) {
             resultado = "";
         }

         return resultado;
     }
       /*Método para consultar los tipos de Rol de la tabla Rol.
       * Requiere: no requiere ningún dato de entrada.
       * Modifica: lleva a cabo la consulta en la base de datos
       * Retorna: Devuelve un datatable (la fila de la base de datos que reponde a la consulta) con los tipos de rol.
       */
       public DataTable consultarRoles()
       {
         DataTable Roles;
         string consulta = "SELECT R.tipo_rol " + " FROM Rol R ";
         Roles = acceso.ejecutarConsultaTabla(consulta);
         return Roles;
       }

     /*Método para consultar todos los dtos personales de un recurso humano específico.
     * Requiere: requiere la cédula del usuario al cual se le consultarán los datos personales.
     * Modifica: lleva a cabo la consulta en la base de datos
     * Retorna: Devuelve un datatable (la fila de la base de datos que reponde a la consulta) con los datos personales de un recurso humano específico.
     */
     public DataTable consultarRH(string ced)
     {
         DataTable datosFuncionario = new DataTable();
         string consulta = "SELECT F.cedula, F.nombre, F.apellido1, F.apellido2, F.usuario, F.Email, F.contrasena, M.tipo_rol " + " FROM Funcionario F LEFT OUTER JOIN Miembro M ON F.cedula = M.cedula_miembro " + " WHERE F.cedula = '" + ced + "'";
         datosFuncionario = acceso.ejecutarConsultaTabla(consulta);
         return datosFuncionario;
     }


     /*Método para consultar el estado de un funcionario, para poder observar si está activo o no
    * Requiere: requiere la cédula del usuario.
    * Modifica: No modifica datos.
    * Retorna: un True o False en forma de string
  */
     public string consultarEstadoFuncionario(string ced)
     {
         string resultado = "";
         DataTable datosFuncionario = new DataTable();
         string consulta = "SELECT * FROM Funcionario WHERE cedula ='" + ced + "';";
         try
         {
             datosFuncionario = acceso.ejecutarConsultaTabla(consulta);
             if (datosFuncionario != null && datosFuncionario.Rows.Count == 1)
             {
                 resultado = datosFuncionario.Rows[0][7].ToString();
             }
             
         }
         catch (SqlException e)
         {
             resultado = "";
         }

         return resultado;
     }



        /*Método para consultar los teléfonos de un recurso humano específico.
       * Requiere: requiere la cédula del usuario al cual se le consultarán los teléfonos.
       * Modifica: lleva a cabo las consultas en la base de datos
       * Retorna: Devuelve un datatable (la fila de la base de datos que reponde a la consulta) con los teléfonos de un recurso humano específico.
       */
        public DataTable consultarTelefonosRH(string cedula)
     {
         DataTable telefonos = new DataTable();
         string consulta = "SELECT T.num_telefono " + " FROM Telefono_Funcionario T " + " WHERE T.cedula_funcionario = '" + cedula + "'";
         telefonos = acceso.ejecutarConsultaTabla(consulta);
         return telefonos;
     }

        /*Método para asignar un estado de sesión en la base de datos, para un usuario específico.
        * Requiere: requiere la cédula del usuario al cual se le asignará un estado específico para la sesión (abierta o cerrada), y el estado de la sesión.
        * Modifica: Modifica el valor del atributo de login.
        * Retorna: Devuelve un true si se ejecutó la actualización correctamente en la base de datos.
        */
        public Boolean modificarEstadoAbrir(string cedula)
        {
            string modif;
            try
            {
                modif = "UPDATE Funcionario SET login = 'True' WHERE cedula ='" + cedula + "'";
                return acceso.insertarDatos(modif);
            }
            catch (SqlException e)
            {
                return false;
            }
        }

        /*Método para consultar los datos de un recurso humano específico.
        * Requiere: requiere la cédula del usuario al cual se le consultarán los datos.
        * Modifica: lleva a cabo las consultas en la base de datos
        * Retorna: Devuelve un datatable (la fila de la base de datos que reponde a la consulta) con la cedula, nombre, apellido 1 y 2 y el tipo de rol de 
        * un usuario particular.
        */
        public DataTable consultarRecursosHumanos(string cedula)
     {
         DataTable dt = new DataTable();
         string consulta;

         if (cedula == null)
         {
             consulta = "SELECT F.cedula, F.nombre, F.apellido1, F.apellido2, M.tipo_Rol " + " FROM Funcionario F LEFT OUTER JOIN Miembro M ON F.cedula = M.cedula_miembro ";
         }
         else
         {
             consulta = "SELECT F.cedula, F.nombre, F.apellido1, F.apellido2, M.tipo_Rol " + " FROM Funcionario F LEFT OUTER JOIN Miembro M ON F.cedula = M.cedula_miembro " + " WHERE F.cedula = '" + cedula + "'"; 
         }

         dt = acceso.ejecutarConsultaTabla(consulta);
         return dt;
     }


   /*Método para insertar funcionario en la base de datos
    * Requiere: un objeto tipo Funcionario el cual trae todos los datos encapsulados
    * Modifica: Crea el string con la consulta y la envia a la clase que maneja la conexion con la base de datos para insertarlo
    * Retorna: true si la inserción fue exitosa, false si hubo algún error y no se insertó
    */
     public bool insertarFuncionario(Funcionario nuevo)
     {
         try
         {
             string insercion = "INSERT INTO Funcionario (cedula, nombre, apellido1, apellido2, email, usuario, contrasena, login) VALUES ('" + nuevo.getCedula + "', '" + nuevo.getNombre + "', '" + nuevo.getApellido1 + "', '" + nuevo.getApellido2 + "', '" + nuevo.getEmail + "','" + nuevo.getUsuario + "', '" +nuevo.getContrasena+ "', '"+nuevo.getLogin+ "')";
             return acceso.insertarDatos(insercion);
           
         }
         catch (SqlException e)
         {
             return false;
         }
     }


     /*Método para insertar administrador en la base de datos
      * Requiere: un objeto tipo EntidadAdministrador el cual trae todos los datos encapsulados
      * Modifica: Crea el string con la consulta y la envia a la clase que maneja la conexion con la base de datos para insertarlo
      * Retorna: true si la inserción fue exitosa, false si hubo algún error y no se insertó
      */
     public bool insertarAdministrador(EntidadAdministrador nuevo)
     {
         try
         {
             string insercion = "INSERT INTO Administrador (cedula_admin) VALUES ('" + nuevo.getCedula_Admin + "')";
             return acceso.insertarDatos(insercion);

         }
         catch (SqlException e)
         {
             return false;
         }
     }


     /*Método para insertar miembro en la base de datos
      * Requiere: un objeto tipo EntidadMiembro el cual trae todos los datos encapsulados
      * Modifica: Crea el string con la consulta y la envia a la clase que maneja la conexion con la base de datos para insertarlo
      * Retorna: true si la inserción fue exitosa, false si hubo algún error y no se insertó
      */
     public bool insertarMiembro(EntidadMiembro nuevo)
     {
         try
         {
             string insercion = "INSERT INTO Miembro (cedula_miembro, tipo_rol) VALUES ('" + nuevo.getCedulaMiembro + "', '"+nuevo.getTipoRol +"')";
             return acceso.insertarDatos(insercion);

         }
         catch (SqlException e)
         {
             return false;
         }
     }

     public bool insertarMiembroProyecto(EntidadTrabajaEn nuevo)
     {
         try
         {
             string insercion = "INSERT INTO Trabaja_En (cedula_miembro, id_proyecto) VALUES ('" + nuevo.getCedulaMiembro + "', '" + nuevo.geIdProyecto + "')";
             return acceso.insertarDatos(insercion);

         }
         catch (SqlException e)
         {
             return false;
         }
     }


     /*Método para insertar telefono de un funcionario en la base de datos
      * Requiere: un objeto tipo Funcionario el cual trae todos los datos encapsulados
      * Modifica: Crea el string EntidadTelFuncionario la consulta y la envia a la clase que maneja la conexion con la base de datos para insertarlo
      * Retorna: true si la inserción fue exitosa, false si hubo algún error y no se insertó
      */
     public bool insertarTelefono(EntidadTelFuncionario nuevo)
     {
         try
         {
             string insercion = "INSERT INTO Telefono_Funcionario (cedula_funcionario, num_telefono) VALUES ('" + nuevo.getCedulaFuncionario+ "', '" + nuevo.getNumTelefono + "')";
             return acceso.insertarDatos(insercion);

         }
         catch (SqlException e)
         {
             return false;
         }
     }

        /*Método para llevar a cabo la modificación de los datos personales de un usuario.
        * Requiere: un objeto Funcionario para ser modificado con los nuevos valores y la cédula de este, para localizarlo en la Base de datos.
        * Modifica: Modifica los datos de un usuario particular.
        * Retorna: Devuelve un true si se ejecutó la actualización correctamente en la base de datos.
        */
        public bool modificarFuncionario(Funcionario nuevo, String cedula)
        {
            try
            {
                string modif = "UPDATE Funcionario SET cedula ='" + nuevo.getCedula + "', nombre='" + nuevo.getNombre + "' , apellido1= '" + nuevo.getApellido1 + "', apellido2= '" + nuevo.getApellido2 + "', email = '"+nuevo.getEmail+"', usuario='" + nuevo.getUsuario + "' WHERE cedula='" + cedula+"';";
                return acceso.insertarDatos(modif);

            }
            catch (SqlException e)
            {
                return false;
            }
        }

        



        /*Método para asignar el valor de sesión cerrada en la base de datos
        * Requiere: requiere la cédula del usuario al cual se le asignará la sesión como cerrada en la base de datos.
        * Modifica: Modifica el valor del atributo de login.
        * Retorna: Devuelve un true si se ejecutó la actualización correctamente en la base de datos.
        */
        public bool modificarEstadoCerrar(String cedula)
        {
            string modif;
            try
            {
                modif = "UPDATE Funcionario SET login = 'False' WHERE cedula ='" + cedula + "'";
                return acceso.insertarDatos(modif);
            }
            catch (SqlException e)
            {
                return false;
            }
        }


        /*Método para buscar el perfil de un usuario específico.
        * Requiere: requiere la cédula del usuario al cual se leconsultará el perfil.
        * Modifica: Lleva a cabo la consulta en la base de datos para conocer sie le usuario es administrador o miembro.
        * Retorna: Un string con el perfil del usuario.
        */
        public string buscarPerfil(string cedulaDeFuncionario)
        {

            String resultado = "";
            try
            {
                string consulta = "SELECT * FROM Administrador WHERE cedula_admin ='" + cedulaDeFuncionario+"'";
                DataTable data = acceso.ejecutarConsultaTabla(consulta);
                if (data.Rows.Count == 1)
                {
                    resultado = "Administrador";
                }else
                {
                    try
                    {
                        string consultaMiembro = "SELECT * FROM Miembro WHERE cedula_miembro ='" + cedulaDeFuncionario+"'";
                        DataTable dataMiembro = acceso.ejecutarConsultaTabla(consultaMiembro);
                        if (dataMiembro.Rows.Count == 1)
                        {
                            resultado = "Miembro";
                        }
              


                    }
                    catch (SqlException e)
                    {
                        resultado = "";
                    }
                }                 
                
            }
            catch (SqlException e)
            {
               resultado = "";
            }

            return resultado;
          
        }


        /*Método para eliminar de la base de datos un funcionario específico
        * Requiere: un string con la cedula de un funcionario específico
        * Modifica: Genera las consultas para mandarlas a la base de datos para borrar el funcionario de la tabla Funcionario y Trabaja_En
        * Retorna: true si se llevó a cabo correctamente la eliminación y false si no fue existosa.
        */
        public bool eliminarFuncionario(String cedulaDeFuncionario)
        {

            try
            {
                string borrado = "Delete from Funcionario where cedula ='" + cedulaDeFuncionario +"'";
                acceso.eliminarDatos(borrado);
                string borrado2 = "Delete from Trabaja_En where cedula_miembro ='" + cedulaDeFuncionario+"'";
                acceso.eliminarDatos(borrado);

                return true;
            }
            catch (SqlException e)
            {
                return false;
            }

        }

        /*Método para eliminar los telefonos de cierto funcionario
        * Requiere: la cédula del funcionario
        * Modifica: crea el string de consulta y lo ejecunto en la BD 
        * Retorna: true si el borrado fue exitoso, false si hubo algún error y no se eliminó
        */
        public bool eliminarTelefonoFuncionario(string cedula)
        {
            try
            {
                string borrado = "Delete from Telefono_Funcionario where cedula_funcionario =" + cedula;
                return acceso.eliminarDatos(borrado);
            }
            catch (SqlException e)
            {
                return false;
            }
        }

        /*Método para obtener el nombre y apellido de los miembros que tienen como rol: líder de pruebas
         * Requiere: no requiere parámetros.
         * Retorna: un DataTable con el nombre y apellido de los miembros que son líderes.
         */
        public DataTable consultarLideres()
        {
            DataTable dt = new DataTable();
            string consulta;

            try
            {
                consulta = "SELECT F.nombre, F.apellido1, F.apellido2, F.cedula " + " FROM Funcionario F JOIN Miembro M ON F.cedula = M.cedula_miembro WHERE M.tipo_rol ='" + "Líder de pruebas" + "'"; ;
                dt = acceso.ejecutarConsultaTabla(consulta);
            }
            catch
            {
                dt = null;
            }

            return dt;
        }

        /*Método para obtener el nombre y apellidos de los miembros del sistema
        * Requiere: no requiere parámetros.
        * Retorna: un DataTable con el nombre y apellidos de los miembros
        */
        public DataTable consultarMiembros()
        {
            DataTable dt = new DataTable();
            string consulta;

            try
            {
                consulta = "SELECT F.nombre, F.apellido1, F.apellido2, M.tipo_Rol, F.cedula " + " FROM Funcionario F, Miembro M WHERE NOT EXISTS (SELECT NULL From Miembro M WHERE F.cedula = M.cedula_miembro AND M.tipo_rol = '" + "Líder de pruebas" + "'" + ")" + " AND F.cedula = M.cedula_miembro ";
                dt = acceso.ejecutarConsultaTabla(consulta);

            }
            catch (SqlException e)
            {


            }

            return dt;
        }


        /*Método para obtener el nombre y apellidos de los miembros asociados a un determinado proyecto
        * Requiere: un string con el identificador del proyecto para conocer los miembros que trabajan en éste.
        * Retorna: un DataTable con el nombre y apellidos de los miembros del proyecto
        */
        public DataTable consultarMiembrosProyecto(string idProyecto)
        {
            DataTable dt = new DataTable();
            string consulta;

            try
            {
                consulta = "SELECT F.nombre, F.apellido1, F.apellido2, M.tipo_rol " + " FROM Funcionario F, Miembro M, Trabaja_En T WHERE T.id_proyecto = '" + idProyecto + "'" + "AND T.cedula_miembro = F.cedula AND T.cedula_miembro = M.cedula_miembro ";
                dt = acceso.ejecutarConsultaTabla(consulta);

            }
            catch
            {
                dt = null;
            }

            return dt;
        }

        /*Método para obtener el/los proyectos a los cuales está asociado un miembro determinado
        * Requiere: un string con la cédula del miembro 
        * Retorna: un DataTable con el identificador del o los proyectos en los cuales el miembro trabaja
        */
        public DataTable consultarProyectosAsociados(string idUsuario)
        {
            DataTable dt = new DataTable();
            string consulta;

            try
            {
                consulta = "SELECT T.id_proyecto FROM Trabaja_En T WHERE T.cedula_miembro = '" + idUsuario + "'";
                dt = acceso.ejecutarConsultaTabla(consulta);
            }
            catch
            {
                dt = null;
            }

            return dt;
        }


        /*Método para obtener el nombre y apellido de los líderes
        * Requiere: una lista compuesta por las cédulas de los miembros que son líderes de ciertos proyectos
        * Retorna: un DataTable con el nombre y apellido de los líderes
        */
        public DataTable obtenerNombresLideres(List<string> cedLideres)
        {
            DataTable dt = new DataTable();
            string consulta = "";
            int contador = 0;

            try
            {
                for (int i = 0; i < cedLideres.Count; ++i)
                {
                    ++contador;
                    consulta = consulta + " " + "SELECT F.nombre, F.apellido1, F.apellido2 FROM Funcionario F WHERE F.cedula = '" + cedLideres[i] + "'";

                    if (contador != cedLideres.Count)
                    {
                        consulta = consulta + "UNION";
                    }
                }

                dt = acceso.ejecutarConsultaTabla(consulta);
            }
            catch
            {
                dt = null;
            }

            return dt;
        }
        /*Método que elimina un miembro de la tabla trabaja en
        * Requiere: id del proyecto
        * Retorna: booleano si lo logra
        */
        public bool eliminarTrabaja_En(int idProyectoConsultado)
        {
            try
            {
                string borrarTrabaja_En = "Delete from Trabaja_En where id_proyecto ='" + idProyectoConsultado + "';";
                acceso.eliminarDatos(borrarTrabaja_En);
                return true;
            }
            catch (SqlException e)
            {
                return false;
            }
        }


    }

}
