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

		[Theory]
		[InlineData("eyJhbGciOiJSUzI1NiIsImtpZCI6ImRjNGQwMGJjM2NiZWE4YjU0NTMzMWQxZjFjOTZmZDRlNjdjNTFlODkiLCJ0eXAiOiJKV1QifQ.eyJpc3MiOiJodHRwczovL3NlY3VyZXRva2VuLmdvb2dsZS5jb20vcGhhcm1hY3ktMzEzMDEwIiwiYXVkIjoicGhhcm1hY3ktMzEzMDEwIiwiYXV0aF90aW1lIjoxNjIzNzUzMzk0LCJ1c2VyX2lkIjoiNjhGVWpxN0RQbGROOWtBcG1UV0xiemY3a1RYMiIsInN1YiI6IjY4RlVqcTdEUGxkTjlrQXBtVFdMYnpmN2tUWDIiLCJpYXQiOjE2MjM3NTMzOTQsImV4cCI6MTYyMzc1Njk5NCwiZW1haWwiOiJkYW1pYW5AdGVzdC5jb20iLCJlbWFpbF92ZXJpZmllZCI6ZmFsc2UsImZpcmViYXNlIjp7ImlkZW50aXRpZXMiOnsiZW1haWwiOlsiZGFtaWFuQHRlc3QuY29tIl19LCJzaWduX2luX3Byb3ZpZGVyIjoicGFzc3dvcmQifX0.tCmoPWwfXiaRSTfGGW0n85wYkWoI4ZptCXx6hS8nZTfizoaG9B34AHoHQ4i4Q0GWnyW56fjF0M4UQJ1If1HOxUq2a7JoP1wQmjfdo4VkTidUCHhwLx4mQ7t5pjDg6Gc9yuNg5owdcGZkdXA_ZUTzotV7Hc8HE4t0Krh1eN5T3xmuRM_n9GkAE7sC-1abQXPrnPPtXeMbwyLM8Fw9Y-3XH6wRef3Xdd9Hyxz-VUTwt_La6XkXv8in10yWqilZiEliEeleY0hM8qWKyjarB7MWdQ3OSOvenSm3vw6SwX4B_4lGk4sNf3kj9KsRe7iAfmRrpHhpPYzbQ8D6pGqYxbqUgw")]
		public async Task GetCartTest(string token)
		{
			m_client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
			var response = await m_client.GetAsync("api/cart");
			Assert.NotEqual(System.Net.HttpStatusCode.Unauthorized, response.StatusCode);
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
