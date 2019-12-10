using System.Collections.Generic;

namespace TimeKeeper.Domain
{
    public class Team: BaseClass
    {
        public Team()
        {
            Projects = new List<Project>();
            Members = new List<Member>();
        }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual List<Project> Projects { get; set; }
        public virtual List<Member> Members { get; set; }
    }
}
