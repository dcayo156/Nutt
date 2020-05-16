using NUT.LIB.GEN.LO;
using NUT.LIB.NEG.LO;
using NUT.LIB.NEG.SF;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NUT.WEB.par
{
    public partial class condiciones_no_patologicas : Utils.PaginaBase
    {
        const string CODIGOPANTALLA = "PAR_NOPATS";
        public override string CODIGO_PANTALLA
        {
            get { return CODIGOPANTALLA; }
        }
        #region Manejo de eventos
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        #endregion
        #region Métodos

        #region Métodos

        void CargaInicial()
        {
            //ddlEstado.DataSource = Utils.ProxyCache.TraerEstadosUsuario();
            //ddlEstado.DataBind();
        }

        #endregion

        #region Ajax

        [WebMethod()]
        public static object Buscar(FCondicionesNoPatologicas filter, int pagina)
        {
            try
            {
                if (string.IsNullOrEmpty(usr.Codigo))
                {
                    throw new Exception("errorSesion");
                }

                if (!GUsuarios.VerificarAccesoCodigoPantallaUsuario(usr.Id, CODIGOPANTALLA))
                {
                    throw new Exception("Acceso denegado.");
                }

                filter.Nombre = Utils.Varios.AgregarComodinesBusqueda(filter.Nombre);
                DataTable tCondicionNoPatologicas = GCondicionesNoPatologicas.BuscarCondicionNoPatologicas(filter).Tables[0];

                int cantidadRegistros = tCondicionNoPatologicas.Rows.Count;
                int cantidadPaginas = Utils.Varios.PaginarDataTable(tCondicionNoPatologicas, pagina);
                return new
                {
                    CondicionNoPatologicas = from rCondicionNoPatologicas in tCondicionNoPatologicas.AsEnumerable()
                                 select new
                                 {
                                     Id = rCondicionNoPatologicas.Field<int>("Id"),
                                     Nom = rCondicionNoPatologicas["Nombre"].ToString(),                                     
                                     Est = rCondicionNoPatologicas["DescripcionEstado"].ToString()
                                 },
                    CantidadRegistros = cantidadRegistros,
                    CantidadPaginas = cantidadPaginas
                };
            }

            catch (Exception ex)
            {
                Utils.Inst.log.Error("Error no controlado de la aplicación", ex);
                throw;
            }
        }
        #endregion

        #endregion
    }
}