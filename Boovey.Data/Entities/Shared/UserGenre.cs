namespace Boovey.Data.Entities.Shared
{
    public class UserGenre
    {
        public int UserId { get; set; }
        public int GenreId { get; set; }
        public bool Favorite { get; set; }
    }
}
