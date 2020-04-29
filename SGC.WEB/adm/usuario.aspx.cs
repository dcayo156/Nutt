using NUT.LIB.GEN.EN;
using NUT.LIB.GEN.LO;
using NUT.LIB.NEG.EN;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NUT.WEB.adm
{
    public partial class usuario : Utils.PaginaBase
    {

        const string CODIGOPANTALLA = "ADM_USU";
        public override string CODIGO_PANTALLA
        {
            get { return CODIGOPANTALLA; }
        }

        #region Declaraciones

        protected bool nuevo = false;

        #endregion

        #region Manejo de eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (RouteData.Values["id"] == null)
            {
                CargaInicial();
                Nuevo();
            }
            else
            {
                string sId = RouteData.Values["id"].ToString();
                int idUsuario;
                if (string.IsNullOrEmpty(sId) || !int.TryParse(sId, out idUsuario))
                {
                    Response.Redirect("~/solicitud_incorrecta");
                    return;
                }
                CargaInicial();
                Editar(idUsuario);
            }
        }
        protected void lvwGrupos_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                ListViewDataItem lvdi = (ListViewDataItem)e.Item;
                ListView lvwFunciones = (ListView)lvdi.FindControl("lvwFunciones");
                lvwFunciones.DataSource = DataBinder.Eval(lvdi.DataItem, "Funciones");
                lvwFunciones.DataBind();
            }
        }

        #endregion

        #region Métodos

        void CargaInicial()
        {
            ddlEstado.DataSource = Utils.ProxyCache.TraerEstadosUsuario();
            ddlEstado.DataBind();
            ddlEstado.Items.Insert(0, new ListItem("<SELECCIONE>", ""));
        }
        void Nuevo()
        {
            Master.Titulo = "NUEVO USUARIO";
            this.nuevo = true;
            trContrasena.Visible = true;
            trContrasena2.Visible = true;
            trRegistro.Visible = false;
            trUltimaModificacion.Visible = false;

            DataTable tFunciones = GUsuarios.TraerFuncionesVisibles().Tables[0];

            lvwGrupos.DataSource = from rGrupo in tFunciones.AsEnumerable()
                                   group rGrupo by new
                                   {
                                       NombreGrupoFuncion = rGrupo["NombreGrupoFuncion"].ToString(),
                                       OrdenGrupoFuncion = rGrupo.Field<int>("OrdenGrupoFuncion")
                                   }
                                       into gGrupo
                                       orderby gGrupo.Key.OrdenGrupoFuncion
                                       select new
                                       {
                                           NombreGrupoFuncion = gGrupo.Key.NombreGrupoFuncion,
                                           Funciones = from rFuncion in gGrupo
                                                       orderby rFuncion.Field<int>("Orden")
                                                       select new
                                                       {
                                                           CodigoFuncion = rFuncion["CodigoFuncion"].ToString(),
                                                           NombreFuncion = rFuncion["NombreFuncion"].ToString(),
                                                           Seleccionado = "N"
                                                       }
                                       };
            lvwGrupos.DataBind();
        }
        void Editar(int idUsuario)
        {
            Master.Titulo = "EDICIÓN DE USUARIO";
            this.nuevo = false;
            trContrasena.Visible = false;
            trContrasena2.Visible = false;
            trRegistro.Visible = true;
            trUltimaModificacion.Visible = true;

            DataSet dsUsuario = GUsuarios.TraerUsuario(idUsuario);
            DataTable tFuncionesAsignadas = dsUsuario.Tables[1];

            if (dsUsuario.Tables[0].Rows.Count == 0)
            {
                Response.Redirect("~/no_encontrado", true);
                return;
            }

            DataRow rUsuario = dsUsuario.Tables[0].Rows[0];
            txbNombre.Value = rUsuario["Nombre"].ToString();
            txbNombre.Value = rUsuario["Nombre"].ToString();
            txbApellidoPaterno.Value = rUsuario["ApellidoPaterno"].ToString();
            txbApellidoMaterno.Value = rUsuario["ApellidoMaterno"].ToString();
            txbCuenta.Value = rUsuario["Cuenta"].ToString();
            txbCorreo.Value = rUsuario["Correo"].ToString();
            ddlEstado.Value = rUsuario.Field<int>("Estado").ToString();
            lblRegistro.InnerText = rUsuario["NombreUsuReg"].ToString() + " - " + Utils.Varios.LeerUTC(rUsuario.Field<DateTime>("FecReg")).ToString("g");
            lblUltimaModificacion.InnerText = rUsuario["NombreUsuMod"].ToString() + " - " + Utils.Varios.LeerUTC(rUsuario.Field<DateTime>("FecMod")).ToString("g");

            DataTable tFunciones = GUsuarios.TraerFuncionesVisibles().Tables[0];
            tFunciones.Columns.Add("Seleccionado");
            foreach (DataRow rFuncion in tFunciones.Rows)
            {
                if (tFuncionesAsignadas.Select("CodigoFuncion='" + rFuncion["CodigoFuncion"].ToString() + "'").Length > 0)
                {
                    rFuncion["Seleccionado"] = "S";
                }
                else
                {
                    rFuncion["Seleccionado"] = "N";
                }
            }
            tFunciones.AcceptChanges();

            lvwGrupos.DataSource = from rGrupo in tFunciones.AsEnumerable()
                                   group rGrupo by new
                                   {
                                       NombreGrupoFuncion = rGrupo["NombreGrupoFuncion"].ToString(),
                                       OrdenGrupoFuncion = rGrupo.Field<int>("OrdenGrupoFuncion")
                                   }
                                       into gGrupo
                                       orderby gGrupo.Key.OrdenGrupoFuncion
                                       select new
                                       {
                                           NombreGrupoFuncion = gGrupo.Key.NombreGrupoFuncion,
                                           Funciones = from rFuncion in gGrupo
                                                       orderby rFuncion.Field<int>("Orden")
                                                       select new
                                                       {
                                                           CodigoFuncion = rFuncion["CodigoFuncion"].ToString(),
                                                           NombreFuncion = rFuncion["NombreFuncion"].ToString(),
                                                           Seleccionado = rFuncion["Seleccionado"].ToString()
                                                       }
                                       };
            lvwGrupos.DataBind();

            ClientScript.RegisterStartupScript(GetType(), "Editar", "idUsuario=" + idUsuario + ";", true);


            if (Request.QueryString["exito"] == "S")
            {
                ClientScript.RegisterStartupScript(GetType(), "Exito", "exito='S';", true);
            }
        }
        #region Ajax

        [WebMethod()]
        public static object Guardar(Usuario eUsuario, string[] lFunciones)
        {
            try
            {
                if (string.IsNullOrEmpty(usr.Codigo))
                {
                    throw new Exception("errorSesion");
                }
                if (!GUsuarios.VerificarAccesoCodigoPantallaUsuario(usr.Id, CODIGOPANTALLA))
                {
                    throw new Exception("Acceso denegado.");
                }

                List<string> lMensajes = new List<string>();

                if (GUsuarios.ExisteCuentaUsuario(eUsuario.Cuenta, eUsuario.Id.Value))
                {
                    lMensajes.Add("Ya existe otro usuario con la misma cuenta.");
                }

                int cantidadMinimaEspeciales = 0, cantidadMinimaMayusculas = 0, cantidadMinimaNumeros = 0, longitudMinima = 0, longitudMaxima = 0;

                if (eUsuario.Id.Value == -1)
                {
                    if (!GUsuarios.VerificarPoliticasContrasena(eUsuario.Contrasena, out cantidadMinimaMayusculas,
                        out cantidadMinimaNumeros, out longitudMaxima, out longitudMinima, out cantidadMinimaEspeciales))
                    {
                        if (eUsuario.Contrasena.Length < longitudMinima)
                        {
                            lMensajes.Add(String.Format("La contraseña debe tener por lo menos {0} caracteres.", longitudMinima));
                        }
                        if (eUsuario.Contrasena.Length > longitudMaxima)
                        {
                            lMensajes.Add(String.Format("La contraseña debe tener máximo hasta {0} caracteres.", longitudMaxima));
                        }
                        if (eUsuario.Contrasena.ToCharArray().Where(x => Char.IsUpper(x)).Count() < cantidadMinimaMayusculas)
                        {
                            lMensajes.Add(String.Format("La contraseña debe incluir por lo menos {0} mayúscula(s).", cantidadMinimaMayusculas));
                        }
                        if (eUsuario.Contrasena.ToArray().Where(x => Char.IsNumber(x)).Count() < cantidadMinimaNumeros)
                        {
                            lMensajes.Add(String.Format("La contraseña debe incluir por lo menos {0} número(s).", cantidadMinimaNumeros));
                        }
                        if ((eUsuario.Contrasena.ToArray().Where(x => Char.IsLetterOrDigit(x) == false).Count() < cantidadMinimaEspeciales))
                        {
                            lMensajes.Add(String.Format("La contraseña debe incluir por lo menos {0} caracter(es) especial(es).", cantidadMinimaEspeciales));
                        }
                    }
                }

                if (lMensajes.Count() > 0)
                {
                    return new
                    {
                        Estado = "error",
                        Mensajes = lMensajes.ToArray()
                    };
                }

                if (eUsuario.Id > 0)
                {
                    GUsuarios.ModificarUsuario(eUsuario, lFunciones);
                }
                else
                {
                    GUsuarios.RegistrarUsuario(eUsuario, lFunciones);
                }

                return new
                {
                    Estado = "ok",
                    Id = eUsuario.Id.Value,
                    Mod = usr.Nombre + " - " + Utils.Varios.LeerUTC(eUsuario.FecMod.Value).ToString("g")
                };
            }

            catch (Exception ex)
            {
                Utils.Inst.log.Error("Error no controlado de la aplicación", ex);
                throw;
            }
        }
        #endregion
        #endregion
    }
}
