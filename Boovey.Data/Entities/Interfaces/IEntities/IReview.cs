namespace Boovey.Data.Entities.Interfaces.IEntities
{
    internal interface IReview
    {
        double Rating { get; set; }
        string Comment { get; set; }
        int Likes { get; set; }
        int BookId { get; set; }
        Book Book { get; set; }
        User Creator { get; set; }
    }
}
