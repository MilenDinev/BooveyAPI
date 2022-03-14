namespace Boovey.Models.Requests.BookModels
{
    using System.Collections.Generic;
    using GenreModels;
    using AuthorModels;
    using PublisherModels;

    public class AddBookModel
    {
        public AddBookModel()
        {
            this.Genres = new HashSet<AddGenreModel>();
            this.Authors = new HashSet<AddAuthorModel>();
        }

        public string CoverUrl { get; set; }
        public string Title { get; set; }
        public int Pages { get; set; }
        public string PublicationDate { get; set; }
        public int CountryId { get; set; }
        public string Description { get; set; }
        public AddPublisherModel Publisher  { get; set; }
        public ICollection<AddGenreModel> Genres { get; set; }
        public ICollection<AddAuthorModel> Authors { get; set; }
    }
}
