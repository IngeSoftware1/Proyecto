using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProyectoInge.App_Code.Capa_de_Control;

namespace ProyectoInge
{
    public partial class RecursosHumanos : System.Web.UI.Page
    {
        ControladoraRecursos controladoraRH = new ControladoraRecursos();

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
            this.comboPerfil.Enabled = condicion;
            this.comboRol.Enabled = condicion;
            this.txtUsuario.Enabled = condicion;
            this.txtContrasena.Enabled = condicion;
            this.txtConfirmar.Enabled = condicion;
            this.txtTelefono.Enabled = condicion;
            this.btnNumero.Enabled = condicion;
            this.btnQuitar.Enabled = condicion;
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
                this.btnQuitar.Enabled = true;
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
            cambiarEnabled(false, this.btnModificar);
            cambiarEnabled(false, this.btnEliminar);
            cambiarEnabled(false, this.btnInsertar);
        }

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

        /*Método para la acción del botón cancelar
         * Modifica: Deshabilita los textbox, los botones y limpia los textbox
         * Retorna: no retorna ningún valor
         */
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            controlarCampos(false);
            vaciarCampos();
            cambiarEnabled(false, this.btnModificar);
            cambiarEnabled(false, this.btnEliminar);
            cambiarEnabled(false, this.btnAceptar);
            cambiarEnabled(false, this.btnCancelar);
            cambiarEnabled(true, this.btnInsertar);
        }

        /*Método para la acción del botón agregar telefonos al listbox
         * Modifica: agrega al listbox el telefono escrito en el textbox de telefono
         * Retorna: no retorna ningún valor
         */
        protected void btnAgregarTelefono(object sender, EventArgs e)
        {
            if (modo == 1)
            {
                if (txtTelefono.Text != "")
                {
                    listTelefonos.Items.Add(txtTelefono.Text);
                    txtTelefono.Text = "";
                }
                habilitarCamposInsertar();
            }
        }

        /*Método para la acción de eliminar telefonos del listbox
         * Modifica: Elimina el telefono seleccionado del listbox
         * Retorna: no retorna ningún valor
         */
        protected void btnEliminarTelefono(object sender, EventArgs e)
        {
            if (modo == 1)
            {
                if (listTelefonos.SelectedIndex != -1)
                {
                    listTelefonos.Items.RemoveAt(listTelefonos.SelectedIndex);
                }
                habilitarCamposInsertar();
            }
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
            this.txtUsuario.Text = "";
            this.txtContrasena.Text = "";
            this.txtConfirmar.Text = "";
            this.txtTelefono.Text = "";
            this.listTelefonos.Items.Clear();
        }

     
        /*Método para la acción de aceptar cuando esta en modo de inserción
         * Requiere: No requiere ningún parámetro
         * Modifica: Crea un objeto con los datos obtenidos en la interfaz mediante textbox y 
         * valida que todos los datos se encuentren para la inserción
         * Retorna: No retorna ningún valor
         */
        protected void btnAceptar_Insertar() {

            //La inserción 1 es insertar un funcionario
            int tipoInsercion = 1;

            if (contrasenasIguales())
            {
                //si hay cajas de texto sin datos avisa al usuario que debe completar los datos
                if (faltanDatos())
                {
                    string mensaje = "<script>window.alert('Para insertar un nuevo funcionario debe completar todos los datos.');</script>";
                    Response.Write(mensaje);
                    habilitarCamposInsertar();
                }
                //Los datos obligatorios se encuentran completos 
                else
                {
                    //Se crea el objeto para encapsular los datos de la interfaz para insertar funcionario
                    Object[] datosNuevos = new Object[7];
                    datosNuevos[0] = this.txtCedula.Text;
                    datosNuevos[1] = this.txtNombre.Text;
                    datosNuevos[2] = this.txtApellido1.Text;
                    datosNuevos[3] = this.txtApellido2.Text;
                    datosNuevos[4] = this.txtUsuario.Text;
                    datosNuevos[5] = this.txtContrasena.Text;
                    datosNuevos[6] = false;

                    //Si la inserción fue correcta insertará en otras tablas
                    if (controladoraRH.ejecutarAccion(modo, tipoInsercion, datosNuevos))
                    {
                        string mensaje;

                        //Si el perfil del nuevo funcionario es administrador guardará un nuevo administrador
                        if (comboPerfil.Text == "Administrador")
                        {
                            Object[] nuevoAdmin = new Object[1];
                            nuevoAdmin[0] = this.txtCedula.Text;
                            tipoInsercion = 2;                          //inserción de tipo 2 es nuevo administrador

                            //Se insertó un nuevo administrador
                            if (controladoraRH.ejecutarAccion(modo, tipoInsercion, nuevoAdmin))
                            {
                                //Se debe llenar el grid con el nuevo
                                controlarCampos(false);
                                cambiarEnabled(false, this.btnModificar);
                                cambiarEnabled(false, this.btnEliminar);
                                cambiarEnabled(false, this.btnAceptar);
                                cambiarEnabled(false, this.btnCancelar);
                                cambiarEnabled(true, this.btnInsertar);
                                mensaje = "<script>window.alert('Nuevo funcionario creado con éxito.');</script>";
                                Response.Write(mensaje);
                                vaciarCampos();
                            }
                            //La inserción de un nuevo administrador en la base de datos falló porque ya estaba en la base
                            else
                            {
                                mensaje = "<script>window.alert('Esta cédula ya se encuentra registrada en el sistema como administrador.');</script>";
                                Response.Write(mensaje);
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
                            if (controladoraRH.ejecutarAccion(modo, tipoInsercion, nuevoMiembro))
                            {
                                //Se debe llenar el grid con el nuevo
                                controlarCampos(false);
                                cambiarEnabled(false, this.btnModificar);
                                cambiarEnabled(false, this.btnEliminar);
                                cambiarEnabled(false, this.btnAceptar);
                                cambiarEnabled(false, this.btnCancelar);
                                cambiarEnabled(true, this.btnInsertar);
                                mensaje = "<script>window.alert('Nuevo funcionario creado con éxito.');</script>";
                                Response.Write(mensaje);
                                vaciarCampos();
                            }
                            //La inserción de un nuevo miembro de equipo en la base de datos falló porque ya estaba en la base
                            else
                            {
                                mensaje = "<script>window.alert('Esta cédula ya se encuentra registrada en el sistema como miembro de equipo de pruebas.');</script>";
                                Response.Write(mensaje);
                                habilitarCamposInsertar();
                            }
                        }
                    }
                    //La inserción del funcionario no se pudo realizar en la base de datos
                    else
                    {
                        string mensaje = "<script>window.alert('La inserción no fue exitosa.');</script>";
                        Response.Write(mensaje);
                        habilitarCamposInsertar();
                    }

                }
            }
            //Las contraseñas no coinciden
            else
            {
                string mensaje = "<script>window.alert('Las contraseñas son distintas.');</script>";
                Response.Write(mensaje);
                habilitarCamposInsertar();
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
            
            cambiarEnabled(true, this.btnQuitar);
            cambiarEnabled(true, this.btnNumero);
            
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
        protected bool faltanDatos(){
            
            bool resultado = false;

            //Pregunta por todas las cajas
            if (txtCedula.Text == "" || txtNombre.Text == "" || txtApellido1.Text == "" || txtApellido2.Text == "" || txtUsuario.Text == "" || txtConfirmar.Text == "" || txtContrasena.Text == "")
            {
                resultado = true;
            }
            else
            {
                resultado = false;
            }

            return resultado;
        }

        protected void btnAceptar_Modificar() {

            //Valida datos, campos
            //llama a modificar en la BD

        }
        protected void btnAceptar_Eliminar() { }

    }
}