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
    public partial class ProyectodePruebas : System.Web.UI.Page
    {
        ControladoraRecursos controladoraRH = new ControladoraRecursos();

        private static int modo = 1; //1 insertar, 2 modificar, 3 eliminar

        ControladoraProyecto controladoraProyecto = new ControladoraProyecto();
        private string idProyectoConsultado;
        private string idOficinaConsultda;
        private string nombProyConsultado;
        private string nombOfConsultada;
        Dictionary<string, string> cedulasTodosMiembros = new Dictionary<string, string>();
        Dictionary<string, string> cedulasLideres = new Dictionary<string, string>();
        Dictionary<string, string> nombreLideresConsultados = new Dictionary<string, string>();

        /* Método para actualizar la interfaz de proyectos
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
                llenarComboEstado();
                llenarComboLideres();
            }


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



        /* Método para llenar el comboBox según los tipos de estados de proceso almacenados en la BD
        * Modifica: llena el comboBox con los datos obtenidos de la BD
        * Retorna: no retorna ningún valor */
        protected void llenarComboEstado()
        {
            this.comboEstado.Items.Clear();
            DataTable Estados = controladoraProyecto.consultarEstados();
            int numDatos = Estados.Rows.Count;
            Object[] datos;


            if (Estados.Rows.Count >= 1)
            {
                numDatos = Estados.Rows.Count;
                datos = new Object[numDatos];

                for (int i = 0; i < Estados.Rows.Count; ++i)
                {
                    datos[i] = Estados.Rows[i][0].ToString();
                }

                this.comboEstado.DataSource = datos;
                this.comboEstado.DataBind();
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


        /* Método para llenar el comboBox según los miembros que sean almacenados como líderes en la BD
        * Modifica: llena el comboBox con los datos obtenidos de la BD
        * Retorna: no retorna ningún valor */
        protected void llenarComboLideres()
        {
            this.comboLider.Items.Clear();
            cedulasLideres.Clear();
            DataTable Lideres = controladoraProyecto.consultarLideres();
            int numDatos = Lideres.Rows.Count;
            Object[] datos;
            string nombre = "";
            int numColumna = 0;
            int indiceLideres = 0;


            if (Lideres.Rows.Count >= 1)
            {

                numDatos = Lideres.Rows.Count;
                datos = new Object[numDatos];

                for (int i = 0; i < Lideres.Rows.Count; ++i)
                {
                    foreach (DataColumn column in Lideres.Columns)
                    {
                       
                            if (numColumna == 3)
                            {

                                cedulasLideres.Add(nombre, Lideres.Rows[i][column].ToString());

                            }
                            else
                            {
                                nombre = nombre + " " + Lideres.Rows[i][column].ToString();
                            }

                            ++numColumna;
                        
                    }

              
                    datos[indiceLideres] = nombre;
                    ++indiceLideres;            
                    numColumna = 0; //Contador para saber el número de columna actual.
                    nombre = "";
                }
                
                this.comboLider.DataSource = datos;
                this.comboLider.DataBind();
                Session["vectorCedulasLideres"] = cedulasLideres; //Se le asocia a la variable global el diccionario con las cédulas de los líderes

            }
        }

        /* Método para llenar el listBox con los miembros de la BD
        * Modifica: llena el listoBox con los datos obtenidos de la BD
        * Retorna: no retorna ningún valor */
        protected void cargarMiembrosSinAsignar()
        {

            //Se obtiene un DataTable con los datos de los miembros de un proyecto (su nombre, apellidos y rol)
            DataTable datosMiembros = controladoraProyecto.consultarMiembros();
            string filaMiembro = "";
            int numColumna = 0;
            cedulasTodosMiembros.Clear();
           

            if (datosMiembros.Rows.Count >= 1)
            {

                for (int i = 0; i < datosMiembros.Rows.Count; ++i)
                {

                    foreach (DataColumn column in datosMiembros.Columns)
                    {
                        if (numColumna == 4) //Se utiliza para saber que es el cuarto atributo que corresponde a la cédula
                        {
                            Debug.Write(datosMiembros.Rows[i][column].ToString());
                            cedulasTodosMiembros.Add(filaMiembro, datosMiembros.Rows[i][column].ToString());
                        }
                        else
                        {

                            if (numColumna == 3) //Se utiliza para hacer la separación entre el nombre y el rol
                            {
                                filaMiembro = filaMiembro + ":" + " " + datosMiembros.Rows[i][column].ToString();
                            }
                            else
                            {
                                filaMiembro = filaMiembro + " " + datosMiembros.Rows[i][column].ToString();
                            }
                        }

                        ++numColumna;
                    }


                    if (listMiembrosAgregados.Items.FindByText(filaMiembro) == null)
                    {
                        listMiembrosDisponibles.Items.Add(filaMiembro);
                    }

                    filaMiembro = "";
                    numColumna = 0;

                }
            }

            Session["vectorCedulasMiembros"] = cedulasTodosMiembros;
            //  UpdateDatos.Update();
        }

        /*Método para obtener la cédula de un miembro a partir del nombre
        * Requiere: el nombre del miembro y el tipo de búsqueda, ya sea sobre un líder o sobre un miembro asignado a un proyecto.
        * Modifica: el valor de la cédula solicitada.
        * Retorna: la cédula del miembro solicitado.
        */
        protected string obtenerCedula(string nombreMiembro, bool lider)
        {
            string cedula = "";

            if (lider == false)
            {
                Dictionary<string, string> cedulasMiembros = (Dictionary<string, string>)Session["vectorCedulasMiembros"];

                if (!cedulasMiembros.TryGetValue(nombreMiembro, out cedula)) // Returns true.
                {
                    lblModalTitle.Text = "ERROR";
                    lblModalBody.Text = "Nombre del miembro es inválido.";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                    upModal.Update();
                }
            }
            else
            {
                Dictionary<string, string> cedulasLid = (Dictionary<string, string>)Session["vectorCedulasLideres"];


                if (!cedulasLid.TryGetValue(nombreMiembro, out cedula)) // Returns true.
                {
                    lblModalTitle.Text = "ERROR";
                    lblModalBody.Text = "Nombre del miembro es inválido.";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                    upModal.Update();
                }
            }

            return cedula;

        }


        /*Método para obtener el registro que se desea consulta en el dataGriedViw y mostrar los resultados de la consulta en pantalla.
        * Modifica: el valor del la variable idProyecto con el ID del proyecto que se desea consultar.
        * Retorna: no retorna ningún valor
        */
        protected void gridProyectos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "seleccionarProyecto")
            {

                LinkButton lnkConsulta = (LinkButton)e.CommandSource;
                idProyectoConsultado = lnkConsulta.CommandArgument;
                Session["idProyectoS"] = idProyectoConsultado;

                controlarCampos(false);
                llenarDatos(idProyectoConsultado);
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

                listMiembrosDisponibles.Items.Clear();
            }

        }

        /*
         * Método para poder indicar que se va a generar la operación de insertar.
         */
        protected void btnInsertar_Click(object sender, EventArgs e)
        {
            vaciarCampos();
            controlarCampos(true);
            cargarMiembrosSinAsignar();
            llenarComboEstado();
            llenarComboLideres();

            modo = 1;
            cambiarEnabled(true, this.btnAceptar);
            cambiarEnabled(true, this.btnCancelar);
            cambiarEnabled(false, this.btnModificar);
            cambiarEnabled(false, this.btnEliminar);
            cambiarEnabled(false, this.btnInsertar);
            this.lnkQuitarMiembros.Enabled = true;
            this.lnkAgregarMiembros.Enabled = true;
            this.lnkAgregarRequerimientos.Enabled = true;
            this.lnkQuitarRequerimientos.Enabled = true;
          
        }
        /*Método para preparar la ventana cuando quiera modificar
         * Requiere: No requiere parámetros
         * Modifica: Habilita y deshabilita botones y textbox
         * Retorna: no retorna ningún valor
         */
        protected void btnModificar_Click(object sender, EventArgs e)
        {
            modo = 2;
            cambiarEnabled(true, this.btnAceptar);
            cambiarEnabled(true, this.btnCancelar);
            cambiarEnabled(false, this.btnModificar);
            cambiarEnabled(false, this.btnEliminar);
            cambiarEnabled(false, this.btnInsertar);
            habilitarCamposModificar();
            cargarMiembrosSinAsignar();
            llenarComboEstado();
            llenarComboLideres();
        }

        /*Método que habilita la ventana modificar dependiendo del pefil actual
         * Requiere: No requiere parámetros
         * Modifica: Habilita y deshabilita botones y texbox
         * Retorna: no retorna ningún valor
         */
        private void habilitarCamposModificar()
        {
            //cargarMiembrosSinAsignar();

            this.txtNombreProy.Enabled = true;
            this.comboEstado.Enabled = true;
            this.txtObjetivo.Enabled = true;
            this.comboLider.Enabled = true;
            this.txtnombreOficina.Enabled = true;
            this.txtnombreRep.Enabled = true;
            this.txtApellido1Rep.Enabled = true;
            this.txtApellido2Rep.Enabled = true;
            this.txtTelefonoOficina.Enabled = true;
            this.listTelefonosOficina.Enabled = true;
            this.listMiembrosAgregados.Enabled = true;
            this.listMiembrosDisponibles.Enabled = true;
            this.lnkAgregarMiembros.Enabled = true;
            this.lnkQuitarMiembros.Enabled = true;
            this.lnkNumero.Enabled = true;
            this.lnkQuitar.Enabled = true;
            this.txtNombreReq.Enabled = true;
            this.txtIdReq.Enabled = true;

            if (Session["perfil"].ToString().Equals("Administrador"))
            {
                //Fecha habilitada para administrador;
                this.calendarFecha.Enabled = true;
                this.txtCalendar.Enabled = true;
                this.lnkCalendario.Enabled = true;
            }
            else
            {
                this.calendarFecha.Enabled = false;
                this.txtCalendar.Enabled = false;
                this.lnkCalendario.Enabled = false;
            }
        }

        /*Método para limpiar los textbox
         * Requiere: No requiere parámetros
         * Modifica: Establece la propiedad text de los textbox en "" y limpia los listbox
         * Retorna: no retorna ningún valor
         */
        protected void vaciarCampos()
        {
            this.txtNombreProy.Text = "";
            this.txtCalendar.Text = "";
            this.txtObjetivo.Text = "";
            this.txtnombreOficina.Text = "";
            this.txtnombreRep.Text = "";
            this.txtApellido1Rep.Text = "";
            this.txtApellido2Rep.Text = "";
            this.txtTelefonoOficina.Text = "";
            this.txtCalendar.Text = "";
            this.listTelefonosOficina.Items.Clear();
            this.listMiembrosDisponibles.Items.Clear();
            this.listMiembrosAgregados.Items.Clear();
            this.listRequerimientosAgregados.Items.Clear();

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
            this.txtCalendar.Enabled = condicion;
            this.calendarFecha.Enabled = condicion;
            this.lnkCalendario.Enabled = condicion;
            this.txtnombreOficina.Enabled = condicion;
            this.txtnombreRep.Enabled = condicion;
            this.txtApellido1Rep.Enabled = condicion;
            this.txtApellido2Rep.Enabled = condicion;
            this.txtTelefonoOficina.Enabled = condicion;
            this.comboLider.Enabled = condicion;
            this.lnkNumero.Enabled = condicion;
            this.lnkQuitar.Enabled = condicion;
            this.lnkAgregarMiembros.Enabled = condicion;
            this.lnkQuitarMiembros.Enabled = condicion;
            this.txtCalendar.Enabled = false;
            this.txtNombreReq.Enabled = condicion;
            this.txtIdReq.Enabled = condicion;
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

            vaciarCampos();
            controlarCampos(false);
            cambiarEnabled(false, this.btnModificar);
            cambiarEnabled(false, this.btnEliminar);
            cambiarEnabled(true, this.btnAceptar);
            cambiarEnabled(true, this.btnCancelar);

            llenarComboEstado();
            llenarComboLideres();
            listMiembrosDisponibles.Items.Clear();
            listMiembrosAgregados.Items.Clear();
            listRequerimientosAgregados.Items.Clear();
            this.calendarFecha.Visible = false;
        }

        /*Método para la acción de aceptar modificar
        * Requiere: No requiere ningún parámetro
        * Modifica: Crea un objeto con los datos obtenidos en la interfaz mediante textbox y 
        * valida que todos los datos se encuentren para la modificación
        * Retorna: No retorna ningún valor
        */
        private void btnAceptar_Modificar()
        {
            //Debug.Write(idOficinaConsultda);
            int tipoModificacion = 1;//Va a cambiar la tabla proyecto
            if (faltanDatos())//2 indica los datos que pueden faltar en el modificar
            {
                lblModalTitle.Text = "AVISO";
                lblModalBody.Text = "Para modificar un proyecto debe completar todos los datos habilitados.";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                upModal.Update();
            }
            else
            {
                //Se crea el objeto para encapsular los datos de la interfaz para modificar proyecto
                //los encapsula todos, sea administrador o miembro
                Object[] datosProyecto = new Object[7];
                datosProyecto[0] = this.txtNombreProy.Text;//nombre_proyecto
                datosProyecto[1] = this.txtObjetivo.Text;//obj_general
                datosProyecto[2] = this.txtCalendar.Text;//fecha_asignacion
                datosProyecto[3] = this.comboEstado.Text;//tipo_estado
                datosProyecto[4] = "";//creador
                datosProyecto[5] = obtenerCedula(this.comboLider.Text, true);
                datosProyecto[6] = (string)Session["idOficinaS"];
                String idP = (string)Session["idProyectoS"];
                String idO = (string)Session["idOficinaS"];
                if (controladoraProyecto.ejecutarAccion(modo, tipoModificacion, datosProyecto, idP, ""))
                {

                    //Se crea el objeto para encapsular los datos de la interfaz para modificar oficina usuaria
                    tipoModificacion = 2;//Va a cambiar la oficina usuaria
                    Object[] datosOfUsuaria = new Object[4];
                    datosOfUsuaria[0] = this.txtnombreOficina.Text;//nombre_oficina
                    datosOfUsuaria[1] = this.txtnombreRep.Text;//nombre_rep
                    datosOfUsuaria[2] = this.txtApellido1Rep.Text;//ape1_rep
                    datosOfUsuaria[3] = this.txtApellido2Rep.Text;//ape2_rep

                    if (controladoraProyecto.ejecutarAccion(modo, tipoModificacion, datosOfUsuaria, idO, ""))
                    {
                        //Para modificar los telefonos de la oficina en la oficina usuaria modificar oficina usuaria
                        tipoModificacion = 3;//Va a cambiar los telefonos de la oficina usuaria
                        //Eliminar los telefonos de la of usuaria
                        if (controladoraProyecto.ejecutarAccion(3, tipoModificacion, null, idO, ""))//modo 3 para eliminar, le mando el id de la oficina
                        {
                            //Insertar los telefonos
                            int idOficinaUsuaria = Int32.Parse(idO);
                            bool insertados = guardarTelefonos(idOficinaUsuaria);
                            //Si todo bien
                            if (insertados)
                            {
                                tipoModificacion = 4;//Va a cambiar trabaja_en, eliminará todos
                                if (controladoraProyecto.ejecutarAccion(3, tipoModificacion, null, idP, ""))//lo mando con 3 pars que elimine
                                {
                                    int idProyecto = Int32.Parse(idP);
                                    guardarMiembros(idProyecto);
                                    tipoModificacion=5;
                                    if (modificarRequeriminetos(idProyecto))//Elimina los requerimientos
                                    {
                                        guardarRequerimientos(idProyecto);//este metodo los agrega los nuevos requerimientos
      
                                        controlarCampos(false);
                                        //se carga la interfaz de nuevo
                                        cambiarEnabled(true, this.btnModificar);
                                        cambiarEnabled(true, this.btnEliminar);
                                        cambiarEnabled(false, this.btnAceptar);
                                        cambiarEnabled(false, this.btnCancelar);
                                        cambiarEnabled(true, this.btnInsertar);

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
                                        lblModalTitle.Text = "";
                                        lblModalBody.Text = "Proyecto modificado con éxito.";
                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                                        upModal.Update();
                                        
                                    }
                                    
                                }
                                else
                                {
                                    lblModalTitle.Text = "ERROR";
                                    lblModalBody.Text = "No eliminó trabaja_en.";
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                                    upModal.Update();
                                }
                            }
                            else
                            {
                                lblModalTitle.Text = "ERROR";
                                lblModalBody.Text = "No insertó los teléfonos.";
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                                upModal.Update();
                            }
                        }
                        else
                        {
                            lblModalTitle.Text = "ERROR";
                            lblModalBody.Text = "No eliminó los teléfonos.";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                            upModal.Update();
                        }
                    }
                    else
                    {
                        lblModalTitle.Text = "ERROR";
                        lblModalBody.Text = "No modificó oficina usuaria.";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                        upModal.Update();
                    }
                }
                else
                {
                    lblModalTitle.Text = "ERROR";
                    lblModalBody.Text = "No modificó oficina proyecto.";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                    upModal.Update();
                }
            }
        }

        private bool modificarRequeriminetos(int idProyecto)
        {
            int i = 0;
            int indiceReq = 0;
            int encontroIgual = 0;
            string sigla = "";
            string nombreRequerimiento = "";
            DataTable tablaReq = controladoraProyecto.consultarReqProyecto(idProyecto);

            int tamTabla = tablaReq.Rows.Count;
            foreach (DataRow fila in tablaReq.Rows)//en fila tengo 
            {
                if (fila[1].ToString() != "Dummy")
                {
                    while (i < listRequerimientosAgregados.Items.Count && listRequerimientosAgregados.Items[i].Text.Equals("") == false)
                    {
                        ControladoraDiseno controladoraDiseno = new ControladoraDiseno();

                        indiceReq = 0;
                        sigla = "";
                        nombreRequerimiento = "";

                        while (indiceReq < listRequerimientosAgregados.Items[i].ToString().Count() && listRequerimientosAgregados.Items[i].ToString().ElementAt(indiceReq) != ' ')
                        {
                            ++indiceReq;
                        }
                        sigla = listRequerimientosAgregados.Items[i].ToString().Substring(0, indiceReq);
                        nombreRequerimiento = listRequerimientosAgregados.Items[i].ToString().Substring(indiceReq + 1, listRequerimientosAgregados.Items[i].ToString().Count() - indiceReq - 1);
                        if (fila[1].ToString() == sigla && fila[3].ToString() == nombreRequerimiento)
                        {
                            encontroIgual++;
                        }
                    }

                    //Aqui va a eliminar de la base si lo requiere
                    if (encontroIgual == 0)//significa que no encontro en la lista y debe eliminarlo
                    {
                        controladoraProyecto.eliminarRequerimientosDiseno(fila[1].ToString(),fila[3].ToString(),idProyecto);
                        controladoraProyecto.eliminarRequeriminto(fila[1].ToString(), fila[3].ToString(), idProyecto);
                    }
                }
            }
            return false;
        }

        /*Método para la acción del botón agregar telefonos al listbox
         * Modifica: agrega al listbox el telefono escrito en el textbox de telefono
         * Retorna: no retorna ningún valor
         */
        protected void btnAgregarTelefono(object sender, EventArgs e)
        {

            if (txtTelefonoOficina.Text != "")
            {
                listTelefonosOficina.Items.Add(txtTelefonoOficina.Text);
                txtTelefonoOficina.Text = "";
            }

            if (modo == 1)
            {
                habilitarCamposInsertar();
            }
            else if (modo == 2)
            {
                habilitarCamposModificar();
            }

            Update_Tel.Update();
        }

        /*Método para la acción de eliminar telefonos del listbox
         * Modifica: Elimina el telefono seleccionado del listbox
         * Retorna: no retorna ningún valor
         */
        protected void btnEliminarTelefono(object sender, EventArgs e)
        {
            if (modo == 1 || modo == 2)
            {
                if (listTelefonosOficina.SelectedIndex != -1)
                {
                    listTelefonosOficina.Items.RemoveAt(listTelefonosOficina.SelectedIndex);
                }
            }

            Update_Tel.Update();
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
                lblModalTitle.Text = "AVISO";
                lblModalBody.Text = "Para insertar un nuevo proyecto debe completar todos los datos obligatorios.";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                upModal.Update();
                habilitarCamposInsertar();
            }
            else
            {
                //Se crea el objeto para encapsular los datos de la interfaz para insertar oficina usuaria
                Object[] datosNuevos = new Object[4];
                datosNuevos[0] = this.txtnombreOficina.Text;
                datosNuevos[1] = this.txtnombreRep.Text;
                datosNuevos[2] = this.txtApellido1Rep.Text;
                datosNuevos[3] = this.txtApellido2Rep.Text;

                //si la oficina usuaria se pudo insertar correctamente entra a este if
                if (controladoraProyecto.ejecutarAccion(modo, tipoInsercion, datosNuevos, "", ""))
                {
                    int idOficina = controladoraProyecto.obtenerOficinaAgregada(this.txtnombreOficina.Text);
                    Session["idOficinaS"] = Convert.ToString(idOficina);

                    guardarTelefonos(idOficina);

                    //Ya guardados la oficina usuaria y sus telefonos se guarda el proyecto en el sistema
                    tipoInsercion = 3; //inserción de proyecto tipo 3
                    Object[] nuevoProyecto = new Object[7];
                    nuevoProyecto[0] = this.txtNombreProy.Text;
                    nuevoProyecto[1] = this.txtObjetivo.Text;
                    nuevoProyecto[2] = this.txtCalendar.Text;
                    nuevoProyecto[3] = this.comboEstado.Text;
                    nuevoProyecto[4] = Session["cedula"].ToString();
                    nuevoProyecto[5] = obtenerCedula(this.comboLider.Text, true);
                    nuevoProyecto[6] = idOficina;

                    //El proyecto si se pudo insertar correctamente en la base
                    if (controladoraProyecto.ejecutarAccion(modo, tipoInsercion, nuevoProyecto, "", ""))
                    {
                        //Se guardan los miembros del equipo para el proyecto
                        int idProyecto = controladoraProyecto.obtenerIDconNombreProyecto(txtNombreProy.Text);
                        Session["idProyectoS"] = Convert.ToString(idProyecto);
                        guardarMiembros(idProyecto);
                        guardarRequerimientos(idProyecto);

                        //se carga la interfaz de nuevo
                        controlarCampos(false);
                        cambiarEnabled(true, this.btnModificar);
                        cambiarEnabled(true, this.btnEliminar);
                        cambiarEnabled(false, this.btnAceptar);
                        cambiarEnabled(false, this.btnCancelar);
                        cambiarEnabled(true, this.btnInsertar);
                        llenarGrid(null);

                        lblModalTitle.Text = " ";
                        lblModalBody.Text = "Nuevo proyecto creado con éxito.";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                        upModal.Update();
                   }
                    //El proyecto no se pudo insertar bien por lo cual se borra la oficina usuaria con sus telefonos
                    else
                    {
                        controladoraProyecto.eliminarOficina(idOficina);
                        lblModalTitle.Text = "ERROR";
                        lblModalBody.Text = "Este proyecto ya se encuentra registrado en el sistema.";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                        upModal.Update();
                        habilitarCamposInsertar();
                    }
                }
                //La oficina usuaria no se pudo registrar en la BD
                else
                {
                    lblModalTitle.Text = "ERROR";
                    lblModalBody.Text = "Esta oficina usuaria ya se encuentra registrada en el sistema.";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                    upModal.Update();
                    habilitarCamposInsertar();
                }
            }
        }

        /*Método para  controlar la inserción de el o los teléfonos de una oficina en particular
         * Requiere: no recibe parámetros
         * Modifica: Revisa que efectivamente se esten insertando telefonos en la caja de texto y que no se inserten repetidos
         * Retorna: No retorna ningún valor
         */
        protected bool guardarTelefonos(int idOficina)
        {
            int malas = 0;
            bool resp = false;
            int i = 0;

            while (i < listTelefonosOficina.Items.Count && listTelefonosOficina.Items[i].Text.Equals("") == false)
            {
                //Objeto para guardar los telefonos de la oficina usuaria en el sistema
                Object[] telefonoOficina = new Object[2];
                telefonoOficina[0] = idOficina;
                telefonoOficina[1] = this.listTelefonosOficina.Items[i].Text;
                int tipoInsercion = 2;                              //inserción de tipo 2 es agregar telefonos

                //Se insertó un nuevo telefono para la oficina usuaria
                if (controladoraProyecto.ejecutarAccion(1, tipoInsercion, telefonoOficina, "", ""))//lo mando con 1 porque aquí siempre va a insertar
                {
                }
                //La inserción de un nuevo telefono para una oficina usuaria en la base de datos falló porque ya estaba en la base
                else
                {
                    lblModalTitle.Text = "ERROR";
                    lblModalBody.Text = "El teléfono ya se encuentra asociado en el sistema a esta oficina usuaria.";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                    upModal.Update();
                    habilitarCamposInsertar();
                    malas++;
                }

                i++;
            }
            if (malas == 0)
            {
                resp = true;
            }
            return resp;
        }

        /*Modifica: Método para  guardar los miembros que trabajan en un proyecto
         * Requiere: recibe el id del proyecto
 \       * Retorna: No retorna ningún valor
         */
        protected void guardarMiembros(int idProyecto)
        {
            int i = 0;

            while (i < listMiembrosAgregados.Items.Count && listMiembrosAgregados.Items[i].Text.Equals("") == false)
            {
                //Objeto para guardar los miembros de un equipo de trabajo para un proyecto
                Object[] nuevoMiembro = new Object[2];
                nuevoMiembro[0] = obtenerCedula(this.listMiembrosAgregados.Items[i].Text, false);
                nuevoMiembro[1] = idProyecto;

                int tipoInsercion = 4;                              //inserción de tipo 4 es agregar miembros

                //Se insertó un nuevo miembro de equipo de pruebas
                if (controladoraProyecto.ejecutarAccion(1, tipoInsercion, nuevoMiembro, "", ""))//Esto siempre inserta, por lo que le mandaremos un 1
                {
                }
                //La inserción de un nuevo miembro de equipo de pruebas en la base de datos falló porque ya estaba en la base
                else
                {
                    lblModalTitle.Text = "ERROR";
                    lblModalBody.Text = "El miembro de equipo de pruebas ya se encuentra asociado a este proyecto en el sistema.";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                    upModal.Update();
                    habilitarCamposInsertar();
                }

                i++;
            }
        }

        /*Método para la acción del botón agregar requerimientos al listbox
         * Modifica: agrega al listbox de requerimientos el requerimiento creado
         * Retorna: no retorna ningún valor
         */
        protected void btnAgregarRequerimiento(object sender, EventArgs e)
        {
            if (txtIdReq.Text != "" && txtNombreReq.Text!="" )
            {
                listRequerimientosAgregados.Items.Add(txtIdReq.Text + " " + txtNombreReq.Text);
                txtIdReq.Text = "";
                txtNombreReq.Text = "";
            }

            if (modo == 1)
            {
                habilitarCamposInsertar();
            }
            else if (modo == 2)
            {
                habilitarCamposModificar();
            }

            UpdateRequerimientos.Update();
        }

        /*Método para la acción de eliminar requerimientos del proyecto
         * Modifica: Elimina el requerimiento seleccionado en el listbox
         * Retorna: no retorna ningún valor
         */
        protected void btnEliminarRequerimiento(object sender, EventArgs e)
        {
            if (modo == 1 || modo == 2)
            {
                if (listRequerimientosAgregados.SelectedIndex != -1)
                {
                    listRequerimientosAgregados.Items.RemoveAt(listRequerimientosAgregados.SelectedIndex);
                }
            }

            UpdateRequerimientos.Update();
        }

        public void guardarRequerimientos(int idProyecto)
        {
            int i = 0;
            int indiceReq = 0;
            string sigla = "";
            string nombreRequerimiento = "";

            while (i < listRequerimientosAgregados.Items.Count && listRequerimientosAgregados.Items[i].Text.Equals("") == false)
            {
                ControladoraDiseno controladoraDiseno = new ControladoraDiseno();
               
                /*Aqui hay que partir el listbox*/

                indiceReq = 0;
                sigla = "";
                nombreRequerimiento = "";

                while(indiceReq < listRequerimientosAgregados.Items[i].ToString().Count() && listRequerimientosAgregados.Items[i].ToString().ElementAt(indiceReq) != ' ' )
                {
                    ++indiceReq;
                }
                sigla = listRequerimientosAgregados.Items[i].ToString().Substring(0, indiceReq);
                nombreRequerimiento = listRequerimientosAgregados.Items[i].ToString().Substring(indiceReq + 1, listRequerimientosAgregados.Items[i].ToString().Count()- indiceReq-1);

                //Objeto para guardar los requerimientos de un proyecto
                Object[] nuevoRequerimiento = new Object[4];
                nuevoRequerimiento[0] = sigla;
                nuevoRequerimiento[1] = idProyecto;
                nuevoRequerimiento[2] = nombreRequerimiento;

                int tipoInsercion = 5;                              //inserción de tipo 5 es agregar requerimientos

                //Se insertó un nuevo requerimiento al proyecto
                if (controladoraProyecto.ejecutarAccion(1, tipoInsercion, nuevoRequerimiento, "", ""))//Esto siempre inserta, por lo que le mandaremos un 1
                {
                }
                //La inserción de un nuevo requerimiento en la base de datos falló porque ya estaba en la base
                else
                {
                   
                    lblModalTitle.Text = "ERROR";
                    lblModalBody.Text = "Este requerimiento ya se encuentra asociado al proyecto.";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                    upModal.Update();
                    habilitarCamposInsertar();
                    
                }

                i++;
            }
        }

        /*
         * Método para poder asignar recursos al proyecto respectivo
         */
        protected void btnAgregarMiembro(object sender, EventArgs e)
        {

            if (listMiembrosDisponibles.SelectedIndex != -1)
            {
                listMiembrosAgregados.Items.Add(listMiembrosDisponibles.SelectedValue);
                listMiembrosDisponibles.Items.RemoveAt(listMiembrosDisponibles.SelectedIndex);
            }

            if (modo == 1)
            {
                habilitarCamposInsertar();
            }
            else if (modo == 2)
            {

            }

            UpdateAsociarDesasociarMiembros.Update();
        }

        /*
         * Método para pooder desasociar un recurso a un proyecto, el cambio se refleja en la base y en la interfaz al cambiar la lista de recursos
         * no asignados y asignados.
        */
        protected void btnEliminarMiembro(object sender, EventArgs e)
        {
            if (modo == 1 || modo == 2)
            {
                if (listMiembrosAgregados.SelectedIndex != -1)
                {
                    listMiembrosDisponibles.Items.Add(listMiembrosAgregados.SelectedValue);
                    listMiembrosAgregados.Items.RemoveAt(listMiembrosAgregados.SelectedIndex);
                }
            }

            UpdateAsociarDesasociarMiembros.Update();
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
            cambiarEnabledTel(true, this.lnkAgregarRequerimientos);
            cambiarEnabledTel(true, this.lnkQuitarRequerimientos);
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
            if (this.txtNombreProy.Text == "" || this.txtObjetivo.Text == "" || this.txtCalendar.Text == "" || this.txtnombreOficina.Text =="")
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


        /*Método para crear la acción de eliminar un proyecto
         * Modifica: Cambia la propiedad enabled de botones y cajas de texto,
         * Limpia cajas de texto y coloca los ejemplos de datos donde es necesario
         * Retorna: no retorna ningún valor
         */
        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            string mensaje;
            cambiarEnabled(false, this.btnInsertar);
            cambiarEnabled(false, this.btnModificar);
            cambiarEnabled(false, this.btnAceptar);
            cambiarEnabled(false, this.btnCancelar);
            modo = 3;
            controlarCampos(false);



            if (Session["perfil"].ToString().Equals("Administrador"))
            {
                //lblModalTitle.Text = " ";
                //lblModalBody.Text = "Está seguro que desea eliminar este proyecto?";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalConfirmar", "$('#modalConfirmar').modal();", true);
                upModal.Update();
            }
            else
            {
                lblModalTitle.Text = " ";
                lblModalBody.Text = "Está seguro que desea cambiar el estado del proyecto?";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                upModal.Update();
                //mensaje = "<script>window.alert('Está seguro que desea cambiar el estado del proyecto?');</script>";
            }

        }


        /*Método para la acción de aceptar cuando esta en modo de borrado
         * Requiere: No requiere ningún parámetro
         * Modifica:Elimina un recurso humano si es valido llevar acabo la acción
         * Retorna: No retorna ningún valor
         */
        protected void btnAceptar_Eliminar(object sender, EventArgs e)
        {

            string perfil = Session["perfil"].ToString();
            idProyectoConsultado = Convert.ToString(controladoraProyecto.obtenerIDconNombreProyecto(txtNombreProy.Text));
            idOficinaConsultda = Convert.ToString(controladoraProyecto.consultarOficinaProyecto(Int32.Parse(idProyectoConsultado)));
            if (perfil.Equals("Administrador"))
            {
                
                if ( controladoraProyecto.consultarEstadoProyecto(Int32.Parse(idProyectoConsultado)).Equals("En ejecución") == false)
                {

                    if (controladoraProyecto.eliminarProyecto(idProyectoConsultado, idOficinaConsultda, perfil) == false)
                    {
                       
                        lblModalTitle.Text = "ERROR";
                        lblModalBody.Text = "No se puede eliminar este proyecto.";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                        upModal.Update();
                    }
                    else
                    {

                        lblModalTitle.Text = "";
                        lblModalBody.Text = "Proyecto eiminado con éxito.";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                        upModal.Update();

                    }

                }
                else
                {
					lblModalTitle.Text = "AVISO";
                    lblModalBody.Text = "El proyecto está en ejecución.";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                    upModal.Update();                }
           
            }
            else if (perfil.Equals("Miembro"))
            {

                if (controladoraProyecto.eliminarProyecto(idProyectoConsultado, idOficinaConsultda, perfil) == false)
                {
                    
                    lblModalTitle.Text = "ERROR";
                    lblModalBody.Text = "No se puede cancelar este proyecto.";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                    upModal.Update();
                }
                else
                {

                   
                    lblModalTitle.Text = " ";
                    lblModalBody.Text = "Proyecto cancelado con éxito.";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                    upModal.Update();

                }

            }
            else
            {
               
                lblModalTitle.Text = "ERROR";
                lblModalBody.Text = "No es posible eliminar el proyecto.";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                upModal.Update();
            }

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

            vaciarCampos();
            controlarCampos(false);
            cambiarEnabled(false, this.btnModificar);
            cambiarEnabled(false, this.btnEliminar);
            cambiarEnabled(false, this.btnAceptar);
            cambiarEnabled(false, this.btnCancelar);

        }
        /*Método para llenar el grid los proyectos del sistema o con los proyectos en los que el miembro se encuentre asociado.
      * Requiere: Requiere la cédula del miembro utilizando el sistema en caso de que éste no sea un administrador
      * Modifica: el valor de cada uno de los campos en la interfaz correspondientes a la consulta retornada por la clase controladora.
      * Retorna: no retorna ningún valor
      */
        protected void llenarGrid(string idUsuario)
        {
            DataTable dt = crearTablaProyectos();
            DataTable proyectos;
            DataTable idProyectos;
            Object[] datos = new Object[5];
            string lider = "";
            int indiceColumna = 0;
            string nombreLider = "";
            DataTable lideres;
            nombreLideresConsultados.Clear();
           


            if (idUsuario == null) //Significa que el usuario utilizando el sistema es un administrador por lo que se le deben mostrar 
            //todos los recursos humanos del sistema
            {
                //Se obtienen todos los proyectos pues el administrador es el usuario del sistema
                proyectos = controladoraProyecto.consultarProyectos(null);

                //Se obtienen todos los lideres del sistema
                lideres = controladoraProyecto.consultarLideres();


                for (int i = 0; i < lideres.Rows.Count; ++i)
                {
                    foreach (DataColumn column in lideres.Columns)
                    {
                      
                            if (indiceColumna == 3)
                            {
                                nombreLideresConsultados.Add(lideres.Rows[i][column].ToString(), nombreLider);

                            }
                            else
                            {
                                nombreLider = nombreLider + " " + lideres.Rows[i][column].ToString();
                            }

                           ++indiceColumna;
                        
                         
                        
                    }

                    indiceColumna = 0; //Contador para saber el número de columna actual.
                    nombreLider = "";

                }

                Session["nombreLideres_Consultados"] = nombreLideresConsultados;


                if (proyectos.Rows.Count >= 1)
                {
                    foreach (DataRow fila in proyectos.Rows)
                    {
                            datos[0] = fila[0].ToString();
                            datos[1] = fila[1].ToString();
                            datos[2] = fila[2].ToString();
                            datos[3] = fila[3].ToString();
                            nombreLideresConsultados.TryGetValue(fila[4].ToString(), out lider);
                            datos[4] = lider;
                            dt.Rows.Add(datos);
                        
                    }

                    lider = "";

                }
                else
                {
                    datos[0] = "-";
                    datos[1] = "-";
                    datos[2] = "-";
                    datos[3] = "-";
                    datos[4] = "-";
                    dt.Rows.Add(datos);
                }
            }
            else
            {
                //Se obtiene un DataTable con el identificador del o los proyectos en los cuales trabaja el miembro

                idProyectos = controladoraProyecto.consultarProyectosAsociados(idUsuario);
                if (idProyectos.Rows.Count > 0)
                {
                    //Se obtiene un DataTable con los datos del o los proyectos 
                    proyectos = controladoraProyecto.consultarProyectos(idProyectos);

                    lideres = controladoraProyecto.consultarLideres();


                    for (int i = 0; i < lideres.Rows.Count; ++i)
                    {
                        foreach (DataColumn column in lideres.Columns)
                        {

                            if (indiceColumna == 3)
                            {
                                nombreLideresConsultados.Add(lideres.Rows[i][column].ToString(), nombreLider);

                            }
                            else
                            {
                                nombreLider = nombreLider + " " + lideres.Rows[i][column].ToString();
                            }

                            ++indiceColumna;
                        }

                        indiceColumna = 0; //Contador para saber el número de columna actual.
                        nombreLider = "";
                        Session["nombreLideres_Consultados"] = nombreLideresConsultados;

                    }




                    if (proyectos.Rows.Count > 0)
                    {

                        foreach (DataRow fila in proyectos.Rows)
                        {                      
                            datos[0] = fila[0].ToString();
                            datos[1] = fila[1].ToString();
                            datos[2] = fila[2].ToString();
                            datos[3] = fila[3].ToString();
                            nombreLideresConsultados.TryGetValue(fila[4].ToString(), out lider);
                            datos[4] = lider;
                            dt.Rows.Add(datos);
                        }

                        lider = "";
                    }
                    else
                    {
                        datos[0] = "-";
                        datos[1] = "-";
                        datos[2] = "-";
                        datos[3] = "-";
                        datos[4] = "-";
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
                    dt.Rows.Add(datos);
                }

            }

            this.gridProyecto.DataSource = dt;
            this.gridProyecto.DataBind();

        }

        /*Método para crear el DataTable donde se mostrará el o los registros de los proyectos del sistema según corresponda.
       * Requiere: No requiere ningún parámetro.
       * Modifica: el nombre de cada columna donde se le especifica el nombre que cada una de ellas tendrá. 
       * Retorna: el DataTable creado. 
       */
        protected DataTable crearTablaProyectos()
        {
            DataTable dt = new DataTable();
            DataColumn columna;

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "ID Proyecto";
            dt.Columns.Add(columna);

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Nombre";
            dt.Columns.Add(columna);

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Estado";
            dt.Columns.Add(columna);


            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Oficina";
            dt.Columns.Add(columna);


            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Nombre líder";
            dt.Columns.Add(columna);

            return dt;
        }

        /*Método para llenar los campos de la interfaz con los resultados de la consulta.
       * Requiere: El identificador del proyecto que se desea consultar.
       * Modifica: Los campos de la interfaz correspondientes a los datos recibidos mediante la clase controladora.
       * Retorna: No retorna ningún valor. 
       */
        public void llenarDatos(string idProyecto)
        {
            idProyectoConsultado = idProyecto;
            DataTable datosFilaProyecto = controladoraProyecto.consultarProyectoTotal(idProyecto); //Se obtienen los datos del proyecto
            DataTable datosFilaMiembros = controladoraProyecto.consultarMiembrosProyecto(idProyecto); //Se obtienen los miembros que trabajan en el proyecto
            DataTable datosOficinaUsuaria = controladoraProyecto.consultarOficina(idProyecto); //Se obtiene los datos de la oficina asociada al proyecto
            DataTable datosTelefOficinaUsuaria = controladoraProyecto.consultarTelOficina(idProyecto); //Se obtiene los teléfonos de la oficina usuaria
            DataTable datosRequerimientos = controladoraProyecto.consultarReqProyecto(Int32.Parse(idProyecto));
            string nombreLider;
            string nombre = "";
            ListItem lider;
            Dictionary<string, string> nombresDelLider = (Dictionary<string, string>)Session["nombreLideres_Consultados"];

            if (datosFilaProyecto.Rows.Count == 1)
            {

                this.txtNombreProy.Text = datosFilaProyecto.Rows[0][0].ToString();
                nombProyConsultado = datosFilaProyecto.Rows[0][0].ToString();

                this.txtObjetivo.Text = datosFilaProyecto.Rows[0][1].ToString();
                this.txtCalendar.Text = datosFilaProyecto.Rows[0][2].ToString();


                if (this.comboEstado.Items.FindByText(datosFilaProyecto.Rows[0][3].ToString()) != null)
                {
                    ListItem estadoProceso = this.comboEstado.Items.FindByText(datosFilaProyecto.Rows[0][3].ToString());
                    this.comboEstado.SelectedValue = estadoProceso.Value;
                }

                //Se obtiene el nombre y apellidos del líder de acuerdo a la cédula de éste en el proyecto consultado
                nombresDelLider.TryGetValue(datosFilaProyecto.Rows[0][4].ToString(), out nombreLider);
                nombre = nombre + nombreLider;

                if (this.comboLider.Items.FindByText(nombre) != null)
                {
                    lider = this.comboLider.Items.FindByText(nombre);
                    this.comboLider.SelectedValue = lider.Value;
                }
            }

            //Datos de la oficina Usuaria asociada al proyecto
            if (datosOficinaUsuaria.Rows.Count == 1)
            {

                this.idOficinaConsultda = datosOficinaUsuaria.Rows[0][0].ToString();
                Session["idOficinaS"] = idOficinaConsultda;
                this.txtnombreOficina.Text = datosOficinaUsuaria.Rows[0][1].ToString();
                nombOfConsultada = datosOficinaUsuaria.Rows[0][1].ToString();
                this.txtnombreRep.Text = datosOficinaUsuaria.Rows[0][2].ToString();
                this.txtApellido1Rep.Text = datosOficinaUsuaria.Rows[0][3].ToString();
                this.txtApellido2Rep.Text = datosOficinaUsuaria.Rows[0][4].ToString();
            }
            else
            {


            }

            //Se obtienen los teléfonos de la oficina usuaria en caso de que sea necesario 
            listTelefonosOficina.Items.Clear();
            if (datosTelefOficinaUsuaria.Rows.Count >= 1)
            {
                listTelefonosOficina.Items.Clear();
                for (int i = 0; i < datosTelefOficinaUsuaria.Rows.Count; ++i)
                {

                    listTelefonosOficina.Items.Add(datosTelefOficinaUsuaria.Rows[i][0].ToString());

                }
            }


            listMiembrosAgregados.Items.Clear();
            string integrantes = "";
            int numColumna = 0;

            //Se obtienen los miembros que trabajan en el proyecto consultado
            if (datosFilaMiembros.Rows.Count >= 1)
            {
                listMiembrosAgregados.Items.Clear();

                for (int i = 0; i < datosFilaMiembros.Rows.Count; ++i)
                {

                    foreach (DataColumn column in datosFilaMiembros.Columns)
                    {

                        if (numColumna == 3)
                        {
                            if (datosFilaMiembros.Rows[i][column].ToString() == "Líder de pruebas")
                            {
                                integrantes = integrantes + ":" + " " + "Líder";
                            }
                            else
                            {
                                integrantes = integrantes + ":" + " " + datosFilaMiembros.Rows[i][column].ToString();
                            }

                        }
                        else
                        {
                            integrantes = integrantes + " " + datosFilaMiembros.Rows[i][column].ToString();
                        }

                        ++numColumna;
                    }

                    listMiembrosAgregados.Items.Add(integrantes);
                    integrantes = "";
                    numColumna = 0;
                }

            }
            //Se obtiene la sigla y el nombre de los requerimientos que posee el proyecto
            if (datosRequerimientos.Rows.Count >= 1)
            {
                for (int i = 0; i < datosRequerimientos.Rows.Count; ++i)
                {
                    listRequerimientosAgregados.Items.Add(datosRequerimientos.Rows[i][0].ToString() + " " + datosRequerimientos.Rows[i][3].ToString());
                }

            }
        }

    }
}