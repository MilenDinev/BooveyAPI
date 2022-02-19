namespace Boovey.Data.Entities.Books
{
    using System.Collections.Generic;

    public class Author : Entity
    {
        public Author()
        {
            this.Books = new HashSet<Book>();
            this.FavoriteByUsers = new HashSet<User>();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Summary { get; set; }
        public virtual ICollection<Book> Books { get; set; }
        public virtual ICollection<User> FavoriteByUsers { get; set; }
    }
}