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
    using Models.Responses.BookModels;

    public class BookService : BaseService<Book>, IBookService
    {
        private readonly IMapper mapper;

        public BookService(BooveyDbContext dbContext, IMapper mapper) : base(dbContext)
        {
            this.mapper = mapper;
        }

        public async Task<Book> CreateAsync(CreateBookModel model, int currentUserId)
        {

            var isValidDate = DateTime.TryParseExact(model.PublicationDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime publicationDate);
            if (!isValidDate)
                throw new ArgumentException(ErrorMessages.InvalidPublicationDate);

            var book = mapper.Map<Book>(model);

            var country = await this.dbContext.Countries.FirstOrDefaultAsync(c => c.Id == model.CountryId)
                ?? throw new ResourceAlreadyExistsException(string.Format(ErrorMessages.EntityDoesNotExist, nameof(Country), model.CountryId));

            book.CountryId = model.CountryId;

            var authors = await this.dbContext.Authors.ToListAsync();
            foreach (var authorModel in model.Authors)
            {
                var author = authors.FirstOrDefault(a => a.Fullname.ToLower() == authorModel.Fullname.ToLower() && a.CountryId == authorModel.CountryId)
                    ?? mapper.Map<Author>(authorModel);

                if (book.Authors.Any(a => a.Fullname.ToLower() == author.Fullname.ToLower() && a.CountryId == author.CountryId))
                    continue;

                book.Authors.Add(author);
            }

            var genres = await this.dbContext.Genres.ToListAsync();
            foreach (var genreModel in model.Genres)
            {
                var genre = genres.FirstOrDefault(g => g.Title.ToLower() == genreModel.Title.ToLower())
                    ?? mapper.Map<Genre>(genreModel);

                if (book.Genres.Any(g => g.Title.ToLower() == genre.Title.ToLower()))
                    continue;

                book.Genres.Add(genre);
            }

            var publisher = await this.dbContext.Publishers.FirstOrDefaultAsync(p => p.Name.ToLower() == model.Publisher.Name.ToLower())
                ?? mapper.Map<Publisher>(model.Publisher);
            book.Publisher = publisher;
            book.CreatorId = currentUserId;
            book.LastModifierId = currentUserId;

            await this.dbContext.Books.AddAsync(book);
            await this.dbContext.SaveChangesAsync();

            return book;
        }
        public async Task EditAsync(Book book, EditBookModel model, int modifierId)
        {

            var country = await this.dbContext.Countries.FirstOrDefaultAsync(c => c.Id == model.CountryId)
                ?? throw new ResourceNotFoundException(string.Format(ErrorMessages.EntityIdDoesNotExist, nameof(Country), model.CountryId));
            book.Country = country;

            var publisher = await this.dbContext.Publishers.FirstOrDefaultAsync(p => p.Id == model.PublisherId)
                ?? throw new ResourceNotFoundException(string.Format(ErrorMessages.EntityIdDoesNotExist, nameof(Publisher), model.PublisherId));

            var isValidDate = DateTime.TryParseExact(model.PublicationDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime publicationDate);

            if (!isValidDate)
                throw new ArgumentException(ErrorMessages.InvalidPublicationDate);

            book.Description = model.Description;
            book.Pages = model.Pages;
            book.PublicationDate = publicationDate;
            book.Publisher = publisher;
            await SetTitleAsync(model.Title, book, modifierId);

        }
        public async Task DeleteAsync(Book book, int modifierId)
        {
            book.Deleted = true;
            await SaveModificationAsync(book, modifierId);
        }

        public async Task<AddedFavoriteBookModel> AddFavorite(int bookId, User currentUser)
        {
            await AlreadyFavoriteBookChecker(bookId, currentUser);

            var book = await GetByIdAsync(bookId);

            currentUser.FavoriteBooks.Add(book);

            foreach (var genre in book.Genres)
            {
                var isAlreadyFavoriteGenre = currentUser.FavoriteGenres.Any(g => g.Id == genre.Id);
                if (isAlreadyFavoriteGenre)
                {
                    currentUser.FavoriteGenres.Add(genre);
                }
            }

            await dbContext.SaveChangesAsync();
            return mapper.Map<AddedFavoriteBookModel>(book);
        }
        public async Task<RemovedFavoriteBookModel> RemoveFavorite(int bookId, User currentUser)
        {
            await NotFavoriteBookChecker(bookId, currentUser);

            var book = await GetByIdAsync(bookId);

            currentUser.FavoriteBooks.Remove(book);

            await dbContext.SaveChangesAsync();
            return mapper.Map<RemovedFavoriteBookModel>(book);
        }

        public async Task<Book> GetActiveByIdAsync(int bookId)
        {
            var book = await GetByIdAsync(bookId);
            if (book.Deleted)
                throw new ResourceNotFoundException(string.Format(ErrorMessages.EntityHasBeenDeleted, nameof(Book)));

            return book;
        }
        public async Task<Book> GetByIdAsync(int bookId)
        {
            var book = await FindByIdOrDefaultAsync(bookId)
            ?? throw new ResourceNotFoundException(string.Format(ErrorMessages.EntityIdDoesNotExist, nameof(Book), bookId));

            return book;
        }
        public async Task<Book> GetByTitleAsync(string title)
        {
            var book = await FindByTitleOrDefaultAsync(title)
            ?? throw new ResourceNotFoundException(string.Format(ErrorMessages.EntityIdDoesNotExist, nameof(Book), title));

            return book;
        }

        public async Task<ICollection<Book>> GetAllActiveAsync()
        {
            var books = await GetAllAsync();

            return books.Where(s => !s.Deleted).ToList();
        }
        public async Task<bool> ContainsActiveByTitleAsync(string title, ICollection<Book> books)
        {
            var contains = books.Any(s => s.Title == title && !s.Deleted);

            return await Task.Run(() => contains);
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

        public async Task<Book> AssignAuthorAsync(Book book, int authorId, int modifierId)
        {
            var author = await GetAuthorById(authorId);
            await AlreadyAssignedAuthorChecker(book, author);
            book.Authors.Add(author);
            await SaveModificationAsync(book, modifierId);
            return book;
        }
        public async Task<Book> AssignGenreAsync(Book book, int genreId, int modifierId)
        {
            var genre = await GetGenreById(genreId);
            await AlreadyAssignedGenreChecker(book, genre);
            book.Genres.Add(genre);
            await SaveModificationAsync(book, modifierId);
            return book;
        }
        public async Task<Book> AssignPublisherAsync(Book book, int publisherId, int modifierId)
        {
            var publisher = await GetPublisherById(publisherId);
            await AlreadyAssignedPublisherChecker(book, publisher.Id);
            book.PublisherId = publisher.Id;
            await SaveModificationAsync(book, modifierId);
            return book;
        }

        private async Task SetTitleAsync(string title, Book book, int modifierId)
        {
            if (title != book.Title)
            {
                book.Title = title;
                await SaveModificationAsync(book, modifierId);
            }
        }
        private async Task<Book> FindByTitleOrDefaultAsync(string title)
        {
            var book = await this.dbContext.Books.FirstOrDefaultAsync(s => s.Title == title);
            return book;
        }
        private async Task AlreadyAssignedAuthorChecker(Book book, Author author)
        {
            var IsAuthorAlreadyAssigned = book.Authors.Contains(author)
                ? throw new ResourceAlreadyExistsException(string.Format(ErrorMessages.EntityAlreadyAssignedId, nameof(Author), author.Id, nameof(Book), book.Id)) : false;

            await Task.Delay(300);
        }
        private async Task AlreadyAssignedGenreChecker(Book book, Genre genre)
        {
            var isGenreAlreadyAssigned = book.Genres.Contains(genre)
                ? throw new ResourceAlreadyExistsException(string.Format(ErrorMessages.EntityAlreadyAssignedId, nameof(Genre), genre.Id, nameof(Book), book.Id)) : false;

            await Task.Delay(300);
        }
        private async Task AlreadyAssignedPublisherChecker(Book book, int publisherId)
        {
            var isPublisherAlreadyAssigned = book.PublisherId == publisherId
                ? throw new ResourceAlreadyExistsException(string.Format(ErrorMessages.EntityAlreadyAssignedId, nameof(Publisher), publisherId, nameof(Book), book.Id)) : false;

            await Task.Delay(300);
        }

        private async Task<Author> GetAuthorById(int authorId)
        {
            var author = await this.dbContext.Authors.FirstOrDefaultAsync(b => b.Id == authorId)
                ?? throw new ResourceNotFoundException(string.Format(ErrorMessages.EntityIdDoesNotExist, nameof(Author), authorId));

            return author;
        }
        private async Task<Genre> GetGenreById(int genreId)
        {
            var genre = await this.dbContext.Genres.FirstOrDefaultAsync(g => g.Id == genreId)
                ?? throw new ResourceNotFoundException(string.Format(ErrorMessages.EntityIdDoesNotExist, nameof(Genre), genreId));

            return genre;
        }
        private async Task<Publisher> GetPublisherById(int publisherId)
        {
            var publisher = await this.dbContext.Publishers.FirstOrDefaultAsync(g => g.Id == publisherId)
                ?? throw new ResourceNotFoundException(string.Format(ErrorMessages.EntityIdDoesNotExist, nameof(Publisher), publisherId));

            return publisher;
        }
    }
}
