<%@ Page Title="" Language="C#" MasterPageFile="~/Plantilla.Master" AutoEventWireup="true" CodeBehind="usuario.aspx.cs" Inherits="NUT.WEB.adm.usuario" %>

<%@ MasterType VirtualPath="~/Plantilla.Master" %>

<asp:Content ID="cCss" ContentPlaceHolderID="cCss" runat="server">
    <style type="text/css">
        #pnlInformacionGeneral {
            width: 100%;
            max-width: 450px;
        }

        #txbNombre{
            width: 96%;
        }

        #pnlPermisos {
            width: 100%;
            max-width: 650px;
        }

        @media only screen and (min-width:1100px) {
            #pnlPermisos {
                width: calc(100% - 550px);
            }

            #tlb2 {
                display: none;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="cLey" ContentPlaceHolderID="cLey" runat="server">
    <%if (this.nuevo)
      { %>
    Especifique la información para registrar el nuevo usuario y seleccione la opción <strong><em>Guardar</em></strong>.
    <%}
      else
      { %>
    Modifique la información del usuario y seleccione la opción <strong><em>Guardar</em></strong>.
    <%} %>
</asp:Content>
<asp:Content ID="c" ContentPlaceHolderID="c" runat="server">
    <div class="toolbar" id="tlb1" data-posicion="arriba">
        <a class="cmd guardar" data-ico="ui-icon-disk">Guardar</a>
        <a class="cmd volver" data-ico="ui-icon-arrowreturnthick-1-w" href="<%=ResolveUrl("~/usuarios")%>">Volver a la lista</a>
    </div>
    <div id="msgError1" style="margin-top: 5px">
    </div>
    <div id="msgExito" style="margin-top: 5px">
    </div>
    <div class="panel" id="pnlInformacionGeneral">
        <h3>Información general</h3>
        <table class="frm" style="width: 100%">
            <tbody>
                <tr>
                    <td class="oblig oculto500">Nombre</td>
                    <td colspan="2">
                        <span class="eti oblig visible500_ib">Nombre</span><br class="visible500" />
                        <input type="text" runat="server" id="txbNombre" maxlength="50" data-eti="nombre" />
                    </td>
                </tr>
                <tr>
                    <td class="oblig">Apellido paterno</td>
                    <td>
                        <input type="text" runat="server" id="txbApellidoPaterno" maxlength="30" style="width: 174px" data-eti="apellido paterno" />
                    </td>
                </tr>
                <tr>
                    <td class="oblig">Apellido materno</td>
                    <td>
                        <input type="text" runat="server" id="txbApellidoMaterno" maxlength="30" style="width: 174px" data-eti="apellido materno" />
                    </td>
                </tr>
                <tr>
                    <td class="oblig" style="width:130px">Cuenta de ingreso</td>
                    <td>
                        <input type="text" runat="server" id="txbCuenta" maxlength="20" style="width: 174px" data-eti="cuenta de ingreso" data-gen="F" />
                    </td>
                </tr>
                <tr runat="server" id="trContrasena">
                    <td class="oblig">Contraseña</td>
                    <td>
                        <input type="password" id="txbContrasena" maxlength="20" style="width: 100%" data-eti="contraseña" data-gen="F" data-anchocontenedor="174px" />
                    </td>
                </tr>
                <tr runat="server" id="trContrasena2">
                    <td class="oblig">Repetir contraseña</td>
                    <td>
                        <input type="password" id="txbContrasena2" maxlength="20" style="width: 100%" data-eti="contraseña por segunda vez" data-gen="F" data-anchocontenedor="174px" />
                    </td>
                </tr>
                <tr>
                    <td>Correo</td>
                    <td>
                        <input type="text" runat="server" id="txbCorreo" maxlength="20" data-eti="correo" data-tip="cor" data-nul="S" style="width: 174px"/>
                    </td>
                </tr>
                <tr>
                    <td class="oblig">Estado</td>
                    <td>
                        <select id="ddlEstado" runat="server" datatextfield="Descripcion" datavaluefield="Codigo" data-eti="estado" style="width: 148px">
                        </select>
                    </td>
                </tr>
                <tr runat="server" id="trRegistro">
                    <td>Registro</td>
                    <td colspan="2">
                        <span id="lblRegistro" runat="server"></span>
                    </td>
                </tr>
                <tr runat="server" id="trUltimaModificacion">
                    <td>Última modificación</td>
                    <td colspan="2">
                        <span id="lblUltimaModificacion" runat="server"></span>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <div class="panel" id="pnlPermisos">
        <h3>Permisos</h3>
        <asp:ListView runat="server" ID="lvwGrupos" OnItemDataBound="lvwGrupos_ItemDataBound">
            <LayoutTemplate>
                <table class="grd" style="width: 100%" data-fil1="grupo" data-fil2="funcion" data-cmd="seleccionar" id="tblPermisos">
                    <thead>
                        <tr>
                            <td style="width: 1px">&nbsp;</td>
                            <td>Permiso</td>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:PlaceHolder runat="server" ID="itemPlaceHolder"></asp:PlaceHolder>
                    </tbody>
                </table>
            </LayoutTemplate>
            <ItemTemplate>
                <tr class="grupo nohover">
                    <td colspan="2">
                        <%#Eval("NombreGrupoFuncion") %>
                    </td>
                </tr>
                <asp:ListView runat="server" ID="lvwFunciones">
                    <ItemTemplate>
                        <tr class="funcion">
                            <td>
                                <input type="checkbox" value="<%#Eval("CodigoFuncion")%>" <%#Eval("Seleccionado").ToString()=="S"?"checked='checked'":""%> class="nocmd seleccionar" />
                            </td>
                            <td>
                                <%#Eval("NombreFuncion") %>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:ListView>
            </ItemTemplate>
        </asp:ListView>
    </div>
    <br style="clear: both" />
    <br />
    <div id="msgError2" style="margin-bottom: 5px">
    </div>
    <div class="toolbar" id="tlb2" data-posicion="abajo">
        <a class="cmd guardar" data-ico="ui-icon-disk">Guardar</a>
        <a class="cmd volver" data-ico="ui-icon-arrowreturnthick-1-w" href="<%=ResolveUrl("~/adm/usuarios")%>">Volver a la lista</a>
    </div>
</asp:Content>
<asp:Content ID="cScr" ContentPlaceHolderID="cScr" runat="server">
    <script type="text/javascript" src="<%=ResolveUrl("~/scripts/ref/adm_usuario.aspx" + System.Configuration.ConfigurationManager.AppSettings["version"] + ".js")%>"></script>
</asp:Content>
