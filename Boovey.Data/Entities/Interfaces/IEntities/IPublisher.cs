namespace Boovey.Data.Entities.Interfaces.IEntities
{
    using IAssignables;

    internal interface IPublisher : IBookAssignable
    {
        string Name { get; set; }
    }
}
