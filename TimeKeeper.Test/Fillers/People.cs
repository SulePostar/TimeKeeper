using OfficeOpenXml;
using System.Threading.Tasks;
using TimeKeeper.DAL;
using TimeKeeper.Domain;

namespace TimeKeeper.Test.Fillers
{
    public static class People
    {
        public static async Task Collect(ExcelWorksheet rawData, TimeContext ctx)
        {
            for (int row = 2; row <= rawData.Dimension.Rows; row++)
            {
                ctx.People.Add(new Employee
                {
                    FirstName = rawData.ReadString(row, 2),
                    LastName = rawData.ReadString(row, 3),
                    Image = rawData.ReadString(row, 4),
                    Email = rawData.ReadString(row, 6),
                    Phone = rawData.ReadString(row, 7),
                    BirthDay = rawData.ReadDate(row, 8),
                    BeginDate = rawData.ReadDate(row, 9),
                    EndDate = rawData.ReadDate(row, 10),
                    Status = (EmployeeStatus)rawData.ReadInteger(row, 11),
                    Position = rawData.ReadString(row, 12)
                });
                ctx.Users.Add(new User
                {
                    Name = rawData.ReadString(row, 2) + " " + rawData.ReadString(row, 3),
                    Role = rawData.ReadString(row, 14),
                    Username = rawData.ReadString(row, 4),
                    Password = "$ch00L"
                });
            }
            await ctx.SaveChangesAsync();
        }
    }
}
