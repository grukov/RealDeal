namespace MvcTemplate.Services.Data
{
    using MvcTemplate.Data.Models;

    public interface IUserImagesService
    {
        void Add(UserImage profilePicture);

        void Delete(UserImage profilePicture);

        UserImage GetById(int id);

        void DeactivateProfilePicture(int id);

        void ActivateProfilePicture(int id);
    }
}
