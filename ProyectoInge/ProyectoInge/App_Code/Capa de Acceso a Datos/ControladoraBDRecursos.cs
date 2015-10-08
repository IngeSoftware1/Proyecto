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
          try
         {
             string consulta = "SELECT * FROM Funcionario WHERE usuario =" + user + " and contrasena = " + pass;
             SqlDataReader data = acceso.ejecutarConsulta(consulta);
             if (data.NextResult() == true)
             {
                 return true;

             }
             else {
                 return true;
             }

         }
          catch (SqlException e)
          {
              return false;
          }
         
     }

     public Boolean modificarContrasena(String user, String pass, String newPass)
     {
         try
         {
             string consulta = "UPDATE FUNCIONARIO SET = newPass WHERE usuario =" + user + " and contrasena = " + pass;
             SqlDataReader data = acceso.ejecutarConsulta(consulta);
             if (data.NextResult() == true)
             {
                 return true;

             }
             else
             {
                 return true;
             }

         }
         catch (SqlException e)
         {
             return false;
         }

     }

     /* Retorna la fila¨de la tabla funcionario en caso de que encuentre el usuario y contraseña y en caso de que no esta retorna null  */
     public DataRow validarFuncionario(string user, string password)
     {
         DataTable funcionarioValidado = new DataTable();
         //= adapterRH.validarFuncionario(user, password);
         if (funcionarioValidado.Rows.Count == 1)
             return funcionarioValidado.Rows[0];
         return null;
     }

   
     public DataTable consultarRH(string cedula) {
        DataTable dt = new DataTable();
        //dt = adapterRH.GetData(cedula);
        return dt;
    }

     public bool insertarFuncionario(Funcionario nuevo)
     {
         try
         {
             string insercion = "INSERT INTO Funcionario (cedula, nombre, apellido1, apellido2, usuario, contrasena, login) VALUES ('" + nuevo.getCedula + "', '" +nuevo.getNombre+ "', '" +nuevo.getApellido1+ "', '" +nuevo.getApellido2+ "', '" +nuevo.getUsuario+ "', '" +nuevo.getContrasena+ "', '"+nuevo.getLogin+ "' ";
             return acceso.insertarDatos(insercion);
         }
         catch (SqlException e)
         {
             int r = e.Number;
             if (r == 2627)
             {
                 // "Ya existe una venta con este id";
             }
             else
             {
                 //"Se ha producido un error al insertar la venta";
             }
             return false;
         }
     }

      }
}
