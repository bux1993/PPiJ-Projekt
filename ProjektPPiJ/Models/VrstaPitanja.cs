//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProjektPPiJ.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class VrstaPitanja
    {
        public VrstaPitanja()
        {
            this.Pitanja = new HashSet<Pitanja>();
        }
    
        public short VrstaPitanjaID { get; set; }
        public string VrstaPitanja1 { get; set; }
    
        public virtual ICollection<Pitanja> Pitanja { get; set; }
    }
}
