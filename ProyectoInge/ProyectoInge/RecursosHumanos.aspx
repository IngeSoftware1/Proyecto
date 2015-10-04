<%@ Page Title="Recursos Humanos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RecursosHumanos.aspx.cs" Inherits="ProyectoInge.RecursosHumanos"  Async="true" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Módulo de Recursos Humanos</h2>
    <div id="btnsControl" style="float:right">
        <asp:Button ID="btnInsertar" runat="server" Text="Insertar" />
        <asp:Button ID="btnModificar" runat="server" Text="Modificar"/>
        <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" />
    </div>

    <br>

    <div>
        <br>
       <div>
       <asp:Label ID="lblCedula" runat="server" Text="Cédula:" CssClass="estiloLabel"></asp:Label>
       <asp:TextBox runat="server" ID="txtCedula" CssClass="estiloCajas"></asp:TextBox>
       </div>

    <%--<asp:Menu ID="MenuPrincipal" runat="server">
            <asp:Items>
                <asp:MenuItem Text="Recursos Humanos" Value="1"></asp:MenuItem>
                <asp:MenuItem Text="Proyectos" Value="2"></asp:MenuItem>
                <asp:MenuItem Text="Diseño de Pruebas" Value="3"></asp:MenuItem>
                <asp:MenuItem Text="Casos de Pruebas" Value="4"></asp:MenuItem>
                <asp:MenuItem Text="Ejecución de Pruebas" Value="5"></asp:MenuItem>
            </asp:Items>
        </asp:Menu>--%>
        <br>
       <div>
       <asp:Label ID="lblNombre" runat="server" Text="Nombre:" CssClass="estiloLabel"></asp:Label>
       <asp:TextBox runat="server" ID="txtNombre" CssClass="estiloCajas"></asp:TextBox>
       </div>
       
        <br>
       <div >
       <asp:Label ID="lblApellido1" runat="server" Text="Apellido 1:" CssClass="estiloLabel"></asp:Label>
       <asp:TextBox runat="server" ID="txtApellido1" CssClass="estiloCajas" ></asp:TextBox>
       </div>

       <br>
       <div >
       <asp:Label ID="lblApellido2" runat="server" Text="Apellido 2:" CssClass="estiloLabel"></asp:Label>
       <asp:TextBox runat="server" ID="txtApellido2" CssClass="estiloCajas" ></asp:TextBox>
       </div>

        <br>
       <div >
       <asp:Label ID="lblUsuario" runat="server" Text="Usuario:" CssClass="estiloLabel"></asp:Label>
       <asp:TextBox runat="server" ID="txtUsuario" CssClass="estiloCajas" ></asp:TextBox>
       </div>

        <br>
       <div >
       <asp:Label ID="lblContrasena" runat="server" Text="Contraseña:" CssClass="estiloLabel"></asp:Label>
       <asp:TextBox runat="server" ID="txtContrasena" CssClass="estiloCajas" TextMode="Password" ></asp:TextBox>
       </div>

       <br>
       <div >
       <asp:Label ID="lblConfirmar" runat="server" Text="Confirmar contraseña:" CssClass="estiloLabel"></asp:Label>
       <asp:TextBox runat="server" ID="txtConfirmar" CssClass="estiloCajas" TextMode="Password" ></asp:TextBox>
       </div>

       <br>
       <div >
       <asp:Label ID="lblTelefono" runat="server" Text="Teléfono:" CssClass="estiloLabel"></asp:Label>
       <asp:TextBox runat="server" ID="txtTelefono" CssClass="estiloCajas" TextMode="Phone" ></asp:TextBox>
       <asp:Button runat="server" ID="btnNumero" Text="+"/>
       </div>
       
         <br>
        <div>
       <asp:ListBox runat="server" ID="listTelefonos" CssClass="estiloLista"></asp:ListBox>
       </div>

    </div>

</asp:Content>