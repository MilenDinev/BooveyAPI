namespace Boovey.Data.Entities.Interfaces.IEntities
{
    using IAssignables;

    internal interface ICountry : IAuthorAssignable, IBookAssignable
    {
        string Name { get; set; }
        string NormalizedName { get; set; }
    }
}
