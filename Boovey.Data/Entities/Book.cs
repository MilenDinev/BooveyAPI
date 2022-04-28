namespace Boovey.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using Interfaces;

    public class Book : Entity, IBook, IAssignable
    {
        public Book()
        {
            this.Authors = new HashSet<Author>();
            this.Genres = new HashSet<Genre>();
            this.Quotes = new HashSet<Quote>();
            this.Shelves = new HashSet<Shelve>();
            this.Reviews = new HashSet<Review>();
            this.FavoriteByUsers = new HashSet<User>();
        }

        public string CoverUrl { get; set; }
        public string Title { get; set; }
        public int Pages { get; set; }
        public DateTime PublicationDate { get; set; }
        public int CountryId { get; set; }
        public virtual Country Country { get; set; }
        public string Description { get; set; }
        public int? PublisherId { get; set; }
        public virtual Publisher Publisher { get; set; }
        public virtual ICollection<Quote> Quotes { get; set; }
        public virtual ICollection<Shelve> Shelves { get; set; }
        public virtual ICollection<Genre> Genres { get; set; }
        public virtual ICollection<Author> Authors { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<User> FavoriteByUsers { get; set; }
    }
}