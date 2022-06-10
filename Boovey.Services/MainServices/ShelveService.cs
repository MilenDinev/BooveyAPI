namespace Boovey.Services.MainServices
{
    using System.Threading.Tasks;
    using AutoMapper;
    using Base;
    using Interfaces;
    using Data;
    using Data.Entities;
    using Models.Requests.ShelveModels;

    public class ShelveService : BaseService<Shelve>, IShelveService
    {
        private readonly IMapper mapper;

        public ShelveService(BooveyDbContext dbContext, IMapper mapper) : base(dbContext)
        {
            this.mapper = mapper;
        }

        public async Task<Shelve> CreateAsync(CreateShelveModel model, int creatorId)
        {
            var shelve = mapper.Map<Shelve>(model);

            await CreateEntityAsync(shelve, creatorId);

            return shelve;
        }
        public async Task EditAsync(Shelve shelve, EditShelveModel model, int modifierId)
        {
            await SetTitleAsync(model.Title, shelve, modifierId);
        }
        public async Task DeleteAsync(Shelve shelve, int modifierId)
        {
            await DeleteEntityAsync(shelve, modifierId);
        }

        private async Task SetTitleAsync(string title, Shelve shelve, int modifierId)
        {
            if (title != shelve.Title)
            {
                shelve.Title = title;
                await SaveModificationAsync(shelve, modifierId);
            }
        }
    }
}
