using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProyectoInge.App_Code.Capa_de_Control;
using System.Data;

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

        protected void Page_Load(object sender, EventArgs e)
        {
        //    perfilGuardadoBD = Session["perfil"].ToString();
         //   cedulaGuardadaBD = Session["cedula"].ToString();

            controlarCampos(false);
            cambiarEnabled(false, this.btnModificar);
            cambiarEnabled(false, this.btnEliminar);
            cambiarEnabled(false, this.btnAceptar);
            cambiarEnabled(false, this.btnCancelar);
            cambiarEnabled(true, this.btnInsertar);
            llenarDropDownPerfil();
            llenarDropDownRol();

       /*     if (perfilGuardadoBD == "Administrador" ) //El usuario en el sistema es el administrador
            {
                llenarGrid(null);
            }
            else if (perfilGuardadoBD == "Miembro") //El usuario en el sistema es un miembro
            {  
                llenarGrid(cedulaGuardadaBD);
            } */

            if (perfil == 1 ) //El usuario en el sistema es el administrador
            {
                llenarGrid(null);
            }
            else if (perfil == 2) //El usuario en el sistema es un miembro
            {  
                llenarGrid(null);
            } 
        }

        protected void llenarDropDownPerfil()
        {
            this.comboPerfil.Items.Clear();
            //  Object[] datos = new Object[3];
            Object[] datos = new Object[2];
            /*    datos[0] = "Seleccione";
                datos[1] = "Administrador";
                datos[2] = "Miembro de equipo de pruebas"; */
            datos[0] = "Administrador";
            datos[1] = "Miembro de equipo de pruebas";
            this.comboPerfil.DataSource = datos;
            this.comboPerfil.DataBind();
        }

        protected void llenarDropDownRol()
        {
            this.comboRol.Items.Clear();
            //  Object[] datos = new Object[4];
            Object[] datos = new Object[3];
            /*    datos[0] = "Seleccione";
                datos[1] = "Líder de pruebas";
                datos[2] = "Tester";
                datos[3] = "Usuario"; */
            datos[0] = "Líder de pruebas";
            datos[1] = "Tester";
            datos[2] = "Usuario";
            this.comboRol.DataSource = datos;
            this.comboRol.DataBind();
        }

        /*Método para cargar ejemplos de datos en las cajas de cedula y telefono
         * Modifica: la propiedad text de las dos cajas
         * retorna: no retorna ningún valor
         */
        protected void EjemplificarCampos()
        {
            txtCedula.Text = "145680958";
            txtTelefono.Text = "88888888";
            
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
        protected void gridVentas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "seleccionarRH")
            {

                LinkButton lnkConsulta = (LinkButton)e.CommandSource;
                idRH = lnkConsulta.CommandArgument;

                llenarDatos(idRH);
                cambiarEnabled(true, this.btnModificar);
                cambiarEnabled(true, this.btnEliminar);
                cambiarEnabled(true, this.btnCancelar);
                cambiarEnabled(false, this.btnInsertar);
            }

        }

        protected void gridVentas_PageIndexChanged(object sender, EventArgs e)
        {

        }

        /*Método para habilitar/deshabilitar todos los campos y los botones que permite el modificar, escucha al boton modificar
         * Requiere: object sender, EventArgs e
         * Modifica: Cambia la propiedad Enabled de las cajas y botones
         * Retorna: no retorna ningún valor
         */
        protected void btnModificar_Click(object sender, EventArgs e)
        {
            //PARA PRUEBAS PERFIL = 2;
            cambiarEnabled(false, this.btnInsertar);
            cambiarEnabled(false, this.btnEliminar);
            //llenar los txtbox con la table
            cambiarEnabled(true, this.btnAceptar);
            cambiarEnabled(true, this.btnCancelar);

            modo = 2;

            habilitarCamposModificar();
            

        }

        //Me dice si hay un RH seleccionado del Grid, y puesto en los txtbox
        private bool rhConsultado()
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
            if (perfil == 1)//Si es un administrador puede modificar todos
            {
                controlarCampos(true);
            }
            else if (perfil == 2)//Si es un miembro, entonces solo puede modificar los campos habilitador
            {
                this.txtCedula.Enabled = true;
                this.txtNombre.Enabled = true;
                this.txtApellido1.Enabled = true;
                this.txtApellido2.Enabled = true;
                this.txtEmail.Enabled = true;
                this.comboPerfil.Enabled = false;
                this.comboRol.Enabled = false;
                this.txtUsuario.Enabled = false;
                this.txtContrasena.Enabled = false;
                this.txtConfirmar.Enabled = false;
                this.txtTelefono.Enabled = true;
                this.lnkNumero.Enabled = true;
                this.lnkQuitar.Enabled = true;
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            cambiarEnabled(false, this.btnInsertar);
            cambiarEnabled(false, this.btnModificar);
            cambiarEnabled(true, this.btnAceptar);
            cambiarEnabled(true, this.btnCancelar);
            modo = 3;
            controlarCampos(false);
        }

        /*Método para crear la acción de insertar un nuevo funcionario
         * Modifica: Cambia la propiedad enabled de botones y cajas de texto,
         * Limpia cajas de texto y coloca los ejemplos de datos donde es necesario
         * Retorna: no retorna ningún valor
         */
        protected void btnInsertar_Click(object sender, EventArgs e)
        {
            vaciarCampos();
            EjemplificarCampos();
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
            txtContrasena.Attributes["Value"] = txtContrasena.Text;
            txtConfirmar.Attributes["Value"] = txtConfirmar.Text;
            if (modo == 1)
            {
                if (listTelefonos.SelectedIndex != -1)
                {
                    listTelefonos.Items.RemoveAt(listTelefonos.SelectedIndex);
                }
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
            this.txtEmail.Text = "";
            this.txtUsuario.Text = "";
            txtContrasena.Attributes["Value"] = "";
            txtConfirmar.Attributes["Value"] = "";
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
                if (faltanDatos(1))
                {
                    string mensaje = "<script>window.alert('Para insertar un nuevo funcionario debe completar todos los datos.');</script>";
                    Response.Write(mensaje);
                    habilitarCamposInsertar();
                }
                //Los datos obligatorios se encuentran completos 
                else
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

                    //Si la inserción fue correcta insertará en otras tablas
                    if (controladoraRH.ejecutarAccion(modo, tipoInsercion, datosNuevos,""))
                    {
                        string mensaje;

                        //Si el perfil del nuevo funcionario es administrador guardará un nuevo administrador
                        if (comboPerfil.Text == "Administrador")
                        {
                            Object[] nuevoAdmin = new Object[1];
                            nuevoAdmin[0] = this.txtCedula.Text;
                            tipoInsercion = 2;                          //inserción de tipo 2 es nuevo administrador

                            //Se insertó un nuevo administrador
                            if (controladoraRH.ejecutarAccion(modo, tipoInsercion, nuevoAdmin,""))
                            {
                                guardarTelefonos();
                                //Se debe llenar el grid con el nuevo
                                llenarGrid(null);
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
                            if (controladoraRH.ejecutarAccion(modo, tipoInsercion, nuevoMiembro,""))
                            {
                                guardarTelefonos();
                                //Se debe llenar el grid con el nuevo
                                llenarGrid(null);
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

        protected void guardarTelefonos()
        {
            int i = 0;

            while (i < listTelefonos.Items.Count && listTelefonos.Items[i].Text.Equals("") == false)
            {
                string mensaje;
                Object[] telefono = new Object[2];
                telefono[0] = this.txtCedula.Text;
                telefono[1] = this.listTelefonos.Items[i].Text;
                int tipoInsercion = 4;                              //inserción de tipo 4 es agregar telefonos

                //Se insertó un nuevo telefono para el funcionario
                if (controladoraRH.ejecutarAccion(1, tipoInsercion, telefono,""))
                {
                }
                //La inserción de un nuevo telefono para un funcionario en la base de datos falló porque ya estaba en la base
                else
                {
                    mensaje = "<script>window.alert('El teléfono ya se encuentra asociado en el sistema a este funcionario.');</script>";
                    Response.Write(mensaje);
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
        protected bool faltanDatos(int modo2){
            
            bool resultado = false;
            if (modo2 == 1)
            {
                //Pregunta por todas las cajas
                if (txtCedula.Text == "" || txtNombre.Text == "" || txtApellido1.Text == "" || txtApellido2.Text == "" || txtUsuario.Text == "" || txtConfirmar.Text == "" || txtContrasena.Text == "")
                {
                    resultado = true;
                }
                else
                {
                    resultado = false;
                }
            }
            else if (modo2 == 2) 
            {
                //Pregunta por algunas cajas
                if (txtCedula.Text == "" || txtNombre.Text == "" || txtApellido1.Text == "" || txtApellido2.Text == "" || txtUsuario.Text == "" || txtEmail.Text == "" )
                {
                    resultado = true;
                }
                else
                {
                    resultado = false;
                }

            }
            return resultado;
        }
        /*Método Para la acción de aceptar cuando está en modo de modificación 
         * Recibe: No recibe ningún parámetro
         * Modifica:  Verifica si faltan datos en alguna caja de texto
         * Retorna: retorna true si alguna caja no tiene texto, false si todas las cajas tienen texto
         */
        protected void btnAceptar_Modificar()
        {
            //MODIFICAR POR PARTE DE UN MIEMBRO
            int tipoModificacion = 1;//Funcionario
            if (faltanDatos(2))
            {
                string mensaje = "<script>window.alert('Para modificar un funcionario debe completar todos los datos habilitados.');</script>";
                Response.Write(mensaje);
                habilitarCamposInsertar();
            }
            else
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
                if (controladoraRH.ejecutarAccion(modo, tipoModificacion, datosNuevos,cedulaGuardadaBD))
                {
                    string mensaje = "<script>window.alert('La modificacion de funcionario fue exitosa.');</script>";
                    Response.Write(mensaje);
                    //Elimino los tels
                    tipoModificacion = 4;//LLAMAR AL DE RO
                    if (controladoraRH.ejecutarAccion(3, tipoModificacion, datosNuevos, cedulaGuardadaBD))//lo mando con 3 para que elimine los tels
                    {
                        //Inserto los tels
                        guardarTelefonos();
                        
                    }
                        if (perfil == 1)//Administrador
                        {

                            if (perfilGuardadoBD != this.comboPerfil.Text)
                            {
                                if (perfilGuardadoBD == "Administrador")
                                {
                                    //Inserto
                                    tipoModificacion = 3;//Miembro
                                    Object[] datosRol2 = new Object[2];
                                    datosRol2[0] = this.txtCedula.Text;
                                    datosRol2[1] = this.comboRol.Text;
                                    if (controladoraRH.ejecutarAccion(1, tipoModificacion, datosRol2, cedulaGuardadaBD))//lo mando con 1 para que inserte
                                    {
                                        string mensaje2 = "<script>window.alert('Se inserto un nuevo miembro ');</script>";
                                        Response.Write(mensaje2);
                                    }
                                    else
                                    {
                                        string mensaje1 = "<script>window.alert('No se pudo insertar el miembro');</script>";
                                        Response.Write(mensaje1);
                                        habilitarCamposInsertar();
                                    }
                                    //Elimino LLAMAR AL DE RO
                                    tipoModificacion = 3;//Administrador
                                    Object[] datosAdmin = new Object[1];
                                    datosAdmin[0] = cedulaGuardadaBD;
                                    if (controladoraRH.ejecutarAccion(3, tipoModificacion, datosRol2, cedulaGuardadaBD))//lo mando con 3 para que elimine
                                    {
                                        string mensaje2 = "<script>window.alert('Se elimino un administrador.');</script>";
                                        Response.Write(mensaje2);

                                    }
                                    else
                                    {
                                        string mensaje1 = "<script>window.alert('No se pudo eliminar el administrador');</script>";
                                        Response.Write(mensaje1);
                                        habilitarCamposInsertar();
                                    }

                                }
                                else if (perfilGuardadoBD == "Miembro de equipo de pruebas")
                                {
                                    //Inserto
                                    tipoModificacion = 2;//Administrador
                                    Object[] datosAdmin = new Object[1];
                                    datosAdmin[0] = this.txtCedula.Text;
                                    if (controladoraRH.ejecutarAccion(1, tipoModificacion, datosAdmin, cedulaGuardadaBD))//lo mando con 1 para que inserte
                                    {
                                        string mensaje2 = "<script>window.alert('Se inserto un administrador.');</script>";
                                        Response.Write(mensaje2);

                                    }
                                    else
                                    {
                                        string mensaje1 = "<script>window.alert('No se pudo insertar el administrador');</script>";
                                        Response.Write(mensaje1);
                                        habilitarCamposInsertar();
                                    }
                                    //Elimino
                                    tipoModificacion = 2;//LLAMAR AL DE RO
                                    Object[] datosMiembro = new Object[2];
                                    datosMiembro[0] = cedulaGuardadaBD;
                                    datosMiembro[1] = this.comboRol.Text;
                                    if (controladoraRH.ejecutarAccion(3, tipoModificacion, datosMiembro, cedulaGuardadaBD))//lo mando con 3 para que elimine
                                    {
                                        string mensaje2 = "<script>window.alert('Se elimino un miembro');</script>";
                                        Response.Write(mensaje2);

                                    }
                                    else
                                    {
                                        string mensaje1 = "<script>window.alert('No se pudo eliminar el miembro');</script>";
                                        Response.Write(mensaje1);
                                        habilitarCamposInsertar();
                                    }
                                }
                            }
                            else if (perfilGuardadoBD == this.comboPerfil.Text)
                            {
                                if (perfilGuardadoBD == "Administrador")
                                {
                                    tipoModificacion = 3;//Administrador
                                    Object[] datosAdmin = new Object[1];
                                    datosAdmin[0] = this.txtCedula.Text;
                                    if (controladoraRH.ejecutarAccion(modo, tipoModificacion, datosAdmin, cedulaGuardadaBD))
                                    {
                                        string mensaje2 = "<script>window.alert('La modif de admin se dio.');</script>";
                                        Response.Write(mensaje2);

                                    }
                                    else
                                    {
                                        string mensaje1 = "<script>window.alert('La modif de admin no se dio');</script>";
                                        Response.Write(mensaje1);
                                        habilitarCamposInsertar();
                                    }
                                }
                                else if (perfilGuardadoBD == "Miembro de equipo de pruebas")
                                {
                                    tipoModificacion = 2;//Miembro
                                    Object[] datosMiembro = new Object[2];
                                    datosMiembro[0] = this.txtCedula.Text;
                                    datosMiembro[1] = this.comboRol.Text;
                                    if (controladoraRH.ejecutarAccion(modo, tipoModificacion, datosMiembro, cedulaGuardadaBD))
                                    {
                                        string mensaje2 = "<script>window.alert('La modif de miembro se dio.');</script>";
                                        Response.Write(mensaje2);

                                    }
                                    else
                                    {
                                        string mensaje1 = "<script>window.alert('La modif de miembro no se dio');</script>";
                                        Response.Write(mensaje1);
                                        habilitarCamposInsertar();
                                    }
                                }
                            }
                        }
                    //
                }
                else
                {
                    string mensaje = "<script>window.alert('La modificacion no fue exitosa,recuerde la cédula es única.');</script>";
                    Response.Write(mensaje);
                    habilitarCamposInsertar();
                    
                }
                //MODIFICAR POR PARTE DE UN ADMINISTRADOR
            }
        }
        protected void btnAceptar_Eliminar()
        {
            if (rhConsultado())
            {
                controladoraRH.ejecutarAccion(modo, 1, null, idRH);
            }
            idRecursosHumanos = -1;  //el recurso està en -1 por que ya fue eliminado y ya no existe
            llenarGrid(idRH);
            vaciarCampos();
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
            Object[] datos = new Object[4];

            if (idRH == null) //Significa que el usuario utilizando el sistema es un administrador por lo que se le deben mostrar 
            //todos los recursos humanos del sistema
            {
                funcionarios = controladoraRH.consultarRecursosHumanos(null);
                if (funcionarios.Rows.Count > 0)
                {
                    foreach (DataRow fila in funcionarios.Rows)
                    {
                        datos[0] = fila[0].ToString();
                        datos[1] = fila[1].ToString();
                        datos[2] = fila[2].ToString();
                        datos[3] = fila[3].ToString();
                        dt.Rows.Add(datos);
                    }
                }
                else
                {
                    datos[0] = "-";
                    datos[1] = "-";
                    datos[2] = "-";
                    datos[3] = "-";
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
                        datos[1] = fila[1].ToString();
                        datos[2] = fila[2].ToString();
                        datos[3] = fila[3].ToString();
                        dt.Rows.Add(datos);
                    }
                }
                else
                {
                    datos[0] = "-";
                    datos[1] = "-";
                    datos[2] = "-";
                    datos[3] = "-";
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
            columna.ColumnName = "Nombre";
            dt.Columns.Add(columna);

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Primer Apellido";
            dt.Columns.Add(columna);

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Segundo Apellido";
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


                if (this.comboRol.Items.FindByText(datosFilaFuncionario.Rows[0][6].ToString()) != null)
                {

                    ListItem rol = this.comboRol.Items.FindByText(datosFilaFuncionario.Rows[0][6].ToString());
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

        protected void cerrarSesion(object sender, EventArgs e)
        {

            string ced = (string)Session["cedula"];
            Boolean a = controladoraRH.modificarEstadoCerrar(ced);
            Response.Redirect("~/Login.aspx");

        }

    }
}