using System;
using System.Linq;
using MvcTemplate.Data.Common;
using MvcTemplate.Data.Models;
using MvcTemplate.Web.Infrastructure.Mapping;

namespace MvcTemplate.Services.Data
{
    public class RealEstatesService : IRealEstatesService
    {
        private readonly IDbRepository<RealEstate> realEstates;
        private readonly IDbRepository<Like> likes;

        public RealEstatesService(IDbRepository<RealEstate> realEstates, IDbRepository<Like> likes)
        {
            this.realEstates = realEstates;
            this.likes = likes;
        }

        public void Add(RealEstate estate)
        {
            this.realEstates.Add(estate);
            this.realEstates.Save();
        }

        public void Delete(RealEstate estate)
        {
            this.realEstates.Delete(estate);
            this.realEstates.Save();
        }

        public IQueryable<RealEstate> GetAll()
        {
            return this.realEstates.All();
        }

        public RealEstate GetById(int id)
        {
            return this.realEstates.GetById(id);
        }

        public void Update(RealEstate estate)
        {
            this.ModifyEstate(estate);
        }

        private void ModifyEstate(RealEstate estate)
        {
            var currentEstate = this.realEstates.GetById(estate.Id);
            currentEstate.City = estate.City;
            currentEstate.Content = estate.Content;
            currentEstate.EstateType = estate.EstateType;
            currentEstate.FurnishedState = estate.FurnishedState;
            currentEstate.Hood = estate.Hood;
            currentEstate.LettingType = estate.LettingType;
            currentEstate.Price = estate.Price;
            currentEstate.Size = estate.Size;
            currentEstate.Title = estate.Title;
            currentEstate.ModifiedOn = DateTime.Now;

            this.realEstates.Save();
        }
    }
}
