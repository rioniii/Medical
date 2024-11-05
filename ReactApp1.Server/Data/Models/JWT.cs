namespace ReactApp1.Server.Data.Models
{
    public class JWT
    {
        public int Id { get; set; }

        // Foreign key for User
        public int UserId { get; set; }
        public User User { get; set; }

        public string RefreshToken { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
