using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PharmacyTests
{
	public class CategoriesControllerTest : IClassFixture<WebApplicationFactory<Pharmacy.Startup>>
	{
		private readonly HttpClient m_client;

		public CategoriesControllerTest(WebApplicationFactory<Pharmacy.Startup> fixture)
		{
			this.m_client = fixture.CreateClient();
		}

		[Fact]
		public async Task CreateCategoryTest()
		{
			string name = "1";
			string description = "1";
			string json = 
				$"{{" +
				$"\"Name\":\"{name}\"" +
				$"," +
				$"\"Description\":\"{description}\"" +
				$"}}";

			var content = new StringContent(json);
			content.Headers.ContentType.MediaType = "application/json";
			var result = await m_client.PostAsync("api/categories", content);
			Assert.Equal(System.Net.HttpStatusCode.OK, result.StatusCode);
		}

		[Fact]
		public async Task GetAllCategoriesTest()
		{
			var response = await m_client.GetAsync("api/categories");
			Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
			Assert.NotEqual("", await response.Content.ReadAsStringAsync());
		}

		[Theory]
		[InlineData(1)]
		[InlineData(0)]
		[InlineData(2)]
		[InlineData(2000)]
		[InlineData(-1)]
		[InlineData(-3)]
		[InlineData(-2000)]
		public async Task GetCategoryByIdTest(int id)
		{
			var response = await m_client.GetAsync($"api/categories/{id}");
			Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
		}
	}
}