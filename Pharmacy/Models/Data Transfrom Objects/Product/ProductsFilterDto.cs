using Pharmacy.Models.Data_Transfrom_Objects.Substance;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Models.Data_Transfrom_Objects.Product
{
	public class ProductsFilterDto
	{
		public IEnumerable<int> CategoryIds { get; set; }

		public string Name { get; set; }

		public int MinPrice { get; set; }

		public int MaxPrice { get; set; }

		public IEnumerable<DoseCreateDto> ActiveSubstances { get; set; }

		public IEnumerable<DoseCreateDto> PassiveSubstances { get; set; }
	}
}
