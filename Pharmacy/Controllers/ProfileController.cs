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
using Pharmacy.Models.Database;
using Pharmacy.Models.Database.Entities;

namespace Pharmacy.Controllers
{
    [Authorize]
    //[Authorize(Policy = "ApiUser")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        //private readonly ClaimsPrincipal _caller;
        private readonly PharmacyDBContext _dbContext;

        public ProfileController(PharmacyDBContext dbContext)
        {
            //_caller = httpContextAccessor.HttpContext.User;
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Home()
        {
            /*var userId = _caller.Claims.Single(c => c.Type == "id");
            var user = _dbContext.Users.SingleAsync(c => c.Id == userId.Value).Result;*/

            //var user = _caller.Claims.FirstOrDefault();

            Console.WriteLine(HttpContext.User.Claims.Single(x => x.Type == "firebase"));
            Console.WriteLine(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value); //Get UID from Firebase Access Token -> it will be used in our database.

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
