using System.Collections.Generic;

namespace TimeKeeper.API.Models
{
    public class TeamModel
    {
        public TeamModel()
        {
            Members = new List<MasterModel>();
            Projects = new List<MasterModel>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int TeamSize { get; set; }
        public int ProjectSize { get; set; }
        public List<MasterModel> Members { get; set; }
        public List<MasterModel> Projects { get; set; }
    }
}
