﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Windows.Forms;
using System.Web.UI.WebControls;
using ProyectoInge.App_Code.Capa_de_Control;
using System.Data;
using System.Drawing;
using iTextSharp.text;
using iTextSharp.text.pdf;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Diagnostics;
using System.Web.UI;
///using Word = Microsoft.Office.Interop.Word;
using System.IO;
using System.Text;
using System.Web.UI.HtmlControls;




namespace ProyectoInge
{
    public partial class ReporteDinamico : System.Web.UI.Page
    {

        ControladoraReportes controladoraReporte = new ControladoraReportes();

        private string idProyectoConsultado;
        private static string idDiseñoConsultado;
        private int CANTIDAD_NC = 8;
        private int CANTIDAD_ESTADOS = 4;
        private bool checkBoxVacios = true;

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
                //ponerNombreDeUsuarioLogueado();
                llenarDropDownTipoDescarga();
                if (Session["perfil"].ToString().Equals("Administrador"))
                {
                    llenarComboProyecto(null);
                }
                else
                {
                    llenarComboProyecto(Session["cedula"].ToString());
                }
            }
        }

        /*Metodo para poner el nombre completo del usuario logueado en ese momento
        *Requiere: nada
        *Modifica: el nombre de la persona logueado en un momento determinado en la ventana de RecursosHumanos
        *Retorna: no retorna ningún valor*/
        /*protected void ponerNombreDeUsuarioLogueado()
        {
            DataTable datosFilaFuncionario = controladoraRh.consultarRH(Session["cedula"].ToString());
            if (datosFilaFuncionario.Rows.Count == 1)
            {
                string nombreCompletoUsuarioLogueado = datosFilaFuncionario.Rows[0][1].ToString() + " " + datosFilaFuncionario.Rows[0][2].ToString() + " " + datosFilaFuncionario.Rows[0][3].ToString();
                this.lblLogueado.Text = nombreCompletoUsuarioLogueado;
            }
        }*/

        //llenar el combobox de proyecto
        /* Método para llenar el comboBox los  proyectos de acuerdo a si es miembro o administrador 
     * Modifica: llena el comboBox con los datos obtenidos de la BD
     * Retorna: no retorna ningún valor */


        protected void llenarComboProyecto(string cedulaUsuario)
        {
            Dictionary<string, string> nombres_id_proyectos = new Dictionary<string, string>();
            Dictionary<string, string> id_nombres_proyectos = new Dictionary<string, string>();
            string nombre = "";
            this.comboProyecto.Items.Clear();  //diseño?
            DataTable nombresProyecto;
            int numDatos = 0;
            Object[] datos;
            int indiceProyecto = 1;
            int numColumna = 0;
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
                this.comboProyecto.DataSource = datos;
                this.comboProyecto.DataBind(); Session["vectorIdProyectos"] = nombres_id_proyectos;
                Session["vectorIdNombres"] = id_nombres_proyectos;
            }
            else
            {
                datos = new Object[1];
                datos[0] = "Seleccione";
                this.comboProyecto.DataSource = datos;
                this.comboProyecto.DataBind();
            }
            proyectoUpdate.Update();
        }

        //metodo para llenar el combo de diseño
        //necesito guardar el id del diseño y el propositp
        protected void llenarComboDiseno()
        {
            Dictionary<string, string> id_propositosDisenos = new Dictionary<string, string>();
            int id = 0;
            //Debug.WriteLine("Estoy aca");
            Object[] requerimientos;
            // String cadena = "";
            int numDatos = 0;
            int contadorReq = 0;
            int indice = 0;
            requerimientos = new Object[chklistReq.Items.Count];
            Debug.Write(chklistReq.Items.Count);

            Boolean indicador = false;

            if (this.chklistReq.Items[chklistReq.Items.Count - 1].Selected == true)
            {
                indicador = true;
            }

            for (int i = 0; i < chklistReq.Items.Count; ++i)
            {
                if (this.chklistReq.Items[i].Selected == true || indicador == true)
                {
                    ++contadorReq;
                    //cadena = (chklistReq.Items[i].Text).Substring(0,8);
                    requerimientos[indice] = chklistReq.Items[i].Text;
                    ++indice;
                }
            }

            //meter el id de diseño y el proposito al dictionary

            this.comboBoxDiseno.Items.Clear();
            id = Int32.Parse(Session["idProyecto"].ToString());

            //con el siguiente se devuelven los ids de los diseños y los propositos de los diseños  
            DataTable disenos = controladoraReporte.consultarDisenosReq(id, requerimientos, contadorReq);
            //meter los diseños al combobox diseño
            numDatos = disenos.Rows.Count;

            //ir a la base a la tabla req diseño 
            string idDiseno = "";
            string proposito = "";
            int contador = 0;
            Object[] propositosDiseno;
            int indiceDos = 0;

            Debug.WriteLine("longitud es " + numDatos);

            foreach (DataRow fila in disenos.Rows)
            {
                idDiseno = fila[0].ToString();
                proposito = fila[1].ToString();
                id_propositosDisenos.Add(proposito, idDiseno);
                contador++;
            }


            if (numDatos > 0)
            {
                propositosDiseno = new Object[numDatos + 1];
                propositosDiseno[0] = "Seleccione";

                for (int i = 1; i <= numDatos; i++)
                {

                    propositosDiseno[i] = disenos.Rows[indiceDos][1].ToString();
                    ++indiceDos;
                }
                this.comboBoxDiseno.DataSource = propositosDiseno;
                this.comboBoxDiseno.DataBind();
            }
            else
            {
                propositosDiseno = new Object[1];
                propositosDiseno[0] = "Seleccione";
                this.comboBoxDiseno.DataSource = propositosDiseno;
                this.comboBoxDiseno.DataBind();
            }
            Session["diccionario"] = id_propositosDisenos;
            comboDisenoUpdate.Update();
        }
        protected void llenarComboCaso()
        {
            Dictionary<string, string> nombreCaso_id = new Dictionary<string, string>();

            if (comboBoxDiseno.SelectedIndex.ToString() == "Seleccione")
            {

            }
            else
            {

                DataTable casos = controladoraReporte.consultarCasosAociadosADiseno(Session["idDiseno"].ToString());
                Object[] cProps;
                if (casos.Rows.Count > 0)
                {
                    cProps = new Object[casos.Rows.Count + 2];
                    int c = 0;
                    cProps[0] = "Seleccione";
                    c++;
                    foreach (DataRow fila in casos.Rows)
                    {
                        Debug.Print(fila[1].ToString());
                        nombreCaso_id.Add(fila[1].ToString(), fila[0].ToString());
                        cProps[c] = fila[1].ToString();
                        c++;
                    }
                    cProps[c] = "Todos";
                    this.comboBoxCaso.DataSource = cProps;
                    this.comboBoxCaso.DataBind();
                }
                else
                {
                    cProps = new Object[2];
                    cProps[0] = "Seleccione";
                    this.comboBoxCaso.DataSource = cProps;
                    this.comboBoxCaso.DataBind();

                }
            }
            Session["casos_id"] = nombreCaso_id;
            Debug.Print("updates");
            UpdatePanelCaso.Update();
            updateChklist.Update();
        }


        // Genera el reporte en Excel.
        protected void generarReporteExcel(string date)
        {
            
            Random rnd = new Random();
            int mon = rnd.Next(1, 100);
            string d = "Reporte" + mon + ".xlsx";
            FileInfo newFile = new FileInfo(Server.MapPath(d));
            ExcelPackage xlPackage = new ExcelPackage(newFile);
            ExcelWorksheet worksheet = xlPackage.Workbook.Worksheets.Add("Tinned Goods");

            worksheet.Cells.Style.Font.Size = 12;
            worksheet.Cells.Style.Font.Name = "Calibri";

            // Poner un titulo.
            worksheet.Cells[1, 1].Value = "Reporte " + DateTime.Today.ToString("(dd/MM/yyyy).");
            worksheet.Row(1).Style.Font.Bold = true;
            worksheet.Row(1).Style.Font.Size = 14;

            // Rellenar los datos.
            int c = 1;
            int r = 2;
            // Poner el header.
            foreach (TableCell cell in gridReportes.HeaderRow.Cells)
            {
                worksheet.Cells[r, c++].Value = cell.Text;
            }
            // Dar formato al header.
            worksheet.Row(r).Style.Font.Bold = true;
            worksheet.Row(r).Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            worksheet.Row(r).Style.Border.Bottom.Color.SetColor(System.Drawing.Color.Black);
            r++;
            // Poner el resto de los datos.
            foreach (TableRow row in gridReportes.Rows)
            {
                c = 1;
                foreach (TableCell cell in row.Cells)
                {
                    worksheet.Cells[r, c++].Value = HttpUtility.HtmlDecode(cell.Text);
                }
                // Coloreamos las filas.
                if (0 == r % 2)
                {
                    worksheet.Row(r).Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet.Row(r).Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                }
                r++;
            }

            // Ajustamos el ancho de las columnas.
            worksheet.DefaultColWidth = 10;
            worksheet.Cells.AutoFitColumns();

            xlPackage.Workbook.Properties.Title = "Sample 1";
            xlPackage.Workbook.Properties.Author = "John Tunnicliffe";
            xlPackage.Workbook.Properties.SetCustomPropertyValue("EmployeeID", "1147");
            xlPackage.Save();

            lblModalTitle.Text = "Reporte";
            lblModalBody.Text = "Su reporte en formato excel ha sido creado con éxito.";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
            upModal.Update();

        }
        protected void generarReporteWord()
        {
            Debug.Write("Entro a wors");
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachmnet;filename=ReporteWord.doc");
            Response.ContentType = "application/vnd.ms-Word";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            gridReportes.RenderControl(hw);
            Response.Output.Write(sw);
            Response.Flush();
            Response.End();
        }

        public override void VerifyRenderingInServerForm(System.Web.UI.Control control)
        {

        }

        // Genera el reporte en PDF.
        protected void generarReportePDF()
        {
            Response.ContentType = "application/pdf; charset=UTF-8";
            //para crear or abrir el documento
            iTextSharp.text.Document documento = new iTextSharp.text.Document(PageSize.LETTER.Rotate());
            PdfWriter.GetInstance(documento, new System.IO.FileStream(Server.MapPath("Reportes.pdf"), System.IO.FileMode.Create));
            documento.Open();

            //insertar imagen
            iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(Request.MapPath("~/dinamico/indice.png"));

            logo.Alignment = iTextSharp.text.Image.ALIGN_LEFT;
            logo.ScalePercent(20f);

            PdfPTable encabezado = new PdfPTable(3);
            encabezado.DefaultCell.Border = PdfPCell.BOTTOM_BORDER;
            float[] columnWidths = new float[] { 25f, 50f, 25f };
            encabezado.SetWidths(columnWidths);
            BaseFont baseFont = BaseFont.CreateFont("c:\\WINDOWS\\fonts\\times.ttf", BaseFont.IDENTITY_H, true);
            iTextSharp.text.Font font = new iTextSharp.text.Font(baseFont);

            PdfPCell cell = new PdfPCell(logo);
            cell.Border = PdfPCell.BOTTOM_BORDER;

            encabezado.AddCell(cell);
            encabezado.AddCell(new Paragraph("Reporte: Sistema de Gestión de Pruebas"));
            encabezado.AddCell(new Paragraph(DateTime.Now.ToString("dd/MM/yyyy")));

            documento.Add(encabezado);

            PdfPTable table = new PdfPTable(gridReportes.Rows[0].Cells.Count);
            iTextSharp.text.Font headerFont = new iTextSharp.text.Font(baseFont, 12, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font texto = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 12, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            Phrase p;
            for (int columnCounter = 0; columnCounter < gridReportes.Rows[0].Cells.Count; columnCounter++)
            {
                p = new Phrase(gridReportes.HeaderRow.Cells[columnCounter].Text, headerFont);
                table.AddCell(p);
            }

            foreach (GridViewRow row in gridReportes.Rows)
            {
                for (int columnCounter = 0; columnCounter < gridReportes.Rows[0].Cells.Count; columnCounter++)
                {
                    p = new Phrase(row.Cells[columnCounter].Text, texto);
                    table.AddCell(p);
                }
            }


            documento.Add(table);
            documento.Close();

            Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('" + "Reportes.pdf" + "','_newtab');", true);
            lblModalTitle.Text = "Reporte";
            lblModalBody.Text = "Su reporte en formato PDF ha sido creado con éxito.";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
            upModal.Update();
        }

        /*protected void generarReporteWord()
        {
            Word.Application app;
            //Creamos otro Objeto del Tipo Word Document  
            Word.Document doc;
            try
            {
                // Creamos otro Objeto para interactuar con el Interop 
                Object oMissing = System.Reflection.Missing.Value;
                //Creamos una instancia de una Aplicación Word. 
                app = new Word.ApplicatResponse.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition",
                "attachment;filename=GridViewExport.doc");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-word ";
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                GridView1.AllowPaging = false;
                GridView1.DataBind();
                GridView1.RenderControl(hw);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();

            }*/

        /*protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition",
            "attachment;filename=GridViewExport.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            GridView1.AllowPaging = false;
            GridView1.DataBind();
            //Change the Header Row back to white color
            GridView1.HeaderRow.Style.Add("background-color", "#FFFFFF");
            //Apply style to Individual Cells
            GridView1.HeaderRow.Cells[0].Style.Add("background-color", "green");
            GridView1.HeaderRow.Cells[1].Style.Add("background-color", "green");
            GridView1.HeaderRow.Cells[2].Style.Add("background-color", "green");
            GridView1.HeaderRow.Cells[3].Style.Add("background-color", "green");
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow row = GridView1.Rows[i];
                //Change Color back to white
                row.BackColor = System.Drawing.Color.White;
                //Apply text style to each Row
                row.Attributes.Add("class", "textmode");
                //Apply style to Individual Cells of Alternating Row
                if (i % 2 != 0)
                {
                    row.Cells[0].Style.Add("background-color", "#C2D69B");
                    row.Cells[1].Style.Add("background-color", "#C2D69B");
                    row.Cells[2].Style.Add("background-color", "#C2D69B");
                    row.Cells[3].Style.Add("background-color", "#C2D69B");
                }
            }
            GridView1.RenderControl(hw);
            //style to format numbers to string
            string style = @"<style> .textmode { mso-number-format:\@; } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }*/
        //metodo para reiniciar los chechBox
        protected void btnReiniciar_Click(object sender, EventArgs e)
        {
            this.checkBoxConf.Checked = false;
            this.checkBoxEstadoEjecucion.Checked = false;
            this.checkBoxID_TipoNC.Checked = false;
            this.checkBoxNC.Checked = false;
            this.checkBoxConf.Checked = false;
            this.checkBoxErrores.Checked = false;
            this.checkBoxResponsableDiseno.Checked = false;
            this.checkBoxResultadoEsperado.Checked = false;
            // this.checkBoxTodos.Checked = false;
            this.comboProyecto.SelectedValue = "Seleccione";
            this.chklistModulos.Items.Clear();
            this.chklistReq.Items.Clear();
            this.comboBoxCaso.Items.Clear();
            this.comboBoxDiseno.Items.Clear();
            this.gridReportes.DataSource = null;
            this.gridReportes.DataBind();
            //this.chklistModulos.SelectedItem[i]= false;
        }

        protected void btnGenerar_Click(object sender, EventArgs e)
        {
            //revisar que todos los campos de arriba esten llenos 
            //revisar que entre todos los de abajo al menos este seleccionado uno 
            Debug.Write(faltanDatos());
            if (!faltanDatos())
            {
                llenarGrid();
                Debug.Write("Entro a llenar grid");
            }
            else
            {
                if (checkBoxVacios == true)
                {
                    lblModalTitle.Text = "Error";
                    lblModalBody.Text = "Para generar un nuevo reporte debe seleccionar la(s) casilla(s) con los datos que desea que contenga.";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                    upModal.Update();
                }
                else
                {
                    lblModalTitle.Text = "Error";
                    lblModalBody.Text = "Para generar un nuevo reporte debe completar todos los campos obligatorios.";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                    upModal.Update();
                }
            }
        }

        private bool faltanDatos()
        {
            bool resultado = false;
            if (this.comboProyecto.Text == "Seleccione" || this.comboBoxDiseno.Text == "Seleccione" || this.comboBoxCaso.Text == "Seleccione" || this.comboProyecto.Text == "" || this.comboBoxCaso.Text == "" || this.comboBoxDiseno.Text == "")
            {
                resultado = true;
            }
            if (this.checkBoxConf.Checked == false && this.checkBoxErrores.Checked == false && this.checkBoxEstadoEjecucion.Checked == false && this.checkBoxResponsableDiseno.Checked == false && this.checkBoxID_TipoNC.Checked == false && this.checkBoxNC.Checked == false && this.checkBoxResultadoEsperado.Checked == false)
            {
                checkBoxVacios = true;
                resultado = true;
            }
            return resultado;
        }

        public void llenarGrid()
        {
            DataTable dt = crearTablaRequerimientos();
            int numCols = dt.Columns.Count;

            //int indice=0;
            Object[] datos = new Object[numCols];


            String proyecto = comboProyecto.Text;

            int i = 0;
            //METO EL PROYECTO
            if (proyecto != "Seleccione")
            {
                Debug.Write("!!!!>" + proyecto);
                datos[i] = proyecto;
                i++;
            }
            String diseno = comboBoxDiseno.Text;
            if (diseno != "Seleccione")
            {
                if (this.checkBoxResponsableDiseno.Checked == true)
                {
                    DataTable responsable = controladoraReporte.consultarResponsableDiseno(diseno);
                    diseno += "\n";
                    foreach (DataRow fila in responsable.Rows)
                    {
                        diseno += " Responsable: " + fila[0].ToString() + " " + fila[1].ToString() + "\n";
                    }
                }
                datos[i] = diseno;
                i++;
            }
            //METE EL CASO
            String caso = comboBoxCaso.Text;
            Object[] casosObj = null;
            int numCasos = 0;
            int siHayCasoSeleccionado = 0;
            if (caso != "Seleccione")
            {
                if (caso == "Todos")
                {
                    caso = "";
                    Debug.Write("Todos los casos");
                    DataTable casos = controladoraReporte.consultarCasosAociadosADiseno(Session["idDiseno"].ToString());
                    numCasos = casos.Rows.Count;
                    casosObj = new Object[numCasos];
                    if (casos.Rows.Count > 0)
                    {
                        int c = 0;
                        foreach (DataRow fila in casos.Rows)
                        {
                            Debug.Write(caso);
                            caso += fila[1].ToString() + "\n";
                            if (this.checkBoxResultadoEsperado.Checked == true)
                            {
                                DataTable resultadoEsperado = controladoraReporte.consultarResultadoCaso(fila[1].ToString());
                                foreach (DataRow fila1 in resultadoEsperado.Rows)
                                {
                                    caso += " Resultado esperdao: " + fila1[0].ToString() + "\n";
                                }
                            }
                            casosObj[c] = fila[1].ToString();
                            c++;
                            siHayCasoSeleccionado++;
                        }
                    }
                }
                else
                {
                    if (this.checkBoxResultadoEsperado.Checked == true)
                    {
                        caso += "";
                        DataTable resultadoEsperado = controladoraReporte.consultarResultadoCaso(caso);
                        foreach (DataRow fila in resultadoEsperado.Rows)
                        {
                            Debug.WriteLine("estos son los casos respectivos" + caso);
                            caso += " Resultado esperdao: " + fila[0].ToString() + "\n";
                        }
                    }
                }
                datos[i] = caso;
                i++;
            }
            //METO LOS MODULOS
            String modulos = "";
            if (chklistModulos.Items[chklistModulos.Items.Count - 1].Selected == true)
            {
                for (int j = 0; j < chklistModulos.Items.Count; ++j)
                {
                    Debug.WriteLine("YO ESTOY EN CLICLO DE MODULOS");
                    modulos += this.chklistModulos.Items[j].ToString() + "\n";
                }
            }
            else
            {
                for (int j = 0; j < chklistModulos.Items.Count; ++j)
                {
                    if (this.chklistModulos.Items[j].Selected == true)
                    {
                        modulos += this.chklistModulos.Items[j].ToString() + "\n";
                    }
                }
            }
            datos[i] = modulos;
            i++;
            //METE LOS REQUERIMIENTOS
            int numRequerimientos = 0;
            String requerimientos = "";
            for (int j = 0; j < chklistReq.Items.Count; ++j)
            {
                if (this.chklistReq.Items[j].Selected == true)
                {
                    ++numRequerimientos;
                }
            }
            if (numRequerimientos > 0)
            {
                requerimientos = "";
                for (int j = 0; j < chklistReq.Items.Count; ++j)
                {
                    if (this.chklistReq.Items[j].Selected == true)
                    {
                        requerimientos += this.chklistReq.Items[j].ToString() + "\n";
                    }
                }
            }
            else if (chklistReq.Items[chklistReq.Items.Count - 1].Selected == true)
            {
                for (int j = 0; j < chklistReq.Items.Count; ++j)
                {
                    Debug.Write("!!!>1");
                    requerimientos += this.chklistReq.Items[j].ToString() + "\n";
                }
            }
            datos[i] = requerimientos;
            i++;
            //METE LAS EJECUCIONES DE PRUEBA
            DataTable ejecs = null;
            String ejecuciones = "-";
            Debug.Write("!!!EJECUCIONES");
            if (this.checkBoxEstadoEjecucion.Checked == true || this.checkBoxID_TipoNC.Checked == true)
            {
                ejecuciones = "";
                if (this.checkBoxEstadoEjecucion.Checked == true && this.checkBoxID_TipoNC.Checked == true)
                {
                    Debug.Write("!!!>AMBOS");
                    ejecs = controladoraReporte.consultarEjecuciones(casosObj, numCasos, 1);
                    if (ejecs != null)
                    {
                        foreach (DataRow fila in ejecs.Rows)
                        {
                            String parte = "";
                            parte = "Id caso:" + fila[0].ToString() + "Id tipo no conformidad: " + fila[1].ToString() + "Estado ejecución: " + fila[2].ToString() + "\n";
                            ejecuciones += parte;
                        }
                    }
                    else
                    {
                        Debug.Write("Viene nula");
                    }
                }
                else if (this.checkBoxEstadoEjecucion.Checked == true && this.checkBoxID_TipoNC.Checked == false)
                {
                    Debug.Write("!!!>SOLO ESTADO");
                    ejecs = controladoraReporte.consultarEjecuciones(casosObj, numCasos, 2);
                    if (ejecs != null)
                    {
                        foreach (DataRow fila in ejecs.Rows)
                        {
                            String parte = "";
                            parte = "Id caso:" + fila[0].ToString() + "Estado ejecución: " + fila[1].ToString() + "\n";
                            ejecuciones += parte;
                        }
                    }
                }
                else if (this.checkBoxEstadoEjecucion.Checked == false && this.checkBoxID_TipoNC.Checked == true)
                {
                    Debug.Write("!!!>SOLO TIPONOC");
                    ejecs = controladoraReporte.consultarEjecuciones(casosObj, numCasos, 3);
                    if (ejecs != null)
                    {
                        foreach (DataRow fila in ejecs.Rows)
                        {
                            Debug.Write("1");
                            ejecs = controladoraReporte.consultarEjecuciones(casosObj, numCasos, 2);
                            String parte = "";
                            parte = "Id caso:" + fila[0].ToString() + "Id tipo no conformidad: " + fila[1].ToString() + "\n";
                            ejecuciones += parte;
                        }
                    }
                }
                datos[i] = ejecuciones;
                i++;
            }

            //METE LAS METRICAS
            String metricas = "-";
            if (this.checkBoxConf.Checked == true)
            {
                datos[i] = calcularPorcentajeConformidad(Int32.Parse(Session["idCaso"].ToString()));
                i++;
            }

            if (this.checkBoxNC.Checked == true)
            {

                datos[i] = calcularPorcentajeNoConformidad(Int32.Parse(Session["idCaso"].ToString()), 0);
                i++;

            }

            if (this.checkBoxErrores.Checked == true)
            {

                datos[i] = calcularPorcentajeNoConformidad(Int32.Parse(Session["idCaso"].ToString()), 1);
                i++;
            }


            dt.Rows.Add(datos);
            this.gridReportes.DataSource = dt;
            this.gridReportes.DataBind();
        }


        /**metodo para calcular porcentaje de conformidad, recibe el indice del caso que se requiere
         * en caso de que se desee hacer la medida para todos los casos del diseño, se ingresa un -1
        **/
        private string calcularPorcentajeNoConformidad(int idCaso, int tipo)
        {
            string[] arreglo = new string[CANTIDAD_NC];
            double[] contador = new double[CANTIDAD_NC];
            DataTable casosEjecutados;
            DataTable estadosDeCasos;
            String resultado = "";
            int indice = 0;
            int indiceArreglo = 0;
            double contadorGeneral = 0.0;
            for (int i = 0; i < CANTIDAD_NC; i++)
            {
                arreglo[i] = "";
                contador[i] = 0.0;
            }

            if (idCaso != -1)
            {
                resultado = controladoraReporte.consultarTipoNC_Caso(idCaso);
            }
            else
            {
                casosEjecutados = controladoraReporte.consultarCasosAociadosADiseno(Session["idDiseno"].ToString());
                estadosDeCasos = controladoraReporte.consultarEstadosDeCasos(casosEjecutados);

                while (indice < estadosDeCasos.Rows.Count && estadosDeCasos != null && indiceArreglo < CANTIDAD_NC)
                {

                    if (arreglo[indiceArreglo].ToString().Equals("") == true)
                    {
                        arreglo[indiceArreglo] = estadosDeCasos.Rows[indice][1].ToString();
                        contador[indiceArreglo] = contador[indiceArreglo] + 1.0;
                        ++contadorGeneral;
                        ++indice;
                        indiceArreglo = 0;
                    }
                    else
                    {
                        if (arreglo[indiceArreglo].Equals(estadosDeCasos.Rows[indice][1]) == true)
                        {
                            contador[indiceArreglo] = contador[indiceArreglo] + 1.0;
                            ++indice;
                            ++contadorGeneral;
                            indiceArreglo = 0;
                        }
                        else
                        {
                            ++indiceArreglo;
                        }
                    }

                }

                int indiceDos = 0;
                Boolean indicador = false;
                String s;

                if (tipo == 0)
                {
                    while (indiceDos < CANTIDAD_NC && indicador == false)
                    {

                        if (arreglo[indiceDos].Equals("") == false)
                        {
                            contador[indiceDos] = (contador[indiceDos] / contadorGeneral) * 100.0;
                            s = string.Format("{0:N2}%", contador[indiceDos]);
                            resultado = resultado + arreglo[indiceDos] + s + " ";
                            ++indiceDos;
                        }
                        else
                        {
                            indicador = true;
                        }
                    }
                }
                else
                {
                    indicador = false;
                    indiceDos = 0;
                    while (indiceDos < CANTIDAD_NC && indicador == false)
                    {

                        if (arreglo[indiceDos].Equals("") == false)
                        {
                            resultado = resultado + arreglo[indiceDos] + " " + contador[indiceDos] + " ";
                            ++indiceDos;
                        }
                        else
                        {
                            indicador = true;
                        }
                    }
                }
            }

            return resultado;
        }

        private string calcularPorcentajeConformidad(int idCaso)
        {
            string[] arreglo = new string[CANTIDAD_ESTADOS]; //SON 4 ESTADOS
            double[] contador = new double[CANTIDAD_ESTADOS];
            DataTable casosEjecutados;
            DataTable estadosDeCasos;
            String resultado = "";
            int indice = 0;
            int indiceArreglo = 0;
            double contadorGeneral = 0.0;
            for (int i = 0; i < CANTIDAD_ESTADOS; i++)
            {
                arreglo[i] = "";
                contador[i] = 0.0;
            }

            if (idCaso != -1)//hay que obtener el estado de los casos ejecutados con respecto al id especifico
            {
                resultado = controladoraReporte.consultarEstadosConIdCaso(idCaso);
            }
            else
            { ////////
                casosEjecutados = controladoraReporte.consultarCasosAociadosADiseno(Session["idDiseno"].ToString());
                estadosDeCasos = controladoraReporte.consultarEstadosDeCasos(casosEjecutados);

                while (indice < estadosDeCasos.Rows.Count && estadosDeCasos != null && indiceArreglo < CANTIDAD_ESTADOS)
                {

                    if (arreglo[indiceArreglo].ToString().Equals("") == true)
                    {
                        arreglo[indiceArreglo] = estadosDeCasos.Rows[indice][1].ToString();
                        contador[indiceArreglo] = contador[indiceArreglo] + 1.0;
                        ++contadorGeneral;
                        ++indice;
                        indiceArreglo = 0;
                    }
                    else
                    {
                        if (arreglo[indiceArreglo].Equals(estadosDeCasos.Rows[indice][1]) == true)
                        {
                            contador[indiceArreglo] = contador[indiceArreglo] + 1.0;
                            ++indice;
                            ++contadorGeneral;
                            indiceArreglo = 0;
                        }
                        else
                        {
                            ++indiceArreglo;
                        }
                    }

                }

                int indiceDos = 0;
                Boolean indicador = false;
                String s;
                while (indiceDos < CANTIDAD_ESTADOS && indicador == false)
                {

                    if (arreglo[indiceDos].Equals("") == false)
                    {
                        contador[indiceDos] = (contador[indiceDos] / contadorGeneral) * 100.0;
                        s = string.Format("{0:N2}%", contador[indiceDos]);
                        resultado = resultado + arreglo[indiceDos] + s + " ";
                        ++indiceDos;
                    }
                    else
                    {
                        indicador = true;
                    }
                }
            }

            return resultado;
        }

        protected DataTable crearTablaRequerimientos()
        {
            DataTable dt_casos = new DataTable();
            DataColumn columna;
            String proyecto = comboProyecto.Text;
            String diseno = comboBoxDiseno.Text;
            String caso = comboBoxCaso.Text;
            int numModulos = 0;
            int numReqs = 0;
            if (proyecto != "Seleccione")
            {
                columna = new DataColumn();
                columna.DataType = System.Type.GetType("System.String");
                columna.ColumnName = "Proyecto";
                dt_casos.Columns.Add(columna);
            }
            if (diseno != "Seleccione")
            {
                columna = new DataColumn();
                columna.DataType = System.Type.GetType("System.String");
                columna.ColumnName = "Diseño";
                dt_casos.Columns.Add(columna);
            }
            if (caso != "Seleccione")
            {
                columna = new DataColumn();
                columna.DataType = System.Type.GetType("System.String");
                columna.ColumnName = "Caso de Pruebas";
                dt_casos.Columns.Add(columna);
            }

            if (chklistModulos.Items.Count > 0)
            {
                columna = new DataColumn();
                columna.DataType = System.Type.GetType("System.String");
                columna.ColumnName = "Modulo";
                dt_casos.Columns.Add(columna);

            }
            for (int i = 0; i < chklistReq.Items.Count; ++i)
            {
                if (this.chklistReq.Items[i].Selected == true)
                {
                    ++numReqs;
                }
            }
            Debug.Write("!!!!!!!>" + numReqs);

            if (numReqs > 0)
            {
                columna = new DataColumn();
                columna.DataType = System.Type.GetType("System.String");
                columna.ColumnName = "Requerimientos";
                dt_casos.Columns.Add(columna);
            }
            if (this.checkBoxEstadoEjecucion.Checked == true || this.checkBoxID_TipoNC.Checked == true)
            {
                columna = new DataColumn();
                columna.DataType = System.Type.GetType("System.String");
                columna.ColumnName = "Estado de ejecucion de Pruebas";
                dt_casos.Columns.Add(columna);
            }
            if (this.checkBoxConf.Checked == true)
            {
                columna = new DataColumn();
                columna.DataType = System.Type.GetType("System.String");
                columna.ColumnName = "Estado";
                dt_casos.Columns.Add(columna);
            }

            if (this.checkBoxNC.Checked == true)
            {
                columna = new DataColumn();
                columna.DataType = System.Type.GetType("System.String");
                columna.ColumnName = "Tipo de no conformidad";
                dt_casos.Columns.Add(columna);
            }

            if (this.checkBoxErrores.Checked == true)
            {
                columna = new DataColumn();
                columna.DataType = System.Type.GetType("System.String");
                columna.ColumnName = "Cantidad de errores";
                dt_casos.Columns.Add(columna);
            }


            return dt_casos;
        }

        //metodo para llenar los modulos morequerimientos del proyecto
        protected void llenarRequerimientosProyecto(int idProyecto)
        {
            DataTable datosReqProyecto = controladoraReporte.consultarReqProyecto(idProyecto);
            string requerimiento = "";
            int contador = 0;
            requerimiento = "";
            chklistModulos.Items.Clear();

            if (datosReqProyecto != null && datosReqProyecto.Rows.Count >= 1)
            {
                for (int i = 0; i < datosReqProyecto.Rows.Count; ++i)
                {
                    //se gurdaran en requerimiento todos los ids de los requerimeintos
                    requerimiento = datosReqProyecto.Rows[i][0].ToString();
                    if (chklistModulos.Items.FindByText(requerimiento.Substring(3, 2)) == null)
                    {
                        chklistModulos.Items.Add(requerimiento.Substring(3, 2));
                        ++contador;
                    }
                }
            }

            if (1 < contador)
            {
                chklistModulos.Items.Add("Todos los módulos");
            }
            chklistModulos.DataBind();
            proyectoUpdate.Update();
            updateChklist.Update();
        }

        //Rosaura
        protected void llenarRequerimientos()
        {
            Dictionary<string, string> id_nombre_req = new Dictionary<string, string>();
            chklistReq.Items.Clear();
            Object[] modulos;
            int contadorModulos = 0;
            int indice = 0;
            modulos = new Object[chklistModulos.Items.Count];
            Boolean indicador = false;

            if (this.chklistModulos.Items[chklistModulos.Items.Count - 1].Selected == true)
            {
                indicador = true;
            }

            for (int i = 0; i < chklistModulos.Items.Count; ++i)
            {
                if (this.chklistModulos.Items[i].Selected == true || indicador == true)
                {
                    ++contadorModulos;
                    modulos[indice] = chklistModulos.Items[i].Text;
                    Debug.WriteLine("dsfsdfsdf" + chklistModulos.Items[i].Text);
                    ++indice;
                }
            }
            DataTable datosReqProyecto = controladoraReporte.consultarReqModulos(modulos, contadorModulos);
            string requerimiento = "";
            int contador = 0;
            requerimiento = "";


            if (datosReqProyecto != null && datosReqProyecto.Rows.Count >= 1)
            {
                for (int i = 0; i < datosReqProyecto.Rows.Count; ++i)
                {
                    requerimiento = datosReqProyecto.Rows[i][0].ToString() + " " + datosReqProyecto.Rows[i][2].ToString();
                    if (chklistReq.Items.FindByText(requerimiento) == null)
                    {
                        ///se guarda el id del requerimiento con su nombre
                        id_nombre_req.Add(datosReqProyecto.Rows[i][0].ToString(), datosReqProyecto.Rows[i][2].ToString());
                        chklistReq.Items.Add(requerimiento);
                        ++contador;
                    }
                }
            }
            //se añade la opcion de Todos los requerimientos cuando se añade en el checklist uno o mas requerimientos
            if (1 < contador)
            {
                chklistReq.Items.Add("Todos los requerimientos");
            }
            chklistReq.DataBind();
            proyectoUpdate.Update();
            updateChklist.Update();
        }

        protected void proyectoSeleccionado(object sender, EventArgs e)
        {
            int id = -1;
            if (this.comboProyecto.Text.Equals("Seleccione") == false)
            {
                id = controladoraReporte.obtenerIDconNombreProyecto(this.comboProyecto.Text);
                Session["idProyecto"] = id;
                llenarRequerimientosProyecto(id);
                updateChklist.Update();
            }
            else
            {
                chklistModulos.Items.Clear();
                chklistReq.Items.Clear();
                comboBoxDiseno.Items.Clear();
                comboBoxCaso.Items.Clear();
                comboDisenoUpdate.Update();
                UpdatePanelCaso.Update();
                updateChklist.Update();
            }
        }


        protected void disenoSeleccionado(object sender, EventArgs e)
        {
            int id = -1;
            String propDiseno = comboBoxDiseno.SelectedValue.ToString();
            Dictionary<string, string> disenos = (Dictionary<string, string>)Session["diccionario"];
            String idDiseno = "";

            if (this.comboBoxDiseno.Text.Equals("Seleccione") == false)
            {
                disenos.TryGetValue(propDiseno, out idDiseno);
                id = Int32.Parse(idDiseno);
                Debug.WriteLine("id del caso" + id);

                Session["idDiseno"] = id;
                llenarComboCaso();
                UpdatePanelCaso.Update();
            }
        }


        protected void casoSeleccionado(object sender, EventArgs e)
        {


            Dictionary<string, string> casosPrueba_id = (Dictionary<string, string>)Session["casos_id"];
            String idCasoString = "";

            Debug.WriteLine("soy yo el caso" + comboBoxCaso.Text);

            if (this.comboBoxCaso.Text.Equals("Seleccione") == false && this.comboBoxCaso.Text.Equals("Todos") == false)
            {
                casosPrueba_id.TryGetValue(this.comboBoxCaso.Text, out idCasoString);


                Session["idCaso"] = idCasoString;


                //UpdatePanelCaso.Update();
            }
            else
            {
                Session["idCaso"] = -1;
                //UpdatePanelCaso.Update();
            }

        }


        protected void seleccionarChkListModulos(object sender, EventArgs e)
        {
            int contadorModulos = 0;
            for (int i = 0; i < chklistModulos.Items.Count; ++i)
            {
                if (this.chklistModulos.Items[i].Selected == true)
                {
                    ++contadorModulos;

                }
            }


            if (contadorModulos != 0)
            {
                llenarRequerimientos();
                updateChklist.Update();
            }
            else if (contadorModulos == 0)
            {
                chklistReq.Items.Clear();
                this.comboBoxDiseno.Items.Clear();
                comboDisenoUpdate.Update();
                updateChklist.Update();
            }
        }

        //dropDownListDescargar
        protected void llenarDropDownTipoDescarga()
        {
            this.comboTipoDescarga.Items.Clear();
            Object[] datos = new Object[4];

            datos[0] = "Seleccione";
            datos[1] = "PDF";
            datos[2] = "EXCEL";
            datos[3] = "WORD";
            this.comboTipoDescarga.DataSource = datos;
            this.comboTipoDescarga.DataBind();
            UpdatePanel1.Update();
        }

        protected void tipoDescargaSeleccionada(object sender, EventArgs e)
        {
            if (this.comboTipoDescarga.Items[this.comboTipoDescarga.SelectedIndex].Text == "PDF")
            {
                Debug.Write("eNTRO A GENERAR EN PDF");
                generarReportePDF();
            }
            else if (this.comboTipoDescarga.Items[this.comboTipoDescarga.SelectedIndex].Text == "WORD")
            {
                generarReporteWord();
            }
            else
            {
                Debug.Write("eNTRO A GENERAR EN EXCEL");
                string date = "";
                date = DateTime.Now.ToString("dd/MM/yyyy") + "_" + DateTime.Now.ToString("hh:mm:ss");
                generarReporteExcel(date);
            }
            UpdatePanel1.Update();
        }

        protected void seleccionarChkListReq(object sender, EventArgs e)
        {
            int contadorReq = 0;
            for (int i = 0; i < chklistReq.Items.Count; ++i)
            {
                if (this.chklistReq.Items[i].Selected == true)
                {
                    ++contadorReq;
                }
            }

            this.comboBoxDiseno.Items.Clear();
            comboDisenoUpdate.Update();

            if (contadorReq != 0)
            {
                llenarComboDiseno();
                comboDisenoUpdate.Update();

            }
            else if (contadorReq == 0)
            {
                chklistReq.Items.Clear();
                chklistModulos.ClearSelection();
                updateChklist.Update();
                comboDisenoUpdate.Update();
            }
        }
    }
}

