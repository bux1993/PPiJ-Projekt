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
    
    public partial class OstvareniAchievementi
    {
        public int UserID { get; set; }
        public int AchievementID { get; set; }
        public bool AchivementOstvaren { get; set; }
    
        public virtual Achievements Achievements { get; set; }
        public virtual UserInfo UserInfo { get; set; }
    }
}
