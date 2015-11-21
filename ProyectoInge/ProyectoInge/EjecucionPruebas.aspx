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

            <div class="col-sm-10 col-sm-offset-1">
                <div class="panel panel-default" style="height: 400px">
                    <div class="panel-body">
                        <div class="form-group">
                            <%--Proyecto --%>

                            <div class="row col-sm-6">
                                <asp:Label ID="lblProyecto" runat="server" Text="Proyecto*:" CssClass="col-sm-3 control-label"></asp:Label>
                                <div class="col-sm-7 col-sm-offset-1 ">
                                    <asp:DropDownList runat="server" ID="comboProyecto" AutoPostBack="True" CssClass="form-control col-sm-offset-2" EnableViewState="true">
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="row col-sm-6">
                                <asp:Label ID="lblDiseñoPruebas" runat="server" Text="Diseño de Prueba*:" CssClass="col-sm-5  control-label"></asp:Label>
                                <div class="col-sm-7 ">
                                    <asp:DropDownList runat="server" ID="comboDiseño" AutoPostBack="True" CssClass="form-control col-sm-pull-1" EnableViewState="true">
                                    </asp:DropDownList>
                                </div>
                            </div>

                        </div>
                        <br>
                        <br>


                        <div class="col-sm-10  ">
                            <div class="panel panel-default" runat="server" id="datosDiseno" style="height: 280px; width: 120%" visible="false">
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
                                                <asp:TextBox runat="server" ID="txtPrueba" Enabled="false" CssClass="form-control col-sm-offset-3"></asp:TextBox>
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
                                            <asp:Label ID="lblProposito" runat="server" Text="Procedimiento:" CssClass="col-sm-5   control-label"></asp:Label>

                                            <div class="col-sm-7 col-sm-pull-1">
                                                <asp:TextBox runat="server" ID="txtProcedimiento" Enabled="false" MultiLine="true" TextMode="MultiLine" Height="100px" MaxLength="70" CssClass="form-control col-sm-offset-2"></asp:TextBox>
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

                                </div>
                                <%--Cierra el panel body--%>
                            </div>
                        </div>

                        <div class="form-group">

                            <asp:GridView ID="gridEjecuciones" ShowFooter="true" runat="server" OnRowCommand="gridTiposNC_RowCommand" AutoGenerateColumns="false">
                                <Columns>

                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText=" ">
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkModificar" CommandName="seleccionaAgregar"> Modificar </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="TipoNC">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTipoNC" runat="server" Text='<%# Eval("TipoNC") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:DropDownList ID="comboTipoNC" runat="server">
                                                <asp:ListItem>Seleccione3</asp:ListItem>
                                                <asp:ListItem>Seleccione4</asp:ListItem>
                                            </asp:DropDownList>
                                        </FooterTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Código caso de prueba">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCasoPrueba" runat="server" Text='<%# Eval("IdPrueba") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:DropDownList ID="comboCasoPrueba" runat="server">
                                                <asp:ListItem>Seleccione3</asp:ListItem>
                                                <asp:ListItem>Seleccione4</asp:ListItem>
                                            </asp:DropDownList>
                                        </FooterTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Descripción de la no conformidad">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDescripcion" runat="server" Text='<%# Eval("Descripcion") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtDescripcion" runat="server" Text='<%# Eval("Descripcion") %>'></asp:TextBox>
                                        </FooterTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Justificación">
                                        <ItemTemplate>
                                            <asp:Label ID="lblJustificacion" runat="server" Text='<%# Eval("Justificacion") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtJustificacion" runat="server" Text='<%# Eval("Justificacion") %>'></asp:TextBox>
                                        </FooterTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Resultados">
                                        <ItemTemplate>
                                            <asp:Label ID="lblResultados" runat="server" Text='<%# Eval("Resultados") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtResultados" runat="server" Text='<%# Eval("Resultados") %>'></asp:TextBox>
                                        </FooterTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Estado">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEstado" runat="server" Text='<%# Eval("Estado") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:DropDownList ID="comboEstado" runat="server">
                                                <asp:ListItem>Seleccione3</asp:ListItem>
                                                <asp:ListItem>Seleccione4</asp:ListItem>
                                            </asp:DropDownList>
                                        </FooterTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText=" ">
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkCargarImagen" CommandName="cargarImagen"> Imagen </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                </Columns>
                            </asp:GridView>
                            <asp:Button ID="btnAddRow" runat="server" OnClick="btnAddRow_Click" Text="Add Row" />

                        </div>
                    </div>
                </div>
            </div>

            <br>
            <br>

            <div class="row col-sm-10 col-sm-offset-1">
                <asp:Label runat="server" ID="lblDatosDiseño" Font-Bold="True" Text="Datos del diseño" CssClass="control-label"></asp:Label>
            </div>
        </div>
        <%--Div que cierra los datos de casos de prueba--%>
    </div>
    <%--Div que cierra el row principal --%>




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
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="gridEjecuciones" EventName="RowCommand" />

            </Triggers>
        </asp:UpdatePanel>
    </div>
    <!-- Edit Modal Ends here -->


</asp:Content>
