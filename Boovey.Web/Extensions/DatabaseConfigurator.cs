namespace Boovey.Web.Extensions
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Data;
    using Constants;

    public static class DatabaseConfigurator
    {
        public static void ConfigureDatabase(this IServiceCollection services)
        {
            services.AddDbContext<BooveyDbContext>(options => options.UseSqlServer(DatabaseConfigValues.DefaultConnection));
        }
    }
}
