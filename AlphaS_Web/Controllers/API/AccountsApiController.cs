using AlphaS_Web.Areas.Identity.Data;
using AlphaS_Web.Models.Requests;
using AlphaS_Web.Services.ApiAuthentication;
using AspNetCore.Identity.Mongo.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AlphaS_Web.Controllers.API
{
    [Route("api/Accounts")]
    [ApiController]
    public class AccountsApiController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<MongoRole> roleManager;

        public AccountsApiController(UserManager<ApplicationUser> userManager, RoleManager<MongoRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest model, [FromServices] IJwtSigningEncodingKey signingEncodingKey)
        {
            var user = await userManager.FindByNameAsync(model.UserName);
            if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name as string, user.UserName as string),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role as string, userRole as string));
                }

                var token = new JwtSecurityToken(
                    issuer: "AlphaS",
                    audience: "AlphaSClient",
                    claims: authClaims,
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: new SigningCredentials(
                    signingEncodingKey.GetKey(),
                    signingEncodingKey.SigningAlgorithm)
                        );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return Unauthorized();
        }
    }
}
