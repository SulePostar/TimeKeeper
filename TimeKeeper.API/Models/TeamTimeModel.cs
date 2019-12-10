using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeKeeper.API.Models
{
    public class TeamTimeModel
    {
        public TeamTimeModel()
        {
            Employees = new List<EmployeeTimeModel>();
        }
        public MasterModel Team { get; set; }
        public IList<EmployeeTimeModel> Employees { get; set; }
    }
}
