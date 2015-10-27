﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProyectoInge.App_Code.Capa_de_Control;
using System.Data;

namespace ProyectoInge
{
    public partial class CasoDePrueba : System.Web.UI.Page
    {
        ControladoraCasosPrueba controladoraCasoPruebas = new ControladoraCasosPrueba();
        private static int modo = 1;//1insertar, 2 modificar, 3eliminar

        /* Método para actualizar la interfaz de recursos humanos
         * Modifica: no modifica nada
         * Retorna: no retorna ningún valor */
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["cedula"] == null)
            {
                Response.Redirect("~/Login.aspx");

            }

            if (!IsPostBack)
            {
                controlarCampos(false);
                cambiarEnabled(false, this.btnModificar);
                cambiarEnabled(false, this.btnEliminar);
                cambiarEnabled(false, this.btnAceptar);
                cambiarEnabled(false, this.btnCancelar);
                llenarDropDownProyecto();
            }
        }

        /** Método para cerrar la sesión abierta de un usuario y dirigirse a la página de inicio.
         * Requiere: recibe el evento cuando se presiona el botón para cerrar sesión.
         * Modifica: Modifica el valor booleano del estado de la sesión
         * Retorna: No retorna ningún valor
         */

        protected void cerrarSesion(object sender, EventArgs e)
        {

            string ced = (string)Session["cedula"];
            Boolean a = controladoraCasoPruebas.cerrarSesion(ced);
            Response.Redirect("~/Login.aspx");

        }

        /*Método para habilitar/deshabilitar todos los campos y los botones + y -
         * Requiere: un booleano para saber si quiere habilitar o deshabilitar los botones y cajas de texto
         * Modifica: Cambia la propiedad Enabled de las cajas y botones
         * Retorna: no retorna ningún valor
         */
        protected void controlarCampos(Boolean condicion)
        {
            this.txtPrueba.Enabled = condicion;
            this.txtTecnicaPrueba.Enabled = condicion;
            this.txtTipoPrueba.Enabled = condicion;
            this.txtProposito.Enabled = condicion;
            this.txtIdentificador.Enabled = condicion;
            this.txtEntradaDatos.Enabled = condicion;
            this.txtFlujoCentral.Enabled = condicion;
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

        /*Método para crear la acción de insertar un nuevo  caso de prueba
         * Modifica: Cambia la propiedad enabled de botones y cajas de texto,
         * Limpia cajas de texto y coloca los ejemplos de datos donde es necesario
         * Retorna: no retorna ningún valor
         */
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
            llenarDropDownProyecto();
            //llenarDropDownProyecto();
            // UpdatePanelDropDown.Update();
        }

        /* Método para llenar el comboBox de los diferentes proyectos que existen
           * Modifica: llena el comboBox con los datos obtenidos de la BD
           * Retorna: no retorna ningún valor */

        protected void llenarComboProyecto()
        {
            this.comboProyecto.Items.Clear();
            DataTable Tipos = controladoraCasoPruebas.consultarProyectos();
            int numDatos = Tipos.Rows.Count;
            Object[] datos;


            if (Tipos.Rows.Count >= 1)
            {
                numDatos = Tipos.Rows.Count;
                datos = new Object[numDatos];

                for (int i = 0; i < Tipos.Rows.Count; ++i)
                {
                    datos[i] = Tipos.Rows[i][0].ToString();
                }

                this.comboProyecto.DataSource = datos;
                this.comboProyecto.DataBind();
            }
        }

        /*Método para limpiar los textbox
         * Requiere: No requiere parámetros
         * Modifica: Establece la propiedad text de los textbox en ""
         * Retorna: no retorna ningún valor
         */
        protected void vaciarCampos()
        {
            //this.txtPrueba.Text = "";
            //this.txtTecnicaPrueba.Text = "";
            //this.txtTipoPrueba.Text = "";
            this.txtProposito.Text = "";
            this.txtIdentificador.Text = "";
            this.txtResultadoEsperado.Text = "";
            this.txtEntradaDatos.Text = "";
            this.txtFlujoCentral.Text = "";
        }

        /* Método para llenar el comboBox de proyecto
       * Modifica: el campo del rol de acuerdo al campo del proyecto
        * Requiere: No requiere
       * Retorna: no retorna ningún valor */

        protected void llenarDropDownProyecto()
        {
            Object[] datos = new Object[2];

            datos[0] = "";
            datos[1] = "";
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
                        btnAceptar_Modificar();
                    }
                    break;
                case 3:
                    {
                        btnAceptar_Eliminar();
                    }
                    break;

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
            cambiarEnabled(true, this.btnCancelar);
        }

        /*Método para la acción de aceptar cuando esta en modo de inserción
         * Requiere: No requiere ningún parámetro
         * Modifica: Crea un objeto con los datos obtenidos en la interfaz mediante textbox y 
         * valida que todos los datos se encuentren para la inserción
         * Retorna: No retorna ningún valor
         */
        private void btnAceptar_Insertar()
        {
            int tipoInsercion = 1;   //1 insertar caso de prueba 

            //si faltan datos no deja insertar
            if (faltanDatos())
            {
                lblModalTitle.Text = " ";
                lblModalBody.Text = "Para insertar un nuevo caso de prueba debe completar todos los datos obligatorios.";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                upModal.Update();
                habilitarCamposInsertar();
            }
            else
            {
                Object[] datosNuevos = new Object[5];
                datosNuevos[0] = this.txtIdentificador.Text;
                datosNuevos[1] = this.txtPropósito.Text;
                datosNuevos[2] = this.txtFlujoCentral.Text;
                datosNuevos[3] = this.txtEntradaDatos.Text;
                datosNuevos[4] = this.txtResultadoEsperado.Text;

                if (controladoraCasoPruebas.ejecutarAccion(modo, tipoInsercion, datosNuevos, "", ""))
                {
                    Object[] datosNuevos2 = new Object[3];
                    datosNuevos[0] = obtenerIdReq();
                    datosNuevos[1] = obtenerIdProyecto();
                    datosNuevos[2] = obtenerIdCaso();
                }
            }

        }

        private object obtenerIdCaso()
        {
            throw new NotImplementedException();
        }

        private object obtenerIdReq()
        {
            throw new NotImplementedException();
        }

        private object obtenerIdProyecto()
        {
            throw new NotImplementedException();
        }

        private void btnAceptar_Modificar()
        {
            throw new NotImplementedException();
        }

        private void btnAceptar_Eliminar()
        {
            throw new NotImplementedException();
        }

        /*Método para saber si hay cajas de texto que no tienen datos en su interior
         * Recibe: No recibe ningún parámetro
         * Modifica:  Verifica si faltan datos en alguna caja de texto
         * Retorna: retorna true si alguna caja no tiene texto, false si todas las cajas tienen texto
         */
        protected bool faltanDatos()
        {

            bool resultado = false;
            if (this.txtPropósito.Text == "" || this.txtIdentificador.Text == "" || this.txtFlujoCentral.Text == "" || this.txtEntradaDatos.Text == "" || this.txtResultadoEsperado.Text == "")
            {
                resultado = true;
            }
            else
            {
                resultado = false;
            }

            return resultado;
        }
    }

}