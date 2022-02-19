namespace Boovey.Data.Configurations.Request
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Entities.Books;

    public class ShelveConfiguration : IEntityTypeConfiguration<Shelve>
    {
        public void Configure(EntityTypeBuilder<Shelve> builder)
        {
            builder.HasOne(s => s.Creator)
            .WithMany(u => u.Shelves)
            .HasForeignKey(s => s.CreatorId)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
