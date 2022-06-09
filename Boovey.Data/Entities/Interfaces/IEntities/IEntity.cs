namespace Boovey.Data.Entities.Interfaces.IEntities
{
    using System;

    public interface IEntity
    {
        int Id { get; set; }
        string NormalizedName { get; set; }
        int CreatorId { get; set; }
        DateTime CreatedOn { get; set; }
        int LastModifierId { get; set; }
        DateTime LastModifiedOn { get; set; }
        bool Deleted { get; set; }
    }
}
