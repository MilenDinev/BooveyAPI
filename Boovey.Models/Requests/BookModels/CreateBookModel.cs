namespace Boovey.Models.Requests.BookModels
{


    public class CreateBookModel
    {
        public string CoverUrl { get; set; }
        public string Title { get; set; }
        public int Pages { get; set; }
        public string PublicationDate { get; set; }
        public int CountryId { get; set; }
        public string Description { get; set; }
    }
}
