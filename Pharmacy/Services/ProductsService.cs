﻿using Pharmacy.Helpers;
using Pharmacy.Models.Converters;
using Pharmacy.Models.Data_Transfrom_Objects.Product;
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
				product.ActiveSubstances.Add(new ProductActiveSubstance { ActiveSubstanceId = it.SubstanceId, Dose = it.Amount });
			}

			foreach (var it in productCreateDto.PassiveSubstances)
			{
				product.PassiveSubstances.Add(new ProductPassiveSubstance { PassiveSubstanceId = it.SubstanceId, Dose = it.Amount });
			}

			await m_productsRepo.SaveChanges();
			return product;
		}

		public async Task<IEnumerable<ProductReadDto>> GetAllProducts()
		{
			return ProductConverter.ToProductReadDtos(await m_productsRepo.GetAllProducts());
		}

		public async Task<IEnumerable<ProductReadDto>> GetNewestProducts(int count)
		{
			return ProductConverter.ToProductReadDtos(await m_productsRepo.GetNewestProducts(count >= 1 ? count : 1));
		}

		public async Task<ProductReadDto> GetProductById(int id)
		{
			return ProductConverter.ToProductReadDto(await m_productsRepo.GetProductById(id));
		}

		public async Task<PagedList<ProductReadDto>> GetPagedSpecificProducts(int pageIndex, int pageSize, ProductsFilterDto filter)
		{
			var productReadDtos = await this.GetSpecificProducts(filter);
			return PagedList<ProductReadDto>.ToPagedList(productReadDtos, pageIndex, pageSize);
		}

		public async Task<IEnumerable<ProductReadDto>> GetSpecificProducts(ProductsFilterDto filter)
		{
			List<string> categories = new List<string>();
			var actives = new List<(int, int)>();
			var passives = new List<(int, int)>();

			if (filter.Categories != null)
			{
				foreach (var it in filter.Categories)
				{
					categories.Add(it);
				}
			}

			if (filter.ActiveSubstances != null)
			{
				foreach (var it in filter.ActiveSubstances)
				{
					actives.Add((it.SubstanceId, it.Amount));
				}
			}

			if (filter.PassiveSubstances != null)
			{
				foreach (var it in filter.PassiveSubstances)
				{
					passives.Add((it.SubstanceId, it.Amount));
				}
			}

			var products = await m_productsRepo.GetSpecificProducts(
					filter.Name,
					filter.SortingPropertyName,
					filter.SortDescending,
					filter.MinPrice,
					filter.MaxPrice,
					categories,
					actives,
					passives);

			return ProductConverter.ToProductReadDtos(products);
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

	}
}
