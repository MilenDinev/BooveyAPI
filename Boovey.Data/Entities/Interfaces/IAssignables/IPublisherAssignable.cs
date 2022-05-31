namespace Boovey.Data.Entities.Interfaces.IAssignables
{
    public interface IPublisherAssignable
    {
        public int? PublisherId { get; set; }
        public Publisher Publisher { get; set; }
    }
}
