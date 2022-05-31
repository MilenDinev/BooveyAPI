namespace Boovey.Data.Entities.Interfaces.IAssignables
{
    using System.Collections.Generic;

    public interface IBookAssignable 
    {
        public ICollection<Book> Books { get; set; }
    }
}
