namespace NasGrad.DBEngine
{
    public class NasGradRole
    {
        public string Id { get; set; }
        public int Type { get; set; }
        public string Description { get; set; }
    }

    public enum AuthRoleType
    {
        Admin = 1,
        Superuser = 2
    }
}