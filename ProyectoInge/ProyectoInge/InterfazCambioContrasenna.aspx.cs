using System;
using System.Collections.Generic;
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

            DataTable datosUsuario = controladora.consultarUsuario(txtUsuario.Text, txtAntPassword.Text);
            if (datosUsuario.Rows.Count == 1)
            {
                Boolean resultado = controladora.modificarContrasena(txtUsuario.Text, txtAntPassword.Text, txtNewPassword.Text);

            }


        }
    }

}