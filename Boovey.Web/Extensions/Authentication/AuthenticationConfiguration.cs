namespace Boovey.Web.Extensions.Authentication
{
    using Boovey.Web.Constants;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.Extensions.DependencyInjection;

    public static class AuthenticationConfiguration
    {
        public static void AddAuthentication(this IServiceCollection services)
        {

            services.AddAuthentication(options =>
                {
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.Authority = IdentityServerConfigValues.URLAddress;
                    options.Audience = IdentityServerConfigValues.ResourcesURLAddress;
                });
        }
    }
}
