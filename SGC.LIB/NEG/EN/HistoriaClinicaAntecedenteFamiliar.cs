using System;
using System.Data;
using System.Runtime.Serialization;

namespace NUT.LIB.NEG.EN
{

    [Serializable]
    [System.Xml.Serialization.XmlType(Namespace="http://www.xpectro.biz/NUT.LIB/NEG/EN")]
    public class HistoriaClinicaAntecedenteFamiliar : XP.EN.EntidadGen<HistoriaClinicaAntecedenteFamiliar>
    {

        public const string TABLA = "NEG.HistoriaClinicaAntecedenteFamiliar";

        #region Constructores

        public HistoriaClinicaAntecedenteFamiliar()
            : base()
        {
        }
        protected HistoriaClinicaAntecedenteFamiliar(SerializationInfo info, StreamingContext context)
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
        public int? IdHistoriaClinica
        {
            get { return this.GetValor<int>("IdHistoriaClinica"); }
            set { this.SetValor("IdHistoriaClinica", value); }
        }
        public string Descripcion
        {
            get { return this.GetValorString("Descripcion"); }
            set { this.SetValor("Descripcion", value); }
        }
        public string Serologia
        {
            get { return this.GetValorString("Serologia"); }
            set { this.SetValor("Serologia", value); }
        }
        public DateTime? Fecha
        {
            get { return this.GetValor<DateTime>("Fecha"); }
            set { this.SetValor("Fecha", value); }
        }

        #endregion

        #region Métodos

        protected override void CrearEsquema()
        {
            this.nombreTabla = TABLA;
            this.datos.Add("Id", new EntidadColumna("Id", null, Convert.ToInt32(DbType.Int32).ToString(), null, null, null, null, 1, 1));
            this.datos.Add("IdHistoriaClinica", new EntidadColumna("IdHistoriaClinica", null, Convert.ToInt32(DbType.Int32).ToString(), null, null, null, null, 0, 0));
            this.datos.Add("Descripcion", new EntidadColumna("Descripcion", null, Convert.ToInt32(DbType.AnsiString).ToString(), null, 150, null, null, 0, 0));
            this.datos.Add("Serologia", new EntidadColumna("Serologia", null, Convert.ToInt32(DbType.AnsiString).ToString(), null, 100, null, null, 0, 0));
            this.datos.Add("Fecha", new EntidadColumna("Fecha", null, Convert.ToInt32(DbType.Date).ToString(), null, null, null, null, 0, 0));
        }

        #endregion

    }

}
