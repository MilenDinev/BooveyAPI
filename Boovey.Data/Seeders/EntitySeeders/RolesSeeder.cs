namespace Boovey.Data.Seeders.EntitySeeders
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;

    public static class RolesSeeder
    {
        public static async Task SeedRolesAsync(RoleManager<IdentityRole<int>> roleManager)
        {
            var adminRole = new IdentityRole<int>()
            {
                Name = "admin",
                NormalizedName = "admin".ToUpper(),
                ConcurrencyStamp = Guid.NewGuid().ToString("D")
            };
            await roleManager.CreateAsync(adminRole);

            var regularRole = new IdentityRole<int>()
            {
                Name = "regular",
                NormalizedName = "regular".ToUpper(),
                ConcurrencyStamp = Guid.NewGuid().ToString("D")
            };
            await roleManager.CreateAsync(regularRole);
        }
    }
}
