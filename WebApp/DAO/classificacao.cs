//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApp.DAO
{
    using System;
    using System.Collections.Generic;
    
    public partial class classificacao
    {
        public classificacao()
        {
            this.customers = new HashSet<customer>();
        }
    
        public int id_classificacao { get; set; }
        public string nome { get; set; }
        public string descricao { get; set; }
    
        public virtual ICollection<customer> customers { get; set; }
    }
}
