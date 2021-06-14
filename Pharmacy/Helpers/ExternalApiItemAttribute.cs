using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Helpers
{
	public class ExternalApiItemAttribute : System.Attribute
	{
		public string ApiName { get; set; }
	}
}
