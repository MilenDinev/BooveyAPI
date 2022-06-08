namespace Boovey.Services.Handlers
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Constants;
    using Exceptions;
    using Data;
    using Interfaces.IHandlers;
    using Data.Entities.Interfaces.IEntities;

    public class EntityChecker : IEntityChecker
    {
        private readonly BooveyDbContext dbContext;

        public EntityChecker(BooveyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> DuplicationChecker<T>(string searchFlag, ICollection<T> collection) where T : class, IEntity 
        {
            var contains = collection.Any(e => e.NormalizedName == searchFlag.ToUpper());

            if (!contains)
                throw new ResourceAlreadyExistsException(string.Format(ErrorMessages.EntityAlreadyContained, nameof(T), searchFlag));
            return await Task.FromResult(contains);
        }
        public async Task NullableHandler<T>(T entity, string searchFlag) where T : class, IEntity
        {
            if (await Task.Run(() => entity == null))
                throw new ResourceNotFoundException(string.Format(ErrorMessages.EntityIdDoesNotExist, nameof(T), searchFlag));
        }
        public async Task DeletedHandler<T>(T entity) where T : class, IEntity
        {
            if (await Task.Run(() => entity.Deleted))
                throw new ResourceNotFoundException(string.Format(ErrorMessages.EntityHasBeenDeleted, nameof(T)));

        }
    }
}
