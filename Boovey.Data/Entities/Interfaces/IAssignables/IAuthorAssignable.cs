namespace Boovey.Data.Entities.Interfaces.IAssignables
{
    using System.Collections.Generic;

    public interface IAuthorAssignable
    {
        public ICollection<Author> Authors { get; set; }
    }
}
