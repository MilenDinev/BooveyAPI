namespace Boovey.Data.Entities.Shared
{
    public class AuthorGenre
    {
        public int AuthorId { get; set; }
        public int GenreId { get; set; }
        public bool Favorite { get; set; }
    }
}
