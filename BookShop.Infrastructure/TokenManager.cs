using BookShop.Application.Dto;
using BookShop.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookShop.Infrastructure
{
    public class TokenManager : ITokenManager
    {
        private readonly IConfiguration _configuration;
        public TokenManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public TokenDto GenerateToken(string email, string username)
        {
            var key = _configuration.GetSection("tokenKey").Value;   
            var issuer = _configuration.GetSection("issuer").Value;  //normally this will be your site URL    

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var permClaims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("username", username),
                new Claim("email", email)
            };

            //Create Security Token object by giving required parameters    
            var token = new JwtSecurityToken(issuer,     
                "",  //Audience    
                permClaims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials);
            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            return new TokenDto() { Access_Token = jwtToken };
        }
    }
}
