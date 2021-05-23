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
using Pharmacy.Models.Database;
using Pharmacy.Models.Database.Entities;

namespace Pharmacy.Controllers
{
    [Authorize(Policy = "ApiUser")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly ClaimsPrincipal _caller;
        private readonly PharmacyDBContext _dbContext;

        public ProfileController(UserManager<Client> userManager, PharmacyDBContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _caller = httpContextAccessor.HttpContext.User;
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Home()
        {
            var userId = _caller.Claims.Single(c => c.Type == "id");
            var user = _dbContext.Users.Single(c => c.Id == userId.Value); //should be SingleAsync but not working...

            return new OkObjectResult(new 
            { 
                Message = "Secured Message",
                user.FirstName,
                user.LastName,
                user.Email,
                user.PhoneNumber
            });
        }
    }
}
