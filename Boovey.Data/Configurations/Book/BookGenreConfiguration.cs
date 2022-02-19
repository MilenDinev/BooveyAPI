namespace Boovey.Data.Configurations.Book
{
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Entities;
    using Entities.Books;

    public class BookGenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.HasMany(g => g.Books)
             .WithMany(b => b.Genres)
                .UsingEntity<Dictionary<string, object>>("BooksGenres",
                x => x.HasOne<Book>().WithMany().HasForeignKey("BookId")
                      .OnDelete(DeleteBehavior.Restrict),
                x => x.HasOne<Genre>().WithMany().HasForeignKey("GenreId")
                      .OnDelete(DeleteBehavior.Restrict));
        }
    }
}
