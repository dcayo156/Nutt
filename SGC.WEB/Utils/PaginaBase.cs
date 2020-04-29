using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Configuration;
using NUT.LIB.GEN.LO;

namespace NUT.WEB.Utils
{
    public abstract class PaginaBase : System.Web.UI.Page
    {

        public PaginaBase()
            : base()
        {
        }

        public static Utils.UsuarioAutenticado usr
        {
            get
            {
                XP.AUT.UsuarioAutenticado aux = XP.AUT.Autenticacion.Usuario;
                if (aux.Codigo.Length == 0)
                {
                    return new Utils.UsuarioAutenticado(-1, "", "", null);
                }
                return (Utils.UsuarioAutenticado)XP.AUT.Autenticacion.Usuario;
            }
        }
        public static HttpApplicationState App
        {
            get
            {
                return HttpContext.Current.Application;
            }
        }

        public abstract string CODIGO_PANTALLA
        {
            get;
        }

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            if (ViewState["IgnorarVerificacionSesion"] == null && usr.Codigo.Length == 0)
            {
                Session.Abandon();
                FormsAuthentication.RedirectToLoginPage();
                return;
            }

            string rutaActual = null;
            if (ViewState["IgnorarVerificacionAccesoMenu"] == null)
            {
                rutaActual = ((System.Web.Routing.Route)(this.RouteData.Route)).Url;
                if (!GUsuarios.VerificarAccesoCodigoPantallaUsuario(usr.Id, this.CODIGO_PANTALLA))
                {
                    Response.Redirect("~/acceso_negado", true);
                    return;
                }
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            if (usr.Codigo.Length > 0 || ViewState["IgnorarVerificacionSesion"] != null)
            {
                base.OnLoad(e);
            }
        }

        protected void BadRequest400(string statusDescription)
        {
            Context.Response.Clear();
            Context.Response.StatusCode = 400;
            Context.Response.StatusDescription = statusDescription;
            Context.Response.End();
        }
        protected void Forbidden403(string statusDescription)
        {
            Context.Response.Clear();
            Context.Response.StatusCode = 403;
            Context.Response.StatusDescription = statusDescription;
            Context.Response.End();
        }
        protected void NotFound404(string statusDescription)
        {
            Context.Response.Clear();
            Context.Response.StatusCode = 404;
            Context.Response.StatusDescription = statusDescription;
            Context.Response.End();
        }

    }
}