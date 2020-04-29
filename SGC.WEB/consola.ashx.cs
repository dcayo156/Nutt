using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NUT.WEB
{

    public class consola : Utils.HandlerBase
    {

        [Flags]
        enum Opciones
        {
            EnviarResumenDiarioEjecutivo = 1,

            //PruebaEnvioCorreo = 1048576
        }

        public override void ProcessRequest(HttpContext context)
        {
            validarSesion = false;
            base.ProcessRequest(context);

            if (string.IsNullOrEmpty(context.Request.QueryString["opciones"]))
            {
                BadRequest400(context, "Parámetros incorrectos");
                return;
            }
            int opciones;
            if (!int.TryParse(context.Request.QueryString["opciones"], out opciones))
            {
                BadRequest400(context, "Parámetros incorrectos");
                return;
            }
            

            
            context.ClearError();
            context.Response.Charset = "";
            context.Response.StatusCode = 200;
            context.Response.StatusDescription = "Ejecución correcta de procesos";
            context.Response.Write(HttpUtility.HtmlEncode("Ejecución correcta de procesos"));
            context.Response.End();



        }

    }
}