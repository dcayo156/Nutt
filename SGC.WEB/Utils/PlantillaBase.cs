using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NUT.WEB.Utils
{
    public class PlantillaBase : System.Web.UI.MasterPage
    {

        public bool IgnorarVerificacionSesion = false;

        public PlantillaBase()
            : base()
        {
        }

        public Utils.UsuarioAutenticado usr
        {
            get
            {
                XP.AUT.UsuarioAutenticado aux = XP.AUT.Autenticacion.Usuario;
                if (aux.Codigo.Length == 0)
                {
                    return new Utils.UsuarioAutenticado(-1,"", "", null);
                }
                return (Utils.UsuarioAutenticado)XP.AUT.Autenticacion.Usuario;
            }
        }

        protected override void OnLoad(System.EventArgs e)
        {
            if (IgnorarVerificacionSesion || usr.Codigo.Length > 0)
            {
                base.OnLoad(e);
            }
        }

    }
}