namespace Boovey.Data.Entities.Interfaces.IEntities
{
    using System.Collections.Generic;
    using IAssignables;

    internal interface IGenre : IBookAssignable, IAuthorAssignable
    {
        ICollection<User> FavoriteByUsers { get; set; }
    }
}
