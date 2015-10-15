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

        private static int modo = 1; //1 insertar, 2 modificar, 3 eliminar

        ControladoraProyecto controladoraProyecto = new ControladoraProyecto();
        private string idProyectoConsultado;
        private string idOficinaConsultda;
        private string idmiembroConsultado;        

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                llenarComboEstado();
                llenarComboLideres();
                cargarMiembrosSinAsignar();
            }
 
            llenarGrid(null); //Por el momento se pone null, pero en realidad se debe verificar si es un miembro o un administrador 
                              //el que se encuentra usando el sistema
            /*if (Session["cedula"] == null)
            {
                Response.Redirect("~/Login.aspx");

            }*/

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

       /* Método para llenar el comboBox según los miembros que sean almacenados como líderes en la BD
       * Modifica: llena el comboBox con los datos obtenidos de la BD
       * Retorna: no retorna ningún valor */
        protected void llenarComboLideres()
        {
            this.comboLider.Items.Clear();
            DataTable Lideres = controladoraProyecto.consultarLideres();
            int numDatos = Lideres.Rows.Count;
            Object[] datos;
            string nombre = "";

            if (Lideres.Rows.Count >= 1)
            {

                numDatos = Lideres.Rows.Count;
                datos = new Object[numDatos];

                for (int i = 0; i < Lideres.Rows.Count; ++i)
                {
                    foreach (DataColumn column in Lideres.Columns)
                    {
                        nombre = nombre + " " + Lideres.Rows[i][column].ToString();
                    }
                    datos[i] = nombre;

                }
                this.comboLider.DataSource = datos;
                this.comboLider.DataBind();

            }
        }

        /* Método para llenar el listBox con los miembros de la BD
        * Modifica: llena el listoBox con los datos obtenidos de la BD
        * Retorna: no retorna ningún valor */
        protected void cargarMiembrosSinAsignar()
        {

            DataTable datosMiembros = controladoraProyecto.consultarMiembros();
            string filaMiembro = "";



            if (datosMiembros.Rows.Count >= 1)
            {

                for (int i = 0; i < datosMiembros.Rows.Count; ++i)
                {

                    foreach (DataColumn column in datosMiembros.Columns)
                    {
                        if (datosMiembros.Rows[i][column].ToString() == "Líder de pruebas")
                        {
                            filaMiembro = filaMiembro + " " + "Líder";
                        }
                        else
                        {
                            filaMiembro = filaMiembro + " " + datosMiembros.Rows[i][column].ToString();
                        }
                
                    }


                    listMiembrosDisponibles.Items.Add(filaMiembro);
                    filaMiembro = "";
                    
                }
            }
        }

         /*Método para obtener la cédula de un miembro a partir del nombre
         * Requiere: el nombre del miembro y el tipo de búsqueda, ya sea sobre un líder o sobre un miembro asignado a un proyecto.
         * Modifica: el valor de la cédula solicitada.
         * Retorna: la cédula del miembro solicitado.
         */
        protected string obtenerCedulaMiembroAsignado(string nombreMiembro, bool lider)
        {
            ControladoraRecursos controladoraRH = new ControladoraRecursos();
            string cedula = controladoraRH.obtenerCedulaMiembro(nombreMiembro, lider);
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

                llenarDatos(idProyectoConsultado);
                cambiarEnabled(true, this.btnModificar);
                cambiarEnabled(true, this.btnCancelar);
                cambiarEnabled(false, this.btnInsertar);

                /*    //El unico botón que cambia de acuerdo al perfil es el de eliminar
                    if (Session["perfil"].ToString().Equals("Administrador"))
                    {

                        cambiarEnabled(true, this.btnEliminar);
                    }
                    else
                    {
                        cambiarEnabled(false, this.btnEliminar);
                    } */
            }

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
            habilitarCamoposModificar();

        }
        
        /*Método que habilita la ventana modificar dependiendo del pefil actual
         * Requiere: No requiere parámetros
         * Modifica: Habilita y deshabilita botones y texbox
         * Retorna: no retorna ningún valor
         */
        private void habilitarCamoposModificar()
        {
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
            if (Session["perfil"].ToString().Equals("Administrador"))
            {
                //Fecha habilitada para administrador;
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
            this.txtObjetivo.Text="";
            this.txtnombreOficina.Text = "";
            this.txtnombreRep.Text = "";
            this.txtApellido1Rep.Text = "";
            this.txtApellido2Rep.Text = "";
            this.txtTelefonoOficina.Text = "";
            this.listTelefonosOficina.Items.Clear();
            this.listMiembrosDisponibles.Items.Clear();
            this.listMiembrosAgregados.Items.Clear();
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
            this.calendarFecha.Enabled = condicion;
            this.txtnombreOficina.Enabled = condicion;
            this.txtnombreRep.Enabled = condicion;
            this.txtApellido1Rep.Enabled = condicion;
            this.txtApellido2Rep.Enabled = condicion;
            this.txtTelefonoOficina.Enabled = condicion;
            this.comboLider.Enabled = condicion;
            this.lnkNumero.Enabled = condicion;
            this.lnkQuitar.Enabled = condicion;
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
                case 3:
                    {
                        btnAceptar_Eliminar();
                    }
                    break;

            }
        }
        /*Método para la acción de aceptar modificar
        * Requiere: No requiere ningún parámetro
        * Modifica: Crea un objeto con los datos obtenidos en la interfaz mediante textbox y 
        * valida que todos los datos se encuentren para la modificación
        * Retorna: No retorna ningún valor
        */
        private void btnAceptar_Modificar()
        {
            int tipoModificacion = 1;//Va a cambiar la tabla proyecto
            if (faltanDatos())//2 indica los datos que pueden faltar en el modificar
            {
                string mensaje = "<script>window.alert('Para modificar un proyecto debe completar todos los datos habilitados.');</script>";
                Response.Write(mensaje);
            }
            else
            {
                //Se crea el objeto para encapsular los datos de la interfaz para modificar proyecto
                //los encapsula todos, sea administrador o miembro
                Object[] datosProyecto = new Object[8];
                datosProyecto[0] = this.idProyectoConsultado;//id_proyecto
                datosProyecto[1] = this.txtNombreProy.Text;//nombre_proyecto
                datosProyecto[2] = this.txtObjetivo.Text;//obj_general
                datosProyecto[3] = "01/01/01";//fecha_asignacion
                datosProyecto[4] = this.comboEstado.Text;//tipo_estado
                datosProyecto[5] = this.comboLider.Text;
                datosProyecto[6] = this.idOficinaConsultda;//ID DE DONDE LO SACO?
                //El creador no se cambia. Y no se como encontrarlo
                if (controladoraProyecto.ejecutarAccion(modo, tipoModificacion, datosProyecto, "", ""))
                {
                    //Se crea el objeto para encapsular los datos de la interfaz para modificar oficina usuaria
                    tipoModificacion = 2;//Va a cambiar la oficina usuaria
                    Object[] datosOfUsuaria = new Object[5];
                    datosOfUsuaria[0] = this.idOficinaConsultda;//id_oficina
                    datosOfUsuaria[1] = this.txtnombreOficina;//nombre_oficina
                    datosOfUsuaria[2] = this.txtnombreRep;//nombre_rep
                    datosOfUsuaria[3] = this.txtApellido1Rep;//ape1_rep
                    datosOfUsuaria[4] = this.txtApellido2Rep;//ape2_rep
                    if (controladoraProyecto.ejecutarAccion(modo, tipoModificacion, datosOfUsuaria, "", ""))
                    {
                        //Para modificar los telefonos de la oficina en la oficina usuaria modificar oficina usuaria
                        tipoModificacion = 3;//Va a cambiar los telefonos de la oficina usuaria
                        //Eliminar los telefonos de la of usuaria
                        if (controladoraProyecto.ejecutarAccion(3, tipoModificacion, null, idOficinaConsultda, ""))//modo 3 para eliminar, le mando el id de la oficina
                        {
                            //Insertar los telefonos
                            bool insertados = insertarTelefonosOficinaUsuaria();
                            //Si todo bien
                            if (insertados)
                            {
                                //Para asociar y desasociar recursos, en la tabla Trabaja_En
                                /**
                                 * ESTO DEBERIA HACERLO EL BOTON DE AGREGAR RECURSO, CADA QUE LO HACE
                                 */
                                tipoModificacion = 4;//Va a cambiar trabaja_en
                                if (controladoraProyecto.ejecutarAccion(1, tipoModificacion, null, idmiembroConsultado, idProyectoConsultado))//lo mando con 1 para que agregue
                                //le mando el id del que debo asociar y el proyecto al que lo debo asociar
                                {

                                }
                                /**
                                 * ESTO DEBERIA HACERLO EL BOTON DE QUITAR RECURSO, CADA QUE LO HACE
                                 */
                                tipoModificacion = 4;//Va a cambiar trabaja_en
                                if (controladoraProyecto.ejecutarAccion(3, tipoModificacion, null, idmiembroConsultado, idProyectoConsultado))//lo mando con 3 pars que elimine
                                {

                                }

                            }
                        }

                    }

                }
            }

        }
        private bool insertarTelefonosOficinaUsuaria()
        {
            throw new NotImplementedException();
        }

        /*Método para la acción de aceptar cuando esta en modo de inserción
         * Requiere: No requiere ningún parámetro
         * Modifica: Crea un objeto con los datos obtenidos en la interfaz mediante textbox y 
         * valida que todos los datos se encuentren para la inserción
         * Retorna: No retorna ningún valor
         */
        protected void btnAceptar_Insertar()
        {
            int tipoInsercion;

            //si faltan datos no deja insertar
            if (faltanDatos())
            {
                string mensaje = "<script>window.alert('Para insertar un nuevo proyecto debe completar todos los datos.');</script>";
                Response.Write(mensaje);
                habilitarCamposInsertar();
            }
            else
            {
 
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

        /*Método para saber si hay cajas de texto que no tienen datos en su interior
         * Recibe: No recibe ningún parámetro
         * Modifica:  Verifica si faltan datos en alguna caja de texto
         * Retorna: retorna true si alguna caja no tiene texto, false si todas las cajas tienen texto
         */
        protected bool faltanDatos()
        {

            bool resultado = false;

            //Pregunta por todas las cajas
            if (modo == 1)
            {
                if (this.txtNombreProy.Text == "" || this.txtObjetivo.Text == "" || this.txtnombreOficina.Text == "" || this.txtnombreRep.Text == "" || this.txtApellido1Rep.Text == "" || this.txtApellido2Rep.Text == "")
                {
                    resultado = true;
                }
                else
                {
                    resultado = false;
                }
            }
            else if (modo == 2)
            {//FALTA LA FECHA
                if (this.txtNombreProy.Text == "" || this.txtObjetivo.Text == "" || this.txtnombreOficina.Text == "" || this.txtnombreRep.Text == "" || this.txtApellido1Rep.Text == "" || this.txtApellido2Rep.Text == "" || this.comboEstado.Text == "" || this.comboLider.Text == "")
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
            string mensaje ;
            cambiarEnabled(false, this.btnInsertar);
            cambiarEnabled(false, this.btnModificar);
            cambiarEnabled(true, this.btnAceptar);
            cambiarEnabled(true, this.btnCancelar);
            modo = 3;
            controlarCampos(false);

            if (Session["perfil"].ToString().Equals("Administrador"))
            {
                mensaje = "<script>window.alert('Está seguro que desea eliminar este proyecto?');</script>";
            }
            else
            {
                mensaje = "<script>window.alert('Está seguro que desea cambiar el estado del proyecto?');</script>";
            }
            Response.Write(mensaje);
        }


        /*Método para la acción de aceptar cuando esta en modo de borrado
         * Requiere: No requiere ningún parámetro
         * Modifica:Elimina un recurso humano si es valido llevar acabo la acción
         * Retorna: No retorna ningún valor
         */
        protected void btnAceptar_Eliminar()
        {
            
            string perfil = Session["perfil"].ToString();

            if (perfil.Equals("Administrador"))
            {
               
                if (controladoraProyecto.ejecutarAccion(modo, 1, null, txtNombreProy.Text, perfil) == false)
                {
                    string mensaje = "<script>window.alert('No se puede eliminar este proyecto');</script>";
                    Response.Write(mensaje);
                }
                else
                {

                    string mensaje = "<script>window.alert('Proyecto eiminado con éxito.');</script>";
                    Response.Write(mensaje);
                }
                vaciarCampos();

                controlarCampos(false);
                cambiarEnabled(false, this.btnModificar);
                cambiarEnabled(false, this.btnEliminar);
                cambiarEnabled(false, this.btnAceptar);
                cambiarEnabled(false, this.btnCancelar);
                //llenarDropDownPerfil();
                //llenarDropDownRol();
            }else if(perfil.Equals("Miembro")) {

                if (controladoraProyecto.ejecutarAccion(modo, 1, null, txtNombreProy.Text, perfil) == false)
                {
                    string mensaje = "<script>window.alert('No se puede cancelar este proyecto');</script>";
                    Response.Write(mensaje);
                }
                else
                {

                    string mensaje = "<script>window.alert('Proyecto cancelado con éxito.');</script>";
                    Response.Write(mensaje);
                }

                vaciarCampos();

                controlarCampos(false);
                cambiarEnabled(false, this.btnModificar);
                cambiarEnabled(false, this.btnEliminar);
                cambiarEnabled(false, this.btnAceptar);
                cambiarEnabled(false, this.btnCancelar);
                //llenarDropDownPerfil();
                //llenarDropDownRol();

            } else
            {
                string mensaje = "<script>window.alert('No es posible eliminar el proyecto);</script>";
                Response.Write(mensaje);
            }
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
            Object[] datos = new Object[5];
            string lider = "";

            if (idUsuario == null) //Significa que el usuario utilizando el sistema es un administrador por lo que se le deben mostrar 
            //todos los recursos humanos del sistema
            {
                proyectos = controladoraProyecto.consultarProyectos(null);
                if (proyectos.Rows.Count > 0)
                {
                    foreach (DataRow fila in proyectos.Rows)
                    {
                        datos[0] = fila[0].ToString();
                        datos[1] = fila[1].ToString();
                        datos[2] = fila[2].ToString();
                        datos[3] = fila[3].ToString();
                        lider = fila[4].ToString();
                        lider = lider + " " + fila[5].ToString();
                        lider = lider + " " + fila[6].ToString();
                        datos[4] = lider;

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
            else
            {
                proyectos = controladoraProyecto.consultarProyectos(idUsuario);
                if (proyectos.Rows.Count == 1)
                {
                    foreach (DataRow fila in proyectos.Rows)
                    {
                        datos[0] = fila[0].ToString();
                        datos[1] = fila[1].ToString();
                        datos[2] = fila[2].ToString();
                        datos[3] = fila[3].ToString();
                        lider = fila[4].ToString();
                        lider = lider + " " + fila[5].ToString();
                        lider = lider + " " + fila[6].ToString();
                        datos[4] = lider;

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
          
            DataTable datosFilaProyecto = controladoraProyecto.consultarProyectoTotal(idProyecto);
            DataTable datosFilaMiembros = controladoraProyecto.consultarMiembrosProyecto(idProyecto);
            DataTable datosOficinaUsuaria = controladoraProyecto.consultarOficina(idProyecto);
            DataTable datosTelefOficinaUsuaria = controladoraProyecto.consultarTelOficina(idProyecto);
            string nombreLider;
            ListItem lider;


            if (datosFilaProyecto.Rows.Count == 1)
            {

                this.txtNombreProy.Text = datosFilaProyecto.Rows[0][0].ToString();
                this.txtObjetivo.Text = datosFilaProyecto.Rows[0][1].ToString();
                this.calendarFecha.SelectedDate = this.calendarFecha.VisibleDate = (System.DateTime)datosFilaProyecto.Rows[0][2];
                if (this.comboEstado.Items.FindByText(datosFilaProyecto.Rows[0][3].ToString()) != null)
                {
                    ListItem estadoProceso = this.comboEstado.Items.FindByText(datosFilaProyecto.Rows[0][3].ToString());
                    this.comboEstado.SelectedValue = estadoProceso.Value;
                }

                nombreLider = datosFilaProyecto.Rows[0][4].ToString();
                nombreLider = nombreLider + " " + datosFilaProyecto.Rows[0][5].ToString();

                if (this.comboLider.Items.FindByText(nombreLider) != null)
                {
                    lider = this.comboLider.Items.FindByText(nombreLider);
                    this.comboLider.SelectedValue = lider.Value;
                }
            }

            if (datosOficinaUsuaria.Rows.Count == 1)
            {
                this.txtnombreOficina.Text = datosOficinaUsuaria.Rows[0][0].ToString();
                this.txtnombreRep.Text = datosOficinaUsuaria.Rows[0][1].ToString();
                this.txtApellido1Rep.Text = datosOficinaUsuaria.Rows[0][2].ToString();
                this.txtApellido2Rep.Text = datosOficinaUsuaria.Rows[0][3].ToString();
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
        }

    }
}