using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using TimeKeeper.API.Factory;
using TimeKeeper.DAL;

namespace TimeKeeper.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasterController : BaseController
    {
        public MasterController(TimeContext context) : base(context) { }

        [HttpGet("teams")]
        public async Task<IActionResult> GetTeams()
        {
            var query = await Unit.Teams.Get();
            return Ok(query.Select(x => x.Master()).ToList());
        }

        [HttpGet("roles")]
        public async Task<IActionResult> GetRoles()
        {
            var query = await Unit.Roles.Get();
            return Ok(query.Select(x => x.Master()).ToList());
        }

        [HttpGet("customers")]
        public async Task<IActionResult> GetCustomers()
        {
            var query = await Unit.Customers.Get();
            return Ok(query.Select(x => x.Master()).ToList());
        }

        [HttpGet("projects")]
        public async Task<IActionResult> GetProjects()
        {
            var query = await Unit.Projects.Get();
            return Ok(query.Select(x => x.Master()).ToList());
        }

        [HttpGet("people")]
        public async Task<IActionResult> GetPeople()
        {
            var query = await Unit.People.Get();
            return Ok(query.Select(x => x.Master()).ToList());
        }
    }
}