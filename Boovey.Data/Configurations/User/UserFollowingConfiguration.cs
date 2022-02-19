namespace Boovey.Data.Configurations.User
{
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Data.Entities;

    public class UserFollowingConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasMany(f => f.Following)
             .WithMany(u => u.Followers)
                .UsingEntity<Dictionary<string, object>>("UsersFollowing",
                x => x.HasOne<User>().WithMany().HasForeignKey("UserId")
                      .OnDelete(DeleteBehavior.Restrict),
                x => x.HasOne<User>().WithMany().HasForeignKey("FollowingId")
                      .OnDelete(DeleteBehavior.Restrict));
        }
    }
}
