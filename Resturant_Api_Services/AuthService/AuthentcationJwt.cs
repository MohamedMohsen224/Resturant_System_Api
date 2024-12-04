using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Resturant_Api_Core.Entites.User;
using Resturant_Api_Core.Services.AuthServices;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Resturant_Api_Services.AuthService
{
    public class AuthentcationJwt : IAuthServices
    {
        private readonly IConfiguration configuration;

        public AuthentcationJwt(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public async Task<string> CreateToken(AppUser user, UserManager<AppUser> userManager)
        {
            var claim = new List<Claim>
            {
                new Claim(ClaimTypes.GivenName,user.UserName),
                new Claim(ClaimTypes.Email,user.Email)
            };

            var UserRoles = await userManager.GetRolesAsync(user);
            foreach(var role in UserRoles)
            {
                claim.Add(new Claim(ClaimTypes.Role,role));
            }

            var AuthKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["MySuperSecretKey"]));
            var Token = new JwtSecurityToken(
                audience: configuration["Jwt:validAudience"],
                issuer: configuration["Jwt:Issuer"],
                expires: DateTime.UtcNow.AddDays(double.Parse(configuration["Jwt:DurayionInDays"])),
                claims: claim,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"])), SecurityAlgorithms.HmacSha256)
                );

            return new JwtSecurityTokenHandler().WriteToken(Token);
           
        }
    }
}
