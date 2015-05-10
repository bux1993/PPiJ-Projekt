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
    
    public partial class Achievements
    {
        public Achievements()
        {
            this.OstvareniAchievementi = new HashSet<OstvareniAchievementi>();
        }
    
        public int AchievementID { get; set; }

        [Display(Name = "Naziv")]
        public string Name { get; set; }


        public string Poruka { get; set; }

        [DataType(DataType.ImageUrl)]
        public byte[] Slika { get; set; }
    
        public virtual Achievements Achievements1 { get; set; }
        public virtual Achievements Achievements2 { get; set; }
        public virtual ICollection<OstvareniAchievementi> OstvareniAchievementi { get; set; }
    }
}