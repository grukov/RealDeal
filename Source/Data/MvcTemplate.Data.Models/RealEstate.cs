namespace MvcTemplate.Data.Models
{
    using System.Collections.Generic;

    using MvcTemplate.Data.Common.Models;

    public class RealEstate : BaseModel<int>
    {
        private ICollection<Like> likes;
        private ICollection<Comment> comments;
        private ICollection<RealEstateImage> images;

        public RealEstate()
        {
            this.likes = new HashSet<Like>();
            this.comments = new HashSet<Comment>();
            this.images = new HashSet<RealEstateImage>();
        }

        public string Title { get; set; }

        public string Content { get; set; }

        public double Size { get; set; }

        public double Price { get; set; }

        public string City { get; set; }

        public string Hood { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        public EstateType EstateType { get; set; }

        public LettingType LettingType { get; set; }

        public FurnishedState FurnishedState { get; set; }

        public virtual ICollection<Like> Likes
        {
            get { return this.likes; }
            set { this.likes = value; }
        }

        public virtual ICollection<Comment> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }

        public virtual ICollection<RealEstateImage> Images
        {
            get { return this.images; }
            set { this.images = value; }
        }
    }
}