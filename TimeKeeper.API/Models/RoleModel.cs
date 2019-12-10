using System.Collections.Generic;

namespace TimeKeeper.API.Models
{
    public class RoleModel
    {
        public RoleModel()
        {
            Members = new List<MasterModel>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal HourlyRate { get; set; }
        public decimal MonthlyRate { get; set; }

        public List<MasterModel> Members { get; set; }
    }
}
