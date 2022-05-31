namespace Boovey.Data.Entities.Interfaces.IEntities
{
    using System;
    using System.Collections.Generic;
    using IAssignables;

    internal interface IBook : IAuthorAssignable, IGenreAssignable, IPublisherAssignable
    {

        public string CoverUrl { get; set; }
        public string Title { get; set; }
        public string NormalizedTitle { get; set; }
        public int Pages { get; set; }
        public DateTime PublicationDate { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
        public string Description { get; set; }
        public ICollection<Quote> Quotes { get; set; }
        public ICollection<Shelve> Shelves { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<User> FavoriteByUsers { get; set; }
    }
}
