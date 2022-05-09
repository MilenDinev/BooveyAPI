namespace Boovey.Data.Entities
{
    using System.Collections.Generic;
    using Interfaces;

    public class Author : Entity, IAssignable, IAccessible
    {
        public Author()
        {
            this.Books = new HashSet<Book>();
            this.Genres = new HashSet<Genre>();
            this.Quotes = new HashSet<Quote>();
            this.FavoriteByUsers = new HashSet<User>();
        }

        public string Fullname { get; set; }
        public int CountryId { get; set; }
        public virtual Country Country { get; set; }
        public string Summary { get; set; }
        public virtual ICollection<Book> Books { get; set; }
        public virtual ICollection<Genre> Genres { get; set; }
        public virtual ICollection<Quote> Quotes { get; set; }
        public virtual ICollection<User> FavoriteByUsers { get; set; }
    }
}