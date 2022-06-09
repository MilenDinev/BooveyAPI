namespace Boovey.Services.Interfaces.IManagers
{
    using System.Threading.Tasks;
    using Data.Entities.Interfaces.IEntities;

    public interface IFavoritesManager
    {
        Task<T> AddFavoriteBook<T>(int id) where T : class, IEntity;
        Task<T> AddFavoriteAuthor<T>(int id) where T : class, IEntity;
        Task<T> AddFavoriteGenre<T>(int id) where T : class, IEntity;
        Task<T> AddFavoriteQuote<T>(int id) where T : class, IEntity;
        Task<T> AddFavoriteShelve<T>(int id) where T : class, IEntity;
    }
}
