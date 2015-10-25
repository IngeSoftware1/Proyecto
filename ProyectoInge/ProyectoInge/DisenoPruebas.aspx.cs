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

        ControladoraDiseno controladoraDiseno = new ControladoraDiseno();
        private string idProyectoConsultado;
        private string idDiseñoConsultado;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["cedula"] == null)
            {
                Response.Redirect("~/Login.aspx");

            }

            if (!IsPostBack)
            {
                controlarCampos(false);
                cambiarEnabled(false, this.btnModificar);
                cambiarEnabled(false, this.btnEliminar);
                cambiarEnabled(false, this.btnAceptar);
                cambiarEnabled(false, this.btnCancelar);
                
            }


            if (Session["perfil"].ToString().Equals("Administrador"))
            {

                cambiarEnabled(true, this.btnInsertar);
                //llenarGrid(null);
            }
            else
            {
                cambiarEnabled(false, this.btnInsertar);
                //llenarGrid(Session["cedula"].ToString());
            }
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


            if(cedulaUsuario.Equals("Administrador")==true){

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
                datos = new Object[numDatos];

                for (int i = 0; i < Niveles.Rows.Count; ++i)
                {
                    datos[i] = Niveles.Rows[i][0].ToString();
                }

                this.comboNivel.DataSource = datos;
                this.comboNivel.DataBind();
            }

        
        }

            /* Método para llenar el comboBox de los diferentes tipos de prueba que existen
           * Modifica: llena el comboBox con los datos obtenidos de la BD
           * Retorna: no retorna ningún valor */

            protected void llenarComboTipo()
            {
                this.comboTipo.Items.Clear();
                DataTable Tipos = controladoraDiseno.consultarTipos();
                int numDatos = Tipos.Rows.Count;
                Object[] datos;


                if (Tipos.Rows.Count >= 1)
                {
                    numDatos = Tipos.Rows.Count;
                    datos = new Object[numDatos];

                    for (int i = 0; i < Tipos.Rows.Count; ++i)
                    {
                        datos[i] = Tipos.Rows[i][0].ToString();
                    }

                    this.comboTipo.DataSource = datos;
                    this.comboTipo.DataBind();
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
                    datos = new Object[numDatos];

                    for (int i = 0; i < Tecnicas.Rows.Count; ++i)
                    {
                        datos[i] = Tecnicas.Rows[i][0].ToString();
                    }

                    this.comboTecnica.DataSource = datos;
                    this.comboTecnica.DataBind();
                }


            }


            /* Método para llenar el comboBox de los recursos
              * Modifica: llena el comboBox con los datos obtenidos de la BD
              * Retorna: no retorna ningún valor */

            protected void llenarComboRecursos()
            {
                this.comboTipo.Items.Clear();
                DataTable Recursos = controladoraDiseno.consultarRecursos(idProyectoConsultado);
                int numDatos = Recursos.Rows.Count;
                Object[] datos;


                if (Recursos.Rows.Count >= 1)
                {
                    numDatos = Recursos.Rows.Count;
                    datos = new Object[numDatos];

                    for (int i = 0; i < Recursos.Rows.Count; ++i)
                    {
                        datos[i] = Recursos.Rows[i][0].ToString();
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
        
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {

        }

        protected void gridDisenos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
           
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
    }
}