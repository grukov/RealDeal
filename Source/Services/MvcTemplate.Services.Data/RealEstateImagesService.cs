namespace MvcTemplate.Services.Data
{
    using System;
    using System.Linq;
    using MvcTemplate.Data.Common;
    using MvcTemplate.Data.Models;

    public class RealEstateImagesService : IRealEstateImagesService
    {
        private IDbRepository<RealEstateImage> images;

        public RealEstateImagesService(IDbRepository<RealEstateImage> images)
        {
            this.images = images;
        }

        public void Add(RealEstateImage image)
        {
            this.images.Add(image);
            this.images.Save();
        }

        public void Delete(RealEstateImage image)
        {
            this.images.Delete(image);
            this.images.Save();
        }

        public RealEstateImage GetById(int id)
        {
            return this.images.GetById(id);
        }

        public bool IsExist(string name)
        {
            var currentImage = this.images.All().FirstOrDefault(x => x.Name == name);
            if (currentImage != null)
            {
                return true;
            }

            return false;
        }
    }
}
