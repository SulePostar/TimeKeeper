using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace TimeKeeper.Domain
{
    [Table("Calendar")]
    public class Day: BaseClass
    {
        public Day()
        {
            Details = new List<Detail>();
        }

        public DayType DayType { get; set; }
        public virtual Employee Employee { get; set; }
        public DateTime Date { get; set; }
        public string Comment { get; set; }
        [NotMapped]
        public decimal TotalHours
        {
            get
            {
                if (DayType == DayType.Workday) { return Details.Sum(x => x.Hours); } else { return 8; };
            }
        }

        public virtual IList<Detail> Details { get; set; }
    }
}
