﻿using Pharmacy.Models.Data_Transfrom_Objects.Substances;
using Pharmacy.Models.Database.Entities;
using System.Collections.Generic;

namespace Pharmacy.Models.Converters
{
	public class ActiveSubstanceConverter
	{
		public static SubstanceReadDto ToSubstanceReadDto(ActiveSubstance activeSubstance)
		{
			return activeSubstance != null ? new SubstanceReadDto { Id = activeSubstance.Id, Name = activeSubstance.Name } : null;
		}

		public static IEnumerable<SubstanceReadDto> ToSubstanceReadDtos(IEnumerable<ActiveSubstance> activeSusbtances)
		{
			List<SubstanceReadDto> list = new List<SubstanceReadDto>();
			foreach (var it in activeSusbtances)
			{
				list.Add(ToSubstanceReadDto(it));
			}
			return list;
		}
	}
}
