namespace Boovey.Data.Entities.Interfaces
{
    using System;

    public interface IEntity
    {
        public int Id { get; set; }
        public int CreatorId { get; set; }
        public DateTime CreatedOn { get; set; }
        public int LastModifierId { get; set; }
        public DateTime LastModifiedOn { get; set; }
        public bool Deleted { get; set; }
    }
}
