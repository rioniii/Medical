using Microsoft.AspNetCore.Identity;

namespace ReactApp1.Server.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public int UserId {  get; set; }
        public string Emri { get; set; }
        public string Mbiemri { get; set; }
        public string Ditelindja { get; set; }
        public string Gjinia { get; set; }

        // Contact information
        public string Adresa { get; set; }
        public string Qyteti { get; set; }
        public string Shteti { get; set; }
        public string Kontakti { get; set; }

        // Healthcare-specific information
        public string Specializimi { get; set; } 
        public string Departmenti { get; set; } 


    }
}