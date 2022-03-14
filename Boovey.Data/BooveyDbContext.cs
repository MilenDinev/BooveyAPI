namespace Boovey.Data
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Entities;
    using Entities.Requests;

    public class BooveyDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public BooveyDbContext(DbContextOptions<BooveyDbContext> options) : base(options)
        {}

        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Publisher> Publishers { get; set; }
        public virtual DbSet<Quote> Quotes { get; set; }
        public virtual DbSet<Shelve> Shelves { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<Request> Requests { get; set; }
        public virtual DbSet<Country> Countries { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assemblyWithConfigurations = GetType().Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assemblyWithConfigurations);
            base.OnModelCreating(modelBuilder);
        }

    }
}
