namespace Boovey.Data.Entities
{
    using System;

    public abstract class Entity
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatorId { get; set; }
        public DateTime LastModifiedOn { get; set; }
        public int LastModifierId { get; set; }
    }
}
