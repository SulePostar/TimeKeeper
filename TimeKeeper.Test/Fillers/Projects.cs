using OfficeOpenXml;
using System;
using System.Threading.Tasks;
using TimeKeeper.DAL;
using TimeKeeper.Domain;

namespace TimeKeeper.Test.Fillers
{
    public static class Projects
    {
        public static async Task Collect(ExcelWorksheet rawData, TimeContext ctx)
        {
            for (int row = 2; row <= rawData.Dimension.Rows; row++)
            {
                ctx.Projects.Add(new Project
                {
                    Name = rawData.ReadString(row, 3),
                    Description = rawData.ReadString(row, 4),
                    BeginDate = rawData.ReadDate(row, 5),
                    EndDate = rawData.ReadDate(row, 6),
                    Status = (ProjectStatus)rawData.ReadInteger(row, 7),
                    Customer = await ctx.Customers.FindAsync(rawData.ReadInteger(row, 8)),
                    Team = await ctx.Teams.FindAsync(rawData.ReadInteger(row, 9)),
                    Pricing = (Pricing)rawData.ReadInteger(row, 10),
                    Amount = rawData.ReadDecimal(row, 11)
                });
            }
            await ctx.SaveChangesAsync();
        }
    }
}
