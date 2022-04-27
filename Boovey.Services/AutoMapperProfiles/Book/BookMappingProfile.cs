namespace Boovey.Services.AutoMapperProfiles.Book
{
    using System;
    using System.Linq;
    using AutoMapper;
    using Data.Entities;
    using Models.Requests.BookModels;
    using Models.Responses.BookModels;
    using Models.Responses.SharedModels;

    public class BookMappingProfile : Profile
    {
        public BookMappingProfile()
        {
            this.CreateMap<CreateBookModel, Book>()
                .ForMember(e => e.CreatedOn, m => m.MapFrom(d => DateTime.Now))
                .ForMember(e => e.LastModifiedOn, m => m.MapFrom(d => DateTime.Now));
            this.CreateMap<Book, CreatedBookModel>()
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
            this.CreateMap<Book, DeletedBookModel>()
                .ForMember(m => m.Authors, e => e.MapFrom(b => string.Join(", ", b.Authors.Select(a => a.Fullname))));
            this.CreateMap<Book, AddedFavoriteBookModel>()
                .ForMember(m => m.BookId, e => e.MapFrom(b => b.Id))
                .ForMember(m => m.UserId, e => e.MapFrom(b => b.FavoriteByUsers.Select(u => u.Id).LastOrDefault()));
            this.CreateMap<Book, RemovedFavoriteBookModel>()
                .ForMember(m => m.BookId, e => e.MapFrom(b => b.Id))
                .ForMember(m => m.UserId, e => e.Ignore());
            this.CreateMap<Book, AssignedBookAuthorModel>()
                .ForMember(m => m.BookId, e => e.MapFrom(b => b.Id))
                .ForMember(m => m.AuthorId, e => e.MapFrom(b => b.Authors.Select(a => a.Id).LastOrDefault()));
            this.CreateMap<Book, AssignedBookGenreModel>()
                .ForMember(m => m.BookId, e => e.MapFrom(b => b.Id))
                .ForMember(m => m.GenreId, e => e.MapFrom(b => b.Genres.Select(g => g.Id).LastOrDefault()));
            this.CreateMap<Book, AssignedBookPublisherModel>()
                .ForMember(m => m.BookId, e => e.MapFrom(b => b.Id))
                .ForMember(m => m.PublisherId, e => e.MapFrom(b => b.PublisherId));
            this.CreateMap<Book, BookListingModel>()
                .ForMember(m => m.Country, e => e.MapFrom(b => b.Country.Name))
                .ForMember(m => m.PublicationDate, e => e.MapFrom(b => b.PublicationDate.ToString("dd-MM-yyyy")))
                .ForMember(m => m.Authors, e => e.MapFrom(b => string.Join(", ", b.Authors.Select(a => a.Fullname))))
                .ForMember(m => m.Genres, e => e.MapFrom(b => string.Join(", ", b.Genres.Select(g => g.Title))))
                .ForMember(m => m.Publisher, e => e.MapFrom(p => p.Publisher.Name));
        }
    }
}
