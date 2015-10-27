<%@ Page Title="Proyecto de Pruebas" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProyectodePruebas.aspx.cs" Inherits="ProyectoInge.ProyectodePruebas" Async="true" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-group">
        <div class="row">
            <div class="col-sm-9 ">
                <nav class="navbar navbar-default navbar-static-top">
                    <ul class="navbar-default nav navbar-nav">
                        <li><a runat="server" href="~/RecursosHumanos.aspx">Recursos Humanos</a></li>
                        <li><a runat="server" style="background-color: ActiveCaption" href="~/ProyectodePruebas.aspx">Proyecto</a></li>
                        <li><a runat="server" href="~/DisenoPruebas.aspx">Diseño de pruebas</a></li>
                        <li><a runat="server" href="~/CasoDePrueba.aspx">Caso de pruebas</a></li>
                        <li><a runat="server">Ejecución de pruebas</a></li>
                        <li><a runat="server" onserverclick="cerrarSesion" href="~/Login.aspx">Cerrar sesión</a></li>
                    </ul>
                </nav>
            </div>
            <br />
            <asp:Label ID="lblLogueado" runat="server" Text="" Font-Bold="True" CssClass="col-sm-2 col-sm-push-1 control-label"></asp:Label>
        </div>
    </div>

    <div class="row">
        <div class="col-lg-12">
            <%--Titulo de la pantalla de Proyectos--%>
            <h2 class="estilo">Módulo de Proyectos de Pruebas</h2>
        </div>

        <div class="col-lg-11">
            <%--Div de botones para el IMEC--%>
            <div id="btnsControl" style="float: right">
                <asp:Button ID="btnInsertar" runat="server" Text="Insertar" CssClass="btn btn-primary" OnClick="btnInsertar_Click" />
                <asp:Button ID="btnModificar" runat="server" Text="Modificar" CssClass="btn btn-primary" OnClick="btnModificar_Click" />
                <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-primary" OnClick="btnEliminar_Click" />
            </div>
        </div>

        <br>
        <br>
        <br>
        <br>
        <div id="DatosProyectos">
            <%--Almacena todos los datos necesarios para el proyecto--%>
            <div class="row">
                <asp:Label runat="server" ID="lblDatosProy" Font-Bold="True" Text="Datos del proyecto" CssClass="col-sm-7 col-sm-offset-1 control-label"></asp:Label>
                <asp:Label runat="server" ID="lblOficina" Font-Bold="True" Text="Datos oficina usuaria" CssClass="col-sm-4 col-sm-pull-2 control-label"></asp:Label>

            </div>
            <div class="row">

                <div class="col-sm-5 col-sm-offset-1">
                    <div class="panel panel-default" style="height: 500px">
                        <div class="panel-body">

                            <div class="form-group">

                                <%--Nombre del proyecto--%>
                                <div class="row">
                                    <asp:Label ID="lblNombreProy" runat="server" Text="Nombre*:" CssClass="col-sm-1 col-sm-offset-1 control-label"></asp:Label>
                                    <div class="col-sm-6 col-sm-offset-3 ">
                                        <asp:TextBox runat="server" ID="txtNombreProy" CssClass="form-control" MaxLength="20"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">

                                <%--Objetivo general--%>
                                <div class="row">
                                    <asp:Label ID="lblObjetivo" runat="server" Text="Objetivo general*" CssClass="col-sm-5 col-sm-offset-1 control-label"></asp:Label>
                                </div>
                                <div class="row col-sm-11">
                                    <asp:TextBox runat="server" ID="txtObjetivo" CssClass="col-sm-offset-1 form-control" MultiLine="true" TextMode="MultiLine" Height="77px" MaxLength="70"></asp:TextBox>
                                </div>
                            </div>
                            <br>
                            <br>
                            <br>
                            <br>
                            <asp:UpdatePanel ID="UpdatePanelCalendario" runat="server" UpdateMode="conditional" ChildrenAsTriggers="false">
                                <ContentTemplate>

                                    <div class="form-group">
                                        <%--Fecha de asignación--%>

                                        <div class="row">

                                            <asp:Label ID="lblFecha" runat="server" Text="Fecha asignación*:" CssClass="col-sm-4 col-sm-offset-1 control-label"></asp:Label>
                                            <div class="col-sm-5 ">
                                                <asp:TextBox runat="server" ID="txtCalendar" CssClass=" form-control"></asp:TextBox>
                                            </div>
                                            <div class="">

                                                <div class="col-sm-push-5">
                                                    <asp:LinkButton runat="server" ID="lnkCalendario" CssClas=".glyphicon.glyphicon-calendar" OnClick="lnkCalendario_Click">
                                        <span aria-hidden="true" class="glyphicon glyphicon-calendar blueColor" ></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                            <br>

                                            <div class="row col-sm-6 col-sm-offset-5">
                                                <asp:Calendar runat="server" ID="calendarFecha" Visible="false" OnVisibleMonthChanged="cambioDeMes" OnSelectionChanged="calendarioSeleccionado"></asp:Calendar>

                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>

                            <div class="form-group">

                                <%--Estado del proceso de pruebas--%>
                                <div class="row">
                                    <asp:Label ID="lblEstado" runat="server" Text="Estado*:" CssClass="col-sm-5 col-sm-offset-1 control-label"></asp:Label>
                                    <div class="col-sm-6 col-sm-pull-1">
                                        <asp:DropDownList runat="server" ID="comboEstado" CssClass="form-control">
                                        </asp:DropDownList>
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
                                    <asp:Label ID="lblNombreOficina" runat="server" Text="Nombre oficina*:" CssClass="col-sm-4 col-sm-offset-1 control-label"></asp:Label>
                                    <div class="col-sm-6">
                                        <asp:TextBox runat="server" ID="txtnombreOficina" CssClass="form-control" MaxLength="20"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">

                                <%-- Nombre del representante de la oficina usuaria--%>
                                <div class="row">
                                    <asp:Label ID="lblNombreRep" runat="server" Text="Nombre representante:" CssClass="col-sm-1 col-sm-offset-1 control-label"></asp:Label>
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

                            <asp:UpdatePanel ID="Update_Tel" runat="server" UpdateMode="conditional" ChildrenAsTriggers="false">
                                <ContentTemplate>
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
                                                <asp:LinkButton runat="server" ID="lnkNumero" CssClass="glyphicon.glyphicon-plus-sign" OnClick="btnAgregarTelefono">
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
                                                <asp:LinkButton runat="server" ID="lnkQuitar" Style="height: 100px" CssClass="" OnClick="btnEliminarTelefono">
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
            </div>
            <br>

            <%--Panel Requerimientos --%>

            <div class="row">
                <asp:Label runat="server" ID="lblRequerimientos" Font-Bold="True" Text="Datos requerimientos" CssClass="col-sm-6 col-sm-offset-1 control-label"></asp:Label>
            </div>


            <asp:UpdatePanel ID="UpdateRequerimientos" runat="server" UpdateMode="conditional" ChildrenAsTriggers="false">
                <ContentTemplate>
                    <div class="row">
                        <div class="col-sm-10 col-sm-offset-1">
                            <div class="panel panel-default" style="height: 290px;">
                                <div class="panel-body">

                                    <div class="form-group col-sm-5">
                                        <div class="row">
                                            <asp:Label ID="lblNuevoReq" runat="server" Text="Nuevo requerimiento:" CssClass="col-sm-5 col-sm-offset-1 control-label"></asp:Label>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="">
 
                                            <br>
                                            <asp:Label ID="lblSiglaReq" runat="server" Text="Código:" CssClass="col-sm-1 col-sm-offset-2 control-label"></asp:Label>
                                            <div class="col-sm-2 ">
                                                <asp:TextBox runat="server" ID="txtIdReq" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                            </div>
                                            <asp:Label ID="lblNombreReq" runat="server" Text="Nombre:" CssClass="col-sm-1  control-label"></asp:Label>

                                            <div class="col-sm-4">
                                                <asp:TextBox runat="server" ID="txtNombreReq" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                            </div>

                                            <%-- Botón para agregar requerimientos a un proyecto --%>
                                            <div class="col-sm-1 ">
                                                <asp:LinkButton runat="server" ID="lnkAgregarRequerimientos" Style="height: 100px" CssClass="" OnClick="btnAgregarRequerimiento">
                                            <span aria-hidden="true" class="glyphicon glyphicon-plus-sign blueColor col-sm-push-11"></span>
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="form-group col-sm-5">
                                        <div class="row">
                                            <%-- Requerimientos asignados a un proyecto --%>
                                            <asp:Label ID="lblRequerimiento" runat="server" Text="Lista requerimientos:" CssClass="col-sm-6 col-sm-offset-1 control-label"></asp:Label>
                                        </div>
                                    </div>

                                    <br>
                                    <div class="row">
                                        <div class="">

                                            <div class="col-sm-8 col-sm-offset-2">
                                                <asp:ListBox runat="server" ID="listRequerimientosAgregados" Style="height: 120px" CssClass="form-control" ></asp:ListBox>
                                            </div>

                                            <%-- Botón para quitar requerimientos de un proyecto --%>
                                            <div class="col-sm-1 ">
                                                <asp:LinkButton runat="server" ID="lnkQuitarRequerimientos"  OnClick="btnEliminarRequerimiento">
                                                <span aria-hidden="true" class="glyphicon glyphicon-minus-sign blueColor col-sm-push-11"></span>
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

            <br>
            <%--Panel Recursos Humanos --%>

            <div class="row">
                <asp:Label runat="server" ID="lblRecursosHumanos" Font-Bold="True" Text="Datos recursos humanos" CssClass="col-sm-6 col-sm-offset-1 control-label"></asp:Label>
            </div>


            <asp:UpdatePanel ID="UpdateAsociarDesasociarMiembros" runat="server" UpdateMode="conditional" ChildrenAsTriggers="false">
                <ContentTemplate>
                    <div class="row">
                        <div class="col-sm-10 col-sm-offset-1">
                            <div class="panel panel-default" style="height: 280px;">
                                <div class="panel-body">


                                    <%--Líder del Proyecto --%>
                                    <div class="form-group col-sm-5">
                                        <div class="row">
                                            <asp:Label ID="lblLider" runat="server" Text="Líder*:" CssClass="col-sm-5 col-sm-offset-1 control-label"></asp:Label>
                                        </div>
                                    </div>

                                    <div class="form-group col-sm-5">
                                        <div class="row">
                                            <div class="col-sm-9 col-sm-pull-8">
                                                <asp:DropDownList runat="server" ID="comboLider" CssClass="form-control">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>


                                    <div class="form-group col-sm-5">
                                        <div class="row">

                                            <%-- Miembros no asignados a un proyecto --%>
                                            <asp:Label ID="lblRH" runat="server" Text="Miembros Disponibles" CssClass="col-sm-12 col-sm-offset-1 control-label"></asp:Label>
                                            <div class="col-sm-12 col-sm-offset-1">
                                                <asp:ListBox runat="server" ID="listMiembrosDisponibles" CssClass="form-control" Style="height: 170px"></asp:ListBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group col-sm-1">
                                        <div class="row">
                                            <div class="">
                                                <br>
                                                <br>
                                                <%-- Botón para agregar miembros a un proyecto --%>
                                                <asp:LinkButton runat="server" ID="lnkAgregarMiembros" Style="height: 100px" CssClass="col-sm-offset-11" OnClick="btnAgregarMiembro">
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
                                                <asp:LinkButton runat="server" ID="lnkQuitarMiembros" Style="height: 100px" CssClass="col-sm-offset-11" OnClick="btnEliminarMiembro">
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
                </ContentTemplate>
            </asp:UpdatePanel>
            <%--Cierra el div de datos de proyecto en general--%>
        </div>

        <div class="col-lg-11">
            <%-- Botones para aceptar y cancelar --%>
            <div id="btnsBD" style="float: right">
                <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" CssClass="btn btn-primary" OnClick="btnAceptar_Click" />
                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-primary" OnClick="btnCancelar_Click" />
            </div>
        </div>
        <br>
        <br>

        <%-- Grid para mostrar los proyectos --%>
        <div class="form-group">
            <div class="row">
                <div id="consultar">
                    <asp:Label ID="lblProyectos" runat="server" Font-Bold="True" Text="Lista de proyectos" CssClass="col-sm-3 col-sm-offset-1 control-label"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-6 col-sm-offset-1">
                    <div id="scroll" style="height: 183px; width: 700px; overflow: auto;">
                        <asp:GridView ID="gridProyecto" runat="server" Style="width: 680px; text-align: center" CssClass="dataGridTable" Font-Size="14px" AutoGenerateColumns="true" OnRowCommand="gridProyectos_RowCommand" HeaderStyle-BackColor="#444444" HeaderStyle-ForeColor="White" AlternatingRowStyle-BackColor="#dddddd">
                            <Columns>
                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkConsulta" CommandName="seleccionarProyecto" CommandArgument='<%#Eval("ID Proyecto") %>'> Consultar </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <!-- Con esto se permite crear los mensajes de aviso en el formato lindo-->
    <div class="modal fade" id="myModal" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <asp:UpdatePanel ID="upModal" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title">
                                <asp:Label ID="lblModalTitle" runat="server" Text=""></asp:Label></h4>
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
                    <h3 class="modal-title" id="modalConfirma"><i class="fa fa-exclamation-triangle text-danger fa-2x"><font color="#Red">Aviso</font></i></h3>
                </div>
                <div class="modal-body">
                    Está seguro que desea eliminar este proyecto de prueba?            
                </div>
                <div class="modal-footer">
                    <asp:Button ID="botonAceptarEliminar" class="btn btn-info" Style="border: #7BC143; background: #0094ff" Text="Aceptar" OnClick="btnAceptar_Eliminar" runat="server" />
                    <button type="button" id="botonVolver" style="border: #0094ff; background: #0094ff" class="btn btn-danger" data-dismiss="modal">Cancelar</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

