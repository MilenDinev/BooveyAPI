﻿namespace Boovey.Services.Interfaces
{
    using System.Threading.Tasks;
    using Data.Entities;
    using Models.Requests.BookModels;

    public interface IBookService
    {
        Task<Book> CreateAsync(CreateBookModel bookModel, int creatorId);
        Task EditAsync(Book book, EditBookModel bookModel, int creatorId);
        Task DeleteAsync(Book book, int modifierId);
        Task AddFavoriteAsync(Book book, User user);
        Task RemoveFavoriteAsync(Book book, User user);
        Task<Book> AssignAuthorAsync(Book book, Author author, int modifierId);
        Task<Book> AssignGenreAsync(Book book, Genre genre, int modifierId);
        Task<Book> AssignPublisherAsync(Book book, Publisher publisher, int modifierId);
    }
}
