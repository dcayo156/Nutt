using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SGC.WEB
{
    public partial class cambiocontrasena : Utils.PaginaBase
    {
        const string CODIGOPANTALLA = "CAMCONT";
        public override string CODIGO_PANTALLA
        {
            get { return CODIGOPANTALLA; }
        }

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            CargaInicial();
        }

        #endregion

        #region Métodos

        void CargaInicial()
        {
            Master.Titulo = "CAMBIO DE CONTRASEÑA";
            //int longitudMaxima = Convert.ToInt32(GParametros.TraerParametro("SeguridadContrasena.LongitudMaxima"));
            //txbNueva.MaxLength = longitudMaxima;
            //txbNueva2.MaxLength = longitudMaxima;
        }

        #endregion

        #region Ajax

        //[WebMethod()]
        //public static object CambiarContrasena(string contrasenaNueva, string contrasenaAnterior)
        //{
        //    try
        //    {
        //        int cantidadMinimaEspeciales, cantidadMinimaMayusculas, cantidadMinimaNumeros, longitudMinima, longitudMaxima;
        //        List<string> lMensajes = new List<string>();

        //        if (!GUsuarios.VerificarContrasenaUsuario(usr.Id, contrasenaAnterior))
        //        {
        //            lMensajes.Add("La contraseña especificada como 'actual' no es la correcta.");
        //        }

        //        if (!GUsuarios.VerificarPoliticasContrasena(contrasenaNueva, out cantidadMinimaMayusculas,
        //             out cantidadMinimaNumeros, out longitudMaxima, out longitudMinima, out  cantidadMinimaEspeciales))
        //        {
        //            if (contrasenaNueva.Length < longitudMinima)
        //            {
        //                lMensajes.Add(String.Format("La nueva contraseña debe tener por lo menos {0} caracteres.", longitudMinima));
        //            }
        //            if (contrasenaNueva.Length > longitudMaxima)
        //            {
        //                lMensajes.Add(String.Format("La nueva contraseña debe tener máximo hasta {0} caracteres.", longitudMaxima));
        //            }
        //            if (contrasenaNueva.ToArray().Where(x => char.IsUpper(x)).Count() < cantidadMinimaMayusculas)
        //            {
        //                lMensajes.Add(String.Format("La nueva contraseña debe incluir por lo menos {0} letra(s) mayúscula(s).", cantidadMinimaMayusculas));
        //            }
        //            if (contrasenaNueva.ToArray().Where(x => char.IsNumber(x)).Count() < cantidadMinimaNumeros)
        //            {
        //                lMensajes.Add(String.Format("La nueva contraseña debe incluir por lo menos {0} número(s).", cantidadMinimaNumeros));
        //            }
        //            if ((contrasenaNueva.ToArray().Where(x => (char.IsLetterOrDigit(x) == false)).Count() < cantidadMinimaEspeciales))
        //            {
        //                lMensajes.Add(String.Format("La nueva contraseña debe incluir por lo menos {0} caracter(es) especial(es).", cantidadMinimaEspeciales));
        //            }
        //        }

        //        if (lMensajes.Count > 0)
        //        {
        //            return new
        //            {
        //                Estado = "error",
        //                Mensajes = lMensajes.ToArray()
        //            };
        //        }

        //        GUsuarios.CambiarContrasenaUsuario(usr.Id, contrasenaNueva);

        //        return new
        //        {
        //            Estado = "ok"
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        Utils.Inst.Error("Error no controlado en la aplicación.", ex);
        //        throw;
        //    }
        //}

        #endregion
    }
}