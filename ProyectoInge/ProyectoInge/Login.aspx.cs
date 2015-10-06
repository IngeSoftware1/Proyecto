﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProyectoInge.App_Code;
using ProyectoInge.App_Code.Capa_de_Control;
using ProyectoInge.App_Code.Capa_de_Datos__Entidad_;

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

        protected void LogIn(object sender, EventArgs e)
        {
            if (IsValid)
            {   
                // Validar la contraseña del usuario, enviarle a Controladora Recursos Humanos 
                var manager = new UserManager();
                ApplicationUser user = manager.Find(txtUsuario.Text, txtPassword.Text);

                if (user != null)
                {
                    IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
                }
                else
                {
                    FailureText.Text = "Correo o contrasena inconrrecta.";
                    ErrorMessage.Visible = true;
                }

            }
        }      

    }
}