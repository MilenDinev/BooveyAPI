namespace Boovey.Services.AutoMapperProfiles.Shelve
{
    using System;
    using AutoMapper;
    using Data.Entities;
    using Models.Requests.ShelveModels;
    using Models.Responses.ShelveModels;

    public class ShelveMappingProfile : Profile
    {
        public ShelveMappingProfile()
        {
            this.CreateMap<CreateShelveModel, Shelve>()
                .ForMember(e => e.CreatedOn, m => m.MapFrom(d => DateTime.UtcNow));
            this.CreateMap<Shelve, CreatedShelveModel>();
            this.CreateMap<Shelve, EditedShelveModel>();
            this.CreateMap<Shelve, DeletedShelveModel>();
            this.CreateMap<Shelve, AddedFavoriteShelveModel>()
                .ForMember(m => m.ShelveId, e => e.MapFrom(s => s.Id))
                .ForMember(m => m.UserId, e => e.Ignore());
            this.CreateMap<Shelve, RemovedFavoriteShelveModel>()
                .ForMember(m => m.ShelveId, e => e.MapFrom(s => s.Id))
                .ForMember(m => m.UserId, e => e.Ignore());
            this.CreateMap<Shelve, ShelveListingModel>();
        }
    }
}

