using Microsoft.EntityFrameworkCore;
using Pharmacy.Models.Database.Entities;
using Pharmacy.Models.Database.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Models.Database.Repositories
{
    public class SqlRatingsRepo : IRatingsRepo
    {
        private readonly PharmacyDBContext _context;

        public SqlRatingsRepo(PharmacyDBContext context)
        {
            _context = context;
        }

        public async Task CreateRating(Rating rating)
        {
            await _context.Ratings.AddAsync(rating);
        }

        public async Task<IEnumerable<Rating>> GetAllRatings(string uid)
        {
            return await _context.Ratings.Where(r => r.ClientId == uid).ToListAsync();
        }

        public async Task<IEnumerable<Rating>> GetAllProductRatings(int productId)
        {
            return await _context.Ratings.Where(r => r.ProductId == productId).ToListAsync();
        }

        public async Task<double> GetAverageRating(int productId)
        {
            var query = _context.Ratings.Where(r => r.ProductId == productId);
            if (query.Count() == 0)
            {
                return -1d;
            }
            return await query.AverageAsync(r => r.Score);
        }

        public async Task<Rating> GetRating(string uid, int productId)
        {
            return await _context.Ratings.FirstOrDefaultAsync(r => r.ClientId == uid && r.ProductId == productId);
        }

        public void MarkForUpdate(Rating rating)
        {
            var entry = _context.Entry(rating);
            if (entry != null)
            {
                entry.State = EntityState.Modified;
            }
        }

        public async Task<bool> RemoveRating(string uid, int productId)
        {
            var ratings = _context.Ratings;
            var rating = await GetRating(uid, productId);
            if (rating == null)
            {
                return false;
            }

            ratings.Remove(rating);
            return true;
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
