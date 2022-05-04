namespace Boovey.Data.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces;

    public class Country : IAccessible
    {
        public Country()
        {
            this.Books = new HashSet<Book>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        [NotMapped]
        public string StringValue => Name;
        public virtual ICollection<Book> Books { get; set; }
        public bool Deleted { get; set; }
    }
}