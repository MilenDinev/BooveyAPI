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

        public async Task<AddedBookModel> Add(AddBookModel bookModel)
        {
            var book = await this.dbContext.Books.FirstOrDefaultAsync(b => b.ISBN == bookModel.ISBN) ?? 
                throw new ArgumentException(string.Format(ErrorMessages.EntityAlreadyExists, nameof(Book), bookModel.Title));

            book = mapper.Map<Book>(bookModel);
            var authors = mapper.Map<ICollection<Author>>(bookModel.Authors);
            var genres = mapper.Map<ICollection<Genre>>(bookModel.Genres);

            book.Authors = authors;
            book.Genres = genres;

            await this.dbContext.Books.AddAsync(book);
            await this.dbContext.SaveChangesAsync();


            return mapper.Map<AddedBookModel>(book);
        }
    }
}
