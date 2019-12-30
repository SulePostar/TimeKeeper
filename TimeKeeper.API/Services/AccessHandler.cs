using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TimeKeeper.DAL;
using TimeKeeper.Domain;

namespace TimeKeeper.API.Services
{
    public class AccessHandler
    {
        protected UnitOfWork Unit;

        public AccessHandler(UnitOfWork unit)
        {
            Unit = unit;
        }

        public async Task<User> Check(string username, string password)
        {
            User user = (await Unit.Users.Get(u => u.Username == username)).FirstOrDefault();
            string pwd = username.HashWith(password);
            if (user == null) return null;
            if (user.Password != pwd) user = null;
            return user;
        }

        public string GetToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Crypto.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim("sub", user.Id.ToString()),
                    new Claim("name", user.Name),
                    new Claim("username", user.Username),
                    new Claim("role", user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };
            return tokenHandler.WriteToken(tokenHandler.CreateJwtSecurityToken(tokenDescriptor));
        }

        public AuthenticationTicket CheckToken(string parameter, string scheme, IHeaderDictionary headers)
        {
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(parameter).Payload.ToArray();
            var claims = new[]
            {
                new Claim("id", token.FirstOrDefault(c => c.Key == "sub").Value.ToString()),
                new Claim("name", token.FirstOrDefault(c => c.Key == "name").Value.ToString()),
                new Claim("username", token.FirstOrDefault(c => c.Key == "username").Value.ToString()),
                new Claim("role", token.FirstOrDefault(c => c.Key == "role").Value.ToString()),
            };
            headers.Add("IsAuth", "true");
            headers.Add("Id", claims[0].Value);
            headers.Add("Name", claims[1].Value);
            headers.Add("Username", claims[2].Value);
            headers.Add("Role", claims[3].Value);

            ClaimsIdentity identity = new ClaimsIdentity(claims, scheme);
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);
            AuthenticationTicket ticket = new AuthenticationTicket(principal, scheme);
            return ticket;
        }
    }
}
