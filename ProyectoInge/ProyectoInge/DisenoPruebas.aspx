<%@ Page Title="Diseño de Pruebas" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DisenoPruebas.aspx.cs" Inherits="ProyectoInge.DisenoPruebas" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
     <nav class="navbar navbar-default navbar-static-top">
    <ul class="navbar-default nav navbar-nav">
         <li><a runat="server"  href="~/RecursosHumanos.aspx">Recursos Humanos</a></li>
            <li ><a runat="server" href="~/ProyectodePruebas.aspx" >Proyecto</a></li>
            <li ><a runat="server" >Diseño de pruebas</a></li>
            <li ><a runat="server" style ="background-color:ActiveCaption" href="~/CasoDePrueba.aspx" >Casos de prueba</a></li>
            <li ><a runat="server" >Ejecución de pruebas</a></li>
           <%-- <li ><a runat="server" onserverclick="cerrarSesion" href="~/Login.aspx" >Cerrar sesión</a></li>--%>
             <li ><a runat="server"  href="~/Login.aspx" >Cerrar sesión</a></li>
    </ul>
</nav>
    
    <div class="row">
        <div class="col-lg-12">
            <%--Titulo de la pantalla de Caso de Pruebas --%>
            <h2 class="estilo">Módulo de Diseño de Pruebas</h2>
        </div>

         <div class="col-lg-11">
            <%--Div de botones para el IMEC--%>
            <div id="btnsControl" style="float: right">
                <asp:Button ID="btnInsertar" runat="server" Text="Insertar" CssClass="btn btn-primary" />
                <asp:Button ID="btnModificar" runat="server" Text="Modificar" CssClass="btn btn-primary"  />
                <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-primary" />
            </div>
        </div>

     </div> <%--Div que cierra el row principal--%>
        
        <br>
        <br>
        <br>
        <br>
        <div id="Datos"> <%-- Div que almacena todos los div internos para los datos del diseño de pruebas --%>
             
            <%--titulo datos del diseño--%>
            <asp:Label runat="server" ID="lblDatosDiseno" Font-Bold="True" Text="Datos del diseño de pruebas" CssClass="col-sm-5 col-sm-offset-1 control-label"></asp:Label>
            
            <%-- Proyecto asociado al diseño --%>
            <asp:Label ID="lblProyecto" runat="server" Text="Proyecto:" CssClass="col-sm-1 col-sm-offset-1 control-label"></asp:Label>
            <asp:DropDownList runat="server" ID="comboProyecto" AutoPostBack="True"  CssClass="form-control" onselectedindexchanged="proyectoSeleccionado">
            </asp:DropDownList>


            <%--Requerimientos del proyecto --%>
            <asp:Label ID="lblReqProyecto" runat="server" Text="Requerimientos del proyecto" CssClass="col-sm-12 col-sm-offset-1 control-label"></asp:Label>
            <asp:ListBox runat="server" ID="listReqProyecto" CssClass="form-control" Style="height: 170px"></asp:ListBox>


            <%-- Botón para agregar requerimientos a un diseño --%>
            <asp:LinkButton runat="server" ID="lnkAgregarReq" Style="height: 100px" CssClass="col-sm-offset-11">
            <span aria-hidden="true" class="glyphicon glyphicon-hand-right blueColor"></span>
            </asp:LinkButton>
                                    
             <%-- Botón para quitar requerimientos de un diseño --%>
             <asp:LinkButton runat="server" ID="lnkQuitarReq" Style="height: 100px" CssClass="col-sm-offset-11">
             <span aria-hidden="true" class="glyphicon glyphicon-hand-left blueColor"></span>
             </asp:LinkButton>
                                 
             <%-- Miembros asignados a un proyecto --%>
             <asp:Label ID="lblReqAsignados" runat="server" Text="Requerimientos del diseño de pruebas" CssClass="col-sm-12 col-sm-offset-2 control-label"></asp:Label>
             <asp:ListBox runat="server" ID="listReqAgregados" CssClass="form-control" Style="height: 170px"></asp:ListBox>
                
             <%-- Propósito del diseño --%>
            <asp:Label ID="lblProposito" runat="server" Text="Propósito:" CssClass="col-sm-1 col-sm-offset-1 control-label"></asp:Label>
            <asp:TextBox runat="server" ID="txtProposito" CssClass="form-control" MaxLength="50"></asp:TextBox>

            <%-- Nivel de la prueba --%>
            <asp:Label ID="lblNivel" runat="server" Text="Nivel de prueba:" CssClass="col-sm-1 col-sm-offset-1 control-label"></asp:Label>
            <asp:DropDownList runat="server" ID="comboNivel" AutoPostBack="True" CssClass="form-control">
            </asp:DropDownList>

            <%-- Tipo de la prueba --%>
            <asp:Label ID="lblTipo" runat="server" Text="Tipo de prueba:" CssClass="col-sm-1 col-sm-offset-1 control-label"></asp:Label>
            <asp:DropDownList runat="server" ID="comboTipo" AutoPostBack="True" CssClass="form-control">
            </asp:DropDownList>

            <%-- Técnica de la prueba --%>
            <asp:Label ID="lblTecnica" runat="server" Text="Técnica de prueba:" CssClass="col-sm-1 col-sm-offset-1 control-label"></asp:Label>
            <asp:DropDownList runat="server" ID="comboTecnica" AutoPostBack="True" CssClass="form-control">
            </asp:DropDownList>

            <%-- ambiente del diseño --%>
            <asp:Label ID="lblAmbiente" runat="server" Text="Ambiente de prueba:" CssClass="col-sm-1 col-sm-offset-1 control-label"></asp:Label>
            <asp:TextBox runat="server" ID="txtAmbiente" CssClass="form-control" MaxLength="100"></asp:TextBox>

            <%-- procedimiento del diseño --%>
            <asp:Label ID="lblProcedimiento" runat="server" Text="Procedimiento utilizado:" CssClass="col-sm-1 col-sm-offset-1 control-label"></asp:Label>
            <asp:TextBox runat="server" ID="txtProcedimiento" CssClass="form-control" MaxLength="100"></asp:TextBox>
            
             <%-- criterios del diseño --%>
            <asp:Label ID="lblCriterios" runat="server" Text="Criterios de aceptación:" CssClass="col-sm-1 col-sm-offset-1 control-label"></asp:Label>
            <asp:TextBox runat="server" ID="txtCriterios" CssClass="form-control" MaxLength="100"></asp:TextBox>

            <%-- Responsable del diseño --%>
            <asp:Label ID="lblResponsable" runat="server" Text="Responsable:" CssClass="col-sm-1 col-sm-offset-1 control-label"></asp:Label>
            <asp:DropDownList runat="server" ID="comboResponsable" AutoPostBack="True" CssClass="form-control">
            </asp:DropDownList>

            <%-- Fecha del diseño --%>
            <asp:UpdatePanel ID="UpdatePanelCalendario" runat="server" UpdateMode="conditional" ChildrenAsTriggers="false">
                <ContentTemplate>
                    <div class="form-group">
                        <div class="row">
                              <asp:Label ID="lblFecha" runat="server" Text="Fecha:" CssClass="col-sm-2 col-sm-offset-1 control-label"></asp:Label>
                              <div class="col-sm-6 col-sm-offset-2 ">
                              <asp:TextBox runat="server" ID="txtCalendar" CssClass=" form-control"></asp:TextBox>
                              </div>
                                    
                            <div class="">

                             <asp:LinkButton runat="server" ID="lnkCalendario"  CssClas=".glyphicon.glyphicon-calendar"  OnClick="lnkCalendario_Click" >
                             <span aria-hidden="true" class="glyphicon glyphicon-calendar blueColor" ></span>
                             </asp:LinkButton>
                            </div>
                
                            <br>

                            <div class ="row col-sm-6 col-sm-offset-5">
                            <asp:Calendar runat="server" ID="calendarFecha" Visible="false"  OnVisibleMonthChanged="cambioDeMes"  OnSelectionChanged="calendarioSeleccionado" ></asp:Calendar>        
                            </div>
                       </div>
                        </div>
                 </ContentTemplate>
            </asp:UpdatePanel>

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
                <asp:Label ID="lblDisenos" runat="server" Font-Bold="True" Text="Lista de diseños de prueba" CssClass="col-sm-3 col-sm-offset-1 control-label"></asp:Label>
              </div> 
                </div>   
                    <div class ="row">
                <div class="col-sm-6 col-sm-offset-1"> 
                    <div id ="scroll" style ="height: 183px; width:700px; overflow:auto;" >
                        <asp:GridView ID="gridDisenos" runat="server" CssClass ="dataGridTable" style ="width: 680px; text-align:center" font-size = "14px" AutoGenerateColumns="true"  OnRowCommand ="gridDisenos_RowCommand" HeaderStyle-BackColor="#444444" HeaderStyle-ForeColor="White" AlternatingRowStyle-BackColor="#dddddd">                                           
                            <Columns>
                                <asp:TemplateField HeaderText=""><ItemTemplate>
                                    <asp:LinkButton runat="server" ID="lnkConsulta" CommandName="seleccionarDiseno"    > Consultar </asp:LinkButton>
                                </ItemTemplate>
                                </asp:TemplateField>                     
                            </Columns> 
                        </asp:GridView>
                        </div>
                    </div>
                    </div>
         </div>

</asp:Content>