using OfficeOpenXml;
using System;
using System.Threading.Tasks;
using TimeKeeper.DAL;
using TimeKeeper.Domain;

namespace TimeKeeper.Test.Fillers
{
    public static class Customers
    {
        public static async Task Collect(ExcelWorksheet rawData, TimeContext ctx)
        {
            for (int row = 2; row <= rawData.Dimension.Rows; row++)
            {
                Customer c = new Customer
                {
                    Name = rawData.ReadString(row, 2),
                    Image = rawData.ReadString(row, 3),
                    Contact = rawData.ReadString(row, 4),
                    Email = rawData.ReadString(row, 5),
                    Phone = rawData.ReadString(row, 6),
                    Status = (CustomerStatus)rawData.ReadInteger(row, 10)
                };
                c.Address.Road = rawData.ReadString(row, 7);
                c.Address.ZipCode = rawData.ReadString(row, 8);
                c.Address.City = rawData.ReadString(row, 9);
                c.Address.Country = rawData.ReadString(row, 9);
                ctx.Customers.Add(c);
            }
            await ctx.SaveChangesAsync();
        }
    }
}
