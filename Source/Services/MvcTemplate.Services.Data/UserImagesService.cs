namespace MvcTemplate.Services.Data
{
    using System;
    using MvcTemplate.Data.Common;
    using MvcTemplate.Data.Models;

    public class UserImagesService : IUserImagesService
    {
        private readonly IDbRepository<UserImage> photos;

        public UserImagesService(IDbRepository<UserImage> photos)
        {
            this.photos = photos;
        }

        public void Add(UserImage profilePicture)
        {
            this.photos.Add(profilePicture);
            this.photos.Save();
        }

        public void Delete(UserImage profilePicture)
        {
            this.photos.Delete(profilePicture);
            this.photos.Save();
        }

        public UserImage GetById(int id)
        {
            return this.photos.GetById(id);
        }

        public void DeactivateProfilePicture(int id)
        {
            var currentProfilePicture = this.photos.GetById(id);
            currentProfilePicture.IsProfilePicture = false;
            this.photos.Save();
        }

        public void ActivateProfilePicture(int id)
        {
            var currentProfilePicture = this.photos.GetById(id);
            currentProfilePicture.IsProfilePicture = true;
            this.photos.Save();
        }
    }
}
