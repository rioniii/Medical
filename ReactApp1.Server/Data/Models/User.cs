using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ReactApp1.Server.Data.Models
{
    public class User
    {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }

            [Required(ErrorMessage = "Name is required")]
            public string Name { get; set; }

            [Required(ErrorMessage = "Email address is required")]
            [EmailAddress(ErrorMessage = "Invalid email address")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Password is required")]
            [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
            public string Password { get; set; } = string.Empty;

            [Required(ErrorMessage = "Confirm Password is required")]
            [MinLength(8, ErrorMessage = "Confirm Password must be at least 8 characters")]
            public string ConfirmPassword { get; set; } = string.Empty;

            [Required(ErrorMessage = "NumriKontaktues is required")]
            [Phone(ErrorMessage = "Invalid phone number")]
            public string NumriKontaktues { get; set; }
    }
    }


