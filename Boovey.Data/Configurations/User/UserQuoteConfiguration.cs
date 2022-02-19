namespace Boovey.Data.Configurations.User
{
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Entities;

    public class UserQuoteConfiguration : IEntityTypeConfiguration<Quote>
    {
        public void Configure(EntityTypeBuilder<Quote> builder)
        {
            builder.HasMany(q => q.FavoriteByUsers)
             .WithMany(u => u.FavoriteQuotes)
                .UsingEntity<Dictionary<string, object>>("UsersQuotes",
                x => x.HasOne<User>().WithMany().HasForeignKey("UserId")
                      .OnDelete(DeleteBehavior.Restrict),
                x => x.HasOne<Quote>().WithMany().HasForeignKey("QuoteId")
                      .OnDelete(DeleteBehavior.Restrict));
        }
    }
}
