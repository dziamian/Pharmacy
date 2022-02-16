﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Models.Data_Transfrom_Objects.Substance
{
	public class DoseReadDto
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public bool Active { get; set; }

		public int Dose { get; set; }
	}
}
