using System;
using System.Data;
using System.Data.Common;
using XP.AD;
using NUT.LIB.GEN.EN;

namespace NUT.LIB.GEN.AD
{

    public class DPantallas : XP.AD.Datos<DPantallas>
    {

        #region Consultas

        /******** VALOR/REGISTRO ÚNICO *********/



        /********** VARIOS REGISTROS ***********/
        public static DataSet TraerFormulariosActivos()
        {
            return bd.EjecutarConsulta(string.Format("SELECT Codigo,Ubicacion,Ruta FROM GEN.Pantalla WHERE Estado={0} ORDER BY PrioridadEnrutamiento DESC",
                Constantes.PANTALLA_ESTADO_ACTIVO));
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
