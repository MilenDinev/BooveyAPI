namespace Boovey.Services
{
    using System;
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
    using Models.Responses.GenreModels;

    public class GenreService : IGenreService
    {

        private readonly BooveyDbContext dbContext;
        private readonly IMapper mapper;

        public GenreService(BooveyDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<AddedGenreModel> AddAsync(AddGenreModel genreModel, int currentUserId)
        {
            var genre = await this.dbContext.Genres.FirstOrDefaultAsync(p => p.Title == genreModel.Title);
            if (genre != null)
                throw new ResourceAlreadyExistsException(string.Format(ErrorMessages.EntityAlreadyExists, nameof(Genre), genreModel.Title));

            genre = mapper.Map<Genre>(genreModel);

            genre.CreatorId = currentUserId;
            genre.LastModifierId = currentUserId;

            await this.dbContext.Genres.AddAsync(genre);
            await this.dbContext.SaveChangesAsync();

            return mapper.Map<AddedGenreModel>(genre);
        }

        public async Task<EditedGenreModel> EditAsync(int genreId, EditGenreModel genreModel, int currentUserId)
        {
            var genre = await GetGenreById(genreId);

            genre.Title = genreModel.Title;
            genre.LastModifierId = currentUserId;
            genre.LastModifiedOn = DateTime.UtcNow;

            await this.dbContext.SaveChangesAsync();

            return mapper.Map<EditedGenreModel>(genre);
        }

        public async Task<AddedFavoriteGenreModel> AddFavoriteGenre(int genreId, User currentUser)
        {
            var genre = await GetGenreById(genreId);

            var isAlreadyFavoriteGenre = currentUser.FavoriteGenres.FirstOrDefault(g => g.Id == genreId);

            if (isAlreadyFavoriteGenre != null)
                throw new ResourceAlreadyExistsException(string.Format(ErrorMessages.AlreadyFavoriteId, nameof(Genre), genre.Id));

            currentUser.FavoriteGenres.Add(genre);

            await dbContext.SaveChangesAsync();
            return mapper.Map<AddedFavoriteGenreModel>(genre);
        }
       
        public async Task<RemovedFavoriteGenreModel> RemoveFavoriteGenre(int genreId, User currentUser)
        {
            var genre = await GetGenreById(genreId);

            var isFavoriteGenre = currentUser.FavoriteGenres.FirstOrDefault(g => g.Id == genreId);

            if (isFavoriteGenre == null)
                throw new ResourceNotFoundException(string.Format(ErrorMessages.NotFavoriteId, nameof(Genre), genre.Id));

            currentUser.FavoriteGenres.Remove(genre);

            await dbContext.SaveChangesAsync();
            return mapper.Map<RemovedFavoriteGenreModel>(genre);
        }

        public async Task<ICollection<GenreListingModel>> GetAllGenresAsync()
        {
            var genres = await this.dbContext.Genres.ToListAsync();

            return mapper.Map<ICollection<GenreListingModel>>(genres);
        }

        private async Task<Genre> GetGenreById(int genreId)
        {
            var genre = await this.dbContext.Genres.FirstOrDefaultAsync(g => g.Id == genreId)
                ?? throw new ResourceNotFoundException(string.Format(ErrorMessages.EntityIdDoesNotExist, nameof(Genre), genreId));

            return genre;
        }
    }
}
