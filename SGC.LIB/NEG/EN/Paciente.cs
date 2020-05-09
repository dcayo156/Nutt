using System;
using System.Data;
using System.Runtime.Serialization;

namespace NUT.LIB.NEG.EN
{

    [Serializable]
    [System.Xml.Serialization.XmlType(Namespace="http://www.xpectro.biz/NUT.LIB/NEG/EN")]
    public class Paciente : XP.EN.EntidadGen<Paciente>
    {

        public const string TABLA = "NEG.Paciente";

        #region Constructores

        public Paciente()
            : base()
        {
        }
        protected Paciente(SerializationInfo info, StreamingContext context)
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
        public int? Genero
        {
            get { return this.GetValor<int>("Genero"); }
            set { this.SetValor("Genero", value); }
        }
        public DateTime? FechaNacimiento
        {
            get { return this.GetValor<DateTime>("FechaNacimiento"); }
            set { this.SetValor("FechaNacimiento", value); }
        }
        public int? Edad
        {
            get { return this.GetValor<int>("Edad"); }
            set { this.SetValor("Edad", value); }
        }
        public int? EstadoCivil
        {
            get { return this.GetValor<int>("EstadoCivil"); }
            set { this.SetValor("EstadoCivil", value); }
        }
        public string OcupacionActual
        {
            get { return this.GetValorString("OcupacionActual"); }
            set { this.SetValor("OcupacionActual", value); }
        }
        public string Direccion
        {
            get { return this.GetValorString("Direccion"); }
            set { this.SetValor("Direccion", value); }
        }
        public int? Religion
        {
            get { return this.GetValor<int>("Religion"); }
            set { this.SetValor("Religion", value); }
        }
        public string Seguro
        {
            get { return this.GetValorString("Seguro"); }
            set { this.SetValor("Seguro", value); }
        }
        public int? GrupoSanguineo
        {
            get { return this.GetValor<int>("GrupoSanguineo"); }
            set { this.SetValor("GrupoSanguineo", value); }
        }

        #endregion

        #region Métodos

        protected override void CrearEsquema()
        {
            this.nombreTabla = TABLA;
            this.datos.Add("Id", new EntidadColumna("Id", null, Convert.ToInt32(DbType.Int32).ToString(), null, null, null, null, 1, 1));
            this.datos.Add("Nombre", new EntidadColumna("Nombre", null, Convert.ToInt32(DbType.AnsiString).ToString(), null, 50, null, null, 0, 0));
            this.datos.Add("ApellidoPaterno", new EntidadColumna("ApellidoPaterno", null, Convert.ToInt32(DbType.AnsiString).ToString(), null, 50, null, null, 0, 0));
            this.datos.Add("ApellidoMaterno", new EntidadColumna("ApellidoMaterno", null, Convert.ToInt32(DbType.AnsiString).ToString(), null, 50, null, null, 0, 0));
            this.datos.Add("Genero", new EntidadColumna("Genero", null, Convert.ToInt32(DbType.Int32).ToString(), null, null, null, null, 0, 0));
            this.datos.Add("FechaNacimiento", new EntidadColumna("FechaNacimiento", null, Convert.ToInt32(DbType.Date).ToString(), null, null, null, null, 0, 0));
            this.datos.Add("Edad", new EntidadColumna("Edad", null, Convert.ToInt32(DbType.Int32).ToString(), null, null, null, null, 0, 0));
            this.datos.Add("EstadoCivil", new EntidadColumna("EstadoCivil", null, Convert.ToInt32(DbType.Int32).ToString(), null, null, null, null, 0, 0));
            this.datos.Add("OcupacionActual", new EntidadColumna("OcupacionActual", null, Convert.ToInt32(DbType.AnsiString).ToString(), null, 30, null, null, 0, 0));
            this.datos.Add("Direccion", new EntidadColumna("Direccion", null, Convert.ToInt32(DbType.AnsiString).ToString(), null, 100, null, null, 0, 0));
            this.datos.Add("Religion", new EntidadColumna("Religion", null, Convert.ToInt32(DbType.Int32).ToString(), null, null, null, null, 0, 0));
            this.datos.Add("Seguro", new EntidadColumna("Seguro", null, Convert.ToInt32(DbType.AnsiString).ToString(), null, 30, null, null, 0, 0));
            this.datos.Add("GrupoSanguineo", new EntidadColumna("GrupoSanguineo", null, Convert.ToInt32(DbType.Int32).ToString(), null, null, null, null, 0, 0));
        }

        #endregion

    }

}
