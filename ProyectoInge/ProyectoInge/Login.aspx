<%@ Page Title="Iniciar sesión" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ProyectoInge.Login" Async="true" %>

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
                             <asp:Label runat="server" ID="lblUsuario"  CssClass="estiloA" >Nombre de usuario</asp:Label>
                             <asp:TextBox runat="server" ID="txtUsuario" CssClass="estiloCaja" TextMode="Email" />
                              <asp:RequiredFieldValidator runat="server" ControlToValidate="txtUsuario" CssClass="text-danger" ErrorMessage="El campo de correo electrónico es obligatorio." />                   
                             <br/>
                             <br/>
                              <asp:Label runat="server" ID="lblPasword" CssClass="estiloA">Contraseña</asp:Label>
                             <asp:TextBox runat="server" ID="txtPassword" TextMode="Password" CssClass="estiloCaja" />
                             <asp:RequiredFieldValidator runat="server" ControlToValidate="txtPassword" CssClass="text-danger" ErrorMessage="El campo de contraseña es obligatorio." />                    
                            <br/>
                            <br/>
                            <a runat="server" href="~/InterfazCambioContrasenna.aspx">Cambiar contraseña</a>

                            <br/>
                            <asp:Button runat="server" ID="botonIniciar" OnClick="LogIn" Text="Iniciar sesión" CssClass="btn btn-default" />
                            <br/>
                          
                    </div>
                       
                </div>


            </section>
      

     

  
</asp:Content>
