using System;
using System.Data;
using XP.AUT;
using NUT.LIB.GEN.EN;
using NUT.LIB.GEN.AD;

namespace NUT.LIB.GEN.LO
{

    public class GEstaticos : XP.LO.Gestor<GEstaticos>
    {

        #region Consultas

        /******** VALOR/REGISTRO ÚNICO *********/



        /********** VARIOS REGISTROS ***********/
        public static DataSet TraerEstaticosxGrupo(string grupo)
        {
            return DEstaticos.TraerEstaticosxGrupo(grupo);
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
