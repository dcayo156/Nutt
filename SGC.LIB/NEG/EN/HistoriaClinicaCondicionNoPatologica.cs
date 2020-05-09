using System;
using System.Data;
using System.Runtime.Serialization;

namespace NUT.LIB.NEG.EN
{

    [Serializable]
    [System.Xml.Serialization.XmlType(Namespace="http://www.xpectro.biz/NUT.LIB/NEG/EN")]
    public class HistoriaClinicaCondicionNoPatologica : XP.EN.EntidadGen<HistoriaClinicaCondicionNoPatologica>
    {

        public const string TABLA = "NEG.HistoriaClinicaCondicionNoPatologica";

        #region Constructores

        public HistoriaClinicaCondicionNoPatologica()
            : base()
        {
        }
        protected HistoriaClinicaCondicionNoPatologica(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        #endregion

        #region Propiedades

        public int? IdHistoriaClinica
        {
            get { return this.GetValor<int>("IdHistoriaClinica"); }
            set { this.SetValor("IdHistoriaClinica", value); }
        }
        public int? IdCondicionNoPatologica
        {
            get { return this.GetValor<int>("IdCondicionNoPatologica"); }
            set { this.SetValor("IdCondicionNoPatologica", value); }
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
            this.datos.Add("IdHistoriaClinica", new EntidadColumna("IdHistoriaClinica", null, Convert.ToInt32(DbType.Int32).ToString(), null, null, null, null, 1, 0));
            this.datos.Add("IdCondicionNoPatologica", new EntidadColumna("IdCondicionNoPatologica", null, Convert.ToInt32(DbType.Int32).ToString(), null, null, null, null, 1, 0));
            this.datos.Add("Aplica", new EntidadColumna("Aplica", null, Convert.ToInt32(DbType.AnsiStringFixedLength).ToString(), null, 1, null, null, 0, 0));
            this.datos.Add("Observacion", new EntidadColumna("Observacion", null, Convert.ToInt32(DbType.AnsiString).ToString(), null, 50, null, null, 0, 0));
        }

        #endregion

    }

}
