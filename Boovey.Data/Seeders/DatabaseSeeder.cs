namespace Boovey.Data.Seeders
{
    using System;
    using System.Linq;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Data.Entities;
    using SampleSeeders;

    [ExcludeFromCodeCoverage]
    public static class DatabaseSeeder
    {
        public async static Task SeedAsync(IServiceProvider applicationServices)
        {
            using (IServiceScope serviceScope = applicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<BooveyDbContext>();
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<User>>();

                await context.Database.MigrateAsync();

                if (!context.Users.Any())
                {
                    await CountriesSeeder.SeedCountriesAsync(context);
                    await RolesSeeder.SeedRolesAsync(roleManager);
                    await UsersSeeder.SeedUsersAsync(userManager);
                    await AuthorsSampleSeeder.Seed(context);
                    await GenresSampleSeeder.Seed(context);
                    await PublishersSampleSeeder.Seed(context);
                    await BooksSampleSeeder.Seed(context);
                }
            }
        }
    }
}
