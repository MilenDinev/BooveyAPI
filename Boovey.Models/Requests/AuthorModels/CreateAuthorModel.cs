namespace Boovey.Models.Requests.AuthorModels
{
    public class CreateAuthorModel
    {
        public string Fullname { get; set; }
        public int CountryId { get; set; }
        public string Summary { get; set; }
    }
}
