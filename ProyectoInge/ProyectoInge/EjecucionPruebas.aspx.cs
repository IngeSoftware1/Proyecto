using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProyectoInge.App_Code.Capa_de_Control;
using System.Data;
using System.Diagnostics;
using System.IO;

namespace ProyectoInge
{
    public partial class EjecucionPruebas : System.Web.UI.Page
    {
        ControladoraEjecucion controladoraEjecucionPruebas = new ControladoraEjecucion();
        private static string idEjecucionConsultada;


        //Variablaes globales utilizadas para cuando el cliente modifica alguna fila del grid
        private static string comboTipoNCTxt = "";
        private static string idCasoTxt = "";
        private static string descripcionTxt = "";
        private static string justificacionTxt = "";
        private static string estadoTxt = "";
        private static string imageBase64String = "";
        private static string imageExtension = "";
        private static int filaEliminar = 0;
        private static List<int> comboDedisenos = new List<int>();
        private static DropDownList comboTipoNCModificar;
        private static DropDownList comboIdCasoModificar;
        private static DropDownList comboIdEstadoModificar;
        private static bool casosIniciados = false;
        private int modo = 1;
        private static string imagenMostrar = "";
        object[] imagenes = new object[100];
        private static byte[] imagen = null;
        private static string base64String = "";                //String global para la columna invisible del grid con la imagen
        private static string extensionImagen = "";             //String global para la columna invisible de la extensión de la imagen en el grid 
        private static int filaConsultada = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["cedula"] == null)
            {
                Response.Redirect("~/Login.aspx");

            }

            if (!IsPostBack)
            {
                //  gridTipoNC_Inicial();
                ponerNombreDeUsuarioLogueado();
                cambiarEnabled(false, this.btnModificar);
                cambiarEnabled(false, this.btnEliminar);
                cambiarEnabled(false, this.btnAceptar);
                cambiarEnabled(false, this.btnCancelar);
                UpdateBotonesIMEC.Update();
                UpdateBotonesAceptarCancelar.Update();
                if (Session["perfil"].ToString().Equals("Administrador"))
                {
                    llenarComboProyecto(null);
                    this.comboDiseño.Enabled = false;
                    this.comboResponsable.Enabled = false;
                    gridTipoNC_Inicial(0, false);
                    habilitarCampos(false);
                    cambiarEnabledGridNC(false);
                    UpdateProyectoDiseno.Update();
                    llenarGrid(null);
                }
                else
                {
                    llenarComboProyecto(Session["cedula"].ToString());
                    this.comboDiseño.Enabled = false;
                    this.comboResponsable.Enabled = false;
                    gridTipoNC_Inicial(0, false);
                    habilitarCampos(false);
                    cambiarEnabledGridNC(false);
                    UpdateProyectoDiseno.Update();
                    llenarGrid(Session["cedula"].ToString());
                }

            }

        }


        /*Metodo para poner el nombre completo del usuario logueado en ese momento
        *Requiere: nada
        *Modifica: el nombre de la persona logueado en un momento determinado en la ventana de RecursosHumanos
        *Retorna: no retorna ningún valor*/
        protected void ponerNombreDeUsuarioLogueado()
        {
            DataTable datosFilaFuncionario = controladoraEjecucionPruebas.consultarRH(Session["cedula"].ToString());
            if (datosFilaFuncionario.Rows.Count == 1)
            {
                string nombreCompletoUsuarioLogueado = datosFilaFuncionario.Rows[0][1].ToString() + " " + datosFilaFuncionario.Rows[0][2].ToString() + " " + datosFilaFuncionario.Rows[0][3].ToString();
                this.lblLogueado.Text = nombreCompletoUsuarioLogueado;
            }
        }


        protected void habilitarCampos(bool condicion)
        {
            txtIncidencias.Enabled = condicion;
            this.comboProyecto.Enabled = condicion;
            UpdateIncidencias.Update();
        }

        protected void cambiarEnabledGridNC(bool condicion)
        {

            (gridNoConformidades.FooterRow.FindControl("btnAgregarNC") as LinkButton).Enabled = condicion;
            (gridNoConformidades.FooterRow.FindControl("comboTipoNC") as DropDownList).Enabled = condicion;
            (gridNoConformidades.FooterRow.FindControl("comboCasoPrueba") as DropDownList).Enabled = condicion;
            (gridNoConformidades.FooterRow.FindControl("txtDescripcion") as TextBox).Enabled = condicion;
            (gridNoConformidades.FooterRow.FindControl("txtJustificacion") as TextBox).Enabled = condicion;
            (gridNoConformidades.FooterRow.FindControl("comboEstado") as DropDownList).Enabled = condicion;
            (gridNoConformidades.FooterRow.FindControl("lnkCargarImagenFoot") as LinkButton).Enabled = condicion;
        }

        /*Método para crear la acción de eliminar una ejecución de pruebas con sus no conformidades
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
            habilitarCampos(false);

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalConfirmar", "$('#modalConfirmar').modal();", true);
            upModal.Update();

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

            int numDatos = 0;
            Object[] datos;

            if (tiposNC.Rows.Count >= 1)
            {

                numDatos = 0;

                for (int i = 0; i < tiposNC.Rows.Count; ++i)
                {
                    if (tiposNC.Rows[i][0].ToString() != "Seleccione")
                    {
                        numDatos++;
                    }
                }

                datos = new Object[numDatos + 1];
                int contador = 0;

                for (int i = 0; i < tiposNC.Rows.Count; ++i)
                {
                    if (tiposNC.Rows[i][0].ToString() != "Seleccione")
                    {
                        datos[contador + 1] = tiposNC.Rows[i][0].ToString();
                        contador++;
                    }
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

            int numDatos = 0;
            Object[] datos;

            if (estados.Rows.Count >= 1)
            {

                for (int i = 0; i < estados.Rows.Count; ++i)
                {
                    if (estados.Rows[i][0].ToString() != "Seleccione")
                    {
                        numDatos++;
                    }
                }

                datos = new Object[numDatos + 1];
                int contador = 0;

                for (int i = 0; i < estados.Rows.Count; ++i)
                {
                    if (estados.Rows[i][0].ToString() != "Seleccione")
                    {
                        datos[contador + 1] = estados.Rows[i][0].ToString();
                        contador++;
                    }
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
                                nombre = Recursos.Rows[i][0].ToString() + " " + Recursos.Rows[i][1].ToString() + " " + Recursos.Rows[i][2].ToString();
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
                                nombre = Recursos.Rows[0][0].ToString() + " " + Recursos.Rows[0][1].ToString() + " " + Recursos.Rows[0][2].ToString();
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
                                nombre = Recursos.Rows[0][0].ToString() + " " + Recursos.Rows[0][1].ToString() + " " + Recursos.Rows[0][2].ToString();
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
                this.comboResponsable.Enabled = false;


            }

            comboResponsableUpdate.Update();


        }

        protected void llenarComboDisenos()
        {
            int idProyecto = 0;
            DataTable disenos;
            comboDedisenos.Clear();

            Object[] datos;
            int numDatos = 0;
            int indice = 1;

            if (this.comboProyecto.Text.Equals("Seleccione") == false)
            {
                idProyecto = (Int32.Parse(Session["idProyectoEjecucion"].ToString()));
                disenos = controladoraEjecucionPruebas.consultarDisenosCasos(idProyecto);
                if (disenos.Rows.Count >= 1)
                {
                    cambiarEnabledGridNC(false);
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
                    this.comboDiseño.Enabled = false;
                }
            }
            else
            {

                datos = new Object[1];
                datos[0] = "Seleccione";
                this.comboDiseño.DataSource = datos;
                this.comboDiseño.DataBind();
                this.comboDiseño.Enabled = false;

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
                if (casosIniciados == true)
                {
                    llenarComboCasos(idDiseno, true);
                }

                cambiarEnabledGridNC(true);
                llenarDatosDiseno(idDiseno, Int32.Parse(Session["idProyectoEjecucion"].ToString()));
                gridTipoNC_Inicial(idDiseno, true);
            }

            UpdateProyectoDiseno.Update();
            UpdateDatosDiseno.Update();
            UpdateGridNoConformidades.Update();
        }

        protected void proyectoSeleccionado(object sender, EventArgs e)
        {
            if (this.comboProyecto.Text != "Seleccione")
            {
                this.comboDiseño.Enabled = true;
                this.comboResponsable.Enabled = true;
                llenarComboRecursos();
                int id = controladoraEjecucionPruebas.obtenerIdProyecto(this.comboProyecto.Text);
                Session["idProyectoEjecucion"] = id;
                llenarComboDisenos();
                this.panelDiseno.Visible = false;
                this.datosDiseno.Visible = false;
                this.lblDatosDiseño.Visible = false;

                gridTipoNC_Inicial(0, false); //Para que inicialize el grid cuando el usuario cambia de proyecto
                cambiarEnabledGridNC(false);
            }
            else
            {

                this.comboDiseño.Enabled = false;
                this.comboResponsable.Enabled = false;
                llenarComboRecursos();
                llenarComboDisenos();
                gridTipoNC_Inicial(0, false); //Para que inicialize el grid cuando el usuario cambia de proyecto
                cambiarEnabledGridNC(false);

            }

            comboResponsableUpdate.Update();
            UpdateProyectoDiseno.Update();
            UpdateDatosDiseno.Update();
        }

        protected void tipoNCSeleccionado(object sender, EventArgs e)
        {
            string tipoNCSeleccionado = (gridNoConformidades.FooterRow.FindControl("comboTipoNC") as DropDownList).SelectedItem.Value;

            if (tipoNCSeleccionado.Equals("Seleccione") == false)
            {
                DataTable descripcionNC = controladoraEjecucionPruebas.getDescripcionNC(tipoNCSeleccionado);

                if (descripcionNC.Rows.Count > 0)
                {
                    (gridNoConformidades.FooterRow.FindControl("txtDescripcion") as TextBox).Text = descripcionNC.Rows[0][0].ToString();
                }
            }
            else
            {

                (gridNoConformidades.FooterRow.FindControl("txtDescripcion") as TextBox).Text = "-";

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

            if (condicion == true)
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
            if (Session["perfil"].ToString().Equals("Administrador"))
            {
                llenarComboProyecto(null);
            }
            else
            {
                llenarComboProyecto(Session["cedula"].ToString());
            }

            llenarComboDisenos();
            llenarComboRecursos();
            Debug.Print("Estoy en la acción del botón insertar y mi modo es " + modo);
            UpdateBotonesIMEC.Update();
            UpdateBotonesAceptarCancelar.Update();

        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            vaciarCampos();
            cambiarEnabled(false, this.btnModificar);
            cambiarEnabled(false, this.btnEliminar);
            cambiarEnabled(false, this.btnAceptar);
            cambiarEnabled(false, this.btnCancelar);
            cambiarEnabled(true, this.btnInsertar);
            if (Session["perfil"].ToString().Equals("Administrador"))
            {
                llenarComboProyecto(null);
                this.comboDiseño.Enabled = false;
                this.comboResponsable.Enabled = false;
                llenarComboDisenos();
                llenarComboRecursos();
                gridTipoNC_Inicial(0, false);
                habilitarCampos(false);
                cambiarEnabledGridNC(false);
                UpdateProyectoDiseno.Update();
                llenarGrid(null);
            }
            else
            {
                llenarComboProyecto(Session["cedula"].ToString());
                this.comboDiseño.Enabled = false;
                this.comboResponsable.Enabled = false;
                llenarComboDisenos();
                llenarComboRecursos();
                gridTipoNC_Inicial(0, false);
                habilitarCampos(false);
                cambiarEnabledGridNC(false);
                UpdateProyectoDiseno.Update();
                llenarGrid(Session["cedula"].ToString());
            }

            UpdateBotonesIMEC.Update();
            UpdateBotonesAceptarCancelar.Update();
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
            cambiarEnabledGridNC(false);
            UpdatePanelCalendario.Update();
            UpdateIncidencias.Update();
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
                case 3:
                    {
                        btnAceptar_Eliminar();
                    }
                    break;

            }

            UpdateBotonesIMEC.Update();
            UpdateBotonesAceptarCancelar.Update();

        }

        /**/
        public void btnAceptar_Eliminar()
        {

            int tipoEliminar = 1;
            modo = 3;

            //Aquí se eliminan las no conformidades
            if (controladoraEjecucionPruebas.ejecutarAccion(modo, tipoEliminar, null, idEjecucionConsultada))
            {
                //Aquí se elimina la ejecución de pruebas
                tipoEliminar = 2;
                if (controladoraEjecucionPruebas.ejecutarAccion(modo, tipoEliminar, null, idEjecucionConsultada))
                {
                    lblModalTitle.Text = "";
                    lblModalBody.Text = "La ejecución de pruebas y sus no conformidades fueron eliminadas correctamente.";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                    upModal.Update();
                }
                else
                {
                    lblModalTitle.Text = "ERROR";
                    lblModalBody.Text = "No se pudo eliminar este diseño.";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                    upModal.Update();
                }

            }
            else
            {
                lblModalTitle.Text = "ERROR";
                lblModalBody.Text = "No se pudieron eliminar las no conformidades asociadas a este diseño.";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                upModal.Update();
            }

            //Aqui se debe limpiar toda la pantalla para que quede como al inicio o si esta vara hace de nuevo el pageload
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
                Debug.Print(this.txtIncidencias.Text);

                datosNuevos[0] = this.txtIncidencias.Text;
                datosNuevos[1] = this.txtCalendar.Text;
                datosNuevos[2] = obtenerCedula(comboResponsable.Text);
                datosNuevos[3] = (Int32.Parse(Session["idDisenoEjecucion"].ToString()));

                //si la ejecución de pruebas se pudo insertar correctamente entra a este if
                if (controladoraEjecucionPruebas.ejecutarAccion(modo, tipoInsercion, datosNuevos, ""))
                {
                    int ejecucion = controladoraEjecucionPruebas.obtenerIdEjecucionRecienCreado();
                    guardarNoConformidades(ejecucion);

                    lblModalTitle.Text = "";
                    lblModalBody.Text = "Nueva ejecución con sus no conformidades creada con éxito";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                    upModal.Update();
                }
                else
                {

                    lblModalTitle.Text = "ERROR";
                    lblModalBody.Text = "La ejecución ya se encuentra en el sistema.";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                    upModal.Update();
                }
            }

            UpdateBotonesIMEC.Update();
            UpdateBotonesAceptarCancelar.Update();
        }

        /*
         * Guarda todo lo del grid de no conformidades
         */
        protected void guardarNoConformidades(int idEjecucion)
        {
            int tipoInsercion = 2;
            DataTable dt = GetTableWithNoData();
            DataRow dr;
            GridViewRow gvr;
            int i = 0;

            for (i = 0; i < gridNoConformidades.Rows.Count; ++i)
            {
                gvr = gridNoConformidades.Rows[i];
                Label lblTipoNC = gvr.FindControl("lblTipoNC") as Label;

                if (lblTipoNC.Text != "-")
                {
                    Object[] NuevaNC = new Object[7];
                    NuevaNC[0] = controladoraEjecucionPruebas.consultarIdCasoPrueba((gvr.FindControl("lblCasoPrueba") as Label).Text);
                    NuevaNC[1] = idEjecucion;
                    NuevaNC[2] = (gvr.FindControl("lblTipoNC") as Label).Text;
                    NuevaNC[3] = (gvr.FindControl("lblJustificacion") as Label).Text;
                    NuevaNC[4] = (gvr.FindControl("lblImagenInvisible") as Label).Text;
                    NuevaNC[5] = (gvr.FindControl("lblImagenExtensionInvisible") as Label).Text;
                    NuevaNC[6] = (gvr.FindControl("lblEstado") as Label).Text;

                    Debug.Print("Imagen extension" + (gvr.FindControl("lblImagenExtensionInvisible") as Label).Text);
                    Debug.Print("Imagen" + (gvr.FindControl("lblImagenInvisible") as Label).Text);

                    if (controladoraEjecucionPruebas.ejecutarAccion(modo, tipoInsercion, NuevaNC, ""))
                    {
                    }
                    else
                    {
                        lblModalTitle.Text = "ERROR";
                        lblModalBody.Text = "La no conformidad con el nombre " + (gvr.FindControl("lblTipoNC") as Label).Text + ", con el caso de prueba " + (gvr.FindControl("lblCasoPrueba") as Label).Text + " y el estado " + (gvr.FindControl("lblEstado") as Label).Text + " no se puedo agregar al sistema.";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                        upModal.Update();
                    }
                }
            }
        }

        protected bool faltanDatos()
        {
            bool resultado = false;

            if (txtCalendar.Text == "" || comboResponsable.Text == "Seleccione" || comboDiseño.Text == "Seleccione" || comboProyecto.Text == "Seleccione")
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
            Response.Write("ENTROOO");
            modo = 2;
            if (faltanDatos())
            {
                lblModalTitle.Text = "ERROR";
                lblModalBody.Text = "Debe tener todos los cmapos obligatorios para poder modificar la ejecucion";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                upModal.Update();
                // habilitarCamposModificar();
            }
            else
            {
                if (controladoraEjecucionPruebas.ejecutarAccion(3, 1, null, idEjecucionConsultada))//Se manda con 3 para eliminar, la accion 2 para modificar la t Req D                            
                {
                    //Aquí se elimina la ejecución de pruebas
                    int tipoEliminar = 2;
                    if (controladoraEjecucionPruebas.ejecutarAccion(3, tipoEliminar, null, idEjecucionConsultada))
                    {
                        lblModalTitle.Text = "";
                        lblModalBody.Text = "La ejecución de pruebas y sus no conformidades fueron eliminadas correctamente.";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                        upModal.Update();

                        //agregar aqui 
                        int tipoInsercion = 1;
                        //Se crea el objeto para encapsular los datos de la interfaz para insertar la ejecución de pruebas
                        Object[] datosNuevos = new Object[4];
                        Debug.Print(this.txtIncidencias.Text);
                        datosNuevos[0] = this.txtIncidencias.Text;
                        datosNuevos[1] = this.txtCalendar.Text;
                        datosNuevos[2] = obtenerCedula(comboResponsable.Text);
                        datosNuevos[3] = (Int32.Parse(Session["idDisenoEjecucion"].ToString()));
                        //si la ejecución de pruebas se pudo insertar correctamente entra a este if
                        if (controladoraEjecucionPruebas.ejecutarAccion(1, tipoInsercion, datosNuevos, ""))
                        {
                            int ejecucion = controladoraEjecucionPruebas.obtenerIdEjecucionRecienCreado();
                            guardarNoConformidades(ejecucion);

                            lblModalTitle.Text = "";
                            lblModalBody.Text = "Nueva ejecución con sus no conformidades creada con éxito";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                            upModal.Update();
                        }
                        else
                        {
                            lblModalTitle.Text = "ERROR";
                            lblModalBody.Text = "La ejecución ya se encuentra en el sistema.";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                            upModal.Update();
                        }


                        //se carga la interfaz de nuevo
                        cambiarEnabled(true, this.btnModificar);
                        cambiarEnabled(true, this.btnEliminar);
                        cambiarEnabled(false, this.btnAceptar);
                        cambiarEnabled(false, this.btnCancelar);
                        cambiarEnabled(true, this.btnInsertar);
                        //llenarGrid(null);
                        // UpdateBotonesIMEC.Update();  //**
                        //UpdateBotonesAceptarCancelar.Update();//***

                        if (Session["perfil"].ToString().Equals("Administrador"))
                        {
                            llenarComboProyecto(null);
                            this.comboDiseño.Enabled = false;
                            this.comboResponsable.Enabled = false;
                            gridTipoNC_Inicial(0, false);
                            habilitarCampos(false);
                            cambiarEnabledGridNC(false);
                            //UpdateProyectoDiseno.Update();
                            llenarGrid(null);
                        }
                        else
                        {
                            llenarComboProyecto(Session["cedula"].ToString());
                            this.comboDiseño.Enabled = false;
                            this.comboResponsable.Enabled = false;
                            gridTipoNC_Inicial(0, false);
                            habilitarCampos(false);
                            cambiarEnabledGridNC(false);
                            // UpdateProyectoDiseno.Update();
                            llenarGrid(Session["cedula"].ToString());
                        }

                        lblModalTitle.Text = "";
                        lblModalBody.Text = "Se modificó la ejecucion de pruebas";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                        upModal.Update();





                    }
                    else
                    {
                        lblModalTitle.Text = "ERROR";
                        lblModalBody.Text = "No se pudo eliminar este diseno.";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                        upModal.Update();
                    }

                }
                else
                {
                    lblModalTitle.Text = "ERROR";
                    lblModalBody.Text = "No se pudieron eliminar las no conformidades asociadas a este diseño.";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                    upModal.Update();
                }

                //UpdateBotonesIMEC.Update();  //**
                //UpdateBotonesAceptarCancelar.Update();//***
                // Response.Write("Pudo eliminar la ejecucion de pruebas ");
                //Ahora debería agregarlos de nuevo
                //Se crea el objeto para encapsular los datos de la interfaz para insertar la ejecución de pruebas


                //UpdateBotonesIMEC.Update();
                //UpdateBotonesAceptarCancelar.Update();
            }
            UpdateBotonesIMEC.Update();
            UpdateBotonesAceptarCancelar.Update();

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
                if ((diseno.Rows[0][6].ToString()) != null)
                {
                    this.txtTecnicaPrueba.Text = diseno.Rows[0][6].ToString();
                }
                if ((diseno.Rows[0][7].ToString()) != null)
                {
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
            modo = 2;
            Debug.Print("estoy en el evento de modificar");
            cambiarEnabled(true, this.btnInsertar);
            cambiarEnabled(false, this.btnEliminar);
            cambiarEnabled(false, this.btnModificar);
            Debug.Print("sigo en modificar");
            //llenar los txtbox con la table
            cambiarEnabled(true, this.btnAceptar);
            cambiarEnabled(true, this.btnCancelar);
            llenarDatos(idEjecucionConsultada);
            // llenarDatos(Session["idEjecuciones"].ToString());
            //this.disenoAsociado(Int32.Parse(Session["idEjecuciones"].ToString()));
            modo = 2;
            habilitarCamposModificar();

            UpdateBotonesIMEC.Update();
            comboResponsableUpdate.Update();
            UpdateIncidencias.Update();
            UpdateBotonesAceptarCancelar.Update();
        }


        /*Método para habilitar/deshabilitar todos los campos que permite el modificar
        * Requiere: 
        * Modifica: Cambia la propiedad Enabled de las cajas 
        * Retorna: no retorna ningún valor
        */
        private void habilitarCamposModificar()
        {
            this.comboResponsable.Enabled = true;
            this.calendarFecha.Enabled = true;
            this.txtIncidencias.Enabled = true;
            this.txtCalendar.Enabled = true;
            this.comboProyecto.Enabled = false;
            this.comboDiseño.Enabled = false;
            cambiarEnabledGridNC(true);
            UpdateProyectoDiseno.Update();
            UpdatePanelCalendario.Update();
            comboResponsableUpdate.Update();
            UpdateIncidencias.Update();

        }




        /*Método para llenar los campos de la interfaz con los resultados de la consulta.
           * Requiere: El identificador del diseño que se desea consultar.
           * Modifica: Los campos de la interfaz correspondientes a los datos recibidos mediante la clase controladora.
           * Retorna: No retorna ningún valor. 
           */
        public void llenarDatos(string idEjecucion)
        {
            if (idEjecucion != null && idEjecucion.Equals("-") == false)
            {
                ListItem responsable;
                ListItem diseno;
                ListItem proyecto;
                idEjecucionConsultada = idEjecucion;
                string nombreDiseno = "";
                string nombreProyecto = "";
                string nombreResponsable = "";
                string imagenNC = "";

                DataRow filaCasoEjecutado;
                GridViewRow gvr = gridEjecuciones.Rows[filaConsultada];
                DataTable gridCasoEjecutado = GetTableWithNoData(); // Se obtiene el esquema del grid
                DataTable datosFilaEjecucion = controladoraEjecucionPruebas.consultarEjecucionPrueba(Int32.Parse(idEjecucion)); //Se obtienen los datos del diseño
                DataTable casoEjecutado = null;
                DataTable datosCasos = null;
                int tamDatosCasos = 0;
                int contadorFilas = 0;
                int indiceColumnas = 0;

                // cargar campos de ejecucion de pruebas 
                if (datosFilaEjecucion != null && datosFilaEjecucion.Rows.Count > 0)
                {
                    Response.Write(datosFilaEjecucion.Rows[0][1].ToString());
                    this.txtIncidencias.Text = datosFilaEjecucion.Rows[0][1].ToString();
                    this.txtCalendar.Text = datosFilaEjecucion.Rows[0][2].ToString();

                    //Se actualiza el combo de proyecto con el dato específico
                    nombreProyecto = (gvr.FindControl("lblProyecto") as Label).Text;
                    if (this.comboProyecto.Items.FindByText(nombreProyecto) != null)
                    {
                        proyecto = this.comboProyecto.Items.FindByText(nombreProyecto);
                        this.comboProyecto.SelectedValue = proyecto.Value;
                    }

                    //Se actualiza el combo de diseno con el dato específico
                    Session["idProyectoEjecucion"] = (gvr.FindControl("lblIDProyecto") as Label).Text;
                    llenarComboDisenos();
                    this.comboDiseño.Enabled = false;
                    nombreDiseno = (gvr.FindControl("lblDiseno") as Label).Text;
                    if (this.comboDiseño.Items.FindByText(nombreDiseno) != null)
                    {
                        diseno = this.comboDiseño.Items.FindByText(nombreDiseno);
                        this.comboDiseño.SelectedValue = diseno.Value;
                    }

                    UpdateProyectoDiseno.Update();

                    //Se actualiza el combo de responsable con el dato específico
                    llenarComboRecursos();
                    nombreResponsable = (gvr.FindControl("lblResponsable") as Label).Text;
                    if (this.comboResponsable.Items.FindByText(nombreResponsable) != null)
                    {
                        responsable = this.comboResponsable.Items.FindByText(nombreResponsable);
                        this.comboResponsable.SelectedValue = responsable.Value;
                    }

                    comboResponsableUpdate.Update();
                }

                //consultar No Conformidades
                Response.Write(Int32.Parse(idEjecucion));
                casoEjecutado = controladoraEjecucionPruebas.consultarCasoEjecutado(Int32.Parse(idEjecucion));

                if (casoEjecutado != null && casoEjecutado.Rows.Count > 0)
                {
                    Response.Write("ENTREEEEEEEEE");
                    datosCasos = controladoraEjecucionPruebas.getCodigosCasos(Int32.Parse(casoEjecutado.Rows[0][0].ToString()));
                    for (int i = 0; i < casoEjecutado.Rows.Count; ++i)
                    {
                        filaCasoEjecutado = gridCasoEjecutado.NewRow();
                        foreach (DataColumn column in casoEjecutado.Columns)
                        {
                            if (indiceColumnas == 0)
                            {
                                contadorFilas = 0;
                                tamDatosCasos = datosCasos.Rows.Count;

                                while ((contadorFilas < tamDatosCasos) && (casoEjecutado.Rows[i][column].ToString() != datosCasos.Rows[contadorFilas][1].ToString()))
                                {
                                    contadorFilas++;
                                }
                                if ((contadorFilas < tamDatosCasos))
                                {
                                    if (casoEjecutado.Rows[i][column].ToString() == datosCasos.Rows[contadorFilas][1].ToString())
                                    {
                                        filaCasoEjecutado[1] = datosCasos.Rows[contadorFilas][0].ToString();// Caso prueba
                                        Response.Write("    ");
                                        Response.Write(datosCasos.Rows[contadorFilas][1].ToString());
                                    }
                                }
                            }
                            if (indiceColumnas == 1)
                            {
                                Response.Write("    ");
                                Response.Write(casoEjecutado.Rows[i][column].ToString());
                                filaCasoEjecutado[0] = casoEjecutado.Rows[i][column].ToString(); //tipo caso no conformidad
                            }
                            if (indiceColumnas == 2)
                            {
                                filaCasoEjecutado[3] = casoEjecutado.Rows[i][column].ToString(); //justificación
                            }
                            if (indiceColumnas == 3)
                            {
                                imagen = (byte[])casoEjecutado.Rows[i][3];
                                imagenNC = Convert.ToBase64String(imagen, 0, imagen.Length);
                                filaCasoEjecutado[6] = imagenNC; //Imagen
                            }
                            if (indiceColumnas == 5)
                            {
                                filaCasoEjecutado[4] = casoEjecutado.Rows[i][5].ToString(); //Estado
                            }
                            if (indiceColumnas == 6)
                            {
                                filaCasoEjecutado[2] = casoEjecutado.Rows[i][6].ToString(); //Descripción no conformidad
                            }
                            if (indiceColumnas == 4)
                            {
                                filaCasoEjecutado[7] = casoEjecutado.Rows[i][4].ToString(); //EXTENSION IMAGEN
                            }

                            ++indiceColumnas;
                        }

                        indiceColumnas = 0;
                        contadorFilas = 0;
                        gridCasoEjecutado.Rows.Add(filaCasoEjecutado);
                        imagenNC = " ";
                    }
                }
                else
                {
                    Response.Write("NOOOOOOOO :(  ");
                    filaCasoEjecutado = gridCasoEjecutado.NewRow();
                    filaCasoEjecutado[0] = "-";
                    filaCasoEjecutado[1] = "-";
                    filaCasoEjecutado[2] = "-";
                    filaCasoEjecutado[3] = "-";
                    filaCasoEjecutado[4] = "-";
                    filaCasoEjecutado[5] = '-';
                    gridCasoEjecutado.Rows.Add(filaCasoEjecutado);

                }

                gridNoConformidades.DataSource = gridCasoEjecutado;
                gridNoConformidades.DataBind();
                cambiarEnabledGridNC(false);


                UpdateGridNoConformidades.Update();
            }


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
            table.Columns.Add("Estado", typeof(string));
            table.Columns.Add("Imagen", typeof(string));
            table.Columns.Add("ImagenInvisible", typeof(string));
            table.Columns.Add("ImagenExtensionInvisible", typeof(string));

            table.Rows.Add("-", "-", "-", "-", "-", "-", "-", "-");

            return table;
        }

        protected void btnAgregarImagen_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalImagen", "$('#modalImagen').modal();", true);

        }


        protected void btnEditarImagen_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalImagen", "$('#modalImagen').modal();", true);
        }



        /*Método para la acción de aceptar cuando esta en modo de aceptar la carga de una imagen
     * Requiere: No requiere ningún parámetro
     * Modifica: La variable global imagen que almacena los bytes de la misma
     * Retorna: No retorna ningún valor
     */
        protected void AceptarImagen(object sender, EventArgs e)
        {
            if (FileImage.HasFile)//Si el usuario seleccionó una imagen
            {
                //guarda esta linea en el row

                string filePath = FileImage.PostedFile.FileName;
                string filename = Path.GetFileName(filePath);
                string ext = Path.GetExtension(filename);
                string contenttype = String.Empty;

                Stream fs = FileImage.PostedFile.InputStream;
                BinaryReader br = new BinaryReader(fs);
                imagen = br.ReadBytes((Int32)fs.Length);

                base64String = Convert.ToBase64String(imagen, 0, imagen.Length);

                Debug.WriteLine("esto tiene el base " + base64String);

                //   image1.ImageUrl = "data:image/png;base64," + base64String;
                //  imagenMostrada.ImageUrl = "data:image/png;base64," + base64String; 
                //    Debug.WriteLine("este es el url" + image1.ImageUrl);
                //  imagenMostrada.ImageUrl = "data:image/png;base64," + base64String;



                //Se obtiene la extension de la imagen con esta línea de código
                string extension = Path.GetExtension(Path.GetFileName(FileImage.PostedFile.FileName));
                extensionImagen = extension;



                ////De acuerdo al tipo de imagen se carga con extension distinta en el image control
                //switch (extension)
                //{
                //    case ".JPEG":
                //        ImagePreview.ImageUrl = "data:image/JPEG;base64," + base64String;
                //        break;
                //    case ".jpg":
                //        ImagePreview.ImageUrl = "data:image/jpg;base64," + base64String;
                //        break;
                //    case ".JPG":
                //        ImagePreview.ImageUrl = "data:image/JPG;base64," + base64String;
                //        break;
                //    case ".gif":
                //        ImagePreview.ImageUrl = "data:image/gif;base64," + base64String;
                //        break;
                //    case ".GIF":
                //        ImagePreview.ImageUrl = "data:image/GIF;base64," + base64String;
                //        break;
                //    case ".png":
                //        ImagePreview.ImageUrl = "data:image/png;base64," + base64String;
                //        break;
                //    case ".PNG":
                //        ImagePreview.ImageUrl = "data:image/PNG;base64," + base64String;
                //        break;
                //    case ".jpeg":
                //        ImagePreview.ImageUrl = "data:image/jpeg;base64," + base64String;
                //        break;
                //}

            }
            else
            {
                imagen = null;
                base64String = " ";
                extensionImagen = " ";

            }
        }

        /*     protected void btnVerImagen_Click(object sender, EventArgs e)
             {
                 imagenMostrada.ImageUrl = "data:image/png;base64," + base64String;
                 ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalVerImagen", "$('#modalVerImagen').modal();", true);
                 updateModalImagen.Update();
             //  ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalVerImagen", "$('#modalVerImagen').modal();", true);
             } */



        /*Método para la acción de aceptar cuando esta en modo de detener la carga de una imagen
         * Requiere: No requiere ningún parámetro
         * Modifica: La variable global imagen con un -1 para indicar que no hay imagen para cargar
         * Retorna: No retorna ningún valor
         */
        protected void CancelarImagen(object sender, EventArgs e)
        {
            imagen = null;
            base64String = " ";
            extensionImagen = "";
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
                    Label lblEstado = gvr.FindControl("lblEstado") as Label;
                    Label lblImagen = gvr.FindControl("lblImagenInvisible") as Label;
                    Label lblExtension = gvr.FindControl("lblImagenExtensionInvisible") as Label;

                    dr[0] = lblTipoNC.Text;
                    dr[1] = lblCasoPrueba.Text;
                    dr[2] = lblDescripcion.Text;
                    dr[3] = lblJustificacion.Text;
                    dr[4] = lblEstado.Text;
                    dr[6] = lblImagen.Text;
                    dr[7] = lblExtension.Text;

                    dt.Rows.Add(dr); // add grid values in to row and add row to the blank table
                }
            }

            dr = dt.NewRow();

            if ((gridNoConformidades.FooterRow.FindControl("comboCasoPrueba") as DropDownList).SelectedItem.Value == "Seleccione")
            {
                lblModalTitle.Text = "ERROR";
                lblModalBody.Text = "Debe ingresar un caso de prueba para poder agregar la no conformidad.";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                upModal.Update();
            }
            else
            {

                string comboTipoNC = (gridNoConformidades.FooterRow.FindControl("comboTipoNC") as DropDownList).SelectedItem.Value;
                string idCaso = (gridNoConformidades.FooterRow.FindControl("comboCasoPrueba") as DropDownList).SelectedItem.Value;
                string descripcion = (gridNoConformidades.FooterRow.FindControl("txtDescripcion") as TextBox).Text;
                string justificacion = (gridNoConformidades.FooterRow.FindControl("txtJustificacion") as TextBox).Text;
                string estado = (gridNoConformidades.FooterRow.FindControl("comboEstado") as DropDownList).SelectedItem.Value;

                Debug.WriteLine("esto tiene la imagen a guardar en el foot " + base64String);
                dr[0] = comboTipoNC;
                dr[1] = idCaso;
                dr[2] = descripcion;
                dr[3] = justificacion;
                dr[4] = estado;
                dr[6] = base64String;
                dr[7] = extensionImagen;
                base64String = " ";
                extensionImagen = "";

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
            table.Columns.Add("Estado", typeof(string));
            table.Columns.Add("Imagen", typeof(string));
            table.Columns.Add("ImagenInvisible", typeof(string));
            table.Columns.Add("ImagenExtensionInvisible", typeof(string));

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
                    Label lblEstado = gvr.FindControl("lblEstado") as Label;
                    Label lblImagen = gvr.FindControl("lblImagenInvisible") as Label;
                    Label lblExtension = gvr.FindControl("lblImagenExtensionInvisible") as Label;

                    if (indice == fila)
                    {
                        comboTipoNCTxt = lblTipoNC.Text;
                        idCasoTxt = lblCasoPrueba.Text;
                        descripcionTxt = lblDescripcion.Text;
                        justificacionTxt = lblJustificacion.Text;
                        estadoTxt = lblEstado.Text;
                        imageBase64String = lblImagen.Text;
                        imageExtension = lblExtension.Text;

                    }

                    dr[0] = lblTipoNC.Text;
                    dr[1] = lblCasoPrueba.Text;
                    dr[2] = lblDescripcion.Text;
                    dr[3] = lblJustificacion.Text;
                    dr[4] = lblEstado.Text;
                    dr[6] = lblImagen.Text;
                    dr[7] = lblExtension.Text;


                    Debug.WriteLine("esto contiene imagen en modificar " + indice + lblImagen.Text);

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
                Debug.Print("estoy en aceptar modificacion " + fila);

                Debug.Print("la cantidad de rows en grid aceptar modif es:  " + gridNoConformidades.Rows.Count);
                for (indice = 0; indice < gridNoConformidades.Rows.Count; ++indice)
                {
                    Debug.Print("la cantidad de rows en grid aceptar modif es:  " + gridNoConformidades.Rows.Count);

                    if (indice != fila)
                    {
                        dr = dt.NewRow();
                        gvr = gridNoConformidades.Rows[indice];

                        Label lblTipoNC = gvr.FindControl("lblTipoNC") as Label;
                        Label lblCasoPrueba = gvr.FindControl("lblCasoPrueba") as Label;
                        Label lblDescripcion = gvr.FindControl("lblDescripcion") as Label;
                        Label lblJustificacion = gvr.FindControl("lblJustificacion") as Label;
                        Label lblEstado = gvr.FindControl("lblEstado") as Label;
                        Label lblImagen = gvr.FindControl("lblImagenInvisible") as Label;
                        Label lblExtension = gvr.FindControl("lblImagenExtensionInvisible") as Label;


                        dr[0] = lblTipoNC.Text;
                        dr[1] = lblCasoPrueba.Text;
                        dr[2] = lblDescripcion.Text;
                        dr[3] = lblJustificacion.Text;
                        dr[4] = lblEstado.Text;
                        dr[6] = lblImagen.Text;
                        dr[7] = lblExtension.Text;
                        dt.Rows.Add(dr); // add grid values in to row and add row to the blank table   
                    }
                    else
                    {
                        dr = dt.NewRow();
                        gvr = gridNoConformidades.Rows[fila];

                        string comboTipoNCModificado = (gvr.FindControl("dropDownListTipoNC") as DropDownList).SelectedItem.Value;
                        string idCasoModificado = (gvr.FindControl("dropDownListPrueba") as DropDownList).SelectedItem.Value;
                        string descripcionModificada = (gvr.FindControl("txtDescripcionEdit") as TextBox).Text;
                        string justificacionModificada = (gvr.FindControl("txtJustificacionEdit") as TextBox).Text;
                        string estadoModificado = (gvr.FindControl("dropDownListEstado") as DropDownList).SelectedItem.Value;
                        string imagenModificada = "";
                        string extensionModificada = "";


                        if (base64String == " " || extensionImagen == " ")
                        {

                            imagenModificada = imageBase64String;
                            extensionModificada = imageExtension;
                        }
                        else
                        {
                            imagenModificada = base64String;
                            extensionModificada = extensionImagen;
                        }

                        base64String = "";
                        extensionImagen = "";


                        dr[0] = comboTipoNCModificado;
                        dr[1] = idCasoModificado;
                        dr[2] = descripcionModificada;
                        dr[3] = justificacionModificada;
                        dr[4] = estadoModificado;
                        dr[6] = imagenModificada;
                        dr[7] = extensionModificada;

                        dt.Rows.Add(dr); // add grid values in to row and add row to the blank tables

                    }

                }

                gridNoConformidades.EditIndex = -1;

                gridNoConformidades.DataSource = dt; // bind new datatable to grid
                gridNoConformidades.DataBind();


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
                        dr[4] = estadoTxt;
                        dr[6] = imageBase64String;
                    }
                    else
                    {
                        Label lblTipoNC = gvr.FindControl("lblTipoNC") as Label;
                        Label lblCasoPrueba = gvr.FindControl("lblCasoPrueba") as Label;
                        Label lblDescripcion = gvr.FindControl("lblDescripcion") as Label;
                        Label lblJustificacion = gvr.FindControl("lblJustificacion") as Label;
                        Label lblEstado = gvr.FindControl("lblEstado") as Label;
                        Label lblImagen = gvr.FindControl("lblImagenInvisible") as Label;
                        Label lblExtension = gvr.FindControl("lblImagenExtensionInvisible") as Label;


                        dr[0] = lblTipoNC.Text;
                        dr[1] = lblCasoPrueba.Text;
                        dr[2] = lblDescripcion.Text;
                        dr[3] = lblJustificacion.Text;
                        dr[4] = lblEstado.Text;
                        dr[6] = lblImagen.Text;
                        dr[7] = lblExtension.Text;

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
            else if (e.CommandName == "mostrarImagen")
            {
                fila = ((GridViewRow)((LinkButton)e.CommandSource).NamingContainer).RowIndex;
                gvr = gridNoConformidades.Rows[fila];
                Label lblImagen = gvr.FindControl("lblImagenInvisible") as Label;
                Label lblExtension = gvr.FindControl("lblImagenExtensionInvisible") as Label;


                if (lblImagen.Text == " ")
                {
                    Debug.WriteLine("entre a imagen vacia");
                    imagenMostrada.Visible = false;
                    lblImagenMostrar.Text = "No hay imágenes asociadas a esta no conformidad";
                    lblImagenMostrar.Visible = true;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalVerImagen", "$('#modalVerImagen').modal();", true);
                    updateModalImagen.Update();
                }
                else
                {
                    imagenMostrada.Visible = true;
                    lblImagenMostrar.Visible = false;
                    switch (lblExtension.Text)
                    {
                        case ".JPEG":
                            imagenMostrada.ImageUrl = "data:image/JPEG;base64," + lblImagen.Text;
                            break;
                        case ".jpg":
                            imagenMostrada.ImageUrl = "data:image/jpg;base64," + lblImagen.Text;
                            break;
                        case ".JPG":
                            imagenMostrada.ImageUrl = "data:image/JPG;base64," + lblImagen.Text;
                            break;
                        case ".gif":
                            imagenMostrada.ImageUrl = "data:image/gif;base64," + lblImagen.Text;
                            break;
                        case ".GIF":
                            imagenMostrada.ImageUrl = "data:image/GIF;base64," + lblImagen.Text;
                            break;
                        case ".png":
                            imagenMostrada.ImageUrl = "data:image/png;base64," + lblImagen.Text;
                            break;
                        case ".PNG":
                            imagenMostrada.ImageUrl = "data:image/PNG;base64," + lblImagen.Text;
                            break;
                        case ".jpeg":
                            imagenMostrada.ImageUrl = "data:image/jpeg;base64," + lblImagen.Text;
                            break;
                    }

                    //   imagenMostrada.ImageUrl = "data:image/png;base64," + lblImagen.Text;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalVerImagen", "$('#modalVerImagen').modal();", true);
                    updateModalImagen.Update();
                }

            }
            else if (e.CommandName == "cargarImagen")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalImagen", "$('#modalImagen').modal();", true);
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
                    Label lblEstado = gvr.FindControl("lblEstado") as Label;
                    Label lblImagen = gvr.FindControl("lblImagenInvisible") as Label;

                    dr[0] = lblTipoNC.Text;
                    dr[1] = lblCasoPrueba.Text;
                    dr[2] = lblDescripcion.Text;
                    dr[3] = lblJustificacion.Text;
                    dr[4] = lblEstado.Text;
                    dr[6] = lblImagen.Text;
                    dt.Rows.Add(dr);// add grid values in to row and add row to the blank table       
                }
            }

            if (dt.Rows.Count == 0)
            {
                gridTipoNC_Inicial(Int32.Parse(Session["idDisenoEjecucion"].ToString()), true);
                cambiarEnabledGridNC(true);
            }
            else
            {
                gridNoConformidades.DataSource = dt;
                gridNoConformidades.DataBind();
            }
        }

        /*Metodo que corresponde a las acciones consecuentes al sleccionar una ejecucion de prueba en el grid*/
        protected void gridEjecuciones_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "seleccionarEjecucion")
            {
                LinkButton lnkConsulta = (LinkButton)e.CommandSource;
                idEjecucionConsultada = lnkConsulta.CommandArgument;
                filaConsultada = ((GridViewRow)((LinkButton)e.CommandSource).NamingContainer).RowIndex;
                if (idEjecucionConsultada.Equals("-") == false)
                {
                    Response.Write("ENTREEEE");
                    Response.Write(idEjecucionConsultada);
                    Session["idEjecuciones"] = idEjecucionConsultada;
                    //   controlarCampos(false); ?????
                    llenarDatos(idEjecucionConsultada);
                    cambiarEnabled(true, this.btnModificar);
                    cambiarEnabled(true, this.btnCancelar);
                    cambiarEnabled(true, this.btnEliminar);
                    cambiarEnabled(false, this.btnAceptar);
                    UpdateBotonesIMEC.Update();
                    UpdateBotonesAceptarCancelar.Update();
                    //El unico botón que cambia de acuerdo al perfil es el de eliminar
                    if (Session["perfil"].ToString().Equals("Administrador"))
                    {
                        cambiarEnabled(true, this.btnInsertar);
                        UpdateBotonesIMEC.Update();
                        UpdateBotonesAceptarCancelar.Update();
                    }
                    else
                    {
                        cambiarEnabled(false, this.btnInsertar);
                        UpdateBotonesIMEC.Update();
                        UpdateBotonesAceptarCancelar.Update();
                    }

                }
            }
        }

        protected DataTable getTablaEjecucion()
        {
            DataTable table = new DataTable();
            table.Columns.Add("IdEjecucion", typeof(string));
            table.Columns.Add("Fecha", typeof(string));
            table.Columns.Add("Responsable", typeof(string));
            table.Columns.Add("Diseno", typeof(string));
            table.Columns.Add("Proyecto", typeof(string));
            table.Columns.Add("IDProyecto", typeof(string));

            return table;
        }


        /*Método para llenar el grid de los estados de ejecucion de un administrador o de los que el miembro se encuentre asociado.
       * Requiere: Requiere la cédula del miembro utilizando el sistema en caso de que éste no sea un administrador
       * Modifica: el valor de cada uno de los campos en la interfaz correspondientes a la consulta retornada por la clase controladora.
       * Retorna: no retorna ningún valor
       */

        protected void llenarGrid(string idUsuario)
        {
            Dictionary<string, string> nombreRepresentantesConsultados = new Dictionary<string, string>();
            Dictionary<string, string> nombreProyectosConsultados = new Dictionary<string, string>();
            DataRow filaEjecucion;
            DataTable disenosProyectos;
            DataTable responsables;
            int contadorFilas = 0;
            int tamResponsables = 0;
            string nombreResponsable = "";
            int tamDisenosProyectos = 0;
            int tamProyecto = 0;
            //DataTable dt = crearTablaDisenos();
            DataTable tablaDatosEjecucion = getTablaEjecucion();
            DataTable ejecuciones;
            DataTable idProyectos;
            Object[] datos = new Object[6];
            int indiceColumna = 0;
            if (idUsuario == null) //Significa que el usuario utilizando el sistema es un administrador por lo que se le deben mostrar 
            //todos las ejecuciones
            {
                //Se obtienen todos las ejecuciones pues el administrador es el usuario del sistema
                ejecuciones = controladoraEjecucionPruebas.consultarEjecucionesDePrueba();
                if (ejecuciones.Rows.Count > 0)
                {
                    disenosProyectos = controladoraEjecucionPruebas.consultarNombresIdDisenosProyectos();
                    responsables = controladoraEjecucionPruebas.consultarResponsables();
                    for (int i = 0; i < ejecuciones.Rows.Count; ++i)
                    {
                        filaEjecucion = tablaDatosEjecucion.NewRow();
                        foreach (DataColumn column in ejecuciones.Columns)
                        {
                            if ((indiceColumna == 0 || indiceColumna == 1) && indiceColumna != 2 && indiceColumna != 3)
                            {
                                filaEjecucion[indiceColumna] = ejecuciones.Rows[i][column].ToString();

                            }
                            if (indiceColumna == 3)
                            {
                                contadorFilas = 0;
                                tamDisenosProyectos = disenosProyectos.Rows.Count;
                                while ((contadorFilas < tamDisenosProyectos) && (ejecuciones.Rows[i][column].ToString() != disenosProyectos.Rows[contadorFilas][0].ToString()))
                                {
                                    contadorFilas++;
                                }
                                if (contadorFilas < tamDisenosProyectos)
                                {
                                    if (ejecuciones.Rows[i][column].ToString() == disenosProyectos.Rows[contadorFilas][0].ToString())
                                    {
                                        filaEjecucion[3] = disenosProyectos.Rows[contadorFilas][1].ToString();// proposito de diseno
                                        filaEjecucion[4] = disenosProyectos.Rows[contadorFilas][3].ToString(); //nombre de proyecto
                                        filaEjecucion[5] = disenosProyectos.Rows[contadorFilas][2].ToString(); //id del proyecto

                                    }
                                }
                            }
                            if (indiceColumna == 2)
                            {
                                contadorFilas = 0;
                                tamResponsables = responsables.Rows.Count;
                                Debug.Print("entre a responsables ");
                                Debug.Print("tam de responsables es: " + tamResponsables);
                                while ((contadorFilas < tamResponsables) && (ejecuciones.Rows[i][column].ToString() != responsables.Rows[contadorFilas][0].ToString()))
                                {
                                    contadorFilas++;
                                }
                                if ((contadorFilas < tamResponsables))
                                {
                                    if (ejecuciones.Rows[i][column].ToString() == responsables.Rows[contadorFilas][0].ToString())
                                    {
                                        nombreResponsable = responsables.Rows[contadorFilas][1].ToString() + " " + responsables.Rows[contadorFilas][2].ToString() + " " + responsables.Rows[contadorFilas][3].ToString();
                                        filaEjecucion[2] = nombreResponsable;
                                    }
                                }
                                else
                                {
                                    contadorFilas = 0;
                                    responsables = controladoraEjecucionPruebas.consultarTodosMiembros(); //Se obtienen todos los miembros
                                    tamResponsables = responsables.Rows.Count;

                                    while ((contadorFilas < tamResponsables) && (ejecuciones.Rows[i][column].ToString() != responsables.Rows[contadorFilas][0].ToString()))
                                    {
                                        contadorFilas++;
                                    }

                                    if (contadorFilas < tamResponsables)
                                    {
                                        nombreResponsable = responsables.Rows[contadorFilas][1].ToString() + " " + responsables.Rows[contadorFilas][2].ToString() + " " + responsables.Rows[contadorFilas][3].ToString();
                                        filaEjecucion[2] = nombreResponsable;
                                    }

                                }
                            }
                            ++indiceColumna;
                        }
                        tablaDatosEjecucion.Rows.Add(filaEjecucion);
                        indiceColumna = 0; //Contador para saber el número de columna actual.
                        tamDisenosProyectos = 0;
                        tamResponsables = 0;
                        contadorFilas = 0;
                        nombreResponsable = "";
                    }
                }
                else
                {
                    filaEjecucion = tablaDatosEjecucion.NewRow();
                    filaEjecucion[0] = "-";
                    filaEjecucion[1] = "-";
                    filaEjecucion[2] = "-";
                    filaEjecucion[3] = "-";
                    filaEjecucion[4] = "-";
                    tablaDatosEjecucion.Rows.Add(filaEjecucion); // preguntar larisa
                }
            }
            else  //usuario normal
            {
                //Se obtiene un DataTable con el identificador del o los proyectos en los cuales trabaja el miembro
                idProyectos = controladoraEjecucionPruebas.consultarProyectosAsociados(idUsuario);
                if (idProyectos.Rows.Count > 0)
                {
                    //Se obtienen todos las ejecuciones pues el administrador es el usuario del sistema
                    ejecuciones = controladoraEjecucionPruebas.consultarEjecucionesDePrueba();
                    if (ejecuciones.Rows.Count > 0)
                    {
                        disenosProyectos = controladoraEjecucionPruebas.consultarNombresIdDisenosProyectos();
                        responsables = controladoraEjecucionPruebas.consultarResponsables();
                        for (int i = 0; i < ejecuciones.Rows.Count; ++i)
                        {
                            filaEjecucion = tablaDatosEjecucion.NewRow();

                            foreach (DataColumn column in ejecuciones.Columns)
                            {
                                if ((indiceColumna == 0 || indiceColumna == 1) && indiceColumna != 2 && indiceColumna != 3)
                                {
                                    filaEjecucion[indiceColumna] = ejecuciones.Rows[i][column].ToString();

                                }
                                if (indiceColumna == 3)
                                {
                                    contadorFilas = 0;
                                    tamDisenosProyectos = disenosProyectos.Rows.Count;
                                    //
                                    while ((contadorFilas < tamDisenosProyectos) && (ejecuciones.Rows[i][column].ToString() != disenosProyectos.Rows[contadorFilas][0].ToString()))
                                    {
                                        contadorFilas++;
                                    }
                                    if ((contadorFilas < tamDisenosProyectos))
                                    {
                                        if (ejecuciones.Rows[i][column].ToString() == disenosProyectos.Rows[contadorFilas][0].ToString())
                                        {
                                            contadorFilas = 0;
                                            tamProyecto = idProyectos.Rows.Count;
                                            string idProyecto = disenosProyectos.Rows[contadorFilas][2].ToString(); //id de proyecto
                                            while ((contadorFilas < tamProyecto) && (idProyecto != idProyectos.Rows[contadorFilas][0].ToString()))
                                            {
                                                contadorFilas++;
                                            }
                                            if ((contadorFilas < tamProyecto))
                                            {
                                                if (idProyecto == idProyectos.Rows[contadorFilas][0].ToString())
                                                {
                                                    filaEjecucion[3] = disenosProyectos.Rows[contadorFilas][1].ToString();// proposito de diseno
                                                    filaEjecucion[4] = disenosProyectos.Rows[contadorFilas][3].ToString(); //nombre de proyecto
                                                    tablaDatosEjecucion.Rows.Add(filaEjecucion);
                                                    filaEjecucion[5] = disenosProyectos.Rows[contadorFilas][2].ToString();
                                                }
                                            }

                                        }
                                    }
                                }
                                if (indiceColumna == 2)
                                {
                                    contadorFilas = 0;
                                    tamResponsables = responsables.Rows.Count;
                                    Debug.Print("entre a responsables ");
                                    Debug.Print("tam de responsables es: " + tamResponsables);
                                    while ((contadorFilas < tamResponsables) && (ejecuciones.Rows[i][column].ToString() != responsables.Rows[contadorFilas][0].ToString()))
                                    {
                                        contadorFilas++;
                                    }
                                    if ((contadorFilas < tamResponsables))
                                    {
                                        if (ejecuciones.Rows[i][column].ToString() == responsables.Rows[contadorFilas][0].ToString())
                                        {
                                            nombreResponsable = responsables.Rows[contadorFilas][1].ToString() + " " + responsables.Rows[contadorFilas][2].ToString() + " " + responsables.Rows[contadorFilas][3].ToString();
                                            filaEjecucion[2] = nombreResponsable;
                                        }
                                    }
                                    else
                                    {
                                        contadorFilas = 0;
                                        responsables = controladoraEjecucionPruebas.consultarTodosMiembros(); //Se obtienen todos los miembros
                                        tamResponsables = responsables.Rows.Count;

                                        while ((contadorFilas < tamResponsables) && (ejecuciones.Rows[i][column].ToString() != responsables.Rows[contadorFilas][0].ToString()))
                                        {
                                            contadorFilas++;
                                        }

                                        if (contadorFilas < tamResponsables)
                                        {
                                            nombreResponsable = responsables.Rows[contadorFilas][1].ToString() + " " + responsables.Rows[contadorFilas][2].ToString() + " " + responsables.Rows[contadorFilas][3].ToString();
                                            filaEjecucion[2] = nombreResponsable;
                                        }
                                    }
                                }
                                ++indiceColumna;
                            }

                            indiceColumna = 0; //Contador para saber el número de columna actual.
                            tamDisenosProyectos = 0;
                            tamResponsables = 0;
                            contadorFilas = 0;
                            nombreResponsable = "";
                        }
                        // SESSION??
                    }
                    else
                    {
                        filaEjecucion = tablaDatosEjecucion.NewRow();
                        filaEjecucion[0] = "-";
                        filaEjecucion[1] = "-";
                        filaEjecucion[2] = "-";
                        filaEjecucion[3] = "-";
                        filaEjecucion[4] = "-";
                        tablaDatosEjecucion.Rows.Add(filaEjecucion); // preguntar larisa           
                    }
                }
            }

            this.gridEjecuciones.DataSource = tablaDatosEjecucion;
            this.gridEjecuciones.DataBind();

        }



    }
}

