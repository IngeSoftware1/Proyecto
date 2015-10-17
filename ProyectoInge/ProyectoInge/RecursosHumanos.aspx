<%@ Page Title="Recursos Humanos" Language="C#" MasterPageFile="~/Site.Master"  AutoEventWireup="true" CodeBehind="RecursosHumanos.aspx.cs" Inherits="ProyectoInge.RecursosHumanos" Async="true" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    
      <nav class="navbar-default">
      <div class="container-fluid">
        <!-- Brand and toggle get grouped for better mobile display -->
        <div class="navbar-header">
          <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1" aria-expanded="false">
            <span class="sr-only">Toggle navigation</span>
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
          </button>
          <a class="navbar-brand" href="#"></a>
        </div>
        <!-- Collect the nav links, forms, and other content for toggling -->
        <div class="navbar-default collapse navbar-collapse" id="bs-example-navbar-collapse-1">
          <ul class="nav navbar-nav">
            <li><a runat="server"  href="~/RecursosHumanos.aspx"  style="color:white"  >Recursos Humanos</a></li>
            <li><a runat="server" href="~/ProyectodePruebas.aspx"  style="color:white"  >Proyecto</a></li>
            <li><a runat="server" style="color:white" >Diseño de pruebas</a></li>
            <li><a runat="server" style="color:white" >Caso de pruebas</a></li>
            <li><a runat="server" style="color:white">Ejecución de pruebas</a></li>
            <li><a runat="server" onserverclick="cerrarSesion" href="~/Login.aspx" style="color:white">Cerrar sesión</a></li>      </ul>
        </div><!-- /.navbar-collapse -->
      </div><!-- /.container-fluid -->
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
                    <asp:Label runat="server" ID="lblDatosPersonales" Text="Datos Personales:" CssClass="col-sm-5 col-sm-offset-1 control-label"></asp:Label>
                </div> 
            <div class="row">
                
                <br>
                    <div class="col-sm-5 col-sm-offset-1">
                    <div class="panel panel-default" style ="height: 600px">
                        <div class="panel-body" >
                            <div class="form-group">
                                <div class="row">
                                    <asp:Label ID="lblCedula" runat="server" Text="Cédula:" CssClass="col-sm-1 col-sm-offset-1 control-label"></asp:Label>
                                    <div class="col-sm-6 col-sm-offset-3 "> 
                                        <asp:TextBox runat="server" ID="txtCedula" PlaceHolder ="145680958" CssClass="form-control" MaxLength="9"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="ERValidator" runat="server" ControlToValidate="txtCedula" ErrorMessage="*Ingrese Valores Numéricos"
                            ForeColor="Red" ValidationExpression="^[0-9]*"> </asp:RegularExpressionValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <asp:Label ID="lblNombre" runat="server" Text="Nombre:" CssClass="col-sm-1 col-sm-offset-1 control-label"></asp:Label>
                                    <div class="col-sm-6 col-sm-offset-3 ">
                                        <asp:TextBox runat="server" ID="txtNombre" CssClass="form-control" MaxLength="20"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="ERNombre" runat="server" ControlToValidate="txtNombre" ErrorMessage="*Ingrese solo letras"
                            ForeColor="Red" ValidationExpression="^[A-Za-z]*$"> </asp:RegularExpressionValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <asp:Label ID="lblApellido1" runat="server" Text="Primer apellido:" CssClass="col-sm-4 col-sm-offset-1 control-label"></asp:Label>
                                    <div class="col-sm-6 col-sm -offset-1 ">
                                        <asp:TextBox runat="server" ID="txtApellido1" CssClass="form-control" MaxLength="20"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="ERLetrasApe1" runat="server" ControlToValidate="txtApellido1" ErrorMessage="*Ingrese solo letras"
                            ForeColor="Red" ValidationExpression="^[A-Za-z]*$"> </asp:RegularExpressionValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <asp:Label ID="lblApellido2" runat="server" Text="Segundo apellido:" CssClass="col-sm-4 col-sm-offset-1 control-label"></asp:Label>
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
                                        <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control" MaxLength="30"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="EREmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="*Correo inválido"
                            ForeColor="Red" ValidationExpression="[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}"> </asp:RegularExpressionValidator>
                                    </div>
                                </div>
                            </div>  
                            <div class="form-group">
                                <div class="row">
                                    <asp:Label ID="lblTelefono" runat="server" Text="Teléfono:" CssClass="col-sm-2 col-sm-offset-1 control-label"></asp:Label>
                                    <div class="col-sm-6 col-sm-offset-2"> 
                                        <asp:TextBox runat="server" ID="txtTelefono"  PlaceHolder ="88888888" CssClass="form-control" TextMode="Phone" MaxLength="11"></asp:TextBox>
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
                                    <asp:Label ID="lblPerfil" runat="server" Text="Perfil:" CssClass="col-sm-5 col-sm-offset-1 control-label" ></asp:Label>
                                    <div class="col-sm-6">
                                        <asp:DropDownList runat="server" ID="comboPerfil" AutoPostBack="True"  CssClass="form-control" onselectedindexchanged="perfilSeleccionado" >
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <asp:Label ID="lblRol" runat="server" Text="Rol:" CssClass="col-sm-5 col-sm-offset-1 control-label "></asp:Label>
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
                <asp:Label ID="lblFuncionarios" runat="server" Text="Lista de funcionarios" CssClass="col-sm-3 col-sm-offset-1 control-label"></asp:Label>
              </div> 
                </div>   
                    <div class ="row">
                <div class="col-sm-6 col-sm-offset-1"> 
                    <div id ="scroll" style ="height: 183px; width:670px; overflow:auto;" >
                        <asp:GridView ID="gridRH" runat="server"  style ="width: 650px" AutoGenerateColumns="true" OnRowCommand ="gridFuncionarios_RowCommand" HeaderStyle-BackColor="#444444" HeaderStyle-ForeColor="White" AlternatingRowStyle-BackColor="#dddddd">                                           
                            <Columns>
                                <asp:TemplateField HeaderText="Consultar Funcionario"><ItemTemplate>
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

</asp:Content>
