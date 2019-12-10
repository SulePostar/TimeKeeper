using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using TimeKeeper.API.Factory;
using TimeKeeper.API.Models;
using TimeKeeper.API.Services;
using TimeKeeper.DAL;
using TimeKeeper.Domain;

namespace TimeKeeper.API.Reports
{
    public class MonthlyReport
    {
        protected UnitOfWork _unit;
        public MonthlyReport(UnitOfWork unit)
        {
            _unit = unit;
        }

        public async Task<EmployeeTimeModel> GetEmployeeReport(int empId, int year, int month)
        {
            Employee emp = await _unit.People.Get(empId);
            EmployeeTimeModel result = new EmployeeTimeModel
            {
                Employee = emp.Master()
            };
            List<Day> list = emp.Calendar.Where(c => c.Date.Month == month && c.Date.Year == year).ToList();
            var query = list.GroupBy(c => c.DayType.ToString()).Select(d => new { type = d.Key, hours = d.Sum(h => h.TotalHours) });
            foreach (var d in query) result.HourTypes[d.type] = d.hours;
            result.TotalHours = list.Sum(h => h.TotalHours);
            result.PTOHours = list.Where(d => d.DayType != DayType.Workday).Sum(h => h.TotalHours);
            result.Overtime = list.Where(d => d.Date.Weekend()).Sum(h => h.TotalHours)
                            + list.Where(d => !d.Date.Weekend() && d.TotalHours > 8).Sum(h => (h.TotalHours - 8));
            return result;
        }

        public async Task<TeamTimeModel> GeTeamReport(int teamId, int year, int month)
        {
            Team team = await _unit.Teams.Get(teamId);
            TeamTimeModel result = new TeamTimeModel
            {
                Team = team.Master()
            };
            List<int> members = team.Members.Select(m => m.Employee.Id).ToList();
            foreach (int empId in members)
            {
                EmployeeTimeModel e = await GetEmployeeReport(empId, year, month);
                if (e.TotalHours != 0) result.Employees.Add(e);
            }
            return result;
        }

        public async Task<ProjectMonthlyModel> GetMonthly(int year, int month)
        {
            ProjectMonthlyModel pmm = new ProjectMonthlyModel();

            var source = (await _unit.Details.Get(d => d.Day.Date.Year == year && d.Day.Date.Month == month)).ToList();
            var tasks = source.OrderBy(x => x.Day.Employee.Id)
                              .ThenBy(x => x.Project.Id)
                              .ToList();

            pmm.Projects = tasks.GroupBy(p => new { p.Project.Id, p.Project.Name })
                                .Select(p => new MasterModel { Id = p.Key.Id, Name = p.Key.Name })
                                .ToList();
            List<int> pList = pmm.Projects.Select(p => p.Id).ToList();

            var details = tasks.GroupBy(x => new {
                empId = x.Day.Employee.Id,
                empName = x.Day.Employee.FullName,
                projId = x.Project.Id,
                projName = x.Project.Name
            })
                                            .Select(y => new
                                            {
                                                employee = new MasterModel { Id = y.Key.empId, Name = y.Key.empName },
                                                project = new MasterModel { Id = y.Key.projId, Name = y.Key.projName },
                                                hours = y.Sum(h => h.Hours)
                                            }).ToList();

            EmployeeProjectModel epm = new EmployeeProjectModel(pList) { Employee = new MasterModel { Id = 0 } };
            foreach (var item in details)
            {
                if (epm.Employee.Id != item.employee.Id)
                {
                    if (epm.Employee.Id != 0) pmm.Employees.Add(epm);
                    epm = new EmployeeProjectModel(pList) { Employee = item.employee };
                }
                epm.Hours[item.project.Id] += item.hours;
                epm.TotalHours += item.hours;
            }
            if (epm.Employee.Id != 0) pmm.Employees.Add(epm);

            return pmm;
        }

        public ProjectMonthlyModel GetStored(int year, int month)
        {
            ProjectMonthlyModel pmm = new ProjectMonthlyModel();

            var cmd = _unit.Context.Database.GetDbConnection().CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = $"select * from MonthlylReport({year}, {month})";
            if (cmd.Connection.State == ConnectionState.Closed) cmd.Connection.Open();
            DbDataReader sql = cmd.ExecuteReader();
            List<ProjectRawModel> rawData = new List<ProjectRawModel>();

            if (sql.HasRows)
            {
                while (sql.Read())
                {
                    rawData.Add(new ProjectRawModel
                    {
                        EmpId = sql.GetInt32(0),
                        FirstName = sql.GetString(1),
                        LastName = sql.GetString(2),
                        ProjId = sql.GetInt32(3),
                        ProjName = sql.GetString(4),
                        Hours = sql.GetDecimal(5)
                    });
                }
                pmm.Projects = rawData.GroupBy(p => new { p.ProjId, p.ProjName })
                                      .OrderBy(p => p.Key.ProjId)
                                      .Select(p => new MasterModel { Id = p.Key.ProjId, Name = p.Key.ProjName })
                                      .ToList();
                List<int> pList = pmm.Projects.Select(p => p.Id).ToList();

                EmployeeProjectModel epm = new EmployeeProjectModel(pList) { Employee = new MasterModel { Id = 0 } };
                foreach (var item in rawData)
                {
                    if (epm.Employee.Id != item.EmpId)
                    {
                        if (epm.Employee.Id != 0) pmm.Employees.Add(epm);
                        epm = new EmployeeProjectModel(pList) { Employee = new MasterModel { Id = item.EmpId, Name = item.FirstName + " " + item.LastName } };
                    }
                    epm.Hours[item.ProjId] += item.Hours;
                    epm.TotalHours += item.Hours;
                }
                if (epm.Employee.Id != 0) pmm.Employees.Add(epm);
            }
            return pmm;
        }
    }
}
