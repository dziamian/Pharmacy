using Pharmacy.Models.Converters;
using Pharmacy.Models.Data_Transfrom_Objects.Substance;
using Pharmacy.Models.Database.Entities;
using Pharmacy.Models.Database.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Services
{
	public class SubstancesService
	{
		private readonly IActiveSubstancesRepo m_activeSubstanceRepo;
		private readonly IPassiveSubstancesRepo m_passiveSubstanceRepo;

		public SubstancesService(IActiveSubstancesRepo activeSubstancesRepo, IPassiveSubstancesRepo passiveSubstancesRepo)
		{
			m_activeSubstanceRepo = activeSubstancesRepo;
			m_passiveSubstanceRepo = passiveSubstancesRepo;
		}

		public SubstancesService(IActiveSubstancesRepo activeSubstancesRepo)
		{
			m_activeSubstanceRepo = activeSubstancesRepo;
		}

		public async Task<ActiveSubstance> CreateActiveSubstance(SubstanceCreateDto substanceCreateDto)
		{
			var substance = new ActiveSubstance { Name = substanceCreateDto.Name };
			await m_activeSubstanceRepo.CreateActiveSubstance(substance);
			await m_activeSubstanceRepo.SaveChanges();
			return substance;
		}

		public async Task<IEnumerable<SubstanceReadDto>> GetAllActiveSubstances()
		{
			return ActiveSubstanceConverter.ToSubstanceReadDtos(await m_activeSubstanceRepo.GetAllActiveSubstances());
		}

		public async Task<SubstanceReadDto> GetActiveSubstanceById(int id)
		{
			return ActiveSubstanceConverter.ToSubstanceReadDto(await m_activeSubstanceRepo.GetActiveSubstanceById(id));
		}

		public async Task<PassiveSubstance> CreatePassiveSubstance(SubstanceCreateDto substanceCreateDto)
		{
			var substance = new PassiveSubstance { Name = substanceCreateDto.Name };
			await m_passiveSubstanceRepo.CreatePassiveSubstance(substance);
			await m_passiveSubstanceRepo.SaveChanges();
			return substance;
		}

		public async Task<IEnumerable<SubstanceReadDto>> GetAllPassiveSubstances()
		{
			return PassiveSubstanceConverter.ToSubstanceReadDtos(await m_passiveSubstanceRepo.GetAllPassiveSubstances());
		}

		public async Task<SubstanceReadDto> GetPassiveSubstanceById(int id)
		{
			return PassiveSubstanceConverter.ToSubstanceReadDto(await m_passiveSubstanceRepo.GetActiveSubstanceById(id));
		}

	}
}
