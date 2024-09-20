namespace ReactApp1.Server.Data.Models
{
    public class EmailConfiguration
    {
        public String From { get; set; } = null!; 
        public String SmtpServer { get; set; } = null!;
        public int Port { get; set; }
        public String UserName { get; set; } = null!;
        public String Password { get; set; } = null!;

    }
}
