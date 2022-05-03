namespace Boovey.Services
{
    using System.Linq;
    using System.Threading.Tasks;
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
    }
}

