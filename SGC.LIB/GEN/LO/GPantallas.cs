using System;
using System.Data;
using XP.AUT;
using NUT.LIB.GEN.EN;
using NUT.LIB.GEN.AD;

namespace NUT.LIB.GEN.LO
{

    public class GPantallas : XP.LO.Gestor<GPantallas>
    {

        #region Consultas

        /******** VALOR/REGISTRO ÚNICO *********/



        /********** VARIOS REGISTROS ***********/
        public static DataSet TraerFormulariosActivos()
        {
            return DPantallas.TraerFormulariosActivos();
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
