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
using Pharmacy.Models.Converters;
using Pharmacy.Models.Data_Transfrom_Objects.Client;
using Pharmacy.Models.Database;
using Pharmacy.Models.Database.Entities;
using Pharmacy.Models.Database.Repositories.Interfaces;

namespace Pharmacy.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : AuthControllerBase
    {
        private readonly IClientsRepo _clientsRepo;
        public AccountsController(IClientsRepo clientsRepo)
        {
            _clientsRepo = clientsRepo;
        }

        [HttpPost]
        public async Task<ActionResult> CreateAccount([FromBody] ClientCreateDto dto)
        {
            var uid = GetUID();

            var createdClient = await _clientsRepo.GetClient(uid);
            if (createdClient != null)
            {
                return Conflict();
            }

            await _clientsRepo.CreateClient(AccountsConverter.FromClientCreateDto(dto, uid));
            await _clientsRepo.SaveChanges();
            
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<ClientReadDto>> GetAccount()
        {
            var client = await _clientsRepo.GetClient(GetUID());
            if (client == null)
            {
                return NotFound();
            }
            return Ok(AccountsConverter.ToClientReadDto(client));
        }

        [HttpPatch]
        public async Task<ActionResult> UpdateAccount([FromBody] ClientUpdateDto dto)
        {
            var uid = GetUID();

            var createdClient = await _clientsRepo.GetClient(uid);
            if (createdClient == null)
            {
                return NotFound();
            }

            createdClient.Name = dto.Name;
            createdClient.Phone = dto.Phone ?? createdClient.Phone;
            createdClient.DateOfBirth = dto.DateOfBirth ?? createdClient.DateOfBirth;
            createdClient.Gender = dto.Gender ?? createdClient.Gender;

            _clientsRepo.MarkForUpdate(createdClient);
            await _clientsRepo.SaveChanges();

            return Ok();
        }
    }
}
