using Pharmacy.Models.Data_Transfrom_Objects.Substance;
using Pharmacy.Models.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Models.Converters
{
	public class PassiveSubstanceConverter
	{
		public static SubstanceReadDto ToSubstanceReadDto(PassiveSubstance passiveSubstance)
		{
			return passiveSubstance != null ? new SubstanceReadDto { Id = passiveSubstance.Id, Name = passiveSubstance.Name } : null;
		}

		public static IEnumerable<SubstanceReadDto> ToSubstanceReadDtos(IEnumerable<PassiveSubstance> passiveSubstances)
		{
			List<SubstanceReadDto> list = new List<SubstanceReadDto>();
			foreach (var it in passiveSubstances)
			{
				list.Add(ToSubstanceReadDto(it));
			}
			return list;
		}

		public static DoseReadDto ToSubstanceDoseDto(PassiveSubstance passiveSubstance, int dose)
		{
			return passiveSubstance != null && dose > 0 ?
				new DoseReadDto
				{
					Id = passiveSubstance.Id,
					Name = passiveSubstance.Name,
					Active = false,
					Dose = dose
				} :
				null;
		}
	}
}
