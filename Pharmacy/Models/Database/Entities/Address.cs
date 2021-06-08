using System.ComponentModel.DataAnnotations;

namespace Pharmacy.Models.Database.Entities
{
	public class Address
	{
		[Key]
		public int Id { get; set; }

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
