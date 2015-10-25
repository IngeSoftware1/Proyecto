<%@ Page Title="Caso de Prueba" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CasoDePrueba.aspx.cs" Inherits="ProyectoInge.CasoDePrueba" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <nav class="navbar navbar-default navbar-static-top">
        <ul class="navbar-default nav navbar-nav">
            <li><a runat="server" href="~/RecursosHumanos.aspx">Recursos Humanos</a></li>
            <li><a runat="server" href="~/ProyectodePruebas.aspx">Proyecto</a></li>
            <li><a runat="server" href="~/DisenoPruebas.aspx">Diseño de pruebas</a></li>
            <li><a runat="server" style="background-color: ActiveCaption" href="~/CasoDePrueba.aspx">Casos de prueba</a></li>
            <li><a runat="server">Ejecución de pruebas</a></li>
            <li><a runat="server" onserverclick="cerrarSesion" href="~/Login.aspx">Cerrar sesión</a></li>
        </ul>
    </nav>

      <div class="row">
    <div class="col-lg-12">
        <%--Titulo de la pantalla de Caso de Pruebas --%>
        <h2 class="estilo">Módulo de Casos de Prueba</h2>
    </div>

    <div class="col-lg-11">
        <%--Div de botones para el IMEC--%>
        <div id="btnsControl" style="float: right">
            <asp:Button ID="btnInsertar" runat="server" Text="Insertar" CssClass="btn btn-primary" />
            <asp:Button ID="btnModificar" runat="server" Text="Modificar" CssClass="btn btn-primary" />
            <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-primary" />
        </div>
    </div>

    <div id="DatosCasosDePrueba">
        <div class="row col-sm-10 col-sm-offset-1">
            <asp:Label runat="server" ID="lblDatosDiseño" Font-Bold="True" Text="Datos del diseño" CssClass="control-label"></asp:Label>
        </div>


        <div class="col-sm-10 col-sm-offset-1">
            <div class="panel panel-default" style="height: 350px">
                <div class="panel-body">
                    <div class="form-group">       <%--Nombre del proyecto--%>              
                        <div class="row col-sm-6">
                            <asp:Label ID="lblProyecto" runat="server" Text="Proyecto:" CssClass="col-sm-3  control-label"></asp:Label>
                            <div class="col-sm-7 col-sm-offset-1 ">
                                <asp:DropDownList runat="server" ID="comboProyecto" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <br>
                    <br>

                    <div class="form-group">  <%--Nombre del diseño--%>                    
                        <div class="row col-sm-6">
                            <asp:Label ID="lblDiseño" runat="server" Text="Diseño:" CssClass="col-sm-3 control-label"></asp:Label>
                            <div class="col-sm-7 col-sm-offset-1">
                                <asp:DropDownList runat="server" ID="comboDiseño" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>

                    <br>
                    <br>

                    <div class="form-group">           <%--Nivel de prueba--%>             
                        <div class="row col-sm-6">
                            <asp:Label ID="lblPrueba" runat="server" Text="Nivel de prueba:" CssClass="col-sm-4  control-label"></asp:Label>

                            <div class="col-sm-7 ">
                                <asp:TextBox runat="server" ID="txtPrueba" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row col-sm-6">
                            <asp:Label ID="lblTecnica" runat="server" Text="Técnica de prueba:" CssClass="col-sm-5  control-label"></asp:Label>

                            <div class="col-sm-7">
                                <asp:TextBox runat="server" ID="txtTecnicaPrueba" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <br>
                    <br>
                 
                    <div class="form-group">  <%--Tipo de prueba--%>
                        <div class="row col-sm-6">
                            <asp:Label ID="lblTipoPrueba" runat="server" Text="Tipo de prueba:" CssClass="col-sm-4 control-label"></asp:Label>
                            <div class="col-sm-7 ">
                                <asp:TextBox runat="server" ID="txtTipoPrueba" CssClass="form-control" MaxLength="20"></asp:TextBox>
                            </div>
                        </div>

                        <div class="row col-sm-6">
                            <asp:Label ID="lblProposito" runat="server" Text="Propósito:" CssClass="col-sm-5  control-label"></asp:Label>

                            <div class="col-sm-7">
                                <asp:TextBox runat="server" ID="txtProposito" MultiLine="true" TextMode="MultiLine" Height="77px" MaxLength="70" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>   <%--Cierra el panel body--%>           
            </div>
        </div>

          <div class="col-sm-10 col-sm-offset-1">
            <div class="panel panel-default" style="height: 250px">
                <div class="panel-body">


                       <div class="form-group col-sm-5">  <%--Nombre del Requerimiento --%>                    
                        <div class="row">
                            <asp:Label ID="lblRequerimiento" runat="server" Text="Requerimiento:" CssClass="col-sm-4 control-label"></asp:Label>                          
                                <asp:DropDownList runat="server" style="position:absolute;top:0%; left:38%; width:61%"  ID="DropDownList1" CssClass=" form-control">
                                </asp:DropDownList>
                            <asp:ListBox runat="server" ID="listRequerimientoDisponibles" style="position:absolute;top:6%; left:38%; width:61%; height:100px" CssClass="form-control"></asp:ListBox>
                        </div>
                           
                    </div>

                     <div class="form-group col-sm-1">
                                <div class="row">
                                    <div class="">
                                        
                                        <%-- Botón para agregar miembros a un proyecto --%>
                                        <asp:LinkButton runat="server" ID="lnkAgregarMiembros" Style="height: 100px" CssClass="col-sm-offset-11" >
                                        <span aria-hidden="true" class="glyphicon glyphicon-hand-right blueColor"></span>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                                <br>
                              
                                <div class="row">
                                    <div class="">
                                        <%-- Botón para quitar miembros de un proyecto --%>
                                        <asp:LinkButton runat="server" ID="lnkQuitarMiembros" Style="height: 100px" CssClass="col-sm-offset-11" >
                                        <span aria-hidden="true" class="glyphicon glyphicon-hand-left blueColor"></span>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </div>










                </div>   <%--Cierra el panel body--%>           
            </div>
        </div>
    </div> <%--Div que cierra los datos de casos de prueba--%>
      </div>  <%--Div que cierra el row principal --%>
</asp:Content>
