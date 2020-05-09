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
    public partial class patologias : Utils.PaginaBase
    {
        const string CODIGOPANTALLA = "PAR_PATS";
        public override string CODIGO_PANTALLA
        {
            get { return CODIGOPANTALLA; }
        }
        #region Manejo de eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            Master.Titulo = "BUSCAR Y EDITAR PATOLOGÍAS";
            CargaInicial();
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
        public static object Buscar(FPatologias filter, int pagina)
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
                DataTable tPatologias = GPatologias.BuscarPatologias(filter).Tables[0];

                int cantidadRegistros = tPatologias.Rows.Count;
                int cantidadPaginas = Utils.Varios.PaginarDataTable(tPatologias, pagina);
                return new
                {
                    Patologias = from rPatologia in tPatologias.AsEnumerable()
                               select new
                               {
                                   Id = rPatologia.Field<int>("Id"),
                                   Nom = rPatologia["Nombre"].ToString(),          
                                   HabFis = Utils.Varios.FormatearLogico(rPatologia, "EsHabitoFisiologico"),
                                   Est = rPatologia["DescripcionEstado"].ToString()
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