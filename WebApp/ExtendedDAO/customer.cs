using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.DAO
{
    [Serializable]
    public partial class customer
    {
        public string sexoNome {
            get {
                if (sexo == 2) return "M";
                else return "F";
            }
        }

        public DateTime fromLastPurchase { get; set; }
        public DateTime toLastPurchase { get; set; }

        public string classificacaoNome { get; set; }
        public string usuarioNome { get; set; }
        public string cidadeNome { get; set; }
        public string regiaoNome { get; set; }
    }
}