using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProyectoInge.App_Code;
using ProyectoInge.App_Code.Capa_de_Control;
using ProyectoInge.App_Code.Capa_de_Datos__Entidad_;
using System.Data;

//Necesarios para iniciar sesión correctamente
using Owin;
using ProyectoInge.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
//Revisar:
using System.Web.Security;


namespace ProyectoInge
{
    public partial class Login : System.Web.UI.Page
    {
        private static ControladoraRecursos controladora; //Instancia de la clase controladora, usada para interactuar con las demás controladoras.

        /** 
         * Función Page_Load.
         * Función default que se ejecuta cada vez que se carga la página.
         * Inicializa la controladora.
         * @param sender Objeto que invoca el evento, no se usa.
         * @param e El evento invocado, no se usa.
         */
        protected void Page_Load(object sender, EventArgs e)
        {
            controladora = new ControladoraRecursos();
        }

        /* Método para realizar el inicio de sesión
         * Requiere: haber presionado el botón iniciar sesión
         * Modifica: Modifica el valor del atributo de login.
         * Retorna: No devuelve datos
         */      
        protected void LogIn(object sender, EventArgs e)
        {
            string cedulaDeFuncionario;

            if (IsValid)
            {
               cedulaDeFuncionario = controladora.consultarCedula(txtUsuario.Text, txtPassword.Text);

               
                if (cedulaDeFuncionario.Equals("") == false)
                {
                    string estaLogueado = controladora.consultarEstadoFuncionario(cedulaDeFuncionario);
                    Response.Write(estaLogueado);
                    if (estaLogueado.Equals("False") == true)
                    {
                        Session["cedula"] = cedulaDeFuncionario;
                        string perfil = controladora.buscarPerfil(cedulaDeFuncionario);
                        Session["perfil"] = perfil;
                        Response.Redirect("~/RecursosHumanos.aspx");
                        controladora.modificarEstado(true, txtUsuario.Text);
                         
                    }
                    else
                    {


                        lblModalTitle.Text = "AVISO";
                        lblModalBody.Text = "Su cuenta esta abierta en otra sesión, su otra sesión será cerrada. Intente de nuevo";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                        upModal.Update();
                        controladora.modificarEstadoCerrar(cedulaDeFuncionario);
        
                    }
                }
                else
                {

                    FailureText.Text = "usuario o contrasena incorrecta.";
                    ErrorMessage.Visible = true;

                }

            }
            else
            {

                FailureText.Text = "usuario o contrasena incorrecta.";
                ErrorMessage.Visible = true;

            }
        }
        
    }
}