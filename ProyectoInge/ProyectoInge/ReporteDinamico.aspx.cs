﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProyectoInge.App_Code.Capa_de_Control;
using System.Data;
using System.Diagnostics;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using OfficeOpenXml.Style;
using OfficeOpenXml;

namespace ProyectoInge
{
    public partial class ReporteDinamico : System.Web.UI.Page
    {
 
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

        protected void checkBoxTodos_SelectedIndexChanged(object sender, EventArgs e)
        {
               if (checkBoxTodos.Checked == true)
               {
                   this.checkBoxConf.Checked = true;
                   this.checkBoxNC.Checked = true;
                   this.checkBoxPropositoCaso.Checked = true;
                   this.checkBoxResultadoEsperado.Checked = true;
                   this.checkBoxRequerimientosDiseno.Checked = true;
                   this.checkBoxPropositoDiseno.Checked = true;
                   this.checkBoxResponsableDiseno.Checked = true;
                   this.checkBoxEstadoEjecucion.Checked = true;
                   this.checkBoxID_TipoNC.Checked = true;
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
            this.comboProyecto.Items.Clear();
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

        // Genera el reporte en Excel.
        protected void generarReporteExcel()
        {

            ExcelPackage package = new ExcelPackage();
            package.Workbook.Worksheets.Add("Reportes");
            ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
            worksheet.Cells.Style.Font.Size = 12;
            worksheet.Cells.Style.Font.Name = "Calibri";

            // Poner un titulo.
            worksheet.Cells[1, 1].Value = "Reporte de proyectos " + DateTime.Today.ToString("(dd/MM/yyyy).");
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

            Response.Clear();
            Response.Buffer = true;
            Response.BinaryWrite(package.GetAsByteArray());
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;  filename=Reporte.xlsx");
            Response.Flush();
            Response.End();
        }

        // Genera el reporte en PDF.
        protected void generarReportePDF()
        {
            Response.ClearContent();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment; filename=Reporte.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);

            Font headerFont = new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD, BaseColor.BLACK);

            Document pdfDoc = new Document(PageSize.LETTER.Rotate(), 5f, 5f, 5f, 0f);
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);

            pdfDoc.Open();

            iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(Request.MapPath("~/estatico/img/logo.png"));
            logo.Alignment = iTextSharp.text.Image.ALIGN_LEFT;
            logo.ScalePercent(20f);

            PdfPTable table = new PdfPTable(3);
            table.DefaultCell.Border = PdfPCell.BOTTOM_BORDER;

            float[] columnWidths = new float[] { 25f, 50f, 25f };
            table.SetWidths(columnWidths);

            PdfPCell cell = new PdfPCell(logo);
            cell.Border = PdfPCell.BOTTOM_BORDER;

            table.AddCell(cell);
            table.AddCell(new Paragraph("Reporte: Sistema de Gestión de Pruebas"));
            table.AddCell(new Paragraph(DateTime.Now.ToString("dd/MM/yyyy")));

            pdfDoc.Add(table);

            int columns = gridReportes.Rows[0].Cells.Count;
            int rows = gridReportes.Rows.Count;

            PdfPTable elGrid = new PdfPTable(columns);
            elGrid.DefaultCell.Border = PdfPCell.BOX;
            elGrid.HeaderRows = 1;
            elGrid.WidthPercentage = 95f; 

            for (int columnCounter = 0; columnCounter < columns; columnCounter++)
                 {
                    string strValue = gridReportes.HeaderRow.Cells[columnCounter].Text;
                     elGrid.AddCell(new Paragraph(HttpUtility.HtmlDecode(strValue), headerFont));
                 } 

                    for (int rowCounter = 0; rowCounter < rows; rowCounter++)
                     {
                         for (int columnCounter = 0; columnCounter < columns; columnCounter++)
                         {
                             string strValue = gridReportes.Rows[rowCounter].Cells[columnCounter].Text;
                             elGrid.AddCell(new Paragraph(HttpUtility.HtmlDecode(strValue)));
                         }
                     }
                     pdfDoc.Add(elGrid); 

            pdfDoc.Close();

            Response.Write(pdfDoc);
            Response.End();
        }


        //metodo para reiniciar los chechBox
        protected void btnReiniciar_Click(object sender, EventArgs e)
        {
            this.checkBoxConf.Checked = false;
            this.checkBoxEstadoEjecucion.Checked = false;
            this.checkBoxID_TipoNC.Checked = false;
            this.checkBoxNC.Checked = false;
            this.checkBoxPropositoCaso.Checked = false;
            this.checkBoxPropositoDiseno.Checked = false;
            this.checkBoxRequerimientosDiseno.Checked = false;
            this.checkBoxResponsableDiseno.Checked = false;
            this.checkBoxResultadoEsperado.Checked = false;
        }

        protected void btnGenerar_Click(object sender, EventArgs e)
        {
           
            llenarGrid();
        }
        public void llenarGrid()
        {
            DataTable dt = crearTablaRequerimientos();
            int numCols = dt.Rows.Count;
            int indice=0;
            Object[] datos = new Object[numCols];
            for (int i = 0; i < numCols; i++)
            {
                datos[i] = "";
            }
            String proyecto = comboProyecto.Text;
            //METO EL PROYECTO
            if (proyecto != "Seleccione")
            {
                //datos[indice] = proyecto;
                //indice++;
            }
            //METE EL DISENO
            String diseno = comboBoxDiseno.Text;
            if (diseno != "Seleccione")
            {
                //datos[indice] = proyecto;
                //indice++;
            }
            //METO LOS MODULOS
            int numModulos=0;
            for (int i = 0; i < chklistModulos.Items.Count; i++ )
            {
                if (this.chklistModulos.Items[i].Selected == true)//DIGAMOS QUE ESTO NOS DICE QUE EL CHECK ESTA SELECCIONADO
                {
                    numModulos++;
                }
            }
            Object[] datosModulos = new Object[numModulos];
            for (int i = 0; i < numModulos; i++)
            {
                datosModulos[i] = "";
            }
            for (int i = 0; i < chklistModulos.Items.Count; i++ )
            {
                if (this.chklistModulos.Items[i].Selected == true)//DIGAMOS QUE ESTO NOS DICE QUE EL CHECK ESTA SELECCIONADO
                {
                    String modulo = this.chklistModulos.Items[i].Text;
                    datosModulos[i] = modulo;
                }
            }
            //METE LOS REQUERIMIENTOS
            int numRequerimientos = 0;
            for (int i = 0; i < chklistModulos.Items.Count; i++)
            {
                if (this.chklistModulos.Items[i].Selected == true)//DIGAMOS QUE ESTO NOS DICE QUE EL CHECK ESTA SELECCIONADO
                {
                    numRequerimientos++;
                }
            }
            Object[] datosRequerimientos = new Object[numRequerimientos];
            for (int i = 0; i < numModulos; i++)
            {
                datosRequerimientos[i] = "";
            }
            for (int i = 0; i < chklistReq.Items.Count; i++)
            {
                if (this.chklistModulos.Items[i].Selected == true)//DIGAMOS QUE ESTO NOS DICE QUE EL CHECK ESTA SELECCIONADO
                {
                    String requrimiento = this.chklistModulos.Items[i].Text;
                    String idReq = conseguirId(requrimiento);
                    String nombreReq = conseguirNombre(requrimiento);
                    String porcentajeConformidad = calcularPorcentajeConformidad(idReq);
                    String porcentajeNoConformidad = calcularPorcentajeNoConformidad(idReq);
                    String resultado = "Id: " + idReq + "Nombre: "+nombreReq+ "\n %Conformidad: " + porcentajeConformidad + "\n % No conformidad: " + porcentajeNoConformidad;
                    datosModulos[i] = resultado;
                }
            }
            //METE CASOS DE PRUEBA
            if (this.checkBoxPropositoCaso.Checked == true || this.checkBoxResultadoEsperado.Checked == true)
            {
                
            }
            //METE LAS EJECUCIONES DE PRUEBA
            if (this.checkBoxEstadoEjecucion.Checked == true || this.checkBoxID_TipoNC.Checked == true || this.checkBoxNC.Checked == true)
            {
               
            }

            this.gridReportes.DataSource = dt;
            this.gridReportes.DataBind();
        }

        private string conseguirNombre(string requrimiento)
        {
            throw new NotImplementedException();
        }

        private string conseguirId(string requrimiento)
        {
            throw new NotImplementedException();
        }

        private string calcularPorcentajeNoConformidad(string modulo)
        {
            throw new NotImplementedException();
        }

        private string calcularPorcentajeConformidad(string modulo)
        {
            throw new NotImplementedException();
        }
        protected DataTable crearTablaRequerimientos()
        {
            DataTable dt_casos = new DataTable();
            DataColumn columna;
            String proyecto = comboProyecto.Text;
            String diseno = comboBoxDiseno.Text;
            if (proyecto != "Seleccione")
            {
                columna = new DataColumn();
                columna.DataType = System.Type.GetType("System.String");
                columna.ColumnName = "Proyecto";
                dt_casos.Columns.Add(columna);
            }
            if (this.chklistModulos.Items.Count > 0)
            {
                columna = new DataColumn();
                columna.DataType = System.Type.GetType("System.String");
                columna.ColumnName = "Módulo";
                dt_casos.Columns.Add(columna);

            }
            if (this.chklistReq.Items.Count > 0)
            {
                columna = new DataColumn();
                columna.DataType = System.Type.GetType("System.String");
                columna.ColumnName = "Requerimientos";
                dt_casos.Columns.Add(columna);
            }
            if (diseno != null)
            {
                columna = new DataColumn();
                columna.DataType = System.Type.GetType("System.String");
                columna.ColumnName = "Diseño";
                dt_casos.Columns.Add(columna);
            }
            if (this.checkBoxPropositoCaso.Checked == true || this.checkBoxResultadoEsperado.Checked == true)
            {
                columna = new DataColumn();
                columna.DataType = System.Type.GetType("System.String");
                columna.ColumnName = "Caso de Pruebas";
                dt_casos.Columns.Add(columna);
            }
            if (this.checkBoxEstadoEjecucion.Checked == true || this.checkBoxID_TipoNC.Checked == true || this.checkBoxNC.Checked==true)
            {
                columna = new DataColumn();
                columna.DataType = System.Type.GetType("System.String");
                columna.ColumnName = "Estado de ejecucion de Pruebas";
                dt_casos.Columns.Add(columna);
            }
            return dt_casos;
        }


        //metodo para llenar requerimientos del proyecto
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
                    requerimiento = datosReqProyecto.Rows[i][0].ToString();
                    if (chklistModulos.Items.FindByText(requerimiento.Substring(2, 2)) == null)
                    {
                        chklistModulos.Items.Add(requerimiento.Substring(2,2));
                        ++contador;
                    }
                }
            }

            if (1 < contador)
            {
                chklistModulos.Items.Add("Todos los requerimientos");
            }
            chklistModulos.DataBind();
            proyectoUpdate.Update();
            //UpdatePanel2.Update();
            UpdatePanel3.Update();
        }

        //Rosaura
        protected void llenarRequerimientos()
        {
            chklistReq.Items.Clear();
            Object[] modulos;
            int contadorModulos = 0;
            int indice = 0;
            modulos = new Object[chklistModulos.Items.Count];
            for (int i = 0; i < chklistModulos.Items.Count; ++i )
            {
                if(this.chklistModulos.Items[i].Selected == true){
                    ++contadorModulos;
                    modulos[indice] = chklistModulos.Items[i].Text;
                    ++indice;
                }
            }

           DataTable datosReqProyecto = controladoraReporte.consultarReqModulos(modulos,contadorModulos);
           Debug.WriteLine("cantidad de rows es: " + datosReqProyecto.Rows.Count);
            string requerimiento = "";
            int contador = 0;
            requerimiento = "";
        

            if (datosReqProyecto != null && datosReqProyecto.Rows.Count >= 1)
            {
                for (int i = 0; i < datosReqProyecto.Rows.Count; ++i)
                {
                    requerimiento = datosReqProyecto.Rows[i][0].ToString() + " "+ datosReqProyecto.Rows[i][2].ToString();
                    if (chklistReq.Items.FindByText(requerimiento) == null)
                    {
                        Debug.WriteLine("ESTOY POR ACA req");
                        chklistReq.Items.Add(requerimiento);
                        ++contador;
                    }
                }
            }

            if (1 < contador)
            {
                   chklistReq.Items.Add("Todos los requerimientos");
            }
            
               chklistReq.DataBind();
             //  proyectoUpdate.Update();
               UpdatePanel3.Update();
             //  UpdatePanel4.Update();
        }

        protected void proyectoSeleccionado(object sender, EventArgs e)
        {
            int id = controladoraReporte.obtenerIDconNombreProyecto(this.comboProyecto.Text);
            Session["idProyecto"] = id;
            llenarRequerimientosProyecto(id);
            UpdatePanel3.Update();
         //   UpdatePanel4.Update();  
        }

        protected void seleccionarChkListModulos(object sender, EventArgs e)
        {
            int contadorModulos = 0;
            for (int i = 0; i < chklistModulos.Items.Count; ++i )
            {
                if(this.chklistModulos.Items[i].Selected == true){
                    ++contadorModulos;

                }
            }


            if(contadorModulos != 0){
                llenarRequerimientos();
                // proyectoUpdate.Update();
                UpdatePanel3.Update();
                //  UpdatePanel4.Update();
            }else if(contadorModulos == 0){
                chklistReq.Items.Clear();
                UpdatePanel3.Update();
            }
            
        }



        //dropDownListDescargar
        protected void llenarDropDownTipoDescarga()
        {
            this.comboTipoDescarga.Items.Clear();
            Object[] datos = new Object[2];

            datos[0] = "PDF";
            datos[1] = "EXCEL";
            this.comboTipoDescarga.DataSource = datos;
            this.comboTipoDescarga.DataBind();
            UpdatePanel1.Update();
        }

        protected void tipoDescargaSeleccionada(object sender, EventArgs e)
        {
            if (this.comboTipoDescarga.Items[this.comboTipoDescarga.SelectedIndex].Text == "PDF")
            {
                generarReportePDF();
            }
            else
            {
                generarReporteExcel();
            }
            UpdatePanel1.Update();
        }


        protected void llenarDatos()
        {
            DataTable requerimientos;
        }
    }
}

