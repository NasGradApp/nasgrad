namespace NasGrad.DBEngine
{
    public class NasGradRole: BaseItem
    {
        public int Type { get; set; }
        public string Description { get; set; }
    }

    public enum AuthRoleType
    {
        Admin = 1,
        Superuser = 2,
        ReadOnly = 3,
    }
}