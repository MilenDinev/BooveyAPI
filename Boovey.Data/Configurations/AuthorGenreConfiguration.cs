namespace Boovey.Data.Configurations
{
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Entities;
    using Entities.Books;

    public class AuthorGenreConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.HasMany(a => a.Genres)
             .WithMany(g => g.Authors)
                .UsingEntity<Dictionary<string, object>>("AuthorGenres",
                x => x.HasOne<Genre>().WithMany().HasForeignKey("GenreId")
                      .OnDelete(DeleteBehavior.Restrict),
                x => x.HasOne<Author>().WithMany().HasForeignKey("AuthorId")
                      .OnDelete(DeleteBehavior.Restrict));
        }
    }
}

