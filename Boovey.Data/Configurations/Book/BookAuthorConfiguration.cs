namespace Boovey.Data.Configurations.Book
{
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Entities.Books;

    public class BookAuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.HasMany(a => a.Books)
             .WithMany(b => b.Authors)
                .UsingEntity<Dictionary<string, object>>("BooksAuthors",
                x => x.HasOne<Book>().WithMany().HasForeignKey("BookId")
                      .OnDelete(DeleteBehavior.Restrict),
                x => x.HasOne<Author>().WithMany().HasForeignKey("AuthorId")
                      .OnDelete(DeleteBehavior.Restrict));
        }
    }
}
