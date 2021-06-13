using Pharmacy.Models.Converters;
using Pharmacy.Models.Data_Transfrom_Objects.Products;
using Pharmacy.Models.Database.Entities;
using Pharmacy.Models.Database.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Services
{
	public class ProductsService
	{
		private readonly IProductsRepo m_productsRepo;
		private readonly IActiveSubstancesRepo m_activeSubstancesRepo;
		private readonly IPassiveSubstancesRepo m_passiveSubstancesRepo;
		private readonly ICategoryRepo m_categoryRepo;

		public ProductsService(
			IProductsRepo productsRepo, 
			IActiveSubstancesRepo activeSubstancesRepo, 
			IPassiveSubstancesRepo passiveSubstancesRepo,
			ICategoryRepo categoryRepo)
		{
			m_productsRepo = productsRepo;
			m_activeSubstancesRepo = activeSubstancesRepo;
			m_passiveSubstancesRepo = passiveSubstancesRepo;
			m_categoryRepo = categoryRepo;
		}

		public async Task<Product> CreateProduct(ProductCreateDto productCreateDto)
		{
			if (!await ValidateProductCreateDto(productCreateDto))
			{
				return null;
			}

			Product product = new Product
			{
				Name = productCreateDto.Name,
				Cost = productCreateDto.Cost,
				Supply = 0,
				Supplement = productCreateDto.ActiveSubstances.Count() == 0,
				Description = productCreateDto.Description,
				Image = productCreateDto.Image,
				CreationDate = DateTime.Now,
				CategoryId = productCreateDto.CategoryId,
				ActiveSubstances = new List<ProductActiveSubstance>(),
				PassiveSubstances = new List<ProductPassiveSubstance>()
			};

			await m_productsRepo.CreateProduct(product);

			foreach (var it in productCreateDto.ActiveSubstances)
			{ 
				product.ActiveSubstances.Add(new ProductActiveSubstance { ActiveSubstanceId = it.Id, Dose = it.Dose });
			}

			foreach (var it in productCreateDto.PassiveSubstances)
			{
				product.PassiveSubstances.Add(new ProductPassiveSubstance { PassiveSubstanceId = it.Id, Dose = it.Dose });
			}

			await m_productsRepo.SaveChanges();
			return product;
		}

		public async Task<IEnumerable<ProductReadDto>> GetAllProducts()
		{
			return ProductConverter.ToProductReadDtos(await m_productsRepo.GetAllProducts());
		}

		public async Task<ProductReadDto> GetProductById(int id)
		{
			return ProductConverter.ToProductReadDto(await m_productsRepo.GetProductById(id));
		}

		public async Task<IEnumerable<ProductReadDto>> GetSubstitutes(int id)
		{
			return ProductConverter.ToProductReadDtos(await m_productsRepo.GetSubstitutes(id));
		}

		private async Task<bool> ValidateProductCreateDto(ProductCreateDto productCreateDto)
		{
			if (!await m_categoryRepo.CategoryExists(productCreateDto.CategoryId))
			{
				return false;
			}

			foreach (var it in productCreateDto.ActiveSubstances)
			{
				if (!await m_activeSubstancesRepo.ActiveSubstanceExists(it.Id))
				{
					return false;
				}
			}

			foreach (var it in productCreateDto.PassiveSubstances)
			{
				if (!await m_passiveSubstancesRepo.PassiveSubstanceExists(it.Id))
				{
					return false;
				}
			}

			return true;
		}

	}
}
