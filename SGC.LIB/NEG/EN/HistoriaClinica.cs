using System;
using System.Data;
using System.Runtime.Serialization;

namespace NUT.LIB.NEG.EN
{

    [Serializable]
    [System.Xml.Serialization.XmlType(Namespace="http://www.xpectro.biz/NUT.LIB/NEG/EN")]
    public class HistoriaClinica : XP.EN.EntidadGen<HistoriaClinica>
    {

        public const string TABLA = "NEG.HistoriaClinica";

        #region Constructores

        public HistoriaClinica()
            : base()
        {
        }
        protected HistoriaClinica(SerializationInfo info, StreamingContext context)
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
        public int? IdPaciente
        {
            get { return this.GetValor<int>("IdPaciente"); }
            set { this.SetValor("IdPaciente", value); }
        }
        public string Codigo
        {
            get { return this.GetValorString("Codigo"); }
            set { this.SetValor("Codigo", value); }
        }
        public DateTime? FechaElaboracion
        {
            get { return this.GetValor<DateTime>("FechaElaboracion"); }
            set { this.SetValor("FechaElaboracion", value); }
        }
        public string AntecedenteGinecologico
        {
            get { return this.GetValorString("AntecedenteGinecologico"); }
            set { this.SetValor("AntecedenteGinecologico", value); }
        }

        #endregion

        #region Métodos

        protected override void CrearEsquema()
        {
            this.nombreTabla = TABLA;
            this.datos.Add("Id", new EntidadColumna("Id", null, Convert.ToInt32(DbType.Int32).ToString(), null, null, null, null, 1, 1));
            this.datos.Add("IdPaciente", new EntidadColumna("IdPaciente", null, Convert.ToInt32(DbType.Int32).ToString(), null, null, null, null, 0, 0));
            this.datos.Add("Codigo", new EntidadColumna("Codigo", null, Convert.ToInt32(DbType.AnsiString).ToString(), null, 20, null, null, 0, 0));
            this.datos.Add("FechaElaboracion", new EntidadColumna("FechaElaboracion", null, Convert.ToInt32(DbType.DateTime).ToString(), null, null, null, null, 0, 0));
            this.datos.Add("AntecedenteGinecologico", new EntidadColumna("AntecedenteGinecologico", null, Convert.ToInt32(DbType.AnsiString).ToString(), null, 50, null, null, 0, 0));
        }

        #endregion

    }

}
