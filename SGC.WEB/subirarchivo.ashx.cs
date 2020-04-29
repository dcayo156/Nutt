using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace NUT.WEB
{

    public class subirarchivo : Utils.HandlerBase
    {

        public override void ProcessRequest(HttpContext context)
        {
            validarSesion = false;
            base.ProcessRequest(context);

            if (context.Request.Files.Count == 0)
            {
                return;
            }

            try
            {
                HttpPostedFile archivo = context.Request.Files[0];
                string extension = Path.GetExtension(archivo.FileName);
                string nombre = Guid.NewGuid().ToString();

                string estado = context.Request.QueryString["estado"];
                if (estado == null)
                {
                    estado = "";
                }

                archivo.SaveAs(Path.Combine(context.Server.MapPath("~/temp"), nombre + extension));
                context.Response.Clear();

                if (context.Request.Browser.Browser == "IE" && context.Request.Browser.MajorVersion <= 6)
                {
                    context.Response.ContentType = "text/html";
                }

                context.Response.Write(JObject.FromObject(new
                {
                    success = true,
                    archivo = nombre + extension,
                    status = 200
                }
                ).ToString(Newtonsoft.Json.Formatting.None));
            }
            catch (Exception ex)
            {
                context.Response.Write(JObject.FromObject(new
                {
                    success = false,
                    error = ex.Message,
                    status = 400
                }
               ).ToString(Newtonsoft.Json.Formatting.None));
            }
        }


    }
}