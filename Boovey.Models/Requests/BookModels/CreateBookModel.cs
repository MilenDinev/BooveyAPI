namespace Boovey.Models.Requests.BookModels
{
    using System.Collections.Generic;
    using GenreModels;
    using AuthorModels;
    using PublisherModels;

    public class CreateBookModel
    {
        public CreateBookModel()
        {
            this.Genres = new HashSet<CreateGenreModel>();
            this.Authors = new HashSet<CreateAuthorModel>();
        }

        public string CoverUrl { get; set; }
        public string Title { get; set; }
        public int Pages { get; set; }
        public string PublicationDate { get; set; }
        public int CountryId { get; set; }
        public string Description { get; set; }
        public CreatePublisherModel Publisher  { get; set; }
        public ICollection<CreateGenreModel> Genres { get; set; }
        public ICollection<CreateAuthorModel> Authors { get; set; }
    }
}
