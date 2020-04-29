using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SGC.WEB.Utils
{
    [Serializable]
    public class EmpresaContexto
    {

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string ClaveUrl { get; set; }
        public string TituloPaginas { get; set; }
        public string NombreSistema { get; set; }
        public string NombreSistemaCorreos { get; set; }

    }
}