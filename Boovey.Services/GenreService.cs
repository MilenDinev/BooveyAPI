namespace Boovey.Services
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using AutoMapper;
    using Interfaces;
    using Exceptions;
    using Constants;
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

        public async Task AddFavoriteAsync(Genre genre, User currentUser)
        {
            var isAlreadyFavoriteGenre = currentUser.FavoriteGenres.Any(s => s.Id == genre.Id);
            if (isAlreadyFavoriteGenre)
                throw new ResourceAlreadyExistsException(string.Format(ErrorMessages.AlreadyFavoriteId, nameof(Genre), genre.Id));

            currentUser.FavoriteGenres.Add(genre);
            await SaveModificationAsync(genre, currentUser.Id);
        }
        public async Task RemoveFavoriteAsync(Genre genre, User currentUser)
        {
            var isFavorite = currentUser.FavoriteGenres.Any(s => s.Id == genre.Id);
            if (!isFavorite)
                throw new ResourceNotFoundException(string.Format(ErrorMessages.NotFavoriteId, nameof(Genre), genre.Id));

            currentUser.FavoriteGenres.Remove(genre);
            await SaveModificationAsync(genre, currentUser.Id);
        }

        public async Task<Genre> GetByIdAsync(int genreId)
        {
            var genre = await FindByIdOrDefaultAsync(genreId)
            ?? throw new ResourceNotFoundException(string.Format(ErrorMessages.EntityIdDoesNotExist, nameof(Genre), genreId));

            return genre;
        }
        public async Task<Genre> GetByTitleAsync(string title)
        {
            var genre = await FindByTitleOrDefaultAsync(title)
            ?? throw new ResourceNotFoundException(string.Format(ErrorMessages.EntityIdDoesNotExist, nameof(Genre), title));

            return genre;
        }
        public async Task<Genre> GetActiveByIdAsync(int genreId)
        {
            var genre = await GetByIdAsync(genreId);
            if (genre.Deleted)
                throw new ResourceNotFoundException(string.Format(ErrorMessages.EntityHasBeenDeleted, nameof(Genre)));

            return genre;
        }
        public async Task<ICollection<Genre>> GetAllActiveAsync()
        {
            var genres = await GetAllAsync();

            return genres.Where(s => !s.Deleted).ToList();
        }
        public async Task<bool> ContainsActiveByTitleAsync(string title)
        {
            var genres = await GetAllAsync();
            var contains = genres.Any(s => s.Title == title && !s.Deleted);

            return await Task.FromResult(contains);
        }

        private async Task SetTitleAsync(string title, Genre genre, int modifierId)
        {
            if (title != genre.Title)
            {
                genre.Title = title;
                await SaveModificationAsync(genre, modifierId);
            }
        }
        private async Task<Genre> FindByTitleOrDefaultAsync(string title)
        {
            var genre = await this.dbContext.Genres.FirstOrDefaultAsync(s => s.Title == title);
            return genre;
        }
    }
}
