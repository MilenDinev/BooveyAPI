namespace Boovey.Services.Managers
{
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Interfaces;
    using Data;
    using Data.Entities;
    using Services.Constants;
    using Services.Exceptions;

    public class CountryManager : ICountryManager
    {
        private readonly BooveyDbContext dbContext;

        public CountryManager(BooveyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task FindCountryById(int countryId)
        {
            var isCountryExists = await this.dbContext.Countries.AnyAsync(c => c.Id == countryId)
                ? true
                : throw new ResourceNotFoundException(string.Format(ErrorMessages.EntityIdDoesNotExist, nameof(Country), countryId));
        }
    }
}
