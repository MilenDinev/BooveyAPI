namespace Boovey.Data.Configurations.Users
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Entities;

    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasOne(r => r.Creator)
            .WithMany(u => u.Reviews)
            .HasForeignKey(r => r.CreatorId)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
