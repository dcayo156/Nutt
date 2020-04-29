using System;
using System.Data;
using XP.AUT;
using NUT.LIB.GEN.EN;
using NUT.LIB.GEN.AD;
using System.Collections.Generic;

namespace NUT.LIB.GEN.LO
{

    public class GParametros : XP.LO.Gestor<GParametros>
    {

        #region Consultas

        /******** VALOR/REGISTRO ÚNICO *********/



        /********** VARIOS REGISTROS ***********/
        public static Dictionary<string, string> TraerParametros(params string[] codigos)
        {
            return DParametros.TraerParametros(codigos);
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
