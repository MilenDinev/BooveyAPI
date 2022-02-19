namespace Boovey.Data.Configurations.Book
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using System.Collections.Generic;
    using Entities;
    using Entities.Books;

    public class BookUserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasMany(u => u.FavoriteBooks)
             .WithMany(b => b.FavoriteByUsers)
                .UsingEntity<Dictionary<string, object>>("BooksUsers",
                x => x.HasOne<Book>().WithMany().HasForeignKey("BookId")
                      .OnDelete(DeleteBehavior.Restrict),
                x => x.HasOne<User>().WithMany().HasForeignKey("UserId")
                      .OnDelete(DeleteBehavior.Restrict));
        }
    }
}