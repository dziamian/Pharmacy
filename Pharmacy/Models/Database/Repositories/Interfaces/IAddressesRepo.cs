using Pharmacy.Models.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Models.Database.Repositories.Interfaces
{
	public interface IAddressesRepo
	{
		Task CreateAddress(Address address);
		Task<Address> GetAddressById(int id);
		Task<Address> GetOrCreateAddress(string city, string postCode, string streetBuilding, string localNo);
		Task<int> SaveChanges();
	}
}
