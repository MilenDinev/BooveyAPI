namespace Boovey.Data.Entities.Books
{
    using System.Collections.Generic;

    public class Publisher : Entity
    {
        public Publisher()
        {
            this.Books = new HashSet<Book>();
        }

        public string Name { get; set; }
        public virtual ICollection<Book> Books { get; set; }
    }
}