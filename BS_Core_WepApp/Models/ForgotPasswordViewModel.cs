using System;
using System.ComponentModel.DataAnnotations;

namespace BS_Core_WepApp.Models
{
    public class ForgotPasswordViewModel: IDisposable
    {
        [Key]
        public int id { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}