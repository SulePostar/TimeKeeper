using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TimeKeeper.API.Controllers
{
    [Route("/")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        public IActionResult Get()
        {
            return Ok("Welcome to the world of time keepers!");
        }

        [HttpGet("AccessDenied")]
        public IActionResult AccessDenied()
        {
            return Ok("It seems you don't have access to this page!");
        }

        [HttpGet("/test")]
        public async Task<IActionResult> TestRoute()
        {
            string data = "No response!";
            using(HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage res = await client.GetAsync("http://www.saray.net"))
                {
                    using(HttpContent content = res.Content)
                    {
                        data = await content.ReadAsStringAsync();
                    }
                }
            }
            return Ok(data);
        }
    }
}