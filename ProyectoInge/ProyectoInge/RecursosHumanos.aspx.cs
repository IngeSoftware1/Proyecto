﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProyectoInge.App_Code.Capa_de_Control;
using System.Data;
using System.Diagnostics;

namespace ProyectoInge
{
    public partial class RecursosHumanos : System.Web.UI.Page
    {
        ControladoraRecursos controladoraRH = new ControladoraRecursos();

        private static int modo = 1;//1insertar, 2 modificar, 3eliminar
        private int perfil = 1;
        private static int idRecursosHumanos = -1;
        private static string idRH = "";
        private static String cedulaGuardadaBD = "";
        private static String perfilGuardadoBD = "";
        private static String funcionarioInsertado = "";
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
                ponerNombreDeUsuarioLogueado();
                controlarCampos(false);
                cambiarEnabled(false, this.btnModificar);
                cambiarEnabled(false, this.btnEliminar);
                cambiarEnabled(false, this.btnAceptar);
                cambiarEnabled(false, this.btnCancelar);
                llenarDropDownPerfil();
                llenarDropDownRol();

                //El unico botón que cambia de acuerdo al perfil es el de insertar y el grid se llena de acuerdo al tipo de usuario utilizando el sistema

                if (Session["perfil"].ToString().Equals("Administrador"))
                {

                    cambiarEnabled(true, this.btnInsertar);
                    llenarGrid(null);
                }
                else
                {
                    cambiarEnabled(false, this.btnInsertar);
                    llenarGrid(Session["cedula"].ToString());
                }
            }
        }
        /* Método para llenar el comboBox de perfil
        * Modifica: el campo del rol de acuerdo al campo del perfil
         * Requiere: No requiere
        * Retorna: no retorna ningún valor */

        protected void llenarDropDownPerfil()
        {
            this.comboPerfil.Items.Clear();
            Object[] datos = new Object[2];

            datos[0] = "Administrador";
            datos[1] = "Miembro de equipo de pruebas";
            this.comboPerfil.DataSource = datos;
            this.comboPerfil.DataBind();
            UpdatePanelDropDown.Update();
        }

        /* Método para habilitar el campo del rol en caso de que el perfil sea un miembro y para bloquearlo cuando no 
        * Modifica: el campo del rol de acuerdo al campo del perfil
        * Retorna: no retorna ningún valor */
        protected void perfilSeleccionado(object sender, EventArgs e)
        {
            if (this.comboPerfil.SelectedIndex != -1)
            {
                if (this.comboPerfil.Items[this.comboPerfil.SelectedIndex].Text == "Administrador")
                {
                    this.comboRol.Enabled = false;
                }
                else
                {
                    this.comboRol.Enabled = true;
                }
            }

            UpdatePanelDropDown.Update();
        }

        /* Método para llenar el comboBox según los datos datos almacenados en la BD
         * Modifica: llena el comboBox con los datos obtenidos de la BD
         * Retorna: no retorna ningún valor */

        protected void llenarDropDownRol()
        {
            this.comboRol.Items.Clear();
            DataTable tiposRoles = controladoraRH.consultarRoles();
            int numDatos = tiposRoles.Rows.Count;
            Object[] datos;

            if (tiposRoles.Rows.Count >= 1)
            {

                numDatos = tiposRoles.Rows.Count;
                datos = new Object[numDatos + 1];

                for (int i = 0; i < tiposRoles.Rows.Count; ++i)
                {
                    datos[i + 1] = tiposRoles.Rows[i][0].ToString();

                }
                datos[0] = "--";
                this.comboRol.DataSource = datos;
                this.comboRol.DataBind();

            }

            if (this.comboPerfil.Text == "Administrador")
            {
                this.comboRol.Enabled = false;
            }

            UpdatePanelDropDown.Update();
        }

        /*Método para habilitar/deshabilitar todos los campos y los botones + y -
         * Requiere: un booleano para saber si quiere habilitar o deshabilitar los botones y cajas de texto
         * Modifica: Cambia la propiedad Enabled de las cajas y botones
         * Retorna: no retorna ningún valor
         */
        protected void controlarCampos(Boolean condicion)
        {
            this.txtCedula.Enabled = condicion;
            this.txtNombre.Enabled = condicion;
            this.txtApellido1.Enabled = condicion;
            this.txtApellido2.Enabled = condicion;
            this.txtEmail.Enabled = condicion;
            this.comboPerfil.Enabled = condicion;
            this.comboRol.Enabled = condicion;
            this.txtUsuario.Enabled = condicion;
            this.txtContrasena.Enabled = condicion;
            this.txtConfirmar.Enabled = condicion;
            this.txtTelefono.Enabled = condicion;
            this.lnkNumero.Enabled = condicion;
            this.lnkQuitar.Enabled = condicion;
        }


        /*Método para obtener el registro que se desea consulta en el dataGriedViw y mostrar los resultados de la consulta en pantalla.
        * Modifica: el valor del la variable idRH con la cédula del funcionario que se desea consultar y se realiza el llamado al 
        método llenarDatos(idRH) el cual llena los campos de la interfaz con los resultados de la consulta especificada mediante el número de cédula.
        * Retorna: no retorna ningún valor
        */
        protected void gridFuncionarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "seleccionarRH")
            {

                LinkButton lnkConsulta = (LinkButton)e.CommandSource;
                idRH = lnkConsulta.CommandArgument;

                controlarCampos(false);
                llenarDatos(idRH);
                cambiarEnabled(true, this.btnModificar);
                cambiarEnabled(true, this.btnCancelar);
                cambiarEnabled(false, this.btnAceptar);

                //El unico botón que cambia de acuerdo al perfil es el de eliminar
                if (Session["perfil"].ToString().Equals("Administrador"))
                {

                    cambiarEnabled(true, this.btnEliminar);
                    cambiarEnabled(true, this.btnInsertar);
                }
                else
                {
                    cambiarEnabled(false, this.btnEliminar);
                    cambiarEnabled(false, this.btnInsertar);
                }
            }

        }

        /*Método para habilitar/deshabilitar todos los campos y los botones que permite el modificar, escucha al boton modificar
         * Requiere: object sender, EventArgs e
         * Modifica: Cambia la propiedad Enabled de las cajas y botones
         * Retorna: no retorna ningún valor
         */
        protected void btnModificar_Click(object sender, EventArgs e)
        {
            cambiarEnabled(false, this.btnInsertar);
            cambiarEnabled(false, this.btnEliminar);
            cambiarEnabled(false, this.btnModificar);
            //llenar los txtbox con la table
            cambiarEnabled(true, this.btnAceptar);
            cambiarEnabled(true, this.btnCancelar);
            //llenarDropDownPerfil();

            modo = 2;

            habilitarCamposModificar();
            //llenarDropDownRol();
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
        /*Método para habilitar/deshabilitar los campos en el modificar
          * Requiere: -
          * Modifica: La propiedad enable del textBox, botones y comboBox  
          * Retorna: no retorna ningún valor
          */
        protected void habilitarCamposModificar()//si es Administrador es 1, si no es 2
        {

            this.txtCedula.Enabled = false;
            this.txtNombre.Enabled = true;
            this.txtApellido1.Enabled = true;
            this.txtApellido2.Enabled = true;
            this.txtEmail.Enabled = true;
            this.comboPerfil.Enabled = false;
            this.comboRol.Enabled = false;
            this.txtUsuario.Enabled = true;
            this.txtContrasena.Enabled = true;
            this.txtConfirmar.Enabled = true;
            this.txtTelefono.Enabled = true;
            this.lnkNumero.Enabled = true;
            this.lnkQuitar.Enabled = true;
            cambiarEnabled(false, btnInsertar);
            cambiarEnabled(true, btnModificar);
            cambiarEnabled(false, btnEliminar);
            cambiarEnabled(true, btnAceptar);
            cambiarEnabled(true, btnCancelar);
        }

        /*Método para crear la acción de eliminar un funcionario
         * Modifica: Cambia la propiedad enabled de botones y cajas de texto,
         * Limpia cajas de texto y coloca los ejemplos de datos donde es necesario
         * Retorna: no retorna ningún valor
         */
        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            cambiarEnabled(true, this.btnInsertar);
            cambiarEnabled(true, this.btnModificar);
            cambiarEnabled(false, this.btnAceptar);
            cambiarEnabled(false, this.btnCancelar);
            modo = 3;
            controlarCampos(false);


            //lblModalTitle.Text = "AVISO";
            // lblModalBody.Text = "Está seguro que desea eliminar este recurso humano?";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalConfirmar", "$('#modalConfirmar').modal();", true);
            upModal.Update();

        }

        /*Método para crear la acción de insertar un nuevo funcionario
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
            llenarDropDownPerfil();
            llenarDropDownRol();
            UpdatePanelDropDown.Update();
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
            }
        }

        /*Método para la acción del botón cancelar
         * Modifica: Deshabilita los textbox, los botones y limpia los textbox
         * Retorna: no retorna ningún valor
         */
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            //Si es un administrador puede insertar
            if (Session["perfil"].ToString().Equals("Administrador"))
            {
                cambiarEnabled(true, this.btnInsertar);

            }
            //Si es un miembro no puede insertar
            else
            {
                cambiarEnabled(false, this.btnInsertar);
            }

            controlarCampos(false);
            vaciarCampos();
            cambiarEnabled(false, this.btnModificar);
            cambiarEnabled(false, this.btnEliminar);
            cambiarEnabled(true, this.btnAceptar);
            cambiarEnabled(true, this.btnCancelar);

            llenarDropDownPerfil();
            llenarDropDownRol();

        }

        /*Método para la acción del botón agregar telefonos al listbox
         * Modifica: agrega al listbox el telefono escrito en el textbox de telefono
         * Retorna: no retorna ningún valor
         */
        protected void btnAgregarTelefono(object sender, EventArgs e)
        {
            txtContrasena.Attributes["Value"] = txtContrasena.Text;
            txtConfirmar.Attributes["Value"] = txtConfirmar.Text;

            if (txtTelefono.Text != "")
            {
                listTelefonos.Items.Add(txtTelefono.Text);
                txtTelefono.Text = "";
            }

            if (modo == 1)
            {
                habilitarCamposInsertar();
            }
            else if (modo == 2)
            {
                habilitarCamposModificar();
            }
            /*else if (modo == 3)
            {
                habilitarCamposEliminar();
            }*/

            Update_Telefonos.Update();
        }

        private void habilitarCamposCancelarEliminar()
        {
            cambiarEnabled(true, this.btnModificar);
            cambiarEnabled(true, this.btnEliminar);
            cambiarEnabled(true, this.btnAceptar);
            cambiarEnabled(true, this.btnCancelar);
            cambiarEnabled(true, this.btnInsertar);
        }

        /*Método para la acción de eliminar telefonos del listbox
         * Modifica: Elimina el telefono seleccionado del listbox
         * Retorna: no retorna ningún valor
         */
        protected void btnEliminarTelefono(object sender, EventArgs e)
        {
            txtContrasena.Attributes["Value"] = txtContrasena.Text;
            txtConfirmar.Attributes["Value"] = txtConfirmar.Text;
            if (modo == 1 || modo == 2)
            {
                if (listTelefonos.SelectedIndex != -1)
                {
                    listTelefonos.Items.RemoveAt(listTelefonos.SelectedIndex);
                }
            }

            Update_Telefonos.Update();
        }

        /*Método para limpiar los textbox
         * Requiere: No requiere parámetros
         * Modifica: Establece la propiedad text de los textbox en ""
         * Retorna: no retorna ningún valor
         */
        protected void vaciarCampos()
        {
            this.txtCedula.Text = "";
            this.txtNombre.Text = "";
            this.txtApellido1.Text = "";
            this.txtApellido2.Text = "";
            this.txtEmail.Text = "";
            this.txtUsuario.Text = "";
            txtContrasena.Attributes["Value"] = "";
            txtConfirmar.Attributes["Value"] = "";
            this.txtContrasena.Text = "";
            this.txtConfirmar.Text = "";
            this.txtTelefono.Text = "";
            this.listTelefonos.Items.Clear();
        }

        protected void btnCancelar_Eliminar(object sender, EventArgs e)
        {
            habilitarCamposCancelarEliminar();
        }
        /*Método para la acción de aceptar cuando esta en modo de inserción
         * Requiere: No requiere ningún parámetro
         * Modifica: Crea un objeto con los datos obtenidos en la interfaz mediante textbox y 
         * valida que todos los datos se encuentren para la inserción
         * Retorna: No retorna ningún valor
         */
        protected void btnAceptar_Insertar()
        {

            //La inserción 1 es insertar un funcionario
            int tipoInsercion = 1;

            //si hay cajas de texto sin datos avisa al usuario que debe completar los datos
            if (faltanDatos())
            {
                lblModalTitle.Text = "AVISO";
                lblModalBody.Text = "Para insertar un nuevo funcionario debe completar los datos obligatorios.";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                upModal.Update();

                habilitarCamposInsertar();
            }
            //Los datos obligatorios se encuentran completos 
            else
            {
                if (faltanDatosCuenta())
                {
                    lblModalTitle.Text = "AVISO";
                    lblModalBody.Text = "Si desea insertar datos de la cuenta del funcionario debe insertar todos los datos, ya que únicamente si se encuentran completos podrá ingresar al sistema.";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                    upModal.Update();

                    habilitarCamposInsertar();
                }
                else
                {

                    if (contrasenasIguales())
                    {
                        //Se crea el objeto para encapsular los datos de la interfaz para insertar funcionario
                        Object[] datosNuevos = new Object[8];
                        datosNuevos[0] = this.txtCedula.Text;
                        datosNuevos[1] = this.txtNombre.Text;
                        datosNuevos[2] = this.txtApellido1.Text;
                        datosNuevos[3] = this.txtApellido2.Text;
                        datosNuevos[4] = this.txtEmail.Text;
                        datosNuevos[5] = this.txtUsuario.Text;
                        datosNuevos[6] = this.txtContrasena.Text;
                        datosNuevos[7] = false;

                        tipoInsercion = 1;

                        //Si la inserción fue correcta insertará en otras tablas
                        if (controladoraRH.ejecutarAccion(modo, tipoInsercion, datosNuevos, ""))
                        {

                            //Si el perfil del nuevo funcionario es administrador guardará un nuevo administrador
                            if (comboPerfil.Text == "Administrador")
                            {
                                Object[] nuevoAdmin = new Object[1];
                                nuevoAdmin[0] = this.txtCedula.Text;
                                tipoInsercion = 2;                          //inserción de tipo 2 es nuevo administrador

                                //Se insertó un nuevo administrador
                                if (controladoraRH.ejecutarAccion(modo, tipoInsercion, nuevoAdmin, ""))
                                {
                                    guardarTelefonos();
                                    //Se debe llenar el grid con el nuevo
                                    funcionarioInsertado = this.txtCedula.Text;
                                    idRH = this.txtCedula.Text;
                                    llenarGrid(null);
                                    controlarCampos(false);
                                    cambiarEnabled(true, this.btnModificar);
                                    cambiarEnabled(true, this.btnEliminar);
                                    cambiarEnabled(false, this.btnAceptar);
                                    cambiarEnabled(false, this.btnCancelar);
                                    cambiarEnabled(true, this.btnInsertar);
                                    idRH = this.txtCedula.Text;

                                    lblModalTitle.Text = "";
                                    lblModalBody.Text = "Nuevo funcionario creado con éxito.";
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                                    upModal.Update();

                                    funcionarioInsertado = "";
                                }
                                //La inserción de un nuevo administrador en la base de datos falló porque ya estaba en la base
                                else
                                {

                                    lblModalTitle.Text = "ERROR";
                                    lblModalBody.Text = "Esta cédula ya se encuentra registrada en el sistema como administrador.";
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                                    upModal.Update();
                                    habilitarCamposInsertar();
                                }
                            }
                            //El perfil es un miembro de equipo
                            else
                            {
                                Object[] nuevoMiembro = new Object[2];
                                nuevoMiembro[0] = this.txtCedula.Text;
                                nuevoMiembro[1] = this.comboRol.Text;
                                tipoInsercion = 3;                              //inserción de tipo 3 es nuevo miembro

                                //Se insertó un nuevo miembro de equipo
                                if (controladoraRH.ejecutarAccion(modo, tipoInsercion, nuevoMiembro, ""))
                                {
                                    guardarTelefonos();
                                    funcionarioInsertado = this.txtCedula.Text;
                                    idRH = this.txtCedula.Text;
                                    //Se debe llenar el grid con el nuevo
                                    llenarGrid(null);

                                    controlarCampos(false);
                                    cambiarEnabled(true, this.btnModificar);
                                    cambiarEnabled(true, this.btnEliminar);
                                    cambiarEnabled(false, this.btnAceptar);
                                    cambiarEnabled(false, this.btnCancelar);
                                    cambiarEnabled(true, this.btnInsertar);
                                    lblModalTitle.Text = " ";
                                    funcionarioInsertado = "";

                                    lblModalBody.Text = "Nuevo funcionario creado con éxito.";
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                                    upModal.Update();

                                }
                                //La inserción de un nuevo miembro de equipo de pruebas falló porque ya estaba en la base
                                else
                                {
                                    lblModalTitle.Text = "ERROR";
                                    lblModalBody.Text = "Esta cédula ya se encuentra registrada en el sistema como miembro de equipo de pruebas.";
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                                    upModal.Update();
                                    habilitarCamposInsertar();
                                }
                            }
                        }
                        //La inserción del funcionario no se pudo realizar en la base de datos
                        else
                        {
                            lblModalTitle.Text = "ERROR";
                            lblModalBody.Text = "La inserción no fue exitosa.";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                            upModal.Update();

                            habilitarCamposInsertar();
                        }
                    }
                    else //Las contraseñas no coinciden
                    {
                        lblModalTitle.Text = "ERROR";
                        lblModalBody.Text = "Las contraseñas son diferentes.";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                        upModal.Update();
                        habilitarCamposInsertar();
                    }

                }


            }

        }

        /*Método para  controlar la inserción de el o los teléfonos de un funcionario en particular
         * Requiere: no recibe parámetros
         * Modifica: Revisa que efectivamente se esten insertando telefonos en la caja de texto y que no se inserten repetidos
         * Retorna: No retorna ningún valor
         */
        protected void guardarTelefonos()
        {
            int i = 0;

            while (i < listTelefonos.Items.Count && listTelefonos.Items[i].Text.Equals("") == false)
            {
                Object[] telefono = new Object[2];
                telefono[0] = this.txtCedula.Text;
                telefono[1] = this.listTelefonos.Items[i].Text;
                int tipoInsercion = 4;                              //inserción de tipo 4 es agregar telefonos

                //Se insertó un nuevo telefono para el funcionario
                if (controladoraRH.ejecutarAccion(1, tipoInsercion, telefono, ""))
                {
                }
                //La inserción de un nuevo telefono para un funcionario en la base de datos falló porque ya estaba en la base
                else
                {
                    lblModalTitle.Text = "ERROR";
                    lblModalBody.Text = "El teléfono ya se encuentra asociado en el sistema a este funcionario.";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                    upModal.Update();
                    habilitarCamposInsertar();
                }

                i++;
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

        /*Método para obtener si las contraseñas son diferentes
         * Requiere: no recibe parámetros
         * Modifica: Compara ambas cajas de texto
         * Retorna: retorna true si las contraseñas son iguales, false si las contraseñas son distintas
         */
        protected bool contrasenasIguales()
        {
            bool resultado = false;

            if (txtContrasena.Text == txtConfirmar.Text)
            {
                resultado = true;
            }

            return resultado;
        }
        /*Método para saber si hay cajas de texto que no tienen datos en su interior
         * Recibe: No recibe ningún parámetro
         * Modifica:  Verifica si faltan datos en alguna caja de texto
         * Retorna: retorna true si alguna caja no tiene texto, false si todas las cajas tienen texto
         */
        protected bool faltanDatos()
        {

            bool resultado = false;
            //Pregunta por las cajas que son obligatorios
            if (txtCedula.Text == "" || txtNombre.Text == "" || txtApellido1.Text == "" || txtApellido2.Text == "")
            {
                resultado = true;
            }
            else
            {
                resultado = false;
            }
            return resultado;
        }

        /*Método para saber si hay cajas de texto que no tienen datos en su interior
         * Recibe: No recibe ningún parámetro
         * Modifica:  Verifica si faltan datos en alguna caja de texto
         * Retorna: retorna true si alguna caja no tiene texto, false si todas las cajas tienen texto
         */
        protected bool faltanDatosCuenta()
        {

            bool resultado = false;
            //Pregunta por las cajas que son obligatorios
            if (txtUsuario.Text != "" && (txtContrasena.Text == "" || txtConfirmar.Text == "" || (comboRol.Text == "--" && comboPerfil.Text == "Miembro de equipo de pruebas")))
            {
                resultado = true;
            }
            else if (txtContrasena.Text != "" && (txtUsuario.Text == "" || txtConfirmar.Text == "" || (comboRol.Text == "--" && comboPerfil.Text == "Miembro de equipo de pruebas")))
            {
                resultado = true;
            }
            else if (txtConfirmar.Text != "" && (txtUsuario.Text == "" || txtContrasena.Text == "" || (comboRol.Text == "--" && comboPerfil.Text == "Miembro de equipo de pruebas")))
            {
                resultado = true;
            }
            else if (comboRol.Text == "--" && comboPerfil.Text == "Miembro de equipo de pruebas" && (txtUsuario.Text == "" || txtContrasena.Text == "" || txtConfirmar.Text == ""))
            {
                resultado = true;
            }
            else
            {
                resultado = false;
            }
            return resultado;
        }

        /*Método Para realizar el modificar de un funcionario, que altera también sus teléfonos 
         * Recibe: No recibe ningún parámetro
         * Modifica:  Se comunica con la controladora de RH para modificar la BD en los teléfnos y funcionarios
         * Retorna: No retorna
         */
        protected void btnAceptar_Modificar()
        {
            int tipoModificacion = 1;//Funcionario
            if (faltanDatos())//2 indica los datos que pueden faltar en el modificar
            {
                lblModalTitle.Text = "AVISO";
                lblModalBody.Text = "Para modificar un funcionario debe completar todos los datos habilitados.";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                upModal.Update();
            }
            else
            {
                if (faltanDatosCuenta())
                {
                    lblModalTitle.Text = "AVISO";
                    lblModalBody.Text = "Si desea modificar datos de la cuenta del funcionario debe insertar todos los datos, ya que únicamente si se encuentran completos podrá ingresar al sistema.";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                    upModal.Update();
                }
                else
                {
                    if (contrasenasIguales())
                    {
                        //Se crea el objeto para encapsular los datos de la interfaz para modificar funcionario
                        //los encapsula todos, sea administrador o miembro
                        Object[] datosNuevos = new Object[8];
                        datosNuevos[0] = this.txtCedula.Text;
                        datosNuevos[1] = this.txtNombre.Text;
                        datosNuevos[2] = this.txtApellido1.Text;
                        datosNuevos[3] = this.txtApellido2.Text;
                        datosNuevos[4] = this.txtEmail.Text;
                        datosNuevos[5] = this.txtUsuario.Text;
                        datosNuevos[6] = this.txtContrasena.Text;
                        datosNuevos[7] = false;
                        if (controladoraRH.ejecutarAccion(modo, tipoModificacion, datosNuevos, this.txtCedula.Text))
                        {
                            //string mensaje = "<script>window.alert('La modificacion de funcionario fue exitosa.');</script>";
                            //Response.Write(mensaje);
                            lblModalTitle.Text = " ";
                            lblModalBody.Text = "La modificación de funcionario fue exitosa.";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                            upModal.Update();


                            if (Session["perfil"].ToString().Equals("Administrador"))
                            {
                                llenarGrid(null);
                            }
                            else
                            {
                                llenarGrid(Session["cedula"].ToString());
                            }


                            //Elimino los tels
                            tipoModificacion = 2;//LLAMAR AL DE RO
                            if (controladoraRH.ejecutarAccion(3, tipoModificacion, datosNuevos, this.txtCedula.Text))//lo mando con 3 para que elimine los tels
                            {
                                //Inserto los tels
                                guardarTelefonos();
                                controlarCampos(false);
                                //      llenarDropDownPerfil();
                                //     llenarDropDownRol();
                                cambiarEnabled(true, this.btnModificar);
                                //   cambiarEnabled(false, this.btnEliminar);
                                cambiarEnabled(false, this.btnAceptar);
                                cambiarEnabled(false, this.btnCancelar);
                                if (Session["perfil"].Equals("Miembro"))
                                {
                                    cambiarEnabled(false, this.btnInsertar);
                                    cambiarEnabled(false, this.btnEliminar);
                                }
                                else
                                {
                                    cambiarEnabled(true, this.btnInsertar);
                                    cambiarEnabled(true, this.btnEliminar);
                                }

                            }
                        }
                        else
                        {

                            lblModalTitle.Text = " ";
                            lblModalBody.Text = "No se pudo modificar, recuerde: la cédula y el usuario son únicos.";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                            upModal.Update();
                            habilitarCamposInsertar();

                        }
                    }
                    else
                    {
                        lblModalTitle.Text = " ";
                        lblModalBody.Text = "Las contraseñas deben ser iguales.";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                        upModal.Update();
                    }


                }
            }
        }

        /*Método para la acción de aceptar cuando esta en modo de borrado
         * Requiere: No requiere ningún parámetro
         * Modifica:Elimina un recurso humano si es valido llevar acabo la acción
         * Retorna: No retorna ningún valor
         */
        protected void btnAceptar_Eliminar(object sender, EventArgs e)
        {
            modo = 3;
            string cedulaUsuarioActual = Session["cedula"].ToString();

            if (cedulaUsuarioActual != this.txtCedula.Text)
            {
                if (controladoraRH.ejecutarAccion(modo, 2, null, this.txtCedula.Text) == true)
                {
                    if (controladoraRH.ejecutarAccion(modo, 1, null, this.txtCedula.Text) == true)
                    {
                        lblModalTitle.Text = " ";
                        lblModalBody.Text = "La eliminación de funcionario fue exitosa.";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                        upModal.Update();
                    }
                    else
                    {
                        lblModalTitle.Text = "ERROR";
                        lblModalBody.Text = "La eliminación de funcionario no fue exitosa.";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                        upModal.Update();
                    }
                    idRecursosHumanos = -1;  //el recurso està en -1 por que ya fue eliminado y ya no existe
                    vaciarCampos();

                    controlarCampos(false);
                    cambiarEnabled(false, this.btnModificar);
                    cambiarEnabled(false, this.btnEliminar);
                    cambiarEnabled(false, this.btnAceptar);
                    cambiarEnabled(false, this.btnCancelar);
                    llenarDropDownPerfil();
                    llenarDropDownRol();

                    //El unico botón que cambia de acuerdo al perfil es el de insertar y el grid se llena de acuerdo al tipo de usuario utilizando el sistema
                    if (Session["perfil"].ToString().Equals("Administrador"))
                    {
                        cambiarEnabled(true, this.btnInsertar);
                        llenarGrid(null);
                    }
                    else
                    {
                        cambiarEnabled(false, this.btnInsertar);
                        llenarGrid(Session["cedula"].ToString());
                    }
                }
                else {
                    lblModalTitle.Text = "ERROR";
                    lblModalBody.Text = "La eliminación de funcionario no fue exitosa.";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                    upModal.Update();
                }
            }
            else
            {

                lblModalTitle.Text = " ";
                lblModalBody.Text = "No es posible eliminar el funcionario ya que posee una sesión abierta en el sistema.";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                upModal.Update();
            }
        }

        /*Metodo para poner el nombre completo del usuario logueado en ese momento
        *Requiere: nada
        *Modifica: el nombre de la persona logueado en un momento determinado en la ventana de RecursosHumanos
        *Retorna: no retorna ningún valor*/
        protected void ponerNombreDeUsuarioLogueado()
        {
            DataTable datosFilaFuncionario = controladoraRH.consultarRH(Session["cedula"].ToString());
            if (datosFilaFuncionario.Rows.Count == 1)
            {
                string nombreCompletoUsuarioLogueado = datosFilaFuncionario.Rows[0][1].ToString() + " " + datosFilaFuncionario.Rows[0][2].ToString() + " " + datosFilaFuncionario.Rows[0][3].ToString();
                this.lblLogueado.Text = nombreCompletoUsuarioLogueado;
            }
        }


        /*Método para llenar el grid con el registro del recurso humano correspondiente al usuario del sistema en caso de que éste sea un miembro, o 
       con todos los registros de los recursos humanos presentes en el sistema en caso de que el usuario sea el administrador.
       * Requiere: Requiere la cédula del usuario que está utilizando el sistema en caso de que éste sea un miembro, sino se especifica el parámetro
       como nulo para indicar que el usuario del sistema es el administrador.
       * Modifica: el valor de cada uno de los campos en la interfaz correspondientes a la consulta retornada por la clase controladora de manera 
       que le sea visible al usuario del sistema los resultados. 
       * Retorna: no retorna ningún valor
       */
        protected void llenarGrid(string idRH)
        {
            DataTable dt = crearTablaFuncionarios();
            DataTable funcionarios;
            Object[] datos = new Object[3];
            Object[] tuplaInsertada = new Object[3];
            int indiceColumnas = 0;
            int ubicacionFuncionario = 0;


            if (idRH == null) //Significa que el usuario utilizando el sistema es un administrador por lo que se le deben mostrar 
            //todos los recursos humanos del sistema
            {
                funcionarios = controladoraRH.consultarRecursosHumanos(null);
                int numeroFuncionarios = funcionarios.Rows.Count;
                if (funcionarios.Rows.Count > 0)
                {

                    if (funcionarioInsertado != "")
                    {

                        for (int i = 0; i < funcionarios.Rows.Count; ++i)
                        {
                            if (funcionarioInsertado == funcionarios.Rows[i][0].ToString())
                            {
                                ubicacionFuncionario = i;
                                datos[0] = funcionarios.Rows[i][0].ToString();
                                datos[1] = funcionarios.Rows[i][1].ToString() + " " + funcionarios.Rows[i][2].ToString() + " " + funcionarios.Rows[i][3].ToString();


                                if (funcionarios.Rows[i][4].ToString() == "")
                                {
                                    datos[2] = "Administrador";
                                }
                                else
                                {

                                    datos[2] = funcionarios.Rows[i][4].ToString();
                                }

                                dt.Rows.Add(datos);
                            }
                        }
                        foreach (DataRow fila in funcionarios.Rows)
                        {

                            if (indiceColumnas != ubicacionFuncionario)
                            {
                                datos[0] = fila[0].ToString();
                                datos[1] = fila[1].ToString() + " " + fila[2].ToString() + " " + fila[3].ToString();


                                if (fila[4].ToString() == "")
                                {
                                    datos[2] = "Administrador";
                                }
                                else
                                {

                                    datos[2] = fila[4].ToString();
                                }

                                dt.Rows.Add(datos);
                            }

                            ++indiceColumnas;
                        }
                    }

                    else
                    {
                        foreach (DataRow fila in funcionarios.Rows)
                        {
                            datos[0] = fila[0].ToString();
                            datos[1] = fila[1].ToString() + " " + fila[2].ToString() + " " + fila[3].ToString();

                            if (fila[4].ToString() == "")
                            {
                                datos[2] = "Administrador";
                            }
                            else
                            {

                                datos[2] = fila[4].ToString();
                            }

                            dt.Rows.Add(datos);
                        }
                    }
                }
                else
                {
                    datos[0] = "-";
                    datos[1] = "-";
                    datos[2] = "-";
                    dt.Rows.Add(datos);
                }
            }
            else
            {
                funcionarios = controladoraRH.consultarRecursosHumanos(idRH);
                if (funcionarios.Rows.Count == 1)
                {
                    foreach (DataRow fila in funcionarios.Rows)
                    {
                        datos[0] = fila[0].ToString();
                        datos[1] = fila[1].ToString() + " " + fila[2].ToString() + " " + fila[3].ToString();

                        if (fila[4].ToString() == null)
                        {
                            datos[2] = "Administrador";
                        }
                        else
                        {
                            datos[2] = "Miembro";
                        }
                        dt.Rows.Add(datos);
                    }
                }
                else
                {
                    datos[0] = "-";
                    datos[1] = "-";
                    datos[2] = "-";
                    dt.Rows.Add(datos);
                }
            }

            this.gridRH.DataSource = dt;
            this.gridRH.DataBind();


        }

        /*Método para crear el DataTable donde se mostrará el o los registros de los recursos humanos del sistema según corresponda.
       * Requiere: No requiere ningún parámetro.
       * Modifica: el nombre de cada columna donde se le especifica el nombre que cada una de ellas tendrá. 
       * Retorna: el DataTable creado. 
       */
        protected DataTable crearTablaFuncionarios()
        {
            DataTable dt = new DataTable();
            DataColumn columna;

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Cédula";
            dt.Columns.Add(columna);

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Nombre Funcionario";
            dt.Columns.Add(columna);

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Rol";
            dt.Columns.Add(columna);

            return dt;
        }


        /*Método para llenar los capos de la interfaz con los resultados de la consulta.
        * Requiere: La cédula del funcionario que se desea consultar.
        * Modifica: Los campos de la interfaz correspondientes a los datos recibidos mediante la clase controladora.
        * Retorna: No retorna ningún valor. 
        */
        public void llenarDatos(string idRH)
        {

            DataTable datosFilaFuncionario = controladoraRH.consultarRH(idRH);
            DataTable datosFilaTelefono = controladoraRH.consultarTelefonosRH(idRH);
            ListItem tipoPerfil;

            if (datosFilaFuncionario.Rows.Count == 1)
            {
                this.txtCedula.Text = datosFilaFuncionario.Rows[0][0].ToString();
                cedulaGuardadaBD = datosFilaFuncionario.Rows[0][0].ToString();
                this.txtNombre.Text = datosFilaFuncionario.Rows[0][1].ToString();
                this.txtApellido1.Text = datosFilaFuncionario.Rows[0][2].ToString();
                this.txtApellido2.Text = datosFilaFuncionario.Rows[0][3].ToString();
                this.txtUsuario.Text = datosFilaFuncionario.Rows[0][4].ToString();
                this.txtEmail.Text = datosFilaFuncionario.Rows[0][5].ToString();
                this.txtContrasena.Text = datosFilaFuncionario.Rows[0][6].ToString();
                this.txtConfirmar.Text = datosFilaFuncionario.Rows[0][6].ToString();

                if (this.comboRol.Items.FindByText(datosFilaFuncionario.Rows[0][7].ToString()) != null)
                {

                    ListItem rol = this.comboRol.Items.FindByText(datosFilaFuncionario.Rows[0][7].ToString());
                    this.comboRol.SelectedValue = rol.Value;
                    tipoPerfil = comboPerfil.Items.FindByText("Miembro de equipo de pruebas");
                    perfilGuardadoBD = "Miembro de equipo de pruebas";
                    this.comboPerfil.SelectedValue = tipoPerfil.Value;
                }

                else
                {
                    tipoPerfil = comboPerfil.Items.FindByText("Administrador");
                    perfilGuardadoBD = "Administrador";
                    this.comboPerfil.SelectedValue = tipoPerfil.Value;
                    this.comboRol.Items.Clear();
                }
            }


            listTelefonos.Items.Clear();
            if (datosFilaTelefono.Rows.Count >= 1)
            {
                listTelefonos.Items.Clear();
                for (int i = 0; i < datosFilaTelefono.Rows.Count; ++i)
                {
                    listTelefonos.Items.Add(datosFilaTelefono.Rows[i][0].ToString());

                }
            }

        }

        /*Método para cerrar la sesión abierta de un usuario y dirigirse a la página de inicio.
         * Requiere: recibe el evento cuando se presiona el botón para cerrar sesión.
         * Modifica: Modifica el valor booleano del estado de la sesión
         * Retorna: No retorna ningún valor
         */
        protected void cerrarSesion(object sender, EventArgs e)
        {

            string ced = (string)Session["cedula"];
            Boolean a = controladoraRH.modificarEstadoCerrar(ced);
            Response.Redirect("~/Login.aspx");
        }
    }
}