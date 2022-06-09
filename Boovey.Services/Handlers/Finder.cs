namespace Boovey.Services.Handlers
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using Interfaces.IHandlers;
    using Data;
    using Data.Entities.Interfaces.IEntities;

    public class Finder : IFinder
    {
        private readonly BooveyDbContext dbContext;

        public Finder(BooveyDbContext dbContext)
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
       
    }
}

