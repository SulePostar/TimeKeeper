using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeper.Domain
{
    public enum CustomerStatus
    {
        Prospect, Client
    }

    public enum EmployeeStatus
    {
        Trial, FullTime, PartTime, Leaver
    }

    public enum Position
    {
        CEO, HRM, MGR, DEV, QAE, UIX
    }

    public enum DayType
    {
        Workday = 1, Holiday, Busines, Religious, Sick, Vacation, Other

    }

    public enum ProjectStatus
    {
        Prospect, InProgress, OnHold, Fnished, Canceled
    }

    public enum Pricing
    {
        FixedBid, Hourly, Monthly
    }
}
