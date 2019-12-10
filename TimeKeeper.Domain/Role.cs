using System.Collections.Generic;

namespace TimeKeeper.Domain
{
    public class Role: BaseClass
    {
        public Role()
        {
            Members = new List<Member>();
        }

        public string Name { get; set; }
        public decimal HourlyRate { get; set; }
        public decimal MonthlyRate { get; set; }

        public virtual List<Member> Members { get; set; }
    }
}
