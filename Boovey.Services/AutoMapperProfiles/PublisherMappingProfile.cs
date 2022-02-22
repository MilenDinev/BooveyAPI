namespace Boovey.Services.AutoMapperProfiles
{
    using AutoMapper;
    using Models.Requests;
    using Data.Entities.Books;

    public class PublisherMappingProfile : Profile
    {
        public PublisherMappingProfile()
        {
            this.CreateMap<AddPublisherModel, Publisher>();
        }
    }
}
