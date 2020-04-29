<%@ Page Title="" Language="C#" MasterPageFile="~/Plantilla.Master" AutoEventWireup="true" CodeBehind="cambiocontrasena.aspx.cs" Inherits="SGC.WEB.cambiocontrasena" %>

<%@ MasterType VirtualPath="~/Plantilla.Master" %>
<asp:Content ID="cCss" ContentPlaceHolderID="cCss" runat="server">
</asp:Content>
<asp:Content ID="cLey" ContentPlaceHolderID="cLey" runat="server">
    Para cambiar su contraseña, introduzca su contraseña anterior, su nueva contraseña (dos veces) y presione la opción <strong><em>Cambiar contraseña</em></strong>:
</asp:Content>
<asp:Content ID="c" ContentPlaceHolderID="c" runat="server">
    <div class="toolbar">
        <a class="cmd cambiarcontrasena" data-ico="ui-icon-disk">Cambiar contraseña</a>
    </div>
    <div id="msgError"></div>
    <div id="msgExito" style="margin-top: 5px">
    </div>
    <div class="panel margen0" style="width: 100%; max-width: 400px">
        <table class="frm" style="width: 100%">
            <tbody>
                <tr>
                    <td class="oblig oculto400" style="width: 130px">
                    Contraseña actual
                    <td>
                        <span class="eti oblig visible400">Contraseña actual</span><br class="visible400" />
                        <input type="password" id="txbAnterior" maxlength="20" style="width: 97%" data-eti="contraseña actual" data-gen="F" />
                    </td>
                </tr>
                <tr>
                    <td class="oblig oculto400">
                    Nueva contraseña
                    <td>
                        <span class="eti oblig visible400">Nueva contraseña</span><br class="visible400" />
                        <input type="password" id="txbNueva" maxlength="20" style="width: 97%" data-eti="nueva contraseña" data-gen="F" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="oblig oculto400">
                    Repita su nueva contraseña
                    <td>
                        <span class="eti oblig visible400">Repita su nueva contraseña</span><br class="visible400" />
                        <input type="password" id="txbNueva2" maxlength="20" style="width: 97%" data-eti="nueva contraseña por segunda vez" data-gen="F" runat="server" />
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</asp:Content>
<asp:Content ID="cScr" ContentPlaceHolderID="cScr" runat="server">
    <script type="text/javascript" src='<%=this.ResolveUrl("~/scripts/ref/cambiocontrasena.aspx"  + this.Application["version"].ToString() + ".js") %>'></script>
</asp:Content>
