namespace Boovey.Data.Entities.Interfaces
{
    public interface ISearchable
    {
        public int Id { get; }
        public string NormalizedName { get; }
        public bool Deleted { get; }
    }
}
