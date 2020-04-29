using System;
using System.Data;
using System.Runtime.Serialization;

namespace NUT.LIB.GEN.EN
{

    [Serializable]
    [System.Xml.Serialization.XmlType(Namespace="http://www.xpectro.biz/NUT/GEN/EN")]
    public class UsuarioFuncion : XP.EN.EntidadGen<UsuarioFuncion>
    {

        public const string TABLA = "GEN.UsuarioFuncion";

        #region Constructores

        public UsuarioFuncion()
            : base()
        {
        }
        protected UsuarioFuncion(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        #endregion

        #region Propiedades

        public int? IdUsuario
        {
            get { return this.GetValor<int>("IdUsuario"); }
            set { this.SetValor("IdUsuario", value); }
        }
        public string CodigoFuncion
        {
            get { return this.GetValorString("CodigoFuncion"); }
            set { this.SetValor("CodigoFuncion", value); }
        }

        #endregion

        #region Métodos

        protected override void CrearEsquema()
        {
            this.nombreTabla = TABLA;
            this.datos.Add("IdUsuario", new EntidadColumna("IdUsuario", null, Convert.ToInt32(DbType.Int32).ToString(), null, null, null, null, 1, 0));
            this.datos.Add("CodigoFuncion", new EntidadColumna("CodigoFuncion", null, Convert.ToInt32(DbType.AnsiString).ToString(), null, 50, null, null, 1, 0));
        }

        #endregion

    }

}
