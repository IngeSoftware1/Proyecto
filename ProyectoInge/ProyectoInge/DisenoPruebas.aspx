<%@ Page Title="Diseño de Pruebas" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DisenoPruebas.aspx.cs" Inherits="ProyectoInge.DisenoPruebas" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
  <div class="form-group">
      <div class="row">
        <div class="col-sm-9 ">
        <nav class="navbar navbar-default navbar-static-top">
            <ul class="navbar-default nav navbar-nav">
                <li><a runat="server" href="~/RecursosHumanos.aspx">Recursos Humanos</a></li>
                <li><a runat="server" href="~/ProyectodePruebas.aspx">Proyecto</a></li>
                <li ><a runat="server" style ="background-color:ActiveCaption" href="~/DisenoPruebas.aspx" >Diseño de pruebas</a></li>
                <li><a runat="server" href="~/CasoDePrueba.aspx">Casos de prueba</a></li>
                <li><a runat="server">Ejecución de pruebas</a></li>
                <%-- <li ><a runat="server" onserverclick="cerrarSesion" href="~/Login.aspx" >Cerrar sesión</a></li>--%>
                <li><a runat="server" href="~/Login.aspx">Cerrar sesión</a></li>
            </ul>
        </nav>
       </div>
         <br/>
         <asp:Label ID="lblLogueado" runat="server" Text="" Font-Bold="True" CssClass="col-sm-2 control-label"></asp:Label>
       </div>
    </div>

    <div class="row">
        <div class="col-lg-12">
            <%--Titulo de la pantalla de Caso de Pruebas --%>
            <h2 class="estilo">Módulo de Diseño de Pruebas</h2>
        </div>

        <div class="col-lg-11">
            <%--Div de botones para el IMEC--%>
            <div id="btnsControl" style="float: right">
                <asp:Button ID="btnInsertar" runat="server" Text="Insertar" CssClass="btn btn-primary" />
                <asp:Button ID="btnModificar" runat="server" Text="Modificar" CssClass="btn btn-primary" />
                <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-primary" />
            </div>
        </div>

    </div>
    <%--Div que cierra el row principal--%>
    <br>
    <div id="Datos">
        <%-- Div que almacena todos los div internos para los datos del diseño de pruebas --%>

        <%--titulo datos del diseño--%>
        <div class="form-group">
            <div class="row">
                <asp:Label runat="server" ID="lblDatosDiseno" Font-Bold="True" Text="Datos del diseño de pruebas" CssClass="col-sm-12 col-sm-offset-1 control-label"></asp:Label>
            </div>
        </div>
        <br>


        <%-- Proyecto asociado al diseño --%>
        <div class="form-group">
            <div class="row">
                <asp:Label ID="lblProyecto" runat="server" Text="Proyecto:" CssClass="col-sm-2 col-sm-offset-1 control-label"></asp:Label>
                <div class="col-sm-3 ">
                    <asp:DropDownList runat="server" ID="comboProyecto" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="proyectoSeleccionado">
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <br>
        

        <asp:UpdatePanel ID="UpdateAsociarDesasociarRequerimientos" runat="server" UpdateMode="conditional" ChildrenAsTriggers="false">
            <ContentTemplate>
                <div class="row">
                    <div class="col-sm-10 col-sm-offset-1">
                        <div class="panel panel-default" style="height: 280px;">
                            <div class="panel-body">

                                <div class="form-group col-sm-5">
                                    <div class="row">
                                        <%--Requerimientos del proyecto --%>
                                        <asp:Label ID="lblReqProyecto" runat="server" Text="Requerimientos del proyecto" CssClass="col-sm-12 col-sm-offset-1 control-label"></asp:Label>
                                        <div class="col-sm-12 col-sm-offset-1">
                                            <asp:ListBox runat="server" ID="listReqProyecto" CssClass="form-control" Style="height: 170px"></asp:ListBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group col-sm-1">
                                    <div class="row">
                                        <div class="">
                                            <br>
                                            <br>
                                            <%-- Botón para agregar requerimientos a un diseño --%>
                                            <asp:LinkButton runat="server" ID="lnkAgregarReq" Style="height: 100px" CssClass="col-sm-offset-11">
                                        <span aria-hidden="true" class="glyphicon glyphicon-hand-right blueColor"></span>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                    <br>
                                    <br>
                                    <br>
                                    <div class="row">
                                        <div class="">
                                            <%-- Botón para quitar requerimientos de un diseño --%>
                                            <asp:LinkButton runat="server" ID="lnkQuitarReq" Style="height: 100px" CssClass="col-sm-offset-11">
                                        <span aria-hidden="true" class="glyphicon glyphicon-hand-left blueColor"></span>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>

                                <div class="form-group col-sm-5">
                                    <div class="row">
                                        <%--Requerimientos asociados al diseño --%>
                                        <asp:Label ID="lblReqAsignados" runat="server" Text="Requerimientos del diseño de pruebas" CssClass="col-sm-12 col-sm-offset-2 control-label"></asp:Label>
                                        <div class="col-sm-12 col-sm-offset-2">
                                            <asp:ListBox runat="server" ID="listReqAgregados" CssClass="form-control" Style="height: 170px"></asp:ListBox>
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
        <%-- Propósito del diseño --%>
        <div class="form-group">
            <div class="row">
                <asp:Label ID="lblProposito" runat="server" Text="Propósito:" CssClass="col-sm-2 col-sm-offset-1 control-label"></asp:Label>
                <div class="col-sm-8 ">
                    <asp:TextBox runat="server" ID="txtProposito" CssClass="form-control" MultiLine="true" TextMode="MultiLine" Height="35px" MaxLength="50"></asp:TextBox>
                </div>
            </div>
        </div>
        <br />

        <div class="form-group">
            <div class="row">
                <%-- Nivel de la prueba --%>
                <asp:Label ID="lblNivel" runat="server" Text="Nivel de prueba:" CssClass="col-sm-2 col-sm-offset-1 control-label"></asp:Label>
                <div class="col-sm-3 ">
                    <asp:DropDownList runat="server" ID="comboNivel" AutoPostBack="True" CssClass="form-control">
                    </asp:DropDownList>
                </div>

                <%-- Técnica de la prueba --%>
                <asp:Label ID="lblTecnica" runat="server" Text="Técnica de prueba:" CssClass="col-sm-2  control-label"></asp:Label>
                <div class="col-sm-3 ">
                    <asp:DropDownList runat="server" ID="comboTecnica" AutoPostBack="True" CssClass="form-control">
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <br />

        <div class="form-group">
            <div class="row">
                <%-- Tipo de la prueba --%>
                <asp:Label ID="lblTipo" runat="server" Text="Tipo de prueba:" CssClass="col-sm-2 col-sm-offset-1 control-label"></asp:Label>
                <div class="col-sm-3 ">
                    <asp:DropDownList runat="server" ID="comboTipo" AutoPostBack="True" CssClass="form-control">
                    </asp:DropDownList>
                </div>
                <%-- ambiente del diseño --%>
                <asp:Label ID="lblAmbiente" runat="server" Text="Ambiente de prueba:" CssClass="col-sm-2 control-label"></asp:Label>
                <div class="col-sm-3 ">
                    <asp:TextBox runat="server" ID="txtAmbiente" CssClass="form-control" MultiLine="true" TextMode="MultiLine" Height="77px" MaxLength="100"></asp:TextBox>
                </div>
            </div>
        </div>
        <br />
        <div class="form-group">
            <div class="row">
                <%-- procedimiento del diseño --%>
                <asp:Label ID="lblProcedimiento" runat="server" Text="Procedimiento utilizado:" CssClass="col-sm-2 col-sm-offset-1 control-label"></asp:Label>
                <div class="col-sm-8 ">
                    <asp:TextBox runat="server" ID="txtProcedimiento" CssClass="form-control" MultiLine="true" TextMode="MultiLine" Height="77px" MaxLength="100"></asp:TextBox>
                </div>
            </div>
        </div>
        <br />
        <div class="form-group">
            <div class="row">
                <%-- criterios del diseño --%>
                <asp:Label ID="lblCriterios" runat="server" Text="Criterios de aceptación:" CssClass="col-sm-2 col-sm-offset-1 control-label"></asp:Label>
                <div class="col-sm-8 ">
                    <asp:TextBox runat="server" ID="txtCriterios" CssClass="form-control" MultiLine="true" TextMode="MultiLine" Height="77px" MaxLength="100"></asp:TextBox>
                </div>
            </div>
        </div>

        <div class="form-group">
            <div class="row">
                <%-- Responsable del diseño --%>
                <asp:Label ID="lblResponsable" runat="server" Text="Responsable:" CssClass="col-sm-2 col-sm-offset-1 control-label"></asp:Label>
                <div class="col-sm-3 ">
                    <asp:DropDownList runat="server" ID="comboResponsable" AutoPostBack="True" CssClass="form-control">
                    </asp:DropDownList>
                </div>
                <%-- Fecha del diseño --%>
                <asp:UpdatePanel ID="UpdatePanelCalendario" runat="server" UpdateMode="conditional" ChildrenAsTriggers="false">
                    <ContentTemplate>
                        
                                <asp:Label ID="lblFecha" runat="server" Text="Fecha:" CssClass="col-sm-2  control-label"></asp:Label>
                                <div class="col-sm-3 ">
                                    <asp:TextBox runat="server" ID="txtCalendar" CssClass=" form-control"></asp:TextBox>
                                </div>
                               <div class="col-sm-push-12">
                                    <asp:LinkButton runat="server" ID="lnkCalendario" CssClas=".glyphicon.glyphicon-calendar" OnClick="lnkCalendario_Click">
                                     <span aria-hidden="true" class="glyphicon glyphicon-calendar blueColor" ></span>
                                    </asp:LinkButton>
                                </div>
                                <br />
                                <div class="form-group">
                                     <div class="row">  
                                        <div class="col-sm-3 col-sm-offset-8  ">
                                            <asp:Calendar runat="server" ID="calendarFecha" Visible="false"  OnVisibleMonthChanged="cambioDeMes" OnSelectionChanged="calendarioSeleccionado"></asp:Calendar>
                                        </div>
                                    </div>
                                </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <div class="col-lg-11">
        <%-- Botones para aceptar y cancelar --%>
        <div id="btnsBD" style="float: right">
            <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" CssClass="btn btn-primary" OnClick="btnAceptar_Click" />
            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-primary" OnClick="btnCancelar_Click" />
        </div>
    </div>
    <br>


    <div class="form-group">
        <div class="row">
            <div id="consultar">
                <asp:Label ID="lblDisenos" runat="server" Font-Bold="True" Text="Lista de diseños de prueba" CssClass="col-sm-3 col-sm-offset-1 control-label"></asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-6 col-sm-offset-1">
                <div id="scroll" style="height: 183px; width: 700px; overflow: auto;">
                    <asp:GridView ID="gridDisenos" runat="server" CssClass="dataGridTable" Style="width: 680px; text-align: center" Font-Size="14px" AutoGenerateColumns="true" OnRowCommand="gridDisenos_RowCommand" HeaderStyle-BackColor="#444444" HeaderStyle-ForeColor="White" AlternatingRowStyle-BackColor="#dddddd">
                        <Columns>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                     <asp:LinkButton runat="server" ID="lnkConsulta" CommandName="seleccionarDiseno" CommandArgument='<%#Eval("ID Diseño") %>'> Consultar </asp:LinkButton>                
                                      
                                </ItemTemplate>

                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                                    
                                     <asp:LinkButton runat="server" ID="linkConsultaCaso" CommandName="seleccionarCaso" CommandArgument='<%#Eval("ID Diseño") %>'> Casos de prueba </asp:LinkButton> 
                                </ItemTemplate>

                            </asp:TemplateField>
                           

                        </Columns>
                    </asp:GridView>
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

</asp:Content>
