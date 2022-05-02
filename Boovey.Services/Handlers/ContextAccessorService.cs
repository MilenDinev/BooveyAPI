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
    using Castle.DynamicProxy;

    public class ContextAccessorService<TEntity> : IContextAccessorService<TEntity>
        where TEntity : class, IAccessible
    {
        private const int BUFFER_LENGHT = 5;
        private readonly BooveyDbContext dbContext;

        public ContextAccessorService(BooveyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<TEntity> GetByIdAsync(int id) // this method should be removed
        {
            var entity = await FindByIdOrDefaultAsync(id);
            if (entity == null)
                await NotExistErrorMessageThrower(entity, id);

            return entity;
        }
        public async Task<TEntity> GetActiveByIdAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity.Deleted)
                await DeletedErrorMessageThrower(entity, id);

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
        public async Task<TEntity> FindByIdOrDefaultAsync(int id)
        {
            var entity = await this.dbContext.Set<TEntity>().FirstOrDefaultAsync(e => e.Id == id); //--- only this method should stay
            return entity;
        }

        private async Task NotExistErrorMessageThrower(TEntity entity, int id)
        {
            var entityProxyType = (entity as IProxyTargetAccessor).DynProxyGetTarget().GetType().Name;
            var entityType = await Task.Run(() => entityProxyType.Remove(entityProxyType.Length - BUFFER_LENGHT));
            if (entity == null)
                throw new ResourceNotFoundException(string.Format(ErrorMessages.EntityIdDoesNotExist, entityType, id)); // - should be moved out of here
        }
        private async Task DeletedErrorMessageThrower(TEntity entity, int id)
        {
            var entityProxyType = (entity as IProxyTargetAccessor).DynProxyGetTarget().GetType().Name;
            var entityType = await Task.Run(() => entityProxyType.Remove(entityProxyType.Length - BUFFER_LENGHT));
                throw new ResourceNotFoundException(string.Format(ErrorMessages.EntityHasBeenDeleted, entityType, id));
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

