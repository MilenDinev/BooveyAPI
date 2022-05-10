namespace Boovey.Data.Entities
{
    using System.Collections.Generic;
    using Interfaces;

    public class Genre : Entity, IAssignee, ISearchable
    {
        public Genre()
        {
            this.Books = new HashSet<Book>();
            this.Authors = new HashSet<Author>();
            this.FavoriteByUsers = new HashSet<User>();
        }

        public string Title { get; set; }
        public virtual ICollection<Book> Books { get; set; }
        public virtual ICollection<Author> Authors { get; set; }
        public virtual ICollection<User> FavoriteByUsers { get; set; }
    }
}