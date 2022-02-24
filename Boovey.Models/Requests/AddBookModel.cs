namespace Boovey.Models.Requests
{
    using Boovey.Models.Constants;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

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


        [Required(ErrorMessage = ErrorMessages.ISBNRequiredProperty)]
        [RegularExpression(BookModelConstants.ISBNValidation,ErrorMessage = ErrorMessages.InvalidISBNProperty)]
        public string ISBN { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }
        public AddPublisherModel Publisher  { get; set; }
        public ICollection<AddGenreModel> Genres { get; set; }
        public ICollection<AddAuthorModel> Authors { get; set; }
    }
}
