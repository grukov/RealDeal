namespace MvcTemplate.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Data.Models;
    using Infrastructure.Mapping;
    using Microsoft.AspNet.Identity;
    using Services.Data;
    using ViewModels.Comment;
    using ViewModels.RealEstate;

    public class RealEstateController : Controller
    {
        private readonly IRealEstatesService realEstates;
        private readonly IRealEstateImagesService images;

        public RealEstateController(IRealEstatesService realEstates, IRealEstateImagesService images)
        {
            this.realEstates = realEstates;
            this.images = images;
        }

        public ActionResult AllRealEstates()
        {
            var model = this.GetTopEstates();
            this.TempData["Category"] = "Top Real Estates";

            return this.View("GetTopRealEstates", model);
        }

        public ActionResult TopRentRealEstates()
        {
            var model = this.GetTopEstatesBy(LettingType.Rent);
            this.TempData["Category"] = "Top Real Estates For Rent";

            return this.View("GetTopRealEstates", model);
        }

        public ActionResult TopSaleRealEstates()
        {
            var model = this.GetTopEstatesBy(LettingType.Sale);
            this.TempData["Category"] = "Top Real Estates For Sale";

            return this.View("GetTopRealEstates", model);
        }

        public ActionResult UserRealEstates(string id)
        {
            var model = this.GetRealEstatesBy(id);
            this.TempData["Category"] = "Real Estates by " + this.TempData["User"];

            return this.View("GetTopRealEstates", model);
        }

        [HttpGet]
        public ActionResult AddRealEstate()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddRealEstate(RealEstateAddViewModel estate, ICollection<HttpPostedFileBase> images)
        {
            if (this.ModelState.IsValid)
            {
                var currentEstate = this.MapFromModel(estate);
                currentEstate.UserId = this.User.Identity.GetUserId();
                this.realEstates.Add(currentEstate);
                this.AddImages(currentEstate, images);

                return this.RedirectToAction("Index", "Home");
            }

            return this.View("Index", estate);
        }

        public ActionResult RealEstateDetails(int id)
        {
            var estate = this.realEstates.GetById(id);
            var model = new RealEstateDetails();
            this.ViewBag.totalPages = Math.Ceiling(estate.Comments.Count / (decimal)5);
            this.TempData["Id"] = id;
            model.RealEstateDetailsViewModel = this.MapFromRealEstate(estate);

            return this.View(model);
        }

        private RealEstate MapFromModel(RealEstateAddViewModel model)
        {
            var currentEstate = new RealEstate();
            currentEstate.Content = model.Content;
            currentEstate.EstateType = model.EstateType;
            currentEstate.LettingType = model.LettingType;
            currentEstate.Price = model.Price;
            currentEstate.Size = model.Size;
            currentEstate.Title = model.Title;
            currentEstate.City = model.City;
            currentEstate.Hood = model.Hood;
            currentEstate.FurnishedState = model.FurnishedState;

            return currentEstate;
        }

        private void AddImages(RealEstate estate, ICollection<HttpPostedFileBase> images)
        {
            if (images != null)
            {
                foreach (var image in images)
                {
                    if (image != null)
                    {
                        var imgName = estate.Id + image.FileName;
                        if (!this.images.IsExist(imgName))
                        {
                            var binaryReader = new BinaryReader(image.InputStream);
                            var numberOfDots = image.FileName.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                            var currentImage = new RealEstateImage();
                            currentImage.RealEstateId = estate.Id;
                            currentImage.Extension = numberOfDots[numberOfDots.Length - 1];
                            currentImage.Name = imgName;
                            currentImage.Content = binaryReader.ReadBytes(image.ContentLength);
                            this.images.Add(currentImage);
                        }
                    }
                }
            }
        }

        private RealEstateDetailsViewModel MapFromRealEstate(RealEstate realEstate)
        {
            var currentEstate = new RealEstateDetailsViewModel();
            currentEstate.Id = realEstate.Id;
            currentEstate.Content = realEstate.Content;
            currentEstate.EstateType = realEstate.EstateType;
            currentEstate.LettingType = realEstate.LettingType;
            currentEstate.Price = realEstate.Price;
            currentEstate.Size = realEstate.Size;
            currentEstate.Title = realEstate.Title;
            currentEstate.Images = realEstate.Images;
            currentEstate.Comments = this.GetTopFiveComments(realEstate.Comments);
            currentEstate.UserId = realEstate.UserId;
            currentEstate.User = realEstate.User;
            currentEstate.Likes = realEstate.Likes;
            currentEstate.City = realEstate.City;
            currentEstate.Hood = realEstate.Hood;
            currentEstate.CommentsCount = realEstate.Comments.Count;
            currentEstate.FurnishedState = realEstate.FurnishedState;

            return currentEstate;
        }

        private ICollection<RealEstateViewModel> GetTopEstatesBy(LettingType? lettingType)
        {
            var model = this.realEstates.GetAll()
                .Where(x => x.LettingType == lettingType)
                .OrderByDescending(x => x.Likes.Count).To<RealEstateViewModel>().ToList();

            return model;
        }

        private ICollection<RealEstateViewModel> GetTopEstates()
        {
            var model = this.realEstates.GetAll()
                .OrderByDescending(x => x.Likes.Count).To<RealEstateViewModel>().ToList();

            return model;
        }

        private ICollection<CommentPagingViewModel> GetTopFiveComments(ICollection<Comment> comments)
        {
            comments = comments.OrderByDescending(x => x.CreatedOn).Take(5).ToList();
            List<CommentPagingViewModel> currentComments = new List<CommentPagingViewModel>();

            foreach (var comment in comments)
            {
                var commentViewModel = new CommentPagingViewModel();
                commentViewModel.Content = comment.Content;
                commentViewModel.CreatedOn = comment.CreatedOn;
                commentViewModel.User = comment.User;
                currentComments.Add(commentViewModel);
            }

            return currentComments;
        }

        private ICollection<RealEstateViewModel> GetRealEstatesBy(string userId)
        {
            var currentEstates = this.realEstates.GetAll()
                .OrderByDescending(x => x.Comments.Count)
                .Where(x => x.UserId == userId).To<RealEstateViewModel>().ToList();

            return currentEstates;
        }
    }
}