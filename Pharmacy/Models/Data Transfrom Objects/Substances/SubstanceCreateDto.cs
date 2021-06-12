using System.ComponentModel.DataAnnotations;
namespace Pharmacy.Models.Data_Transfrom_Objects.Substances
{
	public class SubstanceCreateDto
	{
		[MaxLength(1024)]
		public string Name { get; set; }
	}
}
