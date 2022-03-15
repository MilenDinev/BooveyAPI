namespace Boovey.Data.Configurations.Users
{
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Entities;

    public class UserShelveConfiguration : IEntityTypeConfiguration<Shelve>
    {
        public void Configure(EntityTypeBuilder<Shelve> builder)
        {
            builder.HasMany(g => g.FavoriteByUsers)
             .WithMany(u => u.FavoriteShelves)
                .UsingEntity<Dictionary<string, object>>("UserShelves",
                x => x.HasOne<User>().WithMany().HasForeignKey("UserId")
                      .OnDelete(DeleteBehavior.Restrict),
                x => x.HasOne<Shelve>().WithMany().HasForeignKey("ShelveId")
                      .OnDelete(DeleteBehavior.Restrict));
        }
    }
}
