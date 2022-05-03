namespace Boovey.Data.Entities.Interfaces
{
    public interface IAccessible
    {
        public int Id { get; set; }
        public bool Deleted { get; set; }
    }
}
