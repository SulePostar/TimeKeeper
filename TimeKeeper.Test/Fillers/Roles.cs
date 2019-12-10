using OfficeOpenXml;
using System.Threading.Tasks;
using TimeKeeper.DAL;
using TimeKeeper.Domain;

namespace TimeKeeper.Test.Fillers
{
    public static class Roles
    {
        public static async Task Collect(ExcelWorksheet rawData, TimeContext ctx)
        {
            for (int row = 2; row <= rawData.Dimension.Rows; row++)
            {
                ctx.Roles.Add(new Role
                {
                    Name = rawData.ReadString(row, 2),
                    HourlyRate = rawData.ReadDecimal(row, 3),
                    MonthlyRate = rawData.ReadDecimal(row, 4)
                });
            }
            await ctx.SaveChangesAsync();
        }
    }
}
