namespace Boovey.Services.Managers.Interfaces
{
    using System.Threading.Tasks;
    using Data.Entities;

    public interface IFavoritesManager
    {
        Task AddFavoriteBook(Book book, User user);
        Task AddFavoriteAuthor(Author author, User user);
        Task AddFavoriteGenre(Genre genre, User user);
        Task AddFavoriteQuote(Quote quote, User user);
        Task AddFavoriteShelve(Shelve shelve, User user);

        Task RemoveFavoriteBook(Book book, User user);
        Task RemoveFavoriteAuthor(Author author, User user);
        Task RemoveFavoriteGenre(Genre genre, User user);
        Task RemoveFavoriteQuote(Quote quote, User user);
        Task RemoveFavoriteShelve(Shelve shelve, User user);

    }
}
