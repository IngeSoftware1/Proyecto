<%@ Page Title="Panel Principal" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PanelPrincipal.aspx.cs" Inherits="ProyectoInge.PanelPrincipal" %>

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
        <div class="col-lg-12">
            <%--Panel Principal --%>
            <h2 class="estilo"></h2>
        </div>

    </div>
    <%--Div que cierra el row principal--%>
</asp:Content>
