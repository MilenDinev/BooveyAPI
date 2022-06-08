namespace Boovey.Services.Handlers
{
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.IHandlers;
    using Data.Entities;
    using Data.Entities.Interfaces.IAssignables;

    public class AssignerService : IAssignerService
    {

        public async Task AssignBookAsync(IBookAssignable entity, Book book)
        {
            await Task.Run(() => entity.Books.Add(book));
        }
        public async Task AssignAuthorAsync(IAuthorAssignable entity, Author author)
        {
            await Task.Run(() => entity.Authors.Add(author));
        }
        public async Task AssignPublisherAsync(IPublisherAssignable entity, Publisher publisher)
        {
            await Task.Run(() => entity.Publisher = publisher);
        }
        public async Task AssignGenreAsync(IGenreAssignable entity, Genre genre)
        {
            await Task.Run(() => entity.Genres.Add(genre));
        }

        // new serbvice to be made in order to handle checks
        public async Task<bool> IsAuthorAlreadyAssigned(IAuthorAssignable entity, Author author)
        {
            return await Task.Run(() => entity.Authors.Any(a => a.Id == author.Id));
        }

        public async Task<bool> IsGenreAlreadyAssigned(IGenreAssignable entity, Genre genre)
        {
            return await Task.Run(() => entity.Genres.Any(g => g.Id == genre.Id));
        }

        public async Task<bool> IsPublisherAlreadyAssigned(IPublisherAssignable entity, Publisher publisher)
        {
            return await Task.Run(() => entity.PublisherId == publisher.Id);
        }

        public async Task<bool> IsBookAlreadyAssigned(IBookAssignable entity, Book book)
        {     
            return await Task.Run(() => entity.Books.Any(b => b.Id == book.Id));
        }

    }
}
