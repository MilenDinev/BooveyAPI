namespace Boovey.Models.Requests.BookModels
{
    public class EditBookModel
    {
        public string CoverUrl { get; set; }
        public string Title { get; set; }
        public int Pages { get; set; }
        public string PublicationDate { get; set; }
        public int CountryId { get; set; }
        public string Description { get; set; }
        public int PublisherId { get; set; }
    }
}
