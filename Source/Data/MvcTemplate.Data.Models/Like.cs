namespace MvcTemplate.Data.Models
{
    using MvcTemplate.Data.Common.Models;

    public class Like : BaseModel<int>
    {
        public string UserId { get; set; }

        public virtual User User { get; set; }

        public int RealEstateId { get; set; }

        public virtual RealEstate RealEstate { get; set; }
    }
}
