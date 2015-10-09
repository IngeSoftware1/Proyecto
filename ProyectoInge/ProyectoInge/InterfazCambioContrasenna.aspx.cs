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
using ProyectoInge.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
//Revisar:
using System.Web.Security;


namespace ProyectoInge
{
    public partial class InterfazCambioContrasenna : System.Web.UI.Page
    {

        ControladoraRecursos controladora = new ControladoraRecursos();
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }


        protected void botonClick(object sender, EventArgs e)
        {
            Response.Redirect("~/Login.aspx");
        }




        protected void ChangePassword(object sender, EventArgs e)
        {

            

            Funcionario funcionarioValidado = controladora.validarFuncionario(txtUsuario.Text, txtAntPassword.Text);

            if (funcionarioValidado != null)
            {
                string cedulaDeFuncionario = funcionarioValidado.getCedula;
                Boolean resultado = controladora.modificarContrasena(cedulaDeFuncionario, txtAntPassword.Text, txtNewPassword.Text);

            }else {
                FailureText.Text = "No existe ese usuario, verifique";
                ErrorMessage.Visible = true;
            
            }



        }
    }

}