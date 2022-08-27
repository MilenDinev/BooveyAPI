namespace Boovey.Services.Managers.Interfaces
{
    using System.Threading.Tasks;
    using Data.Entities;

    public interface IFollowersManager
    {
        Task Follow(User follower, User followed);
        Task Unfollow(User follower, User followed);
    }
}
