using System;
using System.Data;
using System.Runtime.Serialization;

namespace NUT.LIB.NEG.EN
{

    [Serializable]
    [System.Xml.Serialization.XmlType(Namespace="http://www.xpectro.biz/NUT.LIB/NEG/EN")]
    public class HistoriaClinicaPatologia : XP.EN.EntidadGen<HistoriaClinicaPatologia>
    {

        public const string TABLA = "NEG.HistoriaClinicaPatologia";

        #region Constructores

        public HistoriaClinicaPatologia()
            : base()
        {
        }
        protected HistoriaClinicaPatologia(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        #endregion

        #region Propiedades

        public int? IdPatologia
        {
            get { return this.GetValor<int>("IdPatologia"); }
            set { this.SetValor("IdPatologia", value); }
        }
        public int? IdHistoriaClinica
        {
            get { return this.GetValor<int>("IdHistoriaClinica"); }
            set { this.SetValor("IdHistoriaClinica", value); }
        }
        public string Aplica
        {
            get { return this.GetValorString("Aplica"); }
            set { this.SetValor("Aplica", value); }
        }
        public string Observacion
        {
            get { return this.GetValorString("Observacion"); }
            set { this.SetValor("Observacion", value); }
        }

        #endregion

        #region Métodos

        protected override void CrearEsquema()
        {
            this.nombreTabla = TABLA;
            this.datos.Add("IdPatologia", new EntidadColumna("IdPatologia", null, Convert.ToInt32(DbType.Int32).ToString(), null, null, null, null, 1, 0));
            this.datos.Add("IdHistoriaClinica", new EntidadColumna("IdHistoriaClinica", null, Convert.ToInt32(DbType.Int32).ToString(), null, null, null, null, 1, 0));
            this.datos.Add("Aplica", new EntidadColumna("Aplica", null, Convert.ToInt32(DbType.AnsiStringFixedLength).ToString(), null, 1, null, null, 0, 0));
            this.datos.Add("Observacion", new EntidadColumna("Observacion", null, Convert.ToInt32(DbType.AnsiString).ToString(), null, 200, null, null, 0, 0));
        }

        #endregion

    }

}
