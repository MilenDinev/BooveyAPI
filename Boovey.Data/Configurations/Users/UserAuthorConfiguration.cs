﻿namespace Boovey.Data.Configurations.Users
{
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Entities;

    public class UserAuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.HasMany(a => a.FavoriteByUsers)
             .WithMany(u => u.FavoriteAuthors)
                .UsingEntity<Dictionary<string, object>>("UsersAuthors",
                x => x.HasOne<User>().WithMany().HasForeignKey("UserId")
                      .OnDelete(DeleteBehavior.Cascade),
                x => x.HasOne<Author>().WithMany().HasForeignKey("AuthorId")
                      .OnDelete(DeleteBehavior.Restrict));
        }
    }
}
