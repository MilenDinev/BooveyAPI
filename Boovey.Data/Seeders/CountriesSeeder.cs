namespace Boovey.Data.Seeders
{
    using System.Linq;
    using System.Globalization;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Entities;

    public static class CountriesSeeder
    {
        public static async Task SeedCountriesAsync(BooveyDbContext dbcontext)
        {
            var countriesNames = CultureInfo.GetCultures(CultureTypes.SpecificCultures).Select(p => new RegionInfo(p.Name).EnglishName).Distinct().ToList();

            var countries = new List<Country>();

            foreach (var name in countriesNames)
            {
                var country = new Country
                {
                    Name = name,
                };

                countries.Add(country);
            }

            await dbcontext.Countries.AddRangeAsync(countries);
            await dbcontext.SaveChangesAsync(); 
        }

    }
}
