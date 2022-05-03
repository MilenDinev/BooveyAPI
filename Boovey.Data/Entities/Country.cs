namespace Boovey.Data.Entities
{
    using System.Collections.Generic;
    using Interfaces;

    public class Country : IAccessible
    {
        public Country()
        {
            this.Books = new HashSet<Book>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Book> Books { get; set; }
        public bool Deleted { get; set; }
    }
}