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

		[Fact]
		public async Task CreateActiveSubstanceTest()
		{
			var response = await m_client.GetAsync("/api/substances/active");
			var str = await response.Content.ReadAsStringAsync();
			throw new Exception();
			Console.WriteLine(str);

		}

		[Fact]
		public async Task GetAllActiveSubstancesTest()
		{

		}

		[Fact]
		public async Task GetActiveSubstanceByIdTest()
		{

		}
	}
}
