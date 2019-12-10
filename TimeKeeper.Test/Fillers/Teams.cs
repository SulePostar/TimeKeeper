using OfficeOpenXml;
using System.Threading.Tasks;
using TimeKeeper.DAL;
using TimeKeeper.Domain;

namespace TimeKeeper.Test.Fillers
{
    public static class Teams
    {
        public static async Task Collect(ExcelWorksheet rawData, TimeContext ctx)
        {
            for (int row = 2; row <= rawData.Dimension.Rows; row++)
            {
                ctx.Teams.Add(new Team
                {
                    Name = rawData.ReadString(row, 2),
                    Description = rawData.ReadString(row, 3)
                });
            }
            await ctx.SaveChangesAsync();
        }
    }
}
