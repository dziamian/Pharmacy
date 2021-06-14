using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Pharmacy.Models.Converters;
using Pharmacy.Models.Data_Transfrom_Objects.Product;
using Pharmacy.Models.Database.Entities;
using Pharmacy.Models.Database.Repositories.Interfaces;
using Pharmacy.Services;

namespace Pharmacy.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductsService m_productsService;

        public ProductsController(ProductsService productsService)
        {
            m_productsService = productsService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateProduct([FromBody] ProductCreateDto productCreateDto)
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

        [HttpGet("newest")]
        public async Task<ActionResult<IEnumerable<ProductReadDto>>> GetNewestProducts([FromQuery(Name = "count")] int count)
        {
            var newestProducts = await m_productsService.GetNewestProducts(count);

            if (count <= 0)
            {
                return BadRequest();
            }

            return Ok(newestProducts);
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

        [HttpGet("{id}/substitutes", Name = nameof(GetSubstitutes))]
        public async Task<ActionResult<IEnumerable<ProductReadDto>>> GetSubstitutes(int id)
        {
            var substitutes = await m_productsService.GetSubstitutes(id);

            if (substitutes == null)
            {
                return NotFound();
            }

            return Ok(substitutes);
        }

        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<ProductReadDto>>> GetPagedSpecificProducts(
            [FromQuery(Name = "pageIndex")] int pageIndex,
            [FromQuery(Name = "pageSize")] int pageSize,
            [FromBody] ProductsFilterDto filter)
		{
            var list = await m_productsService.GetPagedSpecificProducts(pageIndex, pageSize, filter);

            if (list == null)
			{
                return BadRequest();
			}

            var meta = new
            {
                list.TotalSize,
                list.PageSize,
                list.TotalPages,
                list.CurrentPage,
                list.HasNext,
                list.HasPrevious
            };

            Response.Headers.Add("Pagination", JsonConvert.SerializeObject(meta));

            return Ok(list);
		}

        public async Task<ActionResult<IEnumerable<ProductReadDto>>> GetSpecificProducts(
            ProductsFilterDto filter)
		{
            var products = await m_productsService.GetSpecificProducts(filter);

            if (products == null)
			{
                return BadRequest();
			}

            return Ok(products);
		}
    }
}
