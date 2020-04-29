using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NUT.WEB
{

    public class bajartemporal : NUT.WEB.Utils.HandlerBase
    {

        public override void ProcessRequest(HttpContext context)
        {
            base.ProcessRequest(context);

            string archivoTemporal = context.Request.Form["archivotemporal"];
            string archivoReal = context.Request.Form["archivoreal"];

            byte[] datosAdjunto = System.IO.File.ReadAllBytes(System.IO.Path.Combine(context.Server.MapPath("~/temp/"), archivoTemporal));

            context.ClearError();
            context.Response.AddHeader("content-disposition", string.Format("attachment;filename=\"{0}\"", archivoReal));
            context.Response.Charset = "";
            context.Response.ContentType = "application/octet-stream";
            context.Response.BinaryWrite(datosAdjunto);
            context.Response.End();
        }


    }
}