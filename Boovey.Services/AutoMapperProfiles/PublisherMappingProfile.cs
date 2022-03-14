namespace Boovey.Services.AutoMapperProfiles
{
    using System;
    using AutoMapper;
    using Data.Entities;
    using Models.Requests.PublisherModels;
    using Models.Responses.PublisherModels;

    public class PublisherMappingProfile : Profile
    {
        public PublisherMappingProfile()
        {
            this.CreateMap<AddPublisherModel, Publisher>()
            .ForMember(e => e.CreatedOn, m => m.MapFrom(d => DateTime.Now))
            .ForMember(e => e.LastModifiedOn, m => m.MapFrom(d => DateTime.Now));
            this.CreateMap<Publisher, AddedPublisherModel>();
            this.CreateMap<Publisher, EditedPublisherModel>();
            this.CreateMap<Publisher, PublisherListingModel>();
        }
    }
}
