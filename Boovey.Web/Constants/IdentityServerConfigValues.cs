namespace Boovey.Web.Constants
{
    public static class IdentityServerConfigValues
    {
        public const string UrlAddress = @"https://localhost:5001";
        public const string ResourcesUrlAddress = @"https://localhost:5001/resources";
        public const string TokenBaseAddress = @"https://localhost:5001/connect/token";
        public const string ClientId = "boovey";
        public const string SecretValue = "secret";
        public const string GrantType = "password";
        public const bool AllowOfflineAccess = true;
    }
}
