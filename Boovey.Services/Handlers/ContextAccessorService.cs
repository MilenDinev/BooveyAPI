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

    public class ContextAccessorService<TEntity> : IContextAccessorServices<TEntity>
        where TEntity : class, IAccessible
    {
        private readonly BooveyDbContext dbContext;

        public ContextAccessorService(BooveyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            var entity = await FindByIdOrDefaultAsync(id)
            ?? throw new ResourceNotFoundException(string.Format(ErrorMessages.EntityIdDoesNotExist, nameof(TEntity).GetType().Name, id));

            return entity;
        }
        public async Task<TEntity> GetActiveByIdAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity.Deleted)
                throw new ResourceNotFoundException(string.Format(ErrorMessages.EntityHasBeenDeleted, nameof(TEntity).GetType().Name));

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
            var entity = await this.dbContext.Set<TEntity>().FirstOrDefaultAsync(e => e.Id == id);
            return entity;
        }

        //public async Task<Book> GetByTitleAsync(string title)
        //{
        //    var book = await FindByTitleOrDefaultAsync(title)
        //    ?? throw new ResourceNotFoundException(string.Format(ErrorMessages.EntityIdDoesNotExist, nameof(Book), title));

        //    return book;
        //}

    }
}

