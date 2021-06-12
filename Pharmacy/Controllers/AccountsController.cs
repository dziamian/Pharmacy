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
        public async Task<ActionResult> CreateAccount(ClientCreateDto client)
        {
            var uid = GetUID();

            var createdClient = await _clientsRepo.GetClient(uid);
            if (createdClient != null)
            {
                return BadRequest();
                //remove account from firebase
            }

            await _clientsRepo.CreateClient(ClientsConverter.FromClientCreateDto(client, uid));
            await _clientsRepo.SaveChanges();
            
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<ClientReadDto>> GetAccount()
        {
            var client = await _clientsRepo.GetClient(GetUID());
            if (client == null)
            {
                return BadRequest();
            }
            return Ok(ClientsConverter.ToClientReadDto(client));
        }
    }
}
