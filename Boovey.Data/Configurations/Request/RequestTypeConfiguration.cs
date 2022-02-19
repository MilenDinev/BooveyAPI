namespace Boovey.Data.Configurations.Request
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Entities.Requests;

    public class RequestTypeConfiguration : IEntityTypeConfiguration<RequestType>
    {
        public void Configure(EntityTypeBuilder<RequestType> builder)
        {
            builder.HasData(
                    new RequestType
                    {
                        Id = 1,
                        Type = "Friend"
                    });

            builder.HasData(
                    new RequestType
                    {
                        Id = 2,
                        Type = "Follow"
                    });
        }
    }
}