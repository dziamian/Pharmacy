using Pharmacy.Models.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Models.Database.Repositories.Interfaces
{
    public interface IRatingsRepo
    {
        Task CreateRating(Rating rating);
        Task<IEnumerable<Rating>> GetAllRatings(string uid);
        Task<IEnumerable<Rating>> GetAllProductRatings(int productId);
        Task<Rating> GetRating(string uid, int productId);
        Task<double> GetAverageRating(int productId);
        Task<bool> RemoveRating(string uid, int productId);
        void MarkForUpdate(Rating rating);
        Task SaveChanges();
    }
}
