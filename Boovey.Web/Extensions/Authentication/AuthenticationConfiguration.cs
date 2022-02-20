namespace Boovey.Web.Extensions.Authentication
{
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.Extensions.DependencyInjection;
    using Constants;

    public static class AuthenticationConfiguration
    {
        public static void AddAuthenticationConfig(this IServiceCollection services)
        {
            services.AddAuthentication(options =>
                {
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.Authority = IdentityServerConfigValues.UrlAddress;
                    options.Audience = IdentityServerConfigValues.ResourcesUrlAddress;
                });
        }
    }
}
