using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        public BaseController(TimeContext context)
        {
            Unit = new UnitOfWork(context);
            Access = new AccessHandler(Unit);
        }
    }
}