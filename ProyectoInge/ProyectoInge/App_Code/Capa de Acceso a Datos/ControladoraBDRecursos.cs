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
     
        //RHTableAdapter adapterRH;                  
        AccesoBaseDatos acceso = new AccesoBaseDatos();
     public ControladoraBDRecursos()
      {
          //adapterRH = new RHTableAdapter();	
      }

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

     /* Retorna la fila¨de la tabla funcionario en caso de que encuentre el usuario y contraseña y en caso de que no esta retorna null  */
     public DataTable consultarCedula(string user, string password)
     {
         DataTable datosFuncionario = new DataTable();
         string consulta = "SELECT * FROM Funcionario WHERE usuario='" + user + "' and contrasena = '" + password +"'";
         try{
             datosFuncionario = acceso.ejecutarConsultaTabla(consulta);
              modificarEstado(true,user);

         }
         catch (SqlException e) {
             datosFuncionario = null;
         }

         return datosFuncionario;
     }

    public DataTable consultarRoles()
     {
         DataTable Roles;
         string consulta = "SELECT R.tipo_rol " + " FROM Rol R ";
         Roles = acceso.ejecutarConsultaTabla(consulta);
         return Roles;
     }

     public DataTable consultarRH(string ced)
     {
         DataTable datosFuncionario = new DataTable();
         string consulta = "SELECT F.cedula, F.nombre, F.apellido1, F.apellido2, F.usuario, F.Email, M.tipo_rol " + " FROM Funcionario F LEFT OUTER JOIN Miembro M ON F.cedula = M.cedula_miembro " + " WHERE F.cedula = '" + ced + "'";
         datosFuncionario = acceso.ejecutarConsultaTabla(consulta);
         return datosFuncionario;
     }

     public DataTable consultarTelefonosRH(string cedula)
     {
         DataTable telefonos = new DataTable();
         string consulta = "SELECT T.num_telefono " + " FROM Telefono_Funcionario T " + " WHERE T.cedula_funcionario = '" + cedula + "'";
         telefonos = acceso.ejecutarConsultaTabla(consulta);
         return telefonos;
     }

     public Boolean modificarEstado(Boolean es, string user)
     {
         string modif;
         try
         {
             modif = "UPDATE Funcionario SET login ='" + es + "' WHERE usuario ='" + user + "'";
             return acceso.insertarDatos(modif);
         }
         catch (SqlException e)
         {
             return false;
         }
     }




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

        public bool modificarEstadoCerrar(String cedula)
        {
            string modif;
            try
            {
                modif = "UPDATE Funcionario SET login = 'false' WHERE cedula ='" + cedula + "'";
                return acceso.insertarDatos(modif);
            }
            catch (SqlException e)
            {
                return false;
            }
        }

        public bool buscarAsignacionProyectos(string cedulaDeFuncionario)
        {

            bool resultado = false;
            try
            {
                string consultaP = "SELECT * FROM Proyecto WHERE cedula_creador ='" + cedulaDeFuncionario+"'";
                DataTable data = acceso.ejecutarConsultaTabla(consultaP);
                if (data.Rows.Count >= 1)
                {
                    resultado = true;
                }
                else
                {
                    resultado = false;
                }

            }
            catch (SqlException e)
            {
                resultado = false;
            }

            return resultado;

        }

        public bool buscarAsignacionMiembros(string cedulaDeFuncionario)
        {

            bool resultado = false;
            try
            {
                string consultaProyectos = "SELECT * FROM Proyecto WHERE cedula_lider ='" + cedulaDeFuncionario+"'";
                DataTable data = acceso.ejecutarConsultaTabla(consultaProyectos);
                if (data.Rows.Count >= 1)
                {
                    resultado = true;
                }
                else
                {
                    try
                    {
                        string consultaPruebas = "SELECT * FROM Diseno_Pruebas WHERE cedula_responsable ='" + cedulaDeFuncionario+"'";
                        DataTable dataDiseno = acceso.ejecutarConsultaTabla(consultaPruebas);
                        if (dataDiseno.Rows.Count >= 1)
                        {
                            resultado = true;
                        }
                        else
                        {
                            try
                            {
                                string consultaEjecucionPruebas = "SELECT * FROM Ejecucion_Prueba WHERE cedula_responsable ='" + cedulaDeFuncionario+"'";
                                DataTable dataEjec = acceso.ejecutarConsultaTabla(consultaEjecucionPruebas);
                                if (dataEjec.Rows.Count >= 1)
                                {
                                    resultado = true;
                                }

                            }
                            catch (SqlException e)
                            {
                                resultado = false;
                            }
                        }
                    }
                    catch (SqlException e)
                    {
                        resultado = false;
                    }
                }

            }
            catch (SqlException e)
            {
                resultado = false;
            }
            return resultado;
        }

        public  string buscarPerfil(string cedulaDeFuncionario)
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
    }

}
