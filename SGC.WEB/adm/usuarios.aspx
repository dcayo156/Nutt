<%@ Page Title="" Language="C#" MasterPageFile="~/Plantilla.Master" AutoEventWireup="true" CodeBehind="usuarios.aspx.cs" Inherits="NUT.WEB.adm.usuarios" %>

<%@ MasterType VirtualPath="~/Plantilla.Master" %>
<asp:Content ID="cCss" ContentPlaceHolderID="cCss" runat="server">
    <style type="text/css">
        #pnlContenedor {
            max-width: 750px;
        }
        @media only screen and (min-width:750px) {
            div.filtro div.simple .cmd {
                float: right;
            }
        }

        @media only screen and (max-width:500px) {
            div.filtro div.avanzado .cmd {
                margin-top: 2px;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="cLey" ContentPlaceHolderID="cLey" runat="server">
    Para buscar algún usuario registrado, visualizar y/o modificar su información y permisos, especifique el nombre del usuario que desea y luego <em><strong>ENTER</strong></em> para buscar:
</asp:Content>
<asp:Content ID="c" ContentPlaceHolderID="c" runat="server">
    <div id="pnlContenedor">
        <div id="pnlFiltro" class="filtro">
            <div class="simple" data-busqueda="Buscar">
                <input type="text" id="txbNombreCompleto2" style="width: 297px" placeholder="Busque el nombre del usuario" class="focodefecto" />
                <a id="btnBuscar2" class="cmd" data-ico="ui-icon-search" style="float: none">&nbsp;</a>
                <a class="cmd" data-ico="ui-icon-plusthick" href="<%=ResolveUrl("~/usuarios/nuevo")%>">Nuevo usuario</a>
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
                            <td>Cuenta</td>
                            <td>
                                <input type="text" id="txbCuenta" style="width: 150px" placeholder="Busque por la cuenta" maxlength="50" />
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
                                <a class="cmd nuevo" data-ico="ui-icon-plusthick" href="<%=ResolveUrl("~/usuarios/nuevo")%>">Nuevo usuario</a>
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
    <script type="text/x-jsrender" id="tmpResultado">
        {{if CantidadRegistros > 0}}
            <table class="grd" data-cmd="editar" data-eti="usuario" data-etiplural="usuarios" data-col="6" data-fortab="N" style="width: 100%">
                <thead>
                    <tr>
                        <td class="cen" style="width: 1px">&nbsp;</td>
                        <td>Nombre</td>
                        <td class="oculto800">Cuenta</td>
                        <td class="cen">Estado</td>
                    </tr>
                </thead>
                <tbody>
                    {{for Usuarios}}
                        <tr data-idusuario="{{>Id}}">
                            <td style="white-space: nowrap">
                                <a title="Editar" href="usuarios/{{>Id}}" data-ico="ui-icon-pencil" class="cmd editar">&nbsp;</a>
                            </td>
                            <td class="nombre">{{>Nom}}</td>
                            <td class="oculto800">{{>Cue}}</td>
                            <td class="cen">{{>Est}}</td>
                        </tr>
                    {{/for}}
                </tbody>
            </table>
        {{else}}
                <div class="grd_vacio">No se encontraron usuarios que coincidan con el filtro especificado.</div>
        {{/if}}
    </script>
</asp:Content>
<asp:Content ID="cScr" ContentPlaceHolderID="cScr" runat="server">
    <script type="text/javascript" src="<%=ResolveUrl("~/scripts/ref/adm_usuarios.aspx" + System.Configuration.ConfigurationManager.AppSettings["version"] + ".js")%>"></script>
</asp:Content>
