namespace Boovey.Data.Entities
{
    using System.Collections.Generic;
    using Books;

    public class Genre : Entity
    {
        public Genre()
        {
            this.Books = new HashSet<Book>();
            this.FavoriteByUsers = new HashSet<User>();
        }

        public string Title { get; set; }
        public virtual ICollection<Book> Books { get; set; }
        public virtual ICollection<User> FavoriteByUsers { get; set; }
    }
}