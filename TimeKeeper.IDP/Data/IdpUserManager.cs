using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TimeKeeper.DAL;
using TimeKeeper.Domain;

namespace TimeKeeper.IDP.Data
{
    public class IdpUserManager
    {
        public User FindUser(string username)
        {
            //TimeContext context = new TimeContext();
            //User user = context.Users.FirstOrDefault(x => x.Username == username);

            TestUser test = Config.GetUsers().FirstOrDefault(x => x.Username == username);
            User user = new User
            {
                Id = int.Parse(test.SubjectId),
                Username = test.Username,
                Password = test.Password,
                Name = test.Claims.FirstOrDefault(x => x.Type == "name").Value.ToString(),
                Role = test.Claims.FirstOrDefault(x => x.Type == "role").Value.ToString()
            };
            return user;
        }

        public bool CheckPassword(User user, string password)
        {
            // return PasswordHasher.VeryfyPassword(user.Password, password);

            return (user.Password == password);
        }

        public List<Claim> GetClaims(User user)
        {
            return new List<Claim>
            {
                new Claim("name", user.Name),
                new Claim("role", user.Role)
            };
        }
    }
}
