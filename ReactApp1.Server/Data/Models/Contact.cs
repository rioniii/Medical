using System.ComponentModel.DataAnnotations;

namespace ReactApp1.Server.Data.Models
{
    public class Contact
    {
        public int Contact_Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(30, ErrorMessage = "Name cannot exceed 30 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Subject is required")]
        [StringLength(75, ErrorMessage = "Subject cannot exceed 75 characters")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Message is required")]
        public string Message { get; set; }
    }
}
}
