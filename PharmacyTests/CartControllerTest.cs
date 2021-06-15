using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
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
	}
}
