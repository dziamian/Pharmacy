using Pharmacy.Models.Data_Transfrom_Objects.Products;
using Pharmacy.Models.Data_Transfrom_Objects.Substances;
using Pharmacy.Models.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Models.Converters
{
	public class ProductConverter
	{
		public static ProductReadDto ToProductReadDto(Product product)
		{
			if (product == null)
			{
				return null;
			}

			ICollection<SubstanceDoseDto> activeSubstances = new List<SubstanceDoseDto>();
			ICollection<SubstanceDoseDto> passiveSubstances = new List<SubstanceDoseDto>();

			foreach (var it in product.ActiveSubstances)
			{
				activeSubstances.Add(new SubstanceDoseDto { SubstanceId = it.ActiveSubstanceId, Dose = it.Dose });
			}

			foreach (var it in product.PassiveSubstances)
			{
				passiveSubstances.Add(new SubstanceDoseDto { SubstanceId = it.PassiveSubstanceId, Dose = it.Dose });
			}

			return new ProductReadDto 
			{ 
				Id = product.Id, 
				Name = product.Name,
				Cost = product.Cost,
				Description = product.Description,
				Supply = product.Supply,
				Image = product.Image,
				CategoryId = product.CategoryId,
				ActiveSubstances = activeSubstances,
				PassiveSubstances = passiveSubstances
			};
		}

		public static IEnumerable<ProductReadDto> ToProductReadDtos(IEnumerable<Product> products)
		{
			ICollection<ProductReadDto> collection = new List<ProductReadDto>();

			if (products == null)
			{
				return collection;
			}

			foreach (var it in products)
			{
				collection.Add(ToProductReadDto(it));
			}

			return collection;
		}
	}
}
