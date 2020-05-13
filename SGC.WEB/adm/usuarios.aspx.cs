using NUT.LIB.GEN.LO;
using NUT.LIB.GEN.SF;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NUT.WEB.adm
{
    public partial class usuarios : Utils.PaginaBase
    {

        const string CODIGOPANTALLA = "ADM_USUS";
        public override string CODIGO_PANTALLA
        {
            get { return CODIGOPANTALLA; }
        }

        #region Declaraciones

        #endregion

        #region Manejo de eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            Master.Titulo = "BUSCAR Y EDITAR USUARIOS";
            CargaInicial();
        }

        #endregion

        #region Métodos

        #region Métodos

        void CargaInicial()
        {
            ddlEstado.DataSource = Utils.ProxyCache.TraerEstadosUsuario();
            ddlEstado.DataBind();
        }

        #endregion

        #region Ajax

        [WebMethod()]
        public static object Buscar(FilterUsuarios filter, int pagina)
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

                filter.Cuenta = Utils.Varios.AgregarComodinesBusqueda(filter.Cuenta);
                DataTable tUsuarios = GUsuarios.BuscarUsuarios(filter).Tables[0];

                int cantidadRegistros = tUsuarios.Rows.Count;
                int cantidadPaginas = Utils.Varios.PaginarDataTable(tUsuarios, pagina);
                return new
                {
                    Usuarios = from rUsuario in tUsuarios.AsEnumerable()
                               select new
                               {
                                   Id = rUsuario.Field<int>("Id"),
                                   Nom = rUsuario["NombreCompletoUsuario"].ToString(),
                                   Cue = rUsuario["Cuenta"].ToString(),
                                   Est = rUsuario["DescripcionEstado"].ToString()
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