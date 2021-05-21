using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Models.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Please Enter Your Email Address")]
        [MaxLength(300)]
        [EmailAddress(ErrorMessage = "Email Is Not Valid")]
        [Display(Name = "Email")]
        public string Email { get; set; }

       
    }
}
