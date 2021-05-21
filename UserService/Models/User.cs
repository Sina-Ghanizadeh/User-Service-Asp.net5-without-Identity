using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Models
{
    public class User
    {
        public  int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [MaxLength(300)]
        public string  Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [RegularExpression(@"^([0-9]{11})$", ErrorMessage = "موبایل وارد شده معتبر نمی باشد")]
        public string PhoneNumber { get; set; }
        [Required]
        public DateTime RegisterDate { get; set; }
        


    }
}
