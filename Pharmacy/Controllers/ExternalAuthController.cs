using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Pharmacy.Auth;
using Pharmacy.Helpers;
using Pharmacy.Models;
using Pharmacy.Models.Data_Transform_Objects;
using Pharmacy.Models.Database;
using Pharmacy.Models.Database.Entities;

namespace Pharmacy.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ExternalAuthController : ControllerBase
    {
        private readonly PharmacyDBContext _dbContext;
        private readonly UserManager<Client> _userManager;
        private readonly GoogleAuthSettings _googleAuthSettings;
        private readonly IJwtFactory _jwtFactory;
        private readonly JwtIssuerOptions _jwtOptions;
        private static readonly HttpClient Client = new HttpClient();

        public ExternalAuthController(IOptions<GoogleAuthSettings> googleAuthSettings, UserManager<Client> userManager, PharmacyDBContext dbContext, IJwtFactory jwtFactory, IOptions<JwtIssuerOptions> jwtOptions)
        {
            _googleAuthSettings = googleAuthSettings.Value;
            _userManager = userManager;
            _dbContext = dbContext;
            _jwtFactory = jwtFactory;
            _jwtOptions = jwtOptions.Value;
        }

        [HttpPost]
        public async Task<IActionResult> Google([FromBody] GoogleAuthDTO dto)
        {
            var userInfoResponse = await Client.GetAsync($"https://www.googleapis.com/oauth2/v3/userinfo?access_token={dto.AccessToken}"); //post maybe?

            if (!userInfoResponse.IsSuccessStatusCode)
            {
                return BadRequest(Errors.AddErrorToModelState("login_failure", "Invalid Google token.", ModelState));
            }
            string userInfoData = await userInfoResponse.Content.ReadAsStringAsync();
            var userInfo = JsonConvert.DeserializeObject<GoogleAccessUserInfo>(userInfoData);

            var user = await _userManager.FindByEmailAsync(userInfo.Email);

            if (user == null)
            {
                var client = new Client
                {
                    Name = userInfo.Name,
                    GoogleId = userInfo.Sub,
                    Email = userInfo.Email,
                    UserName = userInfo.Email
                };

                var result = await _userManager.CreateAsync(client, Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 8));

                if (!result.Succeeded)
                {
                    return new BadRequestObjectResult(Errors.AddErrorsToModelState(result, ModelState));
                }

                await _dbContext.SaveChangesAsync();
            }

            var localUser = await _userManager.FindByNameAsync(userInfo.Email);

            if (localUser == null)
            {
                return BadRequest(Errors.AddErrorToModelState("login_failure", "Failed to create local user account.", ModelState));
            }

            var jwt = await Tokens.GenerateJwt(_jwtFactory.GenerateClaimsIdentity(localUser.UserName, localUser.Id), _jwtFactory, localUser.UserName, _jwtOptions, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
    }
}
