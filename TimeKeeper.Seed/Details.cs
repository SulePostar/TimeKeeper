using OfficeOpenXml;
using System;
using System.Threading.Tasks;
using TimeKeeper.DAL;
using TimeKeeper.Domain;

namespace TimeKeeper.Seed
{
    public static class Details
    {
        public static async Task Collect(ExcelWorksheet rawData, UnitOfWork unit)
        {
            Console.Write("Details: ");
            int N = 0;
            for (int row = 2; row <= rawData.Dimension.Rows; row++)
            {
                Detail d = new Detail
                {
                    Day = await unit.Calendar.Get(Utility.dicDays[rawData.ReadInteger(row, 4)]),
                    Project = await unit.Projects.Get(Utility.dicProj[rawData.ReadInteger(row,3)]),
                    Hours = rawData.ReadDecimal(row, 2),
                    Description = rawData.ReadString(row, 1)
                };
                unit.Details.Insert(d);
                N++;
                if (N % 100 == 0)
                {
                    Console.Write($"{N} ");
                    await unit.Save();
                }
            }
            await unit.Save();
            Console.WriteLine(N);
        }
    }
}
