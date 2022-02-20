namespace Boovey.Web.Extensions
{
    using Microsoft.Extensions.DependencyInjection;
    using Boovey.Services;
    using Services.Managers;
    using Services.Interfaces;

    public static class ServicesRegistrator
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<IUserManager, BooveyUserManager>();
            services.AddTransient<IUserService, UserService>();
        }
    }
}