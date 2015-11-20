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
        <div class="col-lg-12">
            <%--Titulo de la pantalla de Caso de Pruebas --%>
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

        <asp:UpdatePanel ID="proyectoUpdate" runat="server" UpdateMode="conditional" ChildrenAsTriggers="false">
            <ContentTemplate>
                <%-- Proyecto asociado al diseño --%>
                <div class="form-group">

                    <div class="row">
                        <asp:Label ID="lblProyecto" runat="server" Text="Proyecto*:" CssClass="col-sm-2 col-sm-offset-1 control-label"></asp:Label>
                        <div class="col-sm-3 ">
                            <asp:DropDownList runat="server" ID="comboProyecto" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="proyectoSeleccionado" EnableViewState="true">
                            </asp:DropDownList>
                        </div>
                    </div>


                </div>

            </ContentTemplate>
        </asp:UpdatePanel>

        <br>
    </div>
</asp:Content>
