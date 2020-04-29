using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NUT.WEB.Utils
{

    public class HandlerBase : IHttpHandler, System.Web.SessionState.IReadOnlySessionState
    {

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

        protected bool validarSesion = true;

        public virtual void ProcessRequest(HttpContext context)
        {
            if (validarSesion && usr.Codigo.Length == 0)
            {
                context.Response.Clear();
                context.Response.End();
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        protected void BadRequest400(HttpContext context, string statusDescription)
        {
            context.Response.Clear();
            context.Response.StatusCode = 400;
            context.Response.StatusDescription = statusDescription;
        }
        protected void Forbidden403(HttpContext context, string statusDescription)
        {
            context.Response.Clear();
            context.Response.StatusCode = 403;
            context.Response.StatusDescription = statusDescription;
        }
        protected void NotFound404(HttpContext context, string statusDescription)
        {
            context.Response.Clear();
            context.Response.StatusCode = 404;
            context.Response.StatusDescription = statusDescription;
        }
    }

}