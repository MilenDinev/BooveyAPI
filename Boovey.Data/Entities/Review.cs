namespace Boovey.Data.Entities
{
    using Interfaces;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Review : Entity, IAccessible
    {
        public double Rating { get; set; }
        public string Comment { get; set; }
        [NotMapped]
        public string StringValue => Comment;
        public int Likes { get; set; }
        public int BookId { get; set; }
        public virtual Book Book { get; set; }

        public virtual User Creator { get; set; }
    }
}