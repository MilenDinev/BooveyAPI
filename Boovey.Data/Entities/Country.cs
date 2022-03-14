namespace Boovey.Data.Entities
{
    using System.Collections.Generic;

    public class Country
    {
        public Country()
        {
            this.Books = new HashSet<Book>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Book> Books { get; set; }
    }
}