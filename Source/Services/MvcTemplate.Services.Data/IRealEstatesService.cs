namespace MvcTemplate.Services.Data
{
    using System.Linq;
    using MvcTemplate.Data.Models;

    public interface IRealEstatesService
    {
        void Add(RealEstate estate);

        void Delete(RealEstate estate);

        IQueryable<RealEstate> GetAll();

        RealEstate GetById(int id);

        void Update(RealEstate estate);
    }
}
