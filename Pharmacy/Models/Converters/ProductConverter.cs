﻿using Pharmacy.Models.Data_Transfrom_Objects.Products;
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
