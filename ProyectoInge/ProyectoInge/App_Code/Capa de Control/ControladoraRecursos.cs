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

         public bool ejecutarAccion(int modo, int accion, Object[] datos)
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

      }
}
