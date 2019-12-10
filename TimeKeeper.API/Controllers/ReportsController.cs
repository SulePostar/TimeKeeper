using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TimeKeeper.API.Models;
using TimeKeeper.API.Reports;
using TimeKeeper.DAL;

namespace TimeKeeper.API.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class ReportsController : BaseController
    {
        public ReportsController(TimeContext context) : base(context) { }

        [HttpGet]
        [Route("api/employee/{empId}/month/{year}/{month}")]
        public IActionResult GetEmployeeMonth(int empId, int year, int month)
        {
            MonthlyReport mr = new MonthlyReport(Unit);
            return Ok(mr.GetEmployeeReport(empId, year, month));
        }

        [HttpGet]
        [Route("api/team/{teamId}/month/{year}/{month}")]
        public IActionResult GetTeamMonth(int teamId, int year, int month)
        {
            MonthlyReport mr = new MonthlyReport(Unit);
            return Ok(mr.GeTeamReport(teamId, year, month));
        }

        [HttpGet]
        [Route("api/monthly/{year}/{month}")]
        public IActionResult GetMonthly(int year, int month)
        {
            DateTime start = DateTime.Now;
            var mr = (new MonthlyReport(Unit)).GetMonthly(year, month);
            DateTime final = DateTime.Now;
            return Ok(new { dif = (final - start), mr });
        }

        [HttpGet]
        [Route("api/stored/{year}/{month}")]
        public IActionResult GetStored(int year, int month)
        {
            DateTime start = DateTime.Now;
            var mr = (new MonthlyReport(Unit)).GetStored(year, month);
            DateTime final = DateTime.Now;
            return Ok(new { dif = (final - start), mr });
        }

        [HttpGet]
        [Route("api/annual/{year}")]
        public IActionResult GetAnnual(int year)
        {
            DateTime start = DateTime.Now;
            var ar = (new AnnualReport(Unit)).GetAnnual(year);
            DateTime final = DateTime.Now;
            return Ok(new { dif = (final - start), ar });
        }

        [HttpGet]
        [Route("api/stored/{year}")]
        public IActionResult GetStored(int year)
        {
            DateTime start = DateTime.Now;
            var ar = (new AnnualReport(Unit)).GetStored(year);
            DateTime final = DateTime.Now;
            return Ok(new { dif = (final - start), ar });
        }
    }
}