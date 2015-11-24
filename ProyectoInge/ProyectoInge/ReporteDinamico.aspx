<%@ Page Title="Reporte Dinamico" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReporteDinamico.aspx.cs" Inherits="ProyectoInge.ReporteDinamico" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="form-group">
        <div class="row">
            <div class="col-sm-9 ">
                <nav class="navbar navbar-default navbar-static-top">
                    <ul class="navbar-default nav navbar-nav">
                        <li><a runat="server" href="~/RecursosHumanos.aspx">Recursos Humanos</a></li>
                        <li><a runat="server" href="~/ProyectodePruebas.aspx">Proyecto</a></li>
                        <li><a runat="server" href="~/DisenoPruebas.aspx">Diseño de pruebas</a></li>
                        <li><a runat="server" href="~/EjecucionPruebas.aspx">Ejecución de pruebas</a></li>
                        <li><a runat="server" style="background-color: ActiveCaption" href="ReporteDinamico.aspx">Reportes</a></li>
                        <%-- <li ><a runat="server" onserverclick="cerrarSesion" href="~/Login.aspx" >Cerrar sesión</a></li>--%>
                        <li><a runat="server" href="~/Login.aspx">Cerrar sesión</a></li>
                    </ul>
                </nav>
            </div>
            <br />
            <asp:Label ID="lblLogueado" runat="server" Text="" Font-Bold="True" CssClass="col-sm-2 control-label"></asp:Label>
        </div>
    </div>

    <div class="row">

        <div class="row">
            <div class="col-lg-11">
                <%--Titulo de la pantalla de Reporte Dinamico --%>
                <h2 class="estilo">Módulo de Reportes</h2>
            </div>

        </div>

        <div id="Datos">


            <asp:UpdatePanel ID="reporteUpdate" runat="server" UpdateMode="conditional" ChildrenAsTriggers="false">
                <ContentTemplate>
                    <div class="col-sm-10 col-sm-offset-1">
                        <div class="form-group">
                            <br />
                            <div class="form-group">
                                <div class="row">
                                    <asp:Label runat="server" ID="lblDatosReporte" Font-Bold="True" Text="Forme su reporte: " CssClass="col-sm-11 control-label"></asp:Label>
                                </div>
                            </div>
                            <%-- PROYECTO --%>
                            <div class="panel panel-default col-sm-12">
                                <br />
                                <div class="row">
                                    <div class="col-sm-10">
                                        <div class="form-group col-sm-5 col-sm-offset-1">
                                            <div class="row">
                                                <asp:Label ID="lblProyecto" runat="server" Text="Proyecto*:" CssClass="col-sm-2 col-sm-offset-1 control-label"></asp:Label>
                                                <div class="col-sm-8 col-sm-offset-1 col-sm-push-1">
                                                    <asp:DropDownList runat="server" ID="comboProyecto" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="proyectoSeleccionado" EnableViewState="true">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br />

                                <%-- MODULO /REPORTE--%>
                                <div class="row">
                                    <div class="col-sm-10">

                                        <div class="form-group col-sm-5 col-sm-offset-1">
                                            <div class="row">
                                                <%--Requerimientos del proyecto --%>
                                                <asp:Label ID="lblReqProyecto" runat="server" Text="Módulos del proyecto:" CssClass="col-sm-12 col-sm-offset-1 control-label"></asp:Label>
                                                <div class="col-sm-12 col-sm-offset-1">

                                                    <asp:CheckBoxList ID="chklistModulos" runat="server" Style="height: 170px" CssClass="form-control">
                                                    </asp:CheckBoxList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group col-sm-1">
                                            <div class="row">
                                            </div>
                                        </div>
                                        <div class="form-group col-sm-5">
                                            <div class="row">
                                                <%--Requerimientos asociados al diseño --%>
                                                <asp:Label ID="lblReqAsignados" runat="server" Text="Requerimientos de los módulos:" CssClass="col-sm-12 col-sm-offset-2 control-label"></asp:Label>
                                                <div class="col-sm-12 col-sm-offset-2">

                                                    <asp:CheckBoxList ID="chklistReq" runat="server" Style="height: 170px" CssClass="form-control">
                                                    </asp:CheckBoxList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />

                            <div class="form-group">
                                <div class="row">
                                    <asp:Label ID="Label13" runat="server" Text="Datos del reporte:" Font-Bold="True" CssClass="col-sm-12 control-label"></asp:Label>
                                </div>
                            </div>

                            <%--CHECKS 0--%>
                            <div class="panel panel-default col-sm-12">

                                <div class="row">
                                    <div class="col-sm-10">

                                        <%--Todos--%>
                                        <div class="form-group col-sm-5 col-sm-offset-1">

                                            <div class="row">
                                                <div class="col-sm-1 col-sm-offset-1">
                                                    <asp:CheckBox ID="checkBoxTodos" runat="server" AutoPostBack="true" />
                                                </div>
                                                <asp:Label ID="Label18" runat="server" Text="Seleccionar todos los datos" CssClass="col-sm-12 col-sm-offset-1 control-label"></asp:Label>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                                <%--CHECKS 1--%>
                                <div class="row">
                                    <div class="col-sm-10">
                                        <div class="form-group col-sm-5 col-sm-offset-1">
                                            <%--METRICAS--%>
                                            <div class="row">
                                                <asp:Label ID="Label10" runat="server" Text="Métricas" CssClass="col-sm-12 col-sm-offset-1 control-label"></asp:Label>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-1 col-sm-offset-1">
                                                    <asp:CheckBox ID="checkBoxConf" runat="server" AutoPostBack="true" />
                                                </div>
                                                <asp:Label ID="Label1" runat="server" Text="De conformidades" CssClass="col-sm-12 col-sm-offset-1 control-label"></asp:Label>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-1 col-sm-offset-1">
                                                    <asp:CheckBox ID="checkBoxNC" runat="server" AutoPostBack="true" />
                                                </div>
                                                <asp:Label ID="Label2" runat="server" Text="De no conformidades" CssClass="col-sm-12 col-sm-offset-1 control-label"></asp:Label>
                                            </div>
                                        </div>


                                        <div class="form-group col-sm-1">
                                            <div class="row">
                                            </div>
                                        </div>


                                        <%--DP--%>
                                        <div class="form-group col-sm-5 col-sm-offset-1">
                                            <div class="row">
                                                <asp:Label ID="Label11" runat="server" Text="Diseño de pruebas" CssClass="col-sm-12 col-sm-offset-1 control-label"></asp:Label>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-1 col-sm-offset-1">
                                                    <asp:CheckBox ID="checkBoxPropositoDiseno" runat="server" AutoPostBack="true" />
                                                </div>
                                                <asp:Label ID="Label3" runat="server" Text="Propósito" CssClass="col-sm-12 col-sm-offset-1 control-label"></asp:Label>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-1 col-sm-offset-1">
                                                    <asp:CheckBox ID="checkBoxRequerimientosDiseno" runat="server" AutoPostBack="true" />
                                                </div>
                                                <asp:Label ID="Label4" runat="server" Text="Requerimientos" CssClass="col-sm-12 col-sm-offset-1 control-label"></asp:Label>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-1 col-sm-offset-1">
                                                    <asp:CheckBox ID="checkBoxResponsableDiseno" runat="server" AutoPostBack="true" />
                                                </div>
                                                <asp:Label ID="Label9" runat="server" Text="Responsable" CssClass="col-sm-12 col-sm-offset-1 control-label"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <%--CHECKS 2--%>
                                <div class="row">
                                    <div class="col-sm-10">
                                        <div class="form-group col-sm-5 col-sm-offset-1">

                                            <%--CP--%>
                                            <div class="row">
                                                <asp:Label ID="Label12" runat="server" Text="Casos de Prueba" CssClass="col-sm-12 col-sm-offset-1 control-label"></asp:Label>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-1 col-sm-offset-1">
                                                    <asp:CheckBox ID="checkBoxPropositoCaso" runat="server" AutoPostBack="true" />
                                                </div>
                                                <asp:Label ID="Label5" runat="server" Text="Propósito" CssClass="col-sm-12 col-sm-offset-1 control-label"></asp:Label>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-1 col-sm-offset-1">
                                                    <asp:CheckBox ID="checkBoxResultadoEsperado" runat="server" AutoPostBack="true" />
                                                </div>
                                                <asp:Label ID="Label6" runat="server" Text="Resultado esperado" CssClass="col-sm-12 col-sm-offset-1 control-label"></asp:Label>
                                            </div>
                                        </div>


                                        <div class="form-group col-sm-1">
                                            <div class="row">
                                            </div>
                                        </div>


                                        <%--EP--%>
                                        <div class="form-group col-sm-5 col-sm-offset-1">
                                            <div class="row">
                                                <asp:Label ID="Label15" runat="server" Text="Ejecución de pruebas" CssClass="col-sm-12 col-sm-offset-1 control-label"></asp:Label>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-1 col-sm-offset-1">
                                                    <%--<asp:Label ID="Label12" runat="server" Text="Datos Ejecución de pruebas:" CssClass="col-sm-2 col-sm-offset-1 control-label"></asp:Label>--%>
                                                    <asp:CheckBox ID="checkBoxEstadoEjecucion" runat="server" AutoPostBack="true" />
                                                </div>
                                                <asp:Label ID="Label7" runat="server" Text="Estado" CssClass="col-sm-12 col-sm-offset-1 control-label"></asp:Label>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-1 col-sm-offset-1">
                                                    <asp:CheckBox ID="checkBoxID_TipoNC" runat="server" AutoPostBack="true" />
                                                </div>
                                                <asp:Label ID="Label8" runat="server" Text="Id del tipo de no conformidad" CssClass="col-sm-12 col-sm-offset-1 control-label"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <br>


            <%--Div de botones--%>
            <div class="col-sm-12">
                <div class="col-sm-7">
                </div>
                <div class="col-sm-3">
                    <asp:Button ID="btnGenerar" runat="server" Text="Generar" CssClass="btn btn-primary" />
                    <asp:Button ID="btnReiniciar" runat="server" Text="Reiniciar" CssClass="btn btn-primary" />

                </div>
                <div class="col-sm-2 col-sm-pull-1 ">
                    <asp:DropDownList runat="server" ID="dropDownListDescargar" AutoPostBack="True" CssClass="form-control" EnableViewState="true"></asp:DropDownList>
                </div>

            </div>

            <br>

            <div class="form-group">
                <div class="row">
                    <div id="consultar">
                        <asp:Label ID="lblReporte" runat="server" Font-Bold="True" Text="Vista previa reporte" CssClass="col-sm-3 col-sm-offset-1 control-label"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-6 col-sm-offset-1">
                        <div id="scroll" style="height: 183px; width: 700px; overflow: auto;">
                            <asp:GridView ID="gridReportes" runat="server" CssClass="dataGridTable" Style="width: 680px; text-align: center" Font-Size="14px" AutoGenerateColumns="true" HeaderStyle-BackColor="#444444" HeaderStyle-ForeColor="White" AlternatingRowStyle-BackColor="#dddddd">
                                <Columns>
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkConsulta" CommandName="seleccionarRH" CommandArgument='<%#Eval("Cédula") %>'> Consultar </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
