namespace Boovey.Data.Configurations.Books
{
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Entities;

    public class BookShelveConfiguration : IEntityTypeConfiguration<Shelve>
    {
        public void Configure(EntityTypeBuilder<Shelve> builder)
        {
            builder.HasMany(s => s.Books)
             .WithMany(b => b.Shelves)
                .UsingEntity<Dictionary<string, object>>("BookShelves",
                x => x.HasOne<Book>().WithMany().HasForeignKey("BookId")
                      .OnDelete(DeleteBehavior.Restrict),
                x => x.HasOne<Shelve>().WithMany().HasForeignKey("ShelveId")
                      .OnDelete(DeleteBehavior.Restrict));
        }
    }
}
