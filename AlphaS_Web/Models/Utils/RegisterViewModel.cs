using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaS_Web.Models.Utils
{
    public class RegisterViewModel
    {
        [DisplayName("Имя пользователя")]
        [Required]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Имя")]
        public string Name { get; set; }

        [DisplayName("Email")]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [DisplayName("Пароль")]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("Подтвердите пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Not same passwords.")]
        public string ConfirmPassword { get; set; }

       

    }
}
