﻿namespace Boovey.Services.AutoMapperProfiles.Shelve
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
            this.CreateMap<AddShelveModel, Shelve>()
                .ForMember(e => e.CreatedOn, m => m.MapFrom(d => DateTime.Now))
                .ForMember(e => e.LastModifiedOn, m => m.MapFrom(d => DateTime.Now));
            this.CreateMap<Shelve, CreatedShelveModel>();
            this.CreateMap<Shelve, EditedShelveModel>();
            this.CreateMap<Shelve, AddedFavoriteShelveModel>()
                .ForMember(m => m.ShelveId, e => e.MapFrom(s => s.Id))
                .ForMember(m => m.UserId, e => e.Ignore());
            this.CreateMap<Shelve, RemovedFavoriteShelveModel>()
                .ForMember(m => m.ShelveId, e => e.MapFrom(s => s.Id))
                .ForMember(m => m.UserId, e => e.Ignore());
            this.CreateMap<Shelve, ShelveListingModel>();
            this.CreateMap<Shelve, DeletedShelveModel>();
        }
    }
}

