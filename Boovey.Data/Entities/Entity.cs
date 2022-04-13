﻿namespace Boovey.Data.Entities
{
    using System;
    using Interfaces;

    public abstract class Entity : IEntity
    {
        public int Id { get; set; }
        public int CreatorId { get; set; }
        public DateTime CreatedOn { get; set; }
        public int LastModifierId { get; set; }
        public DateTime LastModifiedOn { get; set; }
        public bool Deleted { get; set; }
    }
}
