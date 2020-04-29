using System;
using System.Collections.Generic;
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
    public partial class Plantilla : Utils.PlantillaBase
    {
        protected string clasesBody = "";
        #region Propiedades


        protected string m_titulo = "SIN TÍTULO";
        public string Titulo
        {
            set { m_titulo = value; }
        }

        #endregion

        #region "Manejo de eventos"

        protected void Page_Init(object sender, EventArgs e)
        {
            HtmlLink css = new HtmlLink();
            css.Attributes["rel"] = "stylesheet";
            css.Attributes["type"] = "text/css";
            css.Href = ResolveUrl(string.Format("~/css/estilos.min{0}.css", Application["version"].ToString()));
            Page.Header.Controls.AddAt(2, css);

            lnkFavicon.Href = ResolveUrl("~/css/img/web.ico");
            lnkFavicon2.Href = ResolveUrl("~/css/img/web.ico");

            if (usr.Id > 0)
            {
                CargarMenu();
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetNoStore();
            Response.Cache.AppendCacheExtension("no-cache");

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("xpectro.global.pathRaiz='{0}';", ResolveUrl("~/"));
            sb.AppendFormat("xpectro.ajax.pathPagina='{0}';", ResolveUrl(((System.Web.Routing.PageRouteHandler)Request.RequestContext.RouteData.RouteHandler).VirtualPath));
            sb.AppendFormat("xpectro.global.tamanoPagina={0};", Application["DistribucionInterfaz.TamanoPaginacion"].ToString());
            sb.AppendFormat("xpectro.global.limiteTamanoAdjuntos={0};", Application["DistribucionInterfaz.LimiteTamanoAdjuntos"].ToString());
            sb.AppendFormat("xpectro.global.limiteResultadosBusqueda={0};", Application["DistribucionInterfaz.LimiteResultadosBusqueda"].ToString());
            sb.AppendFormat("xpectro.global.IdUsuario={0};", usr.Id);

            Page.ClientScript.RegisterStartupScript(this.GetType(), "inicioMaster", sb.ToString(), true);

            Page.Title = Application["General.TituloPaginas"].ToString();
        }
        protected void lbtCerrarSesion_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Redirect("~/ingreso", true);
        }

        #endregion

        #region "Métodos"


        private DataTable tMenu;
        private void CargarMenu()
        {
            tMenu = usr.Menu.Tables[0];

            HtmlGenericControl liLogo = new HtmlGenericControl("li");
            mnu.Controls.Add(liLogo);
            liLogo.Attributes["class"] = "logo";
            HtmlAnchor ancLogo = new HtmlAnchor();
            liLogo.Controls.Add(ancLogo);
            ancLogo.HRef = ResolveUrl("~/inicio");
            ancLogo.Attributes["class"] = "logo";
            HtmlImage img = new HtmlImage();
            ancLogo.Controls.Add(img);
            img.Alt = "";
            img.Src = ResolveUrl("~/css/img/logo.png");

            CargarMenu(mnu.Controls, new DataView(tMenu, "CodigoPadre IS NULL", "Orden", DataViewRowState.CurrentRows), 1);

            HtmlGenericControl li = new HtmlGenericControl("li");
            mnu.Controls.Add(li);
            li.Style["float"] = "right";
            HtmlAnchor anc = new HtmlAnchor();
            li.Controls.Add(anc);
            anc.InnerText = usr.Nombre;

            HtmlGenericControl ul = new HtmlGenericControl("ul");
            ul.Attributes["class"] = "usuario";
            li.Controls.Add(ul);

            HtmlGenericControl liCerrar = new HtmlGenericControl("li");
            ul.Controls.Add(liCerrar);
            LinkButton lbtCerrar = new LinkButton();
            liCerrar.Controls.Add(lbtCerrar);
            lbtCerrar.Text = "Cerrar sesión";
            lbtCerrar.Click += lbtCerrarSesion_Click;

            HtmlGenericControl liContrasena = new HtmlGenericControl("li");
            ul.Controls.Add(liContrasena);
            HtmlAnchor ancContrasena = new HtmlAnchor();
            liContrasena.Controls.Add(ancContrasena);
            ancContrasena.InnerText = "Cambiar contraseña";
            ancContrasena.HRef = ResolveUrl("~/cambio_contrasena");
        }
        private void CargarMenu(ControlCollection items, DataView vItems, int nivel)
        {
            foreach (DataRowView rvItem in vItems)
            {
                HtmlGenericControl li = new HtmlGenericControl("li");
                items.Add(li);
                HtmlAnchor anc = new HtmlAnchor();
                li.Controls.Add(anc);
                anc.InnerText = rvItem["Etiqueta"].ToString();
                if (Convert.ToInt32(rvItem["Tipo"]) == 2)
                {
                    string rutas = rvItem["Ruta"].ToString();
                    string url = null;
                    if (rutas.StartsWith("|"))
                    {
                        url = "~/" + rutas.Substring(1).Split("|".ToCharArray())[0];
                    }
                    else
                    {
                        url = Convert.ToString("~/") + rutas;
                    }
                    anc.HRef = url;
                    anc.Attributes["class"] = "seleccionable";
                }
                DataView vHijos = new DataView(tMenu, "CodigoPadre='" + rvItem["Codigo"].ToString() + "'", "Orden", DataViewRowState.CurrentRows);

                if (vHijos.Count > 0)
                {
                    HtmlGenericControl ul = new HtmlGenericControl("ul");
                    li.Controls.Add(ul);
                    CargarMenu(ul.Controls, vHijos, nivel + 1);
                }
            }
        }

        #endregion

    }
}