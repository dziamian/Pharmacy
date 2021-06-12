using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.Models.Converters;
using Pharmacy.Models.Data_Transfrom_Objects.Products;
using Pharmacy.Models.Database.Entities;
using Pharmacy.Models.Database.Repositories.Interfaces;
using Pharmacy.Services;

namespace Pharmacy.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductsService m_productsService;

        public ProductsController(ProductsService productsService)
        {
            m_productsService = productsService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateProduct(ProductCreateDto productCreateDto)
		{
            var product = await m_productsService.CreateProduct(productCreateDto);

            if (product == null)
			{
                return BadRequest();
			}

            return CreatedAtRoute(nameof(GetProductById), new { product.Id }, null);
		}

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductReadDto>>> GetAllProducts()
        {
            var products = await m_productsService.GetAllProducts();

            return Ok(products);
        }

        [HttpGet("{id}", Name = nameof(GetProductById))]
        public async Task<ActionResult<ProductReadDto>> GetProductById(int id)
        {
            var product = await m_productsService.GetProductById(id);

            if (product == null)
			{
                return NotFound();
            }

            return Ok(product);
        }
    }
}
