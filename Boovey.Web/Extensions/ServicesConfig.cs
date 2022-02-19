namespace Boovey.Web.Extensions
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Data;
    using Constants;
    using Services.Managers;
    using Services.Interfaces;

    public static class ServicesConfig
    {
        public static void AddDatabase(this IServiceCollection services)
        {
            services.AddDbContext<BooveyDbContext>(options => options.UseSqlServer(DatabaseConfigValues.DefaultConnection));
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<IUserManager, BooveyUserManager>();
        }

    }
}