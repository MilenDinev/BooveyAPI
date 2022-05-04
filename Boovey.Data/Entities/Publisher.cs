namespace Boovey.Data.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces;

    public class Publisher : Entity, IAssignable, IAccessible
    {
        public Publisher()
        {
            this.Books = new HashSet<Book>();
        }

        public string Name { get; set; }
        [NotMapped]
        public string StringValue => Name;
        public virtual ICollection<Book> Books { get; set; }
    }
}