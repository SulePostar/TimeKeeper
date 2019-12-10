using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeKeeper.Domain;

namespace TimeKeeper.API.Models
{
    public class DayModel
    {
        public DayModel()
        {
            Details = new List<DetailModel>();
        }

        public int Id { get; set; }
        public string DayType { get; set; }
        public MasterModel Employee { get; set; }
        public DateTime Date { get; set; }
        public string Comment { get; set; }
        public decimal TotalHours { get; set; }
        public virtual List<DetailModel> Details { get; set; }
    }
}
