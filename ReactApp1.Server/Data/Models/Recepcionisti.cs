namespace ReactApp1.Server.Data.Models
{
    public class Recepcionisti
    {
        public int RecepcionistiId { get; set; }

        public int UserId { get; set; }
/*        public ApplicationUser User { get; set; }*/
        public List<Dhomat> Dhomat { get; set; }
    }
}
