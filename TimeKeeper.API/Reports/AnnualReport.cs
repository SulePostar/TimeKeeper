using System.Data.Common;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeKeeper.API.Factory;
using TimeKeeper.API.Models;
using TimeKeeper.API.Services;
using TimeKeeper.DAL;
using TimeKeeper.Domain;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace TimeKeeper.API.Reports
{
    public class AnnualReport
    {
        protected UnitOfWork _unit;

        public AnnualReport(UnitOfWork unit)
        {
            _unit = unit;
        }

        public async Task<List<AnnualTimeModel>> GetAnnual(int year)
        {
            List<AnnualTimeModel> result = new List<AnnualTimeModel>();
            AnnualTimeModel total = new AnnualTimeModel { Project = new MasterModel { Id = 0, Name = "TOTAL" } };
            List<Project> projects = (await _unit.Projects.Get()).OrderBy(p => p.Name).ToList();
            foreach (Project p in projects)
            {
                List<Detail> query = p.Details.Where(d => d.Day.Date.Year == year).ToList();
                if (query.Count != 0)
                {
                    var list = query.GroupBy(d => d.Day.Date.Month)
                                    .Select(x => new { month = x.Key, hours = x.Sum(y => y.Hours) });
                    AnnualTimeModel atm = new AnnualTimeModel { Project = p.Master() };
                    foreach (var item in list)
                    {
                        atm.Hours[item.month - 1] = item.hours;
                        atm.Total += item.hours;

                        total.Hours[item.month - 1] += item.hours;
                        total.Total += item.hours;
                    }
                    total.Project.Id++;
                    result.Add(atm);
                }
            }
            result.Add(total);
            return result;
        }

        public List<AnnualTimeModel> GetStored(int year)
        {
            List<AnnualTimeModel> result = new List<AnnualTimeModel>();
            AnnualTimeModel total = new AnnualTimeModel { Project = new MasterModel { Id = 0, Name = "TOTAL" } };

            var cmd = _unit.Context.Database.GetDbConnection().CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = $"select * from AnnualReport({year})";
            if (cmd.Connection.State == ConnectionState.Closed) cmd.Connection.Open();
            DbDataReader sql = cmd.ExecuteReader();
            List<AnnualRawModel> rawData = new List<AnnualRawModel>();
            if (sql.HasRows)
            {
                while (sql.Read())
                {
                    rawData.Add(new AnnualRawModel
                    {
                        Id = sql.GetInt32(0),
                        Name = sql.GetString(1),
                        Month = sql.GetInt32(2),
                        Hours = sql.GetDecimal(3)
                    });
                }

                AnnualTimeModel atm = new AnnualTimeModel { Project = new MasterModel { Id = 0 } };
                foreach (AnnualRawModel item in rawData)
                {
                    if (atm.Project.Id != item.Id)
                    {
                        if (atm.Project.Id != 0) result.Add(atm);
                        atm = new AnnualTimeModel { Project = new MasterModel { Id = item.Id, Name = item.Name } };
                        total.Project.Id++;
                    }
                    atm.Hours[item.Month - 1] = item.Hours;
                    atm.Total += item.Hours;

                    total.Hours[item.Month - 1] += item.Hours;
                    total.Total += item.Hours;
                }
                if (atm.Project.Id != 0) result.Add(atm);
            }
            result.Add(total);
            return result;
        }
    }
}
