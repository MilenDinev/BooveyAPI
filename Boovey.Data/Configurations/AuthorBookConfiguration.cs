namespace Boovey.Data.Configurations
{
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Entities.Books;

    public class AuthorBookConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.HasMany(a => a.Books)
             .WithMany(b => b.Authors)
                .UsingEntity<Dictionary<string, object>>("AuthorBooks",
                x => x.HasOne<Book>().WithMany().HasForeignKey("BookId")
                      .OnDelete(DeleteBehavior.Restrict),
                x => x.HasOne<Author>().WithMany().HasForeignKey("AuthorId")
                      .OnDelete(DeleteBehavior.Restrict));
        }
    }
}
