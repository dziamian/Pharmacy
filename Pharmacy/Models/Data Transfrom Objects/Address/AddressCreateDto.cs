using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Models.Data_Transfrom_Objects.Address
{
	public class AddressCreateDto
	{
		[Required]
		[MaxLength(1024)]
		public string City { get; set; }

		[Required]
		[MaxLength(6)]
		public string PostalCode { get; set; }

		[Required]
		[MaxLength(2048)]
		public string StreetAndBuildingNo { get; set; }

		[MaxLength(1024)]
		public string LocalNo { get; set; }
	}
}
