using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using ProyectoInge.App_Code.Capa_de_Acceso_a_Datos;
using ProyectoInge.App_Code.Capa_de_Datos__Entidad_;
using ProyectoInge.App_Code;
using System.Web.UI.WebControls;


namespace ProyectoInge.App_Code.Capa_de_Control
{
      public class ControladoraRecursos 
      {
         ControladoraBDRecursos controladoraBDRecurso = new ControladoraBDRecursos();

        
          public ControladoraRecursos()
          {

          }

          public DataTable consultarUsuario(String user, String pass){
            
              return controladoraBDRecurso.consultarUsuario(user,pass);

          }

          public Boolean modificarContrasena(String user, String pass, String newPass)
          {
              Boolean resultado = controladoraBDRecurso.modificarContrasena(user,pass,newPass);
              return resultado;
          }

           /*
          public Funcionario validarUsuario(string nombreUsuario, string password)
          {
              DataRow datosObtenidos = controladoraBDRecurso.validarUsuario(nombreUsuario, password);
              if (datosObtenidos == null)
                  return null;
              Funcionario funcionarioValidado = new Funcionario(datosObtenidos.ItemArray);
              return funcionarioValidado;
          }
          */
 

      }
}
