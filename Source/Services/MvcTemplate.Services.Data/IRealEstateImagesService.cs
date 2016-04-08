using MvcTemplate.Data.Models;

namespace MvcTemplate.Services.Data
{
    public interface IRealEstateImagesService
    {
        void Add(RealEstateImage image);

        void Delete(RealEstateImage image);

        bool IsExist(string name);

        RealEstateImage GetById(int id);
    }
}
