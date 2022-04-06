using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Squares.API.DataLayer.Entities;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Squares.API.Domain.Manager
{
    public class TokenGeneration : ITokenGeneration
    {
        private IConfiguration _config;
        public TokenGeneration(IConfiguration config)
        {
            _config = config;
        }
        public string GenerateToken(UserDetail user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Role,user.Role),
                new Claim("Id",user.Id.ToString())
            };

            var token = new JwtSecurityToken(_config["JWT:Issuer"]
                , _config["JWT:Audience"]
                , claims
                , expires: DateTime.UtcNow.AddMinutes(15)
                , signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
