namespace MvcTemplate.Data.Models
{
    using MvcTemplate.Data.Common.Models;

    public class RealEstateImage : Image
    {
        public int RealEstateId { get; set; }

        public virtual RealEstate RealEstate { get; set; }
    }
}
