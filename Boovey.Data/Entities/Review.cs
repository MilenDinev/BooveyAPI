namespace Boovey.Data.Entities
{
    using Interfaces;

    public class Review : Entity, IAccessible
    {
        public double Rating { get; set; }
        public string Comment { get; set; }
        public int Likes { get; set; }
        public int BookId { get; set; }
        public virtual Book Book { get; set; }

        public virtual User Creator { get; set; }
    }
}