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


        //PROBARRRR
        /*
        protected void LogIn(object sender, EventArgs e)
        {
            if (IsValid)
            {
                DataTable datosFilaFuncionario = controladora.consultarCedula(txtUsuario.Text, txtPassword.Text);
                if (datosFilaFuncionario != null && datosFilaFuncionario.Rows.Count > 0)
                {
                    string estaLogueado = datosFilaFuncionario.Rows[0][7].ToString();
                    if (estaLogueado == "0")
                    {
                        string cedulaDeFuncionario = datosFilaFuncionario.Rows[0][0].ToString();
                        Session["cedula"] = cedulaDeFuncionario;
                        string perfil = controladora.buscarPerfil(cedulaDeFuncionario);
                        Session["perfil"] = perfil;
                        Response.Redirect("~/RecursosHumanos.aspx");
                        controladora.modificarEstado(true, txtUsuario.Text);
                    }
                    else
                    {
                        FailureText.Text = "Ya la cuenta asociada a este usuario esta logueada en otra sesion";
                        ErrorMessage.Visible = true;

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
        **/
        



        // si sirve. Para probar: comentar este y descomentar el de arriba
     
        protected void LogIn(object sender, EventArgs e)
        {
            if (IsValid)
            {
                DataTable datosFilaFuncionario = controladora.consultarCedula(txtUsuario.Text, txtPassword.Text);
                if (datosFilaFuncionario != null && datosFilaFuncionario.Rows.Count > 0)
                {
                    string cedulaDeFuncionario = datosFilaFuncionario.Rows[0][0].ToString();
                    Session["cedula"] = cedulaDeFuncionario;
                    string perfil = controladora.buscarPerfil(cedulaDeFuncionario);
                    Session["perfil"] = perfil;
                    Response.Redirect("~/RecursosHumanos.aspx");
                }
                else
                {

                    FailureText.Text = "usuario o contrasena incorrecta.";
                    ErrorMessage.Visible = true;

                }


            }

        }


       


        //termina comentar metodo arriba







    }
}