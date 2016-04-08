namespace MvcTemplate.Services.Data
{
    using MvcTemplate.Data.Models;

    public interface IUsersService
    {
        User GetById(string id);

        void Update(User user);

        void DisableProfilePicture(User user);
    }
}
