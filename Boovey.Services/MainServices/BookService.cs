namespace Boovey.Services.MainServices
{
    using System;
    using System.Linq;
    using System.Globalization;
    using System.Threading.Tasks;
    using AutoMapper;
    using Base;
    using Constants;
    using Exceptions;
    using Interfaces;
    using Data;
    using Data.Entities;
    using Models.Requests.BookModels;

    public class BookService : BaseService<Book>, IBookService
    {
        private readonly IMapper mapper;

        public BookService(BooveyDbContext dbContext, IMapper mapper)
            : base(dbContext)
        {
            this.mapper = mapper;
        }

        public async Task<Book> CreateAsync(CreateBookModel model, int creatorId)
        {
            var isValidDate = DateTime.TryParseExact(model.PublicationDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime publicationDate);
            if (!isValidDate)
                throw new ArgumentException(ErrorMessages.InvalidPublicationDate);

            var book = mapper.Map<Book>(model);

            await CreateEntityAsync(book, creatorId);
            return book;
        }
        public async Task EditAsync(Book book, EditBookModel model, int modifierId)
        {
            var isValidDate = DateTime.TryParseExact(model.PublicationDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime publicationDate);
            if (!isValidDate)
                throw new ArgumentException(ErrorMessages.InvalidPublicationDate);
            book.PublicationDate = publicationDate;

            book.CountryId = model.CountryId;
            book.Description = model.Description;
            book.Pages = model.Pages;
            book.Publisher.Id = model.PublisherId;

            await SetTitleAsync(model.Title, book);

            await SaveModificationAsync(book, modifierId);
        }
        public async Task DeleteAsync(Book book, int modifierId)
        {
            book.Deleted = true;
            await SaveModificationAsync(book, modifierId);
        }

        private async Task SetTitleAsync(string title, Book book)
        {
            if (title != book.Title)
            {
                await Task.Run(() => book.Title = title);
            }
        }
        private async Task AlreadyFavoriteBookChecker(int bookId, User user)
        {
            var isAlreadyFavoriteBook = user.FavoriteBooks.Any(b => b.Id == bookId)
                ? throw new ResourceAlreadyExistsException(string.Format(ErrorMessages.AlreadyFavoriteId, nameof(Book), bookId)) : false;

            await Task.Delay(300);
        }
        private async Task NotFavoriteBookChecker(int bookId, User user)
        {
            var favoriteBook = user.FavoriteBooks.Any(b => b.Id == bookId)
                ? true : throw new ResourceNotFoundException(string.Format(ErrorMessages.NotFavoriteId, nameof(Book), bookId));

            await Task.Delay(300);
        }
    }
}
