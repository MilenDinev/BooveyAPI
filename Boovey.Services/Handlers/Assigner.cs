namespace Boovey.Services.Handlers
{
    using System.Threading.Tasks;
    using Interfaces.IHandlers;
    using Data.Entities;
    using Data.Entities.Interfaces.IAssignables;

    public class Assigner : IAssigner
    {

        public async Task AssignBookAsync<T>(T entity, Book book) where T : class, IBookAssignable
        {
            await Task.Run(() => entity.Books.Add(book));
        }
        public async Task AssignAuthorAsync<T>(T entity, Author author) where T : class, IAuthorAssignable
        {
            await Task.Run(() => entity.Authors.Add(author));
        }
        public async Task AssignPublisherAsync<T>(T entity, Publisher publisher) where T : class, IPublisherAssignable
        {
            await Task.Run(() => entity.Publisher = publisher);
        }
        public async Task AssignGenreAsync<T>(T entity, Genre genre) where T : class, IGenreAssignable
        {
            await Task.Run(() => entity.Genres.Add(genre));
        }
    }
}
