namespace Boovey.Data.Entities.Requests
{
    using System.Collections.Generic;

    public class RequestType : Entity
    {
        public RequestType()
        {
            this.Requests = new HashSet<Request>();
        }

        public string Type { get; set; }
        public virtual ICollection<Request> Requests { get; set; }
    }
}