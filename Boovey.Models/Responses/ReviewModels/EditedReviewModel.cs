namespace Boovey.Models.Responses.ReviewModels
{
    public class EditedReviewModel
    {
        public int Id { get; set; }
        public double Rating { get; set; }
        public string Comment { get; set; }
        public int BookId { get; set; }
    }
}
