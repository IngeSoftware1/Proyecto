<%@ Page Title="Recursos Humanos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RecursosHumanos.aspx.cs" Inherits="ProyectoInge.RecursosHumanos"  Async="true" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
  <h2 class="estilo">Módulo de Recursos Humanos</h2>
    <%-- Botones para realizar el IMEC en el modulo --%>
    <div id="btnsControl" style="float:right">
        <asp:Button ID="btnInsertar" runat="server" Text="Insertar" CssClass="estiloBotones" OnClick ="btnInsertar_Click"/>
        <asp:Button ID="btnModificar" runat="server" Text="Modificar" CssClass="estiloBotones" OnClick ="btnIModificar_Click"/>
        <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="estiloBotones"/>
    </div>

    <br>
    <%-- Div que almacena todos los div internos para los datos del RH --%>
    <div id="Datos">
       
        <br>
       <div id="Cedula">
       <asp:Label ID="lblCedula" runat="server" Text="Cédula:" CssClass="estiloLabelRH"></asp:Label>
       <asp:TextBox runat="server" ID="txtCedula" CssClass=""></asp:TextBox>
       </div>

        <%-- Menu vertical de modulos que no he logrado arreglar --%>

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
       <div id="NombreRH">
       <asp:Label ID="lblNombre" runat="server" Text="Nombre:" CssClass="estiloLabelRH"></asp:Label>
       <asp:TextBox runat="server" ID="txtNombre" CssClass=""></asp:TextBox>
       </div>
       
       <br>
       <div id="Ape1RH">
       <asp:Label ID="lblApellido1" runat="server" Text="Apellido 1:" CssClass="estiloLabelRH"></asp:Label>
       <asp:TextBox runat="server" ID="txtApellido1" CssClass="" ></asp:TextBox>
       </div>

       <br>
       <div id="Ape2RH">
       <asp:Label ID="lblApellido2" runat="server" Text="Apellido 2:" CssClass="estiloLabelRH"></asp:Label>
       <asp:TextBox runat="server" ID="txtApellido2" CssClass="" ></asp:TextBox>
       </div>

       <br><br><br>
       <div id="PerfilRH">
       <asp:Label ID="lblPerfil" runat="server" Text="Perfil:" CssClass="estiloLabelRH"></asp:Label>
        <asp:DropDownList runat="server" ID="comboPerfil" CssClass="estiloCombobox">
          <asp:ListItem  Value="1">Administrador</asp:ListItem>
            <asp:ListItem  Value="2">Miembro de equipo de pruebas</asp:ListItem>
        </asp:DropDownList>

       </div>

       <br>
       <div id="RolRH"><%--Aquí debe ser cargado por medio de la base de datos--%>
       <asp:Label ID="lblRol" runat="server" Text="Rol:" CssClass="estiloLabelRH"></asp:Label>
        <asp:DropDownList runat="server" ID="comboRol" CssClass="estiloCombobox">
          <asp:ListItem >Líder de pruebas</asp:ListItem>
            <asp:ListItem >Tester</asp:ListItem>
            <asp:ListItem >Usuario</asp:ListItem>
        </asp:DropDownList>
       </div>

        <br>
       <div id="UsuarioRH">
       <asp:Label ID="lblUsuario" runat="server" Text="Usuario:" CssClass="estiloLabelRH"></asp:Label>
       <asp:TextBox runat="server" ID="txtUsuario" CssClass="" ></asp:TextBox>
       </div>

        <br>
       <div id="ContrasenaRH">
       <asp:Label ID="lblContrasena" runat="server" Text="Contraseña:" CssClass="estiloLabelRH"></asp:Label>
       <asp:TextBox runat="server" ID="txtContrasena" CssClass="" TextMode="Password" ></asp:TextBox>
       </div>

       <br>
       <div id="ConfirmarRH">
       <asp:Label ID="lblConfirmar" runat="server" Text="Confirmar contraseña:" CssClass="estiloLabelRH"></asp:Label>
       <asp:TextBox runat="server" ID="txtConfirmar" CssClass="" TextMode="Password" ></asp:TextBox>
       </div>

       <br><br><br>
       <div id="TelefonoRH">
       <asp:Label ID="lblTelefono" runat="server" Text="Teléfono:" CssClass="estiloLabelRH"></asp:Label>
       <asp:TextBox runat="server" ID="txtTelefono" CssClass="" TextMode="Phone" ></asp:TextBox>
       <asp:Button runat="server" ID="btnNumero" Text="+" CssClass="estiloBoton"/>
       </div>
       
        <br>
        <div id="TelefonosRH">
            <asp:Label ID="lbltels" runat="server" Text="Teléfonos agregados" CssClass="estiloLabelRH"></asp:Label><br>
            <asp:ListBox runat="server" ID="listTelefonos" CssClass="estiloLista"></asp:ListBox>
       </div>

    </div><%--Fin del div grande de los datos--%>

    <%-- Botones para aceptar y cancelar --%>
    <div id="btnsBD" style="float:right">
        <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" CssClass="estiloBotones" OnClick ="btnAceptar_Click" />
        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="estiloBotones"/>
    </div>

    <br><br>
    <%-- El grid para consultar se debe activar --%>
    <%-- %>div id="consulta"--%>
        <%-- %>asp:Label ID="lblconsulta" runat="server" Text="Lista de funcionarios" CssClass="estiloLabelRH"></--%><br>
      <%-- <asp:GridView ID="gridRH" runat="server" OnRowCommand="gridRH_RowCommand" OnPageIndexChanged="gridRH_PageIndexChanged">
                <Columns>
                    <asp:ButtonField ButtonType="Button" Text="Consultar" CommandName="seleccionarRH" Visible="true" CausesValidation="false" />
                </Columns> 
            </asp:GridView> --%>
        </%>

</asp:Content>