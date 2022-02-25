namespace Boovey.Services.AutoMapperProfiles.Book
{
    using AutoMapper;
    using Boovey.Data.Entities;
    using Data.Entities.Books;
    using Models.Requests;
    using Models.Responses.BookModels;
    using System;
    using System.Linq;

    public class BookMappingProfile : Profile
    {
        public BookMappingProfile()
        {
            this.CreateMap<AddBookModel, Book>()
            .ForMember(e => e.Country, m => m.Ignore())
            .ForMember(e => e.CreatedOn, m => m.MapFrom(d => DateTime.Now))
            .ForMember(e => e.LastModifiedOn, m => m.MapFrom(d => DateTime.Now))
            .ForMember(e => e.Publisher, m => m.Ignore())
            .ForMember(e => e.Genres, m => m.Ignore())
            .ForMember(e => e.Authors, m => m.Ignore());
            this.CreateMap<Book, AddedBookModel>()
            .ForMember(m => m.Country, e => e.MapFrom(b => b.Country.Name))
            .ForMember(m => m.Authors, e => e.MapFrom(b => string.Join(", ", b.Authors.Select(a => a.FirstName + " " + a.LastName))))
            .ForMember(m => m.Genres, e => e.MapFrom(b => string.Join(", ", b.Genres.Select(g => g.Title))))
            .ForMember(m => m.Publisher, e => e.MapFrom(b => b.Publisher.Name))
            .ForMember(m => m.PublicationDate, e => e.MapFrom(b => b.PublicationDate.ToString("dd-MM-yyyy")));
            this.CreateMap<Book, BooksListingModel>()
            .ForMember(m => m.Country, e => e.MapFrom(b => b.Country.Name))
            .ForMember(m => m.PublicationDate, e => e.MapFrom(b => b.PublicationDate.ToString("dd-MM-yyyy")))
            .ForMember(m => m.Authors, e => e.MapFrom(b => string.Join(", ", b.Authors.Select(a => a.FirstName + " " + a.LastName))))
            .ForMember(m => m.Genres, e => e.MapFrom(b => string.Join(", ", b.Genres.Select(g => g.Title))))
            .ForMember(m => m.Publisher, e => e.MapFrom(p => p.Publisher.Name));
        }
    }
}
