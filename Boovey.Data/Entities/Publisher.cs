namespace Boovey.Data.Entities
{
    using System.Collections.Generic;
    using Interfaces;

    public class Publisher : Entity, IAssignable, IAccessible
    {
        public Publisher()
        {
            this.Books = new HashSet<Book>();
        }

        public string Name { get; set; }
        public virtual ICollection<Book> Books { get; set; }
    }
}