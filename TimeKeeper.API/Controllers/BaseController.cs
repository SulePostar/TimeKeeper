using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using TimeKeeper.API.Models;
using TimeKeeper.API.Services;
using TimeKeeper.DAL;

namespace TimeKeeper.API.Controllers
{
    [Authorize]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected readonly UnitOfWork Unit;
        protected readonly AccessHandler Access;
        protected UserModel CurrentUser;

        public BaseController(TimeContext context)
        {
            Unit = new UnitOfWork(context);
            Access = new AccessHandler(Unit);

            if (User != null)
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier);
                var userName = User.FindFirst(ClaimTypes.Name);
                //CurrentUser = new UserModel();
                //if (User != null)
                //{
                //    CurrentUser.Id = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "sub").Value);
                //    CurrentUser.Name = User.Claims.FirstOrDefault(c => c.Type == "name").Value;
                //    CurrentUser.Username = User.Claims.FirstOrDefault(c => c.Type == "username").Value;
                //    CurrentUser.Role = User.Claims.FirstOrDefault(c => c.Type == "role").Value;
                //}
            }
        }
    }
}