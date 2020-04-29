using System;
using System.Data;
using System.Runtime.Serialization;

namespace NUT.LIB.GEN.EN
{

    [Serializable]
    [System.Xml.Serialization.XmlType(Namespace="http://www.xpectro.biz/NUT.LIB/GEN/EN")]
    public class Usuario : XP.EN.EntidadGen<Usuario>
    {

        public const string TABLA = "GEN.Usuario";

        #region Constructores

        public Usuario()
            : base()
        {
        }
        protected Usuario(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        #endregion

        #region Propiedades

        public int? Id
        {
            get { return this.GetValor<int>("Id"); }
            set { this.SetValor("Id", value); }
        }
        public string Nombre
        {
            get { return this.GetValorString("Nombre"); }
            set { this.SetValor("Nombre", value); }
        }
        public string ApellidoPaterno
        {
            get { return this.GetValorString("ApellidoPaterno"); }
            set { this.SetValor("ApellidoPaterno", value); }
        }
        public string ApellidoMaterno
        {
            get { return this.GetValorString("ApellidoMaterno"); }
            set { this.SetValor("ApellidoMaterno", value); }
        }
        public string Cuenta
        {
            get { return this.GetValorString("Cuenta"); }
            set { this.SetValor("Cuenta", value); }
        }
        public string Contrasena
        {
            get { return this.GetValorString("Contrasena"); }
            set { this.SetValor("Contrasena", value); }
        }
        public string Correo
        {
            get { return this.GetValorString("Correo"); }
            set { this.SetValor("Correo", value); }
        }
        public int? Estado
        {
            get { return this.GetValor<int>("Estado"); }
            set { this.SetValor("Estado", value); }
        }
        public int? IdUsuReg
        {
            get { return this.GetValor<int>("IdUsuReg"); }
            set { this.SetValor("IdUsuReg", value); }
        }
        public DateTime? FecReg
        {
            get { return this.GetValor<DateTime>("FecReg"); }
            set { this.SetValor("FecReg", value); }
        }
        public int? IdUsuMod
        {
            get { return this.GetValor<int>("IdUsuMod"); }
            set { this.SetValor("IdUsuMod", value); }
        }
        public DateTime? FecMod
        {
            get { return this.GetValor<DateTime>("FecMod"); }
            set { this.SetValor("FecMod", value); }
        }

        #endregion

        #region Métodos

        protected override void CrearEsquema()
        {
            this.nombreTabla = TABLA;
            this.datos.Add("Id", new EntidadColumna("Id", null, Convert.ToInt32(DbType.Int32).ToString(), null, null, null, null, 1, 1));
            this.datos.Add("Nombre", new EntidadColumna("Nombre", null, Convert.ToInt32(DbType.AnsiString).ToString(), null, 50, null, null, 0, 0));
            this.datos.Add("ApellidoPaterno", new EntidadColumna("ApellidoPaterno", null, Convert.ToInt32(DbType.AnsiString).ToString(), null, 30, null, null, 0, 0));
            this.datos.Add("ApellidoMaterno", new EntidadColumna("ApellidoMaterno", null, Convert.ToInt32(DbType.AnsiString).ToString(), null, 30, null, null, 0, 0));
            this.datos.Add("Cuenta", new EntidadColumna("Cuenta", null, Convert.ToInt32(DbType.AnsiString).ToString(), null, 20, null, null, 0, 0));
            this.datos.Add("Contrasena", new EntidadColumna("Contrasena", null, Convert.ToInt32(DbType.AnsiString).ToString(), null, 50, null, null, 0, 0));
            this.datos.Add("Correo", new EntidadColumna("Correo", null, Convert.ToInt32(DbType.AnsiString).ToString(), null, 20, null, null, 0, 0));
            this.datos.Add("Estado", new EntidadColumna("Estado", null, Convert.ToInt32(DbType.Int32).ToString(), null, null, null, null, 0, 0));
            this.datos.Add("IdUsuReg", new EntidadColumna("IdUsuReg", null, Convert.ToInt32(DbType.Int32).ToString(), null, null, null, null, 0, 0));
            this.datos.Add("FecReg", new EntidadColumna("FecReg", null, Convert.ToInt32(DbType.DateTime).ToString(), null, null, null, null, 0, 0));
            this.datos.Add("IdUsuMod", new EntidadColumna("IdUsuMod", null, Convert.ToInt32(DbType.Int32).ToString(), null, null, null, null, 0, 0));
            this.datos.Add("FecMod", new EntidadColumna("FecMod", null, Convert.ToInt32(DbType.DateTime).ToString(), null, null, null, null, 0, 0));
        }

        #endregion

    }

}
