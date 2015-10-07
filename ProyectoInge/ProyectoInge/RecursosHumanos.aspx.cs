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
        private static int modo = 1;//1insertar, 2 modificar, 3eliminar
        private int perfil;
        private static int idRecursosHumanos = -1;

        protected void Page_Load(object sender, EventArgs e)
        {
            controlarCampos(false);
            cambiarEnabled(false, this.btnModificar);
            cambiarEnabled(false, this.btnEliminar);
            cambiarEnabled(false, this.btnAceptar);
            cambiarEnabled(false, this.btnCancelar);
            cambiarEnabled(true, this.btnInsertar);
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

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            modo = 2;
            //Iluminar btnModificar
            if (rhConsultado()) {
                if (idRecursosHumanos != -1)
                {
                    habilitarCamposModificar(perfil);
                }

            }
           
        }
        //TODO
        private bool rhConsultado()//Me dice si hay un RH seleccionado del Grid, y puesto en los txtbox
        {
            throw new NotImplementedException();
        }


        /*Método para habilitar/deshabilitar el botón
          * Requiere: el booleano para la acción
          * Modifica: La propiedad enable del botón
          * Retorna: no retorna ningún valor
          */
        protected void cambiarEnabled(bool condicion, Button boton)
        {
            boton.Enabled = condicion;
        }

       
        protected void habilitarCamposModificar(int perfil)//si es Administrador es 1, si no es 2
        {
            if(perfil== 1)
            {
                controlarCampos(true);
            }
            else if(perfil == 2)
            {
                this.txtCedula.Enabled = true;
                this.txtNombre.Enabled = true;
                this.txtApellido1.Enabled = true;
                this.txtApellido2.Enabled = true;
                this.comboPerfil.Enabled = false;
                this.comboRol.Enabled = false;
                this.txtUsuario.Enabled = false;
                this.txtContrasena.Enabled = false;
                this.txtConfirmar.Enabled = false;
                this.txtTelefono.Enabled = true;
                this.btnNumero.Enabled = true;
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
           /* modo = 3;
            if (idRecursos Hmanos != -1){
                controladoraRecursos Hmanos.ejecutarAccion(modo, idRecursos Hmanos);
            }
            idRecursos Hmanos = -1;
            llenarGrid();*/
            vaciarCampos();
        }

        protected void btnInsertar_Click(object sender, EventArgs e)
        {
            vaciarCampos();
            controlarCampos(true);
            modo = 1;
            cambiarEnabled(true, this.btnAceptar);
            cambiarEnabled(true, this.btnCancelar);
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            switch (modo) {
                case '1':btnAceptar_Insertar();
                    break;
                case '2': btnAceptar_Modificar();
                    break;
                case '3': btnAceptar_Eliminar();
                    break; 

            }
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

        protected void btnAceptar_Insertar() {
            Object[] datosNuevos = new Object[7];
            datosNuevos[0] = this.txtCedula.Text;
            datosNuevos[1] = this.txtNombre.Text;
            datosNuevos[2] = this.txtApellido1.Text;
            datosNuevos[3] = this.txtApellido2.Text;
            datosNuevos[4] = this.txtUsuario.Text;
            datosNuevos[5] = this.txtContrasena.Text;
            datosNuevos[6] = false;
        }
        protected void btnAceptar_Modificar() {

            //Valida datos, campos
            //llama a modificar en la BD

        }
        protected void btnAceptar_Eliminar() { }

    }
}