using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUT.LIB.GEN.AD;
using System.Data;
using NUT.LIB.GEN.LO;
using NUT.LIB.GEN.EN;
using XP.LO;

namespace NUT.TEST.GEN.LO
{
    [TestClass]
    public class GUsuariosTest
    {
        #region Inicializacion

        [TestInitialize]
        public void ConfigurarPruebasUnitarias()
        {
            XP.AUT.Autenticacion.Autenticar(101, "epereira", "", "");
            DUsuarios.bd.EjecutarProc("TEST.InicializarPruebasUnitarias");
        }

        #endregion

        #region Consultas

        [TestMethod()]
        [TestCategory("CU101 - Iniciar sesión")]
        public void AutenticarUsuarioTest()
        {
            DataRow rUsuario = GUsuarios.AutenticarUsuario("epereira", "adm1").Tables[0].Rows[0];
            Assert.IsTrue(rUsuario.Field<int>("Id") == 101);
            Assert.IsTrue(rUsuario["Cuenta"].ToString() == "epereira");
            Assert.IsTrue(rUsuario["Nombres"].ToString() == "Erwin");

            Assert.IsTrue(GUsuarios.AutenticarUsuario("epereira", "xx").Tables[0].Rows.Count == 0);

            DUsuarios.bd.Ejecutar(string.Format("UPDATE GEN.Usuario SET Estado={0} WHERE Id=102",
                LIB.GEN.EN.Constantes.USUARIO_ESTADO_INACTIVO));
            Assert.IsTrue(GUsuarios.AutenticarUsuario("kcaballero", "adm1").Tables[0].Rows.Count == 0);
        }

        [TestMethod()]
        [TestCategory("CU101 - Iniciar sesión")]
        public void TraerMenuUsuarioTest()
        {
            Assert.IsTrue(GUsuarios.TraerMenuUsuario(101).Tables[0].Rows.Count == 5);
        }

        //[TestMethod()]
        //[TestCategory("CU101 - Iniciar sesión")]
        //public void BuscarUsuariosTest()
        //{
        //    Assert.IsTrue(GUsuarios.BuscarUsuarios(null, null, -1).Tables[0].Rows.Count == 2);
        //    Assert.IsTrue(GUsuarios.BuscarUsuarios("erw pere", null, -1).Tables[0].Rows.Count == 1);
        //}

        #endregion

        
    }
}
