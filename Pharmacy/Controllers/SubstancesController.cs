using Microsoft.AspNetCore.Mvc;
using Pharmacy.Models.Data_Transfrom_Objects.Substance;
using Pharmacy.Models.Database.Repositories.Interfaces;
using Pharmacy.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pharmacy.Controllers
{
	[Route("api/substances/")]
	[ApiController]
	public class SubstancesController : ControllerBase
	{

		private readonly SubstancesService m_substancesService;

		public SubstancesController(SubstancesService substancesService)
		{
			m_substancesService = substancesService;
		}

		[HttpPost]
		[Route("active/")]
		public async Task<ActionResult> CreateActiveSubstance([FromBody] SubstanceCreateDto activeSubstanceCreateDto)
		{
			var substance = await m_substancesService.CreateActiveSubstance(activeSubstanceCreateDto);
			return CreatedAtRoute(nameof(GetActiveSubstanceById), new { substance.Id }, null);
		}

		[HttpGet]
		[Route("active/")]
		public async Task<ActionResult<IEnumerable<SubstanceReadDto>>> GetAllActiveSubstances()
		{
			return Ok(await m_substancesService.GetAllActiveSubstances());
		}

		[Route("active/")]
		[HttpGet("{id}", Name = nameof(GetActiveSubstanceById))]
		public async Task<ActionResult<SubstanceReadDto>> GetActiveSubstanceById(int id)
		{
			var substance = await m_substancesService.GetActiveSubstanceById(id);

			if (substance == null)
			{
				return NotFound();
			}

			return Ok(substance);
		}
	}
}
