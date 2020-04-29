using System;
using System.Data;
using System.Data.Common;
using XP.AD;
using NUT.LIB.GEN.EN;

namespace NUT.LIB.GEN.AD
{

    public class DEstaticos : XP.AD.Datos<DEstaticos>
    {

        #region Consultas

        /******** VALOR/REGISTRO ÚNICO *********/
        //alkjhzxckjhxzcl


        /********** VARIOS REGISTROS ***********/
        public static DataSet TraerEstaticosxGrupo(string grupo)
        {
            return bd.EjecutarConsulta(string.Format("SELECT Codigo,Descripcion,Observaciones,Orden FROM GEN.Estaticos WHERE Grupo={0} ORDER BY Orden",
                bd.ParametroDML("Grupo")), bd.CrearParametro("Grupo", grupo, DbType.AnsiString, 100));
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
