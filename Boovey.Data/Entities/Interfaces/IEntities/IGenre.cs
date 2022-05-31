namespace Boovey.Data.Entities.Interfaces.IEntities
{
    using System.Collections.Generic;
    using IAssignables;

    internal interface IGenre : IBookAssignable, IAuthorAssignable
    {
        string NormalizedTitle { get; set; }
        ICollection<User> FavoriteByUsers { get; set; }
    }
}
