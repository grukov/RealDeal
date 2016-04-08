namespace MvcTemplate.Web.Controllers
{
    using System;
    using System.Linq;
    using System.IO;
    using System.Web;
    using System.Web.Mvc;
    using Data.Models;
    using Microsoft.AspNet.Identity;
    using Services.Data;
    using ViewModels.User;

    public class UserController : BaseController
    {
        private readonly IUserImagesService photos;
        private readonly IUsersService users;

        public UserController(IUsersService users, IUserImagesService photos)
        {
            this.users = users;
            this.photos = photos;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        [Authorize]
        public ActionResult UserManager()
        {
            var id = this.User.Identity.GetUserId();
            var currentUser = this.users.GetById(id);
            var model = new UserManagerChangeViewModel();
            this.Mapper.Map(currentUser, model);

            return this.View(model);
        }

        public ActionResult UserDetails(string id)
        {
            var currentUser = this.users.GetById(id);

            return this.View(currentUser);
        }

        [Authorize]
        public ActionResult EditUser(UserManagerChangeViewModel model, HttpPostedFileBase avatar)
        {
            if (this.ModelState.IsValid)
            {
                string userId = this.User.Identity.GetUserId();
                var currentUser = this.users.GetById(userId);

                //this.Mapper.Map(model, currentUser);
                this.MapFromModel(model, currentUser);

                if (avatar != null)
                {
                    if (currentUser.Photos != null)
                    {
                        this.users.DisableProfilePicture(currentUser);
                    }

                    this.AddProfilePicture(currentUser, avatar);
                }

                return this.RedirectToAction("Index", "Home");
            }
            else
            {
                return this.View(model);
            }
        }

        public ActionResult UserGallery(string id)
        {
            var currentUser = this.users.GetById(id);
            var model = new UserGalleryViewModel();
            this.Mapper.Map(currentUser, model);

            return this.View(model);
        }

        private void MapFromModel(UserManagerChangeViewModel model, User user)
        {
            user.Email = model.Email;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
        }

        private void AddProfilePicture(User user, HttpPostedFileBase profilePicture)
        {
            if (profilePicture != null)
            {
                var binaryReader = new BinaryReader(profilePicture.InputStream);
                var numberOfDots = profilePicture.FileName.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                var currentProfilePicture = new UserImage();
                currentProfilePicture.UserId = user.Id;
                currentProfilePicture.Extension = numberOfDots[numberOfDots.Length - 1];
                currentProfilePicture.Name = profilePicture.FileName;
                currentProfilePicture.Content = binaryReader.ReadBytes(profilePicture.ContentLength);
                currentProfilePicture.IsProfilePicture = true;
                this.photos.Add(currentProfilePicture);
                var profilePictureId = currentProfilePicture.Id;
            }
        }
    }
}