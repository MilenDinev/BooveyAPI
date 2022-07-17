namespace Boovey.Services.MainServices
{
    using System.Threading.Tasks;
    using AutoMapper;
    using Base;
    using Interfaces;
    using Data;
    using Data.Entities;
    using Models.Requests.AuthorModels;

    public class AuthorService : BaseService<Author>, IAuthorService
    {
        private readonly IMapper mapper;

        public AuthorService(BooveyDbContext dbContext, IMapper mapper) : base(dbContext)
        {
            this.mapper = mapper;
        }

        public async Task<Author> CreateAsync(CreateAuthorModel authorModel, int creatorId)
        {
            var author = mapper.Map<Author>(authorModel);
            await CreateEntityAsync(author, creatorId);
            return author;
        }

        public async Task EditAsync(Author author, EditAuthorModel authorModel, int modifierId)
        {
            author.CountryId = authorModel.CountryId;
            author.Fullname = authorModel.Fullname;
            author.NormalizedName = author.Fullname.ToUpper(); ;
            author.Summary = authorModel.Summary;

            await SaveModificationAsync(author, modifierId);
        }
        public async Task DeleteAsync(Author author, int modifierId)
        {
            await DeleteEntityAsync(author, modifierId);
        }
    }
}

