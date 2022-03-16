namespace Boovey.Services.Interfaces
{
    using System.Threading.Tasks;

    public interface ICountryManager
    {
        Task FindCountryById(int countryId);
    }
}
