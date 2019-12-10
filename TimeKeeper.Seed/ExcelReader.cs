using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeper.Seed
{
    public static class ExcelReader
    {
        public static string ReadString(this ExcelWorksheet sht, int row, int col)
        {
            try
            {
                return sht.Cells[row, col].Value.ToString().Trim();
            }
            catch
            {
                return "";
            }
        }

        public static int ReadInteger(this ExcelWorksheet sht, int row, int col)
        {
            try
            {
                return int.Parse(sht.ReadString(row, col));
            }
            catch
            {
                return 0;
            }
        }

        public static DateTime ReadDate(this ExcelWorksheet sht, int row, int col)
        {
            try
            {
                return DateTime.Parse(sht.ReadString(row, col));
            }
            catch
            {
                return new DateTime(1, 1, 1);
            }
        }

        public static bool ReadBool(this ExcelWorksheet sht, int row, int col)
        {
            try
            {
                return sht.ReadString(row, col) == "-1";
            }
            catch
            {
                return false;
            }
        }

        public static decimal ReadDecimal(this ExcelWorksheet sht, int row, int col)
        {
            try
            {
                return decimal.Parse(sht.ReadString(row, col));
            }
            catch
            {
                return 0;
            }
        }
    }
}
