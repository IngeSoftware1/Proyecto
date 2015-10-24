<%@ Page Title="Caso de Prueba" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CasoDePrueba.aspx.cs" Inherits="ProyectoInge.CasoDePrueba" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
     <nav class="navbar navbar-default navbar-static-top">
    <ul class="navbar-default nav navbar-nav">
         <li><a runat="server"  href="~/RecursosHumanos.aspx">Recursos Humanos</a></li>
            <li ><a runat="server" href="~/ProyectodePruebas.aspx" >Proyecto</a></li>
            <li ><a runat="server" href="~/DisenoPruebas.aspx" >Diseño de pruebas</a></li>
            <li ><a runat="server" style ="background-color:ActiveCaption" href="~/CasoDePrueba.aspx" >Casos de prueba</a></li>
            <li ><a runat="server" >Ejecución de pruebas</a></li>
            <li ><a runat="server" onserverclick="cerrarSesion" href="~/Login.aspx" >Cerrar sesión</a></li>
    </ul>
</nav>
    
    <div class="row">
        <div class="col-lg-12">
            <%--Titulo de la pantalla de Caso de Pruebas --%>
            <h2 class="estilo">Módulo de Casos de Prueba</h2>
        </div>

         <div class="col-lg-11">
            <%--Div de botones para el IMEC--%>
            <div id="btnsControl" style="float: right">
                <asp:Button ID="btnInsertar" runat="server" Text="Insertar" CssClass="btn btn-primary" />
                <asp:Button ID="btnModificar" runat="server" Text="Modificar" CssClass="btn btn-primary"  />
                <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-primary" />
            </div>
        </div>





        </div> <%--Div que cierra el row principal--%>





</asp:Content>
