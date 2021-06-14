using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Models.Data_Transfrom_Objects.Address
{
	public class AddressReadDto
	{
		public int Id { get; set; }

		public string City { get; set; }

		public string PostalCode { get; set; }

		public string StreetAndBuildingNo { get; set; }

		public string LocalNo { get; set; }
	}
}
