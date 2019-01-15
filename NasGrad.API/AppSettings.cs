namespace NasGrad.API
{
    public class AppSettings
    {
        public DB DB { get; set; }
    }

    public class DB
    {
        public string ServerAddress { get; set; }
        public string ServerPort { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string DbName { get; set; }
    }

}
