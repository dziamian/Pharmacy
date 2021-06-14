using Microsoft.EntityFrameworkCore;
using Pharmacy.Models.Database.Entities;
using Pharmacy.Models.Database.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Models.Database.Repositories
{
    public class SqlClientsRepo : IClientsRepo
    {
        private readonly PharmacyDBContext _context;
        public SqlClientsRepo(PharmacyDBContext context)
        {
            _context = context;
        }

        public async Task CreateClient(Client client)
        {
            await _context.Clients.AddAsync(client);
        }

        public async Task<Client> GetClient(string uid)
        {
            return await _context.Clients.FirstOrDefaultAsync(x => x.ClientId == uid);
        }

        public void MarkForUpdate(Client client)
        {
            if (client == null)
            {
                return;
            }

            _context.Entry(client).State = EntityState.Modified;
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
