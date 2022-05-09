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
        public async Task<TEntity> GetByIdAsync(int id, string type)
        {
            var entity = await FindByIdOrDefaultAsync(id);
            await ExistenceChecker(entity, id, type);

            return entity;
        }
        public async Task<TEntity> GetActiveByIdAsync(int id, string type)
        {
            var entity = await GetByIdAsync(id, type);
            await DeletedChecker(entity, id, type);

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

        private async Task ExistenceChecker(TEntity entity, int id, string type)
        {
            if (await Task.Run(() => entity == null))
                throw new ResourceNotFoundException(string.Format(ErrorMessages.EntityIdDoesNotExist, type, id));
        }
        private async Task DeletedChecker(TEntity entity, int id, string type)
        {
            if (await Task.Run(() =>entity.Deleted))
                throw new ResourceNotFoundException(string.Format(ErrorMessages.EntityHasBeenDeleted, type, id));

        }

        //public async Task<TEntity> GetByStringAsync(string _string)
        //{
        //    var entity = await FindByNameOrDefaultAsync(_string)
        //    ?? throw new ResourceNotFoundException(string.Format(ErrorMessages.EntityHasBeenDeleted, nameof(TEntity).GetType().Name));

        //    return entity;
        //}

        //public async Task<bool> ContainsActiveByStringAsync(string _string)
        //{
        //    var contains = entities.Any(a => a._String == name && !a.Deleted);

        //    return await Task.FromResult(contains);
        //}

        //private async Task<TEntity> FindByStringOrDefaultAsync(string _string)
        //{
        //    var entity = await this.dbContext.Set<TEntity>().FirstOrDefaultAsync(e => e._String == name);
        //    return entity;
        //}

    }
}

