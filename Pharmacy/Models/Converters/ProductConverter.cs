using Pharmacy.Models.Data_Transfrom_Objects.Product;
using Pharmacy.Models.Data_Transfrom_Objects.Substance;
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

			ICollection<DoseReadDto> activeSubstances = new List<DoseReadDto>();
			ICollection<DoseReadDto> passiveSubstances = new List<DoseReadDto>();

			foreach (var it in product.ActiveSubstances)
			{
				activeSubstances.Add(ActiveSubstanceConverter.ToSubstanceDoseDto(it.ActiveSubstance, it.Dose));
			}

			foreach (var it in product.PassiveSubstances)
			{
				passiveSubstances.Add(PassiveSubstanceConverter.ToSubstanceDoseDto(it.PassiveSubstance, it.Dose));
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

		public static CartProductReadDto ToCartProductReadDto(Product product)
        {
			return new CartProductReadDto {
				Id = product.Id,
				Name = product.Name,
				Cost = product.Cost,
				Image = product.Image
			};
        }

		public static IEnumerable<ProductReadDto> ToProductReadDtos(IEnumerable<Product> products)
		{
			if (products == null)
			{
				return null;
			}

			ICollection<ProductReadDto> collection = new List<ProductReadDto>();

			foreach (var it in products)
			{
				collection.Add(ToProductReadDto(it));
			}

			return collection;
		}

		public static IEnumerable<CartProductReadDto> ToCartProductReadDtos(IEnumerable<Product> products)
		{
			ICollection<CartProductReadDto> collection = new List<CartProductReadDto>();

			if (products == null)
			{
				return collection;
			}

			foreach (var it in products)
			{
				collection.Add(ToCartProductReadDto(it));
			}

			return collection;
		}
	}
}
