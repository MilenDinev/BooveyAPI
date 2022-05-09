namespace Boovey.Data.Entities
{
    using System.Collections.Generic;
    using Interfaces;

    public class Shelve : Entity, ISearchable
    {
        public Shelve()
        {
            this.Books = new HashSet<Book>();
            this.FavoriteByUsers = new HashSet<User>();
        }

        public string Title { get; set; }
        public virtual User Creator { get; set; }
        public virtual ICollection<Book> Books { get; set; }

        public virtual ICollection<User> FavoriteByUsers { get; set; }
    }
}