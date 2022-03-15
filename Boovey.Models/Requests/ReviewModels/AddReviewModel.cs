namespace Boovey.Models.Requests.ReviewModels
{
    public class AddReviewModel
    {
        public double Rating { get; set; }
        public string Comment { get; set; }
        public int BookId { get; set; }
    }
}
