namespace Boovey.Services.AutoMapperProfiles.Author
{
    using AutoMapper;
    using Data.Entities.Books;
    using Models.Requests;
    using Models.Responses.AuthorModels;

    public class AuthorMappingProfile : Profile
    {
        public AuthorMappingProfile()
        {
            this.CreateMap<AddAuthorModel, Author>();
            this.CreateMap<Author, AddedAuthorModel>();
        }
    }
}
