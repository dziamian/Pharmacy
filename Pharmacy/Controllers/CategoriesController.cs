using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.Models.Converters;
using Pharmacy.Models.Data_Transfrom_Objects.Category;
using Pharmacy.Models.Database.Entities;
using Pharmacy.Models.Database.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Controllers
{
	[Route("api/categories")]
	[ApiController]
	public class CategoriesController : ControllerBase
	{
		private readonly ICategoryRepo m_categoryRepo;

		public CategoriesController(ICategoryRepo categoryRepo)
		{
			m_categoryRepo = categoryRepo;
		}

		[HttpPost]
		public async Task<ActionResult> CreateCategory(CategoryCreateDto categoryCreateDto)
		{
			Category category = new Category 
			{ 
				Name = categoryCreateDto.Name, 
				Description = categoryCreateDto.Description 
			};

			await m_categoryRepo.CreateCategory(category);
			m_categoryRepo.SaveChanges();

			return CreatedAtRoute(nameof(GetCategoryById), new { id = category.Id }, null);
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<CategoryReadDto>>> GetAllCategories()
		{
			return Ok(CategoryConverter.ToCategoryReadDtos(await m_categoryRepo.GetAllCategories()));
		}

		[HttpGet("{id}", Name = nameof(GetCategoryById))]
		public async Task<ActionResult<CategoryReadDto>> GetCategoryById(int id)
		{
			var category = await m_categoryRepo.GetCategoryById(id);

			if (category == null)
			{
				return NotFound();
			}

			return Ok(CategoryConverter.ToCategoryReadDto(category));
		}
	}
}
