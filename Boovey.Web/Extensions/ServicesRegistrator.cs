namespace Boovey.Web.Extensions
{
    using Microsoft.Extensions.DependencyInjection;
    using System.Reflection;
    using Constants;
    using Boovey.Services;
    using Services.Managers;
    using Services.Interfaces;

    public static class ServicesRegistrator
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.Load(AutoMapperConfigValues.Assembly));
            services.AddTransient<IUserManager, BooveyUserManager>();
            services.AddTransient<IUserService, UserService>();
            services.AddHttpContextAccessor();
        }
    }
}