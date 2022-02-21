namespace Boovey.Services.AutoMapperProfiles.Book
{
    using AutoMapper;
    using Data.Entities.Books;
    using Models.Requests;
    using Models.Responses.BookModels;

    public class BookMappingProfile : Profile
    {
        public BookMappingProfile()
        {
            this.CreateMap<AddBookModel, Book>();
            this.CreateMap<Book, AddedBookModel>();
        }
    }
}
