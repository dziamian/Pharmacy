using Pharmacy.Models.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Models.Database.Repositories.Interfaces
{
    public interface IClientsRepo
    {
        Task CreateClient(Client client);
        Task<Client> GetClient(string uid);
        void MarkForUpdate(Client client);
        Task SaveChanges();
    }
}
