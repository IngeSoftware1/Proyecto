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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                gridEjecuciones.DataSource = GetTableWithInitialData(); // get first initial data
                gridEjecuciones.DataBind();
            }

        }

        protected void btnInsertar_Click(object sender, EventArgs e)
        {
            this.panelDiseno.Visible = true;
            this.datosDiseno.Visible = true;
        }


        public DataTable GetTableWithInitialData() // this might be your sp for select
        {
            DataTable table = new DataTable();
            DataRow dr;
            table.Columns.Add("TipoNC", typeof(string));
            table.Columns.Add("IdPrueba", typeof(string));
            table.Columns.Add("Descripcion", typeof(string));
            table.Columns.Add("Justificacion", typeof(string));
            table.Columns.Add("Resultados", typeof(string));
            table.Columns.Add("Estado", typeof(string));

            table.Rows.Add("-", "-", "-", "-", "-", "-" );

            return table;
        }  

        protected void btnAddRow_Click(object sender, EventArgs e)
        {
            DataTable dt = GetTableWithNoData(); // get select column header only records not required
            DataRow dr;
            GridViewRow gvr;
            int i = 0;

            
            for (i = 0; i < gridEjecuciones.Rows.Count; ++i)
            {
                gvr = gridEjecuciones.Rows[i];
                Label lblTipoNC = gvr.FindControl("lblTipoNC") as Label;

                if(lblTipoNC.Text != "-")
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

            string comboTipoNC = (gridEjecuciones.FooterRow.FindControl("comboTipoNC") as DropDownList).SelectedItem.Value;
            string idCaso = (gridEjecuciones.FooterRow.FindControl("comboCasoPrueba") as DropDownList).SelectedItem.Value;
            string descripcion = (gridEjecuciones.FooterRow.FindControl("txtDescripcion") as TextBox).Text;
            string justificacion = (gridEjecuciones.FooterRow.FindControl("txtJustificacion") as TextBox).Text;
            string resultados = (gridEjecuciones.FooterRow.FindControl("txtResultados") as TextBox).Text;
            string estado = (gridEjecuciones.FooterRow.FindControl("comboEstado") as DropDownList).SelectedItem.Value;


            dr[0] = comboTipoNC;
            dr[1] = idCaso;
            dr[2] = descripcion;
            dr[3] = justificacion;
            dr[4] = resultados;
            dr[5] = estado;

            dt.Rows.Add(dr); // add grid values in to row and add row to the blank tables

            gridEjecuciones.DataSource = dt; // bind new datatable to grid
            gridEjecuciones.DataBind();
        }

        public DataTable GetTableWithNoData() // returns only structure if the select columns
        {
            DataTable table = new DataTable();
            table.Columns.Add("TipoNC", typeof(string));
            table.Columns.Add("IdPrueba", typeof(string));
            table.Columns.Add("Descripcion", typeof(string));
            table.Columns.Add("Justificacion", typeof(string));
            table.Columns.Add("Resultados", typeof(string));
            table.Columns.Add("Estado", typeof(string));
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


      /*Método para obtener el registro que se desea consulta en el dataGriedViw y mostrar los resultados de la consulta en pantalla.
      * Modifica: el valor del la variable idDiseño con el ID del diseño que se desea consultar.
      * Retorna: no retorna ningún valor
      */
        protected void gridTiposNC_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "seleccionaAgregar")
            {
                DataTable dt = GetTableWithNoData(); // get select column header only records not required
                DataRow dr;
                GridViewRow gvr;
                int i = 0;

                int indice = ((GridViewRow)((LinkButton)e.CommandSource).NamingContainer).RowIndex;

                gvr = gridEjecuciones.Rows[indice];

                string setTipoNC = (gvr.FindControl("lblTipoNC") as Label).Text;
                string setIdCaso = (gvr.FindControl("lblCasoPrueba") as Label).Text;
                string setDescripcion = (gvr.FindControl("lblDescripcion") as Label).Text;
                string setJustificacion = (gvr.FindControl("lblJustificacion") as Label).Text;
                string setResultados = (gvr.FindControl("lblResultados") as Label).Text;
                string setEstado = (gvr.FindControl("lblEstado") as Label).Text;

                for (i = 0; i < gridEjecuciones.Rows.Count; ++i)
                {

                    Debug.WriteLine("cantidad de rows es: " + gridEjecuciones.Rows.Count);
                    Debug.WriteLine("el valor de la i es: " + i);

                    if (i != indice || (i == indice && gridEjecuciones.Rows.Count == 1 ))
                    {
 
                        dr = dt.NewRow();
                        gvr = gridEjecuciones.Rows[i];

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

                        Debug.WriteLine("dr0 tiene: " + dr[0]);
                        Debug.WriteLine("dr1 tiene: " + dr[1]);
                        Debug.WriteLine("dr2 tiene: " + dr[2]);
                        Debug.WriteLine("dr3 tiene: " + dr[3]);


                        dt.Rows.Add(dr); // add grid values in to row and add row to the blank table       
                    }
                    else if(i == indice && gridEjecuciones.Rows.Count == 1)
                    {
                        dr = dt.NewRow();

                        dr[0] = "-";
                        dr[1] = "-";
                        dr[2] = "-"; 
                        dr[3] = "-"; 
                        dr[4] = "-"; 
                        dr[5] = "-"; 

                        Debug.WriteLine("dr0 tiene: " + dr[0]);
                        Debug.WriteLine("dr1 tiene: " + dr[1]);
                        Debug.WriteLine("dr2 tiene: " + dr[2]);
                        Debug.WriteLine("dr3 tiene: " + dr[3]);


                        dt.Rows.Add(dr); // add grid values in to row and add row to the blank table       
                    }

                }



                gridEjecuciones.DataSource = dt; // bind new datatable to grid
                gridEjecuciones.DataBind();


                ListItem itemCombo = (gridEjecuciones.FooterRow.FindControl("comboTipoNC") as DropDownList).Items.FindByText(setTipoNC);
                (gridEjecuciones.FooterRow.FindControl("comboTipoNC") as DropDownList).SelectedValue = itemCombo.Value;
                itemCombo = (gridEjecuciones.FooterRow.FindControl("comboCasoPrueba") as DropDownList).Items.FindByText(setTipoNC);
                (gridEjecuciones.FooterRow.FindControl("comboCasoPrueba") as DropDownList).SelectedValue = itemCombo.Value;
                (gridEjecuciones.FooterRow.FindControl("txtDescripcion") as TextBox).Text = setDescripcion;
                (gridEjecuciones.FooterRow.FindControl("txtJustificacion") as TextBox).Text = setJustificacion;
                (gridEjecuciones.FooterRow.FindControl("txtResultados") as TextBox).Text = setResultados;
                itemCombo = (gridEjecuciones.FooterRow.FindControl("comboEstado") as DropDownList).Items.FindByText(setEstado);
                (gridEjecuciones.FooterRow.FindControl("comboEstado") as DropDownList).SelectedValue = itemCombo.Value;


            }

        }

    }
}