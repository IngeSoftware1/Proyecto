using System;
using System.Collections.Generic;
using System.Linq;
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
    public partial class EjecucionPruebas : System.Web.UI.Page
    {
        ControladoraEjecucion controladoraEjecucionPruebas = new ControladoraEjecucion();
        

        //Variablaes globales utilizadas para cuando el cliente modifica alguna fila del grid
        private static string comboTipoNCTxt = "";
        private static string idCasoTxt = "";
        private static string descripcionTxt = "";
        private static string justificacionTxt = "";
        private static string resultadosTxt = "";
        private static string estadoTxt = "";
        private static int filaEliminar = 0;
        private static List<int> comboDedisenos = new List<int>();
        private static DropDownList comboTipoNCModificar;
        private static DropDownList comboIdCasoModificar;
        private static DropDownList comboIdEstadoModificar;
        private static bool casosIniciados = false;
        private int modo;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["cedula"] == null)
            {
                Response.Redirect("~/Login.aspx");

            }

            if (!IsPostBack)
            {
                //  gridTipoNC_Inicial();
                if (Session["perfil"].ToString().Equals("Administrador"))
                {
                    llenarComboProyecto(null);
                    cambiarEnabled(false, this.btnModificar);
                    cambiarEnabled(false, this.btnEliminar);
                    cambiarEnabled(false, this.btnAceptar);
                    cambiarEnabled(false, this.btnCancelar);        
                    gridTipoNC_Inicial(0, false);
                    habilitarCampos(false);
                    
                    UpdateProyectoDiseno.Update();
                    //  llenarGrid(null);
                }
                else
                {
                    llenarComboProyecto(Session["cedula"].ToString());
                    cambiarEnabled(false, this.btnModificar);
                    cambiarEnabled(false, this.btnEliminar);
                    cambiarEnabled(false, this.btnAceptar);
                    cambiarEnabled(false, this.btnCancelar);
                    gridTipoNC_Inicial(0, false);
                    UpdateProyectoDiseno.Update();
                    //  llenarGrid(Session["cedula"].ToString());
                }
            }

            

        }

        protected void habilitarCampos(bool condicion)
        {
            txtIncidencias.Enabled = condicion;
            comboResponsable.Enabled = condicion;
            this.comboDiseño.Enabled = condicion;
            this.comboProyecto.Enabled = condicion;
            this.comboResponsable.Enabled = condicion;
            cambiarEnabledGridNC(condicion);
        }

        protected void cambiarEnabledGridNC(bool condicion)
        {

            (gridNoConformidades.FooterRow.FindControl("btnAgregarNC") as LinkButton).Enabled = condicion;
            (gridNoConformidades.FooterRow.FindControl("comboTipoNC") as DropDownList).Enabled = condicion;
            (gridNoConformidades.FooterRow.FindControl("comboCasoPrueba") as DropDownList).Enabled = condicion;
            (gridNoConformidades.FooterRow.FindControl("txtDescripcion") as TextBox).Enabled = condicion;
            (gridNoConformidades.FooterRow.FindControl("txtJustificacion") as TextBox).Enabled = condicion;
            (gridNoConformidades.FooterRow.FindControl("txtResultados") as TextBox).Enabled = condicion;
            (gridNoConformidades.FooterRow.FindControl("comboEstado") as DropDownList).Enabled = condicion;
            (gridNoConformidades.FooterRow.FindControl("lnkCargarImagen") as Button).Enabled = condicion;

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


        /* Método para llenar el comboBox de los tipos de no conformidades
       * Modifica: llena el comboBox con los datos obtenidos de la BD
       * Retorna: no retorna ningún valor */
        protected void llenarComboTipoNC(bool tipoCombo)
        {
            DropDownList comboNoConformidades = (DropDownList)gridNoConformidades.FooterRow.FindControl("comboTipoNC");
            DataTable tiposNC = controladoraEjecucionPruebas.consultarTiposNC();

            if (tipoCombo == true)
            {
                comboNoConformidades = (DropDownList)gridNoConformidades.FooterRow.FindControl("comboTipoNC");
            }
            else
            {
                comboNoConformidades = comboTipoNCModificar;
            }

            int numDatos = tiposNC.Rows.Count;
            Object[] datos;

            if (tiposNC.Rows.Count >= 1)
            {
                numDatos = tiposNC.Rows.Count;
                datos = new Object[numDatos + 1];

                for (int i = 0; i < tiposNC.Rows.Count; ++i)
                {
                    datos[i + 1] = tiposNC.Rows[i][0].ToString();
                }

                datos[0] = "Seleccione";
                comboNoConformidades.DataSource = datos;
                comboNoConformidades.DataBind();

                if (tipoCombo == false)
                {
                    ListItem itemEstablecido = comboNoConformidades.Items.FindByText(comboTipoNCTxt);
                    comboNoConformidades.SelectedValue = itemEstablecido.Value;
                }

            }

            UpdateGridNoConformidades.Update();
        }

        /* Método para llenar el comboBox de los estados 
        * Modifica: llena el comboBox con los datos obtenidos de la BD
        * Retorna: no retorna ningún valor */
        protected void llenarComboEstados(bool tipoCombo)
        {

            DropDownList comboDeEstado = (DropDownList)gridNoConformidades.FooterRow.FindControl("comboEstado");
            DataTable estados = controladoraEjecucionPruebas.consultarEstados();

            if (tipoCombo == true)
            {
                comboDeEstado = (DropDownList)gridNoConformidades.FooterRow.FindControl("comboEstado");
            }
            else
            {
                comboDeEstado = comboIdEstadoModificar;
            }

            int numDatos = estados.Rows.Count;
            Object[] datos;

            if (estados.Rows.Count >= 1)
            {
                numDatos = estados.Rows.Count;
                datos = new Object[numDatos + 1];

                for (int i = 0; i < estados.Rows.Count; ++i)
                {
                    datos[i + 1] = estados.Rows[i][0].ToString();
                }

                datos[0] = "Seleccione";
                comboDeEstado.DataSource = datos;
                comboDeEstado.DataBind();

                if (tipoCombo == false)
                {
                    ListItem itemEstablecido = comboDeEstado.Items.FindByText(estadoTxt);
                    comboDeEstado.SelectedValue = itemEstablecido.Value;
                }

            }

            UpdateGridNoConformidades.Update();
        }

        /* Método para llenar el comboBox los  proyectos de acuerdo a si es miembro o administrador 
        * Modifica: llena el comboBox con los datos obtenidos de la BD
        * Retorna: no retorna ningún valor */
        protected void llenarComboProyecto(string cedulaUsuario)
        {
            Dictionary<string, string> nombres_id_proyectos = new Dictionary<string, string>();
            Dictionary<string, string> id_nombres_proyectos = new Dictionary<string, string>();
            string nombre = "";
            this.comboProyecto.Items.Clear();
            DataTable nombresProyecto;
            int numDatos = 0;
            Object[] datos;
            int indiceProyecto = 1;
            int numColumna = 0;

            if (cedulaUsuario == null)
            {

                nombresProyecto = controladoraEjecucionPruebas.consultarNombresProyectos();
                if (nombresProyecto != null && nombresProyecto.Rows.Count > 0)
                {
                    numDatos = nombresProyecto.Rows.Count;
                }
            }
            else
            {
                nombresProyecto = controladoraEjecucionPruebas.consultarProyectosLider(cedulaUsuario);
                if (nombresProyecto == null || nombresProyecto.Rows.Count == 0)
                {
                    nombresProyecto = controladoraEjecucionPruebas.consultarProyectosDeUsuario(cedulaUsuario);
                }

                numDatos = nombresProyecto.Rows.Count;

            }


            if (numDatos > 0)
            {
                datos = new Object[numDatos + 1];

                for (int i = 0; i < numDatos; ++i)
                {
                    foreach (DataColumn column in nombresProyecto.Columns)
                    {
                        if (numColumna == 1)
                        {

                            nombres_id_proyectos.Add(nombre, nombresProyecto.Rows[i][1].ToString());
                            id_nombres_proyectos.Add(nombresProyecto.Rows[i][1].ToString(), nombre);


                        }
                        else
                        {
                            nombre = nombresProyecto.Rows[i][0].ToString();
                        }

                        ++numColumna;
                    }

                    datos[indiceProyecto] = nombre;
                    ++indiceProyecto;
                    numColumna = 0;
                    nombre = "";
                }
                datos[0] = "Seleccione";
                this.comboProyecto.DataSource = datos;
                this.comboProyecto.DataBind();
                Session["vectorIdProyectos"] = nombres_id_proyectos;
                Session["vectorIdNombres"] = id_nombres_proyectos;
            }
            else
            {
                datos = new Object[1];
                datos[0] = "Seleccione";
                this.comboProyecto.DataSource = datos;
                this.comboProyecto.DataBind();
            }

            UpdateProyectoDiseno.Update();
        }

        protected void llenarComboRecursos()
        {

            Dictionary<string, string> cedulasRepresentantes = new Dictionary<string, string>();
            Dictionary<string, string> cedulasNombreRepresentantes = new Dictionary<string, string>();
            int id = -1;
            DataTable Recursos;
            Object[] datos;
            int numDatos = 0;


            if (this.comboProyecto.Text.Equals("Seleccione") == false)
            {

                id = controladoraEjecucionPruebas.obtenerIdProyecto(this.comboProyecto.Text);
                Recursos = controladoraEjecucionPruebas.consultarMiembrosDeProyecto(id.ToString());
                numDatos = Recursos.Rows.Count;

                string nombre = "";
                int numColumna = 0;
                int indiceResponsables = 1;
                this.comboResponsable.Items.Clear();

                if (Recursos != null && Recursos.Rows.Count >= 1) //bloque para consultar miembros
                {
                    numDatos = Recursos.Rows.Count;
                    datos = new Object[numDatos + 2];

                    for (int i = 0; i < Recursos.Rows.Count; ++i)
                    {


                        foreach (DataColumn column in Recursos.Columns)
                        {
                            if (numColumna == 4)
                            {

                                cedulasRepresentantes.Add(nombre, Recursos.Rows[i][numColumna].ToString());
                                cedulasNombreRepresentantes.Add(Recursos.Rows[i][numColumna].ToString(), nombre);

                            }
                            else
                            {
                                nombre = Recursos.Rows[i][0].ToString() + " " + Recursos.Rows[i][1].ToString();
                            }

                            ++numColumna;
                        }

                        datos[indiceResponsables] = nombre;
                        ++indiceResponsables;
                        numColumna = 0;
                        nombre = "";

                    }

                    numColumna = 0;
                    Recursos = controladoraEjecucionPruebas.consultarLider(id); // consultar lider

                    if (Recursos != null && Recursos.Rows.Count == 1)
                    {

                        foreach (DataColumn column in Recursos.Columns)
                        {
                            if (numColumna == 3)
                            {

                                cedulasRepresentantes.Add(nombre, Recursos.Rows[0][numColumna].ToString());
                                cedulasNombreRepresentantes.Add(Recursos.Rows[0][numColumna].ToString(), nombre);

                            }
                            else
                            {
                                nombre = Recursos.Rows[0][0].ToString() + " " + Recursos.Rows[0][1].ToString();
                            }

                            ++numColumna;
                        }

                        datos[indiceResponsables] = nombre;
                        ++indiceResponsables;
                        nombre = "";

                    }

                    datos[0] = "Seleccione";
                    this.comboResponsable.DataSource = datos;
                    this.comboResponsable.DataBind();
                    Session["vectorCedulasResponsables"] = cedulasRepresentantes;
                    Session["vectorCedulasNombreResponsables"] = cedulasNombreRepresentantes;
                }
                else
                {

                    // en caso que no haya miembros verifica si hay lideres
                    numColumna = 0;
                    Recursos = controladoraEjecucionPruebas.consultarLider(id);
                    indiceResponsables = 1;
                    if (Recursos != null && Recursos.Rows.Count == 1)
                    {

                        numDatos = Recursos.Rows.Count;
                        datos = new Object[numDatos + 1];
                        foreach (DataColumn column in Recursos.Columns)
                        {
                            if (numColumna == 3)
                            {

                                cedulasRepresentantes.Add(nombre, Recursos.Rows[0][numColumna].ToString());
                                cedulasNombreRepresentantes.Add(Recursos.Rows[0][numColumna].ToString(), nombre);

                            }
                            else
                            {
                                nombre = Recursos.Rows[0][0].ToString() + " " + Recursos.Rows[0][1].ToString();
                            }

                            ++numColumna;
                        }

                        datos[indiceResponsables] = nombre;
                        ++indiceResponsables;
                        nombre = "";
                        datos[0] = "Seleccione";
                        Session["vectorCedulasResponsables"] = cedulasRepresentantes;
                        Session["vectorCedulasNombreResponsables"] = cedulasNombreRepresentantes;
                        this.comboResponsable.DataSource = datos;
                        this.comboResponsable.DataBind();
                        Debug.WriteLine("cantidad de items: " + comboResponsable.Items.Count);

                    }
                    else
                    {
                        datos = new Object[1];
                        datos[0] = "Seleccione";
                        this.comboResponsable.DataSource = datos;
                        this.comboResponsable.DataBind();
                    }
                }

            }
            else // si no hay proyecto seleccionado
            {

                datos = new Object[1];
                datos[0] = "Seleccione";
                this.comboResponsable.DataSource = datos;
                this.comboResponsable.DataBind();


            }

            comboResponsableUpdate.Update();


        }

        protected void llenarComboDisenos()
        {
            int idProyecto = (Int32.Parse(Session["idProyectoEjecucion"].ToString()));
            DataTable disenos = controladoraEjecucionPruebas.consultarDisenos(idProyecto);
            comboDedisenos.Clear();

            Object[] datos;
            int numDatos = 0;
            int indice = 1;

            if (disenos.Rows.Count >= 1)
            {
                numDatos = disenos.Rows.Count;
                datos = new Object[numDatos + 1];

                for (int i = 0; i < disenos.Rows.Count; ++i)
                {
                    datos[indice] = disenos.Rows[i][1].ToString();
                    comboDedisenos.Add(Int32.Parse(disenos.Rows[i][0].ToString()));
                    ++indice;
                }

                datos[0] = "Seleccione";
                this.comboDiseño.DataSource = datos;
                this.comboDiseño.DataBind();
            }
            else
            {
                datos = new Object[1];
                datos[0] = "Seleccione";
                this.comboDiseño.DataSource = datos;
                this.comboDiseño.DataBind();
            }

            UpdateProyectoDiseno.Update();

        }

        protected void llenarComboCasos(int idDiseno, bool tipoCombo)
        {
            DataTable casos = controladoraEjecucionPruebas.getCodigosCasos(idDiseno);
            DropDownList comboDeCasos;

            if (tipoCombo == true)
            {
                comboDeCasos = (DropDownList)gridNoConformidades.FooterRow.FindControl("comboCasoPrueba");
            }
            else
            {
                comboDeCasos = comboIdCasoModificar;
            }


            Object[] datos;
            int numDatos = 0;
            int indice = 1;

            if (casos.Rows.Count > 0)
            {
                numDatos = casos.Rows.Count;
                datos = new Object[numDatos + 1];

                for (int i = 0; i < casos.Rows.Count; ++i)
                {
                    datos[indice] = casos.Rows[i][0].ToString();
                    ++indice;
                }

                datos[0] = "Seleccione";
                comboDeCasos.DataSource = datos;
                comboDeCasos.DataBind();

                if (tipoCombo == false)
                {
                    ListItem itemEstablecido = comboDeCasos.Items.FindByText(idCasoTxt);
                    comboDeCasos.SelectedValue = itemEstablecido.Value;
                }

            }
            else
            {
                datos = new Object[1];
                datos[0] = "Seleccione";
                comboDeCasos.DataSource = datos;
                comboDeCasos.DataBind();
            }

            UpdateGridNoConformidades.Update();
        }


        protected void btnOcultarDiseno(object sender, EventArgs e)
        {
            this.panelDiseno.Visible = false;
            this.datosDiseno.Visible = false;
            this.lblDatosDiseño.Visible = false;
            UpdateDatosDiseno.Update();         
        }

        protected void llenarDatosDiseno(int idDiseno, int idProyecto)
        {
           
            DataTable datosDiseno = controladoraEjecucionPruebas.getDatosDiseno(idDiseno);
            DataTable ReqDiseno = controladoraEjecucionPruebas.getReqDiseno(idDiseno, idProyecto);
            DataTable datosReqDiseno;
            string requerimiento = "";
            string idReq = "";

            if (datosDiseno.Rows.Count > 0)
            {
                Debug.WriteLine("estoy en el if de datos ");
                this.txtPropositoDiseño.Text = datosDiseno.Rows[0][0].ToString();
                this.txtTecnicaPrueba.Text = datosDiseno.Rows[0][1].ToString();
                this.txtNivel.Text = datosDiseno.Rows[0][2].ToString();
                this.txtProcedimiento.Text = datosDiseno.Rows[0][3].ToString();

                if (ReqDiseno.Rows.Count > 0)
                {

                    datosReqDiseno = controladoraEjecucionPruebas.getNombreReqDiseno(ReqDiseno, idProyecto);
                    if (datosReqDiseno.Rows.Count > 0)
                    {
                        for (int i = 0; i < datosReqDiseno.Rows.Count; ++i)
                        {
                            idReq = datosReqDiseno.Rows[i][0].ToString();
                            requerimiento = datosReqDiseno.Rows[i][1].ToString();
                            requerimiento = idReq + " " + requerimiento;
                            this.listRequerimientoDisponibles.Items.Add(requerimiento);
                            requerimiento = "";
                            idReq = "";
                        }
                    }
                }

                this.panelDiseno.Visible = true;
                this.datosDiseno.Visible = true;
                this.lblDatosDiseño.Visible = true;

                UpdateDatosDiseno.Update();
            }

        }

        protected void disenoSeleccionado(object sender, EventArgs e)
        {

            if (this.comboDiseño.Text.Equals("Seleccione") == false)
            {
                int idDiseno = comboDedisenos[comboDiseño.SelectedIndex - 1];
                Session["idDisenoEjecucion"] = idDiseno;
                Debug.WriteLine("este es el id en disenoSelecciionado" + idDiseno);
                this.txtPropositoDiseño.Text = "";
                this.txtTecnicaPrueba.Text = "";
                this.txtNivel.Text = "";
                this.txtProcedimiento.Text = "";
                this.listRequerimientoDisponibles.Items.Clear();
                if(casosIniciados == true)
                {
                    llenarComboCasos(idDiseno, true);
                }
            
                llenarDatosDiseno(idDiseno, Int32.Parse(Session["idProyectoEjecucion"].ToString()));
                gridTipoNC_Inicial(idDiseno, true);
            }

            UpdateProyectoDiseno.Update();
            UpdateDatosDiseno.Update();
            UpdateGridNoConformidades.Update();    
        }

        protected void proyectoSeleccionado(object sender, EventArgs e)
        {
            llenarComboRecursos();
            int id = controladoraEjecucionPruebas.obtenerIdProyecto(this.comboProyecto.Text);
            Session["idProyectoEjecucion"] = id;
            llenarComboDisenos();
            this.panelDiseno.Visible = false;
            this.datosDiseno.Visible = false;
            this.lblDatosDiseño.Visible = false;

            gridTipoNC_Inicial(0, false); //Para que inicialize el grid cuando el usuario cambia de proyecto

            UpdateProyectoDiseno.Update();
            UpdateDatosDiseno.Update();
        }

        protected void tipoNCSeleccionado(object sender, EventArgs e)
        {
            string tipoNCSeleccionado = (gridNoConformidades.FooterRow.FindControl("comboTipoNC") as DropDownList).SelectedItem.Value;

            if (tipoNCSeleccionado.Equals("Seleccione") == false)
            {
                Debug.WriteLine("estoy en tipo nc seleccionado");
                DataTable descripcionNC = controladoraEjecucionPruebas.getDescripcionNC(tipoNCSeleccionado);

                if (descripcionNC.Rows.Count > 0)
                {
                    (gridNoConformidades.FooterRow.FindControl("txtDescripcion") as TextBox).Text = descripcionNC.Rows[0][0].ToString();
                }
            }

            UpdateGridNoConformidades.Update();

        }


        protected void tipoNCSeleccionadoModificar(object sender, EventArgs e)
        {
            GridViewRow gvr = gridNoConformidades.Rows[gridNoConformidades.EditIndex];
            DropDownList tipoNCSeleccionado = (gvr.FindControl("dropDownListTipoNC") as DropDownList);

            if (tipoNCSeleccionado.SelectedItem.Value.Equals("Seleccione") == false)
            {
                DataTable descripcionNC = controladoraEjecucionPruebas.getDescripcionNC(tipoNCSeleccionado.SelectedItem.Value);

                if (descripcionNC.Rows.Count > 0)
                {
                    (gvr.FindControl("txtDescripcionEdit") as TextBox).Text = descripcionNC.Rows[0][0].ToString();
                }
            }

            UpdateGridNoConformidades.Update();

        }


        /** Método para mostrar el grid inicial de no conformidades en pantalla.
        * Requiere: no recibe parámetros.
        * Modifica: El grid de no conformidades que se presenta en pantalla.
        * Retorna: No tiene valor de retorno.
        */
        protected void gridTipoNC_Inicial(int idDiseno, bool condicion)
        {
            gridNoConformidades.DataSource = getTablaConDatosIniciales(); // get first initial data
            gridNoConformidades.DataBind();
            GridViewRow gvr = gridNoConformidades.Rows[0];
            gvr.FindControl("lnkModificar").Visible = false;
            gvr.FindControl("lnkEliminar").Visible = false;
            this.lblListaConformidades.Visible = true;
           
            if(condicion == true)
            {
                //Carga del comboBox con los tipos de no conformidades
                llenarComboTipoNC(true);


                //Carga del comboBox con los códigos de los casos de prueba
                llenarComboCasos((Int32.Parse(Session["idDisenoEjecucion"].ToString())), true);

                //Carga del comboBox con los estados de las no conformidades
                llenarComboEstados(true);

                casosIniciados = true;
            }

            UpdateGridNoConformidades.Update();

        }


        protected void btnInsertar_Click(object sender, EventArgs e)
        {
            modo = 1;
            vaciarCampos();
            cambiarEnabled(true, this.btnAceptar);
            cambiarEnabled(true, this.btnCancelar);
            cambiarEnabled(false, this.btnModificar);
            cambiarEnabled(false, this.btnEliminar);
            cambiarEnabled(false, this.btnInsertar);
            habilitarCampos(true);
            cambiarEnabledGridNC(true);

        }

        /*Método para limpiar los textbox
         * Requiere: No requiere parámetros
         * Modifica: Establece la propiedad text de los textbox en "" y limpia los listbox
         * Retorna: no retorna ningún valor
         */
        protected void vaciarCampos()
        {
            txtCalendar.Text = "";
            txtIncidencias.Text = "";
            gridTipoNC_Inicial(0, false);
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

        /*Método para obtener la cédula de un miembro a partir del nombre
        * Requiere: nombre
        * Modifica: el valor de la cédula solicitada.
        * Retorna: la cédula del miembro solicitado.
        */
        protected string obtenerCedula(string nombreMiembro)
        {
            string cedula = "";

            Dictionary<string, string> cedulasMiembros = (Dictionary<string, string>)Session["vectorCedulasResponsables"];

            if (!cedulasMiembros.TryGetValue(nombreMiembro, out cedula)) // Returns true.
            {
                //lblModalTitle.Text = "ERROR";
                //lblModalBody.Text = "Nombre del miembro es inválido.";
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                //upModal.Update();
            }

            return cedula;

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

        /*Método para la acción de aceptar cuando esta en modo de inserción
         * Requiere: No requiere ningún parámetro
         * Modifica: Crea un objeto con los datos obtenidos en la interfaz mediante textbox y 
         * valida que todos los datos se encuentren para la inserción
         * Retorna: No retorna ningún valor
         */
        protected void btnAceptar_Insertar()
        {
            int tipoInsercion = 1;

            if (faltanDatos())
            {
                lblModalTitle.Text = "ERROR";
                lblModalBody.Text = "Debe completar los datos obligatorios para agregar la ejecución.";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                upModal.Update();
                //habilitarCamposInsertar();
            }
            else
            {
                //Se crea el objeto para encapsular los datos de la interfaz para insertar la ejecución de pruebas
                Object[] datosNuevos = new Object[4];
                Debug.Print( this.txtIncidencias.Text);

                datosNuevos[0] = this.txtIncidencias.Text;
                datosNuevos[1] = this.txtCalendar.Text;
                datosNuevos[2] = obtenerCedula(comboResponsable.Text);
                datosNuevos[3] = (Int32.Parse(Session["idDisenoEjecucion"].ToString()));

                //si la ejecución de pruebas se pudo insertar correctamente entra a este if
                if (controladoraEjecucionPruebas.ejecutarAccion(modo, tipoInsercion, datosNuevos, ""))
                {
                    lblModalTitle.Text = "LISTO";
                    lblModalBody.Text = "INSERTÉ CORRECTAMENTE";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                    upModal.Update();
                }
            }
        }

        protected bool faltanDatos()
        {
            bool resultado = false;

            if (txtCalendar.Text == "" || comboResponsable.Text == "Seleccione")
            {
                resultado = true;
            }
            return resultado;
        }


        /*Método para la acción de aceptar cuando esta en modo de modificación
       * Requiere: No requiere ningún parámetro
       * Modifica: Modifica un objeto con los datos obtenidos en la interfaz mediante textbox y 
       * valida que todos los datos se encuentren para la modificaión
       * Retorna: No retorna ningún valor
       */
        protected void btnAceptar_Modificar()
        {
            int tipoModificacion = 1;
            if (faltanDatos())
            {
                lblModalTitle.Text = "ERROR";
                lblModalBody.Text = "Debe completar los datos obligatorios para agregar la ejecución.";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                upModal.Update();
                habilitarCamposModificar();
              
            }
            else
            {
                // BORRAR LA EJECUCION DE PRUEBAS
                // INSERTAR LA EJECUCION DE PUERBAS
            }
        }



        /*Método para llenar y cargar datos en los campos del diseno asociado a la ejecucion de pruebas
        *necesarios para el modificar
        * Requiere: el id del proyecto relacionado al diseno que se quiere modificar
        * Modifica: Cambia el contenido de las cajas de texto de requerimientos diseno y requerimientos proyecto
        * Retorna: no retorna ningún valor
        */
        protected void disenoAsociado(int idDiseno)
        {
            DataTable diseno = controladoraEjecucionPruebas.consultarDiseno(idDiseno);
            if (diseno != null && diseno.Rows.Count >= 1)
            {
                this.txtPropositoDiseño.Text = diseno.Rows[0][1].ToString();
                this.txtCalendar.Text = diseno.Rows[0][2].ToString();
                this.txtProcedimiento.Text = diseno.Rows[0][3].ToString();
                if( (diseno.Rows[0][6].ToString()) != null){
                    this.txtTecnicaPrueba.Text = diseno.Rows[0][6].ToString();
                }
                 if( (diseno.Rows[0][7].ToString()) != null){
                     this.txtNivel.Text = diseno.Rows[0][7].ToString();
                }  
            }
            //UpdateAsociarDesasociarRequerimientos.Update();
            //proyectoUpdate.Update();
        }

        /*Método para habilitar/deshabilitar todos los campos y los botones que permite el modificar, escucha al boton modificar
        * Requiere: object sender, EventArgs e
        * Modifica: Cambia la propiedad Enabled de las cajas y botones
        * Retorna: no retorna ningún valor
        */
        protected void btnModificar_Click(object sender, EventArgs e)
        {
            cambiarEnabled(true, this.btnInsertar);
            cambiarEnabled(false, this.btnEliminar);
            cambiarEnabled(false, this.btnModificar);
            //llenar los txtbox con la table
            cambiarEnabled(true, this.btnAceptar);
            cambiarEnabled(true, this.btnCancelar);
            llenarDatos(Session["idDiseñoS"].ToString()); //LLENA TODOS LOS CAMPOS
            this.disenoAsociado(Int32.Parse(Session["idDiseñoS"].ToString())); //LLENA LOS DATOS DE DISENO
            modo = 2;
            habilitarCamposModificar();
        }


        /*Método para habilitar/deshabilitar todos los campos que permite el modificar
        * Requiere: 
        * Modifica: Cambia la propiedad Enabled de las cajas 
        * Retorna: no retorna ningún valor
        */
        private void habilitarCamposModificar()
        {
            this.comboResponsable.Enabled = true;
            this.txtCalendar.Enabled = true;
            this.calendarFecha.Enabled = true;
            this.txtIncidencias.Enabled = true;
            this.comboProyecto.Enabled = false;
            this.comboDiseño.Enabled = false;
        }


        
        /*Método para llenar los campos de la interfaz con los de la base
         * los de la base Ejecucion_Prueba
         * los de Ejecucion_NC
        * Requiere: El identificador del diseño que se desea consultar.
        * Modifica: Los campos de la interfaz correspondientes a los datos recibidos mediante la clase controladora.
        * Retorna: No retorna ningún valor. 
        */
        public void llenarDatos(string idDiseño)
        {
          
            if (diseno != null && diseno.Rows.Count >= 1)
            {
                DataTable ejecucionPrueba = controladoraEjecucionPruebas.consultarEjecucionPrueba(idDiseno);
                DataTable ejecucionNC = null;
                DataTable datosReqProyecto = null;
                DataTable datosReqDiseno = null;
    

        }

        /** Método para obtener la tabla con los datos iniciales que serán mostrados en pantalla.
        * Requiere: no recibe parámetros.
        * Modifica: El grid de no conformidades que que se presenta en pantalla.
        * Retorna: La tabla que será mostrada mediante el grid de no conformidades en pantalla.
        */
        public DataTable getTablaConDatosIniciales() // this might be your sp for select
        {
            DataTable table = new DataTable();

            table.Columns.Add("TipoNC", typeof(string));
            table.Columns.Add("IdPrueba", typeof(string));
            table.Columns.Add("Descripcion", typeof(string));
            table.Columns.Add("Justificacion", typeof(string));
            table.Columns.Add("Resultados", typeof(string));
            table.Columns.Add("Estado", typeof(string));
            table.Columns.Add("Imagen", typeof(string));

            table.Rows.Add("-", "-", "-", "-", "-", "-", "-");

            return table;
        }

        protected void btnAgregarImagen_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalImagen", "$('#modalImagen').modal();", true);

        }

        /** Método para agregar una nuevo no conformidad en el grid.
        * Modifica: El grid de no conformidades que se presenta en pantalla.
        * Retorna: No tiene valor de retorno.
        */
        protected void btnAgregarNC_Click(object sender, EventArgs e)
        {

            DataTable dt = GetTableWithNoData(); // get select column header only records not required
            DataRow dr;
            GridViewRow gvr;
            int i = 0;



            for (i = 0; i < gridNoConformidades.Rows.Count; ++i)
            {
                gvr = gridNoConformidades.Rows[i];
                Label lblTipoNC = gvr.FindControl("lblTipoNC") as Label;

                if (lblTipoNC.Text != "-")
                {
                    dr = dt.NewRow();

                    Label lblCasoPrueba = gvr.FindControl("lblCasoPrueba") as Label;
                    Label lblDescripcion = gvr.FindControl("lblDescripcion") as Label;
                    Label lblJustificacion = gvr.FindControl("lblJustificacion") as Label;
                    Label lblResultados = gvr.FindControl("lblResultados") as Label;
                    Label lblEstado = gvr.FindControl("lblEstado") as Label;

                    dr[0] = lblTipoNC.Text;
                    dr[1] = lblCasoPrueba.Text;
                    dr[2] = lblDescripcion.Text;
                    dr[3] = lblJustificacion.Text;
                    dr[4] = lblResultados.Text;
                    dr[5] = lblEstado.Text;

                    dt.Rows.Add(dr); // add grid values in to row and add row to the blank table
                }
            }

            dr = dt.NewRow();

            if ((gridNoConformidades.FooterRow.FindControl("comboTipoNC") as DropDownList).SelectedItem.Value == "Seleccione" || (gridNoConformidades.FooterRow.FindControl("comboCasoPrueba") as DropDownList).SelectedItem.Value == "Seleccione" || (gridNoConformidades.FooterRow.FindControl("comboEstado") as DropDownList).SelectedItem.Value == "Seleccione")
            {
                lblModalTitle.Text = "ERROR";
                lblModalBody.Text = "Debe completar los datos obligatorios para agregar la no conformidad.";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                upModal.Update();
            }
            else
            { 

                string comboTipoNC = (gridNoConformidades.FooterRow.FindControl("comboTipoNC") as DropDownList).SelectedItem.Value;
                string idCaso = (gridNoConformidades.FooterRow.FindControl("comboCasoPrueba") as DropDownList).SelectedItem.Value;
                string descripcion = (gridNoConformidades.FooterRow.FindControl("txtDescripcion") as TextBox).Text;
                string justificacion = (gridNoConformidades.FooterRow.FindControl("txtJustificacion") as TextBox).Text;
                string resultados = (gridNoConformidades.FooterRow.FindControl("txtResultados") as TextBox).Text;
                string estado = (gridNoConformidades.FooterRow.FindControl("comboEstado") as DropDownList).SelectedItem.Value;


                dr[0] = comboTipoNC;
                dr[1] = idCaso;
                dr[2] = descripcion;
                dr[3] = justificacion;
                dr[4] = resultados;
                dr[5] = estado;

                dt.Rows.Add(dr); // Agrega las filas


                gridNoConformidades.DataSource = dt;
                gridNoConformidades.DataBind();


                //Carga del comboBox con los tipos de no conformidades
                llenarComboTipoNC(true);


                //Carga del comboBox con los códigos de los casos de prueba
                llenarComboCasos((Int32.Parse(Session["idDisenoEjecucion"].ToString())), true);

                //Carga del comboBox con los estados de las no conformidades
                llenarComboEstados(true);
    
                for (int j = 0; j < gridNoConformidades.Columns.Count; ++j)
                {
                    gridNoConformidades.Columns[j].ItemStyle.Width = 5;
                }
            }

            UpdateGridNoConformidades.Update();
        }


        /** Método para establecer las columnas del grid.
          * Requiere: no recibe parámetros.
          * Modifica: Modifica el grid para que se puedan crear las filas en él
          * Retorna: la estructura de las columnas que tendrá el grid de no conformidades
          */
        public DataTable GetTableWithNoData()
        {
            DataTable table = new DataTable();
            table.Columns.Add("TipoNC", typeof(string));
            table.Columns.Add("IdPrueba", typeof(string));
            table.Columns.Add("Descripcion", typeof(string));
            table.Columns.Add("Justificacion", typeof(string));
            table.Columns.Add("Resultados", typeof(string));
            table.Columns.Add("Estado", typeof(string));
            table.Columns.Add("Imagen", typeof(string));

            return table;

        }


        /** Método para cerrar la sesión abierta de un usuario y dirigirse a la página de inicio.
          * Requiere: recibe el evento cuando se presiona el botón para cerrar sesión.
          * Modifica: Modifica el valor booleano del estado de la sesión
          * Retorna: No retorna ningún valor
          */
        protected void cerrarSesion(object sender, EventArgs e)
        {

            string ced = (string)Session["cedula"];
            Boolean a = controladoraEjecucionPruebas.cerrarSesion(ced);
            Response.Redirect("~/Login.aspx");
        }


        protected void RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && gridNoConformidades.EditIndex == e.Row.RowIndex)
            {
                comboTipoNCModificar = (DropDownList)e.Row.FindControl("dropDownListTipoNC");
                comboIdCasoModificar = (DropDownList)e.Row.FindControl("dropDownListPrueba");
                comboIdEstadoModificar = (DropDownList)e.Row.FindControl("dropDownListEstado");

                //Carga del comboBox con los tipos de no conformidades y selección del valor según el correspondiente al campo editado
                llenarComboTipoNC(false);


                //Carga del comboBox con los códigos de los casos de prueba y selección del valor según el correspondiente al campo editado
                llenarComboCasos((Int32.Parse(Session["idDisenoEjecucion"].ToString())), false);
 

                //Carga del comboBox con los estados de las no conformidades y selección del valor según el correspondiente al campo editado
                llenarComboEstados(false);

            }
        }
        /*Método para obtener la acción que desea realizar el usuario en el grid de no conformidades.
        * Modifica: el grid de no conformidades de acuerdo a la acción que realice el usuario.
        * Retorna: no retorna ningún valor
        */

        protected void gridTiposNC_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            DataTable dt = GetTableWithNoData(); // get select column header only records not required
            DataRow dr;
            GridViewRow gvr;
            int fila;
            int indice = 0;
            if (e.CommandName == "seleccionaModificar")
            {

                fila = ((GridViewRow)((LinkButton)e.CommandSource).NamingContainer).RowIndex;
                gridNoConformidades.EditIndex = fila;

                for (indice = 0; indice < gridNoConformidades.Rows.Count; ++indice)
                {

                    dr = dt.NewRow();
                    gvr = gridNoConformidades.Rows[indice];

                    Label lblTipoNC = gvr.FindControl("lblTipoNC") as Label;
                    Label lblCasoPrueba = gvr.FindControl("lblCasoPrueba") as Label;
                    Label lblDescripcion = gvr.FindControl("lblDescripcion") as Label;
                    Label lblJustificacion = gvr.FindControl("lblJustificacion") as Label;
                    Label lblResultados = gvr.FindControl("lblResultados") as Label;
                    Label lblEstado = gvr.FindControl("lblEstado") as Label;

                    if (indice == fila)
                    {
                        comboTipoNCTxt = lblTipoNC.Text;
                        idCasoTxt = lblCasoPrueba.Text;
                        descripcionTxt = lblDescripcion.Text;
                        justificacionTxt = lblJustificacion.Text;
                        resultadosTxt = lblResultados.Text;
                        estadoTxt = lblEstado.Text;
                    }

                    dr[0] = lblTipoNC.Text;
                    dr[1] = lblCasoPrueba.Text;
                    dr[2] = lblDescripcion.Text;
                    dr[3] = lblJustificacion.Text;
                    dr[4] = lblResultados.Text;
                    dr[5] = lblEstado.Text;
                    dt.Rows.Add(dr); // add grid values in to row and add row to the blank table       
                }

                gvr = gridNoConformidades.Rows[fila];
                gridNoConformidades.DataSource = dt; // bind new datatable to grid
                gridNoConformidades.DataBind();

                //Carga del comboBox con los tipos de no conformidades
                llenarComboTipoNC(true);


                //Carga del comboBox con los códigos de los casos de prueba
                llenarComboCasos((Int32.Parse(Session["idDisenoEjecucion"].ToString())), true);


                //Carga del comboBox con los estados de las no conformidades
                llenarComboEstados(true);


            }
            else if (e.CommandName == "seleccionaAceptar")
            {
                fila = ((GridViewRow)((LinkButton)e.CommandSource).NamingContainer).RowIndex;

                for (indice = 0; indice < gridNoConformidades.Rows.Count; ++indice)
                {
                    if (indice != fila)
                    {
                        dr = dt.NewRow();
                        gvr = gridNoConformidades.Rows[indice];

                        Label lblTipoNC = gvr.FindControl("lblTipoNC") as Label;
                        Label lblCasoPrueba = gvr.FindControl("lblCasoPrueba") as Label;
                        Label lblDescripcion = gvr.FindControl("lblDescripcion") as Label;
                        Label lblJustificacion = gvr.FindControl("lblJustificacion") as Label;
                        Label lblResultados = gvr.FindControl("lblResultados") as Label;
                        Label lblEstado = gvr.FindControl("lblEstado") as Label;

                        dr[0] = lblTipoNC.Text;
                        dr[1] = lblCasoPrueba.Text;
                        dr[2] = lblDescripcion.Text;
                        dr[3] = lblJustificacion.Text;
                        dr[4] = lblResultados.Text;
                        dr[5] = lblEstado.Text;
                        dt.Rows.Add(dr); // add grid values in to row and add row to the blank table   
                    }

                    dr = dt.NewRow();
                    gvr = gridNoConformidades.Rows[fila];

                    string comboTipoNCModificado = (gvr.FindControl("dropDownListTipoNC") as DropDownList).SelectedItem.Value;
                    string idCasoModificado = (gvr.FindControl("dropDownListPrueba") as DropDownList).SelectedItem.Value;
                    string descripcionModificada = (gvr.FindControl("txtDescripcionEdit") as TextBox).Text;
                    string justificacionModificada = (gvr.FindControl("txtJustificacionEdit") as TextBox).Text;
                    string resultadosModificados = (gvr.FindControl("txtResultadosEdit") as TextBox).Text;
                    string estadoModificado = (gvr.FindControl("dropDownListEstado") as DropDownList).SelectedItem.Value;


                    dr[0] = comboTipoNCModificado;
                    dr[1] = idCasoModificado;
                    dr[2] = descripcionModificada;
                    dr[3] = justificacionModificada;
                    dr[4] = resultadosModificados;
                    dr[5] = estadoModificado;

                    dt.Rows.Add(dr); // add grid values in to row and add row to the blank tables
                    gridNoConformidades.EditIndex = -1;

                    gridNoConformidades.DataSource = dt; // bind new datatable to grid
                    gridNoConformidades.DataBind();

                }

                //Carga del comboBox con los tipos de no conformidades
                llenarComboTipoNC(true);


                //Carga del comboBox con los códigos de los casos de prueba
                llenarComboCasos((Int32.Parse(Session["idDisenoEjecucion"].ToString())), true);
    
                //Carga del comboBox con los estados de las no conformidades
                llenarComboEstados(true);

            }
            else if (e.CommandName == "seleccionaCancelar")
            {
                gridNoConformidades.EditIndex = -1;
                fila = ((GridViewRow)((LinkButton)e.CommandSource).NamingContainer).RowIndex;

                for (indice = 0; indice < gridNoConformidades.Rows.Count; ++indice)
                {

                    dr = dt.NewRow();
                    gvr = gridNoConformidades.Rows[indice];

                    if (indice == fila)
                    {
                        dr[0] = comboTipoNCTxt;
                        dr[1] = idCasoTxt;
                        dr[2] = descripcionTxt;
                        dr[3] = justificacionTxt;
                        dr[4] = resultadosTxt;
                        dr[5] = estadoTxt;
                    }
                    else
                    {
                        Label lblTipoNC = gvr.FindControl("lblTipoNC") as Label;
                        Label lblCasoPrueba = gvr.FindControl("lblCasoPrueba") as Label;
                        Label lblDescripcion = gvr.FindControl("lblDescripcion") as Label;
                        Label lblJustificacion = gvr.FindControl("lblJustificacion") as Label;
                        Label lblResultados = gvr.FindControl("lblResultados") as Label;
                        Label lblEstado = gvr.FindControl("lblEstado") as Label;

                        dr[0] = lblTipoNC.Text;
                        dr[1] = lblCasoPrueba.Text;
                        dr[2] = lblDescripcion.Text;
                        dr[3] = lblJustificacion.Text;
                        dr[4] = lblResultados.Text;
                        dr[5] = lblEstado.Text;
                    }


                    dt.Rows.Add(dr); // add grid values in to row and add row to the blank table 
                }

                gridNoConformidades.DataSource = dt; // bind new datatable to grid
                gridNoConformidades.DataBind();

                //Carga del comboBox con los tipos de no conformidades
                llenarComboTipoNC(true);


                //Carga del comboBox con los códigos de los casos de prueba
                llenarComboCasos((Int32.Parse(Session["idDisenoEjecucion"].ToString())), true);


                //Carga del comboBox con los estados de las no conformidades
                llenarComboEstados(true);

            }
            else if (e.CommandName == "seleccionaEliminar")
            {
                filaEliminar = ((GridViewRow)((LinkButton)e.CommandSource).NamingContainer).RowIndex;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalConfirmar", "$('#modalConfirmar').modal();", true);

                //Carga del comboBox con los tipos de no conformidades
                llenarComboTipoNC(true);

                //Carga del comboBox con los códigos de los casos de prueba
                llenarComboCasos((Int32.Parse(Session["idDisenoEjecucion"].ToString())), true);
     

                //Carga del comboBox con los estados de las no conformidades
                llenarComboEstados(true);

            }


            /*     else if (e.CommandName.Equals("cargarImagen"))
                 {
                     lblResult.Visible = false;
                     System.Text.StringBuilder sb = new System.Text.StringBuilder();
                     sb.Append(@"<script type='text/javascript'>");
                     sb.Append("$('#editModal').modal('show');");
                     sb.Append(@"</script>");
                     ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditModalScript", sb.ToString(), false);

                 } */
        }

        /*Método para la acción de aceptar cuando esta en modo de eliminar algún registro del grid de no conformidades
         * Requiere: No requiere ningún parámetro
         * Modifica: Elimina el registro especificado del grid de no conformidades
         * Retorna: No retorna ningún valor
         */
        protected void btnAceptarEliminarTipoNC(object sender, EventArgs e)
        {
            DataTable dt = GetTableWithNoData(); // get select column header only records not required
            DataRow dr;
            GridViewRow gvr;
            int indice = 0;

            for (indice = 0; indice < gridNoConformidades.Rows.Count; ++indice)
            {

                if (indice != filaEliminar)
                {
                    dr = dt.NewRow();
                    gvr = gridNoConformidades.Rows[indice];

                    Label lblTipoNC = gvr.FindControl("lblTipoNC") as Label;
                    Label lblCasoPrueba = gvr.FindControl("lblCasoPrueba") as Label;
                    Label lblDescripcion = gvr.FindControl("lblDescripcion") as Label;
                    Label lblJustificacion = gvr.FindControl("lblJustificacion") as Label;
                    Label lblResultados = gvr.FindControl("lblResultados") as Label;
                    Label lblEstado = gvr.FindControl("lblEstado") as Label;

                    dr[0] = lblTipoNC.Text;
                    dr[1] = lblCasoPrueba.Text;
                    dr[2] = lblDescripcion.Text;
                    dr[3] = lblJustificacion.Text;
                    dr[4] = lblResultados.Text;
                    dr[5] = lblEstado.Text;
                    dt.Rows.Add(dr); // add grid values in to row and add row to the blank table       
                }
            }

            if (dt.Rows.Count == 0)
            {
                gridTipoNC_Inicial(Int32.Parse(Session["idDisenoEjecucion"].ToString()), true); 
            }
            else
            {
                gridNoConformidades.DataSource = dt;
                gridNoConformidades.DataBind();
            }
        }

        protected void gridEjecuciones_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

    }
}

