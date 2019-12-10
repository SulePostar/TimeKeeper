using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TimeKeeper.API.Factory;
using TimeKeeper.API.Models;
using TimeKeeper.DAL;
using TimeKeeper.Domain;

namespace TimeKeeper.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalendarController : BaseController
    {
        public CalendarController(TimeContext context) : base(context) { }

        [HttpGet("{empId}/{year}/{month}")]
        public async Task<IActionResult> Get(int empId, int year, int month)
        {
            try
            {
                List<DayModel> calendar = await CreateMonth(empId, year, month);
                var result = (await Unit.Calendar.Get(x => x.Employee.Id == empId && x.Date.Year == year && x.Date.Month == month)).ToList().Select(x => x.Create());
                foreach (var d in result)
                {
                    calendar[d.Date.Day - 1] = d;
                }
                return Ok(calendar);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        private async Task<List<DayModel>> CreateMonth(int empId, int year, int month)
        {
            List<DayModel> calendar = new List<DayModel>();
            Employee emp = await Unit.People.Get(-1);
            DateTime day = new DateTime(year, month, 1);
            while (day.Month == month)
            {
                DayModel newDay = new DayModel
                {
                    Employee = emp.Master(),
                    Date = day,
                    DayType = "empty"
                };
                if (day.DayOfWeek == DayOfWeek.Sunday || day.DayOfWeek == DayOfWeek.Saturday) newDay.DayType = "weekend";
                if (day > DateTime.Today) newDay.DayType = "future";
                if (day < emp.BeginDate || (day > emp.EndDate)) newDay.DayType = "future";
                calendar.Add(newDay);
                day = day.AddDays(1);
            }

            return calendar;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                Day day = await Unit.Calendar.Get(id);
                if (day == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(day.Create());
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Day day)
        {
            try
            {
                day.Employee = await Unit.People.Get(day.Employee.Id);
                await Unit.Calendar.Insert(day);
                await Unit.Save();
                return Ok(day.Create());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Day day)
        {
            try
            {
                await Unit.Calendar.Update(day, id);
                await Unit.Save();
                return Ok(day.Create());
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
                await Unit.Calendar.Delete(id);
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