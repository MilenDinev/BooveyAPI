namespace Boovey.Data.Entities.Interfaces
{
    public interface IAccessible
    {
        public int Id { get; }
        public string StringValue { get; }
        public bool Deleted { get; }
    }
}
