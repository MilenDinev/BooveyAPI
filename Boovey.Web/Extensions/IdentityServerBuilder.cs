namespace Boovey.Web.Extensions
{
    using Microsoft.Extensions.DependencyInjection;
    using Middleware;

    public static class IdentityServerBuilder
    {
        public static IIdentityServerBuilder IdentityServerBuild(this IServiceCollection services)
        {
            var builder = services.AddIdentityServer((options) =>
            {
                options.EmitStaticAudienceClaim = true;
            })
             .AddInMemoryApiScopes(IdentityServerConfig.ApiScopes)
             .AddInMemoryClients(IdentityServerConfig.Clients);

            builder.AddDeveloperSigningCredential();
            builder.AddResourceOwnerValidator<PasswordValidator>();

            return builder;
        }

    }
}

