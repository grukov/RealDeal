using System.Linq;
using MvcTemplate.Data.Models;

namespace MvcTemplate.Services.Data
{
    public interface ILikesService
    {
        IQueryable<Like> All();

        Like GetById(int id);

        void Add(Like like);

        void Delete(Like like);
    }
}
