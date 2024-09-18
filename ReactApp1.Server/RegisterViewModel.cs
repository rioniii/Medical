using System;
using System.ComponentModel.DataAnnotations;

namespace ReactApp1.Server
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(50)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Password { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; } // New property for full name

        [Required]
        public DateTime DateOfBirth { get; set; } // New property for date of birth
    }
}
