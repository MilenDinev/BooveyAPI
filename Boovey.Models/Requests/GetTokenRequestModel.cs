namespace Boovey.Models.Requests
{
    public class GetTokenRequestModel
    {
        public string UsernameOrEmail { get; set; }
        public string Password { get; set; }
    }
}
