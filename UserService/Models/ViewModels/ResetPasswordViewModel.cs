using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Models.ViewModels
{
    public class ResetPasswordViewModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string  GenCode { get; set; }
        [Required(ErrorMessage = "Please Enter Your Verification Code")]
        [DisplayName("Verification Code ")]
        public  string  EnterCode { get; set; }
        [Required(ErrorMessage = "Please Enter Your Password")]
        [MaxLength(50)]
        [DataType(DataType.Password, ErrorMessage = "Your Password Is Not Valid")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Repeat password")]
        [MaxLength(50)]
        [DataType(DataType.Password, ErrorMessage = "Your Password Is Not Valid")]
        [Compare("Password", ErrorMessage = "Password is different from repeating the password")]
        [Display(Name = "Repeat password")]
        public string RePassword { get; set; }
    }
}
