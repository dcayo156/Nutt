using System;
using System.Data;
using System.Data.Common;
using XP.AD;
using NUT.LIB.NEG.EN;
using System.Text;
using NUT.LIB.NEG.SF;
using System.Collections.Generic;

namespace NUT.LIB.NEG.AD
{

    public class DPatologias : Datos<DPatologias>
    {

        #region Consultas

        /******** VALOR/REGISTRO ÚNICO *********/



        /********** VARIOS REGISTROS ***********/
        public static DataSet BuscarPatologias(FPatologias filter)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@"SELECT
	                        p.Id,
	                        p.Nombre,
	                        p.CargarEnHistoriaClinica,
	                        CASE p.CargarEnHistoriaClinica 
			                        WHEN 'S' THEN 'SI'
			                        WHEN 'N' THEN 'NO' END DescripcionCargarEnHistoriaClinica,
	                        p.EsHabitoFisiologico,
	                        p.Estado,
	                        ee.Descripcion DescripcionEstado
                        FROM
	                        NEG.Patologia p
	                        JOIN GEN.Estaticos ee ON ee.Grupo='NEG.Patologia.Estado' AND p.Estado=ee.Codigo
	                    WHERE  ");
            List<DbParameter> lPars = new List<DbParameter>();

            if (!string.IsNullOrEmpty(filter.Nombre))
            {
                sb.AppendFormat("p.Nombre LIKE {0} AND   ", bd.ParametroDML("Nombre"));
                lPars.Add(bd.CrearParametro("Nombre", filter.Nombre, DbType.AnsiString, 50));
            }
            if (!string.IsNullOrEmpty(filter.EsHabitoFisiologico))
            {
                sb.AppendFormat("p.EsHabitoFisiologico = {0} AND   ", bd.ParametroDML("EsHabitoFisiologico"));
                lPars.Add(bd.CrearParametro("EsHabitoFisiologico", filter.EsHabitoFisiologico, DbType.AnsiString, 50));
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
        public static bool ExisteNombrePatologia(string Nombre, int idPatologia)
        {
            return Convert.ToInt32(bd.EjecutarValor(string.Format(
                "SELECT COUNT(*) FROM NEG.Patologia p WHERE p.Nombre = {0} AND u.Id = {1}",
                bd.ParametroDML("Nombre"), idPatologia),
                bd.CrearParametro("Nombre", Nombre, DbType.AnsiString, 50))) > 0;
        }
        public static bool ExistePatologiaEnHistoriaClinica(int idPatologia)
        {
            return Convert.ToInt32(bd.EjecutarValor(string.Format(
                "SELECT COUNT(*) FROM NEG.HistoriaClinicaPatologia h WHERE h.IdPatologia = {0}",
                idPatologia))) > 0;
        }
        public static void EliminarPatologia(int idPatologia)
        {
            bd.EjecutarValor(string.Format(@"
                                            DELETE FROM NEG.Patologia WHERE Id= {0}", idPatologia));
        }
        #endregion

        #region Transacciones



        #endregion

        #region Procesos



        #endregion

    }

}  
