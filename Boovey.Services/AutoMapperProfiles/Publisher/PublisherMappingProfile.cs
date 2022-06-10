namespace Boovey.Services.AutoMapperProfiles.Publisher
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
            CreateMap<CreatePublisherModel, Publisher>()
            .ForMember(e => e.CreatedOn, m => m.MapFrom(d => DateTime.Now))
            .ForMember(e => e.LastModifiedOn, m => m.MapFrom(d => DateTime.Now));
            CreateMap<Publisher, CreatedPublisherModel>();
            CreateMap<Publisher, EditedPublisherModel>();
            CreateMap<Publisher, DeletedPublisherModel>();
            CreateMap<Publisher, PublisherListingModel>();
        }
    }
}
