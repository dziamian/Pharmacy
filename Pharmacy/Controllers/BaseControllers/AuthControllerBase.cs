using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Pharmacy.Controllers.BaseControllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthControllerBase : ControllerBase
    {
        public string GetUID()
        {
            return HttpContext.User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
        }
    }
}
