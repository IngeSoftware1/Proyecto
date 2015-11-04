using System;
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
    public partial class CasoDePrueba : System.Web.UI.Page
    {

        ControladoraCasosPrueba controladoraCasoPruebas = new ControladoraCasosPrueba();
        ControladoraRecursos controladoraRH = new ControladoraRecursos();
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
                ponerNombreDeUsuarioLogueado();
                controlarCampos(false);
                cambiarEnabled(false, this.btnModificar);
                cambiarEnabled(false, this.btnEliminar);
                cambiarEnabled(false, this.btnAceptar);
                cambiarEnabled(false, this.btnCancelar);
                llenarDatosDiseno(); //NO BORRAR, SE UTILIZA CUANDO YA TENGA LO DE DISEÑO LISTO
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

        /*Método para habilitar/deshabilitar todos los campos y los botones + y -
         * Requiere: un booleano para saber si quiere habilitar o deshabilitar los botones y cajas de texto
         * Modifica: Cambia la propiedad Enabled de las cajas y botones
         * Retorna: no retorna ningún valor
         */
        protected void controlarCampos(Boolean condicion)
        {
            this.txtProposito.Enabled = condicion;
            this.txtIdentificador.Enabled = condicion;
            this.txtEntradaDatos.Enabled = condicion;
            this.txtFlujoCentral.Enabled = condicion;
            this.txtResultadoEsperado.Enabled = condicion;
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
            //llenarDropDownProyecto();
            //llenarDropDownProyecto();
            // UpdatePanelDropDown.Update();
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

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            controlarCampos(false);
            vaciarCampos();
            cambiarEnabled(false, this.btnModificar);
            cambiarEnabled(false, this.btnEliminar);
            cambiarEnabled(true, this.btnAceptar);
            cambiarEnabled(true, this.btnCancelar);
            cambiarEnabled(true, this.btnInsertar);
            ponerNombreDeUsuarioLogueado();
            llenarDatosDiseno(); 

        }

        /*Método para la acción de aceptar cuando esta en modo de inserción
         * Requiere: No requiere ningún parámetro
         * Modifica: Crea un objeto con los datos obtenidos en la interfaz mediante textbox y 
         * valida que todos los datos se encuentren para la inserción
         * Retorna: No retorna ningún valor
         */
        private void btnAceptar_Insertar()
        {
            modo = 1;
            int idDiseño = Convert.ToInt32(Session["idDiseñoS"]);


            //si faltan datos no deja insertar
            if (faltanDatos())
            {
                lblModalTitle.Text = "AVISO";
                lblModalBody.Text = "Para insertar un nuevo caso de prueba debe completar todos los campos obligatorios.";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                upModal.Update();
                habilitarCamposInsertar();
            }
            else
            {
                Object[] datosNuevos = new Object[6];
                datosNuevos[0] = this.txtIdentificador.Text;
                datosNuevos[1] = this.txtProposito.Text;
                datosNuevos[2] = this.txtFlujoCentral.Text;
                datosNuevos[3] = this.txtEntradaDatos.Text;
                datosNuevos[4] = this.txtResultadoEsperado.Text;
                datosNuevos[5] = idDiseño;

                if (controladoraCasoPruebas.ejecutarAccion(modo, datosNuevos, 0, ""))
                {
                    //funcionarioInsertado = this.txtCedula.Text;
                    //llenarGrid(null);
                    controlarCampos(false);
                    cambiarEnabled(true, this.btnModificar);
                    cambiarEnabled(true, this.btnEliminar);
                    cambiarEnabled(false, this.btnAceptar);
                    cambiarEnabled(false, this.btnCancelar);
                    cambiarEnabled(true, this.btnInsertar);

                    lblModalTitle.Text = "";
                    lblModalBody.Text = "Nuevo caso de prueba creado con éxito.";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                    upModal.Update();

                }else
                {
                    lblModalTitle.Text = "ERROR";
                    lblModalBody.Text = "Este caso de prueba ya se encuentra registrado en el sistema.";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                    upModal.Update();
                    habilitarCamposInsertar();
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

        /*Método para modificar un caso de prueba
         * Requiere: No recibe parámetros
         * Modifica: Verifica que se encuentren los campos obligatorios para un caso, si se encuentran guarda la modificación en la base, si no muestra un mensaje de error
         *Retorna: no retorna ningún valor
         */
        private void btnAceptar_Modificar()
        {
            if (faltanDatos())
            {
                lblModalTitle.Text = "AVISO";
                lblModalBody.Text = "Para insertar un nuevo caso de prueba debe completar todos los campos obligatorios.";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                upModal.Update();
                habilitarCamposInsertar();
            }
            else
            {
                modo = 2;
                int idCaso = 0;

                //Crea el objeto con los datos del caso de prueba
                Object[] datosNuevos = new Object[5];
                datosNuevos[0] = this.txtIdentificador.Text;
                datosNuevos[1] = this.txtProposito.Text;
                datosNuevos[2] = this.txtFlujoCentral.Text;
                datosNuevos[3] = this.txtEntradaDatos.Text;
                datosNuevos[4] = this.txtResultadoEsperado.Text;
                datosNuevos[5] = (int)Session["idDiseñoConsultado"];

                //If si la modificación del caso fue exitosa
                if (controladoraCasoPruebas.ejecutarAccion(modo, datosNuevos, idCaso, ""))
                {
                    controlarCampos(false);
                    cambiarEnabled(true, this.btnModificar);
                    cambiarEnabled(true, this.btnEliminar);
                    cambiarEnabled(false, this.btnAceptar);
                    cambiarEnabled(false, this.btnCancelar);
                    cambiarEnabled(true, this.btnInsertar);
                    lblModalTitle.Text = "";
                    lblModalBody.Text = "Nuevo caso de prueba creado con éxito.";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                    upModal.Update();
                }
                    //Si la modificación del caso fue erronea
                else
                {
                    lblModalTitle.Text = "ERROR";
                    lblModalBody.Text = "Este caso de prueba ya se encuentra registrado en el sistema.";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                    upModal.Update();
                }
            }
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
            if (this.txtProposito.Text == "" || this.txtIdentificador.Text == "" )
            {
                resultado = true;
            }
            else
            {
                resultado = false;
            }

            return resultado;
        }


        //metodo para llenar datos de diseño

        protected void llenarDatosDiseno()
        {


            int idProyecto = 0;
            DataTable datosDiseno = controladoraCasoPruebas.consultarInformacionDiseno(Int32.Parse(Session["idDiseñoS"].ToString()));
            DataTable datosProyecto = controladoraCasoPruebas.consultarInformacionProyectoDiseno(Int32.Parse(Session["idDiseñoS"].ToString()));
            DataTable datosRequerimientos;
            string requerimiento = "";
            if(datosDiseno!=null){
                idProyecto = Int32.Parse(datosProyecto.Rows[0][1].ToString());
                this.txtNombreProyecto.Text = datosProyecto.Rows[0][0].ToString();
                this.txtNombreDiseño.Text = datosDiseno.Rows[0][1].ToString();
                this.txtPrueba.Text =  datosDiseno.Rows[0][7].ToString();
                this.txtTecnicaPrueba.Text = datosDiseno.Rows[0][6].ToString();
                this.txtProposito.Text = datosDiseno.Rows[0][3].ToString();
                datosRequerimientos = controladoraCasoPruebas.consultarReqDisenoDeProyecto(Int32.Parse(Session["idDiseñoS"].ToString()),idProyecto);
                this.listRequerimientoDisponibles.Items.Clear();


                if (datosRequerimientos.Rows.Count >= 1)
                {
                    listRequerimientoDisponibles.Items.Clear();
                    for (int i = 0; i < datosRequerimientos.Rows.Count; ++i)
                    {
                        requerimiento = datosRequerimientos.Rows[i][0].ToString() +" "+ datosRequerimientos.Rows[i][2].ToString();
                        listRequerimientoDisponibles.Items.Add(requerimiento);

                    }
                }
            }
        }


        /*Método para crear la acción de eliminar un caso de prueba
        * Modifica: Cambia la propiedad enabled de botones y cajas de texto,
        * Limpia cajas de texto y coloca los ejemplos de datos donde es necesario
        * Retorna: no retorna ningún valor
        */
        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            cambiarEnabled(false, this.btnInsertar);
            cambiarEnabled(false, this.btnModificar);
            cambiarEnabled(false, this.btnAceptar);
            cambiarEnabled(false, this.btnCancelar);
            modo = 3;
            controlarCampos(false);


            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalConfirmar", "$('#modalConfirmar').modal();", true);
            upModal.Update();

        }


        /*Método para la acción de aceptar cuando esta en modo de borrado
         * Requiere: No requiere ningún parámetro
         * Modifica:no modifica datos
         * Retorna: No retorna ningún valor
         */
        protected void btnAceptar_Eliminar(object sender, EventArgs e)
        {
            
            int id = controladoraCasoPruebas.consultarIdCasoPrueba(txtIdentificador.Text);
           
                if (controladoraCasoPruebas.ejecutarAccion(3,null,id,"")==true)
                {
                    lblModalTitle.Text = " ";
                    lblModalBody.Text = "La eliminación del caso fue exitosa.";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                    upModal.Update();
                }else
                {
                    lblModalTitle.Text = "ERROR";
                    lblModalBody.Text = "La eliminación del caso no se pudo dar";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                    upModal.Update();
                }

                vaciarCampos();

                controlarCampos(false);
                cambiarEnabled(false, this.btnModificar);
                cambiarEnabled(false, this.btnEliminar);
                cambiarEnabled(false, this.btnAceptar);
                cambiarEnabled(false, this.btnCancelar);


                //El unico botón que cambia de acuerdo al perfil es el de insertar y el grid se llena de acuerdo al tipo de usuario utilizando el sistema
              /*  if (Session["perfil"].ToString().Equals("Administrador"))
                {
                    cambiarEnabled(true, this.btnInsertar);
                    llenarGrid(null);
                }
                else
                {
                    cambiarEnabled(false, this.btnInsertar);
                    llenarGrid(Session["cedula"].ToString());
                }
             */
          
        }




    }


}