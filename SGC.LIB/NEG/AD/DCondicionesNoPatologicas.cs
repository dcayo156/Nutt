using System;
using System.Data;
using System.Data.Common;
using XP.AD;
using NUT.LIB.NEG.EN;
using NUT.LIB.NEG.SF;
using System.Text;
using System.Collections.Generic;

namespace NUT.LIB.NEG.AD
{

    public class DCondicionesNoPatologicas : XP.AD.Datos<DCondicionesNoPatologicas>
    {

        #region Consultas

        /******** VALOR/REGISTRO ÚNICO *********/
        public static DataSet TraerCondicionNoPatologica(int idCondicionNoPatologica)
        {
            return bd.EjecutarConsultaProc("NEG.TraerCondicionNoPatologica",
                bd.CrearParametro("idCondicionNoPatologica", idCondicionNoPatologica, DbType.Int32));
        }


        /********** VARIOS REGISTROS ***********/
        public static DataSet BuscarCondicionNoPatologicas(FCondicionesNoPatologicas filter)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@"SELECT
	                        c.Id,
	                        c.Nombre,
	                        c.CargarEnHistoriaClinica,
	                        c.Estado,
	                        ee.Descripcion DescripcionEstado
                        FROM
	                        NEG.CondicionNoPatologica c
	                        JOIN GEN.Estaticos ee ON ee.Grupo='NEG.CondicionNoPatologica.Estado' AND c.Estado=ee.Codigo
	                    WHERE  ");
            List<DbParameter> lPars = new List<DbParameter>();

            if (!string.IsNullOrEmpty(filter.Nombre))
            {
                sb.AppendFormat("p.Nombre LIKE {0} AND   ", bd.ParametroDML("Nombre"));
                lPars.Add(bd.CrearParametro("Nombre", filter.Nombre, DbType.AnsiString, 50));
            }
            if (!string.IsNullOrEmpty(filter.CargarEnHistoriaClinica))
            {
                sb.AppendFormat("p.CargarEnHistoriaClinica = {0} AND   ", bd.ParametroDML("CargarEnHistoriaClinica"));
                lPars.Add(bd.CrearParametro("CargarEnHistoriaClinica", filter.CargarEnHistoriaClinica, DbType.AnsiString, 1));
            }
            if (filter.Estado > 0)
            {
                sb.AppendFormat("p.Estado={0} AND   ", filter.Estado);
            }
            sb.Length = sb.Length - 7;
            sb.Append(" ORDER BY Nombre");
            return bd.EjecutarConsulta(sb.ToString(), lPars.ToArray());
        }


        /************** REPORTES ***************/



        #endregion

        #region Verificaciones
        public static bool ExisteNombreCondicionNoPatologica(string Nombre, int idCondicionNoPatologica)
        {
            return Convert.ToInt32(bd.EjecutarValor(string.Format(
                "SELECT COUNT(Id) FROM NEG.CondicionNoPatologica c WHERE c.Nombre = {0} AND c.Id = {1}",
                bd.ParametroDML("Nombre"), idCondicionNoPatologica),
                bd.CrearParametro("Nombre", Nombre, DbType.AnsiString, 50))) > 0;
        }
        public static bool ExisteCondicionNoPatologicaEnHistoriaClinica(int idCondicionNoPatologica)
        {
            return Convert.ToInt32(bd.EjecutarValor(string.Format(
                "SELECT COUNT(Id) FROM NEG.HistoriaClinicaCondicionNoPatologica h WHERE h.IdCondicionNoPatologica = {0}",
                idCondicionNoPatologica))) > 0;
        }

        #endregion

        #region Transacciones



        #endregion

        #region Procesos



        #endregion

    }

}  
