<%@ Page Title="Cambiar Contraseña" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InterfazCambioContrasenna.aspx.cs" Inherits="ProyectoInge.InterfazCambioContrasenna"  Async="true" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
  <h2>Cambiar Contraseña</h2>
  

    <br>
    <%-- Div que almacena todos los datos --%>
    <div id="Datos">
       
         <br/>
       <asp:Label runat="server" ID="lblUsuario" AssociatedControlID="txtUsuario" CssClass="estiloLabelRH" >Nombre de usuario</asp:Label>
       <asp:TextBox runat="server" ID="txtUsuario"  TextMode="Email" />
       <asp:RequiredFieldValidator runat="server" ControlToValidate="txtUsuario" CssClass="text-danger" ErrorMessage="El campo de correo electrónico es obligatorio." />                   
       <br/>
        <br/>



    </div><%--Fin del div grande de los datos--%>

    <%-- Botones para aceptar y cancelar --%>
   
      <asp:Label runat="server" ID="lblNewPassword" AssociatedControlID="txtNewPassword" CssClass="estiloLabelRH">Nueva Contraseña</asp:Label>
      <asp:TextBox runat="server" ID="txtNewPassword" TextMode="Password"  />
      <asp:RequiredFieldValidator runat="server" ControlToValidate="txtNewPassword" CssClass="text-danger" ErrorMessage="El campo de contraseña es obligatorio." />                    
      <br/>
       <br/>
      <asp:Label runat="server" ID="lblAntPassword" AssociatedControlID="txtAntPassword" CssClass="estiloLabelRH">Contraseña anterior</asp:Label>
       <asp:TextBox runat="server" ID="txtAntPassword" TextMode="Password"  />
       <asp:RequiredFieldValidator runat="server" ControlToValidate="txtAntPassword" CssClass="text-danger" ErrorMessage="El campo de contraseña es obligatorio." />                    
                            

    <div id="btnsBD" style="float:right">
        <asp:Button ID="btnAceptar" runat="server"  OnClick="ChangePassword"  Text="Aceptar" CssClass="estiloBotones" />
         <asp:Button ID="btnCancelar" runat="server" onClick="botonClick" PostBackUrl="~/RecursosHumanos.aspx" Text="Cancelar" CssClass="estiloBotones" OnClientClick="aspnetForm.target ='_blank';"/>
        
 
    </div>

   

</asp:Content>
