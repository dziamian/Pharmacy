using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Models.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Models.Database
{
    public class PharmacyDBContext : DbContext
    {
        public PharmacyDBContext(DbContextOptions<PharmacyDBContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
    }
}
