namespace Boovey.Data.Configurations.Request
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Entities.Requests;

    public class RequestStatusConfiguration : IEntityTypeConfiguration<RequestStatus>
    {
        public void Configure(EntityTypeBuilder<RequestStatus> builder)
        {
            builder.HasData(
                    new RequestStatus
                    {
                        Id = 1,
                        State = "Approved"
                    });
            builder.HasData(
                    new RequestStatus
                    {
                        Id = 2,
                        State = "Pending"
                    });

            builder.HasData(
                    new RequestStatus
                    {
                        Id = 3,
                        State = "Rejected"
                    });
        }
    }
}