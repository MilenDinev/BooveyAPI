namespace Boovey.Data.Entities.Interfaces
{
    using System;
    using System.Collections.Generic;

    public interface IBook : IAssignable, ISearchable
    {
        public string CoverUrl { get; set; }
        public string Title { get; set; }
        public int Pages { get; set; }
        public DateTime PublicationDate { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
        public string Description { get; set; }
        public int? PublisherId { get; set; }
        public Publisher Publisher { get; set; }
        public ICollection<Quote> Quotes { get; set; }
        public ICollection<Shelve> Shelves { get; set; }
        public ICollection<Genre> Genres { get; set; }
        public ICollection<Author> Authors { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<User> FavoriteByUsers { get; set; }
    }
}
