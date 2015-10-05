using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoInge
{
    public partial class RecursosHumanos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            controlarCampos(false);
        }

        protected void controlarCampos(Boolean condicion)
        {
            this.txtCedula.Enabled = condicion;
            this.txtNombre.Enabled = condicion;
            this.txtApellido1.Enabled = condicion;
            this.txtApellido2.Enabled = condicion;
            this.comboPerfil.Enabled = condicion;
            this.comboRol.Enabled = condicion;
            this.txtUsuario.Enabled = condicion;
            this.txtContrasena.Enabled = condicion;
            this.txtConfirmar.Enabled = condicion;
            this.txtTelefono.Enabled = condicion;
            this.btnNumero.Enabled = condicion;
        }

        protected void btnInsertar_Click(object sender, EventArgs e)
        {
            vaciarCampos();
            controlarCampos(true);
        }

        protected void vaciarCampos()
        {
            this.txtCedula.Text = "";
            this.txtNombre.Text = "";
            this.txtApellido1.Text = "";
            this.txtApellido2.Text = "";
            this.txtUsuario.Text = "";
            this.txtContrasena.Text = "";
            this.txtConfirmar.Text = "";
            this.txtTelefono.Text = "";
        }
    }
}