using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using TimeKeeper.API.Models;
using TimeKeeper.API.Services;
using TimeKeeper.DAL;
using TimeKeeper.Domain;

namespace TimeKeeper.API.Controllers
{
    [Authorize]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected readonly UnitOfWork Unit;
        protected readonly AccessHandler Access;
        public new UserModel User;

        public BaseController(TimeContext context)
        {
            Unit = new UnitOfWork(context);
            Access = new AccessHandler(Unit);
            if (CurrentUser.Id != 0)
            {
                User = new UserModel
                {
                    Id = CurrentUser.Id,
                    Name = CurrentUser.Name,
                    Username = CurrentUser.User,
                    Role = CurrentUser.Role
                };
            }
        }
    }
}