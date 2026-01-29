using Application.Interfaces;
using Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Auth
{
    public class JwtService(IOptions<AuthSettings> options) : IJwtInterface
    {
        public string GenerateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim("userEmail", user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role)
            };
            // Token generation logic goes here (omitted for brevity)
            var token = new JwtSecurityToken(
                expires: DateTime.UtcNow.Add(options.Value.Expires),
                claims: claims,
                signingCredentials:
                new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Value.SecretKey!)),
                    SecurityAlgorithms.HmacSha256));
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<Guid> GetIdFromClaim(string token)
        {
            var guid = token.FirstOrDefault(x => x.Equals(ClaimTypes.NameIdentifier)).ToString();
            Guid response = Guid.Parse(guid);
            return response;
        }
    }
}
