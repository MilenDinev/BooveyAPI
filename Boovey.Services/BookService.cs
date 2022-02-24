namespace Boovey.Services
{
    using AutoMapper;
    using Interfaces;
    using Data;
    using System.Threading.Tasks;
    using Boovey.Models.Responses.BookModels;
    using Boovey.Models.Requests;
    using Microsoft.EntityFrameworkCore;
    using Boovey.Services.Constants;
    using System;
    using Boovey.Data.Entities.Books;
    using System.Collections.Generic;
    using Boovey.Data.Entities;

    public class BookService : IBookService
    {
        private readonly BooveyDbContext dbContext;
        private readonly IMapper mapper;
        public BookService(BooveyDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<AddedBookModel> AddAsync(AddBookModel bookModel)
        {
            var book = await this.dbContext.Books.FirstOrDefaultAsync(b => b.ISBN == bookModel.ISBN && b.Title == bookModel.Title);
            if (book != null)
                throw new ArgumentException(string.Format(ErrorMessages.EntityAlreadyExists, nameof(Book), bookModel.Title));

            book = mapper.Map<Book>(bookModel);

            var country = await this.dbContext.Countries.FirstOrDefaultAsync(c => c.Name == bookModel.Country);
            book.Country = country;

            var authors = new List<Author>();
            foreach (var authorModel in bookModel.Authors)
            {
                var author = await this.dbContext.Authors.FirstOrDefaultAsync(a => a.FirstName.ToLower() == authorModel.FirstName.ToLower()
                && a.LastName.ToLower() == authorModel.LastName.ToLower()) ?? mapper.Map<Author>(authorModel);
                authors.Add(author);
            }

            book.Authors = authors;

            var genres = new List<Genre>();
            foreach (var genreModel in bookModel.Genres)
            {
                var genre = await this.dbContext.Genres.FirstOrDefaultAsync(g => g.Title.ToLower() == genreModel.Title.ToLower()) ?? mapper.Map<Genre>(genreModel);
                genres.Add(genre);
            }

            book.Genres = genres;

            var publisher = await this.dbContext.Publishers.FirstOrDefaultAsync(p => p.Name.ToLower() == bookModel.Publisher.Name.ToLower()) ?? mapper.Map<Publisher>(bookModel.Publisher);
            book.Publisher = publisher;

            await this.dbContext.Books.AddAsync(book);
            await this.dbContext.SaveChangesAsync();

            return mapper.Map<AddedBookModel>(book);
        }

        public async Task<ICollection<BooksListingModel>> GetAllBooksAsync()
        {
            var books = await this.dbContext.Books.ToListAsync();

            return mapper.Map<ICollection<BooksListingModel>>(books);
        }
    }
}
