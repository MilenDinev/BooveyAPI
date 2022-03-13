namespace Boovey.Data.Configurations.Users
{
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Data.Entities;

    public class FollowersConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasMany(f => f.Followers)
             .WithMany(u => u.Following)
                .UsingEntity<Dictionary<string, object>>("Follows",
                x => x.HasOne<User>().WithMany().HasForeignKey("FollowerId")
                      .OnDelete(DeleteBehavior.Restrict),
                x => x.HasOne<User>().WithMany().HasForeignKey("FollowedId")
                      .OnDelete(DeleteBehavior.Restrict));
        }
    }
}
