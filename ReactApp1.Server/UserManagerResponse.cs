namespace ReactApp1.Server
{
    public class UserManagerResponse
    {
        public string Message { get; set;}
        public bool isSucces  { get; set;}
        public IEnumerable<string> Errors { get; set;}

    }
}
