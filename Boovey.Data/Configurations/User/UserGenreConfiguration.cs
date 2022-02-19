namespace Boovey.Data.Configurations.User
{
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Entities;

    public class UserGenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.HasMany(g => g.FavoriteByUsers)
             .WithMany(u => u.FavoriteGenres)
                .UsingEntity<Dictionary<string, object>>("UsersGenres",
                x => x.HasOne<User>().WithMany().HasForeignKey("UserId")
                      .OnDelete(DeleteBehavior.Restrict),
                x => x.HasOne<Genre>().WithMany().HasForeignKey("GenreId")
                      .OnDelete(DeleteBehavior.Restrict));
        }
    }
}
