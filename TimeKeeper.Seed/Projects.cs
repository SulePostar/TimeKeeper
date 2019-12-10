using OfficeOpenXml;
using System;
using System.Threading.Tasks;
using TimeKeeper.DAL;
using TimeKeeper.Domain;

namespace TimeKeeper.Seed
{
    public static class Projects
    {
        public static async Task Collect(ExcelWorksheet rawData, UnitOfWork unit)
        {
            Console.Write("Projects: ");
            int N = 0;
            for (int row = 2; row <= rawData.Dimension.Rows; row++)
            {
                int oldId = rawData.ReadInteger(row, 1);
                Project p = new Project
                {
                    Name = rawData.ReadString(row, 3),
                    Description = rawData.ReadString(row, 4),
                    BeginDate = rawData.ReadDate(row, 5),
                    EndDate = rawData.ReadDate(row, 6),
                    Status = (ProjectStatus)rawData.ReadInteger(row, 7),
                    Customer = await unit.Customers.Get(Utility.dicCust[rawData.ReadInteger(row, 8)]),
                    Team = await unit.Teams.Get(Utility.dicTeam[rawData.ReadString(row, 9)]),
                    Pricing = (Pricing)rawData.ReadInteger(row, 10),
                    Amount = rawData.ReadDecimal(row, 11)
                };
                unit.Projects.Insert(p);
                await unit.Save();
                Utility.dicProj.Add(oldId, p.Id);
                N++;
            }
            Console.WriteLine(N);
        }
    }
}
