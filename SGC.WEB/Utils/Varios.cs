using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Specialized;
using System.Data;

namespace NUT.WEB.Utils
{
    public class Varios
    {

        #region Manipulación cadenas

        public static string AgregarComodinesBusqueda(string valor)
        {
            if (!string.IsNullOrEmpty(valor))
            {
                valor = (!valor.StartsWith("%") ? "%" : "") + valor + (!valor.EndsWith("%") ? "%" : "");
            }
            return valor;
        }
        public static string AgregarComodinesBusquedaPorPalabra(string valor)
        {
            string resultado = valor;
            if (!string.IsNullOrEmpty(valor))
            {
                string[] palabras = valor.Split(" ".ToCharArray());
                resultado = "";
                foreach (string palabra in palabras)
                {
                    resultado = resultado + AgregarComodinesBusqueda(palabra.Trim()) + " ";
                }

                resultado = resultado.Trim();
            }
            return resultado;
        }
        public static string PrimeraMayuscula(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            return char.ToUpper(s[0]) + s.Substring(1);
        }

        #endregion

        #region Cookies

        public static void ConfigurarCookie(string nombre, string valor)
        {
            HttpCookie cookie = new HttpCookie(nombre, valor);
            cookie.Expires = DateTime.Now.AddYears(1);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
        public static string LeerCookie(string nombre, string valorDefecto)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[nombre];
            if (cookie == null)
            {
                return valorDefecto;
            }
            return cookie.Value;
        }
        public static void ConfigurarCookieVarios(string nombre, params string[] valores)
        {
            HttpCookie cookie = new HttpCookie(nombre);
            for (int i = 0; i < valores.Length - 1; i++)
            {
                cookie.Values.Add(valores[i], valores[i + 1] ?? "");
            }
            cookie.Expires = DateTime.Now.AddYears(1);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
        public static NameValueCollection LeerCookieVarios(string nombre)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[nombre];
            if (cookie == null)
            {
                return null;
            }
            return cookie.Values;
        }
        public static void LimpiarCookie(string nombre)
        {
            HttpCookie cookie = new HttpCookie(nombre);
            cookie.Expires = DateTime.Now.AddYears(-1);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        #endregion

        #region Paginación

        public static int PaginarDataTable(DataTable tDatos, int pagina, int tamanoPagina)
        {
            int cantidadFilas = tDatos.Rows.Count;
            int cantidadPaginas = cantidadFilas % tamanoPagina == 0 ? cantidadFilas / tamanoPagina : (cantidadFilas / tamanoPagina) + 1;
            int inicioPagina = ((pagina - 1) * tamanoPagina);
            int finPagina = inicioPagina + tamanoPagina - 1;
            int i = 0;
            foreach (DataRow rTarjeta in tDatos.Rows)
            {
                if (i < inicioPagina || i > finPagina)
                {
                    rTarjeta.Delete();
                }
                i++;
            }
            tDatos.AcceptChanges();
            return cantidadPaginas;
        }
        public static int PaginarDataTable(DataTable tDatos, int pagina)
        {
            int tamanoPaginacion = Convert.ToInt32(HttpContext.Current.Application["DistribucionInterfaz.TamanoPaginacion"]);
            return PaginarDataTable(tDatos, pagina, tamanoPaginacion);
        }
        public static int PaginarArreglo(ref object[] lDatos, int pagina, int tamanoPagina)
        {
            int cantidadFilas = lDatos.Length;
            int cantidadPaginas = cantidadFilas % tamanoPagina == 0 ? cantidadFilas / tamanoPagina : (cantidadFilas / tamanoPagina) + 1;
            int inicioPagina = ((pagina - 1) * tamanoPagina);
            int finPagina = inicioPagina + tamanoPagina - 1;
            List<object> lDatosNuevo = new List<object>();
            for (int i = inicioPagina; i <= finPagina && i <= lDatos.Length - 1; i++)
            {
                lDatosNuevo.Add(lDatos[i]);
            }
            lDatos = lDatosNuevo.ToArray();
            return cantidadPaginas;
        }
        public static int PaginarArreglo(ref object[] lDatos, int pagina)
        {
            int tamanoPaginacion = Convert.ToInt32(HttpContext.Current.Application["DistribucionInterfaz.TamanoPaginacion"]);
            return PaginarArreglo(ref lDatos, pagina, tamanoPaginacion);
        }

        #endregion

        #region Números literales

        public static string DecimalALetras(decimal numero)
        {
            string prefijo = "";
            if (numero < 0)
            {
                numero = Math.Abs(numero);
                prefijo = "MENOS ";
            }
            long entero = Convert.ToInt64(Math.Truncate(numero));
            int decimales = Convert.ToInt32((Math.Round(numero, 2) - entero) * 100);
            return prefijo + NaturalALetras(entero) + " " + decimales.ToString("00") + "/100";

        }
        private static string NaturalALetras(long numero)
        {
            string sNumero = "";
            if (numero == 0) sNumero = "CERO";
            else if (numero == 1) sNumero = "UNO";
            else if (numero == 2) sNumero = "DOS";
            else if (numero == 3) sNumero = "TRES";
            else if (numero == 4) sNumero = "CUATRO";
            else if (numero == 5) sNumero = "CINCO";
            else if (numero == 6) sNumero = "SEIS";
            else if (numero == 7) sNumero = "SIETE";
            else if (numero == 8) sNumero = "OCHO";
            else if (numero == 9) sNumero = "NUEVE";
            else if (numero == 10) sNumero = "DIEZ";
            else if (numero == 11) sNumero = "ONCE";
            else if (numero == 12) sNumero = "DOCE";
            else if (numero == 13) sNumero = "TRECE";
            else if (numero == 14) sNumero = "CATORCE";
            else if (numero == 15) sNumero = "QUINCE";
            else if (numero < 20) sNumero = "DIECI" + NaturalALetras(numero - 10);
            else if (numero == 20) sNumero = "VEINTE";
            else if (numero < 30) sNumero = "VEINTI" + NaturalALetras(numero - 20);
            else if (numero == 30) sNumero = "TREINTA";
            else if (numero == 40) sNumero = "CUARENTA";
            else if (numero == 50) sNumero = "CINCUENTA";
            else if (numero == 60) sNumero = "SESENTA";
            else if (numero == 70) sNumero = "SETENTA";
            else if (numero == 80) sNumero = "OCHENTA";
            else if (numero == 90) sNumero = "NOVENTA";
            else if (numero < 100) sNumero = NaturalALetras(numero - (numero % 10)) + " Y " + NaturalALetras(numero % 10);
            else if (numero == 100) sNumero = "CIEN";
            else if (numero < 200) sNumero = "CIENTO " + NaturalALetras(numero - 100);
            else if ((numero == 200) || (numero == 300) || (numero == 400) || (numero == 600) || (numero == 800)) sNumero = NaturalALetras(numero / 100) + "CIENTOS";
            else if (numero == 500) sNumero = "QUINIENTOS";
            else if (numero == 700) sNumero = "SETECIENTOS";
            else if (numero == 900) sNumero = "NOVECIENTOS";
            else if (numero < 1000) sNumero = NaturalALetras(numero - (numero % 100)) + " " + NaturalALetras(numero % 100);
            else if (numero == 1000) sNumero = "MIL";
            else if (numero < 2000) sNumero = "MIL " + NaturalALetras(numero % 1000);
            else if (numero < 1000000)
            {
                sNumero = NaturalALetras(numero / 1000) + " MIL";
                if ((numero % 1000) > 0) sNumero = sNumero + " " + NaturalALetras(numero % 1000);
            }
            else if (numero == 1000000) sNumero = "UN MILLON";
            else if (numero < 2000000) sNumero = "UN MILLON " + NaturalALetras(numero % 1000000);
            else if (numero < 1000000000000)
            {
                sNumero = NaturalALetras(numero / 1000000) + " MILLONES ";
                if ((numero % 1000000) > 0) sNumero = sNumero + " " + NaturalALetras(numero % 1000000);
            }
            else if (numero == 1000000000000) sNumero = "UN BILLON";
            else if (numero < 2000000000000) sNumero = "UN BILLON " + NaturalALetras(numero % 1000000000000);
            else
            {
                sNumero = NaturalALetras(numero / 1000000000000) + " BILLONES";
                if ((numero % 1000000000000) > 0) sNumero = sNumero + " " + NaturalALetras(numero % 1000000000000);
            }
            return sNumero;
        }

        #endregion

        #region Formateo datos DataRow

        public static string FormatearString(DataRow r, string columna)
        {
            return !r.IsNull(columna) ? r[columna].ToString() : "----";
        }
        public static string FormatearDecimal(DataRow r, string columna, string formato)
        {
            return !r.IsNull(columna) ? r.Field<decimal>(columna).ToString(formato) : "----";
        }
        public static string FormatearEntero(DataRow r, string columna)
        {
            return !r.IsNull(columna) ? r.Field<int>(columna).ToString() : "----";
        }
        public static string FormatearFecha(DataRow r, string columna, string formato)
        {
            return !r.IsNull(columna) ? r.Field<DateTime>(columna).ToString(formato) : "----";
        }
        public static string FormatearFechaShortDateString(DataRow r, string columna)
        {
            return !r.IsNull(columna) ? r.Field<DateTime>(columna).ToShortDateString() : "----";
        }
        public static string FormatearLogico(DataRow r, string columna)
        {
            return !r.IsNull(columna) ? (r[columna].ToString() == "S" ? "SI" : "NO") : "----";
        }

        #endregion

        public static string FormatearHora(TimeSpan hora)
        {
            return new DateTime(2000, 1, 1, hora.Hours, hora.Minutes, 0).ToString("HH:mm");
        }
        public static string FormatearNombre(string nombres, string apellidoPaterno, string apellidoMaterno)
        {
            return string.Format("{0} {1} {2}", nombres, apellidoPaterno, apellidoMaterno);
        }

        public static DateTime LeerUTC(DateTime origen)
        {
            return origen.AddHours(Convert.ToInt32(HttpContext.Current.Application["General.TimeZoneId"].ToString()));
        }
    }
}
