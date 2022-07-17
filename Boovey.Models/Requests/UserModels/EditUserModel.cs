namespace Boovey.Models.Requests.UserModels
{
    using System.ComponentModel.DataAnnotations;

    public class EditUserModel
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [EmailAddress(ErrorMessage = "Please provide valid email address!")]
        public string Email { get; set; }
    }
}
