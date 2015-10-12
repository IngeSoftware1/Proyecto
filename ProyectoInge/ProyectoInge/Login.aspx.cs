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

        /*Método para realizar el inicio de sesión
         * Requiere: haber presionado el botón iniciar sesión
         * Modifica: Modifica el valor del atributo de login.
         * Retorna: No devuelve datos
         */
        
        protected void LogIn(object sender, EventArgs e)
        {
            string cedulaDeFuncionario;
            if (IsValid)
            {
                DataTable datosFilaFuncionario = controladora.consultarCedula(txtUsuario.Text, txtPassword.Text);
                if (datosFilaFuncionario != null && datosFilaFuncionario.Rows.Count == 1)
                {
                    string estaLogueado = datosFilaFuncionario.Rows[0][7].ToString();
                    
                    if (estaLogueado == "False")
                    {
                        cedulaDeFuncionario = datosFilaFuncionario.Rows[0][0].ToString();
                        Session["cedula"] = cedulaDeFuncionario;
                        string perfil = controladora.buscarPerfil(cedulaDeFuncionario);
                        Session["perfil"] = perfil;
                        Response.Redirect("~/RecursosHumanos.aspx");
                        controladora.modificarEstado(true, txtUsuario.Text);
                    }
                    else
                    {
                        cedulaDeFuncionario = datosFilaFuncionario.Rows[0][0].ToString();
                        controladora.modificarEstadoCerrar(cedulaDeFuncionario);
                        FailureText.Text = "Ya la cuenta asociada a este usuario esta logueada en otra sesion, espere unos minutos";
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
        
    }
}