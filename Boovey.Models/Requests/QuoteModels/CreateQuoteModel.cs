namespace Boovey.Models.Requests.QuoteModels
{
    public class CreateQuoteModel
    {
        public string Content { get; set; }
        public int? AuthorId { get; set; }
        public int? BookId { get; set; }
    }
}
