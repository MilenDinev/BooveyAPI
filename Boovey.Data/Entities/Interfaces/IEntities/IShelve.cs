namespace Boovey.Data.Entities.Interfaces.IEntities
{
    using Interfaces.IAssignables;
    using System.Collections.Generic;

    internal interface IShelve : IBookAssignable
    {
        string Title { get; set; }
        User Creator { get; set; }
        ICollection<User> FavoriteByUsers { get; set; }
    }
}
