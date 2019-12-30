using NUnit.Framework;
using OfficeOpenXml;
using System.IO;
using System.Threading.Tasks;
using TimeKeeper.DAL;
using TimeKeeper.Domain;
using TimeKeeper.Test.Fillers;

namespace TimeKeeper.Test
{
    public static class Database
    {
        public static IRepository<Role> roles;
        public static IRepository<Team> teams;

        public static TimeContext Context;
        public static string fileLocation;

        public static async Task Setup()
        {
            fileLocation = @"C:\TimeKeeper\TimeTest.xlsx";
            Context = new TimeContext("PGS", "User ID=postgres; Password=osmanaga; Server=localhost; Port=5432; Database=testera; Integrated Security=true; Pooling=true;");
            Context.Database.EnsureDeleted();
            Context.Database.EnsureCreated();
            FileInfo file = new FileInfo(fileLocation);
            using (ExcelPackage package = new ExcelPackage(file))
            {
                var sheets = package.Workbook.Worksheets;
                await Teams.Collect(sheets["Teams"], Context);
                await Roles.Collect(sheets["Roles"], Context);
                await Customers.Collect(sheets["Customers"], Context);
                await Projects.Collect(sheets["Projects"], Context);
                await People.Collect(sheets["Employees"], Context);
                await Members.Collect(sheets["Engagement"], Context);
            }

            roles = new Repository<Role>(Context);
            teams = new Repository<Team>(Context);

            Assert.Pass();
        }
    }
}
