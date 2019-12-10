using System;

namespace TimeKeeper.API.Models
{
    public class ProjectModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public MasterModel Customer { get; set; }
        public MasterModel Team { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
        public string Pricing { get; set; }
        public decimal Amount { get; set; }
    }
}
