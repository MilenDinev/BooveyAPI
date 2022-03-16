namespace Boovey.Services.Interfaces
{
    using System.Threading.Tasks;

    public interface ICountryService
    {
        Task FindCountryById(int countryId);
    }
}
