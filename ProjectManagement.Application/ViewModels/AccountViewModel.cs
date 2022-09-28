using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Application.ViewModels
{
    public class RegisterViewModel
    {
        [Display(Name = "Full Name")]
        [Required]
        [MaxLength(100)]
        public string? FullName { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Username { get; set; }

        [Required]
        [MaxLength(100)]
        [MinLength(4)]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string? ConfirmPassword { get; set; }

        [Required]
        [MaxLength(100)]
        [EmailAddress]
        public string? Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [MaxLength(100)]
        public string? Username { get; set; }

        [Required]
        [MaxLength(100)]
        [MinLength(4)]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
