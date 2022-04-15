namespace Boovey.Services.AutoMapperProfiles.Review
{
    using System;
    using AutoMapper;
    using Data.Entities;
    using Models.Requests.ReviewModels;
    using Models.Responses.ReviewModels;

    public class ReviewMappingProfile : Profile
    {
        public ReviewMappingProfile()
        {
            this.CreateMap<CreateReviewModel, Review>()
            .ForMember(e => e.CreatedOn, m => m.MapFrom(d => DateTime.Now))
            .ForMember(e => e.LastModifiedOn, m => m.MapFrom(d => DateTime.Now));
            this.CreateMap<Review, CreatedReviewModel>();
            this.CreateMap<Review, EditedReviewModel>();
            this.CreateMap<Review, DeletedReviewModel>();
            this.CreateMap<Review, ReviewListingModel>();
        }
    }
}
