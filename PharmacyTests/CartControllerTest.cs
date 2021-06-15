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
	public class CartControllerTest : IClassFixture<WebApplicationFactory<Pharmacy.Startup>>
	{
		private readonly HttpClient m_client;

		public CartControllerTest(WebApplicationFactory<Pharmacy.Startup> fixture)
		{
			this.m_client = fixture.CreateClient();
		}

		[Fact]
		public async Task GetCartTest()
		{
			var response = await m_client.GetAsync("api/cart");
			Assert.Equal(System.Net.HttpStatusCode.Unauthorized, response.StatusCode);
		}

		[Fact]
		public async Task GetCartItemTest()
		{
			int id = 1;
			var response = await m_client.GetAsync($"api/cart/{id}");
			Assert.Equal(System.Net.HttpStatusCode.Unauthorized, response.StatusCode);
		}

		[Fact]
		public async Task GetNumberOfItemsInCartTest()
		{
			var response = await m_client.GetAsync("api/cart/size");
			Assert.Equal(System.Net.HttpStatusCode.Unauthorized, response.StatusCode);
		}

		[Fact]
		public async Task ValidateCartTest()
		{
			var response = await m_client.GetAsync("api/cart/validate");
			Assert.Equal(System.Net.HttpStatusCode.Unauthorized, response.StatusCode);
		}

		[Fact]
		public async Task AddItemToCartTest()
		{
			string json = JsonSerializer.Serialize(new { ProductId = 1, Amount = 1 });
			var content = new StringContent(json);
			content.Headers.ContentType.MediaType = "application/json";
			var response = await m_client.PutAsync("api/cart", content);
			Assert.Equal(System.Net.HttpStatusCode.Unauthorized, response.StatusCode);
		}

		[Fact]
		public async Task RemoveItemFromCartTest()
		{
			int id = 1;
			var response = await m_client.DeleteAsync($"api/cart/remove/{id}");
			Assert.Equal(System.Net.HttpStatusCode.Unauthorized, response.StatusCode);
		}

		[Fact]
		public async Task RemoveCartTest()
		{
			var response = await m_client.DeleteAsync("api/cart/remove");
			Assert.Equal(System.Net.HttpStatusCode.Unauthorized, response.StatusCode);
		}
	}
}
