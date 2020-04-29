using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace NUT.WEB.Utils
{
    public class ProxyCache
    {

        const int horasCache = 6;

        #region Generales

        private static void LimpiarCache(string clave)
        {
            System.Web.HttpContext.Current.Cache.Remove(clave);
        }

        public static DataTable TraerEstadosUsuario()
        {
            System.Web.Caching.Cache cache = System.Web.HttpContext.Current.Cache;
            DataTable tDatos = null;
            if ((cache["TraerEstadosUsuario"] != null))
            {
                tDatos = (DataTable)cache["TraerEstadosUsuario"];
            }
            else
            {
                tDatos = NUT.LIB.GEN.LO.GUsuarios.TraerEstadosUsuario().Tables[0];
                cache.Insert("TraerEstadosUsuario", tDatos, null, DateTime.Now.AddHours(horasCache), TimeSpan.Zero);
            }
            return tDatos;
        }

        public static DataTable TraerTiposCampamentos()
        {
            System.Web.Caching.Cache cache = System.Web.HttpContext.Current.Cache;
            DataTable tDatos = null;
            if ((cache["TraerTiposCampamentos"] != null))
            {
                tDatos = (DataTable)cache["TraerTiposCampamentos"];
            }
            else
            {
                tDatos = NUT.LIB.GEN.LO.GEstaticos.TraerEstaticosxGrupo("NEG.Campamento.Tipo").Tables[0];
                cache.Insert("TraerTiposCampamentos", tDatos, null, DateTime.Now.AddHours(horasCache), TimeSpan.Zero);
            }
            return tDatos;
        }

        public static DataTable TraerEstadosCampamento()
        {
            System.Web.Caching.Cache cache = System.Web.HttpContext.Current.Cache;
            DataTable tDatos = null;
            if ((cache["TraerEstadosCampamento"] != null))
            {
                tDatos = (DataTable)cache["TraerEstadosCampamento"];
            }
            else
            {
                tDatos = NUT.LIB.GEN.LO.GEstaticos.TraerEstaticosxGrupo("NEG.Campamento.Estado").Tables[0];
                cache.Insert("TraerEstadosCampamento", tDatos, null, DateTime.Now.AddHours(horasCache), TimeSpan.Zero);
            }
            return tDatos;
        }
        #endregion

    }
}