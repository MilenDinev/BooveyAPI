namespace Boovey.Services.Managers
{
    using System.Threading.Tasks;
    using Interfaces.IManagers;
    using Data.Entities;

    public class FavoritesManager : IFavoritesManager
    {
        public async Task AddFavoriteBook(Book book, User user)
        {
           await Task.Run(() => user.FavoriteBooks.Add(book));
        }

        public async Task AddFavoriteAuthor(Author author, User user)
        {
            await Task.Run(() => user.FavoriteAuthors.Add(author));
        }

        public async Task AddFavoriteGenre(Genre genre, User user)
        {
            await Task.Run(() => user.FavoriteGenres.Add(genre));
        }

        public async Task AddFavoriteQuote(Quote quote, User user)
        {
            await Task.Run(() => user.FavoriteQuotes.Add(quote));
        }

        public async Task AddFavoriteShelve(Shelve shelve, User user)
        {
            await Task.Run(() => user.FavoriteShelves.Add(shelve));
        }

        public async Task RemoveFavoriteBook(Book book, User user)
        {
            await Task.Run(() => user.FavoriteBooks.Remove(book));
        }

        public async Task RemoveFavoriteAuthor(Author author, User user)
        {
            await Task.Run(() => user.FavoriteAuthors.Remove(author));
        }

        public async Task RemoveFavoriteGenre(Genre genre, User user)
        {
            await Task.Run(() => user.FavoriteGenres.Remove(genre));
        }

        public async Task RemoveFavoriteQuote(Quote quote, User user)
        {
            await Task.Run(() => user.FavoriteQuotes.Remove(quote));
        }

        public async Task RemoveFavoriteShelve(Shelve shelve, User user)
        {
            await Task.Run(() => user.FavoriteShelves.Remove(shelve));
        }

    }
}
