using System;
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

        protected void checkBoxTodos_CheckedChanged(object sender, EventArgs e)
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
            for (int i = 0; i < chklistReq.Items.Count; ++i)
            {
                if (this.chklistReq.Items[i].Selected == true)
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

            Debug.WriteLine("longitud es "+ numDatos);
            
            foreach (DataRow fila in disenos.Rows)
            {
                idDiseno = fila[0].ToString();
                proposito = fila[1].ToString();
                id_propositosDisenos.Add(proposito,idDiseno);
                contador++;
            }


            if (numDatos >0)
            {
                propositosDiseno = new Object[numDatos+1];
                propositosDiseno[0] = "Seleccione";
             
                for(int i = 1; i<=numDatos; i++ )
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
            if (comboBoxDiseno.SelectedIndex.ToString() == "Seleccione")
            {

            }
            else {

                DataTable casos = controladoraReporte.consultarCasosAociadosADiseno(Session["idDiseno"].ToString());
                Object[] cProps;
                if (casos.Rows.Count>0)
                {
                    Debug.Print("num casos mayor a 0");
                    cProps = new Object[casos.Rows.Count + 1];
                    int c = 0;
                    cProps[0] = "Seleccione";
                    c++;
                    foreach (DataRow fila in casos.Rows)
                    {
                        Debug.Print(fila[0].ToString());
                        cProps[c]=fila[0].ToString();
                        c++;
                    }

                    this.comboBoxCaso.DataSource = cProps;
                    this.comboBoxCaso.DataBind();
                }
                else
                {
                    cProps = new Object[1];
                    cProps[0] = "Seleccione";
                    this.comboBoxCaso.DataSource = cProps;
                    this.comboBoxCaso.DataBind();
                    
                }
            }
            Debug.Print("updates");
            UpdatePanelCaso.Update();
            updateChklist.Update();
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

            //Font headerFont = new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD, BaseColor.BLACK);

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
                //elGrid.AddCell(new Paragraph(HttpUtility.HtmlDecode(strValue), headerFont));
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
            if (!faltanDatos())
            {
                llenarGrid();
            }
        }

        private bool faltanDatos()
        {
            return false;
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
                Debug.Write("!!!!>"+proyecto);
                datos[i] = proyecto;
                i++;
                
            }
            
            //METE EL DISENO
            String diseno = comboBoxDiseno.Text;
            if (diseno != "Seleccione")
            {
                datos[i] = diseno;
                i++;
            } 
            //METE EL CASO
            //VARIABLE SESION, QUE ME DICE SI SE SELECCIONA SOLO UN CASO SI NO
            String caso = comboBoxDiseno.Text;
            if (caso != "Seleccione")
            {
                datos[i] = diseno;
                i++;
            }
            else if (caso != "Todos")
            {

            }
            //METO LOS MODULOS
            int numModulos=0;
            String modulos="-";
            for (int j = 0; j < chklistModulos.Items.Count - 1; ++j)
            {
                if (this.chklistModulos.Items[j].Selected == true)
                {
                    ++numModulos;
                }
            }
            Debug.Write("!!!>" + numModulos);
            if (numModulos > 0 )
            {
                for (int j = 0; j < chklistModulos.Items.Count - 1; ++j)
                {
                    Debug.Write("!!!>0");
                    if (this.chklistModulos.Items[j].Selected == true)
                    {
                        modulos += this.chklistModulos.Items[j].ToString() + "\n";
                    }
                }
            } else if(chklistModulos.Items[chklistModulos.Items.Count - 1].Selected == true){
                for (int j = 0; j < chklistModulos.Items.Count - 1; ++j)
                {
                    Debug.Write("!!!>1" );
                    modulos += this.chklistModulos.Items[j].ToString() +"\n";
                }
            }
            datos[i] = modulos;
            i++;
            //METE LOS REQUERIMIENTOS
            int numRequerimientos = 0;
            String requerimientos="-";
            for (int j = 0; j < chklistReq.Items.Count - 1; ++j)
            {
                if (this.chklistReq.Items[j].Selected == true)
                {
                    ++numRequerimientos;
                }
            }
            Debug.Write("!!!>" + numRequerimientos);
            if (numRequerimientos > 0)
            {
                for (int j = 0; j < chklistReq.Items.Count - 1; ++j)
                {
                    Debug.Write("!!!>0");
                    if (this.chklistReq.Items[j].Selected == true)
                    {
                        requerimientos += this.chklistReq.Items[j].ToString() + "\n";
                    }
                }
            }
            else if (chklistReq.Items[chklistReq.Items.Count - 1].Selected == true)
            {
                for (int j = 0; j < chklistReq.Items.Count - 1; ++j)
                {
                    Debug.Write("!!!>1");
                    requerimientos += this.chklistReq.Items[j].ToString() + "\n";
                }
            }
            datos[i] = requerimientos;
            i++;
            //METE LAS EJECUCIONES DE PRUEBA
            String ejecuciones = "-";
            if (this.checkBoxEstadoEjecucion.Checked == true || this.checkBoxID_TipoNC.Checked == true )
            {
                if (this.checkBoxEstadoEjecucion.Checked == true && this.checkBoxID_TipoNC.Checked == true)
                {

                }
                else if (this.checkBoxEstadoEjecucion.Checked == true && this.checkBoxID_TipoNC.Checked == false)
                {

                }
                else if (this.checkBoxEstadoEjecucion.Checked == false && this.checkBoxID_TipoNC.Checked == true)
                {

                }
            }
            datos[i] = ejecuciones;
            i++;
            //METE LAS METRICAS
            String metricas = "-";
            if (this.checkBoxConf.Checked == true || this.checkBoxNC.Checked == true)
            {

            }
            datos[i] = metricas;
            for (i = 7; i < numCols; i++)
            {
                datos[i] = "-";
            }
            dt.Rows.Add(datos);
            this.gridReportes.DataSource = dt;
            this.gridReportes.DataBind();
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
            String caso = comboBoxCaso.Text;
            int numModulos=0;
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
            for (int i = 0; i < chklistModulos.Items.Count - 1; ++i)
            {
                if (this.chklistModulos.Items[i].Selected == true)
                {
                    ++numModulos;
                }
            }
            Debug.Write("!!!!!!!>" + numModulos);

            if (numModulos > 0 || chklistModulos.Items[chklistModulos.Items.Count-1].Selected==true)
            {
                columna = new DataColumn();
                columna.DataType = System.Type.GetType("System.String");
                columna.ColumnName = "Módulo";
                dt_casos.Columns.Add(columna);

            }
            for (int i = 0; i < chklistReq.Items.Count - 1; ++i)
            {
                if (this.chklistReq.Items[i].Selected == true)
                {
                    ++numReqs;
                }
            }
            Debug.Write("!!!!!!!>" + numReqs);

            if (numReqs  > 0)
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
            if (this.checkBoxConf.Checked == true || this.checkBoxNC.Checked == true)
            {
                columna = new DataColumn();
                columna.DataType = System.Type.GetType("System.String");
                columna.ColumnName = "Métricas";
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
                    if (chklistModulos.Items.FindByText(requerimiento.Substring(3, 2)) == null)                    {
                         chklistModulos.Items.Add(requerimiento.Substring(3,2));                        ++contador;
                    }
                }
            }

            if (1 < contador)
            {
                chklistModulos.Items.Add("Todos los requerimientos");
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

            if(this.chklistModulos.Items[chklistModulos.Items.Count-1].Selected == true){
                indicador = true;
            }

            for (int i = 0; i < chklistModulos.Items.Count-1; ++i )
            {                if(this.chklistModulos.Items[i].Selected == true || indicador == true){
                    ++contadorModulos;
                    modulos[indice] = chklistModulos.Items[i].Text;
                    ++indice;
                }
            }
           DataTable datosReqProyecto = controladoraReporte.consultarReqModulos(modulos,contadorModulos);
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
                Debug.WriteLine("id del caso"+id);
            
                Session["idDiseno"] = id;
                llenarComboCaso();
                UpdatePanelCaso.Update();
            }
            else
            {
                this.comboBoxCaso.Items.Clear();
                UpdatePanelCaso.Update();
            }
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
                updateChklist.Update();
            }else if(contadorModulos == 0){
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

