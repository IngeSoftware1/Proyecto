<%@ Page Title="Recursos Humanos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" 

CodeBehind="RecursosHumanos.aspx.cs" Inherits="ProyectoInge.RecursosHumanos" Async="true" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <h2 class="estilo">Módulo de Recursos Humanos</h2>
        </div>

        <%-- Botones para realizar el IMEC en el modulo --%>
        <div class="col-lg-11">
            <%-- Botones para realizar el IMEC en el modulo --%>
            <div id="btnsControl" style="float: right">
                <asp:Button ID="btnInsertar" runat="server" Text="Insertar" CssClass="btn btn-primary" OnClick="btnInsertar_Click"/>
                <asp:Button ID="btnModificar" runat="server" Text="Modificar" CssClass="btn btn-primary" />
                <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-primary" />
            </div>

        </div>

        <br>
        <br>
        <div id="Datos"> <%-- Div que almacena todos los div internos para los datos del RH --%>
            <div class="row">
                <div class="col-sm-5 col-sm-offset-1">
                    <div class="panel panel-default" style ="height: 350px">
                        <div class="panel-body" >
                            <div class="form-group">
                                <div class="row">
                                    <asp:Label ID="lblCedula" runat="server" Text="Cédula:" CssClass="col-sm-1 col-sm-offset-1 control-label"></asp:Label>
                                    <div class="col-sm-6 col-sm-offset-3 ">
                                        <asp:TextBox runat="server" ID="txtCedula" CssClass="form-control" MaxLength="9"></asp:TextBox>
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
                                        <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control" MaxLength="20"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="EREmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="*Correo inválido"
                            ForeColor="Red" ValidationExpression="[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}"> </asp:RegularExpressionValidator>
                                    </div>
                                </div>
                            </div>      
                         </div>
                         <br>
                        <br>
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
                                        <asp:TextBox runat="server" ID="txtContrasena" CssClass="form-control" TextMode="Password" MaxLength="30"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <asp:Label ID="lblConfirmar" runat="server" Text="Confirmar contraseña:" CssClass="col-sm-5 col-sm-offset-1 control-label"></asp:Label>
                                    <div class="col-sm-6">
                                        <asp:TextBox runat="server" ID="txtConfirmar" CssClass="form-control" TextMode="Password" MaxLength="30"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <asp:Label ID="lblPerfil" runat="server" Text="Perfil:" CssClass="col-sm-5 col-sm-offset-1 control-label"></asp:Label>
                                    <div class="col-sm-6">
                                        <asp:DropDownList runat="server" ID="comboPerfil" CssClass="form-control">
                                            <asp:ListItem Value="Administrador">Administrador</asp:ListItem>
                                            <asp:ListItem Value="Miembro de equipo de pruebas">Miembro de equipo de pruebas</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <asp:Label ID="lblRol" runat="server" Text="Rol:" CssClass="col-sm-5 col-sm-offset-1 control-label"></asp:Label>
                                    <div class="col-sm-6">
                                        <asp:DropDownList runat="server" ID="comboRol" CssClass="form-control">
                                            <asp:ListItem Value="Líder de pruebas">Líder de pruebas</asp:ListItem>
                                            <asp:ListItem Value="Tester">Tester</asp:ListItem>
                                            <asp:ListItem Value="Usuario">Usuario</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-5 col-sm-offset-1">
                    <div class="panel panel-default" style ="height: 350px">
                        <div class="panel-body">
                            <div class="form-group">
                                <div class="row">
                                    <asp:Label ID="lblTelefono" runat="server" Text="Teléfono:" CssClass="col-sm-2 col-sm-offset-1 control-label"></asp:Label>
                                    <div class="col-sm-6 col-sm-offset-2">
                                        <asp:TextBox runat="server" ID="txtTelefono" CssClass="form-control" TextMode="Phone" MaxLength="11"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="REV2" runat="server" ControlToValidate="txtTelefono" ErrorMessage="*Ingrese Valores Numéricos"
                            ForeColor="Red" ValidationExpression="^[0-9]*"> </asp:RegularExpressionValidator>
                                    </div>
                                    <div class="">
                                        <asp:Button runat="server" ID="btnNumero" Text="+" CssClass="img-circle btn-primary" OnClick="btnAgregarTelefono" />
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <asp:Label ID="lbltels" runat="server" Text="Teléfonos agregados" CssClass="col-sm-5 

col-sm-offset-1 control-label"></asp:Label>
                                </div>
                                <div class="row">
                                    <div class="col-sm-10 col-sm-offset-1">
                                        <asp:ListBox runat="server" ID="listTelefonos" CssClass="form-control"></asp:ListBox>
                                        </div>
                                        <div class="">
                                        <asp:Button runat="server" ID="btnQuitar" Text="-" CssClass="img-circle btn-primary" OnClick="btnEliminarTelefono"/>
                                    </div>

                                    </div>
                                </div>
                        </div>

                    </div>
                </div>
            </div>
             <div class="form-group">
             <div class="row">
            <div id="consulta">
                <asp:Label ID="lblconsulta" runat="server" Text="Lista de funcionarios" CssClass="col-sm-3 col-sm-offset-1 control-label"></asp:Label>
              </div> 
                </div>   
                <div class ="row">
                <div class="col-sm-6 col-sm-offset-1"> 
                        <asp:GridView ID="gridRH" runat="server" AutoGenerateColumns="True" OnRowCommand ="gridVentas_RowCommand" OnPageIndexChanged="gridVentas_PageIndexChanged">             
                            <Columns>
                                <asp:ButtonField ButtonType="Button" Text="Consultar" CommandName="seleccionarRH" Visible="true" CausesValidation="false" />
                            </Columns> 
                        </asp:GridView>
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
    <%-- El grid para consultar se debe activar --%>
    <%-- %>div id="consulta"--%>
    <%-- %>asp:Label ID="lblconsulta" runat="server" Text="Lista de funcionarios" CssClass="estiloLabelRH"></--%><br>
    <%-- <asp:GridView ID="gridRH" runat="server" OnRowCommand="gridRH_RowCommand" 

OnPageIndexChanged="gridRH_PageIndexChanged">
                <Columns>
                    <asp:ButtonField ButtonType="Button" Text="Consultar" CommandName="seleccionarRH" Visible="true" 

CausesValidation="false" />
                </Columns> 
            </asp:GridView> --%>
        </%>
    </div>

</asp:Content>