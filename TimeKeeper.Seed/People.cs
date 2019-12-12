using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TimeKeeper.DAL;
using TimeKeeper.Domain;

namespace TimeKeeper.Seed
{
    public static class People
    {
        public static async Task Collect(ExcelWorksheet rawData, UnitOfWork unit)
        {
            Console.Write("People: ");
            int N = 0;
            for (int row = 2; row <= rawData.Dimension.Rows; row++)
            {
                int oldId = rawData.ReadInteger(row, 1);
                Employee e = new Employee
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
                };
                await unit.People.Insert(e);
                User u = new User
                {
                    Name = e.FullName,
                    Role = rawData.ReadString(row, 14),
                    Username = e.Image,
                    Password = "$ch00L"
                };
                await unit.Users.Insert(u);
                await unit.Save();
                Utility.dicEmpl.Add(oldId, e.Id);
                N++;
            }
            Console.WriteLine(N);
        }
    }
}
