﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeKeeper.Domain;

namespace TimeKeeper.API.Models
{
    public class EmployeeTimeModel
    {
        public EmployeeTimeModel()
        {
            HourTypes = new Dictionary<string, decimal>();
            for (DayType d = DayType.Workday; d <= DayType.Other; d++)
            {
                HourTypes.Add(d.ToString(), 0);
            }
        }
        public MasterModel Employee { get; set; }
        public decimal PTOHours { get; set; }
        public decimal Overtime { get; set; }
        public decimal TotalHours { get; set; }
        public Dictionary<string, decimal> HourTypes { get; set; }
    }
}