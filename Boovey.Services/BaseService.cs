﻿namespace Boovey.Services
{
    using System.Linq;
    using System.Collections.Generic;
    using Data;
    using Data.Entities.Interfaces;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using System;
    using Interfaces;

    public abstract class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class , IEntity
    {
        protected readonly BooveyDbContext dbContext;

        protected BaseService(BooveyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<TEntity> FindByIdOrDefaultAsync(int id)
        {
            var entity = await this.dbContext.Set<TEntity>().FirstOrDefaultAsync(b => b.Id == id);

            return entity;
        }
        public async Task SaveModificationAsync(TEntity entity, int modifierId)
        {
            entity.LastModifierId = modifierId;
            entity.LastModifiedOn = DateTime.UtcNow;

            await this.dbContext.SaveChangesAsync();
        }
    }
}
