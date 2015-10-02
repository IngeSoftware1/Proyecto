<%@ Page Title="Iniciar sesión" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ProyectoInge.Account.Login" Async="true" %>

<%@ Register Src="~/Account/OpenAuthProviders.ascx" TagPrefix="uc" TagName="OpenAuthProviders" %>

<asp:Content runat="server" ID="BodyContent2" ContentPlaceHolderID="MainContent">
    <h2 class="estilo"><%: Title %></h2>
            <section id="loginForm">
                <div class="estilo">
                    <br/>
                    <br/>
                    <h4>Utilice una cuenta local para iniciar sesión.</h4>
                      <hr/>
                      <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
                        <p class="text-danger">
                            <asp:Literal runat="server" ID="FailureText" />
                        </p>

                      </asp:PlaceHolder>
                     
                    <div>
                             <asp:Label runat="server" ID="lblEmail" AssociatedControlID="txtEmail" CssClass="estiloA" >Nombre de usuario</asp:Label>
                             <asp:TextBox runat="server" ID="txtEmail" CssClass="estiloCaja" TextMode="Email" />
                              <asp:RequiredFieldValidator runat="server" ControlToValidate="txtEmail" CssClass="text-danger" ErrorMessage="El campo de correo electrónico es obligatorio." />                   
                             <br/>
                             <br/>
                              <asp:Label runat="server" ID="lblPasword" AssociatedControlID="txtPassword" CssClass="estiloA">Contraseña</asp:Label>
                             <asp:TextBox runat="server" ID="txtPassword" TextMode="Password" CssClass="estiloCaja" />
                             <asp:RequiredFieldValidator runat="server" ControlToValidate="txtPassword" CssClass="text-danger" ErrorMessage="El campo de contraseña es obligatorio." />                    
                            <br/>
                            <br/>
                             <asp:CheckBox runat="server" ID="checkBoxCambioContrasena" />
                                <asp:Label runat="server" AssociatedControlID="checkBoxCambioContrasena">Cambiar contraseña</asp:Label>
                            <br/>
                            <asp:Button runat="server" OnClick="LogIn" Text="Iniciar sesión" CssClass="btn btn-default" />
                    </div>
                       
                </div>


            </section>
      

     

  
</asp:Content>
