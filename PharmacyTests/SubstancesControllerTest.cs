using System;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using System.Threading.Tasks;

namespace PharmacyTests
{
	public class SubstancesControllerTest : IClassFixture<WebApplicationFactory<Pharmacy.Startup>>
	{
		private readonly HttpClient m_client;

		public SubstancesControllerTest(WebApplicationFactory<Pharmacy.Startup> fixture)
		{
			this.m_client = fixture.CreateClient();
		}

		[Theory]
		[InlineData("Paracetamol")]
		[InlineData("Penicillin")]
		[InlineData("Polopyrin")]
		[InlineData("Metamphetamine")]
		[InlineData("Cocaine")]
		[InlineData("LSD")]
		[InlineData("Ibuprofenum")]
		public async Task CreateActiveSubstanceTest(string name)
		{
			var substanceJson = $"{{\"Name\": \"{name}\"}}";
			var content = new StringContent(substanceJson);
			content.Headers.ContentType.MediaType = "application/json";
			var response = await m_client.PostAsync(
				"api/substances/active",
				content);
			Assert.Equal(System.Net.HttpStatusCode.Created, response.StatusCode);
		}

		[Fact]
		public async Task GetAllActiveSubstancesTest()
		{
			var response = await m_client.GetAsync("api/substances/active");
			Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
			Assert.False("".Equals(await response.Content.ReadAsStringAsync()));
		}

		[Theory]
		[InlineData(-1)]
		[InlineData(0)]
		[InlineData(1)]
		[InlineData(2)]
		[InlineData(3)]
		[InlineData(-5)]
		[InlineData(2000)]
		[InlineData(5)]
		public async Task GetActiveSubstanceByIdTest(int id)
		{
			var response = await m_client.GetAsync($"api/substances/active/{id}");
			Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
		}
	}
}
