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
    public partial class ReporteDinamico : System.Web.UI.Page
    {
        ControladoraProyecto controladoraProyecto = new ControladoraProyecto();
        ControladoraReportes controladoraReporte = new ControladoraReportes();
        private string idProyectoConsultado;
        private static string idDiseñoConsultado;

        /*Dictionary<string, string> cedulasRepresentantes = new Dictionary<string, string>();
        Dictionary<string, string> nombreRepresentantesConsultados = new Dictionary<string, string>();
        private static int modo = 1; //1 insertar, 2 modificar, 3 eliminar*/

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["cedula"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }

            if (!IsPostBack)
            {
                /*ponerNombreDeUsuarioLogueado();
                controlarCampos(false);
                cambiarEnabledTxtCalendar(false);
                cambiarEnabled(false, this.btnModificar);
                cambiarEnabled(false, this.btnEliminar);
                cambiarEnabled(false, this.btnAceptar);
                cambiarEnabled(false, this.btnCancelar);
                cambiarEnabledManosReq(false, this.lnkAgregarReq);
                cambiarEnabledManosReq(false, this.lnkQuitarReq);
                cambiarEnabled(true, this.btnInsertar);
                llenarComboNivel();
                llenarComboTecnica();
                if (Session["perfil"].ToString().Equals("Administrador"))
                {
                    llenarComboProyecto(null);
                    llenarComboRecursos();
                    llenarGrid(null);
                }
                else
                {
                    llenarComboProyecto(Session["cedula"].ToString());
                    llenarComboRecursos();
                    llenarGrid(Session["cedula"].ToString());
                }*/

            }
        }

        //llenar el combobox de proyecto
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


            //this.listReqProyecto.Items.Clear();

            if (cedulaUsuario == null)
            {

                nombresProyecto = controladoraReporte.consultarNombresProyectos();
                if (nombresProyecto != null && nombresProyecto.Rows.Count > 0)
                {
                    numDatos = nombresProyecto.Rows.Count;
                }
            }
            else
            {
                nombresProyecto = controladoraReporte.consultarProyectosLider(cedulaUsuario);
                if (nombresProyecto == null || nombresProyecto.Rows.Count == 0)
                {
                    nombresProyecto = controladoraReporte.consultarProyectosDeUsuario(cedulaUsuario);
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
                this.controladoraReporte.DataSource = datos;
                this.controladoraReporte.DataBind();
                Session["vectorIdProyectos"] = nombres_id_proyectos;
                Session["vectorIdNombres"] = id_nombres_proyectos;
            }
            else
            {
                datos = new Object[1];
                datos[0] = "Seleccione";
                this.controladoraReporte.DataSource = datos;
                this.controladoraReporte.DataBind();
            }
            UpdateAsociarDesasociarModulos.Update();
            proyectoUpdate.Update();
        }

    }
}