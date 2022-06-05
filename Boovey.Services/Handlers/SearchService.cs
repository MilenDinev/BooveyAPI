namespace Boovey.Services.Handlers
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using Constants;
    using Exceptions;
    using Interfaces.IHandlers;
    using Data;
    using Data.Entities.Interfaces.IEntities;

    public class SearchService : ISearchService
    {
        private readonly BooveyDbContext dbContext;

        public SearchService(BooveyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<T> FindByIdOrDefaultAsync<T>(int id) where T : class, IEntity
        {
            var entity = await this.dbContext.Set<T>().FirstOrDefaultAsync(e => e.Id == id);
            return entity;
        }
        public async Task<T> FindByStringOrDefaultAsync<T>(string stringValue) where T : class, IEntity
        {
            var entity = await this.dbContext.Set<T>().FirstOrDefaultAsync(e => e.NormalizedName == stringValue.ToUpper());
            return entity;
        }
        public async Task<T> GetByIdAsync<T>(int id) where T : class, IEntity
        {
            var entity = await FindByIdOrDefaultAsync<T>(id);
            await ExistenceChecker(entity, id.ToString());

            return entity;
        }
        public async Task<T> GetByStringAsync<T>(string stringValue) where T : class, IEntity
        {
            var entity = await FindByStringOrDefaultAsync<T>(stringValue);
            await ExistenceChecker(entity, stringValue);

            return entity;
        }
        public async Task<T> GetActiveByIdAsync<T>(int id) where T : class, IEntity
        {
            var entity = await GetByIdAsync<T>(id);
            await DeletedChecker(entity.Deleted, id.ToString());

            return entity;
        }
        public async Task<T> GetActiveByStringAsync<T>(string stringValue) where T : class, IEntity
        {
            var entity = await GetByStringAsync<T>(stringValue);
            await DeletedChecker(entity.Deleted, stringValue);

            return entity;
        }

        public async Task<ICollection<T>> GetAllAsync<T>() where T : class, IEntity
        {
            var entities = await this.dbContext.Set<T>().ToArrayAsync();

            return entities;
        }
        public async Task<ICollection<T>> GetAllActiveAsync<T>() where T : class, IEntity
        {
            var entities = await GetAllAsync<T>();

            return entities.Where(s => !s.Deleted).ToList();
        }


        public async Task<bool> ContainsActiveByStringAsync<T>(string stringValue) where T : class, IEntity
        {
            var entities = await this.dbContext.Set<T>().ToListAsync();
            return await ContainsActiveByStringAsync<T>(stringValue, entities);
        }

        public async Task<bool> ContainsActiveByStringAsync<T>(string stringValue, ICollection<T> collection) where T : class, IEntity
        {
            var contains = collection.Any(e => e.NormalizedName == stringValue.ToUpper() && !e.Deleted);

            return await Task.FromResult(contains);
        }

        private async Task ExistenceChecker(IEntity entity, string searchFlag)
        {
            if (await Task.Run(() => entity == null))
                throw new ResourceNotFoundException(string.Format(ErrorMessages.EntityIdDoesNotExist, "test", searchFlag));
        }
        private async Task DeletedChecker(bool deleted, string searchFlag)
        {
            if (await Task.Run(() => deleted))
                throw new ResourceNotFoundException(string.Format(ErrorMessages.EntityHasBeenDeleted, "test", searchFlag));

        }

       
    }
}

