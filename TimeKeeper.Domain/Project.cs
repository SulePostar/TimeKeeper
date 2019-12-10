using System;
using System.Collections.Generic;

namespace TimeKeeper.Domain
{
    public class Project: BaseClass
    {
        public Project()
        {
            Details = new List<Detail>();
        }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Team Team { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public ProjectStatus Status { get; set; }
        public Pricing Pricing { get; set; }
        public decimal Amount { get; set; }

        public virtual IList<Detail> Details { get; set; }
    }
}
