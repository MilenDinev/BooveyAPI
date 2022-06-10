namespace Boovey.Services
{
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Interfaces.IEntities;
    using Base;
    using Constants;
    using Exceptions;
    using Data;
    using Data.Entities;
    using Models.Requests.GenreModels;

    public class GenreService : BaseService<Genre>, IGenreService
    {

        private readonly IMapper mapper;

        public GenreService(BooveyDbContext dbContext, IMapper mapper) : base (dbContext)
        {
            this.mapper = mapper;
        }

        public async Task<Genre> CreateAsync(CreateGenreModel model, int creatorId)
        {
            var genre = this.mapper.Map<Genre>(model);
            await CreateEntityAsync(genre, creatorId);
            return genre;
        }
        public async Task EditAsync(Genre genre, EditGenreModel model, int modifierId)
        {
            await SetTitleAsync(model.Title, genre, modifierId);
        }
        public async Task DeleteAsync(Genre genre, int modifierId)
        {
            await DeleteEntityAsync(genre, modifierId);
        }

        private async Task SetTitleAsync(string title, Genre genre, int modifierId)
        {
            if (title != genre.Title)
            {
                genre.Title = title;
                await SaveModificationAsync(genre, modifierId);
            }
        }
    }
}
