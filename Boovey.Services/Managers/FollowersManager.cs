namespace Boovey.Services.Managers
{
    using System.Threading.Tasks;
    using Interfaces;
    using Data.Entities;

    public class FollowersManager : IFollowersManager
    {
        public async Task Follow(User follower, User followed)
        {
            await Task.Run(() => follower.Following.Add(followed));
        }

        public async Task Unfollow(User follower, User followed)
        {
            await Task.Run(() => follower.Following.Remove(followed));
        }
    }
}
