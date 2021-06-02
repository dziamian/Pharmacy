﻿using Pharmacy.Models.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Models.Database.Repositories
{
    public interface IProductsRepo
    {
        IEnumerable<Product> GetAllProducts();
        Product GetProductById(int id);
    }
}