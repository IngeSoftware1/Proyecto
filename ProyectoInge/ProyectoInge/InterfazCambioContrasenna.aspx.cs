using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using ProyectoInge.Models;
using ProyectoInge.App_Code;
using ProyectoInge.App_Code.Capa_de_Control;
using System.Drawing.Printing;
using System.Web.UI.WebControls;
using System.Data;
using ProyectoInge.App_Code.Capa_de_Datos__Entidad_;
//Necesarios para iniciar sesión correctamente
using Owin;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
//Revisar:
using System.Web.Security;
using System.Windows;


namespace ProyectoInge
{


    public partial class InterfazCambioContrasenna : System.Web.UI.Page
    {

        ControladoraRecursos controladora = new ControladoraRecursos();


        /*Método para cargar la página para cambiar la contraseña
         * Requiere: que se presione el enlace cambiar contraseña
         * Modifica: No modifica nada
         * Retorna: No retorna datos
         */
        protected void Page_Load(object sender, EventArgs e)
        {
          
        }

        /*Método para salir de la página actual
         * Requiere: presionar botón cancelar
         * Modifica: No modifica nada
         * Retorna: No retorna datos
         */
        protected void botonClick(object sender, EventArgs e)
        {
            Response.Redirect("~/Login.aspx");
        }

        /*Método para limpiar los textbox
         * Requiere: No requiere parámetros
         * Modifica: Establece la propiedad text de los textbox en ""
         * Retorna: no retorna ningún valor
         */
        protected void vaciarCampos()
        {
            this.txtUsuario.Text = "";
            this.txtAntPassword.Text = "";
            this.txtNewPassword.Text = "";
        }



        /*Método para hacer cambiar contraseña
         * Requiere: requiere presionar el botón aceptar
         * Modifica: modifica la contraseña en la base de datos
         * Retorna: No retorna datos
         */
        protected void cambiarContrasenna(object sender, EventArgs e)
        {
            Boolean resultado = false;
           string cedulaDeFuncionario =  controladora.consultarCedula(txtUsuario.Text, txtAntPassword.Text);
          
           if (cedulaDeFuncionario.Equals("")==false)
            {
                
                if(contrasenasIguales() == true){
                    resultado = controladora.modificarContrasena(cedulaDeFuncionario, txtAntPassword.Text, txtNewPassword.Text);
                    
                    
                }
                else
                {
                    FailureText.Text = "Contraseña en el campo confirmar no coincide con la nueva contraseña";
                    ErrorMessage.Visible = true;
                }


            }else {
                FailureText.Text = "usuario o contraseña incorrecta";
                ErrorMessage.Visible = true;
            
            }

           vaciarCampos();
            
            


        }


        /*Método para obtener si las contraseñas son diferentes
         * Requiere: no recibe parámetros
         * Modifica: Compara ambas cajas de texto
         * Retorna: retorna true si las contraseñas son iguales, false si las contraseñas son distintas
         */
        protected bool contrasenasIguales()
        {
            bool resultado = false;

            if (txtNewPassword.Text == txtConfPassword.Text)
            {
                resultado = true;
            }

            return resultado;
        }

    }

}