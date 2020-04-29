using System;
using System.Data;
using XP.AUT;
using NUT.LIB.GEN.EN;
using NUT.LIB.GEN.AD;
using System.Security.Cryptography;
using NUT.LIB.NEG.EN;
using System.Transactions;
using System.Collections.Generic;
using XP.LO;
using NUT.LIB.GEN.SF;

namespace NUT.LIB.GEN.LO
{

    public class GUsuarios : XP.LO.Gestor<GUsuarios>
    {

        #region Consultas

        /******** VALOR/REGISTRO ÚNICO *********/
        public static string Hash(string contrasena)
        {
            return Convert.ToBase64String(new SHA256Managed().ComputeHash(System.Text.Encoding.UTF8.GetBytes(contrasena)));
        }

        public static DataSet TraerUsuario(int idUsuario)
        {
            return DUsuarios.TraerUsuario(idUsuario);
        }

        /********** VARIOS REGISTROS ***********/
        public static DataSet TraerMenuUsuario(int idUsuario)
        {
            return DUsuarios.TraerMenuUsuario(idUsuario);
        }

        public static DataSet TraerFuncionesVisibles()
        {
            return DUsuarios.TraerFuncionesVisibles();
        }

        public static DataSet BuscarUsuarios(FilterUsuarios filter)
        {
            return DUsuarios.BuscarUsuarios(filter);
        }
        public static DataSet TraerEstadosUsuario()
        {
            return DEstaticos.TraerEstaticosxGrupo("GEN.Usuario.Estado");
        }
        /************** REPORTES ***************/


        #endregion

        #region Verificaciones

        public static DataSet AutenticarUsuario(string cuenta, string contrasena)
        {
            return DUsuarios.AutenticarUsuario(cuenta, Hash(contrasena));
        }

        public static bool VerificarAccesoCodigoPantallaUsuario(int idUsuario, string codigoPantalla)
        {
            return DUsuarios.VerificarAccesoCodigoPantallaUsuario(idUsuario, codigoPantalla);
        }

        public static bool ExisteCuentaUsuario(string cuenta, int idUsuarioExcluido)
        {
            return DUsuarios.ExisteCuentaUsuario(cuenta, idUsuarioExcluido);
        }

        public static bool VerificarPoliticasContrasena(string contrasena, out int cantidadMinimaMayusculas, out int cantidadMinimaNumeros,
                      out int longitudMaxima, out int longitudMinima, out int cantidadMinimaEspeciales)
        {
            Dictionary<string, string> dPars = DParametros.TraerParametros(
                "SeguridadContrasena.CantidadMinimaEspeciales",
                "SeguridadContrasena.LongitudMaxima",
                "SeguridadContrasena.LongitudMinima",
                "SeguridadContrasena.CantidadMinimaNumeros",
                "SeguridadContrasena.CantidadMinimaMayusculas");

            cantidadMinimaEspeciales = Convert.ToInt32(dPars["SeguridadContrasena.CantidadMinimaEspeciales"]);
            cantidadMinimaMayusculas = Convert.ToInt32(dPars["SeguridadContrasena.CantidadMinimaMayusculas"]);
            cantidadMinimaNumeros = Convert.ToInt32(dPars["SeguridadContrasena.CantidadMinimaNumeros"]);
            longitudMaxima = Convert.ToInt32(dPars["SeguridadContrasena.LongitudMaxima"]);
            longitudMinima = Convert.ToInt32(dPars["SeguridadContrasena.LongitudMinima"]);

            if (contrasena.Length < longitudMinima)
            {
                return false;
            }
            else if (contrasena.Length > longitudMaxima)
            {
                return false;
            }

            int contadorNumeros = 0, contadorMayusculas = 0, contadorEspeciales = 0;
            foreach (Char caracter in contrasena.ToCharArray())
            {
                if (char.IsDigit(caracter))
                {
                    contadorNumeros++;
                }
                else if (char.IsUpper(caracter))
                {
                    contadorMayusculas++;
                }
                if (!char.IsLetterOrDigit(caracter))
                {
                    contadorEspeciales++;
                }

            }
            if (contadorNumeros < cantidadMinimaNumeros || contadorMayusculas < cantidadMinimaMayusculas || contadorEspeciales < cantidadMinimaEspeciales)
            {
                return false;
            }

            return true;
        }
        #endregion

        #region Transacciones

        public static void RegistrarUsuario(Usuario eUsuario, string[] lFunciones)
        {
            DateTime ahora = DateTime.Now;

            if (ExisteCuentaUsuario(eUsuario.Cuenta, eUsuario.Id.Value))
            {
                throw new LogicaException("La cuenta especificada ya pertenece a otro usuario", "CuentaExistente");
            }
            using (TransactionScope ts = new TransactionScope())
            {
                eUsuario.IdUsuReg = usr.Id;
                eUsuario.FecReg = ahora;
                eUsuario.IdUsuMod = usr.Id;
                eUsuario.FecMod = ahora;

                Insertar(eUsuario);

                foreach (string funcion in lFunciones)
                {
                    Insertar(new UsuarioFuncion() { IdUsuario = eUsuario.Id, CodigoFuncion = funcion });
                }
                ts.Complete();
            }
        }

        public static void ModificarUsuario(Usuario eUsuario, string[] lFunciones)
        {
            DateTime ahora = DateTime.Now;

            if (ExisteCuentaUsuario(eUsuario.Cuenta, eUsuario.Id.Value))
            {
                throw new LogicaException("La cuenta especificada ya pertenece a otro usuario", "CuentaExistente");
            }
            using (TransactionScope ts = new TransactionScope())
            {
                eUsuario.IdUsuMod = usr.Id;
                eUsuario.FecMod = ahora;
                Modificar(eUsuario, "IdUsuReg", "FecReg", "Contrasena");

                DUsuarios.EliminarFuncionesUsuario(eUsuario.Id.Value);
                foreach (string funcion in lFunciones)
                {
                    Insertar(new UsuarioFuncion() { IdUsuario = eUsuario.Id, CodigoFuncion = funcion });
                }
                ts.Complete();
            }
        }
        #endregion

        #region Procesos



        #endregion

    }

}  
