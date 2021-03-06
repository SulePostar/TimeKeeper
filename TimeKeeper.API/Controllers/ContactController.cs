﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimeKeeper.API.Models;
using TimeKeeper.API.Services;

namespace TimeKeeper.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        [HttpPost]
        public IActionResult PostEmail([FromBody] MailModel mail)
        {
            try
            {
                string mailTo = mail.Email;
                string subject = $"Contact request from {mail.Name}";
                string body = $"{mail.Message}, {mail.Phone}";
                MailService.Send(mailTo, subject, body);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}