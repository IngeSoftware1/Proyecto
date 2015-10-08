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

          public bool consultarUsuario(String user, String pass){
            
              return controladoraBDRecurso.consultarUsuario(user,pass);

          }

          public Boolean modificarContrasena(String user, String pass, String newPass)
          {
              Boolean resultado = controladoraBDRecurso.modificarContrasena(user,pass,newPass);
              return resultado;
          }

         
         /* Este metodo es llamado por Login.aspx.cs
          * Retorna: Funcionatio
          * Valida si exite un usuario y en caso de que exista de una vez devuelve la Entidad Funcionario cambi
          * 
         */
         public Funcionario validarFuncionario(string user, string password)
         {
             DataRow datosObtenidos = controladoraBDRecurso.validarFuncionario(user, password);
             if (datosObtenidos == null)
                 return null;
             Funcionario funcionarioValidado = new Funcionario(datosObtenidos.ItemArray);
             return funcionarioValidado;
         }

         public Boolean ejecutarAccion(int modo, Object[] datos)
         {
             Boolean resultado = false;
             switch (modo)
             {
                 case 1:
                     { // INSERTAR
                         Funcionario nuevo = new Funcionario(datos);
                         resultado = controladoraBDRecurso.insertarFuncionario(nuevo);
                     }
                     break;
                 case 2:
                     { // MODIFICAR

                         resultado = false;
                     }
                     break;
             }
             return resultado;
         }

      }
}
