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
    using System.ComponentModel.DataAnnotations;
    
    public partial class Predmeti
    {
        public Predmeti()
        {
            this.Kategorije = new HashSet<Kategorije>();
        }
    
        public int PredmetID { get; set; }

        [Display(Name = "Naziv")]
        public string PredmetName { get; set; }
    
        public virtual ICollection<Kategorije> Kategorije { get; set; }
    }
}
