namespace Boovey.Services.Handlers
{
    using System.Threading.Tasks;
    using Constants;
    using Exceptions;
    using Data.Entities.Interfaces.IEntities;
    using Interfaces.IHandlers;

    public class Validator : IValidator
    {
        private readonly ISearchService searchService;
        private readonly IEntityChecker entityChecker;

        public Validator(ISearchService searchService, IEntityChecker entityChecker)
        {
            this.searchService = searchService;
            this.entityChecker = entityChecker;
        }

        public async Task<bool> ValidateEntityAsync<T>(T entity, string flag) where T : class, IEntity
        {
            await this.entityChecker.NullableHandler<T>(entity, flag);
            await this.entityChecker.DeletedHandler<T>(entity);

            return true;
        }

        public async Task<bool> ValidateUniqueEntityAsync<T>(T entity) where T : class, IEntity
        {
            if (await Task.Run(() => entity != null))
                throw new ResourceAlreadyExistsException(string.Format(ErrorMessages.EntityAlreadyContained, nameof(T)));

            return true;
        }
    }
}
