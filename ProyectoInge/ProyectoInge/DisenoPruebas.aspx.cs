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
    public partial class DisenoPruebas : System.Web.UI.Page
    {
        ControladoraRecursos controladoraRH = new ControladoraRecursos();

        ControladoraDiseno controladoraDiseno = new ControladoraDiseno();
        private string idProyectoConsultado;
        private string idDiseñoConsultado;
         Dictionary<string, string> cedulasRepresentantes = new Dictionary<string, string>();
        Dictionary<string, string> nombreRepresentantesConsultados = new Dictionary<string, string>();
        private static int modo = 1; //1 insertar, 2 modificar, 3 eliminar

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
                llenarComboNivel();
                llenarComboTecnica();
                
            }


            if (Session["perfil"].ToString().Equals("Administrador"))
            {
                cambiarEnabled(true, this.btnInsertar);
                llenarGrid(null);
                llenarComboProyecto(null);
            }
            else
            {
                cambiarEnabled(false, this.btnInsertar);
                llenarGrid(Session["cedula"].ToString());
                llenarComboProyecto(Session["cedula"].ToString());
            }
            llenarComboRecursos();
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
            llenarComboNivel();
            llenarComboProyecto(Session["cedula"].ToString());
            llenarComboRecursos();
            llenarComboTecnica();

            modo = 2;
            //habilitarCamposModificar();
            //llenarDropDownRol();
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
            //this.comboProyecto.Enabled = condicion;
            this.listReqAgregados.Enabled = condicion;
            this.listReqProyecto.Enabled = condicion;
            this.lnkAgregarReq.Enabled = condicion;
            this.lnkQuitarReq.Enabled = condicion;
            this.txtProposito.Enabled = condicion;
            this.comboNivel.Enabled = condicion;
            this.comboTecnica.Enabled = condicion;
            this.comboTipo.Enabled = condicion;
            this.txtAmbiente.Enabled = condicion;
            this.txtProcedimiento.Enabled = condicion;
            this.txtCriterios.Enabled = condicion;
            this.comboResponsable.Enabled = condicion;
            this.txtCalendar.Enabled = condicion;
            this.calendarFecha.Enabled = condicion;
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

        /* Método para llenar el comboBox los  proyectos de acuerdo a si es miembro o administrador 
       * Modifica: llena el comboBox con los datos obtenidos de la BD
       * Retorna: no retorna ningún valor */
        protected void llenarComboProyecto(string cedulaUsuario)
        {
            this.comboProyecto.Items.Clear();
            DataTable nombresProyecto;
            int numDatos = 0;
            Object[] datos;


            if(cedulaUsuario==null){

                nombresProyecto = controladoraDiseno.consultarNombresProyectos();
                if (nombresProyecto != null && nombresProyecto.Rows.Count > 0)
                {
                    numDatos = nombresProyecto.Rows.Count;
                }
            }else{
                nombresProyecto = controladoraDiseno.consultarProyectosDeUsuario(cedulaUsuario);
                if (nombresProyecto != null && nombresProyecto.Rows.Count > 0)
                {
                    numDatos = nombresProyecto.Rows.Count;
                }
            }

            if (numDatos>0)
            {
                datos = new Object[numDatos];

                for (int i = 0; i < numDatos; ++i)
                {
                    datos[i] = nombresProyecto.Rows[i][0].ToString();
                }

                this.comboProyecto.DataSource = datos;
                this.comboProyecto.DataBind();
            }

        }

        /* Método para llenar el comboBox de los diferentes niveles de prueba que existen
       * Modifica: llena el comboBox con los datos obtenidos de la BD
       * Retorna: no retorna ningún valor */
       
            protected void llenarComboNivel()
        {
            this.comboNivel.Items.Clear();
            DataTable Niveles = controladoraDiseno.consultarNiveles();
            int numDatos = Niveles.Rows.Count;
            Object[] datos;


            if (Niveles.Rows.Count >= 1)
            {
                numDatos = Niveles.Rows.Count;
                datos = new Object[numDatos+1];

                for (int i = 0; i < Niveles.Rows.Count; ++i)
                {
                    datos[i+1] = Niveles.Rows[i][0].ToString();
                }

                datos[0] = "Seleccione";
                this.comboNivel.DataSource = datos;
                this.comboNivel.DataBind();
            }

        
        }


            /* Método para llenar el comboBox de los diferentes tecnicas de prueba que existen
              * Modifica: llena el comboBox con los datos obtenidos de la BD
              * Retorna: no retorna ningún valor */

            protected void llenarComboTecnica()
            {
                this.comboTipo.Items.Clear();
                DataTable Tecnicas = controladoraDiseno.consultarTecnicas();
                int numDatos = Tecnicas.Rows.Count;
                Object[] datos;


                if (Tecnicas.Rows.Count >= 1)
                {
                    numDatos = Tecnicas.Rows.Count;
                    datos = new Object[numDatos+1];

                    for (int i = 0; i < Tecnicas.Rows.Count; ++i)
                    {
                        datos[i+1] = Tecnicas.Rows[i][0].ToString();
                    }

                    datos[0] = "Seleccione";
                    this.comboTecnica.DataSource = datos;
                    this.comboTecnica.DataBind();
                }


            }


            /* Método para llenar el comboBox de los recursos
              * Modifica: llena el comboBox con los datos obtenidos de la BD
              * Retorna: no retorna ningún valor */

            protected void llenarComboRecursos()
            {

                this.comboProyecto.Items.Clear();
                int id = controladoraDiseno.obtenerIDconNombreProyecto(this.comboProyecto.Text);
                DataTable Recursos = controladoraDiseno.consultarMiembrosProyecto(id.ToString());
                int numDatos = Recursos.Rows.Count;
                Object[] datos;
                string nombre = "";

                if (Recursos.Rows.Count >= 1)
                {
                    numDatos = Recursos.Rows.Count;
                    datos = new Object[numDatos];

                    for (int i = 0; i < Recursos.Rows.Count; ++i)
                    {
                        nombre = Recursos.Rows[i][0].ToString() + " "+ Recursos.Rows[i][1].ToString();
                        datos[i] = nombre;
                    }

                    this.comboResponsable.DataSource = datos;
                    this.comboResponsable.DataBind();
                }


            }


        protected void proyectoSeleccionado(object sender, EventArgs e)
        {
           
        }

        /*Método para hacer visible el calendario cuando el usuario presiona el botón */
        protected void lnkCalendario_Click(object sender, EventArgs e)
        {
            calendarFecha.Visible = true;
            calendarFecha.SelectedDate = calendarFecha.TodaysDate;
            UpdatePanelCalendario.Update();
        }

        /*Método para leer la fecha seleccionada por el usuario */
        protected void calendarioSeleccionado(object sender, EventArgs e)
        {
            calendarFecha.Visible = false;
            txtCalendar.Text = calendarFecha.SelectedDate.ToString().Substring(0, 10);
            UpdatePanelCalendario.Update();
        }


        /*Método para que la página no se actualice cada vez que el usuario elige un nuevo mes */
        protected void cambioDeMes(object sender, MonthChangedEventArgs e)
        {
            UpdatePanelCalendario.Update();
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
                        // btnAceptar_Modificar();
                    }
                    break;
                case 3:
                    {
                        //btnAceptar_Eliminar();
                    }
                    break;

            }
        
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {

        }

        /*Método para la acción de aceptar cuando esta en modo de inserción
        * Requiere: No requiere ningún parámetro
        * Modifica: Crea un objeto con los datos obtenidos en la interfaz mediante textbox y 
        * valida que todos los datos se encuentren para la inserción
        * Retorna: No retorna ningún valor
        */
        protected void btnAceptar_Insertar()
        {
            int tipoInsercion = 1;   //1 insertar oficina usuaria, 

            //si faltan datos no deja insertar
            if (faltanDatos())
            {
                lblModalTitle.Text = " ";
                lblModalBody.Text = "Para insertar un nuevo diseño de prueba debe completar todos los datos obligatorios.";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                upModal.Update();
                habilitarCamposInsertar();
            }
            else
            {

                int idProyecto = controladoraDiseno.obtenerIdProyecto(this.comboProyecto.Text);
                //Se crea el objeto para encapsular los datos de la interfaz para insertar oficina usuaria
                Object[] datosNuevos = new Object[10];
                datosNuevos[0] = this.txtProposito.Text;
                datosNuevos[1] = this.txtCalendar.Text;
                datosNuevos[2] = this.txtProcedimiento.Text;
                datosNuevos[3] = this.txtAmbiente.Text;
                datosNuevos[4] = this.txtCriterios.Text;
                datosNuevos[5] = this.comboTecnica.Text;
                datosNuevos[6] = this.comboNivel.Text;
                datosNuevos[7] = this.comboTipo.Text;
                datosNuevos[8] = idProyecto;
                // datosNuevos[9] = obtenerCedula(this.comboResponsable.Text);


                //si el diseño de prueba se pudo insertar correctamente entra a este if
                if (controladoraDiseno.ejecutarAccion(modo, tipoInsercion, datosNuevos, "", ""))
                {
                    

                    //Se actualiza la tabla de requerimientos para asociarle el/los requerimientos a un diseño 
                    
                    int i = 0;
                    int indiceReq = 0;
                    string sigla = "";
                    string nombreRequerimiento = "";


                    if (this.comboNivel.Text == "Unitaria")
                    {
                        while (listReqAgregados.Items[0].ToString().ElementAt(indiceReq) != ' ')
                        {
                            ++indiceReq;
                        }

                        sigla = listReqAgregados.Items[0].ToString().Substring(0, indiceReq);
                        nombreRequerimiento = listReqAgregados.Items[0].ToString().Substring(indiceReq + 1, listReqAgregados.Items[0].ToString().Count() - indiceReq - 1);
                        Object[] requerimientoActualizado = new Object[4];
                        requerimientoActualizado[0] = sigla;
                        requerimientoActualizado[1] = idProyecto;
                        requerimientoActualizado[2] = controladoraDiseno.obtenerIdDisenoPorProposito(this.txtProposito.Text);
                        requerimientoActualizado[3] = nombreRequerimiento;
                        tipoInsercion = 2;

                        //Se actualizó un requerimiento
                        if (controladoraDiseno.ejecutarAccion(1, tipoInsercion, requerimientoActualizado, "", ""))//Esto siempre inserta, por lo que le mandaremos un 1
                        {
                        }
                        //La actualizó de un requerimiento falló porque el habían datos inválidos.
                        else
                        {
                            lblModalTitle.Text = " ";
                            lblModalBody.Text = "No fue posible realizar la actualización del requerimiento.";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                            upModal.Update();
                            habilitarCamposInsertar();
                        }
                    }

                    else
                    {
                        // PREGUNTAR SI ESTO ES PARA  INTEGRACION DE SISTEMA Y DE ACEPTACION  O SOLO INTEGRACION

                        while (i < listReqAgregados.Items.Count && listReqAgregados.Items[i].Text.Equals("") == false)
                        {

                            indiceReq = 0;
                            sigla = "";
                            nombreRequerimiento = "";



                            while (indiceReq < listReqAgregados.Items[i].ToString().Count() && listReqAgregados.Items[i].ToString().ElementAt(indiceReq) != ' ')
                            {
                                ++indiceReq;
                            }
                            sigla = listReqAgregados.Items[i].ToString().Substring(0, indiceReq);
                            nombreRequerimiento = listReqAgregados.Items[i].ToString().Substring(indiceReq + 1, listReqAgregados.Items[i].ToString().Count() - indiceReq - 1);


                            Object[] requerimientoActualizado = new Object[4];
                            requerimientoActualizado[0] = sigla;
                            requerimientoActualizado[1] = idProyecto;
                            requerimientoActualizado[2] = controladoraDiseno.obtenerIdDisenoPorProposito(this.txtProposito.Text);
                            requerimientoActualizado[3] = nombreRequerimiento;
                            tipoInsercion = 2;

                            //Se actualizó un requerimiento
                            if (controladoraDiseno.ejecutarAccion(1, tipoInsercion, requerimientoActualizado, "", ""))//Esto siempre inserta, por lo que le mandaremos un 1
                            {
                            }
                            //La actualizó de un requerimiento falló porque el habían datos inválidos.
                            else
                            {
                                lblModalTitle.Text = " ";
                                lblModalBody.Text = "No fue posible realizar la actualización del requerimiento.";
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                                upModal.Update();
                                habilitarCamposInsertar();
                            }

                            i++;

                        }
                    }

                      //se carga la interfaz de nuevo
                        controlarCampos(false);
                        cambiarEnabled(true, this.btnModificar);
                        cambiarEnabled(true, this.btnEliminar);
                        cambiarEnabled(false, this.btnAceptar);
                        cambiarEnabled(false, this.btnCancelar);
                        cambiarEnabled(true, this.btnInsertar);
                        llenarGrid(null);

                        lblModalTitle.Text = " ";
                        lblModalBody.Text = "Nuevo diseño creado con éxito.";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                        upModal.Update();

                }
                else
                {

                        lblModalTitle.Text = " ";
                        lblModalBody.Text = "Este diseño ya se encuentra registrado en el sistema.";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                        upModal.Update();
                        habilitarCamposInsertar();
                }

            } 
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
            cambiarEnabledTel(true, this.lnkAgregarReq);
            cambiarEnabledTel(true, this.lnkQuitarReq);
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
            if (this.comboProyecto.Text == "" || this.txtProposito.Text == "" || this.txtProcedimiento.Text == ""  || this.comboResponsable.Text == "" || this.txtCalendar.Text == "")
            {
                resultado = true;
            }
            else
            {
                resultado = false;
            }

            return resultado;
        }


        /*Método para obtener el registro que se desea consulta en el dataGriedViw y mostrar los resultados de la consulta en pantalla.
       * Modifica: el valor del la variable idDiseño con el ID del diseño que se desea consultar.
       * Retorna: no retorna ningún valor
       */
        protected void gridDisenos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "seleccionarDiseno")
            {

                LinkButton lnkConsulta = (LinkButton)e.CommandSource;
                idDiseñoConsultado = lnkConsulta.CommandArgument;
                Session["idDiseñoS"] = idProyectoConsultado;

                controlarCampos(false);
                llenarDatos(idDiseñoConsultado);
                cambiarEnabled(true, this.btnModificar);
                cambiarEnabled(true, this.btnCancelar);
                cambiarEnabled(true, this.btnEliminar);
                cambiarEnabled(false, this.btnAceptar);

                //El unico botón que cambia de acuerdo al perfil es el de eliminar
                if (Session["perfil"].ToString().Equals("Administrador"))
                {

                    cambiarEnabled(true, this.btnInsertar);
                }
                else
                {
                    cambiarEnabled(false, this.btnInsertar);
                }

                //listMiembrosDisponibles.Items.Clear();
            }

            if (e.CommandName == "seleccionarCaso")
            {
                LinkButton lnkConsulta = (LinkButton)e.CommandSource;
                idDiseñoConsultado = lnkConsulta.CommandArgument;
                Session["idDiseñoCaso"] = idProyectoConsultado;
         
            }


        }

        /*Método para cerrar la sesión abierta de un usuario y dirigirse a la página de inicio.
         * Requiere: recibe el evento cuando se presiona el botón para cerrar sesión.
         * Modifica: Modifica el valor booleano del estado de la sesión
         * Retorna: No retorna ningún valor
         */
        protected void cerrarSesion(object sender, EventArgs e)
        {
            ControladoraRecursos controladoraRH = new ControladoraRecursos();

            string ced = (string)Session["cedula"];
            Boolean a = controladoraRH.modificarEstadoCerrar(ced);
            Response.Redirect("~/Login.aspx");
        }


        /*Método para llenar los campos de la interfaz con los resultados de la consulta.
      * Requiere: El identificador del diseño que se desea consultar.
      * Modifica: Los campos de la interfaz correspondientes a los datos recibidos mediante la clase controladora.
      * Retorna: No retorna ningún valor. 
      */
        public void llenarDatos(string idDiseño)
        {
            string nombreRepresentante;
            string nombre = "";
            ListItem representante;
            Dictionary<string, string> nombresDelRepresentante = (Dictionary<string, string>)Session["nombreRepresentantes_Consultados"];
            String cedulaRepresentante="";
            idDiseñoConsultado = idDiseño;
            Response.Write(idDiseño);
            DataTable datosFilaDiseño = controladoraDiseno.consultarDiseno(Int32.Parse(idDiseño)); //Se obtienen los datos del diseño
            DataTable datosMiembro = null;
            DataTable datosReqProyecto = null;
            DataTable datosReqDiseno = null;
            
            if (datosFilaDiseño.Rows.Count == 1)
            {

                this.txtProposito.Text = datosFilaDiseño.Rows[0][1].ToString();
                this.txtCalendar.Text = datosFilaDiseño.Rows[0][2].ToString();
                this.txtProcedimiento.Text = datosFilaDiseño.Rows[0][3].ToString();
                this.txtAmbiente.Text = datosFilaDiseño.Rows[0][4].ToString();
                this.txtCriterios.Text = datosFilaDiseño.Rows[0][5].ToString();
                idProyectoConsultado = datosFilaDiseño.Rows[0][9].ToString();
                cedulaRepresentante = datosFilaDiseño.Rows[0][10].ToString();
                datosMiembro = controladoraDiseno.consultarRepresentanteDiseno(cedulaRepresentante); //Se obtienen los miembros que trabajan en el proyecto
                datosReqProyecto = controladoraDiseno.consultarReqProyecto(Int32.Parse(idProyectoConsultado));
                datosReqDiseno = controladoraDiseno.consultarReqDisenoDeProyecto(Int32.Parse(idDiseño), Int32.Parse(idProyectoConsultado));

                if (this.comboNivel.Items.FindByText(datosFilaDiseño.Rows[0][7].ToString()) != null)
                {
                    ListItem niveles = this.comboNivel.Items.FindByText(datosFilaDiseño.Rows[0][7].ToString());
                    this.comboNivel.SelectedValue = niveles.Value;
                }
                if (this.comboTecnica.Items.FindByText(datosFilaDiseño.Rows[0][6].ToString()) != null)
                {
                    ListItem tecnica = this.comboTecnica.Items.FindByText(datosFilaDiseño.Rows[0][6].ToString());
                    this.comboTecnica.SelectedValue = tecnica.Value;
                }
                if (this.comboTipo.Items.FindByText(datosFilaDiseño.Rows[0][8].ToString()) != null)
                {
                    ListItem tipo = this.comboTipo.Items.FindByText(datosFilaDiseño.Rows[0][8].ToString());
                    this.comboTipo.SelectedValue = tipo.Value;
                }
                

                if (this.comboResponsable.Items.FindByText(datosMiembro.Rows[0][0].ToString()) != null)
                {

                    nombre = datosMiembro.Rows[0][1].ToString()+ " " + datosMiembro.Rows[0][2].ToString();
                    ListItem represent = this.comboResponsable.Items.FindByText(nombre);
                    this.comboResponsable.SelectedValue = represent.Value;
                }
                
            }
           
            listReqAgregados.Items.Clear();
            string requerimiento = "";
            if (datosReqDiseno.Rows.Count >= 1)
            {
                listReqAgregados.Items.Clear();
                for (int i = 0; i < datosReqDiseno.Rows.Count; ++i)
                {
                    requerimiento = datosReqDiseno.Rows[i][0].ToString() + datosReqDiseno.Rows[i][1].ToString();
                    listReqAgregados.Items.Add(requerimiento);

                }
            }
            requerimiento = "";
            listReqProyecto.Items.Clear();
            if (datosReqProyecto.Rows.Count >= 1)
            {
                listReqAgregados.Items.Clear();
                for (int i = 0; i < datosReqProyecto.Rows.Count; ++i)
                {
                    requerimiento = datosReqProyecto.Rows[i][0].ToString() + " "+ datosReqProyecto.Rows[i][3].ToString();
                    listReqProyecto.Items.Add(requerimiento);

                }
            }

            

            


            


        }


         /*Método para llenar el grid los proyectos del sistema o con los proyectos en los que el miembro se encuentre asociado.
      * Requiere: Requiere la cédula del miembro utilizando el sistema en caso de que éste no sea un administrador
      * Modifica: el valor de cada uno de los campos en la interfaz correspondientes a la consulta retornada por la clase controladora.
      * Retorna: no retorna ningún valor
      */
        protected void llenarGrid(string idUsuario)
        {
            DataTable dt = crearTablaDisenos();
            DataTable diseños;
            DataTable idProyectos;
            DataTable idDiseños;
            Object[] datos = new Object[6];
            string representante = "";
            int indiceColumna = 0;
            string nombreRepresentante = "";
            DataTable representantes;
            nombreRepresentantesConsultados.Clear();

            

            if (idUsuario == null) //Significa que el usuario utilizando el sistema es un administrador por lo que se le deben mostrar 
            //todos los recursos humanos del sistema
            {
                //Se obtienen todos los proyectos pues el administrador es el usuario del sistema
                
                diseños = controladoraDiseno.consultarDisenos(null);

                //Se obtienen todos los lideres del sistema
                representantes = controladoraDiseno.consultarRepresentantesDisenos();


                for (int i = 0; i < representantes.Rows.Count; ++i)
                {
                    foreach (DataColumn column in representantes.Columns)
                    {

                        if (indiceColumna == 3)
                        {
                            nombreRepresentantesConsultados.Add(representantes.Rows[i][column].ToString(), nombreRepresentante);

                        }
                        else
                        {
                            nombreRepresentante = nombreRepresentante + " " + representantes.Rows[i][column].ToString();
                        }

                        ++indiceColumna;



                    }

                    indiceColumna = 0; //Contador para saber el número de columna actual.
                    nombreRepresentante = "";

                }

                Session["nombreRepresentates_Consultados"] = nombreRepresentantesConsultados;


                if (diseños.Rows.Count > 1)
                {
                    foreach (DataRow fila in diseños.Rows)
                    {
                        if (fila[1].ToString() != "Dummy")
                        {
                            datos[0] = fila[0].ToString();
                            datos[1] = fila[1].ToString();
                            datos[2] = fila[2].ToString();
                            datos[3] = fila[3].ToString();
                            datos[4] = fila[4].ToString();
                            nombreRepresentantesConsultados.TryGetValue(fila[5].ToString(), out representante);
                            datos[5] = representante;
                            dt.Rows.Add(datos);
                        }
                    }

                    representante = "";

                }
                else
                {
                    datos[0] = "-";
                    datos[1] = "-";
                    datos[2] = "-";
                    datos[3] = "-";
                    datos[4] = "-";
                    datos[5] = "-";
                    dt.Rows.Add(datos);
                }
            }
            else
            {
                //Se obtiene un DataTable con el identificador del o los proyectos en los cuales trabaja el miembro
                
                idProyectos = controladoraDiseno.consultarProyectosAsociados(idUsuario);
                
                if (idProyectos.Rows.Count > 0)
                {
                    
                    //Se obtiene un DataTable con los datos del o los proyectos 
                    diseños = controladoraDiseno.consultarDisenos(idProyectos);

                    representantes = controladoraDiseno.consultarRepresentantesDisenos();


                    for (int i = 0; i < representantes.Rows.Count; ++i)
                    {
                        foreach (DataColumn column in representantes.Columns)
                        {

                            if (indiceColumna == 3)
                            {
                                nombreRepresentantesConsultados.Add(representantes.Rows[i][column].ToString(), nombreRepresentante);

                            }
                            else
                            {
                                nombreRepresentante = nombreRepresentante + " " + representantes.Rows[i][column].ToString();
                            }

                            ++indiceColumna;
                        }

                        indiceColumna = 0; //Contador para saber el número de columna actual.
                        nombreRepresentante = "";
                        Session["nombreRepresentantes_Consultados"] = nombreRepresentantesConsultados;

                    }




                    if (diseños.Rows.Count > 0)
                    {

                        foreach (DataRow fila in diseños.Rows)
                        {
                            if (fila[1].ToString() != "Dummy")
                            {
                                datos[0] = fila[0].ToString();
                                datos[1] = fila[1].ToString();
                                datos[2] = fila[2].ToString();
                                datos[3] = fila[3].ToString();
                                datos[4] = fila[4].ToString();
                                nombreRepresentantesConsultados.TryGetValue(fila[5].ToString(), out representante);
                                datos[5] = representante;
                                dt.Rows.Add(datos);
                            }

                        }

                        representante = "";
                    }
                    else
                    {
                        datos[0] = "-";
                        datos[1] = "-";
                        datos[2] = "-";
                        datos[3] = "-";
                        datos[4] = "-";
                        datos[5] = "-";
                        dt.Rows.Add(datos);
                    }
                }
                else
                {
                    datos[0] = "-";
                    datos[1] = "-";
                    datos[2] = "-";
                    datos[3] = "-";
                    datos[4] = "-";
                    datos[5] = "-";

     
                    dt.Rows.Add(datos);
                }

            }

            this.gridDisenos.DataSource = dt;
            this.gridDisenos.DataBind();
            
        }


        /*Método para crear el DataTable donde se mostrará el o los registros de los proyectos del sistema según corresponda.
      * Requiere: No requiere ningún parámetro.
      * Modifica: el nombre de cada columna donde se le especifica el nombre que cada una de ellas tendrá. 
      * Retorna: el DataTable creado. 
      */
        protected DataTable crearTablaDisenos()
        {
            DataTable dt = new DataTable();
            DataColumn columna;
            DataRow row = dt.NewRow();

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "ID Diseño";
            dt.Columns.Add(columna);

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Propósito";
            dt.Columns.Add(columna);

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Técnica";
            dt.Columns.Add(columna);


            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Nivel";
            dt.Columns.Add(columna);

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Tipo";
            dt.Columns.Add(columna);


            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Nombre Representante";
            dt.Columns.Add(columna);

      

            return dt;
        }


    }
}