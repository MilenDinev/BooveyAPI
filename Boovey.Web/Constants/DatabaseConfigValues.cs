namespace Boovey.Web.Constants
{
    public static class DatabaseConfigValues
    {
        private static readonly string defaultConnectionSection = Startup.StaticConfig.GetSection("ConnectionStrings").GetSection("Default").Value;
        public static readonly string DefaultConnection = defaultConnectionSection;
    }
}
