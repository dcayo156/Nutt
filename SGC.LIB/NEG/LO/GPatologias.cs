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

    public class GPatologias : XP.LO.Gestor<GPatologias>
    {

        #region Consultas

        /******** VALOR/REGISTRO ÚNICO *********/



        /********** VARIOS REGISTROS ***********/

        public static DataSet BuscarPatologias(FPatologias filter)
        {
            return DPatologias.BuscarPatologias(filter);
        }

        /************** REPORTES ***************/



        #endregion

        #region Verificaciones
        public static bool ExisteNombreUsuario(string nombre, int idPatologia)
        {
            return DPatologias.ExisteNombrePatologia(nombre, idPatologia);
        }
        public static bool ExistePatologiaEnHistoriaClinica(int idPatologia)
        {
            return DPatologias.ExistePatologiaEnHistoriaClinica(idPatologia);
        }

        #endregion

        #region Transacciones

        public static void RegistrarPatologia(Patologia eUsuario)
        {
            //Actualizar la entidad Patologia falta el campo Nombre
            if (ExisteNombreUsuario("Nombre" , eUsuario.Id.Value))
            {
                throw new LogicaException("La nombre especificado ya pertenece a otra Patologia", "NombreExistente");
            }
            using (TransactionScope ts = new TransactionScope())
            {
                Insertar(eUsuario);
                ts.Complete();
            }
        }
        public static void ModificarPatologia(Patologia ePatologia)
        {
            //Actualizar la entidad Patologia falta el campo Nombre
            if (ExisteNombreUsuario("Nombre", ePatologia.Id.Value))
            {
                throw new LogicaException("La nombre especificado ya pertenece a otra Patologia", "NombreExistente");
            }
            using (TransactionScope ts = new TransactionScope())
            {
                Modificar(ePatologia);
                ts.Complete();
            }
        }
        public static void EliminarPatologia(int idPatologia)
        {
            if (ExistePatologiaEnHistoriaClinica(idPatologia))
            {
                throw new LogicaException("La patología esta asociada a una historia clinica", "PatologiaRelacionada");
            }   
            using (TransactionScope ts = new TransactionScope())
            {
                Eliminar(new Patologia { Id = idPatologia });
                ts.Complete();
            }
        }
        #endregion

        #region Procesos



        #endregion

    }

}  
