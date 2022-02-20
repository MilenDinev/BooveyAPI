namespace Boovey.Web.Constants
{
    public static class IdentityServerConfigValues
    {
        public const string TokenBaseAddress = @"https://localhost:5001/connect/token";
        public const string URLAddress = @"https://localhost:5002";
        public const string ResourcesURLAddress = @"https://localhost:5002/resources";
        public const string ClientId = "boovey";
        public const string SecretValue = "secret";
        public const string GrantType = "password";
        public const bool AllowOfflineAccess = true;
    }
}
