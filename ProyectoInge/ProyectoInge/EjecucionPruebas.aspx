<%@ Page Title="Ejecución de Pruebas" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EjecucionPruebas.aspx.cs" Inherits="ProyectoInge.EjecucionPruebas" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-group">
        <div class="row">
            <div class="col-sm-9 ">
                <nav class="navbar navbar-default navbar-static-top">
                    <ul class="navbar-default nav navbar-nav">
                        <li><a runat="server" href="~/RecursosHumanos.aspx">Recursos Humanos</a></li>
                        <li><a runat="server" href="~/ProyectodePruebas.aspx">Proyecto</a></li>
                        <li><a runat="server" href="~/DisenoPruebas.aspx">Diseño de pruebas</a></li>
                        <li><a runat="server" style="background-color: ActiveCaption" href="~/EjecucionPruebas.aspx">Ejecución de pruebas</a></li>
                        <li><a runat="server" onserverclick="cerrarSesion" href="~/Login.aspx">Cerrar sesión</a></li>
                    </ul>
                </nav>
            </div>
            <br />
            <asp:Label ID="lblLogueado" runat="server" Text="" Font-Bold="True" CssClass="col-sm-2 control-label"></asp:Label>
        </div>
    </div>

    <div class="row">
        <%--Row principal --%>

        <div class="col-lg-12">
            <%--Titulo de la pantalla de Ejecución de Pruebas --%>
            <h2 class="estilo">Módulo de Ejecución de Pruebas</h2>
        </div>


        <div class="col-lg-11">
            <%--Div de botones para el IMEC--%>
            <div id="btnsControl" style="float: right">
                <asp:Button ID="btnInsertar" runat="server" Text="Insertar" CssClass="btn btn-primary" />
                <asp:Button ID="btnModificar" runat="server" Text="Modificar" CssClass="btn btn-primary" />
                <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-primary" />
            </div>
        </div>


        <div id="DatosEjecucionDePrueba">
            <br>
            <br>
            <br>
            <br>
            <br>


            <div class="col-sm-10 col-sm-offset-1">
                <div class="panel panel-default" style="height: 80px">
                    <div class="panel-body">
                        <div class="form-group">
                            <%--Proyecto --%>

                            <div class="row col-sm-6">
                                <asp:Label ID="lblProyecto" runat="server" Text="Proyecto*:" CssClass="col-sm-3 control-label"></asp:Label>
                                <div class="col-sm-7 col-sm-offset-1 ">
                                    <asp:DropDownList runat="server" ID="comboProyecto" AutoPostBack="True" CssClass="form-control col-sm-offset-2" EnableViewState="true">
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="row col-sm-6">
                                <asp:Label ID="lblDiseñoPruebas" runat="server" Text="Diseño de Prueba*:" CssClass="col-sm-5  control-label"></asp:Label>
                                <div class="col-sm-7 ">
                                    <asp:DropDownList runat="server" ID="comboDiseño" AutoPostBack="True" CssClass="form-control col-sm-pull-1" EnableViewState="true">
                                    </asp:DropDownList>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>

            <br>
            <br>
            <div class="row col-sm-10 col-sm-offset-1">
                <asp:Label runat="server" ID="lblDatosDiseño" Font-Bold="True" Text="Datos del diseño" CssClass="control-label"></asp:Label>
            </div>

            <div class="col-sm-10 col-sm-offset-1">
                <div class="panel panel-default" style="height: 280px">
                    <div class="panel-body">
                        <div class="form-group">
                            <%--Proposito del diseño --%>
                            <div class="row col-sm-6">
                                <asp:Label ID="lblDiseño" runat="server" Text="Propósito:" CssClass="col-sm-3 control-label"></asp:Label>
                                <div class="col-sm-7 col-sm-offset-1 ">
                                    <asp:TextBox runat="server" ID="txtPropositoDiseño" Enabled="false" CssClass="form-control col-sm-offset-2"></asp:TextBox>
                                </div>
                            </div>
                              <%--Nivel de prueba--%>
                            <div class="row col-sm-6">
                                <asp:Label ID="lblPrueba" runat="server" Text="Nivel de prueba:" CssClass="col-sm-4  control-label"></asp:Label>

                                <div class="col-sm-7 ">
                                    <asp:TextBox runat="server" ID="txtPrueba" Enabled="false" CssClass="form-control col-sm-offset-2"></asp:TextBox>
                                </div>
                            </div>

                        </div>
                        <br>
                        <br>
                        <div class="form-group">
                          
                            <div class="row col-sm-6">
                                <%--Técnica de prueba--%>
                                <asp:Label ID="lblTecnica" runat="server" Text="Técnica de prueba:" CssClass="col-sm-5  control-label"></asp:Label>

                                <div class="col-sm-7">
                                    <asp:TextBox runat="server" ID="txtTecnicaPrueba" Enabled="false" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                              <%--Procedimiento--%>

                            <div class="row col-sm-6">
                                <asp:Label ID="lblProposito" runat="server" Text="Procedimiento:" CssClass="col-sm-5   control-label"></asp:Label>

                                <div class="col-sm-7 col-sm-pull-1">
                                    <asp:TextBox runat="server" ID="txtProcedimiento" Enabled="false" MultiLine="true" TextMode="MultiLine" Height="100px" MaxLength="70" CssClass="form-control col-sm-offset-2"></asp:TextBox>
                                </div>
                            </div>

                        </div>
                        <br>
                        <br>

                        <div class="form-group">
                          

                            <div class="row col-sm-6">
                                <%--Requerimientos--%>
                                <asp:Label ID="lblRequerimiento" runat="server" Text="Requerimientos:" CssClass="col-sm-4 control-label"></asp:Label>

                                <div class="col-sm-7 col-sm-offset-1">
                                    <asp:ListBox runat="server" ID="listRequerimientoDisponibles" Enabled="false" Style="height: 100px" CssClass="form-control"></asp:ListBox>
                                </div>

                            </div>

                        </div>

                    </div>
                    <%--Cierra el panel body--%>
                </div>
            </div>


            <div class="col-sm-10 col-sm-offset-1">
                <div class="panel panel-default" style="height: 280px">
                    <div class="panel-body">
                          <div class="form-group">





                          </div>
                    </div>
                </div>
            </div>



        </div>
        <%--Div que cierra los datos de casos de prueba--%>
    </div>
    <%--Div que cierra el row principal --%>
</asp:Content>
