namespace MvcTemplate.Web.Controllers
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Services.Data;
    using Microsoft.AspNet.Identity;
    using Data.Models;

    public class UserImageController : BaseController
    {
        private readonly IUserImagesService userImages;
        private readonly IUsersService users;

        public UserImageController(IUserImagesService userImages, IUsersService users)
        {
            this.userImages = userImages;
            this.users = users;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult AddUserImage(HttpPostedFileBase photo)
        {
            var currentUserId = this.User.Identity.GetUserId();
            if (photo != null)
            {
                this.AddPicture(photo);
            }

            return this.RedirectToAction("UserGallery/" + currentUserId, "User");
        }

        [HttpGet]
        public ActionResult GetUserImage(int id)
        {
            var currentPhoto = this.userImages.GetById(id);

            return this.File(currentPhoto.Content, "image/jpeg");
        }

        public ActionResult GetProfilePicture(string id)
        {
            var currentUser = this.users.GetById(id);
            var photo = currentUser.Photos.FirstOrDefault(p => p.IsProfilePicture == true);

            return this.File(photo.Content, "image/jpeg");
        }

        public ActionResult MakeProfilePicture(int id)
        {
            var currentUserId = this.User.Identity.GetUserId();
            var currentProfilePicture = this.users.GetById(currentUserId).Photos.FirstOrDefault(p => p.IsProfilePicture == true);
            if (currentProfilePicture != null)
            {
                this.userImages.DeactivateProfilePicture(currentProfilePicture.Id);
            }

            this.userImages.ActivateProfilePicture(id);
            return this.RedirectToAction("UserManager", "User");
        }

        public ActionResult RemoveUserImage(int id)
        {
            var currentUserId = this.User.Identity.GetUserId();
            var currentUser = this.users.GetById(currentUserId);
            var currentPhoto = this.userImages.GetById(id);
            if (currentPhoto.IsProfilePicture)
            {
                currentPhoto.IsProfilePicture = false;
            }

            currentUser.Photos.Remove(currentPhoto);
            this.userImages.Delete(currentPhoto);

            return this.RedirectToAction("UserGallery/" + currentUserId, "User");
        }

        private void AddPicture(HttpPostedFileBase photo)
        {
            var userId = this.User.Identity.GetUserId();

            var currentUser = this.users.GetById(userId);

            var binaryReader = new BinaryReader(photo.InputStream);
            var numberOfDots = photo.FileName.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            var currentPhoto = new UserImage();
            currentPhoto.UserId = currentUser.Id;
            currentPhoto.Extension = numberOfDots[numberOfDots.Length - 1];
            currentPhoto.Name = photo.FileName;
            currentPhoto.Content = binaryReader.ReadBytes(photo.ContentLength);
            currentPhoto.IsProfilePicture = false;
            this.userImages.Add(currentPhoto);
            currentUser.Photos.Add(currentPhoto);
        }
    }
}