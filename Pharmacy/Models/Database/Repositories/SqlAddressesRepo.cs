using Microsoft.EntityFrameworkCore;
using Pharmacy.Helpers.Selectors;
using Pharmacy.Models.Database.Entities;
using Pharmacy.Models.Database.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Models.Database.Repositories
{
	public class SqlAddressesRepo : IAddressesRepo
	{
		private readonly PharmacyDBContext m_context;

		public SqlAddressesRepo(PharmacyDBContext context)
		{
			m_context = context;
		}

		public async Task CreateAddress(Address address)
		{
			await m_context.Addresses.AddAsync(address);
		}

		public async Task<Address> GetAddressById(int id)
		{
			return await m_context.Addresses.FirstOrDefaultAsync(address => address.Id == id);
		}

		public async Task<Address> GetOrCreateAddress(string city, string postCode, string streetBuilding, string localNo)
		{
			/* zjebane
			var address = await m_context.Addresses.FirstOrDefaultAsync(
				address =>
					address.City.ToLower().Equals(city.ToLower()) &&
					address.PostalCode.ToLower().Equals(postCode.ToLower()) &&
					address.StreetAndBuildingNo.ToLower().Equals(streetBuilding.ToLower()) &&
					!(address.LocalNo != null && localNo == null) &&
					!(address.LocalNo == null && localNo != null) &&
					((address.LocalNo == null && localNo == null) ||
					address.LocalNo.ToLower().Equals(localNo.ToLower())));
			//*/

			var tmp = new Address
			{
				City = city,
				PostalCode = postCode,
				StreetAndBuildingNo = streetBuilding,
				LocalNo = localNo
			};

			var selector = new AddressRepetitionSelector();

			var address = await m_context.Addresses.FirstOrDefaultAsync(a => selector.TestForRepetition(tmp, a));

			if (address != null)
			{
				return address;
			}

			address = tmp;

			await m_context.Addresses.AddAsync(address);
			return address;
		}

		public async Task<int> SaveChanges()
		{
			return await m_context.SaveChangesAsync();
		}
	}
}
