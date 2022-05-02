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

        protected async Task CreateEntityAsync(TEntity entity, int creatorId)
        {
            await AddEntityAsync(entity, creatorId);
            await SaveModificationAsync(entity, creatorId);
        }
        protected async Task DeleteEntityAsync(TEntity entity, int modifierId)
        {
            entity.Deleted = true;
            await SaveModificationAsync(entity, modifierId);
        }

        
        protected async Task SaveModificationAsync(TEntity entity, int modifierId)
        {
            entity.LastModifierId = modifierId;
            entity.LastModifiedOn = DateTime.UtcNow;

            await this.dbContext.SaveChangesAsync();
        }
        private async Task AddEntityAsync(TEntity entity, int creatorId)
        {
            entity.CreatorId = creatorId;
            await this.dbContext.AddAsync(entity);
        }
    }
}
