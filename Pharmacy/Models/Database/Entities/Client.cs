﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Models.Database.Entities
{
    public class Client : IdentityUser
    {
        public string Name { get; set; }
        public string GoogleId { get; set; }
    }
}