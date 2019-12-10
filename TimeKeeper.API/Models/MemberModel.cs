namespace TimeKeeper.API.Models
{
    public class MemberModel
    {
        public int Id { get; set; }
        public MasterModel Employee { get; set; }
        public MasterModel Team { get; set; }
        public MasterModel Role { get; set; }
        public int HoursWeekly { get; set; }
    }

    public class MemberEntry
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int TeamId { get; set; }
        public int RoleId { get; set; }
        public int HoursWeekly { get; set; }
    }
}
