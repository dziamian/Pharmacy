using Microsoft.EntityFrameworkCore;
using Pharmacy.Models.Database.Entities;
using System;
using System.Linq;

namespace Pharmacy.Models.Database
{
    public class PharmacyDBContext : DbContext
    {
		public DbSet<Client> Clients { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<CartItem> CartItems { get; set; }

		public DbSet<Address> Addresses { get; set; }
		public DbSet<ActiveSubstance> ActiveSubstances { get; set; }
		public DbSet<PassiveSubstance> PassiveSubstances { get; set; }
		public DbSet<ProductActiveSubstance> ProductActiveSubstances { get; set; }
		public DbSet<ProductPassiveSubstance> ProductPassiveSubstances { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderProduct> OrderProducts { get; set; }
		public DbSet<Rating> Ratings { get; set; }

		public PharmacyDBContext(DbContextOptions<PharmacyDBContext> options) : base(options) 
		{
		
		}
	}
}
