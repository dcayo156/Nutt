<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ingreso.aspx.cs" Inherits="NUT.WEB.ingreso" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv="Content-Type" content="text/html;charset=UTF-8" />
    <meta name="viewport" content="width=device-width" />
    <title>Ingreso - MULTU NUT</title>
    <link rel="shortcut icon" runat="server" id="lnkFavicon" />
    <link rel="icon" type="image/vnd.microsoft.icon" runat="server" id="lnkFavicon2" />
</head>
<body>
    <form id="f1" runat="server">
        <div class="ingreso_externo">
            <div style="" class="ingreso">
                <img alt="MULTU NUT" src="<%=ResolveUrl("~/css/img/logo_ingreso.jpg")%>" class="logo" />
                <span class="leyenda">Identifícate para ingresar:</span>
                <div class="dato">
                    <input type="text" id="txbCuenta" runat="server" placeholder="Cuenta de usuario"/>
                </div>
                <div class="dato">
                    <input type="password" id="txbContrasena" runat="server" placeholder="Contraseña"/><br />
                </div>
                <br />
                <asp:LinkButton runat="server" ID="btnIngresar" Text="INGRESAR" OnClick="btnIngresar_Click"></asp:LinkButton>
                <br style="clear: both" />
                <asp:Button runat="server" ID="btnIngresar2" Text="INGRESAR" OnClick="btnIngresar_Click" />
                <div class="ui-state-highlight ui-corner-all" style="margin-top: 20px; padding: 1em .7em; display: none" runat="server" id="lblMensaje">
                    <span class="ui-icon ui-icon-info" style="float: left; margin-right: .3em;"></span>
                    <span runat="server" id="lblMensajeContenido" class="contenido"></span>
                </div>

            </div>
        </div>
        <script type="text/javascript" src='<%=this.ResolveUrl("~/scripts/ref/comunes.min" + this.Application["version"].ToString() + ".js")%>'></script>
        <script type="text/javascript" src='<%=this.ResolveUrl("~/scripts/ref/ingreso.aspx" + this.Application["version"].ToString() + ".js") %>'></script>
        <input type="text" style="display: none" />
    </form>
</body>
</html>
