namespace Boovey.Models.Requests.ReviewModels
{
    public class CreateReviewModel
    {
        public double Rating { get; set; }
        public string Comment { get; set; }
        public int BookId { get; set; }
    }
}
