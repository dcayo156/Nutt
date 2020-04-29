using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using NUT.LIB.GEN.LO;

namespace NUT.WEB
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(Server.MapPath("~/Inst.config")));
            Aspose.Cells.License lic = new Aspose.Cells.License();
            lic.SetLicense("Aspose.Cells.lic");
            try
            {
                Application["version"] = ConfigurationManager.AppSettings["version"];

                Dictionary<string, string> dPars = GParametros.TraerParametros(
                    "DistribucionInterfaz.TamanoPaginacion",
                    "DistribucionInterfaz.LimiteTamanoAdjuntos",
                    "DistribucionInterfaz.LimiteResultadosBusqueda",
                    "General.DerechosReservados",
                    "General.DerechosReservadosAnio",
                    "General.NombreProveedor",
                    "General.TituloPaginas",
                    "General.TimeZoneId");

                foreach (string clave in dPars.Keys)
                {
                    Application[clave] = dPars[clave];
                }

                RegisterRoutes(RouteTable.Routes);
            }
            catch (Exception ex)
            {
                Utils.Inst.Error("Error inesperado al iniciar la aplicación.", ex);
            }
        }
        void RegisterRoutes(RouteCollection routes)
        {
            DataTable tFormularios = GPantallas.TraerFormulariosActivos().Tables[0];
            foreach (DataRow rForm in tFormularios.Rows)
            {
                string codigo = rForm["Codigo"].ToString();
                if (!rForm.IsNull("Ruta"))
                {
                    string auxRutas = rForm["Ruta"].ToString();
                    if (auxRutas.StartsWith("|"))
                    {
                        auxRutas = auxRutas.Substring(1, auxRutas.Length - 1);
                    }
                    if (auxRutas.EndsWith("|"))
                    {
                        auxRutas = auxRutas.Substring(0, auxRutas.Length - 1);
                    }
                    string[] rutas = auxRutas.Split("|".ToCharArray());
                    string ubicacion = "~/" + rForm["Ubicacion"].ToString();
                    int i = 1;
                    foreach (string ruta in rutas)
                    {
                        routes.MapPageRoute(codigo + (i++).ToString(), ruta, ubicacion);
                    }
                }
            }
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }


        protected void Application_PostAcquireRequestState(object sender, EventArgs e)
        {
            
        }
        
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            if (ConfigurationManager.AppSettings["SIMULARRETRASO"] == "S")
            {
                System.Threading.Thread.Sleep(1000);
            }
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Utils.Inst.Error("Error no controlado en la aplicación.", Server.GetLastError());
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}