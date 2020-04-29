using System;
using System.Web;
using System.Configuration;
using System.IO;

namespace NUT.WEB
{
    public class ArchivosVersionados : IHttpHandler
    {

        #region IHttpHandler Members

        public void ProcessRequest(HttpContext context)
        {
            string textoVersion = ConfigurationManager.AppSettings["version"];
            string pathRelativo = context.Request.AppRelativeCurrentExecutionFilePath;
            string extension = Path.GetExtension(pathRelativo);
            if (string.IsNullOrEmpty(textoVersion) || (extension != ".js" && extension != ".css"))
            {
                context.RemapHandler(null);
                return;
            }

            //No permitir llamadas a javascript/css sin que estén versionadas correctamente
            //if (pathRelativo.IndexOf(textoVersion + ".js") == -1 && pathRelativo.IndexOf(textoVersion + ".css") == -1)
            //{
            //    context.Response.StatusCode = 404;
            //    context.Response.End();
            //    return;
            //}

            context.Response.Clear();
            if (pathRelativo.IndexOf(textoVersion) >= 0 && !string.IsNullOrEmpty(context.Request.Headers["If-Modified-Since"]))
            {
                context.Response.StatusCode = 304;
                context.Response.StatusDescription = "Not Modified";
                context.Response.End();
                return;
            }
            pathRelativo = pathRelativo.Replace(textoVersion, "");
            if (extension == ".js")
            {
                context.Response.ContentType = "text/javascript";
            }
            else //css
            {
                context.Response.ContentType = "text/css";
            }

            DateTime ahora = DateTime.Now;
            context.Response.Clear();
            context.Response.Charset = "";
            context.Response.Cache.SetCacheability(HttpCacheability.Private);
            context.Response.Cache.SetLastModified(ahora);
            context.Response.Cache.SetExpires(ahora.AddYears(10));
            context.Response.WriteFile(context.Server.MapPath(pathRelativo));
            context.Response.End();
        }
        public bool IsReusable
        {
            get { return false; }
        }

        #endregion

    }
}
