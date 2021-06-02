using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Controllers.BaseControllers;
using Pharmacy.Models.Database;
using Pharmacy.Models.Database.Entities;

namespace Pharmacy.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : AuthControllerBase
    {

        public ProfileController()
        {

        }

        [HttpGet]
        public IActionResult Home()
        {
            Console.WriteLine(HttpContext.User.Claims.Single(x => x.Type == "firebase"));
            Console.WriteLine(GetUID());

            return Ok("XD");

            /*return new OkObjectResult(new 
            { 
                Message = "Secured Message",
                user.Name,
                user.Email,
                user.PhoneNumber
            });*/
        }
    }
}
