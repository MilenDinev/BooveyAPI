namespace Boovey.Data.Entities
{
    using System.Collections.Generic;

    public class Quote : Entity
    {
        public Quote()
        {
            this.FavoriteByUsers = new HashSet<User>();
        }

        public string Content { get; set; }
        public int Likes { get; set; }
        public int? AuthorId { get; set; }
        public virtual Author Author { get; set; }
        public int? BookId { get; set; }
        public virtual Book Book { get; set; }
        public virtual ICollection<User> FavoriteByUsers { get; set; }
    }
}