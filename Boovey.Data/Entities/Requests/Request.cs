namespace Boovey.Data.Entities.Requests
{
    public class Request : Entity
    {
        public string Description { get; set; }
        public int RequesterId { get; set; }
        public virtual User Requester { get; set; }
        public int ApproverId { get; set; }
        public virtual User Approver { get; set; }
        public int TypeId { get; set; }
        public virtual RequestType Type { get; set; }
        public int StatusId { get; set; }
        public virtual RequestStatus Status { get; set; }
    }
}