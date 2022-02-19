namespace Boovey.Data.Entities.Books
{
    using System.Collections.Generic;

    public class Shelve : Entity
    {
        public Shelve()
        {
            this.Books = new HashSet<Book>();
        }

        public string Title { get; set; }
        public virtual User Creator { get; set; }
        public virtual ICollection<Book> Books { get; set; }
    }
}