using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SGC.WEB
{
    public partial class demo : Utils.PaginaBase
    {

        public override string CODIGO_PANTALLA
        {
            get { return "INICIO";}
        }

        #region Manejo de eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            Master.Titulo = "TÍTULO DE LA PÁGINA";
            CargaInicial();
        }

        #endregion

        #region Métodos

        void CargaInicial()
        {
            DataTable tEstadosCiviles = new DataTable();
            tEstadosCiviles.Columns.Add("val");
            tEstadosCiviles.Columns.Add("des");
            tEstadosCiviles.Rows.Add("1", "Soltero");
            tEstadosCiviles.Rows.Add("2", "Casado");
            tEstadosCiviles.Rows.Add("3", "Viudo");
            tEstadosCiviles.Rows.Add("4", "Divorciado");
            lvwEstadosCiviles.DataSource = tEstadosCiviles;
            lvwEstadosCiviles.DataBind();

            DataTable tPasatiempos = tEstadosCiviles.Clone();
            tPasatiempos.Rows.Add("1", "Fútbol");
            tPasatiempos.Rows.Add("2", "Póker");
            tPasatiempos.Rows.Add("3", "Lectura");
            tPasatiempos.Rows.Add("4", "Gimnasio");
            lvwPasatiempos.DataSource = tPasatiempos;
            lvwPasatiempos.DataBind();

            DataTable tAdjuntos = tEstadosCiviles.Clone();
            tAdjuntos.Rows.Add("12", "NombreArchivo1.docx");
            tAdjuntos.Rows.Add("65", "NombreArchivo2.docx");
            lvwAdjuntos.DataSource = tAdjuntos;
            lvwAdjuntos.DataBind();

            DataTable tComidas = tEstadosCiviles.Clone();
            tComidas.Rows.Add("1", "Majadito");
            tComidas.Rows.Add("2", "Churrasco");
            tComidas.Rows.Add("3", "Sopa de maní");
            lvwComidas.DataSource = tComidas;
            lvwComidas.DataBind();

            DataTable tFeriados = tEstadosCiviles.Clone();
            tFeriados.Rows.Add("01/01/2012", "01/01/2012");
            tFeriados.Rows.Add("23/01/2012", "23/01/2012");
            tFeriados.Rows.Add("18/02/2012", "18/02/2012");
            lvwFeriados.DataSource = tFeriados;
            lvwFeriados.DataBind();

            DataTable tRespuestas = tEstadosCiviles.Clone();
            tRespuestas.Rows.Add("Primera", "Primera");
            tRespuestas.Rows.Add("Segunda", "Segunda");
            tRespuestas.Rows.Add("Tercera", "Tercera");
            tRespuestas.Rows.Add("Cuarta", "Cuarta");
            lvwRespuestas.DataSource = tRespuestas;
            lvwRespuestas.DataBind();

            DataTable tSupervisores = tEstadosCiviles.Clone();
            tSupervisores.Rows.Add("1", "José Molina");
            tSupervisores.Rows.Add("2", "Maria Belén Arias");
            tSupervisores.Rows.Add("3", "Janira Riva Simons");
            tSupervisores.Rows.Add("4", "Gabriela Belaunde Pinto");
            tSupervisores.Rows.Add("5", "Carolina Barbery Flambury");
            JArray jSupervisores = new JArray();
            foreach (DataRow rSupervisor in tSupervisores.Rows)
            {
                JObject jSupervisor = new JObject();
                jSupervisor["v"] = rSupervisor["val"].ToString();
                jSupervisor["e"] = rSupervisor["des"].ToString();
                jSupervisores.Add(jSupervisor);
            }
            ClientScript.RegisterStartupScript(GetType(), "cargaInicial", string.Format("lSupervisores={0};",
                jSupervisores.ToString(Newtonsoft.Json.Formatting.None)), true);

            DataTable tPersonas = tEstadosCiviles.Clone();
            tPersonas.Rows.Add("101", "José Molina");
            tPersonas.Rows.Add("102", "Maria Belén Arias");
            tPersonas.Rows.Add("103", "Janira Riva Simons");
            tPersonas.Rows.Add("104", "Gabriela Belaunde Pinto");
            tPersonas.Rows.Add("105", "Carolina Barbery Flambury");
            lvwPersonas.DataSource = tPersonas;
            lvwPersonas.DataBind();

            DataTable tGerencias = tEstadosCiviles.Clone();
            tGerencias.Rows.Add("1", "Gerencia A");
            tGerencias.Rows.Add("2", "Gerencia B");
            tGerencias.Rows.Add("3", "Gerencia C");
            ddlGerencia.DataSource = tGerencias;
            ddlGerencia.DataBind();
        }

        #endregion

        #region Ajax

        [WebMethod]
        public static IEnumerable FiltrarDependientes(string filtro)
        {
            try
            {
                System.Threading.Thread.Sleep(500);
                DataTable tDependientes = new DataTable();
                tDependientes.Columns.Add("val");
                tDependientes.Columns.Add("des");
                tDependientes.Rows.Add("1", "José Molina");
                tDependientes.Rows.Add("2", "Maria Belén Arias");
                tDependientes.Rows.Add("3", "Janira Riva Simons");
                tDependientes.Rows.Add("4", "Gabriela Belaunde Pinto");
                tDependientes.Rows.Add("5", "Carolina Barbery Flambury");
                tDependientes.Rows.Add("6", "Josefa Mujía");
                tDependientes.Rows.Add("7", "Mariano Melgarejo");
                tDependientes.Rows.Add("8", "Fernando Luján");
                tDependientes.Rows.Add("9", "Gabriel Terrazas");
                tDependientes.Rows.Add("10", "Bernardo Carreras");

                return from rRep in tDependientes.AsEnumerable()
                       orderby rRep["des"].ToString()
                       where rRep["des"].ToString().ToLower().Contains(filtro.ToLower())
                       select new
                       {
                           v = rRep["val"].ToString(),
                           e = rRep["des"].ToString()
                       };
            }
            catch (Exception ex)
            {
                Utils.Inst.Error("Error no controlado en la aplicación", ex);
                throw;
            }
        }
        [WebMethod]
        public static IEnumerable TraerJefaturasxGerencias(int[] idsGerencias)
        {
            try
            {
                System.Threading.Thread.Sleep(500);
                DataTable tJefaturas = new DataTable();
                tJefaturas.Columns.Add("val");
                tJefaturas.Columns.Add("des");
                if (idsGerencias.Contains(1))
                {
                    tJefaturas.Rows.Add("11", "Jefatura A1");
                    tJefaturas.Rows.Add("12", "Jefatura A2");
                    tJefaturas.Rows.Add("13", "Jefatura A3");
                }
                if (idsGerencias.Contains(2))
                {
                    tJefaturas.Rows.Add("21", "Jefatura B1");
                    tJefaturas.Rows.Add("22", "Jefatura B2");
                    tJefaturas.Rows.Add("23", "Jefatura B3");
                }
                if (idsGerencias.Contains(3))
                {
                    tJefaturas.Rows.Add("31", "Jefatura C1");
                    tJefaturas.Rows.Add("32", "Jefatura C2");
                    tJefaturas.Rows.Add("33", "Jefatura C3");
                }


                return from rJef in tJefaturas.AsEnumerable()
                       select new
                       {
                           val = rJef["val"].ToString(),
                           des = rJef["des"].ToString()
                       };
            }
            catch (Exception ex)
            {
                Utils.Inst.Error("Error no controlado en la aplicación", ex);
                throw;
            }
        }
        [WebMethod]
        public static IEnumerable FiltrarEmpleados(string filtro, string idsGerencias, string idsJefaturas)
        {
            try
            {
                System.Threading.Thread.Sleep(500);
                DataTable tEmpleados = new DataTable();
                tEmpleados.Columns.Add("val");
                tEmpleados.Columns.Add("des");
                tEmpleados.Rows.Add("1", string.Format("José Molina (idsGerencias={0},idsJefaturas={1})", idsGerencias != null ? idsGerencias : "----", idsJefaturas != null ? idsJefaturas : "----"));
                tEmpleados.Rows.Add("2", string.Format("Maria Belén Arias (idsGerencias={0},idsJefaturas={1})", idsGerencias != null ? idsGerencias : "----", idsJefaturas != null ? idsJefaturas : "----"));
                tEmpleados.Rows.Add("3", string.Format("Janira Riva Simons (idsGerencias={0},idsJefaturas={1})", idsGerencias != null ? idsGerencias : "----", idsJefaturas != null ? idsJefaturas : "----"));
                tEmpleados.Rows.Add("4", string.Format("Gabriela Belaunde Pinto (idsGerencias={0},idsJefaturas={1})", idsGerencias != null ? idsGerencias : "----", idsJefaturas != null ? idsJefaturas : "----"));
                tEmpleados.Rows.Add("5", string.Format("Carolina Barbery Flambury (idsGerencias={0},idsJefaturas={1})", idsGerencias != null ? idsGerencias : "----", idsJefaturas != null ? idsJefaturas : "----"));
                tEmpleados.Rows.Add("6", string.Format("Josefa Mujía (idsGerencias={0},idsJefaturas={1})", idsGerencias != null ? idsGerencias : "----", idsJefaturas != null ? idsJefaturas : "----"));
                tEmpleados.Rows.Add("7", string.Format("Mariano Melgarejo (idsGerencias={0},idsJefaturas={1})", idsGerencias != null ? idsGerencias : "----", idsJefaturas != null ? idsJefaturas : "----"));
                tEmpleados.Rows.Add("8", string.Format("Fernando Luján (idsGerencias={0},idsJefaturas={1})", idsGerencias != null ? idsGerencias : "----", idsJefaturas != null ? idsJefaturas : "----"));
                tEmpleados.Rows.Add("9", string.Format("Gabriel Terrazas (idsGerencias={0},idsJefaturas={1})", idsGerencias != null ? idsGerencias : "----", idsJefaturas != null ? idsJefaturas : "----"));
                tEmpleados.Rows.Add("10", string.Format("Bernardo Carreras (idsGerencias={0},idsJefaturas={1})", idsGerencias != null ? idsGerencias : "----", idsJefaturas != null ? idsJefaturas : "----"));

                return from rRep in tEmpleados.AsEnumerable()
                       orderby rRep["des"].ToString()
                       select new
                       {
                           v = rRep["val"].ToString(),
                           e = rRep["des"].ToString()
                       };
            }
            catch (Exception ex)
            {
                Utils.Inst.Error("Error no controlado en la aplicación", ex);
                throw;
            }
        }

        #endregion
    }
}