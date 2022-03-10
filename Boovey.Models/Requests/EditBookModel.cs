namespace Boovey.Models.Requests
{
    using System.Collections.Generic;

    public class EditBookModel
    {
        public string CoverUrl { get; set; }
        public string Title { get; set; }
        public int Pages { get; set; }
        public string PublicationDate { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }
        public int PublisherId { get; set; }
    }
}
