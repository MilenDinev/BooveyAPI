namespace Boovey.Data.Entities
{
    using System.Collections.Generic;
    using Interfaces.IEntities;

    public class Country : Entity, ICountry
    {
        public Country()
        {
            this.Books = new HashSet<Book>();
            this.Authors = new HashSet<Author>();
        }

        public string Name { get; set; }
        public virtual ICollection<Book> Books { get; set; }
        public virtual ICollection<Author> Authors { get; set; }
    }
}