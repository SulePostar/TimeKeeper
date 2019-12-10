using OfficeOpenXml;
using System;
using System.Threading.Tasks;
using TimeKeeper.DAL;
using TimeKeeper.Domain;

namespace TimeKeeper.Seed
{
    public static class Members
    {
        public static async Task Collect(ExcelWorksheet rawData, UnitOfWork unit)
        {
            Console.Write("Members: ");
            int N = 0;
            for (int row = 2; row <= rawData.Dimension.Rows; row++)
            {
                Member m = new Member
                {
                    Employee = await unit.People.Get(Utility.dicEmpl[rawData.ReadInteger(row, 1)]),
                    Team = await unit.Teams.Get(Utility.dicTeam[rawData.ReadString(row, 2)]),
                    Role = await unit.Roles.Get(Utility.dicRole[rawData.ReadString(row, 3)]),
                    HoursWeekly = rawData.ReadInteger(row, 4)
                };
                unit.Members.Insert(m);
                N++;
            }
            await unit.Save();
            Console.WriteLine(N);
        }
    }
}
