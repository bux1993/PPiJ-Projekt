using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ProjektPPiJ.Models
{

    public class LoginModel
    {
        [Required]
        [Display(Name = "Korisničko ime")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Lozinka")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterModel
    {
        [Required]
        [Display(Name = "Korisničko ime")]
        public string Username { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Lozinka")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Ponovi Lozinku")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Ime")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Prezime")]
        public string LastName { get; set; }

        public UserInfo toUserInfo() {
            UserInfo ret = new UserInfo();
            ret.Username = this.Username;
            ret.Password = this.Password;
            ret.Name = this.Name;
            ret.LastName = this.LastName;
            ret.Email = this.Email;
            return ret;
        }

    }

    

}