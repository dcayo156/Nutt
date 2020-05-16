using System;
using System.Data;
using XP.AUT;
using NUT.LIB.NEG.EN;
using NUT.LIB.NEG.AD;
using NUT.LIB.NEG.SF;
using XP.LO;
using System.Transactions;

namespace NUT.LIB.NEG.LO
{

    public class GCondicionesNoPatologicas : XP.LO.Gestor<GCondicionesNoPatologicas>
    {

        #region Consultas

        /******** VALOR/REGISTRO ÚNICO *********/

        public static DataSet TraerCondicionNoPatologica(int idCondicionNoPatologica)
        {
            return DCondicionesNoPatologicas.TraerCondicionNoPatologica(idCondicionNoPatologica);
        }

        /********** VARIOS REGISTROS ***********/

        public static DataSet BuscarCondicionNoPatologicas(FCondicionesNoPatologicas filter)
        {
            return DCondicionesNoPatologicas.BuscarCondicionNoPatologicas(filter);
        }

        /************** REPORTES ***************/



        #endregion

        #region Verificaciones
        public static bool ExisteNombreCondicionNoPatologica(string nombre, int idCondicionNoPatologica)
        {
            return DCondicionesNoPatologicas.ExisteNombreCondicionNoPatologica(nombre, idCondicionNoPatologica);
        }
        public static bool ExisteCondicionNoPatologicaEnHistoriaClinica(int idCondicionNoPatologica)
        {
            return DCondicionesNoPatologicas.ExisteCondicionNoPatologicaEnHistoriaClinica(idCondicionNoPatologica);
        }

        #endregion

        #region Transacciones

        public static void RegistrarCondicionNoPatologica(CondicionNoPatologica eCondicionNoPatologica)
        {
            DateTime ahora = DateTime.Now.ToUniversalTime();
            if (ExisteNombreCondicionNoPatologica(eCondicionNoPatologica.Nombre, eCondicionNoPatologica.Id.Value))
            {
                throw new LogicaException("El nombre especificado ya pertenece a otra Condición no Patologica", "NombreExistente");
            }
            eCondicionNoPatologica.IdUsuReg = usr.Id;
            eCondicionNoPatologica.FecReg = ahora;
            eCondicionNoPatologica.IdUsuMod = usr.Id;
            eCondicionNoPatologica.FecMod = ahora;
            using (TransactionScope ts = new TransactionScope())
            {
                Insertar(eCondicionNoPatologica);
                ts.Complete();
            }
        }
        public static void ModificarCondicionNoPatologica(CondicionNoPatologica eCondicionNoPatologica)
        {
            DateTime ahora = DateTime.Now.ToUniversalTime();
            if (ExisteNombreCondicionNoPatologica(eCondicionNoPatologica.Nombre, eCondicionNoPatologica.Id.Value))
            {
                throw new LogicaException("El nombre especificado ya pertenece a otra Condición no Patologica", "NombreExistente");
            }
            eCondicionNoPatologica.IdUsuMod = usr.Id;
            eCondicionNoPatologica.FecMod = ahora;
            using (TransactionScope ts = new TransactionScope())
            {
                Modificar(eCondicionNoPatologica, "IdUsuReg", "FecReg");
                ts.Complete();
            }
        }
        public static void EliminarCondicionNoPatologica(int idCondicionNoPatologica)
        {
            if (ExisteCondicionNoPatologicaEnHistoriaClinica(idCondicionNoPatologica))
            {
                throw new LogicaException("La condición no patologica esta asociada a una historia clinica", "CondicionNoPatologicaRelacionada");
            }
            using (TransactionScope ts = new TransactionScope())
            {
                Eliminar(new CondicionNoPatologica { Id = idCondicionNoPatologica });
                ts.Complete();
            }
        }
        #endregion

        #region Procesos



        #endregion
    }

}  
