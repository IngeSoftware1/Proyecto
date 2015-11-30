<%@ Page Title="Ejecución de Pruebas" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EjecucionPruebas.aspx.cs" Inherits="ProyectoInge.EjecucionPruebas" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-group">
        <div class="row">
            <div class="col-sm-9 ">
                <nav class="navbar navbar-default navbar-static-top">
                    <ul class="navbar-default nav navbar-nav">
                        <li><a runat="server" href="~/RecursosHumanos.aspx">Recursos Humanos</a></li>
                        <li><a runat="server" href="~/ProyectodePruebas.aspx">Proyecto</a></li>
                        <li><a runat="server" href="~/DisenoPruebas.aspx">Diseño de pruebas</a></li>
                        <li><a runat="server" style="background-color: ActiveCaption" href="~/EjecucionPruebas.aspx">Ejecución de pruebas</a></li>
                        <li><a runat="server" href="ReporteDinamico.aspx">Reportes</a></li>
                        <li><a runat="server" onserverclick="cerrarSesion" href="~/Login.aspx">Cerrar sesión</a></li>
                    </ul>
                </nav>
            </div>
            <br />
            <asp:Label ID="lblLogueado" runat="server" Text="" Font-Bold="True" CssClass="col-sm-2 control-label"></asp:Label>
        </div>
    </div>

    <div class="row">
        <%--Row principal --%>

        <div class="col-lg-12">
            <%--Titulo de la pantalla de Ejecución de Pruebas --%>
            <h2 class="estilo">Módulo de Ejecución de Pruebas</h2>
        </div>


        <div class="col-lg-11">
            <asp:UpdatePanel ID="UpdateBotonesIMEC" runat="server" UpdateMode="conditional" ChildrenAsTriggers="false">
                <ContentTemplate>
                    <%--Div de botones para el IMEC--%>
                    <div id="btnsControl" style="float: right">
                        <asp:Button ID="btnInsertar" runat="server" Text="Insertar" CssClass="btn btn-primary" OnClick="btnInsertar_Click" />
                        <asp:Button ID="btnModificar" runat="server" Text="Modificar" CssClass="btn btn-primary" OnClick="btnModificar_Click" />
                        <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-primary" OnClick="btnEliminar_Click" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

        </div>





        <div id="DatosEjecucionDePrueba">
            <br>
            <br>
            <br>
            <br>
            <br>

            <div class="row col-sm-6 ">
                <asp:Label runat="server" ID="lblDatosEjecucion" Font-Bold="True" Text=" Datos de la Ejecución" CssClass="col-sm-5 col-sm-offset-2 control-label"></asp:Label>
            </div>
            <div class="col-sm-10 col-sm-offset-1">
                <div class="panel panel-default" runat="server" id="datosPanelEjecucion">
                    <div class="panel-body" runat="server" id="panelEjecucion">
                        <div class="form-group">
                            <asp:UpdatePanel ID="UpdateProyectoDiseno" runat="server" UpdateMode="conditional" ChildrenAsTriggers="false">
                                <ContentTemplate>
                                    <%--Proyecto --%>
                                    <div class="row col-sm-6">
                                        <asp:Label ID="lblProyecto" runat="server" Text="Proyecto*:" CssClass="col-sm-3 control-label"></asp:Label>
                                        <div class="col-sm-7">
                                            <asp:DropDownList runat="server" ID="comboProyecto" AutoPostBack="True" OnSelectedIndexChanged="proyectoSeleccionado" CssClass="form-control col-sm-offset-1" EnableViewState="true">
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="row col-sm-6">
                                        <asp:Label ID="lblDiseñoPruebas" runat="server" Text="Diseño de Prueba*:" CssClass="col-sm-5  control-label"></asp:Label>
                                        <div class="col-sm-7 ">
                                            <asp:DropDownList runat="server" ID="comboDiseño" AutoPostBack="True" OnSelectedIndexChanged="disenoSeleccionado" CssClass="form-control col-sm-pull-1" EnableViewState="true">
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <br>
                            <br>
                            <asp:UpdatePanel ID="UpdateDatosDiseno" runat="server" UpdateMode="conditional" ChildrenAsTriggers="false">
                                <ContentTemplate>
                                    <div class="row col-sm-6 ">
                                        <asp:Label runat="server" ID="lblDatosDiseño" Visible="false" Font-Bold="True" Text="Datos del diseño" CssClass="col-sm-5 control-label"></asp:Label>
                                    </div>

                                    <div class="col-sm-10  ">
                                        <div class="panel panel-default" runat="server" id="datosDiseno" style="height: 300px; width: 120%" visible="false">
                                            <div class="panel-body" runat="server" id="panelDiseno" visible="false">

                                                <div class="form-group">
                                                    <%--Proposito del diseño --%>
                                                    <div class="row col-sm-6">
                                                        <asp:Label ID="lblDiseño" runat="server" Text="Propósito:" CssClass="col-sm-3 control-label"></asp:Label>
                                                        <div class="col-sm-7 col-sm-offset-1 ">
                                                            <asp:TextBox runat="server" ID="txtPropositoDiseño" Enabled="false" CssClass="form-control col-sm-offset-2"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <%--Nivel de prueba--%>
                                                    <div class="row col-sm-6">
                                                        <asp:Label ID="lblPrueba" runat="server" Text="Nivel de prueba:" CssClass="col-sm-4  control-label"></asp:Label>

                                                        <div class="col-sm-7 ">
                                                            <asp:TextBox runat="server" ID="txtNivel" Enabled="false" CssClass="form-control col-sm-offset-3"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                </div>
                                                <br>
                                                <br>
                                                <div class="form-group">

                                                    <div class="row col-sm-6">
                                                        <%--Técnica de prueba--%>
                                                        <asp:Label ID="lblTecnica" runat="server" Text="Técnica de prueba:" CssClass="col-sm-5  control-label"></asp:Label>

                                                        <div class="col-sm-7">
                                                            <asp:TextBox runat="server" ID="txtTecnicaPrueba" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <%--Procedimiento--%>

                                                    <div class="row col-sm-6">
                                                        <asp:Label ID="lblProcedimiento" runat="server" Text="Procedimiento:" CssClass="col-sm-4   control-label"></asp:Label>

                                                        <div class="col-sm-7 ">
                                                            <asp:TextBox runat="server" ID="txtProcedimiento" Enabled="false" MultiLine="true" TextMode="MultiLine" Height="100px" MaxLength="70" CssClass="form-control col-sm-offset-3"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                </div>
                                                <br>
                                                <br>

                                                <div class="form-group">


                                                    <div class="row col-sm-6">
                                                        <%--Requerimientos--%>
                                                        <asp:Label ID="lblRequerimiento" runat="server" Text="Requerimientos:" CssClass="col-sm-4 control-label"></asp:Label>

                                                        <div class="col-sm-7 col-sm-offset-1">
                                                            <asp:ListBox runat="server" ID="listRequerimientoDisponibles" Enabled="false" Style="height: 100px" CssClass="form-control"></asp:ListBox>
                                                        </div>

                                                    </div>

                                                </div>
                                                <br>
                                                <br>
                                                <br>
                                                <br>

                                                <div class="row">

                                                    <br>
                                                    <br>
                                                    <%-- Botón para ocultar el diseño --%>
                                                    <asp:LinkButton runat="server" ID="lnkOcultarReq" Style="height: 100px" CssClass=".glyphicon.glyphicon-remove col-sm-offset-6" Enabled="true" OnClick="btnOcultarDiseno">
                                        <span aria-hidden="true" class="glyphicon glyphicon-remove-circle blueColor" style="font-size:25px"></span>
                                                    </asp:LinkButton>

                                                </div>
                                            </div>
                                            <%--Cierra el panel body--%>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <div class="form-group">
                                <asp:UpdatePanel ID="UpdatePanelCalendario" runat="server" UpdateMode="conditional" ChildrenAsTriggers="false">
                                    <ContentTemplate>

                                        <div class="row col-sm-6">
                                            <asp:Label ID="lblFecha" runat="server" Text="Fecha última asignación*:" CssClass="col-sm-3  control-label"></asp:Label>
                                            <div class="col-sm-7">
                                                <asp:TextBox runat="server" ID="txtCalendar" CssClass=" form-control col-sm-offset-1" Enabled="false"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-1">
                                                <asp:LinkButton runat="server" ID="lnkCalendario" CssClas=".glyphicon.glyphicon-calendar col-sm-pull-2" OnClick="lnkCalendario_Click">
                                     <span aria-hidden="true" class="glyphicon glyphicon-calendar blueColor" ></span>
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                        <asp:UpdatePanel ID="comboResponsableUpdate" runat="server" UpdateMode="conditional" ChildrenAsTriggers="false">
                                            <ContentTemplate>
                                                <div class="row col-sm-6">
                                                    <asp:Label ID="lblResponsable" runat="server" Text="Responsable*:" CssClass="col-sm-3  control-label"></asp:Label>

                                                    <div class="col-sm-7 ">
                                                        <asp:DropDownList runat="server" ID="comboResponsable" AutoPostBack="True" CssClass="form-control col-sm-offset-4" EnableViewState="true">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>

                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <br>
                                        <div class="form-group">
                                            <div class="row">
                                                <br>
                                                <div class="col-sm-6 col-sm-pull-4">
                                                    <asp:Calendar runat="server" ID="calendarFecha" Visible="false" OnVisibleMonthChanged="cambioDeMes" OnSelectionChanged="calendarioSeleccionado"></asp:Calendar>
                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>


                            <div class="form-group">
                                <asp:UpdatePanel ID="UpdateGridNoConformidades" runat="server" UpdateMode="conditional" ChildrenAsTriggers="false">
                                    <ContentTemplate>
                                        <div class="row">
                                            <div id="consultar">
                                                <asp:Label ID="lblListaConformidades" runat="server" Visible="false" Font-Size="120%" Font-Bold="True" Text="Lista de no conformidades clasificada por el tipo" CssClass="col-sm-6 col-sm-offset-4 control-label"></asp:Label>
                                            </div>
                                        </div>
                                        <div id="scroll" style="width: 950px; max-height: 500px; overflow-y: auto; overflow-x: hidden">
                                            <asp:GridView ID="gridNoConformidades" ShowFooter="true"
                                                ShowFooterWhenEmpty="true" ShowHeaderWhenEmpty="true" CssClass="dataGridTable" OnRowDataBound="RowDataBound"
                                                Style="text-align: center; width: 930px" Font-Size="12px" runat="server" OnRowCommand="gridTiposNC_RowCommand" RowStyle-Wrap="true"
                                                AutoGenerateColumns="false" HeaderStyle-BackColor="#444444" HeaderStyle-ForeColor="White" AlternatingRowStyle-BackColor="#dddddd">

                                                <Columns>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="5px" HeaderText=" ">
                                                        <ItemTemplate>
                                                            <asp:LinkButton runat="server" ID="lnkModificar" CommandName="seleccionaModificar">
                                                    <span aria-hidden="true" class="glyphicon glyphicon-edit blueColor" style="font-size:25px" ></span>
                                                            </asp:LinkButton>
                                                            <asp:LinkButton runat="server" ID="lnkEliminar" CommandName="seleccionaEliminar">
                                                  <span aria-hidden="true" class="glyphicon glyphicon-remove-sign  blueColor" style="font-size:25px" ></span>
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:LinkButton ID="lnkAceptar" runat="server" CommandName="seleccionaAceptar">
                                                     <span aria-hidden="true" class="glyphicon glyphicon-ok-circle blueColor" style="font-size:25px" ></span>
                                                            </asp:LinkButton>
                                                            <asp:LinkButton ID="lnkCancelar" runat="server" CommandName="seleccionaCancelar">
                                                     <span aria-hidden="true" class="glyphicon glyphicon-remove-circle blueColor" style="font-size:25px" ></span>
                                                            </asp:LinkButton>
                                                        </EditItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:LinkButton runat="server" ID="btnAgregarNC" OnClick="btnAgregarNC_Click">
                                            <span aria-hidden="true" class="glyphicon glyphicon-plus-sign blueColor" style="font-size:25px" ></span>
                                                            </asp:LinkButton>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>



                                                    <%--Tipo de no conformidad  --%>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="5px" ItemStyle-Font-Size="12px" HeaderText="TipoNC">

                                                        <EditItemTemplate>
                                                            <asp:DropDownList ID="dropDownListTipoNC" Style="width: 111px; font-size: 12px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="tipoNCSeleccionadoModificar" CssClass="form-control">
                                                            </asp:DropDownList>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTipoNC" runat="server" Style="width: 111px; font-size: 12px" Text='<%# Bind("TipoNC") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:DropDownList ID="comboTipoNC" Style="width: 111px; font-size: 12px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="tipoNCSeleccionado" CssClass="form-control">
                                                            </asp:DropDownList>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                    <%--Código caso de prueba  --%>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="5px" HeaderText="Código caso de prueba*">

                                                        <EditItemTemplate>
                                                            <asp:DropDownList ID="dropDownListPrueba" Style="width: 111px; font-size: 12px" runat="server" CssClass="form-control">
                                                            </asp:DropDownList>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCasoPrueba" runat="server" Style="width: 111px; font-size: 12px" Text='<%# Bind("IdPrueba") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:DropDownList ID="comboCasoPrueba" Style="width: 111px; font-size: 12px" runat="server" CssClass="form-control">
                                                            </asp:DropDownList>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                    <%-- Descripción de la no conformidad --%>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="150px" HeaderText="Descripción no conformidad">


                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtDescripcionEdit" runat="server" Style="width: 120px; font-size: 12px" MultiLine="true" TextMode="MultiLine" CssClass="form-control" Text='<%# Bind("Descripcion") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDescripcion" runat="server" Style="width: 150px; font-size: 12px" CssClass="control-label" Text='<%# Bind("Descripcion") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:TextBox ID="txtDescripcion" runat="server" MultiLine="true" TextMode="MultiLine" Style="width: 150px; font-size: 12px" CssClass="form-control" Text='<%# Bind("Descripcion") %>'></asp:TextBox>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                    <%-- Justificación --%>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="360px" HeaderText="Justificación">

                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtJustificacionEdit" Style="width: 360px; font-size: 12px" runat="server" MultiLine="true" TextMode="MultiLine" CssClass="form-control" Text='<%# Bind("Justificacion") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblJustificacion" Style="width: 360px; font-size: 12px" runat="server" CssClass="control-label" Text='<%# Bind("Justificacion") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:TextBox ID="txtJustificacion" Style="width: 360px; font-size: 12px" runat="server" MultiLine="true" TextMode="MultiLine" CssClass="form-control" Text='<%# Bind("Justificacion") %>'></asp:TextBox>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                    <%-- Estado --%>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="111px" HeaderText="Estado">

                                                        <EditItemTemplate>
                                                            <asp:DropDownList ID="dropDownListEstado" Style="width: 111px; font-size: 12px" runat="server" CssClass="form-control">
                                                            </asp:DropDownList>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEstado" Style="width: 111px; font-size: 12px" runat="server" Text='<%# Bind("Estado") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:DropDownList ID="comboEstado" Style="width: 111px; font-size: 12px" runat="server" CssClass="form-control">
                                                            </asp:DropDownList>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>



                                                    <%-- Imagen --%>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="30px" HeaderText="Imagen">
                                                        <EditItemTemplate>
                                                            <asp:LinkButton runat="server" ID="lnkCargarImagen" Style="width: 30px; font-size: 12px"  CommandName="cargarImagen" Text="Add">
                                                            </asp:LinkButton>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:LinkButton runat="server" ID="lnkCargarImagenTemplate" Style="width: 30px; font-size: 12px" CommandName="mostrarImagen" Text="Ver">
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:LinkButton runat="server" ID="lnkCargarImagenFoot" Style="width: 30px; font-size: 12px"  CommandName="cargarImagen" Text="Cargar">
                                                            </asp:LinkButton>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                    <%--Imagen Invisible  --%>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" Visible="false">

                                                        <EditItemTemplate>
                                                            <asp:Label ID="lblImagenInvisibleEdit" runat="server" Visible="false" Text='<%# Bind("ImagenInvisible") %>'></asp:Label>
                                                        </EditItemTemplate>


                                                        <ItemTemplate>
                                                            <asp:Label ID="lblImagenInvisible" runat="server" Visible="false" Text='<%# Bind("ImagenInvisible") %>'></asp:Label>
                                                        </ItemTemplate>


                                                        <FooterTemplate>
                                                            <asp:Label ID="lblFootImagenInvisible" runat="server" Visible="false" Text='<%# Bind("ImagenInvisible") %>'></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                    <%--Imagen Extensión Invisible  --%>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" Visible="false">

                                                        <EditItemTemplate>
                                                            <asp:Label ID="lblImagenExtensionInvisibleEdit" runat="server" Visible="false" Text='<%# Bind("ImagenExtensionInvisible") %>'></asp:Label>
                                                        </EditItemTemplate>


                                                        <ItemTemplate>
                                                            <asp:Label ID="lblImagenExtensionInvisible" runat="server" Visible="false" Text='<%# Bind("ImagenExtensionInvisible") %>'></asp:Label>
                                                        </ItemTemplate>


                                                        <FooterTemplate>
                                                            <asp:Label ID="lblFootImagenExtensionInvisible" runat="server" Visible="false" Text='<%# Bind("ImagenExtensionInvisible") %>'></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="form-group">
                                <asp:UpdatePanel ID="UpdateIncidencias" runat="server" UpdateMode="conditional" ChildrenAsTriggers="false">
                                    <ContentTemplate>
                                        <div class="row col-sm-12">
                                            <asp:Label ID="lblIndidencias" runat="server" Text="Incidencias durante la ejecución:" CssClass="col-sm-4  control-label"></asp:Label>
                                        </div>
                                        <div class="row col-sm-12">
                                            <div class="col-sm-11">
                                                <asp:TextBox runat="server" ID="txtIncidencias" CssClass=" form-control col-sm-pull-3"
                                                    MultiLine="true" TextMode="MultiLine" Height="80px"></asp:TextBox>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>



                            <br>
                            <br>
                            <%--Cierra el panel body--%>
                        </div>
                    </div>

                </div>
                <%--Div que cierra los datos de la ejecucion --%>
            </div>

        </div>
        <%--Div que cierra el row principal --%>
        <%--  </div> --%>


        <div class="row">
            <asp:UpdatePanel ID="UpdateBotonesAceptarCancelar" runat="server" UpdateMode="conditional" ChildrenAsTriggers="false">
                <ContentTemplate>

                    <div class="col-lg-11">
                        <%-- Botones para aceptar y cancelar --%>
                        <div id="btnsBD" style="float: right">
                            <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" CssClass="btn btn-primary" OnClick="btnAceptar_Click" />
                            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-primary" OnClick="btnCancelar_Click" />
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>


        <br>


        <div class="form-group">
            <asp:UpdatePanel ID="UpdateGridEjecuciones" runat="server" UpdateMode="conditional" ChildrenAsTriggers="false">
                <ContentTemplate>
                    <div class="row">
                        <div id="consultarEjecuciones">
                            <asp:Label ID="lblConsultarEjecuciones" runat="server" Font-Bold="True" Text="Lista de ejecuciones" CssClass="col-sm-3 col-sm-offset-1 control-label"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-6 col-sm-offset-1">
                            <div id="scrollGrid" style="max-height: 250px; width: 845px; overflow-y: auto; overflow-x:hidden">
                                <asp:GridView ID="gridEjecuciones" runat="server" AutoGenerateColumns="false" CssClass="dataGridTable" Style="width: 845px; text-align: center" Font-Size="14px" OnRowCommand="gridEjecuciones_RowCommand" HeaderStyle-BackColor="#444444" HeaderStyle-ForeColor="White" AlternatingRowStyle-BackColor="#dddddd">
                                    <Columns>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" ID="lnkConsulta" CommandName="seleccionarEjecucion" CommandArgument='<%#Eval("IDEjecucion") %>'> Consultar </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblIDEjecucion" Visible="false" Text='<%#Eval("IDEjecucion") %>'> </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Fecha de última ejecución">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblFecha" Text='<%#Bind("Fecha") %>'> </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Responsable de ejecución">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblResponsable" Text='<%#Bind("Responsable") %>'> </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Diseño">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblDiseno" Text='<%#Bind("Diseno") %>'> </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Proyecto">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblProyecto" Text='<%#Bind("Proyecto") %>'> </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblIDProyecto" Text='<%#Bind("IDProyecto") %>'> </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>


        <!-- Edit Modal Starts here -->
        <div id="editModal" class="modal hide fade" tabindex="-1" role="dialog" aria-labelledby="editModalLabel" aria-hidden="true">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                <h3 id="editModalLabel">Edit Record</h3>
            </div>
            <asp:UpdatePanel ID="upEdit" runat="server">
                <ContentTemplate>

                    <div class="modal-footer">
                        <asp:Label ID="lblResult" Visible="false" runat="server"></asp:Label>

                        <button class="btn btn-info" data-dismiss="modal" aria-hidden="true">Close</button>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

        <!-- Con esto se permite crear los mensajes de aviso en el formato lindo-->
        <div class="modal fade" id="myModal" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <asp:UpdatePanel ID="upModal" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                <h4 class="modal-title">
                                    <asp:Label ID="lblModalTitle" runat="server" Text=""></asp:Label></h4>
                            </div>
                            <div class="modal-body">
                                <asp:Label ID="lblModalBody" runat="server" Text=""></asp:Label>
                            </div>
                            <div class="modal-footer">
                                <button class="btn btn-info" data-dismiss="modal" aria-hidden="true">Close</button>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>

        </div>
        <!-- Edit Modal Ends here -->
        <%--Modal Confirmar--%>
        <div class="modal fade" id="modalConfirmar" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h3 class="modal-title" id="modalConfirma"><i class="fa fa-exclamation-triangle text-danger fa-2x"><font color="#Red">Aviso</font></i></h3>
                    </div>
                    <div class="modal-body">
                        ¿Está seguro que desea eliminar esta no conformidad?            
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="botonAceptarEliminar" class="btn btn-info" Text="Aceptar" OnClick="btnAceptarEliminarTipoNC" runat="server" />
                        <button type="button" id="botonVolver" class="btn btn-info" data-dismiss="modal">Cancelar</button>
                    </div>
                </div>
            </div>
        </div>

        <%--Modal Confirmar2--%>
        <div class="modal fade" id="modalConfirmarEliminar" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h3 class="modal-title" id="modalConfirmaEliminar"><i class="fa fa-exclamation-triangle text-danger fa-2x"><font color="#Red">Aviso</font></i></h3>
                    </div>
                    <div class="modal-body">
                        ¿Está seguro que desea eliminar esta ejecución con sus no conformidades asociadas?            
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="botonAceptarEliminar2" class="btn btn-info" Text="Aceptar" OnClick="btnAceptar_Eliminar" runat="server" />
                        <button type="button" id="botonVolver2" class="btn btn-info" data-dismiss="modal">Cancelar</button>
                    </div>
                </div>
            </div>
        </div>

        <%--Modal Imagen --%>
        <div class="modal fade" id="modalImagen" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-sm">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h3 class="modal-title" id="modalImagenConfirma"><i class="fa fa-exclamation-triangle text-danger fa-2x"><font color="#Red">Cargar Imagen</font></i></h3>
                    </div>
                    <div class="modal-body">
                        Seleccione la imagen que desea cargar:      
                           <asp:FileUpload ID="FileImage" runat="server" Font-Size="14px"></asp:FileUpload>

                        <asp:RegularExpressionValidator runat="server" ErrorMessage="*Solo se permiten imágenes" ForeColor="Red" ValidationExpression="^.*\.(jpg|JPG|gif|GIF|PNG|png|jpeg|JPEG)$" ControlToValidate="FileImage"></asp:RegularExpressionValidator>
                        <asp:Image ID="image1" runat="server" Visible="true" />
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnAceptarImagen" runat="server" class="btn btn-info" Text="Aceptar" OnClick="AceptarImagen" />
                        <asp:Button ID="btnCancelarImagen" runat="server" class="btn btn-info" Text="Cancelar" OnClick="CancelarImagen" />
                    </div>
                </div>
            </div>
        </div>

        <%--Modal Ver Imagen --%>
        <div class="modal fade" id="modalVerImagen" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog" style="width: 765px">
                <asp:UpdatePanel ID="updateModalImagen" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                <h3 class="modal-title" id="modalVerImagenConfirma" style="text-align: center"><i class="fa fa-exclamation-triangle text-danger fa-2x"><font color="#Red">Imagen </font></i></h3>
                            </div>
                            <div class="modal-body" style="height: 500px">
                                <asp:Label ID="lblImagenMostrar" Visible="false" runat="server"></asp:Label>
                                <asp:Image ID="imagenMostrada" runat="server" Width="700px" Height="500px" />
                            </div>
                            <div class="modal-footer">
                                <button class="btn btn-info" data-dismiss="modal" aria-hidden="true">Close</button>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
</asp:Content>
