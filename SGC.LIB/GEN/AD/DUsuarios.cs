using System;
using System.Data;
using System.Data.Common;
using XP.AD;
using NUT.LIB.GEN.EN;
using System.Text;
using System.Collections.Generic;
using NUT.LIB.GEN.SF;

namespace NUT.LIB.GEN.AD
{

    public class DUsuarios : XP.AD.Datos<DUsuarios>
    {

        #region Consultas

        /******** VALOR/REGISTRO ÚNICO *********/
        public static DataSet TraerUsuario(int idUsuario)
        {
            return bd.EjecutarConsultaProc("GEN.TraerUsuario",
                bd.CrearParametro("IdUsuario", idUsuario, DbType.Int32));
        }


        /********** VARIOS REGISTROS ***********/
        public static DataSet TraerMenuUsuario(int idUsuario)
        {
            return bd.EjecutarConsultaProc("GEN.TraerMenuUsuario",
                bd.CrearParametro("IdUsuario", idUsuario, DbType.Int32));
        }

        public static DataSet TraerFuncionesVisibles()
        {
            return bd.EjecutarConsultaProc("GEN.TraerFuncionesVisibles");
        }

        public static DataSet BuscarUsuarios(FilterUsuarios filter)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@"SELECT
                            u.Id,
                            u.Cuenta,
                            u.Estado,
                            ee.Descripcion DescripcionEstado,
                            GEN.ArmarNombreCompleto(u.Nombre, u.ApellidoPaterno, u.ApellidoMaterno) NombreCompletoUsuario
                        FROM
                            GEN.Usuario u
                            JOIN GEN.Estaticos ee ON ee.Grupo='GEN.Usuario.Estado' AND u.Estado=ee.Codigo
	                    WHERE  ");
            List<DbParameter> lPars = new List<DbParameter>();

            if (!string.IsNullOrEmpty(filter.NombreCompleto))
            {
                int numero = 1;
                string[] partesFiltro = filter.NombreCompleto.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                foreach (string parte in partesFiltro)
                {
                    sb.AppendFormat("(u.Nombre LIKE {0} OR u.ApellidoPaterno LIKE {0} OR u.ApellidoMaterno LIKE {0}) AND   ", bd.ParametroDML("NombreCompleto" + numero));
                    lPars.Add(bd.CrearParametro("NombreCompleto" + numero, (parte.StartsWith("%") ? "" : "%") + parte + (parte.EndsWith("%") ? "" : "%"), DbType.AnsiString));
                    numero++;
                }
            }
            if (!string.IsNullOrEmpty(filter.Cuenta))
            {
                sb.AppendFormat("u.Cuenta LIKE {0} AND   ", bd.ParametroDML("Cuenta"));
                lPars.Add(bd.CrearParametro("Cuenta", filter.Cuenta, DbType.AnsiString, 50));
            }
            if (filter.Estado > 0)
            {
                sb.AppendFormat("u.Estado={0} AND   ", filter.Estado);
            }
            sb.Length = sb.Length - 7;
            sb.Append(" ORDER BY NombreCompletoUsuario");
            return bd.EjecutarConsulta(sb.ToString(), lPars.ToArray());
        }
        /************** REPORTES ***************/



        #endregion

        #region Verificaciones

        public static DataSet AutenticarUsuario(string cuenta, string contrasena)
        {
            return bd.EjecutarConsultaProc("GEN.AutenticarUsuario",
                bd.CrearParametro("Cuenta", cuenta, DbType.AnsiString, 50),
                bd.CrearParametro("Contrasena", contrasena, DbType.AnsiString, 50));
        }

        public static bool VerificarAccesoCodigoPantallaUsuario(int idUsuario, string codigoPantalla)
        {
            return Convert.ToInt32(bd.EjecutarValorProc("GEN.VerificarAccesoCodigoPantallaUsuario",
                bd.CrearParametro("IdUsuario", idUsuario, DbType.Int32),
                bd.CrearParametro("CodigoPantalla", codigoPantalla, DbType.AnsiString, 50))) > 0;
        }

        public static bool ExisteCuentaUsuario(string cuenta, int idUsuarioExcluido)
        {
            return Convert.ToInt32(bd.EjecutarValor(string.Format(
                "SELECT COUNT(*) FROM GEN.Usuario u WHERE u.Cuenta = {0} AND u.Id<>{1}",
                bd.ParametroDML("Cuenta"), idUsuarioExcluido),
                bd.CrearParametro("Cuenta", cuenta, DbType.AnsiString, 50))) > 0;
        }
        #endregion

        #region Transacciones

        public static void EliminarFuncionesUsuario(int idUsuario)
        {
            bd.EjecutarValor(string.Format(@"
                                            DELETE FROM GEN.UsuarioFuncion WHERE IdUsuario= {0}", idUsuario));
        }

        #endregion

        #region Procesos



        #endregion

    }

}  
