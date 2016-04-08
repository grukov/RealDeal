namespace MvcTemplate.Services.Data
{
    using System.Linq;

    using Microsoft.AspNet.Identity;

    using MvcTemplate.Data.Common;
    using MvcTemplate.Data.Models;

    public class UsersService : IUsersService
    {
        private IDbRepository<UserImage> profilePictures;
        private UserManager<User> manager;

        public UsersService(UserManager<User> manager, IDbRepository<UserImage> profilePictures)
        {
            this.manager = manager;
            this.profilePictures = profilePictures;
        }

        public User GetById(string id)
        {
            return this.manager.Users.FirstOrDefault(u => u.Id == id);
        }

        public void Update(User user)
        {
            var currentUser = this.manager.Users.FirstOrDefault(u => u.Id == user.Id);
            currentUser.Email = user.Email;
            currentUser.FirstName = user.FirstName;
            currentUser.LastName = user.LastName;
        }

        public void DisableProfilePicture(User user)
        {
            var currentProfilePicture = user.Photos.FirstOrDefault(p => p.IsProfilePicture == true);

            if (currentProfilePicture != null)
            {
                currentProfilePicture.IsProfilePicture = false;
                this.profilePictures.Save();
            }
        }
    }
}
