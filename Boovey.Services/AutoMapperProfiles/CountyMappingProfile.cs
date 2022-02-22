namespace Boovey.Services.AutoMapperProfiles.Book
{
    using AutoMapper;
    using Boovey.Data.Entities;
    using Data.Entities.Books;
    using Models.Requests;
    using Models.Responses.BookModels;

    public class CountyMappingProfile : Profile
    {
        public CountyMappingProfile()
        {
            this.CreateMap<string, Country>();
        }
    }
}
