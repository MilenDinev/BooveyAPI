namespace Boovey.Services.AutoMapperProfiles.Quote
{
    using System;
    using System.Linq;
    using AutoMapper;
    using Data.Entities;
    using Models.Requests.QuoteModels;
    using Models.Responses.QuoteModels;

    public class QuoteMappingProfile : Profile
    {
        public QuoteMappingProfile()
        {
            this.CreateMap<CreateQuoteModel, Quote>()
                .ForMember(e => e.CreatedOn, m => m.MapFrom(d => DateTime.Now))
                .ForMember(e => e.LastModifiedOn, m => m.MapFrom(d => DateTime.Now));
            this.CreateMap<Quote, CreatedQuoteModel>();
            this.CreateMap<Quote, EditedQuoteModel>();
            this.CreateMap<Quote, DeletedQuoteModel>();
            this.CreateMap<Quote, AddedFavoriteQuoteModel>()
                .ForMember(m => m.QuoteId, e => e.MapFrom(q => q.Id))
                .ForMember(m => m.UserId, e => e.MapFrom(q => q.FavoriteByUsers.Select(u => u.Id).LastOrDefault()));
            this.CreateMap<Quote, RemovedFavoriteQuoteModel>()
                .ForMember(m => m.QuoteId, e => e.MapFrom(q => q.Id))
                .ForMember(m => m.UserId, e => e.Ignore());
        }

    }
}
