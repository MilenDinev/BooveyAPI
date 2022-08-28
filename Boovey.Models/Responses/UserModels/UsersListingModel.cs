namespace Boovey.Models.Responses.UserModels
{
    using System.Collections.Generic;

    public class UserListingModel
    {
        public UserListingModel()
        {
            this.FavoriteAuthors = new HashSet<string>();
            this.FavoriteBooks = new HashSet<string>();
            this.FavoriteGenres = new HashSet<string>();
            this.Followers = new HashSet<string>();
            this.Following = new HashSet<string>();
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public ICollection<string> FavoriteAuthors { get; set; }
        public ICollection<string> FavoriteBooks { get; set; }
        public ICollection<string> FavoriteGenres { get; set; }
        public ICollection<string> Followers { get; set; }
        public ICollection<string> Following { get; set; }

    }
}
