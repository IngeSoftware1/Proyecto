<%@ Page Title="Iniciar sesión" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Proyecto.Account.Login" Async="true" %>

<%@ Register Src="~/Account/OpenAuthProviders.ascx" TagPrefix="uc" TagName="OpenAuthProviders" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h2 class="estilo"><%: Title %>.</h2>

  
      
            
            <section id="loginForm">
                <div class="estilo">
                    <h4>Utilice una cuenta local para iniciar sesión.</h4>
                      <hr/>
                      <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
                        <p class="text-danger">
                            <asp:Literal runat="server" ID="FailureText" />
                        </p>

                      </asp:PlaceHolder>
                     
                    <div>

                             <asp:Label runat="server" AssociatedControlID="Email" CssClass="estiloA" >Nombre de usuario</asp:Label>
                             <asp:TextBox runat="server" ID="Email" CssClass="estiloCaja" TextMode="Email" />
                              <asp:RequiredFieldValidator runat="server" ControlToValidate="Email" CssClass="text-danger" ErrorMessage="El campo de correo electrónico es obligatorio." />
                     
                             <br/>
                             <br/>
                              <asp:Label runat="server" AssociatedControlID="Password" CssClass="estiloA">Contraseña</asp:Label>
                             <asp:TextBox runat="server" ID="Password" TextMode="Password" CssClass="estiloCaja" />
                             <asp:RequiredFieldValidator runat="server" ControlToValidate="Password" CssClass="text-danger" ErrorMessage="El campo de contraseña es obligatorio." />
                         
                            <br/>
                            <br/>
                             <asp:CheckBox runat="server" ID="RememberMe" />
                                <asp:Label runat="server" AssociatedControlID="RememberMe">¿Recordar cuenta?</asp:Label>
                            <br/>
                                <asp:Button runat="server" OnClick="LogIn" Text="Iniciar sesión" CssClass="btn btn-default" />
                    </div>
                       
                </div>


            </section>
      

     

  
</asp:Content>
