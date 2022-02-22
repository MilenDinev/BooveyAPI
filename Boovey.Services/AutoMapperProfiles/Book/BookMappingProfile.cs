namespace Boovey.Services.AutoMapperProfiles.Book
{
    using AutoMapper;
    using Boovey.Data.Entities;
    using Data.Entities.Books;
    using Models.Requests;
    using Models.Responses.BookModels;

    public class BookMappingProfile : Profile
    {
        public BookMappingProfile()
        {
            this.CreateMap<AddBookModel, Book>()
            .ForMember(e => e.Country, m => m.Ignore())
            .ForMember(e => e.Publisher, m => m.Ignore())
            .ForMember(e => e.Genres, m => m.Ignore())
            .ForMember(e => e.Authors, m => m.Ignore());
            this.CreateMap<Book, AddedBookModel>()
            .ForMember(m => m.Country, e => e.MapFrom(c => c.Country.Name));
            this.CreateMap<Book, BooksListingModel>();
        }
    }
}
