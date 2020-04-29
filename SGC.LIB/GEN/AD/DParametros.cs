using System;
using System.Data;
using System.Data.Common;
using XP.AD;
using NUT.LIB.GEN.EN;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace NUT.LIB.GEN.AD
{

    public class DParametros : XP.AD.Datos<DParametros>
    {

        #region Consultas

        /******** VALOR/REGISTRO ÚNICO *********/



        /********** VARIOS REGISTROS ***********/
        public static Dictionary<string, string> TraerParametros(params string[] codigos)
        {
            if (codigos.Length == 0)
            {
                return null;
            }
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT Codigo,Valor FROM GEN.Parametro WHERE Codigo IN({0})", string.Join(",", codigos.Select(x => "'" + x + "'")));
            Dictionary<string, string> dic = new Dictionary<string, string>();
            using (DbDataReader dtr = bd.EjecutarDataReader(sb.ToString()))
            {
                while (dtr.Read())
                {
                    dic.Add(dtr["Codigo"].ToString(), dtr["Valor"].ToString());
                }
            }
            return dic;
        }


        /************** REPORTES ***************/



        #endregion

        #region Verificaciones



        #endregion

        #region Transacciones



        #endregion

        #region Procesos



        #endregion

    }

}  
