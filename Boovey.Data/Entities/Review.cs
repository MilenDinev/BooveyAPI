namespace Boovey.Data.Entities
{

    public class Review : Entity
    {
        public double Rating { get; set; }
        public string Comment { get; set; }
        public int Likes { get; set; }
        public int BookId { get; set; }
        public virtual Book Book { get; set; }
    }
}