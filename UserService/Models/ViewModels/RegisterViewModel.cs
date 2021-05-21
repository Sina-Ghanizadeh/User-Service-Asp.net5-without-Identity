using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Models.ViewModels
{
    public class RegisterViewModel
    {

        [Required(ErrorMessage = "Please Enter FirstName")]
        [MaxLength(50)]
        [Display(Name = "FirstName")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please Enter LastName")]
        [MaxLength(50)]
        [Display(Name = "LastName")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please Enter PhoneNumber")]
        [Display(Name = "PhoneNumber")]
        [RegularExpression(@"^([0-9]{11})$", ErrorMessage = "Phone Number Is Not Valid ")]
        public string PhoneNumber { get; set; }

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

        [Required(ErrorMessage = "Repeat password")]
        [MaxLength(50)]
        [DataType(DataType.Password, ErrorMessage = "Your Password Is Not Valid")]
        [Compare("Password", ErrorMessage = "Password is different from repeating the password")]
        [Display(Name = "Repeat password")]
        public string RePassword { get; set; }



    }
}
