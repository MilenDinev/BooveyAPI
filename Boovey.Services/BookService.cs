namespace Boovey.Services
{
    using System;
    using System.Linq;
    using System.Globalization;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using AutoMapper;
    using Interfaces;
    using Exceptions;
    using Constants;
    using Data;
    using Data.Entities;
    using Models.Requests.BookModels;
    using Services.Interfaces.IHandlers;

    public class BookService : BaseService<Book>, IBookService
    {
        private readonly IAssigningService<Book> assigningService;
        private readonly ICountryManager countryManager;
        private readonly IMapper mapper;
        public BookService(IAssigningService<Book> assigningHandler, ICountryManager countryManager, IMapper mapper, BooveyDbContext dbContext) 
            : base(dbContext)
        {
            this.assigningService = assigningHandler;
            this.countryManager = countryManager;
            this.mapper = mapper;
        }

        public async Task<Book> CreateAsync(CreateBookModel model, int creatorId)
        {
            var isValidDate = DateTime.TryParseExact(model.PublicationDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime publicationDate);
            if (!isValidDate)
                throw new ArgumentException(ErrorMessages.InvalidPublicationDate);

            await this.countryManager.FindCountryById(model.CountryId);

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

            await this.countryManager.FindCountryById(model.CountryId);

            book.CountryId= model.CountryId; 
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

        public async Task AddFavoriteAsync(Book book, User currentUser)
        {
            await AlreadyFavoriteBookChecker(book.Id, currentUser);
            currentUser.FavoriteBooks.Add(book);

            foreach (var genre in book.Genres)
            {
                var isAlreadyFavoriteGenre = currentUser.FavoriteGenres.Any(g => g.Id == genre.Id);
                if (isAlreadyFavoriteGenre)
                {
                    currentUser.FavoriteGenres.Add(genre);
                }
            }
            await SaveModificationAsync(book, currentUser.Id);
        }
        public async Task RemoveFavoriteAsync(Book book, User currentUser)
        {
            await NotFavoriteBookChecker(book.Id, currentUser);

            currentUser.FavoriteBooks.Remove(book);

            await SaveModificationAsync(book, currentUser.Id);
        }

        public async Task<Book> AssignAuthorAsync(Book book, Author author, int modifierId)
        {
            var isAlreadyAssigned = await this.assigningService.IsAlreadyAssigned(book, author);

            if (isAlreadyAssigned)
                throw new ResourceAlreadyExistsException(string.Format(ErrorMessages.EntityAlreadyAssignedId,
                    nameof(Author), author.Id, nameof(Book), book.Id));

            await this.assigningService.AssignAsync(book, author);
            await SaveModificationAsync(book, modifierId);
            return book;
        }
        public async Task<Book> AssignGenreAsync(Book book, Genre genre, int modifierId)
        {
            var isAlreadyAssigned = await this.assigningService.IsAlreadyAssigned(book, genre);

            if (isAlreadyAssigned)
                throw new ResourceAlreadyExistsException(string.Format(ErrorMessages.EntityAlreadyAssignedId,
                    nameof(Genre), genre.Id, nameof(Book), book.Id));

            await this.assigningService.AssignAsync(book, genre);
            await SaveModificationAsync(book, modifierId);
            return book;

            
        }
        public async Task<Book> AssignPublisherAsync(Book book, Publisher publisher, int modifierId)
        {
            var isAlreadyAssigned = await this.assigningService.IsAlreadyAssigned(book, publisher);

            if (isAlreadyAssigned)
                throw new ResourceAlreadyExistsException(string.Format(ErrorMessages.EntityAlreadyAssignedId,
                    nameof(Publisher), publisher.Id, nameof(Book), book.Id));

            await this.assigningService.AssignAsync(book, publisher);
            await SaveModificationAsync(book, modifierId);
            return book;
        }


        public async Task<bool> ContainsActiveByTitleAsync(string title, ICollection<Book> books)
        {
            var contains = await Task.Run(() => books.Any(s => s.Title == title && !s.Deleted));

            return contains;
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

        private async Task<Book> FindByTitleOrDefaultAsync(string title)
        {
            var book = await this.dbContext.Books.FirstOrDefaultAsync(s => s.Title == title);
            return book;
        }
    }
}
