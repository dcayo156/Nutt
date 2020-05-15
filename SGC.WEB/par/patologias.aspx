<%@ Page Title="" Language="C#" MasterPageFile="~/Plantilla.Master" AutoEventWireup="true" CodeBehind="patologias.aspx.cs" Inherits="NUT.WEB.par.patologias" %>
<%@ MasterType VirtualPath="~/Plantilla.Master" %>
<asp:Content ID="cCss" ContentPlaceHolderID="cCss" runat="server">
</asp:Content>
<asp:Content ID="cLey" ContentPlaceHolderID="cLey" runat="server">
    Para buscar alguna patología registrada, visualizar y/o modificar su información, especifique el nombre de la patología que desea y luego <em><strong>ENTER</strong></em> para buscar:
</asp:Content>
<asp:Content ID="c" ContentPlaceHolderID="c" runat="server">
      <div id="pnlContenedor">
        <div id="pnlFiltro" class="filtro">
            <div class="simple" data-busqueda="Buscar">
                <input type="text" id="txbNombreCompleto2" style="width: 297px" placeholder="Busque el nombre de la patología" class="focodefecto" />
                <a id="btnBuscar2" class="cmd" data-ico="ui-icon-search" style="float: none">&nbsp;</a>
                <a class="cmd" data-ico="ui-icon-plusthick" href="<%=ResolveUrl("~/patologias/nueva")%>">Nueva patología</a>
                <a class="cmd busquedaavanzada" data-ico="ui-icon-triangle-1-s">Búsqueda avanzada</a>
                <br style="clear: both" />
            </div>
            <div class="avanzado panel margen0">
                <h3>Búsqueda avanzada</h3>
                <table class="frm" data-btnbusqueda="btnBuscar">
                    <tbody>
                        <tr>
                            <td class="oculto500" style="width:115px">Nombre completo</td>
                            <td colspan="2">
                                <span class="eti visible500">Nombre completo</span><br class="visible500" />
                                <input type="text" id="txbNombreCompleto" style="width: 96.5%" placeholder="Busque por el nombre" maxlength="100" class="focodefecto" />
                            </td>
                        </tr>                        
                        <tr>
                            <td>Es habito fisiologico</td>
                            <td>
                                <input type="text" id="txbEsHabitoFisiologico" style="width: 150px" placeholder="Busque por habito fisiologíco" maxlength="50" />
                            <%--    <label class="radio-inline">
                                    <input type="radio" id="rbEsHabitoFisiologico" name="S" value="S">Si</label>
                                <label class="radio-inline">
                                    <input type="radio" id="rbEsHabitoFisiologico2" name="N" value="N">NO</label>--%>
                            </td>
                        </tr>
                        <tr>
                            <td>Estado</td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlEstado" DataTextField="Descripcion" DataValueField="Codigo" AppendDataBoundItems="true" Width="162px">
                                    <asp:ListItem Text="<TODOS>" Value="-1"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <a id="btnBuscar" class="cmd" data-ico="ui-icon-search">Buscar</a>
                                <a class="cmd busquedasimple" data-ico="ui-icon-triangle-1-n">Cerrar búsqueda avanzada</a>
                                <a class="cmd nuevo" data-ico="ui-icon-plusthick" href="<%=ResolveUrl("~/patologias/nuevo")%>">Nuevo patología</a>
                                <span style="clear: both"></span>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
        <div id="pnlResultado" style="clear: both">
        </div>
    </div>
</asp:Content>
<asp:Content ID="cScr" ContentPlaceHolderID="cScr" runat="server">
    <script type="text/javascript" src="<%=ResolveUrl("~/scripts/ref/par_patologias.aspx" + System.Configuration.ConfigurationManager.AppSettings["version"] + ".js")%>"></script>
</asp:Content>
