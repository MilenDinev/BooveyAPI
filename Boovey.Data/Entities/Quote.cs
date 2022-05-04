namespace Boovey.Data.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces;

    public class Quote : Entity, IAccessible
    {
        public Quote()
        {
            this.FavoriteByUsers = new HashSet<User>();
        }

        public string Content { get; set; }
        [NotMapped]
        public string StringValue => Content;
        public int Likes { get; set; }
        public int? AuthorId { get; set; }
        public virtual Author Author { get; set; }
        public int? BookId { get; set; }
        public virtual Book Book { get; set; }
        public virtual ICollection<User> FavoriteByUsers { get; set; }
    }
}