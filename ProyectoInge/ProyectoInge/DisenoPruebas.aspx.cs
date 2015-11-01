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
                cambiarEnabledManosReq(false, this.lnkAgregarReq);
                cambiarEnabledManosReq(false, this.lnkQuitarReq);
                llenarComboNivel();
                llenarComboTecnica();
                if (Session["perfil"].ToString().Equals("Administrador"))
                {
                    cambiarEnabled(true, this.btnInsertar);                  
                    llenarComboProyecto(null);
                    llenarComboRecursos();
                    llenarGrid(null);
                }
                else
                {
                    cambiarEnabled(false, this.btnInsertar);         
                    llenarComboProyecto(Session["cedula"].ToString());
                    llenarComboRecursos();
                    llenarGrid(Session["cedula"].ToString());
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
            this.comboProyecto.Enabled = false;
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


     
       /* Método para habilitar el campo del rol en caso de que el perfil sea un miembro y para bloquearlo cuando no 
        * Modifica: el campo del rol de acuerdo al campo del perfil
        * Retorna: no retorna ningún valor */
        protected void  pruebaUnitariaSeleccionada(object sender, EventArgs e)
        {
            if (this.comboNivel.SelectedIndex != -1)
            {
                if (this.comboNivel.Items[this.comboNivel.SelectedIndex].Text == "Unitaria" && this.listReqAgregados.Items.Count > 1)
                {
                    lblModalTitle.Text = " ";
                    lblModalBody.Text = "Recuerde que una prueba unitaria solo puede probar un requerimiento";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                    upModal.Update();
                }
            }

          //  UpdatePanelDropDown.Update();
        }


        /*Método para habilitar/deshabilitar todos los campos y los botones + y -
        * Requiere: un booleano para saber si quiere habilitar o deshabilitar los botones y cajas de texto
        * Modifica: Cambia la propiedad Enabled de las cajas y botones
        * Retorna: no retorna ningún valor
        */
        protected void controlarCampos(Boolean condicion)
        {
            this.comboProyecto.Enabled = condicion;
            this.listReqAgregados.Enabled = condicion;
            this.listReqProyecto.Enabled = condicion;
            this.lnkAgregarReq.Enabled = condicion;
            this.lnkQuitarReq.Enabled = condicion;
            this.txtProposito.Enabled = condicion;
            this.comboNivel.Enabled = condicion;
            this.comboTecnica.Enabled = condicion;
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

        /*Método para habilitar/deshabilitar el botón tipo LinkButton
       * Requiere: el booleano para la acción
       * Modifica: La propiedad enable del botón
       * Retorna: no retorna ningún valor
       */
        protected void cambiarEnabledManosReq(bool condicion, LinkButton boton)
        {
            boton.Enabled = condicion;
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

            this.listReqProyecto.Items.Clear();

            if(cedulaUsuario==null){

                nombresProyecto = controladoraDiseno.consultarNombresProyectos();
                if (nombresProyecto != null && nombresProyecto.Rows.Count > 0)
                {
                    numDatos = nombresProyecto.Rows.Count;
                }
            }else{
                nombresProyecto = controladoraDiseno.consultarProyectosLider(cedulaUsuario);
                if(nombresProyecto==null){
                     nombresProyecto = controladoraDiseno.consultarProyectosDeUsuario(cedulaUsuario);
                }

                numDatos = nombresProyecto.Rows.Count;

            }
            

            if (numDatos>0)
            {
                datos = new Object[numDatos+1];

                for (int i = 0; i < numDatos; ++i)
                {
                    foreach (DataColumn column in nombresProyecto.Columns)
                    {
                        if (numColumna == 1)
                        {

                            nombres_id_proyectos.Add(nombre, nombresProyecto.Rows[i][1].ToString());
                            id_nombres_proyectos.Add(nombresProyecto.Rows[i][1].ToString(),nombre);
                       
                            
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

                Dictionary<string, string> cedulasRepresentantes = new Dictionary<string, string>();
                Dictionary<string, string> cedulasNombreRepresentantes= new Dictionary<string, string>();


                int id = controladoraDiseno.obtenerIdProyecto(this.comboProyecto.Text);
                DataTable Recursos = controladoraDiseno.consultarMiembrosDeProyecto(id.ToString());
                int numDatos = Recursos.Rows.Count;
                Object[] datos;
                string nombre = "";
                int numColumna = 0;
                int indiceResponsables = 1;

                if (Recursos.Rows.Count >= 1)
                {
                    numDatos = Recursos.Rows.Count;
                    datos = new Object[numDatos+2];

                    for (int i = 0; i < Recursos.Rows.Count; ++i)
                    {


                        foreach(DataColumn column in Recursos.Columns)
                        {
                            if(numColumna == 4 ){

                                cedulasRepresentantes.Add(nombre,Recursos.Rows[i][numColumna].ToString());
                                cedulasNombreRepresentantes.Add(Recursos.Rows[i][numColumna].ToString(),nombre);
                            
                            }
                            else
                            {
                                nombre = Recursos.Rows[i][0].ToString() + " "+ Recursos.Rows[i][1].ToString();
                            }

                            ++numColumna;
                        }
                        
                        datos[indiceResponsables] = nombre;
                        ++indiceResponsables;
                        numColumna = 0;
                        nombre = "";

                    }

                    numColumna = 0;
                    Recursos = controladoraDiseno.consultarLider(id);

                    if(Recursos != null && Recursos.Rows.Count==1){
                    


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

         
            }

         

        protected void proyectoSeleccionado(object sender, EventArgs e)
        {
            llenarComboRecursos();
            int id = controladoraDiseno.obtenerIDconNombreProyecto(this.comboProyecto.Text);
            DataTable datosReqProyecto = controladoraDiseno.consultarReqProyecto(id);
            string requerimiento = "";
            
            requerimiento = "";
            if (datosReqProyecto != null && datosReqProyecto.Rows.Count >= 1)
            {
                listReqProyecto.Items.Clear();
                for (int i = 0; i < datosReqProyecto.Rows.Count; ++i)
                {
                    requerimiento = datosReqProyecto.Rows[i][0].ToString() + " " + datosReqProyecto.Rows[i][2].ToString();
                    listReqProyecto.Items.Add(requerimiento);

                }
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

                     //    btnAceptar_Modificar();
                    }
                    break;
                case 3:
                    {
                        //btnAceptar_Eliminar();
                    }
                    break;

            }
        
        }

        /*Método para limpiar los textbox
      * Requiere: No requiere parámetros
      * Modifica: Establece la propiedad text de los textbox en "" y limpia los listbox
      * Retorna: no retorna ningún valor
      */
        protected void vaciarCampos()
        {

            this.listReqAgregados.Items.Clear();
            this.listReqProyecto.Items.Clear();
            this.txtProposito.Text = "";
            this.txtAmbiente.Text = "";
            this.txtProcedimiento.Text = "";
            this.txtCriterios.Text = "";
            this.txtCalendar.Text = "";
        }


        /*
         * Método para poder indicar que se va a generar la operación de insertar.
         */
        protected void btnInsertar_Click(object sender, EventArgs e)
        {
            vaciarCampos();
            controlarCampos(true);
            llenarComboNivel();
            if (Session["perfil"].ToString().Equals("Administrador"))
            {
                llenarComboProyecto(null);
            }
            else
            {
                llenarComboProyecto(Session["cedula"].ToString());
            }
            
            llenarComboRecursos();
            llenarComboTecnica();

            modo = 1;
            cambiarEnabled(true, this.btnAceptar);
            cambiarEnabled(true, this.btnCancelar);
            cambiarEnabled(false, this.btnModificar);
            cambiarEnabled(false, this.btnEliminar);
            cambiarEnabled(false, this.btnInsertar);

            cambiarEnabledManosReq(true, this.lnkAgregarReq);
            cambiarEnabledManosReq(true, this.lnkQuitarReq);
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {

        }

        /*
       * Método para poder asignar requerimientos al diseño respectivo
       */
        protected void btnlnkAgregarReq(object sender, EventArgs e)
        {
            if (listReqAgregados.SelectedIndex != -1)
            {
                listReqAgregados.Items.Add(listReqAgregados.SelectedValue);
                listReqAgregados.Items.RemoveAt(listReqAgregados.SelectedIndex);
            }

            if (modo == 1)
            {
                habilitarCamposInsertar();  ///
            }
            else if (modo == 2)
            {

            }

            UpdateAsociarDesasociarRequerimientos.Update();
        }

        /*
         * Método para poder desasociar un requerimiento a un diseño, el cambio se refleja en la base y en la interfaz
         * no asignados y asignados.
        */
        protected void btnlnkQuitarReq(object sender, EventArgs e)
        {
            if (modo == 1 || modo == 2)
            {
                if (listReqAgregados.SelectedIndex != -1)
                {
                    listReqProyecto.Items.Add(listReqAgregados.SelectedValue);
                    listReqAgregados.Items.RemoveAt(listReqAgregados.SelectedIndex);
                }
            }

            UpdateAsociarDesasociarRequerimientos.Update();
        }

        /*Método para la acción de aceptar cuando esta en modo de inserción
       * Requiere: No requiere ningún parámetro
       * Modifica: Crea un objeto con los datos obtenidos en la interfaz mediante textbox y 
       * valida que todos los datos se encuentren para la modificacion
       * Retorna: No retorna ningún valor
       */
  /*      private void btnAceptar_Modificar()
        {
            int tipoInsercion = 1;
            if (faltanDatos())
            {
                lblModalTitle.Text = " ";
                lblModalBody.Text = "Para modificar un nuevo diseño de prueba debe completar todos los campos obligatorios.";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                upModal.Update();
                //habilitarCamposModificar();
            }
            else
            {
                //eliminar requerimientos


                int idProyecto = controladoraDiseno.obtenerIDconNombreProyecto(this.comboProyecto.Text);
                //Se crea el objeto para encapsular los datos de la interfaz para insertar oficina usuaria
                Object[] datosNuevos = new Object[10];
                datosNuevos[0] = this.txtProposito.Text;
                datosNuevos[1] = this.txtCalendar.Text;
                datosNuevos[2] = this.txtProcedimiento.Text;
                datosNuevos[3] = this.txtAmbiente.Text;
                datosNuevos[4] = this.txtCriterios.Text;
                datosNuevos[5] = this.comboTecnica.Text;
                datosNuevos[6] = this.comboNivel.Text;
                //datosNuevos[7] = this.comboTipo.Text;
                datosNuevos[7] = idProyecto;
                datosNuevos[8] = obtenerCedula(this.comboResponsable.Text);



                /*aqui*/


                //si el diseño de prueba se pudo insertar correctamente entra a este if
  /*              if (controladoraDiseno.ejecutarAccion(modo, tipoInsercion, datosNuevos, "", ""))
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
         */
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

                int idProyecto = controladoraDiseno.obtenerIDconNombreProyecto(this.comboProyecto.Text);
                //Se crea el objeto para encapsular los datos de la interfaz para insertar oficina usuaria
                Object[] datosNuevos = new Object[10];
                datosNuevos[0] = this.txtProposito.Text;
                datosNuevos[1] = this.txtCalendar.Text;
                datosNuevos[2] = this.txtProcedimiento.Text;
                datosNuevos[3] = this.txtAmbiente.Text;
                datosNuevos[4] = this.txtCriterios.Text;
                datosNuevos[5] = this.comboTecnica.Text;
                datosNuevos[6] = this.comboNivel.Text;
                datosNuevos[8] = idProyecto;
                datosNuevos[9] = obtenerCedula(this.comboResponsable.Text);


                //si el diseño de prueba se pudo insertar correctamente entra a este if
                if (controladoraDiseno.ejecutarAccion(modo, tipoInsercion, datosNuevos, 0, ""))
                {

                    //Se actualiza la tabla de requerimientos para asociarle el/los requerimientos a un diseño 

                    int i = 0;
                    int indiceReq = 0;
                    string sigla = "";
                    string nombreRequerimiento = "";


                    if (this.comboNivel.Text == "Unitaria")
                    {

                        if (this.listReqAgregados.Items.Count > 1)
                        {
                            lblModalTitle.Text = " ";
                            lblModalBody.Text = "Debe elegir un solo requerimiento para el nivel unitario.";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                            int idDiseño = controladoraDiseno.obtenerIdDisenoPorProposito(this.txtProposito.Text);

                            if (controladoraDiseno.ejecutarAccion(3, 1, null, idDiseño, ""))//Se pone 3 porque este siempre elimina y 1 porque esto indica que se va a borrar el diseño
                            {
                            }
                            upModal.Update();
                            habilitarCamposInsertar();
                        }
                        else
                        {
                            indiceReq = 0;

                            while (indiceReq < listReqAgregados.Items[0].ToString().Count() && listReqAgregados.Items[0].ToString().ElementAt(indiceReq) != ' ')
                            {
                                ++indiceReq;
                            }

                            sigla = listReqAgregados.Items[0].ToString().Substring(0, indiceReq);
                            nombreRequerimiento = listReqAgregados.Items[0].ToString().Substring(indiceReq + 1, listReqAgregados.Items[0].ToString().Count() - indiceReq - 1);


                            Object[] nuevoReqDiseño = new Object[3];
                            nuevoReqDiseño[0] = controladoraDiseno.obtenerIdDisenoPorProposito(this.txtProposito.Text);
                            nuevoReqDiseño[1] = sigla;
                            nuevoReqDiseño[2] = idProyecto;
                            tipoInsercion = 2;

                            //Se actualizó un requerimiento
                            if (controladoraDiseno.ejecutarAccion(1, tipoInsercion, nuevoReqDiseño, 0, ""))//Esto siempre inserta, por lo que le mandaremos un 1
                            {
                            }
                            //La actualizó de un requerimiento falló porque el habían datos inválidos.
                            else
                            {
                                lblModalTitle.Text = " ";
                                lblModalBody.Text = "No fue posible realizar la inserción de/los requerimientos.";
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                                upModal.Update();
                                habilitarCamposInsertar();
                            }


                        }
                    }

                    else
                    {
                        //Recorre el list de requerimientos agregados para el nivel de integracion, aceptacion y de sistema
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


                            Object[] requerimientoActualizado = new Object[3];
                            requerimientoActualizado[0] = controladoraDiseno.obtenerIdDisenoPorProposito(this.txtProposito.Text);
                            requerimientoActualizado[1] = sigla;
                            requerimientoActualizado[2] = idProyecto;
                            tipoInsercion = 2;


                            //Se actualizó un requerimiento
                            if (controladoraDiseno.ejecutarAccion(1, tipoInsercion, requerimientoActualizado, 0, ""))//Esto siempre inserta, por lo que le mandaremos un 1
                            {
                            }
                            //La actualizó de un requerimiento falló porque el habían datos inválidos.
                            else
                            {
                                lblModalTitle.Text = " ";
                                lblModalBody.Text = "No fue posible realizar la inserción de/los requerimientos.";
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


       /*Método que agrega al listbox de requerimeintos
       * el/los requerientos(s) que el usuario escoge
       * Modifica: listbox de requerimientos
       * Retorna: no retorna ningún valor
       */
        protected void btnAgregarRequerimiento(object sender, EventArgs e)
        {

            if (listReqProyecto.SelectedIndex != -1)
            {
                if (listReqProyecto.SelectedIndex.ToString() == "Todos los requerimientos")
                {
                    int contador = 0;
                    while (contador < listReqProyecto.Items.Count - 1)
                    {
                        listReqAgregados.Items.Add(listReqProyecto.Items[contador].ToString());
                        listReqProyecto.Items.RemoveAt(contador);
                        ++contador;
                    }

                }
                else
                {
                    listReqAgregados.Items.Add(listReqProyecto.SelectedValue);
                    listReqProyecto.Items.RemoveAt(listReqProyecto.SelectedIndex);
                }

            }

            if (modo == 1)
            {
                habilitarCamposInsertar();
            }
            else if (modo == 2)
            {

            }

            UpdateAsociarDesasociarRequerimientos.Update();
        }

        protected void btnAgregarRequerimiento(object sender, EventArgs e)
        {
            if (modo == 1 || modo == 2)
            {
                if (listReqAgregados.SelectedIndex != -1)
                {
                    listReqProyecto.Items.Add(listReqAgregados.SelectedValue);
                    listReqAgregados.Items.RemoveAt(listReqAgregados.SelectedIndex);
                }
            }

            UpdateAsociarDesasociarRequerimientos.Update();
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
            if (this.comboProyecto.Text == "" || this.txtProposito.Text == "" || this.txtProcedimiento.Text == "" || this.comboResponsable.Text == "" || this.txtCalendar.Text == "")
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
                Session["idDiseñoS"] = idDiseñoConsultado;

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
                LinkButton linkConsultaCaso = (LinkButton)e.CommandSource;
                idDiseñoConsultado = linkConsultaCaso.CommandArgument;
                Session["idDiseñoS"] = idDiseñoConsultado;
                Response.Redirect("~/CasoDePrueba.aspx");
                
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
            if (idDiseño != null && idDiseño.Equals("-") == false )
            {
                string nombreRepresentante = "";
                string nombre = "";
                ListItem representante;
                ListItem proyecto;
                Dictionary<string, string> nombresDelRepresentante;
                Dictionary<string, string> nombresDelProyecto = (Dictionary<string, string>)Session["vectorIdNombres"];
                String cedulaRepresentante = "";
                String nombreProyecto = "";
                idDiseñoConsultado = idDiseño;
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
                    idProyectoConsultado = datosFilaDiseño.Rows[0][8].ToString();
                    cedulaRepresentante = datosFilaDiseño.Rows[0][9].ToString();
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
                    
                    

                    nombresDelProyecto.TryGetValue(datosFilaDiseño.Rows[0][8].ToString(), out nombreProyecto);

                    nombre = nombre + nombreProyecto;
                    if (this.comboProyecto.Items.FindByText(nombre) != null)
                    {
                        proyecto = this.comboProyecto.Items.FindByText(nombre);
                        this.comboProyecto.SelectedValue = proyecto.Value;
                       
                    }

                    llenarComboRecursos();
                    nombresDelRepresentante = (Dictionary<string, string>)Session["vectorCedulasNombreResponsables"];
                    nombre = "";
                    nombresDelRepresentante.TryGetValue(datosFilaDiseño.Rows[0][9].ToString(), out nombreRepresentante);
                    nombre = nombre + nombreRepresentante;

                    if (this.comboResponsable.Items.FindByText(nombre) != null)
                    {
                        representante = this.comboResponsable.Items.FindByText(nombre);
                        this.comboResponsable.SelectedValue = representante.Value;
                    }

                }
                string requerimiento = "";
                if (datosReqDiseno.Rows.Count >= 1)
                {
                    listReqAgregados.Items.Clear();
                    for (int i = 0; i < datosReqDiseno.Rows.Count; ++i)
                    {
                        requerimiento = datosReqDiseno.Rows[i][0].ToString() +" "+ datosReqDiseno.Rows[i][2].ToString();
                        listReqAgregados.Items.Add(requerimiento);

                    }
                }
                requerimiento = "";
                if (datosReqProyecto.Rows.Count >= 1)
                {
                    listReqProyecto.Items.Clear();
                    for (int i = 0; i < datosReqProyecto.Rows.Count; ++i)
                    {
                        requerimiento = datosReqProyecto.Rows[i][0].ToString() + " " + datosReqProyecto.Rows[i][2].ToString();
                        listReqProyecto.Items.Add(requerimiento);
                    }
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
            Dictionary<string, string> nombreRepresentantesConsultados = new Dictionary<string, string>();
            DataTable dt = crearTablaDisenos();
            DataTable diseños;
            DataTable idProyectos;
            Object[] datos = new Object[5];
            string representante = "";
            int indiceColumna = 0;
            string nombreRepresentante = "";
            DataTable representantes;
            

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
                Session["nombreRepresentantes_Consultados"] = nombreRepresentantesConsultados;



                if (diseños.Rows.Count > 0)
                {
                    foreach (DataRow fila in diseños.Rows)
                    {
                        
                            datos[0] = fila[0].ToString();
                            datos[1] = fila[1].ToString();
                            datos[2] = fila[2].ToString();
                            datos[3] = fila[3].ToString();
                            nombreRepresentantesConsultados.TryGetValue(fila[4].ToString(), out representante);
                            datos[4] = representante;
                            dt.Rows.Add(datos);
                        
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
                    dt.Rows.Add(datos);
                }
            }
            else
            {
                //Se obtiene un DataTable con el identificador del o los proyectos en los cuales trabaja el miembro
                
                idProyectos = controladoraDiseno.consultarProyectosAsociados(idUsuario);

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
                Session["nombreRepresentantes_Consultados"] = nombreRepresentantesConsultados;

                
                if (idProyectos.Rows.Count > 0)
                {
                    
                    //Se obtiene un DataTable con los datos del o los proyectos 
                    diseños = controladoraDiseno.consultarDisenos(idProyectos);


                    if (diseños.Rows.Count > 0)
                    {

                        foreach (DataRow fila in diseños.Rows)
                        {
                            
                                datos[0] = fila[0].ToString();
                                datos[1] = fila[1].ToString();
                                datos[2] = fila[2].ToString();
                                datos[3] = fila[3].ToString();
                                nombreRepresentantesConsultados.TryGetValue(fila[4].ToString(), out representante);
                                datos[4] = representante;
                                dt.Rows.Add(datos);
                            

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
            columna.ColumnName = "Nombre Representante";
            dt.Columns.Add(columna);


            return dt;
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
                    lblModalTitle.Text = "ERROR";
                    lblModalBody.Text = "Nombre del miembro es inválido.";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                    upModal.Update();
                }

            return cedula;

        }

        /*Método para obtener requerimientosDiseño de un miembro a partir del nombre
       * Requiere: nombre
       * Modifica: el valor de la cédula solicitada.
       * Retorna: la cédula del miembro solicitado.
       */
        protected string obtenerIdRequerimiento(string requerimientosDiseño)
        {
            string requerimientos = "";

            Dictionary<string, string> idRequerimientosDiseño = (Dictionary<string, string>)Session["vectoRequerimientosDiseño"];

            if (!idRequerimientosDiseño.TryGetValue(requerimientosDiseño, out requerimientos)) // Returns true.
            {
                lblModalTitle.Text = "ERROR";
                lblModalBody.Text = "Nombre del requerimiento es inválido.";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                upModal.Update();
            }
            return requerimientos;
        }



        /*Método para obtener id del proyecto con nombre
        * Requiere: nombre
        * Modifica: no genera modificaciones
        * Retorna: id del proyecto
        */
        protected string obtenerIdProyecto(string nombreProyecto)
        {
            string idProyecto = "";

            Dictionary<string, string> identificadores = (Dictionary<string, string>)Session["vectorIdProyectos"];

            if (!identificadores.TryGetValue(nombreProyecto, out idProyecto)) // Returns true.
            {
                lblModalTitle.Text = "ERROR";
                lblModalBody.Text = "Nombre de proyecto es inválido.";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                upModal.Update();
            }

            return idProyecto;

        }



    }
}