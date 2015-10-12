<%@ Page Title="Proyecto de Pruebas" Language="C#" MasterPageFile="~/Site.Master"  AutoEventWireup="true" CodeBehind="ProyectodePruebas.aspx.cs" Inherits="ProyectoInge.ProyectodePruebas" Async="true" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-lg-12">
            <%--Titulo de la pantalla de Proyectos--%>
                <h2 class="estilo">Módulo de Proyectos de Pruebas</h2>
            </div>

        <div class="col-lg-11">
            <%--Div de botones para el IMEC--%>
        <div id="btnsControl" style="float: right">
                    <asp:Button ID="btnInsertar" runat="server" Text="Insertar" CssClass="btn btn-primary" />
                    <asp:Button ID="btnModificar" runat="server" Text="Modificar" CssClass="btn btn-primary"  />
                    <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-primary"  />
                </div>
         </div>

        <br>
        <br>
        <br>
        <br>
        <div id="DatosProyectos"> <%--Almacena todos los datos necesarios para el proyecto--%>
            <div class ="row"> 
                    <asp:Label runat="server" ID="lblDatosProy" Text="Datos del Proyecto" CssClass="col-sm-5 col-sm-offset-1 control-label"></asp:Label>
                </div> 
            <div class="row">
                <br>
                <div class="col-sm-5 col-sm-offset-1">
                    <div class="panel panel-default" style ="height: 600px">
                        <div class="panel-body" >

                            <div class="form-group">

                                <%--Nombre del proyecto--%>
                                <div class="row">
                                    <asp:Label ID="lblNombreProy" runat="server" Text="Nombre:" CssClass="col-sm-1 col-sm-offset-1 control-label"></asp:Label>
                                    <div class="col-sm-6 col-sm-offset-3 "> 
                                        <asp:TextBox runat="server" ID="txtNombreProy" CssClass="form-control" MaxLength="20"></asp:TextBox>
                                    </div>
                                </div>

                            </div>

                            <div class="form-group">

                                <%--Fecha de asignación--%>
                                <div class="row">
                                    <asp:Label ID="lblFecha" runat="server" Text="Fecha de asignación:" CssClass="col-sm-1 col-sm-offset-1 control-label"></asp:Label>
                                    <div class="col-sm-6 col-sm-offset-3 ">
                                        
                                        <asp:Calendar runat="server" ID="calendarFecha"></asp:Calendar> <%--No se si hay que agregarle un estilo--%>
                                    </div>
                                </div>

                            </div>

                            <div class="form-group">

                                <%--Estado del proceso de pruebas--%>
                                <div class="row">
                                    <asp:Label ID="lblEstado" runat="server" Text="Estado:" CssClass="col-sm-5 col-sm-offset-1 control-label"></asp:Label>
                                    <div class="col-sm-6">
                                        <asp:DropDownList runat="server" ID="comboEstado" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>

                            </div>

                            <div class="form-group">

                                <%--Objetivo general--%>
                                 <div class="row">
                                    <asp:Label ID="lblObjetivo" runat="server" Text="Objetivo General" CssClass="col-sm-5 col-sm-offset-1 control-label"></asp:Label>
                                </div>
                                <div class="row">
                                    <div class="col-sm-10 col-sm-offset-1">
                                        <asp:Textbox runat="server" ID="txtObjetivo" CssClass="form-control" MaxLength="30"></asp:Textbox>
                                        </div>
                                    </div>

                            </div>

                </div> <%--Cierra el panel body--%>
                </div> <%--Cierra el panel panel-default--%>
                    </div>

                 <div class ="row"> 
                    <asp:Label runat="server" ID="lblOficina" Text="Datos Oficina Usuaria:" CssClass="col-sm-5 col-sm-offset-1 control-label"></asp:Label>
                </div> 

                <div class="col-sm-5 ">
                    <div class="panel panel-default" style ="height: 350px">
                        <div class="panel-body">
                            <div class="form-group">

                                <%--Nombre Oficina Usuaria--%>
                                <div class="row">
                                    <asp:Label ID="lblNombreOficina" runat="server" Text="Nombre:" CssClass="col-sm-1 col-sm-offset-1 control-label"></asp:Label>
                                    <div class="col-sm-6 col-sm-offset-4">
                                        <asp:TextBox runat="server" ID="txtnombreOficina" CssClass="form-control" MaxLength="20"></asp:TextBox>
                                    </div>
                                </div>

                            </div>

                            <div class="form-group">

                               <%-- Nombre del representante de la oficina usuaria--%>
                                <div class="row">
                                    <asp:Label ID="lblNombreRep" runat="server" Text="Nombre Representante:" CssClass="col-sm-1 col-sm-offset-1 control-label"></asp:Label>
                                    <div class="col-sm-6 col-sm-offset-4">
                                        <asp:TextBox runat="server" ID="txtnombreRep" CssClass="form-control" MaxLength="20"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="ERNombre" runat="server" ControlToValidate="txtNombreRep" ErrorMessage="*Ingrese solo letras"
                            ForeColor="Red" ValidationExpression="^[A-Za-z]*$"> </asp:RegularExpressionValidator>
                                    </div>
                                </div>

                            </div>

                            <div class="form-group">

                               <%-- Apellido 1 del representante de la oficina usuaria--%>
                                <div class="row">
                                    <asp:Label ID="lblApellido1" runat="server" Text="Primer apellido:" CssClass="col-sm-4 col-sm-offset-1 control-label"></asp:Label>
                                    <div class="col-sm-6 col-sm -offset-1 ">
                                        <asp:TextBox runat="server" ID="txtApellido1Rep" CssClass="form-control" MaxLength="20"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="ERLetrasApe1" runat="server" ControlToValidate="txtApellido1Rep" ErrorMessage="*Ingrese solo letras"
                            ForeColor="Red" ValidationExpression="^[A-Za-z]*$"> </asp:RegularExpressionValidator>
                                    </div>
                                </div>

                            </div>

                            <div class="form-group">

                                <%-- Apellido 2 del representante de la oficina usuaria--%>
                                <div class="row">
                                    <asp:Label ID="lblApellido2" runat="server" Text="Segundo apellido:" CssClass="col-sm-4 col-sm-offset-1 control-label"></asp:Label>
                                    <div class="col-sm-6 ">
                                        <asp:TextBox runat="server" ID="txtApellido2Rep" CssClass="form-control" MaxLength="20"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="ERApe2" runat="server" ControlToValidate="txtApellido2Rep" ErrorMessage="*Ingrese solo letras"
                            ForeColor="Red" ValidationExpression="^[A-Za-z]*$"> </asp:RegularExpressionValidator>
                                    </div>
                                </div>

                            </div>


                            <div class="form-group">

                                <%-- Telefonos de la oficina usuaria--%>
                                <div class="row">
                                    <asp:Label ID="lblTelefono" runat="server" Text="Teléfono:" CssClass="col-sm-2 col-sm-offset-1 control-label"></asp:Label>
                                    <div class="col-sm-6 col-sm-offset-2"> 
                                        <asp:TextBox runat="server" ID="txtTelefonoOficina"  PlaceHolder ="88888888" CssClass="form-control" TextMode="Phone" MaxLength="11"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="REV2" runat="server" ControlToValidate="txtTelefonoOficina" ErrorMessage="*Ingrese Valores Numéricos"
                            ForeColor="Red" ValidationExpression="^[0-9]*"> </asp:RegularExpressionValidator>
                                    </div>
                                    <div class="">
                                         <asp:LinkButton runat="server" ID="lnkNumero" CssClass="glyphicon.glyphicon-plus-sign">
                                        <span aria-hidden="true" class="glyphicon glyphicon-plus-sign blueColor"></span>
                                        </asp:LinkButton>
                                    </div>
                                </div>

                            </div>

                            <div class="form-group">

                                <%-- Telefonos de la oficina usuaria--%>
                                <div class="row">
                                    <asp:Label ID="lbltels" runat="server" Text="Teléfonos agregados" CssClass="col-sm-5 col-sm-offset-1 control-label"></asp:Label>
                                </div>

                                <div class="row">
                                    <div class="col-sm-10 col-sm-offset-1">
                                        <asp:ListBox runat="server" ID="listTelefonosOficina" CssClass="form-control"></asp:ListBox>
                                        </div>
                                        <div class="">
                                         <asp:LinkButton runat="server" ID="lnkQuitar" style= "height: 100px" CssClass="">
                                        <span aria-hidden="true" class="glyphicon glyphicon-minus-sign blueColor"></span>
                                        </asp:LinkButton>
                                    </div>
                                </div>

                            </div>

                        </div>
                    </div>
                </div>



                </div>
        </div> <%--Cierra el div de datos de proyecto en general--%>
    </div>
</asp:Content>