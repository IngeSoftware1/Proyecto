<%@ Page Title="Caso de Prueba" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CasoDePrueba.aspx.cs" Inherits="ProyectoInge.CasoDePrueba" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-group">
  <div class="row">
    <div class="col-sm-9 ">     
    <nav class="navbar navbar-default navbar-static-top">
        <ul class="navbar-default nav navbar-nav">
            <li><a runat="server" href="~/RecursosHumanos.aspx">Recursos Humanos</a></li>
            <li><a runat="server" href="~/ProyectodePruebas.aspx">Proyecto</a></li>
            <li><a runat="server" href="~/DisenoPruebas.aspx">Diseño de pruebas</a></li>
            <li><a runat="server">Ejecución de pruebas</a></li>
            <li><a runat="server" onserverclick="cerrarSesion" href="~/Login.aspx">Cerrar sesión</a></li>
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
        <h2 class="estilo">Módulo de Casos de Prueba</h2>
    </div>

    <div class="col-lg-11">
        <%--Div de botones para el IMEC--%>
        <div id="btnsControl" style="float: right">
            <asp:Button ID="btnInsertar" runat="server" Text="Insertar" CssClass="btn btn-primary" OnClick="btnInsertar_Click" />
            <asp:Button ID="btnModificar" runat="server" Text="Modificar" CssClass="btn btn-primary" />
            <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-primary" OnClick="btnEliminar_Click" />
        </div>
    </div>

    <div id="DatosCasosDePrueba">
        <div class="row col-sm-10 col-sm-offset-1">
            <asp:Label runat="server" ID="lblDatosDiseño" Font-Bold="True" Text="Datos del diseño" CssClass="control-label"></asp:Label>
        </div>


        <div class="col-sm-10 col-sm-offset-1">
            <div class="panel panel-default" style="height: 280px">
                <div class="panel-body">
                    <div class="form-group">       <%--Nombre del proyecto--%>              
                        <div class="row col-sm-6">
                            <asp:Label ID="lblProyecto" runat="server" Text="Proyecto:" CssClass="col-sm-3  control-label"></asp:Label>
                            <div class="col-sm-7 col-sm-offset-1">
                                <asp:TextBox runat="server" ID="txtNombreProyecto" Enabled="false" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        
                        <div class="row col-sm-6">
                            <asp:Label ID="lblDiseño" runat="server" Text="Diseño:" CssClass="col-sm-3 control-label"></asp:Label>
                            <div class="col-sm-7 col-sm-offset-2 ">
                                <asp:TextBox runat="server" ID="txtNombreDiseño" Enabled="false" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <br>
                    <br>
                    <div class="form-group">           <%--Nivel de prueba--%>             
                        <div class="row col-sm-6">
                            <asp:Label ID="lblPrueba" runat="server" Text="Nivel de prueba:" CssClass="col-sm-4  control-label"></asp:Label>

                            <div class="col-sm-7 ">
                                <asp:TextBox runat="server" ID="txtPrueba" Enabled="false" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row col-sm-6">
                            <asp:Label ID="lblTecnica" runat="server" Text="Técnica de prueba:" CssClass="col-sm-5  control-label"></asp:Label>

                            <div class="col-sm-7">
                                <asp:TextBox runat="server" ID="txtTecnicaPrueba" Enabled="false" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <br>
                    <br>
                 
                    <div class="form-group">  <%--Tipo de prueba--%>

                        <div class="row col-sm-6">
                            <asp:Label ID="lblProposito" runat="server" Text="Procedimiento:" CssClass="col-sm-5  control-label"></asp:Label>

                            <div class="col-sm-7 col-sm-pull-1">
                                <asp:TextBox runat="server" ID="txtProcedimiento" Enabled="false" MultiLine="true" TextMode="MultiLine" Height="100px" MaxLength="70" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                      
                              
                        <div class="row col-sm-6">
                            <asp:Label ID="lblRequerimiento" runat="server" Text="Requerimientos:" CssClass="col-sm-4 control-label"></asp:Label>                          
                            
                           <div class="col-sm-7 col-sm-offset-1">
                            <asp:ListBox runat="server" ID="listRequerimientoDisponibles" Enabled="false" style="height:100px" CssClass="form-control"></asp:ListBox>
                        </div>
                        
                        </div>
                           
                   </div>   

                </div>   <%--Cierra el panel body--%>           
            </div>
        </div>

        <div class="row col-sm-10 col-sm-offset-1">
            <asp:Label runat="server" ID="lblDatosCasoPrueba" Font-Bold="True" Text="Datos del caso de prueba" CssClass="control-label"></asp:Label>
        </div>
          <div class="col-sm-10 col-sm-offset-1">
            <div class="panel panel-default" style="height: 300px">
                <div class="panel-body">


                   

                   
                    <%--  --%>
                      <div class="form-group col-sm-6">  
                        <div class="row ">
                            <asp:Label ID="lblIdentificador" runat="server" Text="Código*:" CssClass="col-sm-4 control-label"></asp:Label>
                             <div class="col-sm-7"  >
                             <asp:TextBox runat="server" ID="txtIdentificador" CssClass="form-control" MaxLength="20"></asp:TextBox>
                        </div>
                                  </div>
                          </div>

         
                       <div class="form-group col-sm-6">  
                        <div class="row ">
                            <asp:Label ID="lblPropositoCasoPrueba" runat="server" Text="Propósito*:" CssClass="col-sm-4 control-label"></asp:Label>
                             <div class="col-sm-7 ">
                             <asp:TextBox runat="server" ID="txtProposito" CssClass="form-control" MultiLine="true" TextMode="MultiLine" Height="77px" MaxLength="20"></asp:TextBox>
                        </div>
                                  </div>
                          </div>
          
                       <div class="form-group col-sm-6">  
                        <div class="row ">
                            <asp:Label ID="lblEntradaDatos" runat="server" Text="Entrada de datos:" CssClass="col-sm-4 control-label"></asp:Label>
                             <div class="col-sm-7 ">
                             <asp:TextBox runat="server" ID="txtEntradaDatos" CssClass="form-control" MultiLine="true" TextMode="MultiLine" Height="77px" MaxLength="20"></asp:TextBox>
                        </div>
                                  </div>
                          </div>


                       <div class="form-group col-sm-6 "> 
                        <div class="row ">
                            <asp:Label ID="lblResultadoEsperado" runat="server" Text="Resultado esperado:" CssClass="col-sm-4 control-label"></asp:Label>
                             <div class="col-sm-7 ">
                             <asp:TextBox runat="server" ID="txtResultadoEsperado" CssClass="form-control" MultiLine="true" TextMode="MultiLine" Height="77px" MaxLength="20"></asp:TextBox>
                        </div>
                                  </div>
                          </div>

                    <div class="form-group col-sm-6 "> 
                        <div class="row ">
                            <asp:Label ID="lblFlujoCentral" runat="server" Text="Flujo central:" CssClass="col-sm-4 control-label"></asp:Label>
                            <div class ="col-sm-7 ">
                             <asp:TextBox runat="server" ID="txtFlujoCentral" CssClass="  form-control" MultiLine="true" TextMode="MultiLine" Width ="293%" Height="77px" MaxLength="20"></asp:TextBox>
                        </div>
                                  </div>
                        </div>

                </div>   <%--Cierra el panel body--%>           
            </div>
        </div>
    </div> <%--Div que cierra los datos de casos de prueba--%>
     
      <div class="col-lg-11">
            <%-- Botones para aceptar y cancelar --%>
            <div id="btnsBD" style="float: right">
                <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" CssClass="btn btn-primary"  OnClick="btnAceptar_Click"/>
                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-primary" OnClick="btnCancelar_Click"  />
            </div>
        </div>
        <br>
        <br>



        <%-- Grid para mostrar los proyectos --%>
        <div class="form-group">
            <div class="row col-sm-10 col-sm-offset-1">
                <div id="consultar">
                    <asp:Label ID="lblCasosPrueba" runat="server" Font-Bold="True" Text="Lista de casos de prueba" CssClass="control-label"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-6 col-sm-offset-1">
                    <div id="scroll" style="height: 183px; width: 700px; overflow: auto;">
                        <asp:GridView ID="gridCasosPrueba" runat="server" Style="width: 680px; text-align:center" CssClass ="dataGridTable" font-size = "14px" AutoGenerateColumns="true"  HeaderStyle-BackColor="#444444" HeaderStyle-ForeColor="White" AlternatingRowStyle-BackColor="#dddddd">
                            <Columns>
                                <asp:TemplateField HeaderText="">
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

           <!-- Con esto se permite crear los mensajes de aviso en el formato lindo-->
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
           </div>  <%--Div que cierra el row principal --%>

    <%--Modal Confirmar--%>
    <div class="modal fade" id="modalConfirmar" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h3 class="modal-title" id="modalConfirma"><i class="fa fa-exclamation-triangle text-danger fa-2x"><font color = "#Red">Aviso</font></i></h3>
                </div>
                <div class="modal-body">
                     Está seguro que desea eliminar este caso de prueba?            
                </div>
                <div class="modal-footer">
                    <asp:Button ID="botonAceptarEliminar" class="btn btn-info" style="border:#7BC143 ; background:#0094ff" Text="Aceptar" OnClick="btnAceptar_Eliminar" runat="server"/> 
                    <button type="button" id="botonVolver" style="border:#0094ff; background:#0094ff" class="btn btn-danger" data-dismiss="modal">Cancelar</button>
                </div>
            </div>
        </div>
    </div>



</asp:Content>
