using Pharmacy.Models.Database.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Helpers.Selectors
{
	public class AddressRepetitionSelector
	{

		public bool TestForRepetition([AllowNull] Address original, [AllowNull] Address tested)
		{
			if (original == null || tested == null)
			{
				return false;
			}

			if (!original.City.ToLower().Equals(tested.City.ToLower()))
			{
				return false;
			}

			if (!original.PostalCode.ToLower().Equals(tested.PostalCode.ToLower()))
			{
				return false;
			}

			if (!original.StreetAndBuildingNo.ToLower().Equals(tested.StreetAndBuildingNo.ToLower()))
			{
				return false;
			}

			if (original.LocalNo != null && tested.LocalNo == null)
			{
				return false;
			}

			if (original.LocalNo == null && tested.LocalNo != null)
			{
				return false;
			}

			return original.LocalNo == null && tested.LocalNo == null || original.LocalNo.ToLower().Equals(tested.LocalNo.ToLower());
		}

		public bool Invoke([AllowNull] Address p1, [AllowNull] Address p2)
		{
			return TestForRepetition(p1, p2);
		}

	}
}
