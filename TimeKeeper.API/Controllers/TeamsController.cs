using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using TimeKeeper.API.Factory;
using TimeKeeper.DAL;
using TimeKeeper.Domain;

namespace TimeKeeper.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : BaseController
    {
        public TeamsController(TimeContext context) : base(context) { }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var query = await Unit.Teams.Get();
                return Ok(query.Select(x => x.Create(false)).ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                Team team = await Unit.Teams.Get(id);
                if (team == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(team.Create(true));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Team team)
        {
            try
            {
                await Unit.Teams.Insert(team);
                await Unit.Save();
                return Ok(team.Create(false));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Team team)
        {
            try
            {
                await Unit.Teams.Update(team, id);
                await Unit.Save();
                return Ok(team.Create(true));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await Unit.Teams.Delete(id);
                await Unit.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}