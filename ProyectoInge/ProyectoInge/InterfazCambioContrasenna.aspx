<%@ Page Title="Cambiar Contraseña" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InterfazCambioContrasenna.aspx.cs" Inherits="ProyectoInge.InterfazCambioContrasenna"  Async="true" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
  

    <h2>Cambiar Contraseña</h2>
  <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
                        <p class="text-danger">
                            <asp:Literal runat="server" ID="FailureText" />
                        </p>

  </asp:PlaceHolder>

    <br>
    <%-- Div que almacena todos los datos --%>
    <div id="Datos">
       
         <br/>
       <asp:Label runat="server" ID="lblUsuario"  CssClass="estiloLabelRH" >Nombre de usuario</asp:Label>
       <asp:TextBox runat="server" ID="txtUsuario" />
       <asp:RequiredFieldValidator runat="server" ControlToValidate="txtUsuario" CssClass="text-danger" ErrorMessage="El campo de correo electrónico es obligatorio." />                   
       <br/>
        <br/>



    </div><%--Fin del div grande de los datos--%>

    <%-- Botones para aceptar y cancelar --%>
   

      <asp:Label runat="server" ID="lblAntPassword" CssClass="estiloLabelRH">Contraseña anterior</asp:Label>
      <asp:TextBox runat="server" ID="txtAntPassword" TextMode="Password"  />
      <asp:RequiredFieldValidator runat="server" ControlToValidate="txtAntPassword" CssClass="text-danger" ErrorMessage="El campo de contraseña es obligatorio." />        
      <br/>
      <br/>
      <asp:Label runat="server" ID="lblNewPassword" CssClass="estiloLabelRH">Nueva Contraseña</asp:Label>
      <asp:TextBox runat="server" ID="txtNewPassword" TextMode="Password"  />
      <asp:RequiredFieldValidator runat="server" ControlToValidate="txtNewPassword" CssClass="text-danger" ErrorMessage="El campo de contraseña es obligatorio." />                    
      <br/>
      <br/>
      <asp:Label runat="server" ID="lblConfPassword" CssClass="estiloLabelRH">Confirmar contraseña</asp:Label>
      <asp:TextBox runat="server" ID="txtConfPassword" TextMode="Password"  />
      <asp:RequiredFieldValidator ID="controlConfirmar" runat="server" ControlToValidate="txtConfPassword" CssClass="text-danger" ErrorMessage="El campo de contraseña es obligatorio." />                
                            
    <div class="col-lg-11">
        <div id="btnsBD" style="float:right">
            <asp:Button ID="btnAceptar" runat="server"  OnClick="cambiarContrasenna"  Text="Aceptar" CssClass="btn btn-primary" />
            <asp:Button ID="btnCancelar" runat="server" onClick="botonClick" Text="Cancelar" CssClass="btn btn-primary" OnClientClick="aspnetForm.target ='_blank';"/>
        </div>
    </div>
                    <!-- Modal Dialog -->
                <div class="modal fade" id="myModal" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                    <div class="modal-dialog">
                        <asp:UpdatePanel ID="upModal" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                        <h4 class="modal-title"><asp:Label ID="lblModalTitle" runat="server" Text=""></asp:Label></h4>
                                    </div>
                                    <div class="modal-body">
                                        <asp:Label ID="lblModalBody" runat="server" Text=""></asp:Label>
                                    </div>
                                    <div class="modal-footer">
                                        <button class="btn btn-info" data-dismiss="modal" aria-hidden="true">Close</button>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div> 
   

</asp:Content>
