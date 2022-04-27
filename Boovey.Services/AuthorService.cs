namespace Boovey.Services
{
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using AutoMapper;
    using Interfaces;
    using Exceptions;
    using Constants;
    using Data;
    using Data.Entities;
    using Models.Requests.AuthorModels;

    public class AuthorService : BaseService<Author>, IAuthorService
    {
        private readonly IMapper mapper;
        private readonly ICountryManager countryManager;

        public AuthorService(BooveyDbContext dbContext, ICountryManager countryService, IMapper mapper) : base(dbContext)
        {
            this.countryManager = countryService;
            this.mapper = mapper;
        }

        public async Task<Author> CreateAsync(CreateAuthorModel authorModel, int creatorId)
        {
            await this.countryManager.FindCountryById(authorModel.CountryId);
            var author = mapper.Map<Author>(authorModel);
            await CreateEntityAsync(author, creatorId);
            return author;
        }
        public async Task EditAsync(Author author, EditAuthorModel authorModel, int modifierId)
        {
            await this.countryManager.FindCountryById(authorModel.CountryId);

            author.CountryId = authorModel.CountryId;
            author.Fullname = authorModel.Fullname;
            author.Summary = authorModel.Summary;

            await SaveModificationAsync(author, modifierId);
        }
        public async Task DeleteAsync(Author author, int modifierId)
        {
            await DeleteEntityAsync(author, modifierId);
        }

        public async Task AddFavoriteAuthorAsync(Author author, User currentUser)
        {
            var isFavorite = currentUser.FavoriteAuthors.Any(a => a.Id == author.Id);
            if (isFavorite)
                throw new ResourceAlreadyExistsException(string.Format(ErrorMessages.NotFavoriteId, nameof(Author), author.Id));

            currentUser.FavoriteAuthors.Add(author);
            await SaveModificationAsync(author, currentUser.Id);
        }
        public async Task RemoveFavoriteAuthorAsync(Author author, User currentUser)
        {
            var isFavorite = currentUser.FavoriteAuthors.Any(a => a.Id == author.Id);
            if (!isFavorite)
                throw new ResourceNotFoundException(string.Format(ErrorMessages.NotFavoriteId, nameof(Author), author.Id));

            currentUser.FavoriteAuthors.Remove(author);
            await SaveModificationAsync(author, currentUser.Id);
        }

        public async Task<Author> GetByIdAsync(int authorId)
        {
            var author = await FindByIdOrDefaultAsync(authorId)
            ?? throw new ResourceNotFoundException(string.Format(ErrorMessages.EntityIdDoesNotExist, nameof(Author), authorId));

            return author;
        }
        public async Task<Author> GetByNameAsync(string name)
        {
            var author = await FindByNameOrDefaultAsync(name)
            ?? throw new ResourceNotFoundException(string.Format(ErrorMessages.EntityIdDoesNotExist, nameof(Author), name));

            return author;
        }
        public async Task<Author> GetActiveByIdAsync(int authorId)
        {
            var author = await GetByIdAsync(authorId);
            if (author.Deleted)
                throw new ResourceNotFoundException(string.Format(ErrorMessages.EntityHasBeenDeleted, nameof(Author)));

            return author;
        }
        public async Task<ICollection<Author>> GetAllActiveAsync()
        {
            var authors = await GetAllAsync();

            return authors.Where(s => !s.Deleted).ToList();
        }
        public async Task<bool> ContainsActiveByNameAsync(string name)
        {
            var authors = await GetAllAsync();
            var contains = authors.Any(a => a.Fullname == name && !a.Deleted);

            return await Task.Run(() => contains);
        }
        private async Task<Author> FindByNameOrDefaultAsync(string name)
        {
            var author = await this.dbContext.Authors.FirstOrDefaultAsync(a => a.Fullname == name);
            return author;
        }
    }
}

