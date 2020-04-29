using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using XP.LO;

namespace NUT.WEB
{
    public partial class inicio : Utils.PaginaBase
    {
        public override string CODIGO_PANTALLA
        {
            get { return "INICIO"; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            CargaInicial();
        }

        #region Métodos

        void CargaInicial()
        {
            Master.Titulo = "INICIO";
            
        }
        #endregion

        #region Ajax
       
        #endregion
    }
}