using OfficeOpenXml;
using System;
using System.Threading.Tasks;
using TimeKeeper.DAL;
using TimeKeeper.Domain;

namespace TimeKeeper.Test.Fillers
{
    public static class Members
    {
        public static async Task Collect(ExcelWorksheet rawData, TimeContext ctx)
        {
            for (int row = 2; row <= rawData.Dimension.Rows; row++)
            {
                ctx.Members.Add(new Member
                {
                    Employee = await ctx.People.FindAsync(rawData.ReadInteger(row, 1)),
                    Team = await ctx.Teams.FindAsync(rawData.ReadInteger(row, 2)),
                    Role = await ctx.Roles.FindAsync(rawData.ReadInteger(row, 3)),
                    HoursWeekly = rawData.ReadInteger(row, 4)
                });
            }
            await ctx.SaveChangesAsync();
        }
    }
}
