﻿<%@ Page Title="Ejecución de Pruebas" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EjecucionPruebas.aspx.cs" Inherits="ProyectoInge.EjecucionPruebas" %>


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
            <%--Div de botones para el IMEC--%>
            <div id="btnsControl" style="float: right">
                <asp:Button ID="btnInsertar" runat="server" Text="Insertar" CssClass="btn btn-primary" OnClick="btnInsertar_Click" />
                <asp:Button ID="btnModificar" runat="server" Text="Modificar" CssClass="btn btn-primary" />
                <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-primary" />
            </div>
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
                <div class="panel panel-default" runat="server" id="datosPanelEjecucion" style="height: 700px">
                    <div class="panel-body" runat="server" id ="panelEjecucion">
                        <div class="form-group">
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
                        <br>
                        <br>

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
                                        <span aria-hidden="true" class="glyphicon glyphicon-remove-circle blueColor"></span>
                                        </asp:LinkButton>

                                    </div>


                                </div>
                                <%--Cierra el panel body--%>
                            </div>
                        </div>

                        <div class="form-group">
                            <div class="row">
                                <div id="consultar">
                                    <asp:Label ID="lblListaConformidades" runat="server" Visible="false" Font-Size="120%" Font-Bold="True" Text="Lista de no conformidades clasificada por el tipo" CssClass="col-sm-6 col-sm-offset-4 control-label"></asp:Label>
                                </div>
                            </div>
                            <div id="scroll" style="width: 900px; height: 183px; overflow: auto;">
                                <asp:GridView ID="gridNoConformidades" ShowFooter="true"
                                    ShowFooterWhenEmpty="true" ShowHeaderWhenEmpty="true" CssClass="dataGridTable" OnRowDataBound="RowDataBound"
                                    Style="text-align: center; width: 900px" Font-Size="14px" runat="server" OnRowCommand="gridTiposNC_RowCommand" RowStyle-Wrap="true"
                                    AutoGenerateColumns="false" HeaderStyle-BackColor="#444444" HeaderStyle-ForeColor="White" AlternatingRowStyle-BackColor="#dddddd">

                                    <Columns>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText=" ">
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" ID="lnkModificar" CommandName="seleccionaModificar"> Modificar </asp:LinkButton>
                                                <asp:LinkButton runat="server" ID="lnkEliminar" CommandName="seleccionaEliminar"> Eliminar </asp:LinkButton>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:LinkButton ID="lnkAceptar" runat="server" CommandName="seleccionaAceptar">Aceptar</asp:LinkButton>
                                                <asp:LinkButton ID="lnkCancelar" runat="server" CommandName="seleccionaCancelar">Cancelar</asp:LinkButton>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:Button ID="btnAddRow" runat="server" CssClass="btn btn-primary" OnClick="btnAddRow_Click" Text="Add Row" />
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <%--Tipo de no conformidad  --%>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="TipoNC">

                                            <EditItemTemplate>
                                                <asp:DropDownList ID="dropDownListTipoNC" runat="server" AutoPostBack="True" OnSelectedIndexChanged="tipoNCSeleccionadoModificar" CssClass="form-control">
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblTipoNC" runat="server" Text='<%# Bind("TipoNC") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:DropDownList ID="comboTipoNC" runat="server" AutoPostBack="True" OnSelectedIndexChanged="tipoNCSeleccionado" CssClass="form-control">
                                                    <asp:ListItem>Seleccione</asp:ListItem>
                                                </asp:DropDownList>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <%--Código caso de prueba  --%>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Código caso de prueba">

                                            <EditItemTemplate>
                                                <asp:DropDownList ID="dropDownListPrueba" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblCasoPrueba" runat="server" Text='<%# Bind("IdPrueba") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:DropDownList ID="comboCasoPrueba" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <%-- Descripción de la no conformidad --%>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Descripción no conformidad">


                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtDescripcionEdit" runat="server" CssClass="form-control" Text='<%# Bind("Descripcion") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblDescripcion" runat="server" CssClass="control-label" Text='<%# Bind("Descripcion") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" Text='<%# Bind("Descripcion") %>'></asp:TextBox>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <%-- Justificación --%>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="5px" ItemStyle-VerticalAlign="Top" HeaderText="Justificación">

                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtJustificacionEdit" runat="server" MultiLine="true" TextMode="MultiLine" CssClass="form-control" Text='<%# Bind("Justificacion") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblJustificacion" Style="width: 5px" runat="server" CssClass="control-label" Text='<%# Bind("Justificacion") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtJustificacion" runat="server" MultiLine="true" TextMode="MultiLine" CssClass="form-control" Text='<%# Bind("Justificacion") %>'></asp:TextBox>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <%-- Resultados --%>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Resultados">


                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtResultadosEdit" runat="server" CssClass="form-control" Text='<%# Bind("Resultados") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblResultados" runat="server" CssClass="control-label" Text='<%# Bind("Resultados") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtResultados" runat="server" CssClass="form-control" Text='<%# Bind("Resultados") %>'></asp:TextBox>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <%-- Estado --%>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Estado">

                                            <EditItemTemplate>
                                                <asp:DropDownList ID="dropDownListEstado" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblEstado" runat="server" Text='<%# Bind("Estado") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:DropDownList ID="comboEstado" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <%-- Imagen --%>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Imagen">
                                            <HeaderStyle Width="5%" />
                                            <EditItemTemplate>
                                                <asp:FileUpload ID="FileImage" runat="server"></asp:FileUpload>
                                                <asp:RegularExpressionValidator runat="server" ErrorMessage="*Solo se permiten imágenes" ForeColor="Red" ValidationExpression="^.*\.(jpg|JPG|gif|GIF|PNG|png|jpeg|JPEG)$" ControlToValidate="FileImage"></asp:RegularExpressionValidator>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" ID="lnkCargarImagen" CommandName="cargarImagen"> Imagen </asp:LinkButton>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:FileUpload ID="FileImage" runat="server"></asp:FileUpload>
                                                <asp:RegularExpressionValidator runat="server" ErrorMessage="*Solo se permiten imágenes" ForeColor="Red" ValidationExpression="^.*\.(jpg|JPG|gif|GIF|PNG|png|jpeg|JPEG)$" ControlToValidate="FileImage"></asp:RegularExpressionValidator>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>

                        <div class="form-group">
                            <asp:UpdatePanel ID="UpdatePanelCalendario" runat="server" UpdateMode="conditional" ChildrenAsTriggers="false">
                                <ContentTemplate>

                                    <div class="row col-sm-6">
                                        <asp:Label ID="lblFecha" runat="server" Text="Fecha última asignación:" CssClass="col-sm-3  control-label"></asp:Label>
                                        <div class="col-sm-7">
                                            <asp:TextBox runat="server" ID="txtCalendar" CssClass=" form-control col-sm-offset-1"></asp:TextBox>
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
                                                <asp:Label ID="lblResponsable" runat="server" Text="Responsable:" CssClass="col-sm-3  control-label"></asp:Label>

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

                            <div class="form-group">

                                <div class="row col-sm-6">
                                    <asp:Label ID="lblIndidencias" runat="server" Text="Incidencias durante la ejecución:" CssClass="col-sm-3  control-label"></asp:Label>

                                    <div class="col-sm-8 ">
                                        <asp:TextBox runat="server" ID="txtIncidencias" CssClass=" form-control col-sm-offset-1"
                                            MultiLine="true" TextMode="MultiLine" Height="80px"></asp:TextBox>
                                    </div>
                                </div>
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
    </div>

    <div class="row">

        <div class="col-lg-11">
            <%-- Botones para aceptar y cancelar --%>
            <div id="btnsBD" style="float: right">
                <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" CssClass="btn btn-primary" />
                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-primary" />
            </div>
        </div>
    </div>
    <br>


    <div class="form-group">
        <div class="row">
            <div id="consultarEjecuciones">
                <asp:Label ID="lblConsultarEjecuciones" runat="server" Font-Bold="True" Text="Lista de ejecuciones" CssClass="col-sm-3 col-sm-offset-1 control-label"></asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-6 col-sm-offset-1">
                <div id="scrollGrid" style="height: 183px; width: 700px; overflow: auto;">
                    <asp:GridView ID="gridEjecuciones" runat="server" CssClass="dataGridTable" Style="width: 680px; text-align: center" Font-Size="14px" AutoGenerateColumns="true" OnRowCommand="gridEjecuciones_RowCommand" HeaderStyle-BackColor="#444444" HeaderStyle-ForeColor="White" AlternatingRowStyle-BackColor="#dddddd">
                        <Columns>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" ID="lnkConsulta" CommandName="seleccionarDiseno" CommandArgument='<%#Eval("ID Diseño") %>'> Consultar </asp:LinkButton>

                                </ItemTemplate>

                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>

                                    <asp:LinkButton runat="server" ID="linkConsultaCaso" CommandName="seleccionarCaso" CommandArgument='<%#Eval("ID Diseño") %>'> Casos de prueba </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>


    <!-- Edit Modal Starts here -->
    <div id="editModal" class="modal hide fade" tabindex="-1" role="dialog" aria-labelledby="editModalLabel" aria-hidden="true">
        <div class="modal-header">
            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
            <h3 id="editModalLabel">Edit Record</h3>
        </div>
        <asp:UpdatePanel ID="upEdit" runat="server">
            <ContentTemplate>
                <!--  <div class="modal-body">
                            <table class="table">
                                <tr>
                                    <td>Country Code : 
                            <asp:Label ID="lblCountryCode" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Population : 
                            <asp:TextBox ID="txtPopulation" runat="server"></asp:TextBox>
                                        <asp:Label runat="server" Text="Type Integer Value!" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Country Name:
                            <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Continent:
                            <asp:TextBox ID="txtContinent1" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>-->
                <div class="modal-footer">
                    <asp:Label ID="lblResult" Visible="false" runat="server"></asp:Label>

                    <button class="btn btn-info" data-dismiss="modal" aria-hidden="true">Close</button>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
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
                    Está seguro que desea eliminar esta no conformidad?            
                </div>
                <div class="modal-footer">
                    <asp:Button ID="botonAceptarEliminar" class="btn btn-info" Text="Aceptar" OnClick="btnAceptarEliminarTipoNC" runat="server" />
                    <button type="button" id="botonVolver" class="btn btn-info" data-dismiss="modal">Cancelar</button>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
