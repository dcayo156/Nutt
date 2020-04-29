using NUT.LIB.GEN.LO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace NUT.WEB
{
    public partial class ingreso : System.Web.UI.Page
    {
        #region "Variables"
        #endregion

        #region "Manejo de eventos"

        protected void Page_PreInit(object sender, EventArgs e)
        {
            ViewState["IgnorarVerificacionSesion"] = true;
            ViewState["IgnorarVerificacionAccesoMenu"] = true;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            CargaInicial();
        }
        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            Ingresar();
        }

        #endregion

        #region "Métodos"

        private void CargaInicial()
        {
            HtmlLink css = new HtmlLink();
            css.Attributes["rel"] = "stylesheet";
            css.Attributes["type"] = "text/css";
            css.Href = ResolveUrl(string.Format("~/css/estilos.min{0}.css", ConfigurationManager.AppSettings["version"]));
            Page.Header.Controls.AddAt(2, css);
            lnkFavicon.Href = ResolveUrl("~/css/img/web.ico");
            lnkFavicon2.Href = ResolveUrl("~/css/img/web.ico");

            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("xpectro.ajax.pathPagina='{0}';", ResolveUrl("~/ingreso.aspx")));
            sb.Append("foco=true;");
            ClientScript.RegisterStartupScript(GetType(), "cargaInicial", sb.ToString(), true);
        }
        private void Ingresar()
        {
            string cuenta = txbCuenta.Value.Trim();
            string contrasena = txbContrasena.Value.Trim();

            DataTable tUsuario = GUsuarios.AutenticarUsuario(cuenta, contrasena).Tables[0];
            if (tUsuario.Rows.Count == 0)
            {
                lblMensaje.Style["display"] = "";
                lblMensajeContenido.InnerText = "La cuenta de usuario y/o contraseña no son válidos o no corresponden a un usuario habilitado.";
                return;
            }

            DataRow rUsuario = tUsuario.Rows[0];
            int idUsuario = rUsuario.Field<int>("Id");
            DataSet dsMenu = GUsuarios.TraerMenuUsuario(idUsuario);
            string nombrePersona = rUsuario["Nombre"].ToString() + " " + rUsuario["ApellidoPaterno"].ToString() + " " + rUsuario["ApellidoMaterno"].ToString();
            XP.AUT.Autenticacion.Autenticar(new Utils.UsuarioAutenticado(idUsuario, cuenta, nombrePersona, dsMenu));

            //AUTENTICACIÓN FORMULARIOS CLÁSICA
            FormsAuthentication.RedirectFromLoginPage(rUsuario["Cuenta"].ToString(), false);
        }
        protected void NotFound404(string statusDescription)
        {
            Context.Response.Clear();
            Context.Response.StatusCode = 404;
            Context.Response.StatusDescription = statusDescription;
            Context.Response.End();
        }

        #endregion

        #region "Ajax"


        #endregion

    }
}