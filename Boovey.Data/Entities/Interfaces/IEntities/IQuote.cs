namespace Boovey.Data.Entities.Interfaces.IEntities
{
    using System.Collections.Generic;

    internal interface IQuote
    {
        public string Content { get; set; }
        public int Likes { get; set; }
        public int? AuthorId { get; set; }
        public Author Author { get; set; }
        public int? BookId { get; set; }
        public Book Book { get; set; }
        public ICollection<User> FavoriteByUsers { get; set; }
    }
}