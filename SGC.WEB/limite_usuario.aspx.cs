using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NUT.WEB
{
    public partial class limite_usuario : Utils.PaginaBase
    {
        const string CODIGOPANTALLA = "LIMITEUSUARIO";
        public override string CODIGO_PANTALLA
        {
            get { return CODIGOPANTALLA; }
        }

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            Master.Titulo = "LÍMITE USUARIO ALCANZADO";
        }

        #endregion

        #region Métodos

   

        #endregion

        #region Ajax

        #endregion
    }
}