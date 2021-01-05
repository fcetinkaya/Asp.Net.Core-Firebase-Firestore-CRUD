using System;
using System.ComponentModel.DataAnnotations;

namespace BS_Core_WepApp.Models
{
    public class LoginViewModel: IDisposable
    {
        [Key]
        public int id { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}