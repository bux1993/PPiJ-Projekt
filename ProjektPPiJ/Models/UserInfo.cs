﻿//------------------------------------------------------------------------------
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
    
    public partial class UserInfo
    {
        public UserInfo()
        {
            this.OstvareniAchievementi = new HashSet<OstvareniAchievementi>();
            this.Rezultati = new HashSet<Rezultati>();
        }
    
        public int UserID { get; set; }

        [Display(Name = "Korisničko ime")]
        public string Username { get; set; }

        [Display(Name = "Lozinka")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Ime")]
        public string Name { get; set; }

        [Display(Name = "Prezime")]
        public string LastName { get; set; }

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Admin")]
        public bool UserType { get; set; }
        public byte[] Picture { get; set; }
    
        public virtual ICollection<OstvareniAchievementi> OstvareniAchievementi { get; set; }
        public virtual ICollection<Rezultati> Rezultati { get; set; }
    }
}
