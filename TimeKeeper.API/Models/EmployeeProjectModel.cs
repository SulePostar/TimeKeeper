using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeKeeper.API.Models
{
    public class EmployeeProjectModel
    {
        public EmployeeProjectModel(List<int> projects)
        {
            Hours = new Dictionary<int, decimal>();
            foreach (int p in projects) Hours.Add(p, 0);
        }
        public MasterModel Employee { get; set; }
        public decimal TotalHours { get; set; }
        public Dictionary<int, decimal> Hours { get; set; }
    }
}
