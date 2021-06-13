using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Pharmacy.Helpers;

namespace Pharmacy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IOptions<Contact> _contact;

        public ContactController(IOptions<Contact> contact)
        {
            _contact = contact;
        }

        [HttpGet]
        public ActionResult<Contact> GetContactInfo()
        {
            if (_contact.Value == null)
            {
                return NotFound();
            }
            return Ok(_contact.Value);
        }
    }
}
