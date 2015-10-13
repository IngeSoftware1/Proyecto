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

        private static int modo = 1; //1 insertar, 2 modificar, 3 eliminar

        ControladoraProyecto controladoraProyecto = new ControladoraProyecto();

        protected void Page_Load(object sender, EventArgs e)
        {
            /*if (Session["cedula"] == null)
            {
                Response.Redirect("~/Login.aspx");

            }*/

        }

        protected void btnInsertar_Click(object sender, EventArgs e)
        {
            vaciarCampos();
            controlarCampos(true);
            modo = 1;
            cambiarEnabled(true, this.btnAceptar);
            cambiarEnabled(true, this.btnCancelar);
            cambiarEnabled(false, this.btnModificar);
            cambiarEnabled(false, this.btnEliminar);
            cambiarEnabled(false, this.btnInsertar);
        }

        /*Método para limpiar los textbox
         * Requiere: No requiere parámetros
         * Modifica: Establece la propiedad text de los textbox en "" y limpia los listbox
         * Retorna: no retorna ningún valor
         */
        protected void vaciarCampos()
        {
            this.txtNombreProy.Text = "";
            this.txtObjetivo.Text="";
            this.txtnombreOficina.Text = "";
            this.txtnombreRep.Text = "";
            this.txtApellido1Rep.Text = "";
            this.txtApellido2Rep.Text = "";
            this.txtTelefonoOficina.Text = "";
            this.listTelefonosOficina.Items.Clear();
            this.listMiembrosDisponibles.Items.Clear();
            this.listMiembrosAgregados.Items.Clear();
        }

        /*Método para habilitar/deshabilitar todos los campos y los botones + y -
         * Requiere: un booleano para saber si quiere habilitar o deshabilitar los botones y cajas de texto
         * Modifica: Cambia la propiedad Enabled de las cajas y botones
         * Retorna: no retorna ningún valor
         */
        protected void controlarCampos(Boolean condicion)
        {
            this.txtNombreProy.Enabled = condicion;
            this.txtObjetivo.Enabled = condicion;
            this.comboEstado.Enabled = condicion;
            this.calendarFecha.Enabled = condicion;
            this.txtnombreOficina.Enabled = condicion;
            this.txtnombreRep.Enabled = condicion;
            this.txtApellido1Rep.Enabled = condicion;
            this.txtApellido2Rep.Enabled = condicion;
            this.txtTelefonoOficina.Enabled = condicion;
            this.comboLider.Enabled = condicion;
            this.lnkNumero.Enabled = condicion;
            this.lnkQuitar.Enabled = condicion;
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

        /*Método para habilitar/deshabilitar el botón tipo LinkButton
         * Requiere: el booleano para la acción
         * Modifica: La propiedad enable del botón
         * Retorna: no retorna ningún valor
         */
        protected void cambiarEnabledTel(bool condicion, LinkButton boton)
        {
            boton.Enabled = condicion;
        }

        /*Método para distinguir las diferentes situaciones en las que se puede seleccionar el botón de aceptar.
         * Requiere: Recibe el evento cuando se presiona el botón de aceptar
         * Modifica: Contiene un case el cual depende del tipo de modo en que se encuentre la aplicación ya sea Insertar, Modificar o Eliminar (1,2,3 respectivamente)
         * Retorna: no retorna ningún valor
         */
        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            switch (modo)
            {
                case 1:
                    {
                        btnAceptar_Insertar();
                    }
                    break;

                case 2:
                    {
                        //btnAceptar_Modificar();
                    }
                    break;
                case 3:
                    {
                        //btnAceptar_Eliminar();
                    }
                    break;

            }
        }

        /*Método para la acción de aceptar cuando esta en modo de inserción
         * Requiere: No requiere ningún parámetro
         * Modifica: Crea un objeto con los datos obtenidos en la interfaz mediante textbox y 
         * valida que todos los datos se encuentren para la inserción
         * Retorna: No retorna ningún valor
         */
        protected void btnAceptar_Insertar()
        {
            int tipoInsercion;

            //si faltan datos no deja insertar
            if (faltanDatos())
            {
                string mensaje = "<script>window.alert('Para insertar un nuevo proyecto debe completar todos los datos.');</script>";
                Response.Write(mensaje);
                habilitarCamposInsertar();
            }
            else
            {
 
            }
        }

        /*Método para habilitar los campos y botones cuando se debe seguir en la funcionalidad insertar
        * Requiere: no recibe parámetros
        * Modifica: Modifica la propiedad enabled de los distintos controles
        * Retorna: No retorna ningún valor
        */
        protected void habilitarCamposInsertar()
        {
            controlarCampos(true);
            cambiarEnabled(false, this.btnInsertar);
            cambiarEnabled(false, this.btnModificar);
            cambiarEnabled(false, this.btnEliminar);
            cambiarEnabled(true, this.btnAceptar);
            cambiarEnabledTel(true, this.lnkNumero);
            cambiarEnabledTel(true, this.lnkQuitar);
            cambiarEnabled(true, this.btnCancelar);

        }

        /*Método para saber si hay cajas de texto que no tienen datos en su interior
         * Recibe: No recibe ningún parámetro
         * Modifica:  Verifica si faltan datos en alguna caja de texto
         * Retorna: retorna true si alguna caja no tiene texto, false si todas las cajas tienen texto
         */
        protected bool faltanDatos()
        {

            bool resultado = false;
 
                //Pregunta por todas las cajas
                if (this.txtNombreProy.Text == "" || this.txtObjetivo.Text == "" || this.txtnombreOficina.Text == "" || this.txtnombreRep.Text == "" || this.txtApellido1Rep.Text == "" || this.txtApellido2Rep.Text == "")
                {
                    resultado = true;
                }
                else
                {
                    resultado = false;
                }
            
           
            return resultado;
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