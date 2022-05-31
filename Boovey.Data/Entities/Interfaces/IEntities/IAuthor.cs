namespace Boovey.Data.Entities.Interfaces.IEntities
{
    using IAssignables;
    using System.Collections.Generic;

    public interface IAuthor : IBookAssignable, IGenreAssignable
    {
        public string Fullname { get; set; }
        public string NormalizedName { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
        public string Summary { get; set; }
        public ICollection<Quote> Quotes { get; set; }
        public ICollection<User> FavoriteByUsers { get; set; }
    }
}
