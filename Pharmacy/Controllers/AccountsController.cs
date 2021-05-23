using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.Helpers;
using Pharmacy.Models.Data_Transform_Objects;
using Pharmacy.Models.Database;
using Pharmacy.Models.Database.Entities;

namespace Pharmacy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly PharmacyDBContext _dbContext;
        private readonly UserManager<Client> _userManager;
        private readonly IMapper _mapper;

        public AccountsController(UserManager<Client> userManager, IMapper mapper, PharmacyDBContext dBContext)
        {
            _userManager = userManager;
            _mapper = mapper;
            _dbContext = dBContext;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RegistrationDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userIdentity = _mapper.Map<Client>(dto);
            var result = await _userManager.CreateAsync(userIdentity, dto.Password);

            if (!result.Succeeded) return new BadRequestObjectResult(Errors.AddErrorsToModelState(result, ModelState));

            //await _dbContext.Users.AddAsync(new Client { });

            return new OkObjectResult("Account Created");
        }
    }
}
