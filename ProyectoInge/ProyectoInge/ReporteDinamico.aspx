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
            <div class="col-lg-12">
                <%--Titulo de la pantalla de Reporte Dinamico --%>
                <h2 class="estilo">Módulo de Reportes</h2>
            </div>

        </div>

        <div id="Datos">
            <div class="form-group">
                <div class="row">
                    <asp:Label runat="server" ID="lblDatosReporte" Font-Bold="True" Text="Datos del reporte" CssClass="col-sm-12 col-sm-offset-1 control-label"></asp:Label>
                </div>
            </div>
            <br>

            <asp:UpdatePanel ID="reporteUpdate" runat="server" UpdateMode="conditional" ChildrenAsTriggers="false">
                <ContentTemplate>
                    <div class="col-sm-10 col-sm-offset-1">
                        <div class="panel panel-default">
                            <div class="form-group">
                                <br />
                                <%-- PROYECTO --%>
                                <div class="row">
                                    <div class="col-sm-10">
                                        <div class="form-group col-sm-5 col-sm-offset-1">
                                            <div class="row">
                                                <asp:Label ID="lblProyecto" runat="server" Text="Proyecto*:" CssClass="col-sm-2 col-sm-offset-1 control-label"></asp:Label>
                                                <div class="col-sm-8 col-sm-offset-1 col-sm-push-1">
                                                    <asp:DropDownList runat="server" ID="comboProyecto" AutoPostBack="True" CssClass="form-control" EnableViewState="true"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br />

                                <%-- MODULO --%>
                                <div class="row">
                                    <div class="col-sm-10">

                                        <div class="form-group col-sm-5 col-sm-offset-1">
                                            <div class="row">
                                                <%--Requerimientos del proyecto --%>
                                                <asp:Label ID="lblReqProyecto" runat="server" Text="Modulos del proyecto" CssClass="col-sm-12 col-sm-offset-1 control-label"></asp:Label>
                                                <div class="col-sm-12 col-sm-offset-1">
                                                    <asp:ListBox runat="server" ID="listModProyecto" CssClass="form-control" Style="height: 170px"></asp:ListBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group col-sm-1">
                                            <div class="row">
                                                <div class="">
                                                    <br>
                                                    <br>
                                                    <%-- Botón para agregar requerimientos a un diseño --%>
                                                    <asp:LinkButton runat="server" ID="lnkAgregarMod" Style="height: 100px" CssClass="col-sm-offset-11">
                                        <span aria-hidden="true" class="glyphicon glyphicon-hand-right blueColor"></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                            <br>
                                            <br>
                                            <br>
                                            <div class="row">
                                                <div class="">
                                                    <%-- Botón para quitar requerimientos de un diseño --%>
                                                    <asp:LinkButton runat="server" ID="lnkQuitarMod" Style="height: 100px" CssClass="col-sm-offset-11">
                                        <span aria-hidden="true" class="glyphicon glyphicon-hand-left blueColor"></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="form-group col-sm-5">
                                            <div class="row">
                                                <%--Requerimientos asociados al diseño --%>
                                                <asp:Label ID="lblReqAsignados" runat="server" Text="Modulos del reporte" CssClass="col-sm-12 col-sm-offset-2 control-label"></asp:Label>
                                                <div class="col-sm-12 col-sm-offset-2">
                                                    <asp:ListBox runat="server" ID="listModAgregados" CssClass="form-control" Style="height: 170px"></asp:ListBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <%--REQUERIMIENTO --%>
                                <div class="row">
                                    <div class="col-sm-10">

                                        <div class="form-group col-sm-5 col-sm-offset-1">
                                            <div class="row">
                                                <%--Requerimientos del proyecto --%>
                                                <asp:Label ID="LabelReq1" runat="server" Text="Requerimentos del proyecto" CssClass="col-sm-12 col-sm-offset-1 control-label"></asp:Label>
                                                <div class="col-sm-12 col-sm-offset-1">
                                                    <asp:ListBox runat="server" ID="listReqProyecto" CssClass="form-control" Style="height: 170px"></asp:ListBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group col-sm-1">
                                            <div class="row">
                                                <div class="">
                                                    <br>
                                                    <br>
                                                    <%-- Botón para agregar requerimientos a un diseño --%>
                                                    <asp:LinkButton runat="server" ID="lnkAgregarReq" Style="height: 100px" CssClass="col-sm-offset-11">
                                        <span aria-hidden="true" class="glyphicon glyphicon-hand-right blueColor"></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                            <br>
                                            <br>
                                            <br>
                                            <div class="row">
                                                <div class="">
                                                    <%-- Botón para quitar requerimientos de un diseño --%>
                                                    <asp:LinkButton runat="server" ID="lnkQuitarReq" Style="height: 100px" CssClass="col-sm-offset-11">
                                        <span aria-hidden="true" class="glyphicon glyphicon-hand-left blueColor"></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="form-group col-sm-5">
                                            <div class="row">
                                                <%--Requerimientos asociados al diseño --%>
                                                <asp:Label ID="LabelReq2" runat="server" Text="Modulos del reporte" CssClass="col-sm-12 col-sm-offset-2 control-label"></asp:Label>
                                                <div class="col-sm-12 col-sm-offset-2">
                                                    <asp:ListBox runat="server" ID="listReqAgregados" CssClass="form-control" Style="height: 170px"></asp:ListBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>


                                <%--CHECKS--%>
                                <div class="row">
                                    <div class="col-sm-10">
                                        <div class="form-group col-sm-5 col-sm-offset-1">
                                            <div class="row">
                                                <div class="col-sm-1 col-sm-offset-1">
                                                    <asp:CheckBox ID="checkBoxConf" runat="server" AutoPostBack="true" />
                                                </div>
                                                <asp:Label ID="Label1" runat="server" Text="Metricas de conformidades" CssClass="col-sm-12 col-sm-offset-1 control-label"></asp:Label>

                                            </div>

                                            <div class="row">
                                                <div class="col-sm-1 col-sm-offset-1">
                                                    <asp:CheckBox ID="checkBoxNC" runat="server" AutoPostBack="true" />
                                                </div>
                                                <asp:Label ID="Label2" runat="server" Text="Metricas de no conformidades" CssClass="col-sm-12 col-sm-offset-1 control-label"></asp:Label>

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
            <div class="col-lg-11">
                <div id="btnsControl" style="float: right">
                    <asp:Button ID="btnGenerar" runat="server" Text="Generar" CssClass="btn btn-primary" />
                    <asp:Button ID="btnReiniciar" runat="server" Text="Reiniciar" CssClass="btn btn-primary" />
                    <asp:Button ID="btnDescargar" runat="server" Text="Descargar" CssClass="btn btn-primary" />
                </div>
            </div>

            <br>
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
                            <asp:GridView ID="gridRH" runat="server" CssClass="dataGridTable" Style="width: 680px; text-align: center" Font-Size="14px" AutoGenerateColumns="true" HeaderStyle-BackColor="#444444" HeaderStyle-ForeColor="White" AlternatingRowStyle-BackColor="#dddddd">
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
            <br>
            <br>
        </div>
    </div>
</asp:Content>
