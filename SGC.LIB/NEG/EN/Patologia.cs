using System;
using System.Data;
using System.Runtime.Serialization;

namespace NUT.LIB.NEG.EN
{

    [Serializable]
    [System.Xml.Serialization.XmlType(Namespace="http://www.xpectro.biz/NUT.LIB/NEG/EN")]
    public class Patologia : XP.EN.EntidadGen<Patologia>
    {

        public const string TABLA = "NEG.Patologia";

        #region Constructores

        public Patologia()
            : base()
        {
        }
        protected Patologia(SerializationInfo info, StreamingContext context)
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
        public string CargarEnHistoriaClinica
        {
            get { return this.GetValorString("CargarEnHistoriaClinica"); }
            set { this.SetValor("CargarEnHistoriaClinica", value); }
        }
        public int? Estado
        {
            get { return this.GetValor<int>("Estado"); }
            set { this.SetValor("Estado", value); }
        }
        public string EsHabitoFisiologico
        {
            get { return this.GetValorString("EsHabitoFisiologico"); }
            set { this.SetValor("EsHabitoFisiologico", value); }
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
            this.datos.Add("CargarEnHistoriaClinica", new EntidadColumna("CargarEnHistoriaClinica", null, Convert.ToInt32(DbType.AnsiStringFixedLength).ToString(), null, 1, null, null, 0, 0));
            this.datos.Add("Estado", new EntidadColumna("Estado", null, Convert.ToInt32(DbType.Int32).ToString(), null, null, null, null, 0, 0));
            this.datos.Add("EsHabitoFisiologico", new EntidadColumna("EsHabitoFisiologico", null, Convert.ToInt32(DbType.AnsiStringFixedLength).ToString(), null, 1, null, null, 0, 0));
            this.datos.Add("IdUsuReg", new EntidadColumna("IdUsuReg", null, Convert.ToInt32(DbType.Int32).ToString(), null, null, null, null, 0, 0));
            this.datos.Add("FecReg", new EntidadColumna("FecReg", null, Convert.ToInt32(DbType.DateTime).ToString(), null, null, null, null, 0, 0));
            this.datos.Add("IdUsuMod", new EntidadColumna("IdUsuMod", null, Convert.ToInt32(DbType.Int32).ToString(), null, null, null, null, 0, 0));
            this.datos.Add("FecMod", new EntidadColumna("FecMod", null, Convert.ToInt32(DbType.DateTime).ToString(), null, null, null, null, 0, 0));
        }

        #endregion

    }

}
