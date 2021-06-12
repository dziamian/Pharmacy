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

		private async Task<bool> ValidateProductCreateDto(ProductCreateDto productCreateDto)
		{
			foreach (var it in productCreateDto.ActiveSubstances)
			{
				if (!await m_activeSubstancesRepo.ActiveSubstanceExists(it.SubstanceId))
				{
					return false;
				}
			}

			foreach (var it in productCreateDto.PassiveSubstances)
			{
				if (!await m_passiveSubstancesRepo.PassiveSubstanceExists(it.SubstanceId))
				{
					return false;
				}
			}

			return true;
		}

		public ProductsService(IProductsRepo productsRepo, IActiveSubstancesRepo activeSubstancesRepo, IPassiveSubstancesRepo passiveSubstancesRepo)
		{
			m_productsRepo = productsRepo;
			m_activeSubstancesRepo = activeSubstancesRepo;
			m_passiveSubstancesRepo = passiveSubstancesRepo;
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
				ActiveSubstances = new List<ProductActiveSubstance>(),
				PassiveSubstances = new List<ProductPassiveSubstance>()
			};

			await m_productsRepo.CreateProduct(product);

			foreach (var it in productCreateDto.ActiveSubstances)
			{
				product.ActiveSubstances.Add(new ProductActiveSubstance { ActiveSubstanceId = it.SubstanceId, Dose = it.Dose });
			}

			foreach (var it in productCreateDto.PassiveSubstances)
			{
				product.PassiveSubstances.Add(new ProductPassiveSubstance { PassiveSubstanceId = it.SubstanceId, Dose = it.Dose });
			}

			m_productsRepo.SaveChanges();
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

	}
}
