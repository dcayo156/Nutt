using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace NUT.WEB.Utils
{
    [Serializable]
    public class UsuarioAutenticado : XP.AUT.UsuarioAutenticado
    {

        #region Variables

        DataSet dsMenu;
        string nombreCorto;

        #endregion

        #region Propiedades

        public DataSet Menu
        {
            get
            {
                return dsMenu;
            }
        }
        public string Nombre
        {
            get
            {
                return nombreCorto;
            }
        }

        #endregion

        #region Constructor

        public UsuarioAutenticado(int id, string codigo, string nombre, DataSet dsMenu)
            : base(id, codigo, "", nombre)
        {
            this.dsMenu = dsMenu;
            this.nombreCorto = nombre;
        }

        #endregion

    }
}