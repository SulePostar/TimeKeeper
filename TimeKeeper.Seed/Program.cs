using OfficeOpenXml;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TimeKeeper.DAL;
using TimeKeeper.Domain;

namespace TimeKeeper.Seed
{
    class Program
    {
        static readonly string fileLocation = @"C:\TimeKeeper\TimeKeeper.xlsx";
        static readonly string conStr = "User ID=postgres; Password=osmanaga; Server=localhost; Port=5432; Database=tracker; Integrated Security=true; Pooling=true;";
        static readonly string sqlStr = @"Server=.\SqlExpress;Database=TimeKeeper;Trusted_Connection=True;MultipleActiveResultSets=true";


        static async Task Main()
        {
            FileInfo file = new FileInfo(fileLocation);
            using (ExcelPackage package = new ExcelPackage(file))
            {
                using (UnitOfWork unit = new UnitOfWork(new TimeContext("SQL", sqlStr)))
                {
                    unit.Context.Database.EnsureDeleted();
                    unit.Context.Database.EnsureCreated();
                    unit.Context.ChangeTracker.AutoDetectChangesEnabled = false;

                    var sheets = package.Workbook.Worksheets;
                    await Teams.Collect(sheets["Teams"], unit);
                    await Roles.Collect(sheets["Roles"], unit);
                    await Customers.Collect(sheets["Customers"], unit);
                    await Projects.Collect(sheets["Projects"], unit);
                    await People.Collect(sheets["Employees"], unit);
                    await Members.Collect(sheets["Engagement"], unit);
                    await Calendar.Collect(sheets["Calendar"], unit);
                    await Details.Collect(sheets["Details"], unit);

                    foreach (Employee e in await unit.People.Get())
                    {
                        if (e.Calendar.Count != 0)
                        {
                            DateTime minD = e.Calendar.Min(x => x.Date);
                            e.BeginDate = minD;
                            if (e.Status == EmployeeStatus.Leaver)
                            {
                                DateTime maxD = e.Calendar.Max(x => x.Date);
                                e.EndDate = maxD;
                            }
                            await unit.People.Update(e, e.Id);
                        }
                    }
                    await unit.Save();

                    foreach (Project p in await unit.Projects.Get())
                    {
                        if (p.Details.Count != 0)
                        {
                            p.BeginDate = p.Details.Min(x => x.Day.Date);
                            p.EndDate = p.Details.Max(x => x.Day.Date);
                            await unit.Projects.Update(p, p.Id);
                        }
                    }
                    await unit.Save();
                }
            }
            Console.WriteLine("All tasks done.");
            Console.ReadKey();
        }
    }
}
