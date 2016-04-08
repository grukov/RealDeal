namespace MvcTemplate.Web.ViewModels.RealEstate
{
    using System.Collections.Generic;
    using MvcTemplate.Data.Models;
    using MvcTemplate.Web.Infrastructure.Mapping;
    using MvcTemplate.Web.ViewModels.Comment;

    public class RealEstateDetailsViewModel : IMapFrom<MvcTemplate.Data.Models.RealEstate>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public double Size { get; set; }

        public double Price { get; set; }

        public string City { get; set; }

        public string Hood { get; set; }

        public int CommentsCount { get; set; }

        public string UserId { get; set; }

        public virtual MvcTemplate.Data.Models.User User { get; set; }

        public EstateType EstateType { get; set; }

        public LettingType LettingType { get; set; }

        public FurnishedState FurnishedState { get; set; }

        public CommentAddViewModel CommentAddViewModel { get; set; }

        public ICollection<Like> Likes { get; set; }

        public ICollection<CommentPagingViewModel> Comments { get; set; }

        public ICollection<RealEstateImage> Images { get; set; }
    }
}