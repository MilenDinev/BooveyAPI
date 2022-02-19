namespace Boovey.Data.Entities.Requests
{
    using System.Collections.Generic;

    public class RequestStatus : Entity
    {
        public RequestStatus()
        {
            this.Requests = new HashSet<Request>();
        }

        public string State { get; set; }
        public virtual ICollection<Request> Requests { get; set; }
    }
}