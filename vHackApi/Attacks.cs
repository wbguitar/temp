//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace vHackApi
{
    using System;
    using System.Collections.Generic;
    
    public partial class Attacks
    {
        public System.DateTime Dt { get; set; }
        public string IP { get; set; }
        public Nullable<long> MoneyOwned { get; set; }
        public Nullable<long> MoneyWon { get; set; }
        public Nullable<long> RepWon { get; set; }
    
        public virtual IPs IPs { get; set; }
    }
}
