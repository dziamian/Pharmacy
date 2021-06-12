using Pharmacy.Models.Data_Transfrom_Objects.Category;
using Pharmacy.Models.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Models.Converters
{
	public class CategoryConverter
	{
		public static CategoryReadDto ToCategoryReadDto(Category category)
		{
			return new CategoryReadDto 
			{ 
				Id = category.Id, 
				Name = category.Name, 
				Description = category.Description 
			};
		}

		public static IEnumerable<CategoryReadDto> ToCategoryReadDtos(IEnumerable<Category> categories)
		{
			var collection = new List<CategoryReadDto>();

			foreach (var it in categories)
			{
				collection.Add(ToCategoryReadDto(it));
			}

			return collection;
		}

	}
}
