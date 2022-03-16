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
    using Models.Responses.SharedModels;

    public class BookService : IBookService
    {
        private readonly BooveyDbContext dbContext;
        private readonly IMapper mapper;

        public BookService(BooveyDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<AddedBookModel> AddAsync(AddBookModel bookModel, int currentUserId)
        {
            await AlreadyExistBookByTitleChecker(bookModel.Title);

            var isValidDate = DateTime.TryParseExact(bookModel.PublicationDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime publicationDate);
            if (!isValidDate)
                throw new ArgumentException(ErrorMessages.InvalidPublicationDate);

            var book = mapper.Map<Book>(bookModel);

            var country = await this.dbContext.Countries.FirstOrDefaultAsync(c => c.Id == bookModel.CountryId)
                ?? throw new ResourceAlreadyExistsException(string.Format(ErrorMessages.EntityDoesNotExist, nameof(Country), bookModel.CountryId));

            book.CountryId = bookModel.CountryId;

            var authors = await this.dbContext.Authors.ToListAsync();
            foreach (var authorModel in bookModel.Authors)
            {
                var author = authors.FirstOrDefault(a => a.Fullname.ToLower() == authorModel.Fullname.ToLower() && a.CountryId == authorModel.CountryId)
                    ?? mapper.Map<Author>(authorModel);

                if (book.Authors.Any(a => a.Fullname.ToLower() == author.Fullname.ToLower() && a.CountryId == author.CountryId))
                    continue;

                book.Authors.Add(author);
            }

            var genres = await this.dbContext.Genres.ToListAsync();
            foreach (var genreModel in bookModel.Genres)
            {
                var genre = genres.FirstOrDefault(g => g.Title.ToLower() == genreModel.Title.ToLower())
                    ?? mapper.Map<Genre>(genreModel);

                if (book.Genres.Any(g => g.Title.ToLower() == genre.Title.ToLower()))
                    continue;

                book.Genres.Add(genre);
            }

            var publisher = await this.dbContext.Publishers.FirstOrDefaultAsync(p => p.Name.ToLower() == bookModel.Publisher.Name.ToLower())
                ?? mapper.Map<Publisher>(bookModel.Publisher);
            book.Publisher = publisher;
            book.CreatorId = currentUserId;
            book.LastModifierId = currentUserId;

            await this.dbContext.Books.AddAsync(book);
            await this.dbContext.SaveChangesAsync();

            return mapper.Map<AddedBookModel>(book);
        }
        public async Task<EditedBookModel> EditAsync(int bookId, EditBookModel bookModel, int currentUserId)
        {
            var book = await GetBookById(bookId);

            var country = await this.dbContext.Countries.FirstOrDefaultAsync(c => c.Id == bookModel.CountryId)
                ?? throw new ResourceNotFoundException(string.Format(ErrorMessages.EntityIdDoesNotExist, nameof(Country), bookModel.CountryId));
            book.Country = country;

            var publisher = await this.dbContext.Publishers.FirstOrDefaultAsync(p => p.Id == bookModel.PublisherId)
                ?? throw new ResourceNotFoundException(string.Format(ErrorMessages.EntityIdDoesNotExist, nameof(Publisher), bookModel.PublisherId));

            var isValidDate = DateTime.TryParseExact(bookModel.PublicationDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime publicationDate);

            if (!isValidDate)
                throw new ArgumentException(ErrorMessages.InvalidPublicationDate);

            book.Title = bookModel.Title;
            book.Description = bookModel.Description;
            book.Pages = bookModel.Pages;
            book.PublicationDate = publicationDate;
            book.Publisher = publisher;
            book.LastModifierId = currentUserId;
            book.LastModifiedOn = DateTime.UtcNow;

            await this.dbContext.SaveChangesAsync();

            return mapper.Map<EditedBookModel>(book);
        }
        public async Task<AddedFavoriteBookModel> AddFavoriteBook(int bookId, User currentUser)
        {
            await AlreadyFavoriteBookChecker(bookId, currentUser);

            var book = await GetBookById(bookId);

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
        public async Task<RemovedFavoriteBookModel> RemoveFavoriteBook(int bookId, User currentUser)
        {
            await NotFavoriteBookChecker(bookId, currentUser);

            var book = await GetBookById(bookId);

            currentUser.FavoriteBooks.Remove(book);

            await dbContext.SaveChangesAsync();
            return mapper.Map<RemovedFavoriteBookModel>(book);
        }
        public async Task<ICollection<BookListingModel>> GetAllBooksAsync()
        {
            var books = await this.dbContext.Books.ToListAsync();

            return mapper.Map<ICollection<BookListingModel>>(books);
        }

        public async Task<AssignedAuthorBookModel> AssignAuthorAsync(int bookId, int authorId, int modifierId)
        {
            var book = await GetBookById(bookId);
            var author = await GetAuthorById(authorId);
            await AlreadyAssignedAuthorChecker(book, author);

            book.Authors.Add(author);
            book.LastModifiedOn = DateTime.UtcNow;
            book.LastModifierId = modifierId;

            await this.dbContext.SaveChangesAsync();

            return this.mapper.Map<AssignedAuthorBookModel>(book);
        }
        public async Task<AssignedBookGenreModel> AssignGenreAsync(int bookId, int genreId, int modifierId)
        {
            var book = await GetBookById(bookId);
            var genre = await GetGenreById(genreId);

            await AlreadyAssignedGenreChecker(book, genre);

            book.Genres.Add(genre);
            book.LastModifiedOn = DateTime.UtcNow;
            book.LastModifierId = modifierId;

            await this.dbContext.SaveChangesAsync();

            return this.mapper.Map<AssignedBookGenreModel>(book);
        }
        public async Task<AssignedPublisherBookModel> AssignPublisherAsync(int bookId, int publisherId, int modifierId)
        {
            var book = await GetBookById(bookId);
            var publisher = await GetPublisherById(publisherId);

            await AlreadyAssignedPublisherChecker(book, publisherId);

            book.PublisherId = publisher.Id;
            book.LastModifiedOn = DateTime.UtcNow;
            book.LastModifierId = modifierId;

            await this.dbContext.SaveChangesAsync();

            return this.mapper.Map<AssignedPublisherBookModel>(book);
        }

        private async Task AlreadyExistBookByTitleChecker(string bookTitle)
        {
            var IsbookAlreadyExists = await this.dbContext.Books.AnyAsync(b => b.Title == bookTitle)
                ? throw new ResourceAlreadyExistsException(string.Format(ErrorMessages.EntityAlreadyExists, nameof(Book), bookTitle)) : false;

            await Task.Delay(300);
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
        private async Task<Book> GetBookById(int bookId)
        {
            var book = await this.dbContext.Books.FirstOrDefaultAsync(b => b.Id == bookId)
                ?? throw new ResourceNotFoundException(string.Format(ErrorMessages.EntityIdDoesNotExist, nameof(Book), bookId));

            return book;
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
    }
}
