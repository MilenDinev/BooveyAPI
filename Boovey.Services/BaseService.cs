namespace Boovey.Services
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Data;
    using Data.Entities.Interfaces;
    using System.Collections.Generic;

    public abstract class BaseService<TEntity> where TEntity : class, IEntity
    {
        protected readonly BooveyDbContext dbContext;

        protected BaseService(BooveyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        protected async Task AddEntityAsync(TEntity entity, int creatorId)
        {
            await SetCreatorAsync(entity, creatorId);
            await this.dbContext.AddAsync(entity);
            await SaveModificationAsync(entity, creatorId);
        }
        protected async Task DeleteEntityAsync(TEntity entity, int modifierId)
        {
            entity.Deleted = true;
            await SaveModificationAsync(entity, modifierId);
        }
        protected  async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var entities = await this.dbContext.Set<TEntity>().ToArrayAsync();

            return entities;
        }
        protected async Task<TEntity> FindByIdOrDefaultAsync(int id)
        {
            var entity = await this.dbContext.Set<TEntity>().FirstOrDefaultAsync(b => b.Id == id);

            return entity;
        }
        protected async Task SaveModificationAsync(TEntity entity, int modifierId)
        {
            entity.LastModifierId = modifierId;
            entity.LastModifiedOn = DateTime.UtcNow;

            await this.dbContext.SaveChangesAsync();
        }
        private async Task SetCreatorAsync(TEntity entity, int creatorId)
        {
            entity.CreatorId = creatorId;
            await SaveModificationAsync(entity, creatorId);
        }
    }
}
