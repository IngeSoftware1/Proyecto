<%@ Page Title="Proyecto de Pruebas" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProyectodePruebas.aspx.cs" Inherits="ProyectoInge.ProyectodePruebas" Async="true" %>


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
            <li><a runat="server" style="color:white"  >Proyecto</a></li>
            <li><a runat="server" style="color:white" >Diseño de pruebas</a></li>
            <li><a runat="server" style="color:white" >Caso de pruebas</a></li>
            <li><a runat="server" style="color:white">Ejecución de pruebas</a></li>
            <li><a runat="server" href="~/Login.aspx" style="color:white">Cerrar sesión</a></li>      </ul>
        </div><!-- /.navbar-collapse -->
      </div><!-- /.container-fluid -->
    </nav>
    <div class="row">
        <div class="col-lg-12">
            <%--Titulo de la pantalla de Proyectos--%>
            <h2 class="estilo">Módulo de Proyectos de Pruebas</h2>
        </div>

        <div class="col-lg-11">
            <%--Div de botones para el IMEC--%>
            <div id="btnsControl" style="float: right">
                <asp:Button ID="btnInsertar" runat="server" Text="Insertar" CssClass="btn btn-primary" />
                <asp:Button ID="btnModificar" runat="server" Text="Modificar" CssClass="btn btn-primary" />
                <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-primary" />
            </div>
        </div>

        <br>
        <br>
        <br>
        <br>
        <div id="DatosProyectos">
            <%--Almacena todos los datos necesarios para el proyecto--%>
            <div class="row">
                <asp:Label runat="server" ID="lblDatosProy" Text="Datos del Proyecto" CssClass="col-sm-7 col-sm-offset-1 control-label"></asp:Label>
                <asp:Label runat="server" ID="lblOficina" Text="Datos Oficina Usuaria:" CssClass="col-sm-4 col-sm-pull-2 control-label"></asp:Label>

            </div>
            <div class="row">
                <br>
                <div class="col-sm-5 col-sm-offset-1">
                    <div class="panel panel-default" style="height: 500px">
                        <div class="panel-body">

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
                                    <asp:Label ID="lblFecha" runat="server" Text="Fecha de asignación:" CssClass="col-sm-8 col-sm-offset-1 control-label"></asp:Label>
                                    <div class="col-sm-6 col-sm-offset-5 ">

                                        <asp:Calendar runat="server" ID="calendarFecha"></asp:Calendar>
                                        <%--No se si hay que agregarle un estilo--%>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">

                                <%--Estado del proceso de pruebas--%>
                                <div class="row">
                                    <asp:Label ID="lblEstado" runat="server" Text="Estado:" CssClass="col-sm-5 col-sm-offset-1 control-label"></asp:Label>
                                    <div class="col-sm-6 col-sm-pull-1">
                                        <asp:DropDownList runat="server" ID="comboEstado" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">

                                <%--Líder del Proyecto --%>
                                <div class="row">
                                    <asp:Label ID="lblLider" runat="server" Text="Líder:" CssClass="col-sm-5 col-sm-offset-1 control-label"></asp:Label>
                                    <div class="col-sm-6 col-sm-pull-1">
                                        <asp:DropDownList runat="server" ID="comboLider" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>


                            <div class="form-group">

                                <%--Objetivo general--%>
                                <div class="row">
                                    <asp:Label ID="lblObjetivo" runat="server" Text="Objetivo General" CssClass="col-sm-5 col-sm-offset-1 control-label"></asp:Label>
                                    <div class="col-sm-8 col-sm-offset-1">
                                        <asp:TextBox runat="server" ID="txtObjetivo" CssClass="form-control " MaxLength="30"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <%--Cierra el panel body--%>
                    </div>
                </div>
                <%--Cierra el panel panel-default--%>

                <div class="col-sm-5">
                    <div class="panel panel-default" style="height: 500px">
                        <div class="panel-body">
                            <div class="form-group">

                                <%--Nombre Oficina Usuaria--%>
                                <div class="row">
                                    <asp:Label ID="lblNombreOficina" runat="server" Text="Nombre Oficina:" CssClass="col-sm-4 col-sm-offset-1 control-label"></asp:Label>
                                    <div class="col-sm-6">
                                        <asp:TextBox runat="server" ID="txtnombreOficina" CssClass="form-control" MaxLength="20"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">

                                <%-- Nombre del representante de la oficina usuaria--%>
                                <div class="row">
                                    <asp:Label ID="lblNombreRep" runat="server" Text="Nombre Representante:" CssClass="col-sm-1 col-sm-offset-1 control-label"></asp:Label>
                                    <div class="col-sm-6 col-sm-offset-3">
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
                                        <asp:TextBox runat="server" ID="txtTelefonoOficina" PlaceHolder="88888888" CssClass="form-control" TextMode="Phone" MaxLength="11"></asp:TextBox>
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
                                        <asp:LinkButton runat="server" ID="lnkQuitar" Style="height: 100px" CssClass="">
                                        <span aria-hidden="true" class="glyphicon glyphicon-minus-sign blueColor"></span>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <br>
            <div class="row">
                <asp:Label runat="server" ID="lblRecursosHumanos" Text="Datos Recursos Humanos" CssClass="col-sm-6 col-sm-offset-1 control-label"></asp:Label>
            </div>
            <br>

            <div class="row">
                <div class="col-sm-10 col-sm-offset-1">
                    <div class="panel panel-default" style="height: 250px; width: 995px;">
                        <div class="panel-body">
                            <div class="form-group col-sm-5">
                                <div class="row">

                                    <%-- Miembros no asignados a un proyecto --%>
                                    <asp:Label ID="lblRH" runat="server" Text="Miembros no asignados" CssClass="col-sm-12 col-sm-offset-1 control-label"></asp:Label>
                                    <div class="col-sm-12 col-sm-offset-1">
                                        <asp:ListBox runat="server" ID="listaMiembrosNoAsignados" CssClass="form-control" Style="height: 170px"></asp:ListBox>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group col-sm-1">
                                <div class="row">
                                    <div class="">
                                        <br>
                                        <br>
                                        <%-- Botón para agregar miembros a un proyecto --%>
                                        <asp:LinkButton runat="server" ID="lnkAgregarMiembros" Style="height: 100px" CssClass="col-sm-offset-11">
                                        <span aria-hidden="true" class="glyphicon glyphicon-hand-right blueColor"></span>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                                <br>
                                <br>
                                <br>
                                <div class="row">
                                    <div class="">
                                        <%-- Botón para quitar miembros de un proyecto --%>
                                        <asp:LinkButton runat="server" ID="lnkQuitarMiembros" Style="height: 100px" CssClass="col-sm-offset-11">
                                        <span aria-hidden="true" class="glyphicon glyphicon-hand-left blueColor"></span>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group col-sm-5">
                                <div class="row">
                                    <%-- Miembros asignados a un proyecto --%>
                                    <asp:Label ID="lblRHAsignados" runat="server" Text="Miembros asignados" CssClass="col-sm-12 col-sm-offset-2 control-label"></asp:Label>
                                    <div class="col-sm-12 col-sm-offset-2">
                                        <asp:ListBox runat="server" ID="listMiembrosAgregados" CssClass="form-control" Style="height: 170px"></asp:ListBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <%--Cierra el div de datos de proyecto en general--%>
        </div>

        <div class="col-lg-11">
            <%-- Botones para aceptar y cancelar --%>
            <div id="btnsBD" style="float: right">
                <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" CssClass="btn btn-primary" />
                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-primary" />
            </div>
        </div>
        <br>
        <br>

        <%-- Grid para mostrar los proyectos --%>
        <div class="form-group">
            <div class="row">
                <div id="consultar">
                    <asp:Label ID="lblProyectos" runat="server" Text="Lista de Proyectos" CssClass="col-sm-3 col-sm-offset-1 control-label"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-6 col-sm-offset-1">
                    <div id="scroll" style="height: 183px; width: 670px; overflow: auto;">
                        <asp:GridView ID="gridRH" runat="server" Style="width: 650px" AutoGenerateColumns="true" HeaderStyle-BackColor="#444444" HeaderStyle-ForeColor="White" AlternatingRowStyle-BackColor="#dddddd">
                            <Columns>
                                <asp:TemplateField HeaderText="Consultar Funcionario">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkConsulta" CommandName="seleccionarProyecto"> Consultar </asp:LinkButton>
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

