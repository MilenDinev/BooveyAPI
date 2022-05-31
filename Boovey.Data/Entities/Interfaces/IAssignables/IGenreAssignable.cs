namespace Boovey.Data.Entities.Interfaces.IAssignables
{
    using System.Collections.Generic;

    public interface IGenreAssignable 
    {
        public ICollection<Genre> Genres { get; set; }
    }
}
