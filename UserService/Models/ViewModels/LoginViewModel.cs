using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Models.ViewModels
{
    public class LoginViewModel
    {



        [Required(ErrorMessage = "Please Enter Your Email Address")]
        [MaxLength(300)]
        [EmailAddress(ErrorMessage = "Email Is Not Valid")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please Enter Your Password")]
        [MaxLength(50)]
        [DataType(DataType.Password, ErrorMessage = "Your Password Is Not Valid")]
        [Display(Name = "Password")]
        public string Password { get; set; }

       


    }
}
