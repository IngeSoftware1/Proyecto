using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using ProyectoInge.Models;
using ProyectoInge.App_Code;
using ProyectoInge.App_Code.Capa_de_Control;
using System.Drawing.Printing;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Data;

namespace ProyectoInge.Account
{
    public partial class Login : Page
    {

        ControladoraRecursos controladora = new ControladoraRecursos();
        protected void Page_Load(object sender, EventArgs e)
        {
            /*RegisterHyperLink.NavigateUrl = "Register";
            OpenAuthLogin.ReturnUrl = Request.QueryString["ReturnUrl"];
            var returnUrl = HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
            if (!String.IsNullOrEmpty(returnUrl))
            {
                RegisterHyperLink.NavigateUrl += "?ReturnUrl=" + returnUrl;
            }*/
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