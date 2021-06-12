using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Models.Data_Transfrom_Objects.Substances
{
	public class SubstanceReadDto
	{
		public int Id { get; set; }

		[MaxLength(1024)]
		public string Name { get; set; }
	}
}
