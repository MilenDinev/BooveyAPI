namespace Boovey.Services.AutoMapperProfiles.Book
{
    using System;
    using System.Linq;
    using AutoMapper;
    using Data.Entities.Books;
    using Models.Requests;
    using Models.Responses.BookModels;
    using Models.Responses.SharedModels;

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
                .ForMember(m => m.Authors, e => e.MapFrom(b => string.Join(", ", b.Authors.Select(a => a.Fullname))))
                .ForMember(m => m.Genres, e => e.MapFrom(b => string.Join(", ", b.Genres.Select(g => g.Title))))
                .ForMember(m => m.Publisher, e => e.MapFrom(b => b.Publisher.Name))
                .ForMember(m => m.PublicationDate, e => e.MapFrom(b => b.PublicationDate.ToString("dd-MM-yyyy")))
                .ForMember(m => m.Country, e => e.MapFrom(b => b.Country.Name));
            this.CreateMap<Book, EditedBookModel>()
                .ForMember(m => m.Authors, e => e.MapFrom(b => string.Join(", ", b.Authors.Select(a => a.Fullname))))
                .ForMember(m => m.Genres, e => e.MapFrom(b => string.Join(", ", b.Genres.Select(g => g.Title))))
                .ForMember(m => m.PublicationDate, e => e.MapFrom(b => b.PublicationDate.ToString("dd-MM-yyyy")))
                .ForMember(m => m.Publisher, e => e.MapFrom(b => b.Publisher.Name))
                .ForMember(m => m.Country, e => e.MapFrom(b => b.Country.Name));
            this.CreateMap<Book, AddedFavoriteBookModel>()
                .ForMember(m => m.Username, e => e.MapFrom(b => b.FavoriteByUsers.Select(u => u.UserName).LastOrDefault()));
            this.CreateMap<Book, RemovedFavoriteBookModel>();
            this.CreateMap<Book, AssignedAuthorBookModel>()
                .ForMember(m => m.BookId, e => e.MapFrom(b => b.Id))
                .ForMember(m => m.AuthorId, e => e.MapFrom(b => b.Authors.Select(a => a.Id).LastOrDefault()));
            this.CreateMap<Book, AssignedBookGenreModel>()
                .ForMember(m => m.BookId, e => e.MapFrom(b => b.Id))
                .ForMember(m => m.GenreId, e => e.MapFrom(b => b.Genres.Select(g => g.Id).LastOrDefault()));
            this.CreateMap<Book, AssignedPublisherBookModel>()
                .ForMember(m => m.PublisherId, e => e.MapFrom(b => b.PublisherId))
                .ForMember(m => m.BookId, e => e.MapFrom(b => b.Id));
            this.CreateMap<Book, BooksListingModel>()
                .ForMember(m => m.Country, e => e.MapFrom(b => b.Country.Name))
                .ForMember(m => m.PublicationDate, e => e.MapFrom(b => b.PublicationDate.ToString("dd-MM-yyyy")))
                .ForMember(m => m.Authors, e => e.MapFrom(b => string.Join(", ", b.Authors.Select(a => a.Fullname))))
                .ForMember(m => m.Genres, e => e.MapFrom(b => string.Join(", ", b.Genres.Select(g => g.Title))))
                .ForMember(m => m.Publisher, e => e.MapFrom(p => p.Publisher.Name));
        }
    }
}
