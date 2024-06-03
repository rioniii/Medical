namespace ReactApp1.Server.Data.Models
{

    public class RefreshToken
    {
        public string Token { get; set; }
        public DateTime Expired { get; set; }
        public DateTime Created { get; set; }
    }
}
