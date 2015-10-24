<%@ Page Title="Recursos Humanos" Language="C#" MasterPageFile="~/Site.Master"  AutoEventWireup="true" CodeBehind="RecursosHumanos.aspx.cs" Inherits="ProyectoInge.RecursosHumanos" Async="true" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    
   <nav class="navbar navbar-default navbar-static-top">
    <ul class="navbar-default nav navbar-nav">
         <li><a runat="server" style ="background-color:ActiveCaption"  href="~/RecursosHumanos.aspx">Recursos Humanos</a></li>
            <li ><a runat="server" href="~/ProyectodePruebas.aspx" >Proyecto</a></li>
            <li ><a runat="server" href="~/DisenoPruebas.aspx" >Diseño de pruebas</a></li>
            <li ><a runat="server" href="~/CasoDePrueba.aspx" >Caso de pruebas</a></li>
            <li ><a runat="server" >Ejecución de pruebas</a></li>
            <li ><a runat="server" onserverclick="cerrarSesion" href="~/Login.aspx" >Cerrar sesión</a></li>
    </ul>
</nav>
    <div class="row">
        <div class="col-lg-12">
            <h2 class="estilo">Módulo de Recursos Humanos</h2>
        </div>

        <%-- Botones para realizar el IMEC en el modulo --%>
        <div class="col-lg-11">
            <%-- Botones para realizar el IMEC en el modulo --%>
            <div id="btnsControl" style="float: right">
                <asp:Button ID="btnInsertar" runat="server" Text="Insertar" CssClass="btn btn-primary" OnClick="btnInsertar_Click"/>
                <asp:Button ID="btnModificar" runat="server" Text="Modificar" CssClass="btn btn-primary" OnClick="btnModificar_Click" />
                <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-primary" OnClick="btnEliminar_Click" />
            </div>

        </div>

        <br>
        <br>
        <br>
        <br>
        <div id="Datos"> <%-- Div que almacena todos los div internos para los datos del RH --%>
             <div class ="row"> 
                    <asp:Label runat="server" ID="lblDatosPersonales" Font-Bold="True" Text="Datos personales" CssClass="col-sm-5 col-sm-offset-1 control-label"></asp:Label>
                    <asp:Label runat="server" ID="lblDatosUsuario" Font-Bold="True" Text="Datos de usuario" CssClass="col-sm-5  control-label"></asp:Label>
                </div> 
            <div class="row">
                  <div class="col-sm-5 col-sm-offset-1">
                    <div class="panel panel-default" style ="height: 600px">
                        <div class="panel-body" >
                            <div class="form-group">
                                <div class="row">
                                    <asp:Label ID="lblCedula" runat="server" Text="Cédula*:" CssClass="col-sm-1 col-sm-offset-1 control-label"></asp:Label>
                                    <div class="col-sm-6 col-sm-offset-3 "> 
                                        <asp:TextBox runat="server" ID="txtCedula" PlaceHolder ="Ej: 145680958" CssClass="form-control" MaxLength="9"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="ERValidator" runat="server" ControlToValidate="txtCedula" ErrorMessage="*Ingrese Valores Numéricos"
                            ForeColor="Red" ValidationExpression="^[0-9]*"> </asp:RegularExpressionValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <asp:Label ID="lblNombre" runat="server" Text="Nombre*:" CssClass="col-sm-1 col-sm-offset-1 control-label"></asp:Label>
                                    <div class="col-sm-6 col-sm-offset-3 ">
                                        <asp:TextBox runat="server" ID="txtNombre" CssClass="form-control" MaxLength="20"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="ERNombre" runat="server" ControlToValidate="txtNombre" ErrorMessage="*Ingrese solo letras"
                            ForeColor="Red" ValidationExpression="^[A-Za-z]*$"> </asp:RegularExpressionValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <asp:Label ID="lblApellido1" runat="server" Text="Primer apellido*:" CssClass="col-sm-4 col-sm-offset-1 control-label"></asp:Label>
                                    <div class="col-sm-6 col-sm -offset-1 ">
                                        <asp:TextBox runat="server" ID="txtApellido1" CssClass="form-control" MaxLength="20"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="ERLetrasApe1" runat="server" ControlToValidate="txtApellido1" ErrorMessage="*Ingrese solo letras"
                            ForeColor="Red" ValidationExpression="^[A-Za-z]*$"> </asp:RegularExpressionValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <asp:Label ID="lblApellido2" runat="server" Text="Segundo apellido*:" CssClass="col-sm-4 col-sm-offset-1 control-label"></asp:Label>
                                    <div class="col-sm-6 ">
                                        <asp:TextBox runat="server" ID="txtApellido2" CssClass="form-control" MaxLength="20"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="ERApe2" runat="server" ControlToValidate="txtApellido2" ErrorMessage="*Ingrese solo letras"
                            ForeColor="Red" ValidationExpression="^[A-Za-z]*$"> </asp:RegularExpressionValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                             <asp:Label ID="lblEmail" runat="server" Text="Email:" CssClass="col-sm-4 col-sm-offset-1 control-label"></asp:Label>
                                    <div class="col-sm-6 ">
                                        <asp:TextBox runat="server" ID="txtEmail" PlaceHolder ="Ej: correo@gmail.com" CssClass="form-control" MaxLength="30"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="EREmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="*Correo inválido"
                            ForeColor="Red" ValidationExpression="[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}"> </asp:RegularExpressionValidator>
                                    </div>
                                </div>
                            </div>  

                            <asp:UpdatePanel ID="Update_Telefonos" runat="server" UpdateMode="conditional" ChildrenAsTriggers="false">
                         <ContentTemplate>

                            <div class="form-group">
                                <div class="row">
                                    <asp:Label ID="lblTelefono" runat="server" Text="Teléfono:" CssClass="col-sm-2 col-sm-offset-1 control-label"></asp:Label>
                                    <div class="col-sm-6 col-sm-offset-2"> 
                                        <asp:TextBox runat="server" ID="txtTelefono"  PlaceHolder ="Ej: 88888888" CssClass="form-control" TextMode="Phone" MaxLength="11"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="REV2" runat="server" ControlToValidate="txtTelefono" ErrorMessage="*Ingrese Valores Numéricos"
                            ForeColor="Red" ValidationExpression="^[0-9]*"> </asp:RegularExpressionValidator>
                                    </div>
                                    <div class="">
                                         <asp:LinkButton runat="server" ID="lnkNumero" CssClass="glyphicon.glyphicon-plus-sign" OnClick="btnAgregarTelefono">
                                        <span aria-hidden="true" class="glyphicon glyphicon-plus-sign blueColor"></span>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <asp:Label ID="lbltels" runat="server" Text="Teléfonos agregados" CssClass="col-sm-5 col-sm-offset-1 control-label"></asp:Label>
                                </div>
                                <div class="row">
                                    <div class="col-sm-10 col-sm-offset-1">
                                        <asp:ListBox runat="server" ID="listTelefonos" CssClass="form-control"></asp:ListBox>
                                        </div>
                                        <div class="">
                                         <asp:LinkButton runat="server" ID="lnkQuitar" style= "height: 100px" CssClass="" OnClick="btnEliminarTelefono">
                                        <span aria-hidden="true" class="glyphicon glyphicon-minus-sign blueColor"></span>
                                        </asp:LinkButton>
                                    </div>

                                    </div>
                                </div>

                             </ContentTemplate>
                </asp:UpdatePanel>
                </div>
                </div>
                    </div>
                <div class="col-sm-5 ">
                    <div class="panel panel-default" style ="height: 350px">
                        <div class="panel-body">
                            <div class="form-group">
                                <div class="row">
                                    <asp:Label ID="lblUsuario" runat="server" Text="Usuario:" CssClass="col-sm-1 col-sm-offset-1 control-label"></asp:Label>
                                    <div class="col-sm-6 col-sm-offset-4">
                                        <asp:TextBox runat="server" ID="txtUsuario" CssClass="form-control" MaxLength="20"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <asp:Label ID="lblContrasena" runat="server" Text="Contraseña" CssClass="col-sm-1 col-sm-offset-1 control-label"></asp:Label>
                                    <div class="col-sm-6 col-sm-offset-4">
                                        <asp:TextBox runat="server" ID="txtContrasena" CssClass="form-control" MaxLength="30"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <asp:Label ID="lblConfirmar" runat="server" Text="Confirmar contraseña:" CssClass="col-sm-5 col-sm-offset-1 control-label"></asp:Label>
                                    <div class="col-sm-6">
                                        <asp:TextBox runat="server" ID="txtConfirmar" CssClass="form-control" MaxLength="30"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                             <asp:UpdatePanel ID="UpdatePanelDropDown" runat="server" UpdateMode="conditional" ChildrenAsTriggers="false">
                         <ContentTemplate>

                            <div class="form-group">
                                <div class="row">
                                    <asp:Label ID="lblPerfil" runat="server" Text="Perfil*:" CssClass="col-sm-5 col-sm-offset-1 control-label" ></asp:Label>
                                    <div class="col-sm-6">
                                        <asp:DropDownList runat="server" ID="comboPerfil" AutoPostBack="True"  CssClass="form-control" onselectedindexchanged="perfilSeleccionado" >
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <asp:Label ID="lblRol" runat="server" Text="Rol*:" CssClass="col-sm-5 col-sm-offset-1 control-label "></asp:Label>
                                    <div class="col-sm-6">
                                        <asp:DropDownList runat="server" ID="comboRol" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                         </ContentTemplate>
                             </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    
     <div class="col-lg-11">
        <%-- Botones para aceptar y cancelar --%>
            <div id="btnsBD" style="float: right">
            <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" CssClass="btn btn-primary" OnClick="btnAceptar_Click"/>
            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-primary" OnClick="btnCancelar_Click" />
            </div>
    </div>

    <br>
    <br>


      <div class="form-group">
             <div class="row">
            <div id="consultar">
                <asp:Label ID="lblFuncionarios" runat="server" Font-Bold="True" Text="Lista de funcionarios" CssClass="col-sm-3 col-sm-offset-1 control-label"></asp:Label>
              </div> 
                </div>   
                    <div class ="row">
                <div class="col-sm-6 col-sm-offset-1"> 
                    <div id ="scroll" style ="height: 183px; width:700px; overflow:auto;" >
                        <asp:GridView ID="gridRH" runat="server" CssClass ="dataGridTable" style ="width: 680px; text-align:center" font-size = "14px" AutoGenerateColumns="true"  OnRowCommand ="gridFuncionarios_RowCommand" HeaderStyle-BackColor="#444444" HeaderStyle-ForeColor="White" AlternatingRowStyle-BackColor="#dddddd">                                           
                            <Columns>
                                <asp:TemplateField HeaderText=""><ItemTemplate>
                                    <asp:LinkButton runat="server" ID="lnkConsulta" CommandName="seleccionarRH" CommandArgument='<%#Eval("Cédula") %>'   > Consultar </asp:LinkButton>
                                </ItemTemplate>
                                </asp:TemplateField>                     
                            </Columns> 
                        </asp:GridView>
                        </div>
                    </div>
                    </div>
         </div>




 </div>


              <!-- Modal Dialog/   Con esto se permite crear los mensajes de aviso en el formato lindo-->
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

      <%--Modal Confirmar--%>
    <div class="modal fade" id="modalConfirmar" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h3 class="modal-title" id="modalConfirma"><i class="fa fa-exclamation-triangle text-danger fa-2x"><font color = "#Red">Aviso</font></i></h3>
                </div>
                <div class="modal-body">
                     Está seguro que desea eliminar este recurso humano?            
                </div>
                <div class="modal-footer">
                    <asp:Button ID="botonAceptarEliminar" class="btn btn-info" style="border:#7BC143 ; background:#0094ff" Text="Aceptar" OnClick="btnAceptar_Eliminar" runat="server"/> 
                    <button type="button" id="botonVolver" style="border:#0094ff; background:#0094ff" class="btn btn-danger" data-dismiss="modal">Cancelar</button>
                </div>
            </div>
        </div>
    </div>




</asp:Content>
