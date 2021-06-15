using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace PharmacyTests
{
	public class ProductsControllerTest : IClassFixture<WebApplicationFactory<Pharmacy.Startup>>
	{
		private readonly HttpClient m_client;

		public ProductsControllerTest(WebApplicationFactory<Pharmacy.Startup> fixture)
		{
			this.m_client = fixture.CreateClient();
		}

        [Theory]
        [InlineData("1", 50, "", "First", 1, "1, 2, 3", "")]
        [InlineData("2", 100, "", "Second", 1, "1", "1, 2, 3")]
        [InlineData("3", 200, "", "Third", 1, "2", "1, 2")]
        [InlineData("4", 300, "", "Fourth", 1, "", "1, 2, 3")]
		public async Task CreateProductTest(string name, int cost, string image, string desc, int category, string actives, string passives)
		{
            string json = 
                $"{{" +
                $"\"Name\":\"{name}\"" +
                $"," +
                $"\"Cost\":{cost}" +
                $"," + 
                $"\"Image\":\"{image}\"" +
                $"," + 
                $"\"Desc\":\"{desc}\"" +
                $"," +
                $"\"CategoryId\":{category}" +
                $"," +
                $"\"ActiveSubstances\":[{actives}]" +
                $"," +
                $"\"PassiveSubstances\":[{passives}]" +
                $"}}";
            var content = new StringContent(json);
            content.Headers.ContentType.MediaType = "application/json";
            var postResponse = await m_client.PostAsync("api/products", content);
            Assert.Equal(System.Net.HttpStatusCode.Created, postResponse.StatusCode);
            var getResponse = await m_client.GetAsync(postResponse.Headers.Location);
            Assert.Equal(System.Net.HttpStatusCode.OK, getResponse.StatusCode);
		}

        [Fact]
		public async Task GetAllProductsTest()
        {
            var response = await m_client.GetAsync("api/products");
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(50)]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-7)]
        [InlineData(-2000)]
        public async Task GetNewestProductsTest(int count)
        {
            var response = await m_client.GetAsync($"api/products/newest?count={count}");
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(5)]
        [InlineData(21)]
        [InlineData(22)]
        [InlineData(2000)]
        [InlineData(2001)]
        [InlineData(-1)]
        [InlineData(-3)]
        public async Task GetProductByIdTest(int id)
        {
            var response = await m_client.GetAsync($"api/products/{id}");
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(4)]
        [InlineData(18)]
        [InlineData(19)]
        [InlineData(20)]
        public async Task GetSubstitutesTest(int id)
        {
            var response = await m_client.GetAsync($"api/products/{id}/substitutes");
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData(new int[] { 1 }, "", 0, 2000)]
        [InlineData(new int[] { }, "Para", 0, 2000)]
        public async Task GetSpecificProductsTest(int[] categoryIds, string name, int minPrice, int maxPrice)
        {
            var json = JsonSerializer.Serialize(new
            {
                Name = name,
                MinPrice = minPrice,
                MaxPrice = maxPrice,
                CategoryIds = categoryIds ?? new int[] { },
                ActiveSubstances = new int[] { },
                PassiveSubstances = new int[] { },
            });
            var content = new StringContent(json);
            content.Headers.ContentType.MediaType = "application/json";
            var response = await m_client.PostAsync($"api/products/filter/all", content);
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

    }
}
