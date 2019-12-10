using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimeKeeper.API.Factory;
using TimeKeeper.API.Models;
using TimeKeeper.DAL;
using TimeKeeper.Domain;

namespace TimeKeeper.API.Controllers
{
    //[Authorize(AuthenticationSchemes = "TokenAuthentication")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController
    {
        public UsersController(TimeContext context) : base(context) { }

        [HttpGet("password")]
        public async Task<IActionResult> GetUsersAndUpdate()
        {
            var query = await Unit.Users.Get();
            foreach (User user in query)
            {
                user.Password = user.Username.HashWith(user.Password);
                Unit.Context.Entry(user).CurrentValues.SetValues(user);
            }
            await Unit.Save();
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var query = await Unit.Users.Get();
                return Ok(query.Select(x => x.Create()).ToList().OrderBy(x => x.Name));
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
                User user = await Unit.Users.Get(id);
                if (user == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(user.Create());
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User user)
        {
            try
            {
                await Unit.Users.Insert(user);
                await Unit.Save();
                return Ok(user.Create());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] User user, int id)
        {
            try
            {
                await Unit.Users.Update(user, id);
                await Unit.Save();
                return Ok(user.Create());
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
                await Unit.Users.Delete(id);
                await Unit.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("/login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            try
            {
                User user = await Access.Check(model.Username, model.Password);
                if (user != null)
                {
                    string token = Access.GetToken(user);
                    return Ok(new { user = user.Create(), token });
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}