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
    using Constants;
    using Data;
    using Data.Entities;
    using Data.Entities.Books;
    using Models.Requests;
    using Models.Responses.BookModels;

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
            var book = await this.dbContext.Books.FirstOrDefaultAsync(b => b.Title == bookModel.Title);
            if (book != null)
                throw new ArgumentException(string.Format(ErrorMessages.EntityAlreadyExists, nameof(Book), bookModel.Title));

            book = mapper.Map<Book>(bookModel);

            var country = await this.dbContext.Countries.FirstOrDefaultAsync(c => c.Name == bookModel.Country)
                ?? throw new ArgumentException(string.Format(ErrorMessages.EntityDoesNotExist, nameof(Country), bookModel.Country));
            book.Country = country;

            var authors = new List<Author>();
            foreach (var authorModel in bookModel.Authors)
            {
                var author = await this.dbContext.Authors.FirstOrDefaultAsync(a => a.Fullname.ToLower() == authorModel.Fullname.ToLower()) 
                    ?? mapper.Map<Author>(authorModel);
                authors.Add(author);
            }

            book.Authors = authors;

            var genres = new List<Genre>();
            foreach (var genreModel in bookModel.Genres)
            {
                var genre = await this.dbContext.Genres.FirstOrDefaultAsync(g => g.Title.ToLower() == genreModel.Title.ToLower()) 
                    ?? mapper.Map<Genre>(genreModel);
                genres.Add(genre);
            }

            book.Genres = genres;

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
            var book = await this.dbContext.Books.FirstOrDefaultAsync(b => b.Id == bookId)
               ?? throw new ArgumentException(string.Format(ErrorMessages.EntityDoesNotExist, nameof(Book), bookModel.Title));

            var country = await this.dbContext.Countries.FirstOrDefaultAsync(c => c.Name == bookModel.Country)
                ?? throw new ArgumentException(string.Format(ErrorMessages.EntityDoesNotExist, nameof(Country), bookModel.Country));
            book.Country = country;

            var publisher = await this.dbContext.Publishers.FirstOrDefaultAsync(p => p.Id == bookModel.PublisherId)
                ?? throw new ArgumentException(string.Format(ErrorMessages.EntityDoesNotExist, nameof(Publisher), bookModel.PublisherId));

            var isValidDate = DateTime.TryParseExact(bookModel.PublicationDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime publicationDate);

            // TOO FIX DATETIME INPUT IN ADD BOOK METHOD
            if (!isValidDate)
            {
                throw new ArgumentException("Please provide valid publication date!");
            }

            book.Title = bookModel.Title;
            book.Description = bookModel.Description;
            book.Pages = bookModel.Pages;
            book.PublicationDate = publicationDate;
            book.Publisher = publisher;
            book.LastModifierId = currentUserId;

            await this.dbContext.SaveChangesAsync();

            return mapper.Map<EditedBookModel>(book);
        }

        public async Task<AddedFavoriteBookModel> AddFavoriteBook(int bookId, User currentUser)
        {
            var book = await this.dbContext.Books.FirstOrDefaultAsync(b => b.Id == bookId)
                ?? throw new ArgumentException(string.Format(ErrorMessages.EntityDoesNotExist, nameof(Book)));

            var isAlreadyFavoriteBook = currentUser.FavoriteBooks.FirstOrDefault(b => b.Id == bookId);

            if (isAlreadyFavoriteBook != null)
                throw new ArgumentException(string.Format(ErrorMessages.IsAlreadyFavorite, nameof(Book), book.Title));

            currentUser.FavoriteBooks.Add(book);

            foreach (var genre in book.Genres)
            {
                var isAlreadyFavoriteGenre = currentUser.FavoriteGenres.FirstOrDefault(g => g.Id == genre.Id);
                if (isAlreadyFavoriteGenre == null)
                {
                    currentUser.FavoriteGenres.Add(genre);
                }

            }

            await dbContext.SaveChangesAsync();
            return mapper.Map<AddedFavoriteBookModel>(book);
        }

        public async Task<RemovedFavoriteBookModel> RemoveFavoriteBook(int bookId, User currentUser)
        {
            var book = await this.dbContext.Books.FirstOrDefaultAsync(b => b.Id == bookId)
                ?? throw new ArgumentException(string.Format(ErrorMessages.EntityDoesNotExist, nameof(Book)));

            var isAlreadyFavoriteBook = currentUser.FavoriteBooks.FirstOrDefault(b => b.Id == bookId);

            if (isAlreadyFavoriteBook == null)
                throw new ArgumentException(string.Format(ErrorMessages.IsNotFavorite, nameof(Book), book.Title));

            currentUser.FavoriteBooks.Remove(book);

            await dbContext.SaveChangesAsync();
            return mapper.Map<RemovedFavoriteBookModel>(book);
        }

        public async Task<ICollection<BooksListingModel>> GetAllBooksAsync()
        {
            var books = await this.dbContext.Books.ToListAsync();

            return mapper.Map<ICollection<BooksListingModel>>(books);
        }
    }
}
