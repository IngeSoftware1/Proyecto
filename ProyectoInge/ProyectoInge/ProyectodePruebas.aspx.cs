using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProyectoInge.App_Code.Capa_de_Control;

namespace ProyectoInge
{
    public partial class ProyectodePruebas : System.Web.UI.Page
    {

        ControladoraProyecto controladoraProyecto = new ControladoraProyecto();
        protected void Page_Load(object sender, EventArgs e)
        {
            /*if (Session["cedula"] == null)
            {
                Response.Redirect("~/Login.aspx");

            }*/

        }

       /** Método para cerrar la sesión abierta de un usuario y dirigirse a la página de inicio.
         * Requiere: recibe el evento cuando se presiona el botón para cerrar sesión.
         * Modifica: Modifica el valor booleano del estado de la sesión
         * Retorna: No retorna ningún valor
         */
        protected void cerrarSesion(object sender, EventArgs e)
        {

            string ced = (string)Session["cedula"];
            Boolean a = controladoraProyecto.cerrarSesion(ced);
            Response.Redirect("~/Login.aspx");
        }


    }
}