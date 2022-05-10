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
    using Data.Entities.Interfaces;

    public class SearchService<TEntity> : ISearchService<TEntity>
        where TEntity : class, ISearchable
    {
        private readonly BooveyDbContext dbContext;

        public SearchService(BooveyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<TEntity> FindByIdOrDefaultAsync(int id)
        {
            var entity = await this.dbContext.Set<TEntity>().FirstOrDefaultAsync(e => e.Id == id);
            return entity;
        }
        public async Task<TEntity> FindByStringOrDefaultAsync(string stringValue)
        {
            var entity = await this.dbContext.Set<TEntity>().FirstOrDefaultAsync(e => e.NormalizedName == stringValue.ToUpper());
            return entity;
        }
        public async Task<TEntity> GetByIdAsync(int id, string type)
        {
            var entity = await FindByIdOrDefaultAsync(id);
            await ExistenceChecker(entity, id.ToString(), type);

            return entity;
        }
        public async Task<TEntity> GetByStringAsync(string stringValue, string type)
        {
            var entity = await FindByStringOrDefaultAsync(stringValue);
            await ExistenceChecker(entity, stringValue, type);

            return entity;
        }
        public async Task<TEntity> GetActiveByIdAsync(int id, string type)
        {
            var entity = await GetByIdAsync(id, type);
            await DeletedChecker(entity.Deleted, id.ToString(), type);

            return entity;
        }
        public async Task<TEntity> GetActiveByStringAsync(string stringValue, string type)
        {
            var entity = await GetByStringAsync(stringValue, type);
            await DeletedChecker(entity.Deleted, stringValue, type);

            return entity;
        }
        public async Task<ICollection<TEntity>> GetAllAsync()
        {
            var entities = await this.dbContext.Set<TEntity>().ToArrayAsync();

            return entities;
        }
        public async Task<ICollection<TEntity>> GetAllActiveAsync()
        {
            var entities = await GetAllAsync();

            return entities.Where(s => !s.Deleted).ToList();
        }


        public async Task<bool> ContainsActiveByStringAsync(string stringValue)
        {
            var entities = await this.dbContext.Set<TEntity>().ToListAsync();
            return await ContainsActiveByStringAsync(stringValue, entities);
        }

        public async Task<bool> ContainsActiveByStringAsync(string stringValue, ICollection<TEntity> collection)
        {
            var contains = collection.Any(e => e.NormalizedName == stringValue.ToUpper() && !e.Deleted);

            return await Task.FromResult(contains);
        }

        private async Task ExistenceChecker(TEntity entity, string searchFlag, string type)
        {
            if (await Task.Run(() => entity == null))
                throw new ResourceNotFoundException(string.Format(ErrorMessages.EntityIdDoesNotExist, type, searchFlag));
        }
        private async Task DeletedChecker(bool deleted, string searchFlag, string type)
        {
            if (await Task.Run(() => deleted))
                throw new ResourceNotFoundException(string.Format(ErrorMessages.EntityHasBeenDeleted, type, searchFlag));

        }

    }
}

